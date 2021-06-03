using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class SparePartController : Controller
    {
        private readonly IRepository<SparePart> _sparePartRepository;

        public SparePartController(IRepository<SparePart> sparePartRepository)
        {
            _sparePartRepository = sparePartRepository;
        }
        public IActionResult Index()
        {
            var spareParts = _sparePartRepository.GetAll();
            spareParts = spareParts.OrderBy(spareParts => spareParts.Id);
            return View(spareParts);
        }

        public IActionResult Delete(int id)
        {
            _sparePartRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var sparePart = _sparePartRepository.GetById(id);
            return View(sparePart);
        }

        [HttpPost]
        public IActionResult Update(SparePart sparePart)
        {
            _sparePartRepository.Update(sparePart);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SparePart sparePart)
        {
            _sparePartRepository.Create(sparePart);
            return RedirectToAction("Index");
        }
    }
}
