using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.MutationOperators
{
    public class GenPointMutationOperator : IMutationOperator
    {
        Random rnd;
         
        public GenPointMutationOperator()
        {
            rnd = Helpers.Rnd;
        }

        public void MakeMutation(ref Encoding being)
        {
            var combination = being.Combination;
            int mutationIndex = rnd.Next(0, combination.Length);
            combination[mutationIndex] = (combination[mutationIndex] + 1) % 2;
            being.Combination = combination;
            being.Suitability = Helpers.GetSuitability(combination);
        }
    }
}
