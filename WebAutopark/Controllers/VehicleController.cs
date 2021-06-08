using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleController(IVehicleRepository vehicleRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAll();

            return View(vehicles);
        }

        public IActionResult IndexOrder(string orderingCol, string orderingDir)
        {
            var vehicles = _vehicleRepository.GetAllOrderBy(orderingCol, orderingDir);

            return View("Index", vehicles);
        }
        
        public IActionResult Details(int id)
        {
            Vehicle vehicle = _vehicleRepository.GetById(id);

            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddVehicleTypesSelectListToViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            _vehicleRepository.Create(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vehicle = _vehicleRepository.GetById(id);
            AddVehicleTypesSelectListToViewBag();
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult Update(Vehicle vehicle)
        {
            _vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _vehicleRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private void AddVehicleTypesSelectListToViewBag()
        {
            var vehicleTypes = _vehicleTypeRepository.GetAll();
            ViewBag.VehicleTypes = new SelectList(vehicleTypes, "Id", "TypeName");
        }
    }
}
