using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal_Engines.Interfaces;

namespace Terminal_Engines.Classes.Vehicles.VehicleComponents
{
    public abstract class VehiclePart : IFixable
    {
        public required string Name { get; set; }
        public float Condition { get; set; }

        public virtual void Repair(float amount)
        {
            Condition = Math.Clamp(Condition + amount, 0, 100);
        }
        public abstract string GetStatusReport();

        public virtual bool IsFine => Condition >= 85f;
    }
}
