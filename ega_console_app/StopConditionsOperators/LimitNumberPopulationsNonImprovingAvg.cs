using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.StopConditionsOperators
{
    public class LimitNumberPopulationsNonImprovingAvg : IStopConditionsOperator
    {
        public int Limit { get; set; }
        float currentAvg = 0.0f;
        int countNonImprovingPopulations = 0;

        public LimitNumberPopulationsNonImprovingAvg() { }

        public LimitNumberPopulationsNonImprovingAvg(int limit)
        {
            this.Limit = limit;
        }
        public bool NeedStop(IList<Encoding> population)
        {
            float avg = population.Sum(a => a.Suitability) / (float)population.Count;
            if (avg > currentAvg)
            {
                countNonImprovingPopulations = 0;
                currentAvg = avg;
                return false;
            }
            else
                countNonImprovingPopulations++;
            
            return !(countNonImprovingPopulations < Limit);
        }

        public void Reset()
        {
            currentAvg = 0.0f;
            countNonImprovingPopulations = 0;
        }
    }
}
