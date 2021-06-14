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
    public class OrderElementController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersElementsRepository _orderElementRepository;
        private readonly IRepository<SparePart> _sparePartRepository;

        public OrderElementController(IOrderRepository orderRepository, IOrdersElementsRepository orderElementRepository, IRepository<SparePart> sparePartRepository)
        {
            _orderRepository = orderRepository;
            _orderElementRepository = orderElementRepository;
            _sparePartRepository = sparePartRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(OrderElementAddModel orderElementAdd)
        {
            AddSparePartSelectListToViewBag();            

            return View(orderElementAdd);
        }

        [HttpPost]
        public IActionResult Create(OrderElementAddModel orderElementAdd, string action, int orderId)
        {
            orderElementAdd.OrderElementToAdd.OrderId = orderId;
            _orderElementRepository.Create(orderElementAdd.OrderElementToAdd);
            AddSparePartSelectListToViewBag();

            if (action == "CreateAndExit")
            {
                return RedirectToAction("Index", "Order");
            }

            orderElementAdd = new OrderElementAddModel
            {
                Order = _orderRepository.GetById(orderId)
            };
            orderElementAdd.Order.OrderElements = _orderElementRepository.GettAllByOrderId(orderId).ToList();

            return View(orderElementAdd);
        }

        private void AddSparePartSelectListToViewBag()
        {
            var spareParts = _sparePartRepository.GetAll();
            ViewBag.SpareParts = new SelectList(spareParts, "Id", "PartName");
        }
    }
}
