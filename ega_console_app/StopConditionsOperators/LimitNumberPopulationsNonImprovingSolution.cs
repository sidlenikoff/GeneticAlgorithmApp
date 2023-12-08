using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.StopConditionsOperators
{
    public class LimitNumberPopulationsNonImprovingSolution : IStopConditionsOperator
    {
        Encoding currentBest;
        int countNonImprovingPopulations = 0;
        public int Limit { get; set; }

        public LimitNumberPopulationsNonImprovingSolution() { }

        public LimitNumberPopulationsNonImprovingSolution(int limit)
        {
            this.Limit = limit;
            currentBest = new Encoding() { Suitability = 0 };
        }
        public bool NeedStop(IList<Encoding> population)
        {
            bool hasImproved = false;
            foreach(var being in population)
            {
                if(being.Suitability > currentBest.Suitability)
                {
                    hasImproved = true;
                    currentBest = being;
                }
            }

            if (hasImproved)
            {
                countNonImprovingPopulations = 0;
                return false;
            }
            else
                countNonImprovingPopulations++;
            
            return !(countNonImprovingPopulations < Limit);
        }

        public void Reset()
        {
            countNonImprovingPopulations = 0;
            currentBest = new Encoding() { Suitability = 0 };
        }
    }
}
