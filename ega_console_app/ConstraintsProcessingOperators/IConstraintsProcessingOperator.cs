using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ConstraintsProcessingOperators
{
    public interface IConstraintsProcessingOperator
    {
        public IList<Encoding> Process(IList<Encoding> population, int maxWeignt);
    }
}
