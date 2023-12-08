using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.CrossoverOperators
{
    public class SmoothCrossoverOperator : ICrossoverOperator
    {
        Random rnd;

        public SmoothCrossoverOperator()
        {
            rnd = Helpers.Rnd;
        }

        public List<Encoding> MakeCrossover(Encoding parent1, Encoding parent2)
        {
            int length = parent1.Combination.Length;
            int k = 2;

            var result = new List<Encoding>();

            for (int j = 0; j < k; j++)
            {
                Encoding newBeing = new Encoding();
                var combination = new int[length];
                for (int i = 0; i < length; i++)
                {
                    if (rnd.Next(11) % 2 == 0)
                        combination[i] = parent1.Combination[i];
                    else
                        combination[i] = parent2.Combination[i];
                }
                newBeing.Combination = combination;
                newBeing.Suitability = Helpers.GetSuitability(combination);
                result.Add(newBeing);
            }

            return result;
        }
    }
}
