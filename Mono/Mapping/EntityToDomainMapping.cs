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
        
        protected override void Configure()
        {
            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<VehicleModel, VehicleModelRestModel>();
                config.CreateMap<VehicleMake, VehicleMakeRestModel>();
                config.CreateMap<VehicleModelEntity, VehicleModel>();
                config.CreateMap<VehicleMakeEntity, VehicleMake > ();
            });
        }

        #endregion Constructors
    }
}