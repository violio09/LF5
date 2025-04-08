using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JsonCon___Jan_Fiur
{
    public class Person
    {
        public string vorname { get; set; }

        public string nachname { get; set; }

        public int alter {  get; set; } 

        public string beruf {  get; set; }

        public Person(string vorname, string nachname, int alter, string beruf) 
        {
            this.vorname = vorname;
            this.nachname = nachname;
            this.alter = alter;
            this.beruf = beruf;
        }
    }
}
