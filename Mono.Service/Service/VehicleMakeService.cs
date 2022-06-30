using AutoMapper;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Repository.Common;
using Mono.Service.Repository.Filters;
using Mono.Service.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        #region Constructors

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            VehicleMakeRepository = vehicleMakeRepository;
        }

        #endregion Constructors

        #region Properties

        public IVehicleMakeRepository VehicleMakeRepository { get; set; }

        #endregion Properties

        #region Methods

        public async Task DeleteVehicleMakeAsync(Guid id)
        {
            await VehicleMakeRepository.DeleteAsync(id);
        }

        public async Task<VehicleMake> FindVehicleMakeAsync(Guid id)
        {
            return await VehicleMakeRepository.FindAsync(id);
        }

        public async Task<VehicleMake> InsertVehicleMakeAsync(VehicleMake vehicleMake)
        {
            CreateVehicleMake(vehicleMake);
            var entity = Mapper.Map<VehicleMakeEntity>(vehicleMake);
            var result = await VehicleMakeRepository.InsertAsync(entity);
            return result;
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            var entity = Mapper.Map<VehicleMakeEntity>(vehicleMake);
            await VehicleMakeRepository.UpdateAsync(entity);
        }

        private void CreateVehicleMake(VehicleMake vehicleMake)
        {
            vehicleMake.Id = Guid.NewGuid();
            vehicleMake.Abrv = vehicleMake.Name.ToLower().Replace(" ", "-").Replace("č", "c").Replace("ć", "c").Replace("ž", "z").Replace("š", "s").Replace("đ", "d");
        }

        public async Task<IEnumerable<VehicleMake>> SearchVehicleMakers(IVehicleMakeFilter filter)
        {
            return await VehicleMakeRepository.FindVehicleMaker(filter);
        }

        #endregion Methods
    }
}