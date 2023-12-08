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
    public class GeneticAlgorithm
    {

        public IList<Item> Items { get; set; }
        public int PopulatioSize { get; set; }
        public int MaxWeight { get; set; }
        public float CrossoverProbabillity { get; set; }
        public float MutationProbabillity { get; set; }

        public IPopulationGenerator PopulationGenerator { get; set; }
        public IParentSelector ParentSelector { get; set; }
        public ISelectionOperator SelectionOperator { get; set; }
        public ICrossoverOperator CrossoverOperator { get; set; }
        public IMutationOperator MutationOperator { get; set; }
        public IReproductiveGroupGenerator ReproductiveGroupGenerator { get; set; }
        public IConstraintsProcessingOperator ConstraintsProcessingOperator { get; set; }

        public IStopConditionsOperator[] StopConditionsOperators { get; set; }

        public ILogger Logger { get; set; }

        public GeneticAlgorithm() { }


        public static GeneticAlgorithmBuilder CreateBuilder()
        {
            return new GeneticAlgorithmBuilder();
        }

        public Encoding PerformAlgorithm()
        {
            if (!IsOperatorsInitialized())
                throw new InvalidOperationException("Genetic algortihm operators isn't initialized");

            foreach (var stop in StopConditionsOperators)
                stop.Reset();
    
            IList<Encoding> population = PopulationGenerator.Generate(PopulatioSize, Items, MaxWeight);
            var best = population[0];
            foreach (var being in population)
                if (best.Suitability < being.Suitability)
                    best = being;
            Logger.MakeLog(0, population, best);

            int steps = 0;
            bool needStop = false;
            while(!needStop)
            {
                ParentSelector.SetPopulation(population);
                var children = new List<Encoding>();
                for (int i = 0; i < population.Count; i++)
                {
                    try
                    {
                        var parents = ParentSelector.Select();
                        if (Helpers.SelectRandomSection(new List<float>() { CrossoverProbabillity, 1 - CrossoverProbabillity }) == 0)
                            children.AddRange(CrossoverOperator.MakeCrossover(parents.Parent1, parents.Parent2));
                    }
                    catch(ArgumentException ex)
                    {
                        if (population.Count == 0)
                            throw ex;
                        ParentSelector.SetPopulation(population);
                        i--;
                    }
                }
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    if (Helpers.SelectRandomSection(new List<float>() { MutationProbabillity, 1 - MutationProbabillity }) == 0)
                        MutationOperator.MakeMutation(ref child);
                }
                var reproductiveGroup = ReproductiveGroupGenerator.Create(population, children);
                var selectedGroup = SelectionOperator.MakeSelection(population, reproductiveGroup, PopulatioSize);
                var newPopulation = ConstraintsProcessingOperator.Process(selectedGroup, MaxWeight);

                Encoding bestBeing = newPopulation[0];
                for (int i = 0; i < newPopulation.Count; i++)
                {
                    var being = newPopulation[i];
                    being.Suitability = Helpers.GetSuitability(being.Combination);
                    if(bestBeing.Suitability < being.Suitability)
                        bestBeing = being;
                }

                Logger.MakeLog(steps + 1, newPopulation, bestBeing);

                population = newPopulation;
                steps++;

                foreach (var op in StopConditionsOperators)
                {
                    if (op.NeedStop(population))
                    {
                        needStop = true;
                        break;
                    }
                }
            }

            var bestSuitability = population.Max(en => en.Suitability);
            best = population.Where(en => en.Suitability == bestSuitability).First();
            Logger.LogSolution(best);

            return best;
        }

        public bool IsOperatorsInitialized()
        {
            return !(PopulationGenerator == null || ParentSelector == null
                || SelectionOperator == null || CrossoverOperator == null || MutationOperator == null
                || ReproductiveGroupGenerator == null || ConstraintsProcessingOperator == null);
        }

        public bool IsReady()
        {
            return IsOperatorsInitialized() && Logger != null && Items != null && MaxWeight != 0;
        }
    }
}
