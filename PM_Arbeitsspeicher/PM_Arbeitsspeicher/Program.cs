/* Berechnung Mittelwert Arbeitsspeicherauslastung */

string title = "Achtung!!!"; //Warnmeldung
string message = "Arbeitsspeicherauslastung über 85%"; //Warnmeldung
int avgUsedRam = 0; //Mittelwert Tag
int sumTemp = 0; //Summe der Stundenwerte

//Array mit Testdaten
int[] usedRAM = new int[24]
{17,17,16,18,20,25,33,44,40,52,60,56,33,44,40,52,60,56,33,44,34,28,23,16};

for (int i = 0; i < usedRAM.length; i++)
{
    avgUsedRam = usedRAM[i];

    if(avgUsedRam > 85)
    {
        Console.WriteLine(message);
    }
}