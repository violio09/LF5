namespace CL_Vererbung
{
    public class Person
    {
        private string name;
        private string vorname;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; }
        }
        public Person(string name, string vorname)
        {
            Name = name;
            Vorname = vorname;
        }
        public virtual string Vorstellen()
        {
                return $"Hallo, mein Name ist {vorname} {Name}.";
        }  
    }
}




