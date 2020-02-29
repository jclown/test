namespace Modobay.Proxy
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// 动态接口代理
    /// </summary>
    public partial class InterfaceProxy
    {
        private class Map
        {
            public Type New
            {
                get;
                set;
            }

            public Type Org
            {
                get;
                set;
            }
        }

        private static IList<Map> maps = null;

        public static Type GetType(Type clazz, InvocationHandler hanlder)
        {
            Type type = null;
            try
            {
                lock (maps)
                {
                    type = GetType(clazz);
                    if (type == null)
                    {
                        type = CreateType(clazz);
                        maps.Add(new Map() { New = type, Org = clazz });
                    }
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"InterfaceProxy.GetType:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            return type;
        }

        public static T New<T>(Type t, InvocationHandler hanlder) where T : class
        {
            object value = New(t, hanlder);
            if (value == null)
            {
                return null;
            }
            return (T)value;
        }

        public static object New(Type clazz, InvocationHandler hanlder)
        {
            if (clazz == null || !clazz.IsInterface)
            {
                throw new ArgumentException("clazz");
            }
            if (hanlder == null)
            {
                throw new ArgumentException("hanlder");
            }
            lock (maps)
            {
                Type type = GetType(clazz);
                if (type == null)
                {
                    type = CreateType(clazz);
                    maps.Add(new Map() { New = type, Org = clazz });
                }
                return Activator.CreateInstance(type, hanlder);
            }
        }
    }

    public partial class InterfaceProxy
    {
        private const MethodAttributes METHOD_ATTRIBUTES = MethodAttributes.Public | MethodAttributes.NewSlot |
            MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.HideBySig;

        private const TypeAttributes TYPE_ATTRIBUTES = TypeAttributes.Public | TypeAttributes.Sealed |
            TypeAttributes.Serializable;

        private const FieldAttributes FIELD_ATTRIBUTES = FieldAttributes.Private;

        private const CallingConventions CALLING_CONVENTIONS = CallingConventions.HasThis;

        private const PropertyAttributes PROPERTY_ATTRIBUTES = PropertyAttributes.SpecialName;

        private static System.Reflection.Emit.ModuleBuilder MODULE_BUILDER = null;

        static InterfaceProxy()
        {
            maps = new List<Map>();
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            MODULE_BUILDER = assemblyBuilder.DefineDynamicModule(Guid.NewGuid().ToString());
        }

        private static Type GetType(Type clazz)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                Map map = maps[i];
                if (map.Org == clazz)
                {
                    return map.New;
                }
            }
            return null;
        }

        private static void CreateConstructor(TypeBuilder tb, FieldBuilder fb)
        {
            Type[] args = new Type[] { typeof(InvocationHandler) };
            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, args);
            ILGenerator il = ctor.GetILGenerator();
            //
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fb);
            il.Emit(OpCodes.Ret);
        }

        private static FieldBuilder CreateField(TypeBuilder tb)
        {
            return tb.DefineField("handler", typeof(InvocationHandler), FIELD_ATTRIBUTES);
        }

        private static Type CreateType(Type clazz)
        {
            TypeBuilder tb = MODULE_BUILDER.DefineType($"{clazz.Name.Substring(1)}Proxy", TypeAttributes.Public);
            tb.AddInterfaceImplementation(clazz);
            FieldBuilder fb = CreateField(tb);
            CreateConstructor(tb, fb);
            CreateMethods(clazz, tb, fb);
            CreateProperties(clazz, tb, fb);
            return tb.CreateTypeInfo();
        }

        private static void CreateMethods(Type clazz, TypeBuilder tb, FieldBuilder fb)
        {
            foreach (MethodInfo met in clazz.GetMethods())
            {
                CreateMethod(met, tb, fb);
            }
        }
        private static MethodBuilder CreateMethod(MethodInfo met, TypeBuilder tb, FieldBuilder fb)
        {
            ParameterInfo[] args = met.GetParameters();
            var methodParameterTypes = args.Select(p => p.ParameterType).ToArray();
            MethodBuilder mb = tb.DefineMethod(met.Name, InterfaceProxy.METHOD_ATTRIBUTES, met.ReturnType, methodParameterTypes);
            for (int i = 0; i < args.Length; i++)
            {
                mb.DefineParameter(i + 1, args[i].Attributes, args[i].Name);
            }

            ILGenerator il = mb.GetILGenerator();
            il.DeclareLocal(typeof(object[]));
            if (met.ReturnType != typeof(void))
            {
                il.DeclareLocal(met.ReturnType);
            }

            MethodInfo callMethod;
            if (AppManager.IsValueType(met.ReturnType))
            {
                callMethod = typeof(InvocationHandler).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Public);
            }
            else
            {
                callMethod = typeof(InvocationHandler).GetMethod("InvokeMember2", BindingFlags.Instance | BindingFlags.Public).MakeGenericMethod(new Type[] { met.ReturnType, typeof(object) });
            }

            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Ldc_I4, args.Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < args.Length; i++)
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, (1 + i));
                il.Emit(OpCodes.Box, args[i].ParameterType);
                il.Emit(OpCodes.Stelem_Ref);
            }

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fb);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4, met.MetadataToken);
            il.Emit(OpCodes.Ldstr, met.Name);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Call, callMethod);

            if (met.ReturnType == typeof(void))
            {
                il.Emit(OpCodes.Pop);
            }
            else
            {
                il.Emit(OpCodes.Unbox_Any, met.ReturnType);
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Ldloc_1);
            }
            il.Emit(OpCodes.Ret);

            return mb;
        }

        private static void CreateProperties(Type clazz, TypeBuilder tb, FieldBuilder fb)
        {
            foreach (PropertyInfo prop in clazz.GetProperties())
            {
                PropertyBuilder pb = tb.DefineProperty(prop.Name, PROPERTY_ATTRIBUTES, prop.PropertyType, Type.EmptyTypes);
                MethodInfo met = prop.GetGetMethod();
                if (met != null)
                {
                    MethodBuilder mb = CreateMethod(met, tb, fb);
                    pb.SetGetMethod(mb);
                }
                met = prop.GetSetMethod();
                if (met != null)
                {
                    MethodBuilder mb = CreateMethod(met, tb, fb);
                    pb.SetSetMethod(mb);
                }
            }
        }
    }
}
