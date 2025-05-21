using System;
using System.Collections.Generic;
using System.Linq;

public class WerwolfSpiel
{
    public static void Main(string[] args)
    {
        int anzahlSpieler = 0;
        bool inputValid = false;

        // Eingabe der Spieleranzahl mit Validierung
        while (!inputValid)
        {
            Console.WriteLine("Gib die Anzahl der Spieler ein (mindestens 3):");
            string eingabe = Console.ReadLine();
            if (int.TryParse(eingabe, out anzahlSpieler))
            {
                if (anzahlSpieler >= 3) // Mindestanzahl für ein sinnvolles Spiel
                {
                    inputValid = true;
                }
                else
                {
                    Console.WriteLine("Es werden mindestens 3 Spieler benötigt.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Bitte gib eine Zahl ein.");
            }
        }

        string[] spielerRollen = new string[anzahlSpieler];
        List<string> zuVergebendeRollen = new List<string>();
        Random random = new Random();

        // Definiere maximale Anzahl für jede Rolle
        int maxWerwolf = 5;
        int maxJäger = 1;
        int maxHexe = 1;
        int maxAmor = 1;
        int maxBürgermeister = 1;

        int aktuelleRollenAnzahlInListe = 0;

        // Füge einzigartige Rollen hinzu, wenn genügend Spieler vorhanden sind
        // und die Rolle noch nicht vergeben wurde
        // Reihenfolge kann die Priorisierung bei wenigen Spielern beeinflussen

        // Jäger
        if (aktuelleRollenAnzahlInListe < anzahlSpieler && maxJäger > 0)
        {
            zuVergebendeRollen.Add("Jäger");
            maxJäger--; // Zähler für diese spezifische Rolle dekrementieren
            aktuelleRollenAnzahlInListe++;
        }

        // Hexe
        if (aktuelleRollenAnzahlInListe < anzahlSpieler && maxHexe > 0)
        {
            zuVergebendeRollen.Add("Hexe");
            maxHexe--;
            aktuelleRollenAnzahlInListe++;
        }

        // Amor
        if (aktuelleRollenAnzahlInListe < anzahlSpieler && maxAmor > 0)
        {
            zuVergebendeRollen.Add("Amor");
            maxAmor--;
            aktuelleRollenAnzahlInListe++;
        }

        // Bürgermeister
        if (aktuelleRollenAnzahlInListe < anzahlSpieler && maxBürgermeister > 0)
        {
            zuVergebendeRollen.Add("Bürgermeister");
            maxBürgermeister--;
            aktuelleRollenAnzahlInListe++;
        }

        // Füge Werwölfe hinzu
        // Anzahl der Werwölfe ist maximal `maxWerwolf` oder die Anzahl der verbleibenden Spieler-Slots
        int werwölfeHinzuzufügen = Math.Min(maxWerwolf, anzahlSpieler - aktuelleRollenAnzahlInListe);
        for (int k = 0; k < werwölfeHinzuzufügen; k++)
        {
            if (aktuelleRollenAnzahlInListe < anzahlSpieler)
            {
                zuVergebendeRollen.Add("Werwolf");
                aktuelleRollenAnzahlInListe++;
            }
            else break; // Sollte nicht passieren, wenn Logik korrekt ist
        }

        // Fülle die restlichen Plätze mit Dorfbewohnern
        while (aktuelleRollenAnzahlInListe < anzahlSpieler)
        {
            zuVergebendeRollen.Add("Dorfbewohner");
            aktuelleRollenAnzahlInListe++;
        }

        // Mische die Liste der zu vergebenden Rollen
        // LINQ OrderBy mit random.Next() ist eine einfache Methode zum Mischen
        zuVergebendeRollen = zuVergebendeRollen.OrderBy(x => random.Next()).ToList();

        // Rollen zuweisen und anzeigen
        for (int i = 0; i < anzahlSpieler; i++)
        {
            // Sicherstellen, dass genügend Rollen in der gemischten Liste sind
            // Dies sollte immer der Fall sein, da die Liste oben auf anzahlSpieler aufgefüllt wurde
            if (i < zuVergebendeRollen.Count)
            {
                spielerRollen[i] = zuVergebendeRollen[i];
            }
            else
            {
                // Fallback, falls etwas schiefgelaufen ist (sollte nicht eintreten)
                spielerRollen[i] = "Dorfbewohner";
                Console.WriteLine($"Warnung: Nicht genügend spezifische Rollen für Spieler {i + 1}, wurde Dorfbewohner.");
            }


            Console.WriteLine($"Spieler {i + 1} ist an der Reihe.");
            Console.WriteLine("Drücke ENTER, um deine Rolle geheim zu sehen.");
            Console.ReadLine(); // Wartet auf Enter-Druck

            Console.Clear(); // Löscht die Konsole

            Console.WriteLine($"Du bist: {spielerRollen[i]}");
            Console.WriteLine("\nBitte merke dir deine Rolle.");
            Console.WriteLine("Drücke ENTER, um den Bildschirm für den nächsten Spieler zu löschen.");
            Console.ReadLine(); // Wartet auf Enter-Druck

            Console.Clear(); // Löscht die Konsole erneut
        }

        Console.WriteLine("Alle Rollen wurden vergeben. Das Spiel kann beginnen!");
    }
}
