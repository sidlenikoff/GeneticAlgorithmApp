using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public class RoulleteSelectionOperator : ISelectionOperator
    {
        public IElitistSelection ElitistSelection { get; set; }

        public RoulleteSelectionOperator() { }

        public RoulleteSelectionOperator(IElitistSelection _elitistSelection)
        {
            ElitistSelection = _elitistSelection;
        }

        public virtual IList<Encoding> MakeSelection(IList<Encoding> currentPopulation, IList<Encoding> reproductiveGroup, int outputPopulationSize)
        {
            IList<Encoding> result = new List<Encoding>();
            int size = reproductiveGroup.Count;

            var selectcor = new List<(int index, float value)>(reproductiveGroup.Select(a => (reproductiveGroup.IndexOf(a), (float)a.Suitability)));
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
