using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.PopulationGenerators
{
    public abstract class GreedyPopulationGenerator : IPopulationGenerator
    {
        protected int populationSize;
        int backpackCapacity;

        protected List<Item> items;

        public GreedyPopulationGenerator()
        {

        }

        public virtual IList<Encoding> Generate(int populationSize, IList<Item> items, int backpackCapacity)
        {
            this.populationSize = populationSize;
            this.backpackCapacity = backpackCapacity;
            this.items = new List<Item>(items);
            return null;
        }

        protected int[] GenerateBeing(Func<Item, (int index, float value)> func)
        {
            var selectcor = new List<(int index, float value)>(items.Select(func));
            int[] being = new int[items.Count];
            int sumWeight = 0;
            int sumPrice = 0;
            while (sumWeight < backpackCapacity && selectcor.Count > 0)
            {
                var selectedSectionIndex = Helpers.SelectRandomSection(selectcor.Select(a => a.value).ToList());
                var bestElement = items[selectcor[selectedSectionIndex].index];
                bool needBreak = false;
                if (sumWeight + bestElement.Weight <= backpackCapacity)
                {
                    sumWeight += bestElement.Weight;
                    sumPrice += bestElement.Price;
                    being[selectcor[selectedSectionIndex].index] = 1;
                }
                else
                    needBreak = true;
                selectcor.RemoveAt(selectedSectionIndex);
                if (needBreak)
                    break;
            }

            return being;
        }

        
    }
}
