using System;
using System.Web;
using AutoMapper;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Mono.Mapping;
using Mono.Service;
using Mono.Service.Models;
using Mono.Service.Repository;
using Mono.Service.Repository.Common;
using Mono.Service.Service;
using Mono.Service.Service.Common;
using Mono.Service.Service;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Mono.Service.Models.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mono.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Mono.App_Start.NinjectWebCommon), "Stop")]

namespace Mono.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new DIModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            
            kernel.Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleMake>().To<VehicleMake>();

            kernel.Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            kernel.Bind<IVehicleModelService>().To<VehicleModelService>();
            kernel.Bind<IVehicleModel>().To<VehicleModel>();
        }
    }
}