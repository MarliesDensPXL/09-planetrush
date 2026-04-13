using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    public class TravelLog
    {
        private string[] _actionsTaken = { "Terraformed", "Genestealed", "Nuked" };

		private int _number;

		public int Number
		{
			get { return _number; }
			set { _number = value; }
		}

		private Planet _planetVisited;

		public Planet PlanetVisited
		{
			get { return _planetVisited; }
			set { _planetVisited = value; }
		}

		private string _actionTaken;

		public string ActionTaken
		{
			get { return _actionTaken; }
			set 
				{ 
				if (value.Equals("Terraformed") || value.Equals("Genestealed") || value.Equals("Nuked"))
				{
                    _actionTaken = value;
                }
				 }
		}

		private bool _isSuccesfullyDepleted;

		public bool IsSuccesfullyDepleted
		{
			get { return _isSuccesfullyDepleted; }
			set { _isSuccesfullyDepleted = value; }
		}


		private int _rawAetheriumDepleted;

		public int RawAetheriumDepleted
		{
			get { return _rawAetheriumDepleted; }
		}

		private int _trilliumAlloysDepleted;

		public int TrilliumAlloysDepleted
		{
			get { return _trilliumAlloysDepleted; }
		}

		public TravelLog(int number, Planet planetVisited, string actionTaken, bool isSuccesfullyDepleted, int rawAetheriumDepleted, int trilliumAlloysDepleted)
        {
			Number = number;
			PlanetVisited = planetVisited;
			ActionTaken = actionTaken;
			IsSuccesfullyDepleted = isSuccesfullyDepleted;

			_rawAetheriumDepleted = rawAetheriumDepleted;
			_trilliumAlloysDepleted = trilliumAlloysDepleted;


        }

		public static string[] GetStringArrayHeaders()
		{
			return new string[]
			{
				"Number","Planet Name","Action Taken","Succes?","Raw Aetherium Depleted (Tons)","Trillium Alloys Depleted (Tons)"
		};
				
				
		}

		public string[] ToStringArray()
		{

			string rawAetherium = "0";
			string trilliumAlloys = "0";

			if (IsSuccesfullyDepleted)
			{
				rawAetherium = RawAetheriumDepleted.ToString();
				trilliumAlloys = TrilliumAlloysDepleted.ToString();
			}
			return new string[]
			{
				  Number.ToString(),
				  PlanetVisited.ToString(),
				  ActionTaken,
				  IsSuccesfullyDepleted.ToString(),
				  rawAetherium, 
				  trilliumAlloys
			};

		}





    }
}
