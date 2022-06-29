using AutoMapper;
using Mono.Models;
using Mono.Service.DAL;
using Mono.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mono.Mapping
{
    public class DomainToEntityMapping : Profile
    {
        protected override void Configure()
        {
            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<VehicleModelRestModel, VehicleModel>();
                config.CreateMap<VehicleMakeRestModel, VehicleMake > ();
                config.CreateMap<VehicleModel, VehicleModelEntity> ();
                config.CreateMap<VehicleMake, VehicleMakeEntity>();
            });
        }
    }
}