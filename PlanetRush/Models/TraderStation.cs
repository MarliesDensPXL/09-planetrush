using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

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
                if (_desiredMetal.Equals(TrilliumAlloys) || _desiredMetal.Equals(RawAetherium))
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
            int desiredMetalChance = rng.Next(2);
            switch (desiredMetalChance)
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


    }

}
