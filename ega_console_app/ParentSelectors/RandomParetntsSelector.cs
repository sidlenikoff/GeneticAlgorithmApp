using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public class RandomParetntsSelector : IParentSelector
    {
        List<Encoding> population;
        Random rnd;

        public RandomParetntsSelector() 
        {
            rnd = Helpers.Rnd;
            population = new List<Encoding>();
        }

        public ParentsPair Select()
        {
            if(population.Count == 0)
                throw new ArgumentException("Population count is 0");
            List<int> range = Enumerable.Range(0, population.Count).ToList();
            int index1 = range[rnd.Next(range.Count)];
            range.Remove(index1);
            int index2 = range[rnd.Next(range.Count)];

            return new ParentsPair() { Parent1 = population[index1], Parent2 = population[index2] };
        }

        public void SetPopulation(IList<Encoding> _population)
        {
            population = new List<Encoding>(_population);
        }
    }
}
