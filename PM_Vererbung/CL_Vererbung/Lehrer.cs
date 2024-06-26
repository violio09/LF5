using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_Vererbung
{
    public class Lehrer:Person
    {
		private string fach;

		public string Fach
		{
			get { return fach; }
			set { fach = value; }
		}

        public Lehrer(string name, string vorname, string fach)
        : base(name, vorname)
        {
            Fach = fach;
        }

        public override string Vorstellen()
        {
            return base.Vorstellen() + $" Ich unterrichte das Fach {fach}";
        }

    }
}

