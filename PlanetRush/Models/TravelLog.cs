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





	}
}
