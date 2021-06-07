using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.Extentions;

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
        public IActionResult Index(string order)
        {
            var vehicles = _vehicleRepository.GetAll();
            vehicles = OrderVehicle(vehicles, order);

            return View(vehicles);
        }
        
        public IActionResult GetById(int id)
        {
            Vehicle vehicle = _vehicleRepository.GetById(id);
            VehicleType vehicleType = _vehicleTypeRepository.GetById(vehicle.VehicleTypeId);
            vehicle.VehicleType = vehicleType;

            ViewBag.TaxPerMonth = vehicle.GetTaxPerMonth().ToString("0.00");
            ViewBag.MaxKm = vehicle.GetMaxKm().ToString("0.00");

            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.vehicleTypes = GetVehicleTypesSelectList();
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
            ViewBag.vehicleTypes = GetVehicleTypesSelectList();
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

        private SelectList GetVehicleTypesSelectList()
        {
            var vehicleTypes = _vehicleTypeRepository.GetAll().ToList();
            return new SelectList(vehicleTypes, "Id", "TypeName");
        }

        private IEnumerable<Vehicle> OrderVehicle(IEnumerable<Vehicle> vehicles, string order)
        {
            switch (order)
            {
                case "id":
                    vehicles = vehicles.OrderBy(v => v.Id);
                    break;
                case "idDesc":
                    vehicles = vehicles.OrderByDescending(v => v.Id);
                    break;
                case "modelName":
                    vehicles = vehicles.OrderBy(v => v.ModelName);
                    break;
                case "modelNameDesc":
                    vehicles = vehicles.OrderByDescending(v => v.ModelName);
                    break;
                case "vehicleType":
                    vehicles = vehicles.OrderBy(v => v.VehicleType.TypeName);
                    break;
                case "vehicleTypeDesc":
                    vehicles = vehicles.OrderByDescending(v => v.VehicleType.TypeName);
                    break;
                case "mileageKm":
                    vehicles = vehicles.OrderBy(v => v.MileageKm);
                    break;
                case "mileageKmDesc":
                    vehicles = vehicles.OrderByDescending(v => v.MileageKm);
                    break;
                default:
                    break;
            }

            return vehicles;
        }
    }
}
