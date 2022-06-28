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
    public interface IVehicleMakeRepository
    {
        #region Methods

        Task DeleteAsync(Guid id);
        Task<IEnumerable<VehicleMake>> FindVehicleMaker(IVehicleMakeFilter filter);

        Task<VehicleMake> FindAsync(Guid id);

        Task<IEnumerable<VehicleMake>> GetAllAsync();

        Task<VehicleMake> InsertAsync(VehicleMakeEntity entity);

        Task UpdateAsync(VehicleMakeEntity entity);

        #endregion Methods
    }
}