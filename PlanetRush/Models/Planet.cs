using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    internal class Planet
    {
        Random rng = new Random();

        public Planet()
		{
			galaxyCode = 0;
		}

        public Planet(int galaxyCode)
        {
			
			_radius = rng.Next(4, 14);
			_seed = rng.Next();
			_name = GeneratePlanetCode(galaxyCode);
			_tonsOfRawAetherium = rng.Next(10, 111);
			_tonsOfTrilliumAlloys = rng.Next(-10, 31);
			if (_tonsOfTrilliumAlloys < 0)
			{
				_tonsOfTrilliumAlloys = 0;
			}
			_numberOfLightYearsToReach = rng.Next(5 - 20);

			ConfigureCorrectOption();
        }

        private int _radius;

		public int Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		private int _seed;

		public int Seed
		{
			get { return _seed; }
			set { _seed = value; }
		}

		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private int _tonsOfRawAetherium;

		public int TonsOfRawAetherium
		{
			get { return _tonsOfRawAetherium; }
			set { _tonsOfRawAetherium = value; }
		}

		private int _tonsOfTrilliumAlloys;

		public int TonsOfTrilliumAlloys
        {
			get { return _tonsOfTrilliumAlloys; }
			set { _tonsOfTrilliumAlloys = value; }
		}

		private bool _needsTerraforming;

		public bool NeedsTerraforming
		{
			get { return _needsTerraforming; }
			set { _needsTerraforming = value; }
		}

		private bool _needsGenestealing;

		public bool NeedsGenestealing
		{
			get { return _needsGenestealing; }
			set { _needsGenestealing = value; }
		}

		private bool _needsNuking;

		public bool NeedsNuking
		{
			get { return _needsNuking; }
			set { _needsNuking = value; }
		}

		private int _numberOfLightYearsToReach;

		public int NumberOfLightYearsToReach
		{
			get { return _numberOfLightYearsToReach; }
			set { _numberOfLightYearsToReach = value; }
		}

		
		public int ResourceDensity
		{
			get { return ((TonsOfRawAetherium + TonsOfTrilliumAlloys)/10); }
			
		}


		private void ConfigureCorrectOption()
		{
			_needsTerraforming = false;
			_needsGenestealing = false;
			_needsNuking = false;
			int isTrue = rng.Next(3);
			switch (isTrue)
			{
				case 0:
					_needsTerraforming = true;
					break;
				case 1:
					_needsGenestealing = true;
					break;

				case 2:
					_needsNuking = true;
					break;
			}

					
		}

		private string GeneratePlanetCode(int galaxyCode = 0)
		{
			string part1Name;
			if (galaxyCode == 1)
			{
				part1Name = "M31";
			}
			else if (galaxyCode == 2)
			{
				part1Name = "M33";
			}
			else
			{
				part1Name = "MW";
			}

            char randomChar1 = (char)rng.Next('A', 'Z');
            char randomChar2 = (char)rng.Next('A', 'Z');

			int part3Name = rng.Next(1000 - 10000);

            _name = $"{part1Name}-{randomChar1}{randomChar2}-{part3Name}";
				return _name;
		}



	}
}
