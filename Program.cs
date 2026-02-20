using Spectre.Console;
using System.Threading.Tasks;
using Terminal_Engines.Classes.AccountLogin;
using Terminal_Engines.Classes.ShopClasses;

namespace Terminal_Engines
{
    internal class Program
    {
        static async Task Main(string[] args)
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
        }
    }
}
