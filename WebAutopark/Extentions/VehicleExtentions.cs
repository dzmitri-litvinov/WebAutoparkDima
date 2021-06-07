using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Extentions
{
    public static class VehicleExtentions
    {
        public static double GetTaxPerMonth(this Vehicle vehicle)
        {
            return vehicle.WeightKg * 0.0013 + vehicle.VehicleType.TaxCoefficient * 30 + 5;
        }

        public static double GetMaxKm(this Vehicle vehicle)
        {
            return vehicle.FuelTankOrBattery / vehicle.Consumption;
        }
    }
}
