using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.StopConditionsOperators
{
    public interface IStopConditionsOperator
    {
        public int Limit { get; set; }
        public bool NeedStop(IList<Encoding> population);

        public void Reset();
    }
}
