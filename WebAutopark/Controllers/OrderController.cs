using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    { 

        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersElementsRepository _orderElementRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRepository<SparePart> _sparePartRepository;

        public OrderController(IOrderRepository orderRepository, IOrdersElementsRepository orderElementRepository, IVehicleRepository vehicleRepository, IRepository<SparePart> sparePartRepository)
        {
            _orderRepository = orderRepository;
            _orderElementRepository = orderElementRepository;
            _vehicleRepository = vehicleRepository;
            _sparePartRepository = sparePartRepository;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);
                        
            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddVehicleSelectListToViewBag();
            return View();
        }
        public IActionResult Delete(int id)
        {
            _orderRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            int orderId = _orderRepository.CreateAndReturnId(order);
            OrderElementAddModel orderElementAdd = new OrderElementAddModel { Order = _orderRepository.GetById(orderId) };
            AddSparePartSelectListToViewBag();

            return View("../OrderElement/Create", orderElementAdd);
        }

        private void AddVehicleSelectListToViewBag()
        {
            var vehicleTypes = _vehicleRepository.GetAll();
            ViewBag.Vehicles = new SelectList(vehicleTypes, "Id", "ModelName");
        }

        private void AddSparePartSelectListToViewBag()
        {
            var spareParts = _sparePartRepository.GetAll();
            ViewBag.SpareParts = new SelectList(spareParts, "Id", "PartName");
        }
    }
}