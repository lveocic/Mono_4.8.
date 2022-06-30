using AutoMapper;
using Mono.Models;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mono.Mapping
{
    public class AutoMapperConfig
    {

        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<IVehicleModel, VehicleModelRestModel>().ReverseMap();
                config.CreateMap<VehicleModel, VehicleModelRestModel>().ReverseMap();
                config.CreateMap<IVehicleMake, VehicleMakeRestModel>().ReverseMap();
                config.CreateMap<VehicleMake, VehicleMakeRestModel>().ReverseMap();
                config.CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
                config.CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
                config.CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
                config.CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
            });

        }
    }
}