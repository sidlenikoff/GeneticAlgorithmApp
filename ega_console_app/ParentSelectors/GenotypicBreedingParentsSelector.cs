using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public abstract class GenotypicBreedingParentsSelector : BreedingParentsSelector
    {
        public GenotypicBreedingParentsSelector() : base()
        {
        }

        protected sealed override int FindDifference(Encoding parent1, Encoding parent2)
        {
            int countDiff = 0;
            for (int i = 0; i < parent1.Combination.Length; i++)
                if (parent1.Combination[i] != parent2.Combination[i])
                    countDiff++;
            return countDiff;
        }

        protected sealed override Encoding GetFirstParent(in List<Encoding> population)
        {
            return population[rnd.Next(0, population.Count)];
        }
    }
}
