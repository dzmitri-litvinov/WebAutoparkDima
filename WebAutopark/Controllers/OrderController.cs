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
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRepository<SparePart> _sparePartRepository;

        public OrderController(IOrderRepository orderRepository, 
                                IVehicleRepository vehicleRepository, 
                                IRepository<SparePart> sparePartRepository)
        {
            _orderRepository = orderRepository;
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
            ViewBagHelper.AddVehicleSelectListToViewBag(_vehicleRepository, ViewBag);
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

            return RedirectToAction("Create", "OrderElement", new { orderId =  orderId});
        }
    }
}
