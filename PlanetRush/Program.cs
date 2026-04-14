using PlanetRush.Models;
using PlanetRush.Sounds;
using PlanetRush.Visuals;
using System.Numerics;

namespace PlanetRush
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "🚀 Planet Rush";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Player player = new Player();
            int numberOfPlanetsTravelled = 1;
            Planet planet = new Planet();
            bool isSuccesfullExcavation = false;
            

            while (player.LitersOfFuel >= 0)
            {
                AsciiEffects.LaunchRocket();

                #region ShowArrivalText
                string arrivalText = $"You arrive at planet *{planet.Name}*";
                await AsciiEffects.RevealTextAsync(arrivalText, 0, 0, 40);
                #endregion

                #region ShowMenuChooseAction
                int currentSelectedOption = 0;
                int numberOfMenuOptions = 3;
                bool isOptionChosen = false;
                while (!isOptionChosen)
                {
                    AsciiEffects.DrawPlanet(planet);
                    Console.SetCursorPosition(0, 3);
                    WriteActionMenu(currentSelectedOption);
                    isOptionChosen = TryProcessPlayerMenuResponse(ref currentSelectedOption, numberOfMenuOptions);
                    Console.Clear();
                    Console.WriteLine(arrivalText);
                }
                #endregion

                #region ProcessChosenMenuOption
                Console.Clear();
                if ((currentSelectedOption == 0 && planet.NeedsTerraforming) ||
                    (currentSelectedOption == 1 && planet.NeedsGenestealing) ||
                    (currentSelectedOption == 2 && planet.NeedsNuking))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your actions were a success!\nYou depleted the planet of its rare metals.");
                    SoundBoard.PlayVictorySound();
                    Console.WriteLine($"\n\t +{planet.TonsOfRawAetherium} tons of raw Aetherium");
                    Console.WriteLine($"\t +{planet.TonsOfTrilliumAlloys} tons of Trillium alloys");
                    player.TonsOfRawAetherium += planet.TonsOfRawAetherium;
                    player.TonsOfTrilliumAlloys += planet.TonsOfTrilliumAlloys;
                    isSuccesfullExcavation = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your actions ended in disaster.\nThe planet proved too hostile of an environment. No rare metals were depleted.");
                    SoundBoard.PlayFailSound();
                    isSuccesfullExcavation = false;
                }
                Console.ResetColor();
                AsciiEffects.PressEnterToContinue();
                #endregion

                #region RecordTravelLog
                string actionTaken = new string[] { "Terraformed", "Genestealed", "Nuked" }[currentSelectedOption];
                player.RecordTravelLog(numberOfPlanetsTravelled, planet, actionTaken, isSuccesfullExcavation);
                #endregion

                bool isTradingActive = true;
                if (isTradingActive)
                {
                    TraderStation traderStation = new TraderStation(planet);  //TODO: Maak hier een nieuwe object aan van de klasse TraderStation

                    Console.WriteLine("\nBefore you leave the current star,\nyou visit the local trader station.");

                    AsciiEffects.DrawStar(new Random());
                    Console.ReadLine();

                    
                    traderStation.Visit(player);   //TODO: Roep hier de Visit-methode op van het nieuwe object
                }

                #region ShowMenuChoosePlanet
                ShowPlayerInformation(player, numberOfPlanetsTravelled);
                AsciiEffects.PressEnterToContinue();
                Console.Clear();

                Planet nextPlanet1 = new Planet(numberOfPlanetsTravelled / 10);
                Planet nextPlanet2 = new Planet(numberOfPlanetsTravelled / 10);
                Planet nextPlanet3 = new Planet(numberOfPlanetsTravelled / 10);
                Planet[] nextPlanets = { nextPlanet1, nextPlanet2, nextPlanet3 };
                currentSelectedOption = 0;
                isOptionChosen = false;
                Console.WriteLine($"You are currently at {planet.Name}");
                while (!isOptionChosen)
                {
                    AsciiEffects.DrawPlanet(nextPlanets[currentSelectedOption]);
                    Console.SetCursorPosition(0, 3);
                    WriteNextPlanetMenu(currentSelectedOption, nextPlanet1, nextPlanet2, nextPlanet3);
                    isOptionChosen = TryProcessPlayerMenuResponse(ref currentSelectedOption, numberOfMenuOptions);
                    Console.Clear();
                    Console.WriteLine($"You are currently at {planet.Name}");
                }
                #endregion

                #region ProcessChosenPlanet
                switch (currentSelectedOption)
                {
                    case 0:
                        planet = nextPlanet1;
                        break;
                    case 1:
                        planet = nextPlanet2;
                        break;
                    default:
                        planet = nextPlanet3;
                        break;
                }
                player.TravelLightYears(planet.NumberOfLightYearsToReach);
                numberOfPlanetsTravelled++;
                #endregion

            }

            Console.Clear();
            SoundBoard.PlayFailSound();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You sadly ran out of fuel... \nYour spacecraft now drifts in the enternity of the abyss.\n\n");
            Console.ResetColor();
            ShowPlayerInformation(player, numberOfPlanetsTravelled, false);
        }

        private static bool TryProcessPlayerMenuResponse(ref int currentSelectedOption, int numberOfMenuOptions)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    SoundBoard.PlayUISound();
                    currentSelectedOption = (currentSelectedOption - 1 + numberOfMenuOptions) % numberOfMenuOptions;
                    return false;
                case ConsoleKey.DownArrow:
                    SoundBoard.PlayUISound();
                    currentSelectedOption = (currentSelectedOption + 1) % numberOfMenuOptions;
                    return false;
                case ConsoleKey.Enter:
                    SoundBoard.PlayConfirmSound();
                    return true;
                default:
                    return false;
            }
        }

        private static void ShowPlayerInformation(Player player, int numberOfPlanetsVisited, bool isShowingFuel = true)
        {
            Console.WriteLine($"\nYou currently visited {numberOfPlanetsVisited} planets");
            Console.WriteLine($"\nDuring your travels you collected a treasure of:");
            Console.WriteLine($"\t{player.TonsOfRawAetherium} tons of Aetherium");
            Console.WriteLine($"\t{player.TonsOfTrilliumAlloys} tons of Trillium alloys");
            if (isShowingFuel)
            {
                Console.WriteLine($"\nYour spacecraft has {player.LitersOfFuel} liters of fuel left");
            }
        }

        private static void WriteNextPlanetMenu(int selectedOption, Planet p1, Planet p2, Planet p3)
        {
            AsciiEffects.WriteMenuOption($"1. {p1.Name} - Resource Density {p1.ResourceDensity} - {p1.NumberOfLightYearsToReach} light years away", selectedOption == 0);
            AsciiEffects.WriteMenuOption($"2. {p2.Name} - Resource Density {p2.ResourceDensity} - {p2.NumberOfLightYearsToReach} light years away", selectedOption == 1);
            AsciiEffects.WriteMenuOption($"3. {p3.Name} - Resource Density {p3.ResourceDensity} - {p3.NumberOfLightYearsToReach} light years away", selectedOption == 2);
        }

        private static void WriteActionMenu(int selectedOption)
        {
            AsciiEffects.WriteMenuOption("1. Terraform the planet - change the planet to be more earthlike", selectedOption == 0);
            AsciiEffects.WriteMenuOption("2. Genesteal DNA from the exotic lifeforms - assimilate our explorers into the native population", selectedOption == 1);
            AsciiEffects.WriteMenuOption("3. Nuke its inhabitants - remove resistance with violence", selectedOption == 2);
        }
    }
}
