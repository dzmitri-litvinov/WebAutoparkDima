using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Models
{
    public class OrderElementModel
    {
        public Order Order { get; set; }
        public OrderElement OrderElement { get; set; }
    }
}
