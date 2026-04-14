using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using PlanetRush.Sounds;
using PlanetRush.Visuals;

namespace PlanetRush.Models
{
    public class TraderStation
    {
        private const string TrilliumAlloys = "Trillium Alloys";
        private const string RawAetherium = "Raw Aetherium";

        private string _desiredMetal = ""; // waarschuwingsmelding wegwerken bij de constructur TraderStation door hier alvast een lege waarde toe te kennen

        public string DesiredMetal
        {
            get { return _desiredMetal; }
            set
            {
                if (value == TrilliumAlloys || value == RawAetherium) // value controleren, niet _desiredMetal, want value is de nieuwe waarde die binnenkomt, _desiredMetal is de huidige interne toestand
                {
                    _desiredMetal = value;
                }
                else
                {
                    _desiredMetal = "NONE";
                }
            }
        }

        private bool _hasWarpDriveChargesForSale;

        public bool HasWarpDriveChargesForSale
        {
            get { return _hasWarpDriveChargesForSale; }
            set { _hasWarpDriveChargesForSale = value; }
        }

        private int _fuelForMetalRate;

        public int FuelForMetalRate
        {
            get { return _fuelForMetalRate; }
            set { _fuelForMetalRate = value; }
        }

        private Planet _planet;

        public Planet Planet
        {
            get { return _planet; }
            set { _planet = value; }
        }

        public TraderStation(Planet planet)
        {
            Planet = planet;
                        
            Random rng = new Random();
            int desiredMetalOption = rng.Next(2);
            switch (desiredMetalOption)
            {
                case 0:
                    DesiredMetal = "Trillium Alloys"; //property ipv veld? 
                    FuelForMetalRate = rng.Next(10, 20);
                    break;

                case 1:
                    DesiredMetal = "Raw Aetherium";
                    
                    FuelForMetalRate = rng.Next(2, 7);
                    break;
                
            }

            HasWarpDriveChargesForSale = (rng.Next(10) == 0); // alleen als het 0 is true, bij 1 tem 9 false
        }

        public void Visit(Player player)
        {
            Console.WriteLine(@$"The alien-looking trader greets you as you enter his home.
            The station is crawling with robotic servants.

             'Welcome in the Trader Station of {Planet.Name}.
             I'm looking to buy {DesiredMetal}. Do you have any?
              I can trade you for it, fellow space explorer.'
               ");

            string traderStationOptions = $" 1. Sell {DesiredMetal} for Fuel ({FuelForMetalRate} per ton of {DesiredMetal})\n" +
" 2. Leave and travel to the next planet\n" +
" 3. Look at your travel logs";
            string option;
            do
            {
                Console.WriteLine($"{traderStationOptions}");
                Console.WriteLine();
                Console.Write("Choose an option: ");
                option = Console.ReadLine();
                while (option != "1" && option != "2" && option != "3")
                {
                    Console.Write("Choose a valid option instead: ");
                    option = Console.ReadLine();
                }
                switch (option)
                {
                    case "1":
                        SellMetals(player);
                        break;

                    case "2":
                        string leaveTraderStation = "You leave the safety of the trader station and journey back into the eternal void of deepspace.";
                        Console.WriteLine($"{leaveTraderStation}");
                        break;
                    case "3":
                        Console.WriteLine("In the safety of the trader station you take the time to look at your travel logs.");
                        AsciiEffects.PressEnterToContinue();
                        TableViewer.ViewTable(player.GetTravelLogRows(), TravelLog.GetStringArrayHeaders(), true);
                        break;
                }


            } while (option != "2") ;


        }

        private void SellMetals(Player player)
        {
            int carriedTons = DesiredMetal == TrilliumAlloys ? player.TonsOfTrilliumAlloys : player.TonsOfRawAetherium;
            Console.WriteLine("How many tons do you sell?");
            Console.WriteLine($"You currently carry {carriedTons} tons.");
            Console.WriteLine($"Your spacecraft has {player.Spacecraft.CurrentFuel}/{player.Spacecraft.FuelCapacity} liters.");
            int chosenAmount;
            while (!int.TryParse(Console.ReadLine(), out chosenAmount) || chosenAmount > carriedTons || chosenAmount < 0)
            {
                SoundBoard.PlayFailSound();
                Console.Write("Chose a valid amount instead: ");
            }
            if (DesiredMetal == TrilliumAlloys)
            {
                player.TonsOfTrilliumAlloys -= chosenAmount;
            }
            else
            {
                player.TonsOfRawAetherium -= chosenAmount;
            }

            player.AddFuel(chosenAmount * FuelForMetalRate);
            SoundBoard.PlayConfirmSound();
            Console.WriteLine($"You sold {chosenAmount} tons of metals and bought {chosenAmount * FuelForMetalRate} liters of fuel");
        }

    }

}
