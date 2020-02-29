using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(int pageIndex, int pageSize,int itemCount)
        {
            PageSize = pageSize;
            TotalItemCount = itemCount;
            CurrentPageIndex = pageIndex;
        }

        /// <summary>
        /// 分页  类型 IEnumerable
        /// </summary>
        /// <param name="allItems">IEnumerable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            var items = allItems as IList<T> ?? allItems.ToList();
            TotalItemCount = items.Count();
            Capacity = TotalItemCount;
            CurrentPageIndex = pageIndex;
            AddRange(items.Skip(StartItemIndex - 1).Take(pageSize));
        }

        /// <summary>
        /// 分页  类型 IEnumerable
        /// </summary>
        /// <param name="currentPageItems">IEnumerable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalItemCount">总数</param>
        public PagedList(IEnumerable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            Capacity = TotalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 分页 类型 IQueryable
        /// </summary>
        /// <param name="allItems">IQueryable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            AddRange(allItems.Skip(startIndex).Take(pageSize));
            TotalItemCount = allItems.Count();
            Capacity = TotalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 分页 类型 IQueryable
        /// </summary>
        /// <param name="currentPageItems">IQueryable</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalItemCount">总数</param>
        public PagedList(IQueryable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            Capacity = TotalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }

        /// <summary>
        /// 开始页
        /// </summary>
        public int StartItemIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }

        /// <summary>
        /// 结束页
        /// </summary>
        public int EndItemIndex { get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; } }
    }
}
