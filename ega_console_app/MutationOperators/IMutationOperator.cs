using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.MutationOperators
{
    public interface IMutationOperator
    {
        public void MakeMutation(ref Encoding being);
    }
}
