    
string title = "Achtung!!!"; //Warnmeldung
string message = "Arbeitsspeicherauslastung über 85%"; //Warnmeldung
int avgUsedRam = 0; //Mittelwert Tag
int sumTemp = 0; //Summe der Stundenwerte

int[] usedRAM = new int[24]
{17,100,16,18,100,25,33,44,40,85,60,33,33,84,100,52,60,56,33,84,34,28,23,16};

for (int i = 0; i < usedRAM.Length; i++)
{
    sumTemp += usedRAM[i];
}

avgUsedRam = sumTemp / usedRAM.Length;

if (avgUsedRam > 85)
{
    Console.WriteLine(title + " " + message + " // RAM AUSLASTUNG BEI " + avgUsedRam + "%");
}
else { Console.WriteLine("Die durchschnittliche RAM Auslastung liegt bei " + avgUsedRam + "%. Alles gut. Die Auslastung sieht gut aus"); }