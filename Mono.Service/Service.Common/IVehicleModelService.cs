using Mono.Service.Models;
using Mono.Service.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Service.Common
{
    public interface IVehicleModelService
    {
        #region Methods
        Task<PagedList<VehicleModel>> SearchVehicleModels(IVehicleModelFilter filter);
        Task DeleteVehicleModel(Guid id);

        Task<VehicleModel> FindVehicleModelAsync(Guid id);

        Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleMake);

        Task UpdateVehicleModelAsync(VehicleModel vehicleMake);

        #endregion Methods
    }
}