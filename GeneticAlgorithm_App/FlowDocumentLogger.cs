using ega_console_app;
using ega_console_app.Loggers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GeneticAlgorithm_App
{
    public class FlowDocumentLogger : ILogger
    {
        FlowDocument flowDocument;

        public FlowDocumentLogger(FlowDocument flowDocument)
        {
            this.flowDocument = flowDocument;
        }

        public void LogSolution(ega_console_app.Encoding bestBeing)
        {
            flowDocument.Blocks.Add(new Paragraph(new Run($" ")));
            
            var paragraph = new Paragraph(new Bold(new Run($"РЕШЕНИЕ")));
            paragraph.Inlines.Add(new Run($"\nЛучшая особь: {bestBeing}"));
            //paragraph.Inlines.Add(new Run($"\nВес предметов в ранце: {Helpers.GetWeight(bestBeing.Combination)}"));


            flowDocument.Blocks.Add(paragraph);
            
        }

        public void MakeLog(int populationNumber, IList<ega_console_app.Encoding> population, ega_console_app.Encoding currentBestBeing)
        {
            flowDocument.Blocks.Add(new Paragraph(new Bold(new Run($"Популяция {populationNumber}"))));
            var itemList = new List();
            foreach (var being in population)
                itemList.ListItems.Add(new ListItem(new Paragraph(new Run(being.ToString()))));
            flowDocument.Blocks.Add(itemList);

            flowDocument.Blocks.Add(new Paragraph(new Run($"Лучшая особь: {currentBestBeing}")));
            //flowDocument.Blocks.Add(new Paragraph(new Run($"Вес предметов в ранце: {Helpers.GetWeight(currentBestBeing.Combination)}")));
        }
    }
}
