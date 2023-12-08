using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.StopConditionsOperators
{
    public class LimitPopulationNumberOperator : IStopConditionsOperator
    {
        public int Limit { get; set; }
        int countPopulationNumber = 0;

        public LimitPopulationNumberOperator() { }

        public LimitPopulationNumberOperator(int limit) 
        {
            this.Limit = limit;
        }
        public bool NeedStop(IList<Encoding> population)
        {
            countPopulationNumber++;
            return countPopulationNumber >= Limit;
        }

        public void Reset()
        {
            countPopulationNumber = 0;
        }
    }
}
