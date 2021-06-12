using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    { 

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderElement> _orderElementRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;

        public OrderController(IRepository<Order> orderRepository, IRepository<OrderElement> orderElementRepository, IRepository<Vehicle> vehicleRepository)
        {
            _orderRepository = orderRepository;
            _orderElementRepository = orderElementRepository;
            _vehicleRepository = vehicleRepository;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);
            var vehicle = _vehicleRepository.GetById(order.VehicleId);
            order.Vehicle = vehicle;
            var ordersElements = _orderElementRepository.GetAll();

            order.OrderElements = ordersElements.Where(o => o.OrderId == id).ToList();
                        
            return View(order);
        }
    }
}
