using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.CrossoverOperators
{
    public interface ICrossoverOperator
    {
        public List<Encoding> MakeCrossover(Encoding parent1, Encoding parent2);
    }
}
