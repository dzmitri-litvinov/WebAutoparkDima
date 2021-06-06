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
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleController(IRepository<Vehicle> vehicleRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAll();

            return View(vehicles);
        }
        
        public IActionResult GetById(int id)
        {
            Vehicle vehicle = _vehicleRepository.GetById(id);
            VehicleType vehicleType = _vehicleTypeRepository.GetById(vehicle.VehicleTypeId);
            vehicle.VehicleType = vehicleType;

            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vehicleTypes = _vehicleTypeRepository.GetAll().ToList();
            ViewBag.vehicleTypes = new SelectList(vehicleTypes, "Id", "TypeName");
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
            var vehicleTypes = _vehicleTypeRepository.GetAll().ToList();
            ViewBag.vehicleTypes = new SelectList(vehicleTypes, "Id", "TypeName");
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
    }
}
