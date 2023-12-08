using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.SelectionOperators
{
    public class TournamentSelectionOperator : ISelectionOperator
    {
        Random rnd;
        public IElitistSelection ElitistSelection { get; set; }

        public TournamentSelectionOperator() { rnd = Helpers.Rnd; }

        public TournamentSelectionOperator(IElitistSelection _eletistSelection) 
        {
            rnd = Helpers.Rnd;
            ElitistSelection = _eletistSelection;
        }

        public virtual IList<Encoding> MakeSelection(IList<Encoding> currentPopulation, IList<Encoding> reproductiveGroup, int outputPopulationSize)
        {
            IList<Encoding> result = new List<Encoding>();
            int size = reproductiveGroup.Count;

            var tempPopulation = new List<Encoding>(reproductiveGroup);

            for(int i = 0; i < outputPopulationSize; i++)
            {
                var being1 = reproductiveGroup[rnd.Next(0, tempPopulation.Count)];
                var being2 = reproductiveGroup[rnd.Next(0, tempPopulation.Count)];

                var toAdd = being1.Suitability >= being2.Suitability ? being1 : being2;
                result.Add(toAdd);
            }

            ElitistSelection.AddBestBeing(currentPopulation, ref result);

            return result;
        }
    }
}
