using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 分页IPagedList
    /// </summary>
    public interface IPagedList : IEnumerable
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int CurrentPageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        int TotalItemCount { get; set; }
    }

    /// <summary>
    /// 分页IPagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IEnumerable<T>, IPagedList { }
}
