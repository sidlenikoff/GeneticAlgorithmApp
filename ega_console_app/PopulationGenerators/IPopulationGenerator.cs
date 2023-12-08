using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.PopulationGenerators
{
    public interface IPopulationGenerator
    {
        public IList<Encoding> Generate(int _populationSize, IList<Item> _items, int _backpackCapacity);
    }
}
