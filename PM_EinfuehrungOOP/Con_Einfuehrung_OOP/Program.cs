using CL_EinfuehrungOOP;
using System.Formats.Asn1;
using System.Globalization;


//Erzeugen eines neuen Objekts vom Typ Person und Zuweisung an Referenz person
//Instanziierung
Person person2 = new Person();

Person person3 = new Person();

Person person = new Person();




Console.WriteLine("Bitte geben sie ihren Vorname ein: ");
person2.Vorname = Console.ReadLine();

Console.Clear();

Console.WriteLine("Bitte geben sie ihren Nachname ein: ");
person2.Name = Console.ReadLine();

Console.Clear();

Console.WriteLine("Bitte geben sie ihren Geburtsort ein: ");
person2.GebOrt = Console.ReadLine();

Console.Clear();

Console.WriteLine("Wie alt sind sie? ");
person2.Alter = Convert.ToInt32(Console.ReadLine());

person2.BrthDay = "19.09.2002";


Console.Clear();
Console.WriteLine("\n");
Console.WriteLine("\n");
Console.WriteLine("\n");
Console.Clear();


Console.WriteLine("Bitte geben sie den Vorname der zweiten Person ein: ");
person3.Vorname = Console.ReadLine();

Console.Clear();

Console.WriteLine("Bitte geben sie den Nachname der zweiten Person ein: ");
person3.Name = Console.ReadLine();

Console.Clear();

Console.WriteLine("Bitte geben sie den Geburtsort der zweiten Person ein: ");
person3.GebOrt = Console.ReadLine();

Console.Clear();

Console.WriteLine("Wie alt ist die andere Person? ");
person3.Alter = Convert.ToInt32(Console.ReadLine());

person3.BrthDay = "09.01.2001";
//person2.brthDay = DateOnly(2004, 01, 09);

Console.Clear();

Console.WriteLine($"Hallo {person.Vorname} bitte stellen Sie sich vor.");
Console.WriteLine(person.Vorstellen() + "\n");

Console.WriteLine($"Hallo {person2.Vorname} bitte stellen Sie sich vor.");
Console.WriteLine(person2.Vorstellen() + "\n");

Console.WriteLine("\n");

Console.WriteLine($"Und nun sie,{person3.Vorname} stellen sie sich vor");
Console.WriteLine(person3.Vorstellen() + "\n");