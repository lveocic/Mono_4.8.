using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mono.Models;
using Mono.Service.DAL;
using AutoMapper;
using Mono.Service.Service.Common;
using Mono.Service.Repository.Filters;
using Mono.Service.Models;


namespace Mono.Controllers
{
    public class VehicleMakeController : Controller
    {
        public IVehicleMakeService VehicleMakeService { get; set; }
        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            VehicleMakeService = vehicleMakeService;
        }

        // GET: VehicleMakeRestModels
        public async Task<ActionResult> Index(string sortOrder, string searchPhrase = "", int? page = 1, int? pageSize = 10)
        {
            var filter = new VehicleMakeFilter();
            filter.Page = page;
            filter.PageSize = pageSize;
            
            if (!String.IsNullOrEmpty(searchPhrase))
            {
                filter.SearchQuery = searchPhrase;
            }
            if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "name_desc";
                filter.OrderBy = "Name";
                filter.OrderDirection = "desc";
            }
            else
            {
                ViewBag.NameSortParm = "";
                filter.OrderBy = "Name";
                filter.OrderDirection = "asc";
            }

            var result = await VehicleMakeService.SearchVehicleMakers(filter);
            if (result != null)
            {
                var list = Mapper.Map<List<VehicleMakeRestModel>>(result);
                var restMakeList = new PagedList<VehicleMakeRestModel>(list, result.PageIndex, result.PageSize, result.TotalCount);
                return View(restMakeList);
            }
            return View();
        }


        // GET: VehicleMakeRestModels/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMake = Mapper.Map<VehicleMakeRestModel>(await VehicleMakeService.FindVehicleMakeAsync(id));
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakeRestModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakeRestModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Abrv,VehicleMakeId,Name")] VehicleMakeRestModel vehicleMakeRestModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleMake = Mapper.Map<VehicleMake>(vehicleMakeRestModel);
                await VehicleMakeService.InsertVehicleMakeAsync(vehicleMake);
                return RedirectToAction("Details", new { id = vehicleMake.Id });
            }

            return View(vehicleMakeRestModel);
        }

        // GET: VehicleMakeRestModels/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMakeRestModel = Mapper.Map<VehicleMakeRestModel>(await VehicleMakeService.FindVehicleMakeAsync(id));
            if (vehicleMakeRestModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMakeRestModel);
        }

        // POST: VehicleMakeRestModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Abrv,Name")] VehicleMakeRestModel vehicleMakeRestModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleMake = Mapper.Map<VehicleMake>(vehicleMakeRestModel);
                await VehicleMakeService.UpdateVehicleMakeAsync(vehicleMake);
                return RedirectToAction("Index");
            }
            return View(vehicleMakeRestModel);
        }

        // GET: VehicleMakeRestModels/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleMakeRestModel = Mapper.Map<VehicleMakeRestModel>(await VehicleMakeService.FindVehicleMakeAsync(id));
            if (vehicleMakeRestModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMakeRestModel);
        }


        // POST: VehicleMakeRestModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            if (VehicleMakeService.DeleteVehicleMake(id))
            {
                return RedirectToAction("Index");
            }else
            {
                return HttpNotFound();
            }
        }
    }
}
