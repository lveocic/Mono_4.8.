using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.DAL
{
    public class MonoContext : DbContext
    {
        #region Properties
       
        public DbSet<VehicleMakeEntity> VehicleMakers { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }

        #endregion Properties

        //public System.Data.Entity.DbSet<Mono.Models.VehicleModelRestModel> VehicleModelRestModels { get; set; }
    }
}