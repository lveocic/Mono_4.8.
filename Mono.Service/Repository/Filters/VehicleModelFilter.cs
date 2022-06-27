using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository.Filters
{
    public class VehicleModelFilter : Filter, IVehicleModelFilter
    {
        #region Properties

        public IEnumerable<Guid> VehicleMakeIds { get; set; }

        #endregion Properties
    }
}
