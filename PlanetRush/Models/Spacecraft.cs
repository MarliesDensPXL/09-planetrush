using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    internal class Spacecraft
    {
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private int _fuelCapacity;

		public int FuelCapacity
		{
			get { return _fuelCapacity; }
			set { _fuelCapacity = value; }
		}

		private int _cargoCapacity;

		public int CargoCapacity
		{
			get { return _cargoCapacity; }
			set { _cargoCapacity = value; }
		}

		private int _engineEfficiency;

		public int EngineEfficiency
		{
			get { return _engineEfficiency; }
			set { _engineEfficiency = value; }
		}

		private bool _hasWarpDriveCharge;

		public bool HasWarpDriveCharge
		{
			get { return _hasWarpDriveCharge; }
			set { _hasWarpDriveCharge = value; }
		}



		private int _currentFuel;

		public int CurrentFuel
		{
			get { return _currentFuel; }
			set { _currentFuel = (Math.Min(value, FuelCapacity)); }
		}


	}
}
