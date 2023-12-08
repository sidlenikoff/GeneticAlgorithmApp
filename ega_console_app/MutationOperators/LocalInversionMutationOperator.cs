using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.MutationOperators
{
    public class LocalInversionMutationOperator : IMutationOperator
    {
        Random rnd;

        public LocalInversionMutationOperator()
        {
            rnd = Helpers.Rnd;
        }

        public void MakeMutation(ref Encoding being)
        {
            var combination = being.Combination;

            int startFrom = rnd.Next(0, combination.Length - 1);
            int genesToMutate = rnd.Next(1, combination.Length - startFrom);
            for(int i = startFrom; i < genesToMutate; i++)
                combination[i] = (combination[i] + 1) % 2;

            being.Combination = combination;
            being.Suitability = Helpers.GetSuitability(combination);
        }
    }
}
