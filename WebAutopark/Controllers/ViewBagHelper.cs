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
        public static void AddSparePartSelectListToViewBag(IRepository<SparePart> _sparePartRepository, dynamic VB)
        {
            var spareParts = _sparePartRepository.GetAll();
            VB = new SelectList(spareParts, "Id", "PartName");
        }
    }
}
