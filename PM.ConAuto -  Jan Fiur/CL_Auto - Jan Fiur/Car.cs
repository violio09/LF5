using System.Security.AccessControl;
using System.Transactions;

namespace CL_Auto
{
    public class Car
    {
        private string manufacter { get; }

        private string model { get; }   

        private double maxFuelLevel { get; }

        private double fuelComsumption { get;}

        private double currentFuelLevel { get; set; }


        public Car()
        {
            manufacter = "Audi";
            model = "RS Q8";
            maxFuelLevel = 85;
            fuelComsumption = 17.5;
            currentFuelLevel = 45;
        }

      

        public string AutoShowRoom()
        {
            string verstellen = $"Das ist der {manufacter} {model}. Der Tank hat ein Volumen von {maxFuelLevel} Litern und verbraucht {fuelComsumption} L / 100km \n";
            return verstellen;
        }

        public string Refuel(double auftanken)
        {
            if(auftanken <= 0)
            {
                throw new ArgumentException("Sie können nicht Minus Liter tanken");
            }

            if (currentFuelLevel == maxFuelLevel)
            {
                return $"Die aktuelle Tankladung liegt bei {currentFuelLevel} Litern und ist voll. \n";
            }

            if(currentFuelLevel + auftanken == maxFuelLevel)
            {
                return $"Sie wollen {auftanken} Liter tanken und haben damit voll eingetankt. Kein Tropfen wurde verschwendet";
            }

            if(currentFuelLevel + auftanken > maxFuelLevel)
            {
                double zwischenRech = currentFuelLevel + auftanken;

                double overflow = zwischenRech - maxFuelLevel;

                double overTank = auftanken - overflow;

                currentFuelLevel = maxFuelLevel;

                return $"Die Tanken viel zu viel.Es wurde nur {overTank} Liter getankt. Sie haben dabei {overflow} Liter zu viel getankt"; 
            }

            if(currentFuelLevel + auftanken < maxFuelLevel)
            {
                double zwischenRech = currentFuelLevel + auftanken;

                double underflow = maxFuelLevel - zwischenRech;

                currentFuelLevel = zwischenRech;

                return $"Sie haben nicht komplett voll getankt. Sie haben noch {underflow} Liter im Tank frei. ";
            }

            else
            {
                return "Sie tanken nicht, schöne Weiterfahrt";
            }
        }

        public string drive(double strecke) // Die Methode, wo das voreingestellte Auto fahren soll 
        {

            //Rechnung des verbrauchten Tankes

            double fuelWhileDrive = 0;

            strecke = strecke / 100;

            fuelWhileDrive = fuelComsumption * strecke;


            //Die Strecke kann gefahren werden / Gib die verbrauchte Tankmenge an
            if(fuelWhileDrive == currentFuelLevel || fuelWhileDrive < currentFuelLevel)
            {

                return $"Perfekt, sie kommen an das Ziel mit dem aktuellen Tank an. Bei der Fahrt haben sie {fuelWhileDrive} Liter verbraucht \n";
            
            }
            //Die Strecke kann nicht gefahren werden / Gib die extra dazu benötigte Tankmenge an
            else
            {
                double needToRefuel = 0;

                needToRefuel = fuelWhileDrive - currentFuelLevel;
                return $"Sie kommen leider nicht an das Ziel an. Sie müssten {needToRefuel} Liter nachtanken \n";   }
        }
    }

}