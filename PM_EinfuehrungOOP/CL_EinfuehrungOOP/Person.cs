namespace CL_EinfuehrungOOP
{
    //Deklariert eine neue Klasse namens “Person“
    public class Person { 

    public string Name { get; set; }

    public string Vorname { get; set; }

    public int Alter { get; set; }

    public string GebOrt { get; set; }

    public string BrthDay { get; set; }

 
   
        public string Vorstellen()
        {
            return $"Hallo, mein Name ist {Vorname} {Name} und bin {Alter} Jahre alt. Ich komme aus {GebOrt}, wo ich am {BrthDay} geboren wurde. ";
         }

        public Person()
        {
            Name = "Apocalypse";
            Vorname = "Otto";
            GebOrt = "Kiel";
            Alter = 59;
            BrthDay = "09.01.2023";
        }
    }
}







