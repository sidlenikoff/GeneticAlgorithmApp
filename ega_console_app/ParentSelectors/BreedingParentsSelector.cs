using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public abstract class BreedingParentsSelector : IParentSelector
    {
        List<Encoding> population;
        protected Random rnd;

        public BreedingParentsSelector()
        {
            rnd = Helpers.Rnd;
            population = new List<Encoding>();
        }

        public void SetPopulation(IList<Encoding> _population)
        {
            population = new List<Encoding>(_population);
        }

        public ParentsPair Select()
        {
            if (population.Count == 0)
                throw new ArgumentException("Population count is 0");

            var parent1 = GetFirstParent(in population);
            Encoding parent2 = parent1;
            int currentBestDiff = 0;
            foreach(var pair in population)
            {
                int countDiff = FindDifference(parent1, parent2);
                GetBestPair(countDiff, pair, ref currentBestDiff, ref parent2);
            }
            population.Remove(parent1);

            if (parent1.Combination.Except(parent2.Combination).Count() != 0)
                parent2 = population[rnd.Next(0, population.Count)];

            population.Remove(parent2);

            return new ParentsPair() { Parent1 = parent1, Parent2 = parent2 };
        }

        protected abstract Encoding GetFirstParent(in List<Encoding> population);
        protected abstract int FindDifference(Encoding parent1, Encoding parent2);
        protected abstract void GetBestPair(int countDiff, Encoding probablePair, ref int currentBestDiff, ref Encoding currentBest);
    }
}
