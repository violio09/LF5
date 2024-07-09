using CL.Lotto___Jan_Fiur;

Lotto ziehZahlen = new Lotto();

List<int> userZahlen = new List<int>();

int richtigeZahlen = 0;
int falscheZahlen = 0;

for (int i = 0; i < 6; i++)
{
    try
    {
        Console.WriteLine("Bitte geben sie ihre Tippzahl ein.");


        userZahlen.Add(Convert.ToInt32(Console.ReadLine()));

        Console.Clear();
    }
    catch(Exception e) 
    { 
        Console.WriteLine(e); 
        Environment.Exit(1);
    }
}

List<int> calledList = ziehZahlen.GetList();

foreach (int called in calledList)
{
    Console.Write("|" + called + "|");
}

Console.WriteLine("\n");

foreach (int nutzer in userZahlen)
{
    Console.Write("|" + nutzer + "|");
}

for(int i = 0;i < 6; i++)
{
    if (calledList[i] == userZahlen[i])
    {
        richtigeZahlen++;
    }
    else{ 

    falscheZahlen++; 

    }
}
Console.WriteLine("\n");
Console.WriteLine("Die Überprüfung hatte folgendes ergeben: \n");
Console.WriteLine("Sie hatten " + richtigeZahlen + " richtige Zahlen und " + falscheZahlen + " falsche Zahlen");


