using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal_Engines.Classes.Vehicles;

namespace Terminal_Engines.Classes.ShopClasses
{
    public class GameUtilities
    {
        public void BootGame()
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("blue"))
                .Start("Opening the shop...", ctx =>
                {
                    Thread.Sleep(2000);

                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    ctx.Status("Turning on the lights...");
                    Thread.Sleep(2000);

                    ctx.Spinner(Spinner.Known.Arc);
                    ctx.SpinnerStyle(Style.Parse("blue"));
                    ctx.Status("Revving the engines...");
                    Thread.Sleep(1500);
                });

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green]All engines are go![/]");
            Thread.Sleep(1000);
            AnsiConsole.Clear();
        }

        public Table JobCard(Car car)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Blue)
                .Title($"[yellow]JOB CARD: {car.Manufacturer} {car.VehicleName}[/]")
                .AddColumn("Status") 
                .AddColumn("Component")
                .AddColumn("Condition")
                .AddColumn("Technical Details");

            foreach (var part in car.Parts)
            {
                string statusIcon = part.IsFine ? "[green]✔ PASS[/]" : "[red]✖ FAIL[/]";

                string conditionColour = part.Condition >= 85f ? "green" : (part.Condition < 50f ? "red" : "yellow");

                table.AddRow(
                    statusIcon,
                    part.Name,
                    $"[{conditionColour}]{part.Condition}%[/]",
                    part.GetStatusReport()
                );
            }

            return table;
        }
    }
}
