using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mono.Service.DAL;
using AutoMapper;
using Mono.Service.Service.Common;
using Mono.Service.Models;
using Mono.Service.Repository.Filters;
using Mono.Models;

namespace Mono.Controllers
{
    public class VehicleModelController : Controller
    {

        public IVehicleModelService VehicleModelService { get; set; }
        public IVehicleMakeService VehicleMakeService { get; set; }

        public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            VehicleModelService = vehicleModelService;
            VehicleMakeService = vehicleMakeService;
        }


        // GET: VehicleModelRestModels
        public async Task<ActionResult> Index(string sortOrder,string ids = "", string searchPhrase = "", int? page = 1, int? pageSize = 10)
        {
            var filter = new VehicleModelFilter();
            filter.Page = page;
            filter.PageSize = pageSize;
            filter.Ids = !String.IsNullOrWhiteSpace(ids) ? ids.Split(new string[] { "," }, StringSplitOptions.None).Select(x => new Guid(x)) : new List<Guid>();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            if (!String.IsNullOrEmpty(searchPhrase))
            {
                filter.SearchQuery = searchPhrase;
            }
            if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "name_desc";
                filter.OrderBy = "Name";
                filter.OrderDirection = "desc";
            } else
            {
                ViewBag.NameSortParm = "";
                filter.OrderBy = "Name";
                filter.OrderDirection = "asc";
            }
            
            var result = await VehicleModelService.SearchVehicleModels(filter);
            if (result != null)
            {
                var list = Mapper.Map<List<VehicleModelRestModel>>(result);
                var restModelList = new PagedList<VehicleModelRestModel>(list, result.PageIndex, result.PageSize, result.TotalCount);
                return View(restModelList);
            }
            var nullResult = new List<VehicleModelRestModel>();
            return View(nullResult);
        }

        // GET: VehicleModelRestModels/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await VehicleModelService.FindVehicleModelAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            var vehicleModelRestModel = Mapper.Map<VehicleModelRestModel>(result);
            return View(vehicleModelRestModel);
        }

        // GET: VehicleModelRestModels/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Makers = new SelectList(await VehicleMakeService.GetAllVehicleMakersAsync(),"Id","Name");
            return View();
        }

        // POST: VehicleModelRestModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Abrv,VehicleMakeId,Name")] VehicleModelRestModel vehicleModelRestModel)
        {
            if (ModelState.IsValid)
            {
                vehicleModelRestModel.Id = Guid.NewGuid();
                var vehicleModel = Mapper.Map<VehicleModel>(vehicleModelRestModel);
                await VehicleModelService.InsertVehicleModelAsync(vehicleModel);
                return RedirectToAction("Index");
            }

            return View(vehicleModelRestModel);
        }

        // GET: VehicleModelRestModels/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModelRestModel = Mapper.Map<VehicleModelRestModel>(await VehicleModelService.FindVehicleModelAsync(id));
            if (vehicleModelRestModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Makers = new SelectList(await VehicleMakeService.SearchVehicleMakers(new VehicleMakeFilter()), "Id", "Name", vehicleModelRestModel.VehicleMakeId);
            return View(vehicleModelRestModel);
        }

        // POST: VehicleModelRestModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Abrv,VehicleMakeId,Name")] VehicleModelRestModel vehicleModelRestModel)
        {
            if (ModelState.IsValid)
            {
                var vehicleModel = Mapper.Map<VehicleModel>(vehicleModelRestModel);
                await VehicleModelService.UpdateVehicleModelAsync(vehicleModel);
                return RedirectToAction("Index");
            }
            return View(vehicleModelRestModel);
        }

        // GET: VehicleModelRestModels/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModel = Mapper.Map<VehicleModelRestModel>(await VehicleModelService.FindVehicleModelAsync(id));
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }
            

        // POST: VehicleModelRestModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {

            await VehicleModelService.DeleteVehicleModelAsync(id);
            return RedirectToAction("Index");
        }

    }
}
