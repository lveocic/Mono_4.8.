using AutoMapper;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        #region Fields

        private MonoContext Context;

        #endregion Fields

        #region Constructors

        public VehicleModelRepository(MonoContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        public IMapper Mapper { get; set; }

        #endregion Properties

        #region Methods

        public async Task DeleteAsync(Guid id)
        {
            var vehicleModel = Context.VehicleModels.Find(id);
            Context.VehicleModels.Remove(vehicleModel);
            await Context.SaveChangesAsync();
        }

        public async Task<VehicleModel> FindAsync(Guid id)
        {
            return Mapper.Map<VehicleModel>(await Context.VehicleModels.FindAsync(id));
        }

        public async Task<VehicleModel> InsertAsync(VehicleModelEntity entity)
        {
            var insert = Context.VehicleModels.Add(entity);
            return Mapper.Map<VehicleModel>(await Context.SaveChangesAsync());
        }

        public async Task UpdateAsync(VehicleModelEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        #endregion Methods
    }
}