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
        private readonly IOrderElementsRepository _orderElementRepository;
        private readonly IRepository<SparePart> _sparePartRepository;

        public OrderElementController(IOrderRepository orderRepository, 
                                        IOrderElementsRepository orderElementRepository, 
                                        IRepository<SparePart> sparePartRepository)
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
        public IActionResult Create(int orderId)
        {
            OrderElementModel orderElementModel = new OrderElementModel { Order = _orderRepository.GetById(orderId), OrderElement = null };
            ViewBagHelper.AddSparePartSelectListToViewBag(_sparePartRepository, ViewBag);

            return View(orderElementModel);
        }

        [HttpPost]
        public IActionResult Create(OrderElementModel orderElementModel, string action, int orderId)
        {
            orderElementModel.OrderElement.OrderId = orderId;
            _orderElementRepository.Create(orderElementModel.OrderElement);

            if (action == "CreateAndExit")
            {
                return RedirectToAction("Index", "Order");
            }

            ViewBagHelper.AddSparePartSelectListToViewBag(_sparePartRepository, ViewBag);
            orderElementModel = new OrderElementModel
            {
                Order = _orderRepository.GetById(orderId)
            };
            orderElementModel.Order.OrderElements = _orderElementRepository.GetAllByOrderId(orderId).ToList();

            return View(orderElementModel);
        }
    }
}
