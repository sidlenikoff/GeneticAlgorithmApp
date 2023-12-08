using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app
{
    public class Encoding
    {
        public int[] Combination;
        public int Suitability;

        public override string ToString()
        {
            return $"[ {String.Join(' ', Combination)} ] : [ {Suitability} ]";
        }

        public override int GetHashCode()
        {
            return String.Join(' ',Combination).GetHashCode();
        }
    }
}
