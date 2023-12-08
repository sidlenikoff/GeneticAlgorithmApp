using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.PopulationGenerators
{
    public class RandomPopulationGenerator : IPopulationGenerator
    {
        public RandomPopulationGenerator()
        {
        }

        public IList<Encoding> Generate(int populationSize, IList<Item> items, int backpackCapacity)
        {
            var result = new List<Encoding>();
            for (int i = 0; i < populationSize; i++)
            {
                var newBeing = new int[items.Count];
                for (int j = 0; j < items.Count; j++)
                    newBeing[j] = Helpers.Rnd.Next(11) % 2;
                result.Add(new Encoding() { Combination = newBeing, 
                    Suitability = Helpers.GetSuitability(newBeing)});
            }

            return result;
        }
    }
}
