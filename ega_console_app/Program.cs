using ega_console_app.ConstraintsProcessingOperators;
using ega_console_app.CrossoverOperators;
using ega_console_app.Loggers;
using ega_console_app.MutationOperators;
using ega_console_app.ParentSelectors;
using ega_console_app.PopulationGenerators;
using ega_console_app.ReproductiveGroupGenerators;
using ega_console_app.SelectionOperators;
using ega_console_app.StopConditionsOperators;

namespace ega_console_app
{
    internal class Program
    {
        static int backpackCapacity;
        static List<Item> items;

        static void Main(string[] args)
        {
            IPopulationGenerator populationGenerator = new RandomPopulationGenerator();
            IParentSelector parentSelector = new RandomParetntsSelector();
            ICrossoverOperator crossoverOperator = new OnePointCtossoverOperator();
            IMutationOperator mutationOperator = new LocalInversionMutationOperator();
            IReproductiveGroupGenerator reproductiveGroupGenerator = new ParentsPlusChildrenReproductiveGroupGenerator();
            ISelectionOperator selectionOperator = new RankSelectionOperator(new ElitistSelection());
            IConstraintsProcessingOperator constraintsProcessingOperator = new ModificationConstraintProcessingOperator();
            IStopConditionsOperator[] stopConditionsOperators = { new LimitPopulationNumberOperator(20) };

            items = new List<Item>();
            ReadFile("D:\\Projects_Visual_Studio\\LR_1\\LR_8\\a.txt");

            GeneticAlgorithm geneticAlgorithm = GeneticAlgorithm.CreateBuilder().SetItems(items)
                                                                                .SetMaxWeight(backpackCapacity)
                                                                                .SetStopConditions(stopConditionsOperators)
                                                                                .SetPopulationSize(3)
                                                                                .SetMutationProbability(0.01f)
                                                                                .SetCrossoverProbability(0.9f)
                                                                                .SetPopulationGenerator(populationGenerator)
                                                                                .SetParentSelector(parentSelector)
                                                                                .SetCrossoverOperator(crossoverOperator)
                                                                                .SetMutationOperator(mutationOperator)
                                                                                .SetReproductiveGroupGenerator(reproductiveGroupGenerator)
                                                                                .SetSelectionOperator(selectionOperator)
                                                                                .SetConstraintProcessingOperator(constraintsProcessingOperator)
                                                                                .SetLogger(new ConsoleLogger())
                                                                                .Build();
            var result = geneticAlgorithm.PerformAlgorithm();

        }

        static void ReadFile(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            backpackCapacity = int.Parse(reader.ReadLine());
            int N = int.Parse(reader.ReadLine());
            for (int i = 0; i < N; i++)
            {
                var inp = reader.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                items.Add(new Item(i, inp[0], inp[1]));
            }
        }
    }
}