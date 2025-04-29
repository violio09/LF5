using CL_XML___Jan_Fiur;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
using System;

var xmlPfad = XDocument.Load("C:\\Users\\Jan Maximilian Fiur\\source\\repos\\violio09\\LF5\\PM.CL_XML - Jan Fiur\\CL_XML - Jan Fiur\\xmlFiles\\personen.xml");

string vorname = "";
string nachname = "";
int alter = 0;
string beruf = "";

XDocument xdoc = new XDocument(xmlPfad);

foreach (XElement person in xdoc.Descendants("person"))
{
    Console.WriteLine("-------------------------------------------------");

    vorname = person.Element("firstname")?.Value ?? string.Empty;
    nachname = person.Element("lastname")?.Value ?? string.Empty;
    alter = Convert.ToInt32(person.Element("age")?.Value ?? "0");
    beruf = person.Element("profession")?.Value ?? string.Empty;

    Person Mensch = new Person(vorname, nachname, alter, beruf);

    if (vorname == "Ben" && nachname == "Schulz")
    {
        Mensch.Alter = alter + 1;
    }

    Console.WriteLine("Vorname: " + Mensch.Vorname);
    Console.WriteLine("Nachname: " + Mensch.Nachname);
    Console.WriteLine("Alter: " + Convert.ToString(Mensch.Alter));
    Console.WriteLine("Beruf: " + Mensch.Beruf);

    Console.WriteLine("------------------------------------------------- \n");
}

Console.WriteLine("Wie viele Personen möchten Sie hinzufügen?");
int anzahl = Convert.ToInt32(Console.ReadLine());

for (int i = 0; i < anzahl; i++)
{
    Console.WriteLine("\n" + "Bitte geben Sie den Vornamen ein:");
    string vornameNeu = Console.ReadLine();

    Console.WriteLine("\n"+"Bitte geben Sie den Nachnamen ein:");
    string nachnameNeu = Console.ReadLine();

    Console.WriteLine("\n" + "Bitte geben Sie das Alter ein:");
    int alterNeu = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("\n" + "Bitte geben Sie den Beruf ein:");
    string berufNeu = Console.ReadLine();

    Person MenschNeu = new Person(vornameNeu, nachnameNeu, alterNeu, berufNeu);

  
    xdoc.Root.Add(new XElement("person",
        new XElement("firstname", MenschNeu.Vorname),
        new XElement("lastname", MenschNeu.Nachname),
        new XElement("age", MenschNeu.Alter),
        new XElement("profession", MenschNeu.Beruf)));
    

    xdoc.Save("C:\\Users\\Jan Maximilian Fiur\\source\\repos\\violio09\\LF5\\PM.CL_XML - Jan Fiur\\CL_XML - Jan Fiur\\xmlFiles\\personen3.xml");
}
