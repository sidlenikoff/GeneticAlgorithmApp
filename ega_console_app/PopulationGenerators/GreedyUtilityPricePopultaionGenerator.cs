using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.PopulationGenerators
{
    public class GreedyUtilityPricePopultaionGenerator : GreedyPopulationGenerator
    {
        public GreedyUtilityPricePopultaionGenerator() : base()
        {
        }

        public override IList<Encoding> Generate(int _populationSize, IList<Item> _items, int _backpackCapacity)
        {
            base.Generate(_populationSize, _items, _backpackCapacity);
            var result = new List<Encoding>();
            for (int i = 0; i < populationSize; i++)
            {
                var newBeing = GenerateBeing(a => (items.IndexOf(a), a.Price));
                result.Add(new Encoding() { Combination = newBeing, 
                    Suitability = Helpers.GetSuitability(newBeing) });
            }

            return result;
        }
    }
}
