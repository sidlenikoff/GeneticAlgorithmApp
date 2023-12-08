using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void LogSolution(Encoding bestBeing)
        {
            Console.WriteLine("РЕШЕНИЕ");
            Console.WriteLine($"\nЛучшая особь: {bestBeing}");
            Console.WriteLine($"Вес предметов в ранце: {Helpers.GetWeight(bestBeing.Combination)}");
        }

        public void MakeLog(int populationNumber, IList<Encoding> population, Encoding currentBestBeing)
        {
            Console.WriteLine($"\nПопуляция {populationNumber}");
            Console.WriteLine(String.Join('\n', population));
            Console.WriteLine($"\nЛучшая особь: {currentBestBeing}");
            Console.WriteLine($"Вес предметов в ранце: {Helpers.GetWeight(currentBestBeing.Combination)}");
        }
    }
}
