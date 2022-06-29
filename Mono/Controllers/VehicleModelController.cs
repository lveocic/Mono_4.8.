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
        public IMapper Mapper { get; set; }
        public IVehicleModelService VehicleModelService { get; set; }
        public VehicleModelController(IMapper mapper, IVehicleModelService vehicleModelService)
        {
            Mapper = mapper;
            VehicleModelService = vehicleModelService;
        }


        // GET: VehicleModelRestModels
        public async Task<ActionResult> Index(string ids = "", string searchPhrase = "", int? page = 1, int? pageSize = 10)
        {
            var filter = new VehicleModelFilter();
            filter.SearchQuery = searchPhrase;
            filter.Page = page;
            filter.PageSize = pageSize;
            filter.Ids = !String.IsNullOrWhiteSpace(ids) ? ids.Split(new string[] { "," }, StringSplitOptions.None).Select(x => new Guid(x)) : new List<Guid>();
            var result = VehicleModelService.SearchVehicleModels(filter);
            if (result != null)
            {
                var restModel = Mapper.Map<List<VehicleModelRestModel>>(result);
                return View(restModel);
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
            var vehicleModel = Mapper.Map<VehicleModel>(await VehicleModelService.FindVehicleModelAsync(id));
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModelRestModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleModelRestModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Abrv,MakeId,Name")] VehicleModelRestModel vehicleModelRestModel)
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
            var vehicleModel = Mapper.Map<VehicleModel>(await VehicleModelService.FindVehicleModelAsync(id));
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModelRestModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Abrv,MakeId,Name")] VehicleModelRestModel vehicleModelRestModel)
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
            var vehicleModel = Mapper.Map<VehicleModel>(await VehicleModelService.FindVehicleModelAsync(id));
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
