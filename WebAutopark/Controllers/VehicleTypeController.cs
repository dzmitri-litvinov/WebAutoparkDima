using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepository;

        public VehicleTypeController(IRepository<VehicleType> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        public IActionResult Index()
        {
            var vehicleTypes = _vehicleTypeRepository.GetAll();
            vehicleTypes = vehicleTypes.OrderBy(vehicleTypes => vehicleTypes.Id);
            return View(vehicleTypes);
        }

        public IActionResult Delete(int id)
        {
            _vehicleTypeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vehicleType = _vehicleTypeRepository.GetById(id);
            return View(vehicleType);
        }

        [HttpPost]
        public IActionResult Update(VehicleType vehicleType)
        {
            _vehicleTypeRepository.Update(vehicleType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleType vehicleType)
        {
            _vehicleTypeRepository.Create(vehicleType);
            return RedirectToAction("Index");
        }
    }
}
