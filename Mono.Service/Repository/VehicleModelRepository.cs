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
    public class VehicleModelRepository : IVehicleModelRepository
    {
        #region Fields

        private MonoContext Context;

        #endregion Fields

        #region Constructors

        public VehicleModelRepository(MonoContext context)
        {
            Context = context;
        }

        #endregion Constructors

        #region Properties


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
            try
            {
                var result = await Context.VehicleModels.Include(x => x.VehicleMake).FirstOrDefaultAsync(q => q.Id == id);
                return Mapper.Map<VehicleModel>(result);
            }
            catch (Exception exception)
            {
                throw new Exception($"{this.ToString()} - find failed", exception);
            }
        }

        public async Task<VehicleModel> InsertAsync(VehicleModelEntity entity)
        {
            var insert = Context.VehicleModels.Add(entity);
            await Context.SaveChangesAsync();
            return Mapper.Map<VehicleModel>(insert);
        }

        public async Task UpdateAsync(VehicleModelEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        public Task<IQueryable<VehicleModelEntity>> ApplyFilteringAsync(IQueryable<VehicleModelEntity> query, IVehicleModelFilter filter)
        {
            if (filter.SearchQuery != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
                {
                    query = query.Where(x => x.VehicleMake.Name.ToLower().Contains(filter.SearchQuery.ToLower()));
                }
                if (filter.Ids != null && filter.Ids.Any())
                {
                    query = query.Where(x => filter.Ids.Contains(x.Id));
                }
                if (filter.VehicleMakeIds != null && filter.VehicleMakeIds.Any())
                {
                    query = query.Where(x => filter.VehicleMakeIds.Contains(x.VehicleMakeId));
                }
            }
            return Task.FromResult(query);
        }

        public Task<IQueryable<VehicleModelEntity>> ApplyPagingAsync(IQueryable<VehicleModelEntity> query, IVehicleModelFilter filter)
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

        public Task<IQueryable<VehicleModelEntity>> ApplySortingAsync(IQueryable<VehicleModelEntity> query, IVehicleModelFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.OrderBy) && !string.IsNullOrWhiteSpace(filter.OrderDirection))
            {
                if (filter.OrderBy == nameof(IVehicleModel.Name))
                {
                    query = filter.OrderDirection == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
                else
                {
                    query = query.OrderBy(x => x.Name);
                }
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }
            return Task.FromResult(query);
        }
        public async Task<PagedList<VehicleModel>> FindVehicleModel(IVehicleModelFilter filter)
        {
            try
            {
                IQueryable<VehicleModelEntity> query = Context.VehicleModels;
                query = await ApplyFilteringAsync(query, filter);
                query = await ApplySortingAsync(query, filter);
                int count = await query.CountAsync();
                query = await ApplyPagingAsync(query, filter);
                var result = await query.Include(x => x.VehicleMake).ToListAsync();
                var mapped = Mapper.Map<List<VehicleModel>>(result);
                PagedList<VehicleModel> pagedList = new PagedList<VehicleModel>(mapped, filter.Page.Value, filter.PageSize.Value, count);
                return pagedList;
            }
            catch (Exception exception)
            {
                throw new Exception($"{this.ToString()} - find failed", exception);
            }
        }

        #endregion Methods
    }
}