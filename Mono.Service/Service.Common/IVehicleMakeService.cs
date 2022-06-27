using Mono.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Service.Common
{
    public interface IVehicleMakeService
    {
        #region Methods

        Task DeleteVehicleMakerAsync(Guid id);

        Task<VehicleMake> FindVehicleMakerAsync(Guid id);

        Task<IEnumerable<VehicleMake>> GetAllVehicleMakers();

        Task<VehicleMake> InsertVehicleMakerAsync(VehicleMake vehicleMake);

        Task UpdateVehicleMakerAsync(VehicleMake vehicleMake);

        #endregion Methods
    }
}