using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public interface ISelectionOperator
    {
        public IElitistSelection ElitistSelection { get; set; }
        public IList<Encoding> MakeSelection(IList<Encoding> currentPopulation,IList<Encoding> reproductiveGroup, int outputPopulationSize);
    }
}
