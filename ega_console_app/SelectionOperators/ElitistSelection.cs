using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public class ElitistSelection : IElitistSelection
    { 
        public void AddBestBeing(IList<Encoding> population, ref IList<Encoding> newPopulation)
        {
            var bestBeing = population[0];
            foreach (var being in population)
                if (being.Suitability > bestBeing.Suitability)
                    bestBeing = being;
            
            if (!newPopulation.Contains(bestBeing))
                newPopulation.Add(bestBeing);
        }
    }
}
