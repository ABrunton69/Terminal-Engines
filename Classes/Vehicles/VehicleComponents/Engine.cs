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

        public override IEnumerable<string> GetActions() =>
            new List<string> { "Repair Core Engine", "Fill Oil", "Recharge Battery", "Back" };

        public override void ExecuteAction(string action)
        {
            switch (action)
            {
                case "Repair Core Engine":
                    AnsiConsole.Status().Start("Repairing Engine Block... ", ctx => { Thread.Sleep(1000); });
                    Repair(100f);  
                    break;
                case "Fill Oil":
                    AnsiConsole.Status().Start("Filling Oil...", ctx => { Thread.Sleep(1000); });
                    OilLevel = 100f;
                    break;
                case "Recharge Battery":
                    AnsiConsole.Status().Start("Charging Battery...", ctx => { Thread.Sleep(1000); });
                    BatteryCharge = 100f;
                    break;
            }
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
