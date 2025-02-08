using System.Text;

namespace PlacasAPI.Utils
{
    public class GetRandomPlate
    {
        public List<string> GenerateRandomPlates(int amount)
        {
            List<string> platesList = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                platesList.Add(PlateGerator());
            }
            return platesList;
        }

        private string PlateGerator()
        {
            StringBuilder plate = new StringBuilder(7);
            Random rnd = new();
            plate.Append(ThreeLetterGerator(rnd));
            plate.Append(NumberGenerator(rnd).ToString());
            plate.Append(NumberOrLetterGenerator(rnd));
            plate.Append(NumberGenerator(rnd).ToString());
            plate.Append(NumberGenerator(rnd).ToString());
            return plate.ToString();
        }

        private string NumberOrLetterGenerator(Random rnd)
        {
            char letra = (char)rnd.Next(65, 90);
            return rnd.Next(0, 2) == 0 ? letra.ToString() : NumberGenerator(rnd).ToString();
        }

        private char NumberGenerator(Random rnd)
        {
            return (char)rnd.Next(48, 57);
        }

        private string ThreeLetterGerator(Random rnd)
        {
            StringBuilder letras = new StringBuilder(3);
            letras.Append((char)rnd.Next(65, 90));
            letras.Append((char)rnd.Next(65, 90));
            letras.Append((char)rnd.Next(65, 90));
            return letras.ToString();
        }
    }
}
}
