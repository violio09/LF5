void expEingabe()
{
    try
    {
        Console.WriteLine("Bitte geben sie ihr Alter ein \n");

        int alter = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Sehr gut, sie sind also {alter} Jahre alt \n");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
        Console.WriteLine("Ein Alter besteht nur aus Zahlen. Bitte erneut versuchen\n");
    }
    finally
    {
        Console.WriteLine("Hallo, ich bin der Code der immer dabei ist");
    }
}

expEingabe();
