using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository.Common
{
    public interface IVehicleModelRepository
    {
        #region Methods
       
        Task<PagedList<VehicleModel>> FindVehicleModel(IVehicleModelFilter filter);
        Task Delete(Guid id);

        Task<VehicleModel> FindAsync(Guid id);

        Task<VehicleModel> InsertAsync(VehicleModelEntity entity);

        Task UpdateAsync(VehicleModelEntity entity);

        #endregion Methods
    }
}