namespace CL.Lotto___Jan_Fiur
{
    public class Lotto
    {
        private List<int> lottoZahlen = new List<int>();

        public List<int> GetList()
        {
            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                int ziehZahl = random.Next(0,50);
                lottoZahlen.Add(ziehZahl);
            }

            return lottoZahlen;
        }
    }
}