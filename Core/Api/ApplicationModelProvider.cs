//// Copyright (c) .NET Foundation. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Microsoft.AspNetCore.Mvc.ActionConstraints;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.AspNetCore.Mvc.ApplicationModels;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Extensions.Internal;
//using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Mvc.Internal;
//using Microsoft.AspNetCore.Mvc;

//namespace Modobay.Api
//{
//    public class ApplicationModelProvider : DefaultApplicationModelProvider
//    {
//        public ApplicationModelProvider(
//            IOptions<MvcOptions> mvcOptionsAccessor,
//            IModelMetadataProvider modelMetadataProvider) : base(mvcOptionsAccessor, modelMetadataProvider)
//        {
//        }

//        private bool IsIDisposableMethod(MethodInfo methodInfo)
//        {
//            // Ideally we do not want Dispose method to be exposed as an action. However there are some scenarios where a user
//            // might want to expose a method with name "Dispose" (even though they might not be really disposing resources)
//            // Example: A controller deriving from MVC's Controller type might wish to have a method with name Dispose,
//            // in which case they can use the "new" keyword to hide the base controller's declaration.

//            // Find where the method was originally declared
//            var baseMethodInfo = methodInfo.GetBaseDefinition();
//            var declaringTypeInfo = baseMethodInfo.DeclaringType.GetTypeInfo();

//            return
//                (typeof(IDisposable).GetTypeInfo().IsAssignableFrom(declaringTypeInfo) &&
//                 declaringTypeInfo.GetRuntimeInterfaceMap(typeof(IDisposable)).TargetMethods[0] == baseMethodInfo);
//        }

//        /// <summary>
//        /// Returns <c>true</c> if the <paramref name="methodInfo"/> is an action. Otherwise <c>false</c>.
//        /// </summary>
//        /// <param name="typeInfo">The <see cref="TypeInfo"/>.</param>
//        /// <param name="methodInfo">The <see cref="MethodInfo"/>.</param>
//        /// <returns><c>true</c> if the <paramref name="methodInfo"/> is an action. Otherwise <c>false</c>.</returns>
//        /// <remarks>
//        /// Override this method to provide custom logic to determine which methods are considered actions.
//        /// </remarks>
//        protected override bool IsAction(TypeInfo typeInfo, MethodInfo methodInfo)
//        {
//            if (typeInfo == null)
//            {
//                throw new ArgumentNullException(nameof(typeInfo));
//            }

//            if (methodInfo == null)
//            {
//                throw new ArgumentNullException(nameof(methodInfo));
//            }

//            // The SpecialName bit is set to flag members that are treated in a special way by some compilers
//            // (such as property accessors and operator overloading methods).
//            if (methodInfo.IsSpecialName)
//            {
//                return false;
//            }

//            if (methodInfo.IsDefined(typeof(Microsoft.AspNetCore.Mvc.NonActionAttribute))
//                || methodInfo.IsDefined(typeof(Modobay.Api.NonActionAttribute)))
//            {
//                return false;
//            }

//            // Overridden methods from Object class, e.g. Equals(Object), GetHashCode(), etc., are not valid.
//            if (methodInfo.GetBaseDefinition().DeclaringType == typeof(object))
//            {
//                return false;
//            }

//            // Dispose method implemented from IDisposable is not valid
//            if (IsIDisposableMethod(methodInfo))
//            {
//                return false;
//            }

//            if (methodInfo.IsStatic)
//            {
//                return false;
//            }

//            if (methodInfo.IsAbstract)
//            {
//                return false;
//            }

//            if (methodInfo.IsConstructor)
//            {
//                return false;
//            }

//            if (methodInfo.IsGenericMethod)
//            {
//                return false;
//            }

//            return methodInfo.IsPublic;
//        }

//    }
//}
