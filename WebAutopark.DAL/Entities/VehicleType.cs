using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public float TaxCoefficient { get; set; }       

        public VehicleType()
        {

        }
    }
}
