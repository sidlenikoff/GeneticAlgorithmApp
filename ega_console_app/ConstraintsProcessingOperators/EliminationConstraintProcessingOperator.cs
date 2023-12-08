using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ConstraintsProcessingOperators
{
    public class EliminationConstraintProcessingOperator : IConstraintsProcessingOperator
    {
        public IList<Encoding> Process(IList<Encoding> population, int maxWeight)
        {
            var result = new List<Encoding>();
            for(int i = 0; i < population.Count; i++)
            {
                if (Helpers.GetWeight(population[i].Combination) <= maxWeight)
                    result.Add(population[i]);
            }

            return result;
        }
    }
}
