using AutoMapper;
using Mono.Service.DAL;
using Mono.Service.Models;
using Mono.Service.Models.Common;
using Mono.Service.Repository.Common;
using Mono.Service.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        #region Fields

        private MonoContext Context;

        #endregion Fields

        #region Constructors

        public VehicleMakeRepository(MonoContext context)
        {
            Context = context;
        }

        #endregion Constructors

        #region Properties


        #endregion Properties

        #region Methods

        public async Task DeleteAsync(Guid id)
        {
            var vehicleModel = Context.VehicleMakers.Find(id);
            Context.VehicleMakers.Remove(vehicleModel);
            await Context.SaveChangesAsync();
        }

        public async Task<VehicleMake> FindAsync(Guid id)
        {
            return Mapper.Map<VehicleMake>(await Context.VehicleMakers.FindAsync(id));
        }


        public async Task<VehicleMake> InsertAsync(VehicleMakeEntity entity)
        {
            var insert = Context.VehicleMakers.Add(entity);
            await Context.SaveChangesAsync();
            return Mapper.Map<VehicleMake>(insert);
        }

        public async Task UpdateAsync(VehicleMakeEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        public Task<IQueryable<VehicleMakeEntity>> ApplyFilteringAsync(IQueryable<VehicleMakeEntity> query, IVehicleMakeFilter filter)
        {
            if (filter.SearchQuery != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(filter.SearchQuery.ToLower()));
                }
                if (filter.Ids != null && filter.Ids.Any())
                {
                    query = query.Where(x => filter.Ids.Contains(x.Id));
                }
            }
            return Task.FromResult(query);
        }

        public Task<IQueryable<VehicleMakeEntity>> ApplyPagingAsync(IQueryable<VehicleMakeEntity> query, IVehicleMakeFilter filter)
        {
            if (filter != null)
            {
                if (filter.Page.HasValue && filter.PageSize.HasValue)
                {
                    query = query.Skip((filter.Page.Value - 1) * filter.PageSize.Value).Take(filter.PageSize.Value);
                }
            }
            return Task.FromResult(query);
        }

        public Task<IQueryable<VehicleMakeEntity>> ApplySortingAsync(IQueryable<VehicleMakeEntity> query, IVehicleMakeFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.OrderBy) && !string.IsNullOrWhiteSpace(filter.OrderDirection))
            {
                if (filter.OrderBy == nameof(IVehicleMake.Name))
                {
                    query = filter.OrderDirection == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
            }
            return Task.FromResult(query);
        }

        public async Task<IEnumerable<VehicleMake>> FindVehicleMaker(IVehicleMakeFilter filter)
        {
            IQueryable<VehicleMakeEntity> query = Context.Set<VehicleMakeEntity>();
            query = await ApplyFilteringAsync(query, filter);
            query = await ApplySortingAsync(query, filter);
            query = await ApplyPagingAsync(query, filter);
            var result = await query.ToListAsync();
            return Mapper.Map<IEnumerable<VehicleMake>>(result);
        }

        #endregion Methods
    }
}