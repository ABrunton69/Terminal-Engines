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

        public override string GetStatusReport()
        {
            return $"Oil: {OilLevel}% | Battery {BatteryCharge}% | Core: {Condition}%";
        }
    }
}
