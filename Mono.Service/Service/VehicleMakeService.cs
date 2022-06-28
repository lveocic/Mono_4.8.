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

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            VehicleMakeRepository = vehicleMakeRepository;
            Mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        public IMapper Mapper { get; set; }
        public IVehicleMakeRepository VehicleMakeRepository { get; set; }

        #endregion Properties

        #region Methods

        public async Task DeleteVehicleMakerAsync(Guid id)
        {
            await VehicleMakeRepository.DeleteAsync(id);
        }

        public async Task<VehicleMake> FindVehicleMakerAsync(Guid id)
        {
            var result = VehicleMakeRepository.FindAsync(id);
            return Mapper.Map<VehicleMake>(result);
        }

        public async Task<IEnumerable<VehicleMake>> GetAllVehicleMakers()
        {
            var result = await VehicleMakeRepository.GetAllAsync();
            return result;
        }

        public async Task<VehicleMake> InsertVehicleMakerAsync(VehicleMake vehicleMake)
        {
            CreateVehicleMaker(vehicleMake);
            var entity = Mapper.Map<VehicleMakeEntity>(vehicleMake);
            var result = await VehicleMakeRepository.InsertAsync(entity);
            return result;
        }

        public async Task UpdateVehicleMakerAsync(VehicleMake vehicleMake)
        {
            var entity = Mapper.Map<VehicleMakeEntity>(vehicleMake);
            await VehicleMakeRepository.UpdateAsync(entity);
        }

        private void CreateVehicleMaker(VehicleMake vehicleMake)
        {
            vehicleMake.Id = Guid.NewGuid();
            vehicleMake.Abrv = vehicleMake.Name.ToLower().Replace(" ", "-").Replace("č", "c").Replace("ć", "c").Replace("ž", "z").Replace("š", "s").Replace("đ", "d");
        }

        public async Task <IEnumerable<VehicleMake>> SearchVehicleMakers (IVehicleMakeFilter filter)
        {
            return await VehicleMakeRepository.FindVehicleMaker(filter);
        }

        #endregion Methods
    }
}