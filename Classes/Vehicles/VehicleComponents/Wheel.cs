using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal_Engines.Classes.Vehicles.VehicleComponents
{
    public class Wheel : VehiclePart
    {
        public float TirePressure { get; set; } 
        public bool IsBent { get; set; }

        public void Replace()
        {
            IsBent = false;
            Condition = new Random().Next(85, 101);
        }

        public void InFlate(float targetPsi)
        {
            TirePressure = targetPsi;
        }

        public override string GetStatusReport()
        {
            string bentStatus = IsBent ? "[red]BENT[/]" : "[green]Straight[/]";
            return $"PSI: {TirePressure} | RIM: {bentStatus} | Tread: {Condition}%";
        }
        public override bool IsFine =>
            base.IsFine && 
            !IsBent &&
            TirePressure >= 30f && TirePressure <= 35f;
    }
}
