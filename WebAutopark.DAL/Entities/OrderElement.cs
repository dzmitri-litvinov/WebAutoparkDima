using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public class OrderElement
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SparePartId { get; set; }
        public SparePart SparePart { get; set; }
        public int SparePartQuantity { get; set; }
    }
}
