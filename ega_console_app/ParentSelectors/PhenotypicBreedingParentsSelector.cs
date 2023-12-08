using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public abstract class PhenotypicBreedingParentsSelector : BreedingParentsSelector
    {
        public PhenotypicBreedingParentsSelector() : base()
        {
        }

        protected sealed override int FindDifference(Encoding parent1, Encoding parent2)
        {
            return Math.Abs(parent1.Suitability - parent2.Suitability);
        }

        protected sealed override Encoding GetFirstParent(in List<Encoding> population)
        {
            var temp = population.OrderByDescending(a => a.Suitability);
            return population.First();
        }
    }
}
