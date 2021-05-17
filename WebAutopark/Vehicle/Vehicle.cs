using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark
{
    public class Vehicle : IComparable<Vehicle>
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
        
        public Vehicle()
        {

        }
        
        public double GetCalcTaxPerMonth()
        {
            return WeightKg * TaxWeightCoeff + VehicleType.TaxCoefficient * 30 + 5;
        }

        public double GetMaxKm()
        {
            return FuelTankOrBattery / Consumption;
        }
        public int CompareTo(Vehicle other)
        {
            return GetCalcTaxPerMonth().CompareTo(other.GetCalcTaxPerMonth());
        }
    }
}
