using AutoMapper;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Mapping
{
    public class EntityToDomainMapping : Profile
    {
        #region Constructors

        public EntityToDomainMapping()
        {
            CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
        }

        #endregion Constructors
    }
}