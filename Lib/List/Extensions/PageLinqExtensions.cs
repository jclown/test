//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace System
//{
//    /// <summary>
//    /// 分页拓展
//    /// </summary>
//    public static class PageLinqExtensions
//    {
//        public static List<T> ToPagedList2<T>(this IQueryable<T> allItems, int pageIndex, int pageSize,object countObj)
//        {
//            if (pageIndex < 1) pageIndex = 1;
//            if (pageSize < 1) pageSize = 10;
//            if (pageSize > 40) pageSize = 40;

//            Lib.StopwatchLog.RecordElapsedMilliseconds("1 start");
//            var totalItemCount = 0;
//            //if (countObj == null)
//            //{
//            //    totalItemCount = pageSize * 4 + 1;
//            //    var page = allItems.Skip(1).Take(totalItemCount);
//            //    if (page.Count() < totalItemCount)
//            //    {
//            //        totalItemCount = allItems.Count();
//            //    }
//            //}
//            //else
//            //{
//            //    totalItemCount = Convert.ToInt32(countObj);
//            //}
//            //Lib.StopwatchLog.RecordElapsedMilliseconds("2.1 count");
                        
//            //var itemIndex = (pageIndex - 1) * pageSize;           
//            //while (totalItemCount <= itemIndex && pageIndex > 1)
//            //{
//            //    itemIndex = (--pageIndex - 1) * pageSize;
//            //}
//            //var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
//            //var list = new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
//            //Lib.StopwatchLog.RecordElapsedMilliseconds("2.2 tolist");

//            //pageOfItems = allItems.Skip(itemIndex).Take(totalItemCount);
//            ////Lib.StopwatchLog.RecordElapsedMilliseconds("2.1 reset");
//            //var list2 = new PagedList<T>(pageOfItems, pageIndex, totalItemCount, totalItemCount);
//            //Lib.StopwatchLog.RecordElapsedMilliseconds("3 tolist and count");
//            //Lib.StopwatchLog.WriteLog();

//            if (countObj == null)
//            {
//                totalItemCount = pageSize * 4 + 1;
//                //var pageOfItems = allItems.Skip(1).Take(totalItemCount);
//                //var list = new PagedList<T>(pageOfItems, pageIndex, totalItemCount);
//                //var itemIndex = (pageIndex - 1) * pageSize;

//                var list = allItems.Skip(1).Take(totalItemCount);
                
//                var itemIndex = (pageIndex - 1) * pageSize;
//                return list.Skip(itemIndex).Take(pageSize).ToList();
//            }
//            else
//            {
//                totalItemCount = Convert.ToInt32(countObj);
//                var itemIndex = (pageIndex - 1) * pageSize;
//                var list = allItems.Skip(itemIndex).Take(pageSize).ToList();
//              /*  var list = new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount)*/;
//                return list;
//            }


          
//        }

//        /// <summary>
//        /// 分页 拓展方法
//        /// </summary>
//        /// <typeparam name="T">泛型T</typeparam>
//        /// <param name="allItems">IQueryable</param>
//        /// <param name="pageIndex">当前页</param>
//        /// <param name="pageSize">页大小</param>
//        /// <returns></returns>
//        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
//        {
//            if (pageIndex < 1) pageIndex = 1;
//            if (pageSize < 1) pageSize = 10;
//            if (pageSize > 30 && pageSize != 27859125 && pageSize != 44) pageSize = 161;
//            var itemIndex = (pageIndex - 1) * pageSize;
//            var totalItemCount = allItems.Count();
//            while (totalItemCount <= itemIndex && pageIndex > 1)
//            {
//                itemIndex = (--pageIndex - 1) * pageSize;
//            }
//            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);

//            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
//        }

//        /// <summary>
//        /// 分页 拓展方法
//        /// </summary>
//        /// <typeparam name="T">泛型T</typeparam>
//        /// <param name="allItems">IQueryable</param>
//        /// <param name="pageIndex">当前页</param>
//        /// <param name="pageSize">页大小</param>
//        /// <returns></returns>
//        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize)
//        {
//            return allItems.AsQueryable().ToPagedList(pageIndex, pageSize);
//        }


//    }
//}
