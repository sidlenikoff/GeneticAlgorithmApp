using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.Loggers
{
    public interface ILogger
    {
        public void MakeLog(int populationNumber, IList<Encoding> population, Encoding currentBestBeing);
        public void LogSolution(Encoding bestBeing);
    }
}
