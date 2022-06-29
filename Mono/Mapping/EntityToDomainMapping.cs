using AutoMapper;
using Mono.Models;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Mapping
{
    public class EntityToDomainMapping : Profile
    {
        #region Constructors

        public static void AutoMapperMapping()
        {
            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<IVehicleModel, VehicleModelRestModel>().ReverseMap();
                config.CreateMap<IVehicleMake, VehicleMakeRestModel>().ReverseMap();
                config.CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
                config.CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
            });
        }

        #endregion Constructors
    }
}