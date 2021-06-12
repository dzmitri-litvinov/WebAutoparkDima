﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public ICollection<OrderElement> OrderElements { get; set; }
    }
}
