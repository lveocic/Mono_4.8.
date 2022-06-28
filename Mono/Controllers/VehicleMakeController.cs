using AutoMapper;
using Mono.MVC.Models;
using Mono.Service.Models;
using Mono.Service.Repository.Filters;
using Mono.Service.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mono.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        #region Constructors

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            VehicleMakeService = vehicleMakeService;
            Mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        public IMapper Mapper { get; set; }
        public IVehicleMakeService VehicleMakeService { get; set; }

        #endregion Properties

        #region Methods

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VehicleMakeRestModel maker)
        {
            if (maker != null)
            {
                VehicleMakeService.InsertVehicleMakerAsync(Mapper.Map<VehicleMake>(maker));
                return RedirectToAction("Index");
            }
            return View(maker);
        }

        
       
            
        [HttpDelete()]
        [Route("{id}")]

        public async Task<ActionResult> DeleteVehicleMaker(Guid id)
        {
            var vehicleMaker = await VehicleMakeService.FindVehicleMakerAsync(id);
            if (vehicleMaker == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            await VehicleMakeService.DeleteVehicleMakerAsync(vehicleMaker.Id);
            return new HttpStatusCodeResult(HttpStatusCode.NoContent);
        }
        [HttpGet()]
        [Route("{id}")]
        public async Task<VehicleMake> FindVehicleMaker(Guid id)
        {
            return await VehicleMakeService.FindVehicleMakerAsync(id);
        }

        public ActionResult Index(string ids = "", string searchPhrase = "", int? page = 1, int? pageSize = 10)
        {
            var filter = new VehicleMakeFilter();
            filter.SearchQuery = searchPhrase;
            filter.Page = page;
            filter.PageSize = pageSize;
            filter.Ids = !String.IsNullOrWhiteSpace(ids) ? ids.Split(new string[] {","}, StringSplitOptions.None).Select(x => new Guid(x)) : new List<Guid>();
            var result = VehicleMakeService.SearchVehicleMakers(filter);
            if (result != null)
            {
                var restModel = Mapper.Map<List<VehicleMakeRestModel>>(result);
                return View(restModel);
            }
            var nullResult = new List<VehicleMakeRestModel>();
            return View(nullResult);
        }

        /*
        public IActionResult Index()
        {
            var result = VehicleMakeService.GetAllVehicleMakers();
            if (result != null)
            {
                var restModel = Mapper.Map<IEnumerable<VehicleMakeRestModel>>(result);
                return View(restModel);
            }
            var nullResult = new List<VehicleMakeRestModel>();
            return View(nullResult);
        }
        */

        [HttpPost]
        public async Task<VehicleMake> PostVehicleMaker(/*[FromBody]*/ VehicleMake vehicleMake)
        {
            var newVehicleMaker = await VehicleMakeService.InsertVehicleMakerAsync(vehicleMake);
            return newVehicleMaker;
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [HttpPut]
        public async Task<ActionResult> PutVehicleMaker(Guid id, /*[FromBody]*/ VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await VehicleMakeService.UpdateVehicleMakerAsync(vehicleMake);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            var vehicleMaker = VehicleMakeService.FindVehicleMakerAsync(id);

            if (vehicleMaker == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<VehicleMakeRestModel>(vehicleMaker));
        }



        #endregion Methods
    }
}