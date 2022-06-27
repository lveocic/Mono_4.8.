using AutoMapper;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Repository.Common;
using Mono.Service.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monok.Service.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        #region Constructors

        public VehicleModelService(IVehicleModelRepository vehicleModelRepository, IMapper mapper)
        {
            VehicleModelRepository = vehicleModelRepository;
            Mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        public IMapper Mapper { get; set; }
        public IVehicleModelRepository VehicleModelRepository { get; set; }

        #endregion Properties

        #region Methods

        public async Task DeleteVehicleModelAsync(Guid id)
        {
            await VehicleModelRepository.DeleteAsync(id);
        }

        public async Task<VehicleModel> FindVehicleModelAsync(Guid id)
        {
            var result = VehicleModelRepository.FindAsync(id);
            return Mapper.Map<VehicleModel>(result);
        }

        public async Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleModel)
        {
            CreateVehicleModel(vehicleModel);
            var entity = Mapper.Map<VehicleModelEntity>(vehicleModel);
            var result = await VehicleModelRepository.InsertAsync(entity);
            return result;
        }

        public async Task UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            var entity = Mapper.Map<VehicleModelEntity>(vehicleModel);
            await VehicleModelRepository.UpdateAsync(entity);
        }

        private void CreateVehicleModel(VehicleModel vehicleModel)
        {
            vehicleModel.Id = Guid.NewGuid();
            vehicleModel.Abrv = vehicleModel.Name.ToLower().Replace(" ", "-").Replace("č", "c").Replace("ć", "c").Replace("ž", "z").Replace("š", "s").Replace("đ", "d");
        }

        #endregion Methods
    }
}