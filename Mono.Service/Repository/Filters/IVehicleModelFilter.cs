using System;
using System.Collections.Generic;

namespace Mono.Service.Repository.Filters
{
    public interface IVehicleModelFilter : IFilter
    {
        #region Properties

        IEnumerable<Guid> VehicleMakeIds { get; set; }

        #endregion Properties
    }
}
