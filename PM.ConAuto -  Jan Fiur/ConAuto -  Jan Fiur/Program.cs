using CL_Auto;

Car automobil = new Car();


Console.WriteLine(automobil.AutoShowRoom());

try
{
    Console.WriteLine("Wilkommen bei der Tanke. Wie viel Liter wollen sie tanken");

    Console.WriteLine(automobil.Refuel(Convert.ToDouble(Console.ReadLine())));

    Console.WriteLine("Wie weit wollen sie fahren? (km)");

    Console.WriteLine(automobil.drive(Convert.ToDouble(Console.ReadLine())));
}
catch (FormatException ex)
{
    Console.WriteLine(ex.Message);
}
