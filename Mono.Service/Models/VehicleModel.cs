using Mono.Service.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Models
{
    public class VehicleModel : IVehicleModel
    {
        #region Properties

        public string Abrv { get; set; }
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string Name { get; set; }
        public IVehicleMake VehicleMake { get; set; }

        #endregion Properties
    }
}