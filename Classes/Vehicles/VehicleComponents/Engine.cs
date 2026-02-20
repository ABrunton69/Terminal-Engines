using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal_Engines.Interfaces;

namespace Terminal_Engines.Classes.Vehicles.VehicleComponents
{
    public class Engine : VehiclePart
    {
        public float OilLevel { get; set; }
        public float BatteryCharge { get; set; }

        public void FillOil()
        {
            OilLevel = 100f;
        }

        public void RechargeBattery()
        {
            BatteryCharge = 100f;
        }

        public override string GetStatusReport()
        {
            return $"Oil: {OilLevel}% | Battery {BatteryCharge}% | Core: {Condition}%";
        }

        public override bool IsFine => 
            base.IsFine && 
            OilLevel >= 85f &&
            BatteryCharge >= 85f;
    }
}
