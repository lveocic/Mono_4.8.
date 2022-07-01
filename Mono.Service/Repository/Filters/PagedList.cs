using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository.Filters
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }


        public PagedList(IList<T> list, int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            this.AddRange(list);

        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 <= TotalPages);
            }
        }
    }
}
