using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public interface IElitistSelection
    {
        public void AddBestBeing(IList<Encoding> population, ref IList<Encoding> newPopulation);
    }
}
