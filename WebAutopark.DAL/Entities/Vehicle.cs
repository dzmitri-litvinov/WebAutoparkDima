using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public class Vehicle
    {
        private const double TaxWeightCoeff = 0.0013;
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; }
        public string ModelName { get; }
        public string RegistrationNumber { get; }
        public double WeightKg { get; }
        public int ManufactureYear { get; }
        public double MileageKm { get; set; }
        public Color Color { get; set; }
        public string Engine { get; set; }
        public double EngineCapacity { get; set; }
        public double Consumption { get; set; }
        public double FuelTankOrBattery { get; set; }
    }
}
