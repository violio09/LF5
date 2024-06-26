/* BubbleSort - Sortieren durch Vertauschen
 * 
 * Bei diesem Sortieralgorithmus werden immer zwei benachbarte Elemente 
 * miteinander verglichen. Man vergleicht zuerst das erste mit dem zweiten Element. 
 * Wenn das zweite Element kleiner ist als das erste, so werden die beiden Elemente 
 * miteinander vertauscht. Dann vergleicht man das zweite mit dem dritten Element, 
 * vertauscht beide, wenn das dritte kleiner ist als das zweite usw., bis alle n Elemente 
 * untersucht wurden. Dabei wandern" die größeren Elemente nach rechts. Nach dem 
 * ersten Durchgang steht das größte Element ganz rechts. Jetzt beginnt das Verfahren 
 * von vorne. Man muss jetzt aber nur noch die ersten -1 Elemente sortieren. 
 * Wiederum steigt das größte dieser Elemente wie eine Blase aus dem Wasser nach 
 * oben und befindet sich nach dem 2. Durchgang am Ende der um 1 verkürzten Liste. 
 * Mit jedem Durchgang nimmt die Gruppe der zu sortierenden Elemente um 1 ab und 
 * steigt das jeweils größte Element an das Ende der verkürzten Liste. 
 * Der Sortiervorgang ist beendet, wenn die linke Teilliste auf 1 Element verkürzt wurde.
 */


//// Einlesen der Größe des Arrays
//Console.Write("Geben Sie die Größe des Arrays ein: ");
//int groesse = Convert.ToInt32(Console.ReadLine());

//// Einlesen der unteren Grenze
//Console.Write("Geben Sie die untere Grenze ein: ");
//int uGrenze = Convert.ToInt32(Console.ReadLine());

//// Einlesen der oberen Grenze
//Console.Write("Geben Sie die obere Grenze ein: ");
//int oGrenze = Convert.ToInt32(Console.ReadLine());

//// Initialisieren des Arrays mit Zufallszahlen im Bereich [uGrenze, oGrenze]
//int[] werte = new int[groesse];
//Random rand = new Random();
//for (int i = 0; i < groesse; i++)
//{
//    werte[i] = rand.Next(uGrenze, oGrenze + 1);
//}

Console.WriteLine("_______________________________________________\n" +
        "\nBubbleSort - Sortieren durch Vertauschen");

//Array mit sechs Elementen anlegen
int[] werte = { 7, 3, 8, 6, 9, 4 };

// Ausgabe des unsortierten Arrays
Console.WriteLine("_______________________________________________\n" +
    "\nUnsortiertes Array:");

//Ausgabe der unsortierten Werte
foreach (var wert in werte)
{
    Console.Write(wert + " ");
}

// Bubblesort-Algorithmus
// Äußere Schleife: i beginnt am Ende des Arrays und läuft bis zum zweiten Element
// am Ende des ersten Durchlaufs ist die größte Zahl ans Ende geblubbert, 
// dann die zweitgrößte usw., daher werden die fertig sortierten Zahlen nicht weiter
// berücksichtigt -> i wird dekrementiert
for (int i = werte.Length - 1; i > 0; i--)
{
    Console.WriteLine("\n\n_______________________________________________\n" +
        $"\nArraysortierung im {werte.Length - i}.äußeren (i = {i}) Durchlauf:");
    // Innere Schleife, j beginnt beim ersten Element und läuft gegen i
    for (int j = 0; j < i; j++)
    {
        // Ist das Element an der Stelle j größer als
        // das Element an der Stelle j+1 wird getauscht
        if (werte[j] > werte[j + 1])
        {
            // Werte tauschen
            int temp = werte[j + 1];
            werte[j + 1] = werte[j];
            werte[j] = temp;
        }

        //Ausgabe des Arrays nach einem inneren Durchlauf (nur zur Übersicht)
        Console.WriteLine($"\n\nNach {j + 1}. innerem Durchlauf (j = {j}):");
        foreach (var wert in werte)
        {
            Console.Write(wert + " ");
        }
    }
}

// Ausgabe des sortierten Arrays
Console.WriteLine("\n\n_______________________________________________\n" +
    "\nSortiertes Array:");
foreach (var wert in werte)
{
    Console.Write(wert + " ");
}
Console.WriteLine("\n\nENDE\n");