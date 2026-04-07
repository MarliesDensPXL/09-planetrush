using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    internal class Player
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

		public int LitersOfFuel
		{
			get { return Spacecraft.CurrentFuel; }
			
		}

	}
}
