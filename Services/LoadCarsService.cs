using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Terminal_Engines.Classes.Vehicles;
using Terminal_Engines.Classes.Vehicles.VehicleComponents;

namespace Terminal_Engines.Services
{
    public class GetCarsDto
    {
        public required string VehicleName { get; set; }
        public string? Manufacturer { get; set; }
        public required Engine Engine { get; set; }
        public required List<Wheel> Wheels { get; set; }
    }
    
    public class ShopManager
    {
        public List<Car> LoadShopQueue(string filePath)
        {
            var shopQueue = new List<Car>();

            if (!File.Exists(filePath))
            {
                return shopQueue;
            }

            string jsonString = File.ReadAllText(filePath);

            var carDataList = JsonSerializer.Deserialize<List<GetCarsDto>>(jsonString);

            if (carDataList == null)
            {
                return shopQueue;
            }

            foreach (var data in carDataList)
            {
                var car = new Car
                {
                    VehicleName = data.VehicleName,
                    Manufacturer = data.Manufacturer,
                };

                car.InitializeCar(data.Engine, data.Wheels);

                shopQueue.Add(car);
            }

            return shopQueue;
        }
    }
}
