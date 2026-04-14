using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
	public class Player
	{
		private int _tonsOfTrilliumAlloys;

		public int TonsOfTrilliumAlloys
		{
			get { return _tonsOfTrilliumAlloys; }
			set { _tonsOfTrilliumAlloys = value; }
		}

		private int _tonsOfRawAetherium;

		public int TonsOfRawAetherium
		{
			get { return _tonsOfRawAetherium; }
			set { _tonsOfRawAetherium = value; }
		}

		private Spacecraft _spacecraft;

		public Spacecraft Spacecraft
		{
			get { return _spacecraft; }
			set { _spacecraft = value; }
		}

		

		public Player()
		{
			Spacecraft = new Spacecraft(); //code aangepast van 'Spacecraft spacecraft = new Spacecraft(); naar dit (op aanraden van Copilot)
			_travelLogs = new List<TravelLog>();
		}

        public int LitersOfFuel
        {

            get { return Spacecraft.CurrentFuel; }

        }

        public void AddFuel(int fuel)
		{
			Spacecraft.CurrentFuel += fuel;
		}

		public void TravelLightYears(int ligthYears)
		{
			Spacecraft.CurrentFuel -= (ligthYears * Spacecraft.EngineEfficiency);
		}

		private List<TravelLog> _travelLogs;

		public void RecordTravelLog(int number, Planet planetVisited, string actionTaken, bool isSuccesfullyDepleted) //, int rawAetheriumDepleted, int trilliumAlloysDepleted)
		{
			TravelLog log = new TravelLog(number, planetVisited, actionTaken, isSuccesfullyDepleted); //, rawAetheriumDepleted, trilliumAlloysDepleted);

			_travelLogs.Add(log);
		}

		public List<string[]> GetTravelLogRows()
		{
			List<string[]> rows = new List<string[]>();

			foreach (TravelLog log in _travelLogs)
			{
				rows.Add(log.ToStringArray());
			}


			return rows;
		}

    }
}
