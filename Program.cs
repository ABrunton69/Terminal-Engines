using Spectre.Console;
using System.Threading.Tasks;
using Terminal_Engines.Classes.AccountLogin;
using Terminal_Engines.Classes.ShopClasses;
using Terminal_Engines.Classes.Vehicles;
using Terminal_Engines.Classes.Vehicles.VehicleComponents;
using Terminal_Engines.Services;

namespace Terminal_Engines
{
    internal class Program
    {
        private static GameUtilities gameUtilities = new GameUtilities();
        private static Account? CurrentAccount;
        private static List<Car>? ShopQueue;

        public static void Main(string[] args)
        {
            // Initialise ShopManager for car queue
            var shopManager = new ShopManager();
            ShopQueue = shopManager.LoadShopQueue("C:\\Users\\amdru\\Desktop\\Terminal-Engines\\Data\\Vehicles.json");

            gameUtilities.BootGame();

            CurrentAccount = new Account();
            CurrentAccount.credits = 1000;

            var username = AnsiConsole.Ask<string>("What's your [green]username[/]?");
            CurrentAccount.UserName = username;

            AcceptVehicleIntoShop();
        }

        public static void AcceptVehicleIntoShop()
        {
            while (ShopQueue?.Count > 0)
            {

                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule($"[yellow]TERMINAL ENGINES GARAGE | LOGGED IN AS[/] [green]{CurrentAccount?.UserName}[/] | [blue]{CurrentAccount?.credits}cr[/]").LeftJustified());

                var currentCar = AnsiConsole.Prompt(
                    new SelectionPrompt<Car>()
                        .Title("Which vehicle would you like to [green]accept[/] into the shop?")
                        .AddChoices(ShopQueue)
                        .UseConverter(c => $"{c.Manufacturer} {c.VehicleName} ({c.Parts.Count} parts)")
                    );

                BeginRepairWork(currentCar);

                ShopQueue.Remove(currentCar);
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

                var choices = new List<string> { "[bold green]Submit Vehicle to Customer[/]" };
                choices.AddRange(car.Parts.Select(p => p.Name));

                var mainAction = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nSelect a component to work on, or return the car:")
                        .AddChoices(choices)
                );

                if (mainAction == "[bold green]Submit Vehicle to Customer[/]")
                {
                    var brokenParts = car.Parts.Where(p => !p.IsFine).ToList();

                    if (brokenParts.Any())
                    {
                        AnsiConsole.MarkupLine("\n[bold red]The customer is furious![/]");
                        AnsiConsole.MarkupLine("They checked the car and found the following issues were ignored:");

                        foreach (var part in brokenParts)
                        {
                            AnsiConsole.MarkupLine($"- [yellow]{part.Name}[/]: {part.GetStatusReport()}");
                        }

                        AnsiConsole.MarkupLine("\n[red]You lost shop reputation and received NO payment for this job![/]");
                    }
                    else
                    {
                        var rnd = new Random();
                        var pay = rnd.Next(250, 1001);
                        AnsiConsole.MarkupLine("\n[bold green]The customer is thrilled![/]");
                        AnsiConsole.MarkupLine($"The car runs perfectly. You got paid {pay}cr");
                        CurrentAccount!.credits += pay;
                    }

                    AnsiConsole.MarkupLine("\n[grey]Press any key to return to the garage...[/]");
                    Console.ReadKey(true);

                    workingOnCar = false; 
                    continue;
                }

                var selectedPart = car.Parts.First(p => p.Name == mainAction);

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"What do you want to do with {selectedPart.Name}?")
                        .AddChoices(selectedPart.GetActions())
                );

                if (action != "Back")
                {
                    selectedPart.ExecuteAction(action);
                }
            }
        }
    }
}
