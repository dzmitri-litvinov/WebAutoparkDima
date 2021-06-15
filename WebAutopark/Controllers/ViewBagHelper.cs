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
    public static class ViewBagHelper
    {
        public static void AddSparePartSelectListToViewBag(IRepository<SparePart> _sparePartRepository, dynamic viewBag)
        {
            var spareParts = _sparePartRepository.GetAll();
            viewBag.SpareParts = new SelectList(spareParts, "Id", "PartName");
        }

        public static void AddVehicleSelectListToViewBag(IVehicleRepository _vehicleRepository, dynamic viewBag)
        {
            var vehicleTypes = _vehicleRepository.GetAll();
            viewBag.Vehicles = new SelectList(vehicleTypes, "Id", "ModelName");
        }
    }
}
