using Mono.Service.Models;
using Mono.Service.Models.Common;
using Mono.Service.Repository;
using Mono.Service.Repository.Common;
using Mono.Service.Service;
using Mono.Service.Service.Common;
using Mono.Service.Service;
using Ninject.Modules;
using System;

namespace Mono.Service
{
    public class DIModule : NinjectModule
    {
        #region Methods

        public override void Load()
        {
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleMake>().To<VehicleMake>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            Bind<IVehicleModel>().To<VehicleModel>();
        }

        #endregion Methods
    }
}