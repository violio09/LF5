﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_Vererbung
{
    public class Schüler:Person
    {
        private string klasse;
        private int alter;
        public int note;

        private string Klasse
        {
            get { return klasse; }
            set { klasse = value; }
        }

        private int Alter
        {
            get { return alter; }
            set { alter = value; }
        }


        public int Note
        {
            get { return alter; } 
            set { note = value; }
        }

        public Schüler(string name, string vorname, string klasse, int alter)
        : base(name, vorname)
        {
            Klasse = klasse;
            Alter = alter;
            Note = note;
        }

        public override string Vorstellen()
        {
            return base.Vorstellen() + $"Ich bin {alter} Jahre alt. Gerade bin ich in der Klasse {klasse}.";
        }

        public string Teilnehmen(Lehrer teacher)
        {
            return $"Hallo Herr {teacher.Name}. Ich mag ihren {teacher.Fach} Unterricht";
        }
    }
}
