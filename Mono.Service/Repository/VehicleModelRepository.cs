﻿using AutoMapper;
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
                    query = query.Where(x => x.Name.ToLower().Contains(filter.SearchQuery.ToLower()));
                }
                if (filter.Ids != null && filter.Ids.Any())
                {
                    query = query.Where(x => filter.Ids.Contains(x.Id));
                }
                if (filter.VehicleMakeIds != null && filter.VehicleMakeIds.Any())
                {
                    query = query.Where(x => filter.VehicleMakeIds.Contains(x.MakeId));
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
            }
            return Task.FromResult(query);
        }

        public async Task<IEnumerable<VehicleModel>> FindVehicleModel(IVehicleModelFilter filter)
        {
            IQueryable<VehicleModelEntity> query = Context.Set<VehicleModelEntity>();
            query = await ApplyFilteringAsync(query, filter);
            query = await ApplySortingAsync(query, filter);
            query = await ApplyPagingAsync(query, filter);
            return Mapper.Map<IEnumerable<VehicleModel>>(await query.ToListAsync());
        }

        #endregion Methods
    }
}