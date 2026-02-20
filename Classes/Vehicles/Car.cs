using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal_Engines.Classes.Vehicles.VehicleComponents;

namespace Terminal_Engines.Classes.Vehicles
{
    public class Car
    {
        public required string VehicleName { get; set; }
        public string? Manufacturer { get; set; }

        public List<VehiclePart> Parts { get; set; } = new();

        public bool IsJobComplete => Parts.All(p => p.IsFine);

        public void InitializeCar(Engine engine, List<Wheel> wheels)
        {
            Parts.Add(engine);
            foreach (var wheel in wheels)
            {
                Parts.Add(wheel);
            }
        }
    }
}
