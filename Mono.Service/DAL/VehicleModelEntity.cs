using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.DAL
{
    public class VehicleModelEntity
    {
        #region Properties

        public string Abrv { get; set; }
        public Guid Id { get; set; }
        public Guid VehicleMakeId { get; set; }
        public string Name { get; set; }
        public VehicleMakeEntity VehicleMake { get; set; }

        #endregion Properties
    }
}