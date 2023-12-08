using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.MutationOperators
{
    public class FullInversionMutationOperator : IMutationOperator
    {
        public void MakeMutation(ref Encoding being)
        {
            for (int i = 0; i < being.Combination.Length; i++)
                being.Combination[i] = (being.Combination[i] + 1) % 2;

            being.Suitability = Helpers.GetSuitability(being.Combination);
        }
    }
}
