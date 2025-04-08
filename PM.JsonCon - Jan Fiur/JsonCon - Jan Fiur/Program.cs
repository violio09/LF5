using System.Text.Json;
using System.Xml;
using JsonCon___Jan_Fiur;


string json = File.ReadAllText("C:/LF5/PM.JsonCon - Jan Fiur/JsonCon - Jan Fiur/jsonFiles/personen.json");

Person[] personenArray = JsonSerializer.Deserialize<Person[]>(json);

for (int i = 0; i < personenArray.Length; i++)
{
    Person person = personenArray[i];

    if (person.vorname == "Ben" && person.nachname == "Schulz")
    {
        person.alter = 26;
    }

    Console.WriteLine($"Vorname: {person.vorname}");
    Console.WriteLine($"Nachname: {person.nachname}");
    Console.WriteLine($"Alter: {person.alter}");
    Console.WriteLine($"Beruf: {person.beruf} \n");

    string jsonString = JsonSerializer.Serialize(personenArray[i]);

    File.AppendAllLines("C:/LF5/PM.JsonCon - Jan Fiur/JsonCon - Jan Fiur/jsonFiles/neuePersonen.json", [jsonString]);
}



