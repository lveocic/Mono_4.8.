using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Models.Common
{
    public interface IVehicleModel
    {
        #region Properties

        string Abrv { get; set; }
        Guid Id { get; set; }
        Guid MakeId { get; set; }
        string Name { get; set; }
        IVehicleMake VehicleMake { get; set; }

        #endregion Properties
    }
}