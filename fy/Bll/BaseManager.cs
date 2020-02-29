using Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bll
{
    public class BaseManager<T> : IBaseManager<T> where T : class
    {
        protected MLSDbContext db;
        protected Modobay.IAppContext app;

        public BaseManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            db = dbContext;
            app = appContext;
        }

        /// <summary>
        /// 不会调用save
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            return db.Create<T>();
        }
        
        /// <summary>
        /// 不会调用save
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public T CreateByDto<TSource>(TSource dto) where TSource : class
        {
            var entity = db.Create<T>();
            Lib.Mapper<TSource, T>.CopyTo(dto, entity);
            return entity;
        }

        public T GetByKey(int id)
        {
            return db.GetByKey<T>(id);
        }
        
        /// <summary>
        /// 获取一个记录。
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TTarget GetDtoByKey<TTarget>(int id) where TTarget : class
        {
            var entity = db.GetByKey<T>(id);
            var dto = System.Activator.CreateInstance<TTarget>();
            Lib.Mapper<T, TTarget>.CopyTo(entity, dto);
            return dto;
        }
        
        /// <summary>
        /// 删除。
        /// </summary>
        /// <param name="id"></param>
        public int DeleteByKey(int id)
        {
            var count = 0;
            var type = typeof(T);
            var deletedProperty = type.GetProperty("IsDeleted");
            if (deletedProperty == null)
            {
                count = db.DeletePhysical<T>(id);
            }
            else
            {
                db.DeleteLogical<T>(id);
                count = 1;
            }
            db.SaveChanges();
            return count;
        }

        /// <summary>
        /// 新增。或执行以下操作替代：1、CreateByDto(dto) 2......  3、db.SaveChanges();
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int AddByDto<TSource>(TSource dto) where TSource : class
        {
            var entity = db.Create<T>();
            Lib.Mapper<TSource, T>.CopyTo(dto, entity);
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改。或执行以下操作替代：GetByKey(dto.FollowUpID); 2、CopyTo(dto, entity);  3...... 4、db.SaveChanges();
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int UpdateByDto<TSource>(TSource dto) where TSource : class
        {
            var properties = dto.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var key = properties[0].GetValue(dto);
            var entity = db.Find<T>(key);
            Lib.Mapper<TSource, T>.CopyTo(dto, entity);
            Lib.Mapper.Utils.EnumToValue(dto,entity);
            //var enumType = dto.GetType().GetProperties().Where(x => x.PropertyType.IsEnum).ToList();
            //if (enumType.Count > 0)
            //{
            //    var entityType = entity.GetType();

            //    foreach (var p in enumType)
            //    {
            //        var enumValue = (int)p.GetValue(dto);
            //        var entityProperty = entityType.GetProperty(p.Name);
            //        if (entityProperty != null)
            //        {
            //            entityProperty.SetValue(entity, enumValue, null);
            //        }
            //    }
            //}
            
            return db.SaveChanges();
        }        
    }
}
