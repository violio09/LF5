while (true)
{
	try
	{
		Console.WriteLine("Geben sie eine Zahl ein, die durch eine zufällige Zahl geteilt wird");
		int eingabe = Convert.ToInt32(Console.ReadLine());


		Random r = new Random();
		int random = r.Next(1, 10);

		int ausgabe = random / eingabe;

		Console.WriteLine($"Glückwunsch, sie haben das Ergebnis von {ausgabe}");

		break;
	}
	catch (DivideByZeroException e)
	{

		Console.WriteLine(e.Message);

	}
	catch (FormatException e)
	{

		Console.WriteLine(e.Message);

	}
}