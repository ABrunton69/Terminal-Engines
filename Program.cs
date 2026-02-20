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
        private static GameUtilities gameUtilities = new GameUtilities();

        public static void Main(string[] args)
        {

            gameUtilities.BootGame();

            Account CurrentAccount = new Account();
            CurrentAccount.credits = 1000;

            var username = AnsiConsole.Ask<string>("What's your [green]username[/]?");
            CurrentAccount.UserName = username;

            var name = AnsiConsole.Ask<string>("What's your [green]first name[/]?");
            CurrentAccount.Name = name;

            List<Car> ShopQueue = GetTestVehicles();

            while (ShopQueue.Count > 0)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[yellow]TERMINAL ENGINES GARAGE[/]").LeftJustified());

                var currentCar = AnsiConsole.Prompt(
                    new SelectionPrompt<Car>()
                        .Title("Which vehicle would you like to [green]accept[/] into the shop?")
                        .AddChoices(ShopQueue)
                        .UseConverter(c => $"{c.Manufacturer} {c.VehicleName} ({c.Parts.Count} parts)")
                    );

                BeginRepairWork(currentCar);

                ShopQueue.Remove(currentCar);
                AnsiConsole.MarkupLine("[bold green]Vehicle Complete! Customer paid and left.[/]");
                Thread.Sleep(2000);
            }
            AnsiConsole.MarkupLine("[yellow]No more cars in the queue. Closing shop![/]");
        }

        public static void BeginRepairWork(Car car)
        {
            bool workingOnCar = true;

            while (workingOnCar)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(gameUtilities.JobCard(car));

                var choices = new List<string> { "Finish Job" };
                choices.AddRange(car.Parts.Select(p => $"Fix {p.Name}"));

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices(choices)
                );

                if (action == "Finish Job")
                {
                    if (car.IsJobComplete)
                    {
                        workingOnCar = false;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]You can't finish yet! Parts are still broken.[/]");
                        Thread.Sleep(1500);
                    }
                }
                else
                {
                    string partName = action.Replace("Fix ", "");
                    var selectedPart = car.Parts.FirstOrDefault(p => p.Name == partName);

                    if (selectedPart != null)
                    {
                        AnsiConsole.Status()
                            .Start($"Repairing {selectedPart.Name}...", ctx => {
                                Thread.Sleep(1000); 
                                selectedPart.Repair(100f);
                            });
                    }
                }
            }
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
