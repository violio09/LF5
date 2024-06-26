while (true)
{
	try
	{
		int[] array = { 1, 2, 3 };

		Console.WriteLine("Welche stelle vom Array wollen sie abfragen?");
		int eingabe = Convert.ToInt32(Console.ReadLine());

		Console.WriteLine(array[eingabe]);

		break;
	}
	catch (IndexOutOfRangeException e)
	{
		Console.WriteLine(e.Message);
	}
	catch(FormatException e)
	{
		Console.WriteLine(e.Message);
	}
}