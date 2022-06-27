using System;
using System.Collections.Generic;

namespace Mono.Service.Repository.Filters
{
    public interface IFilter
    {
        #region Properties

        IEnumerable<Guid> Ids { get; set; }
        string OrderBy { get; set; }
        string OrderDirection { get; set; }
        int? Page { get; set; }
        int? PageSize { get; set; }
        string SearchQuery { get; set; }

        #endregion Properties
    }
}