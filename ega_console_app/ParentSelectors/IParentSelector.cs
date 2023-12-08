using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public interface IParentSelector
    {
        public ParentsPair Select();
        public void SetPopulation(IList<Encoding> _population);
    }
}
