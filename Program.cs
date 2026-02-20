using Spectre.Console;
using System.Threading.Tasks;
using Terminal_Engines.Classes.AccountLogin;
using Terminal_Engines.Classes.ShopClasses;
using Terminal_Engines.Classes.Vehicles;
using Terminal_Engines.Classes.Vehicles.VehicleComponents;

namespace Terminal_Engines
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GameUtilities gameUtilities = new GameUtilities();

            gameUtilities.BootGame();

            // Set player account
            Account CurrentAccount = new Account();
            CurrentAccount.credits = 1000;

            // Set player account username
            var username = AnsiConsole.Ask<string>("What's your [green]username[/]?");
            CurrentAccount.UserName = username;

            // Set player account first name
            var name = AnsiConsole.Ask<string>("What's your [green]first name[/]?");
            CurrentAccount.Name = name;

            Car FirstCar = GetTestVehicles().FirstOrDefault();

            AnsiConsole.Write(gameUtilities.JobCard(FirstCar));
        }

        public static List<Car> GetTestVehicles()
        {
            var shopQueue = new List<Car>();

            // --- Vehicle 1: The "Rusty Bucket" ---
            var car1 = new Car
            {
                VehicleName = "Civic",
                Manufacturer = "Honda",
            };

            var engine1 = new Engine
            {
                Name = "VTEC Engine",
                Condition = 45f,
                OilLevel = 10f,
                BatteryCharge = 80f
            };

            var wheels1 = new List<Wheel> {
                new Wheel { Name = "Front Left Wheel", Condition = 20f, TirePressure = 15f, IsBent = true },
                new Wheel { Name = "Front Right Wheel", Condition = 90f, TirePressure = 32f, IsBent = false },
                new Wheel { Name = "Rear Left Wheel", Condition = 85f, TirePressure = 30f, IsBent = false },
                new Wheel { Name = "Rear Right Wheel", Condition = 88f, TirePressure = 31f, IsBent = false }
            };

            car1.InitializeCar(engine1, wheels1);
            shopQueue.Add(car1);

            // --- Vehicle 2: The "Luxury Breakdown" ---
            var car2 = new Car
            {
                VehicleName = "911",
                Manufacturer = "Porsche",
            };

            var engine2 = new Engine
            {
                Name = "Flat-Six Engine",
                Condition = 95f,
                OilLevel = 90f,
                BatteryCharge = 5f // Flat battery!
            };

            // Quick way to add 4 identical wheels
            var wheels2 = new List<Wheel>();
            for (int i = 0; i < 4; i++)
            {
                wheels2.Add(new Wheel { Name = $"Alloy Wheel {i + 1}", Condition = 100f, TirePressure = 35f });
            }

            car2.InitializeCar(engine2, wheels2);
            shopQueue.Add(car2);

            return shopQueue;
        }
    }
}
