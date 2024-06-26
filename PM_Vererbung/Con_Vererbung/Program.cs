using CL_Vererbung;

Person person = new Person("Doe", "John");
Lehrer lehrer = new Lehrer("Fiur","Jan","LF5");


Schüler schüler = new Schüler("Stahl", "Elias","ITF-23-d" ,23);

Console.WriteLine(lehrer.Vorstellen());
Console.WriteLine(lehrer.Unterrichten() + "\n");

Console.WriteLine(schüler.Vorstellen() + "\n");
Console.WriteLine(schüler.Teilnehmen(lehrer) + "\n");