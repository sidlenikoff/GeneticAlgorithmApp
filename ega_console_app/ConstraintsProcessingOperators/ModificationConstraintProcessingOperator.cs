using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ConstraintsProcessingOperators
{
    public class ModificationConstraintProcessingOperator : IConstraintsProcessingOperator
    {
        Random rnd;

        public ModificationConstraintProcessingOperator()
        {
            rnd = Helpers.Rnd;
        }

        public IList<Encoding> Process(IList<Encoding> population, int maxWeignt)
        {
            var result = new List<Encoding>();
            for(int i = 0; i < population.Count; i++)
            {
                var being = population[i];
                while(Helpers.GetWeight(being.Combination) > maxWeignt)
                {
                    being.Combination[rnd.Next(0, being.Combination.Length)] = 0;
                    being.Suitability = Helpers.GetSuitability(being.Combination);
                }
                result.Add(being);
            }

            return result;
        }
    }
}
