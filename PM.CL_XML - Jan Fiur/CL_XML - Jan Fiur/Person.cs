using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_XML___Jan_Fiur
{
    internal class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int Alter { get; set; }
        public string Beruf { get; set; }
        public Person(string vorname, string nachname, int alter, string beruf)
        {
            Vorname = vorname;
            Nachname = nachname;
            Alter = alter;
            Beruf = beruf;
        }
    }
}
