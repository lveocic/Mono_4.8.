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
        public MonoContext() : base("MonoContext")
        {

        }
        #region Properties
       
        public DbSet<VehicleMakeEntity> VehicleMakers { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }



        #endregion Properties

    }
}