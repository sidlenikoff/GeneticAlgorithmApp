using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.CrossoverOperators
{
    public class MultiPointCrossoverOperator : ICrossoverOperator
    {
        public int NumberOfPoints { get; set; }

        public MultiPointCrossoverOperator() { }

        public MultiPointCrossoverOperator(int _numberOfPoints) 
        {
            NumberOfPoints = _numberOfPoints;
        }

        public List<Encoding> MakeCrossover(Encoding parent1, Encoding parent2)
        {
            int length = parent1.Combination.Length;

            var points = Enumerable.Range(1, length - 1).OrderBy(a => Guid.NewGuid()).Take(NumberOfPoints).ToList();

            var combination1 = new int[length];
            var combination2 = new int[length];

            bool isFromFirstParent = true;

            points.Add(length);

            int prevPoint = 0;
            foreach(var point in points)
            {
                for(int i = prevPoint; i < point; i++)
                {
                    if (isFromFirstParent)
                    {
                        combination1[i] = parent1.Combination[i];
                        combination2[i] = parent2.Combination[i];
                    }
                    else
                    {
                        combination1[i] = parent2.Combination[i];
                        combination2[i] = parent1.Combination[i];
                    }
                }
                prevPoint = point;
                isFromFirstParent = !isFromFirstParent;
            }

            var result = new List<Encoding>
            {
                new Encoding() { Combination = combination1, Suitability = Helpers.GetSuitability(combination1) },
                new Encoding() { Combination = combination2, Suitability = Helpers.GetSuitability(combination2) }
            };

            return result;
        }
    }
}
