using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    internal class Planet
    {
		public Planet()
		{
			galaxyCode = 0;
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




	}
}
