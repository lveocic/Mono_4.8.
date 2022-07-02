using Mono.Service.Models;
using Mono.Service.Repository.Filters;
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
        Task<IEnumerable<VehicleMake>> GetAllVehicleMakersAsync();
        Task<PagedList<VehicleMake>> SearchVehicleMakers(IVehicleMakeFilter filter);
        Task DeleteVehicleMakeAsync(Guid id);

        Task<VehicleMake> FindVehicleMakeAsync(Guid id);

        Task<VehicleMake> InsertVehicleMakeAsync(VehicleMake vehicleMake);

        Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);

        #endregion Methods
    }
}