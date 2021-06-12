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
        private const double TypeTaxCoeff = 30;
        private const double AdditionalTaxCoeff = 5;
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public double WeightKg { get; set; }
        public int ManufactureYear { get; set; }
        public double MileageKm { get; set; }
        public Color Color { get; set; }
        public string Engine { get; set; }
        public double EngineCapacity { get; set; }
        public double Consumption { get; set; }
        public double FuelTankOrBattery { get; set; }
        public double TaxPerMonth => WeightKg * TaxWeightCoeff + VehicleType.TaxCoefficient * TypeTaxCoeff + AdditionalTaxCoeff;
        public double MaxKm => FuelTankOrBattery / Consumption;
    }
}
