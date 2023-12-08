using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public class RankSelectionOperator : ISelectionOperator
    {
        public IElitistSelection ElitistSelection { get; set; }

        public RankSelectionOperator() { }

        public RankSelectionOperator(IElitistSelection _elitistSelection)
        {
            ElitistSelection = _elitistSelection;
        }

        public virtual IList<Encoding> MakeSelection(IList<Encoding> currentPopulation, IList<Encoding> reproductiveGroup, int outputPopulationSize)
        {
            IList<Encoding> result = new List<Encoding>();
            int size = reproductiveGroup.Count;

            var sortedPopulation = reproductiveGroup.OrderByDescending(a => a.Suitability).ToList();
            
            float a, b;
            a = 1.3f;
            b = 2 - a;

            var selectcor = new List<(int index, float value)>();
            for(int i = 0; i < sortedPopulation.Count; i++)
            {
                float value = (1f / (float)size) * (a - (a - b) * (i) / (size - 1));
                selectcor.Add((i, value));
            }

            for (int i = 0; i < outputPopulationSize; i++)
            {
                var selectedSectionIndex = Helpers.SelectRandomSection(selectcor.Select(a => a.value).ToList());
                var bestElement = reproductiveGroup[selectcor[selectedSectionIndex].index];
                result.Add(bestElement);
            }

            ElitistSelection.AddBestBeing(currentPopulation, ref result);

            return result;
        }
    }
}
