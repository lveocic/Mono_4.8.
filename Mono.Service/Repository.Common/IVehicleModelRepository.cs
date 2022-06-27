using Mono.Service.DAL;
using Mono.Service.Models;
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

        Task DeleteAsync(Guid id);

        Task<VehicleModel> FindAsync(Guid id);

        Task<VehicleModel> InsertAsync(VehicleModelEntity entity);

        Task UpdateAsync(VehicleModelEntity entity);

        #endregion Methods
    }
}