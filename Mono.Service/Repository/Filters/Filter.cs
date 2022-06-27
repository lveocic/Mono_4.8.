using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository.Filters
{
    public class Filter : IFilter
    {
        #region Properties

        public IEnumerable<Guid> Ids { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string SearchQuery { get; set; }

        #endregion Properties
    }
}