using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
