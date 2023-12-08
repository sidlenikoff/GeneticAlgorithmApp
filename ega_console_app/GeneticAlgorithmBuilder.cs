using ega_console_app.ConstraintsProcessingOperators;
using ega_console_app.CrossoverOperators;
using ega_console_app.Loggers;
using ega_console_app.MutationOperators;
using ega_console_app.ParentSelectors;
using ega_console_app.PopulationGenerators;
using ega_console_app.ReproductiveGroupGenerators;
using ega_console_app.SelectionOperators;
using ega_console_app.StopConditionsOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app
{
    public class GeneticAlgorithmBuilder
    {
        private GeneticAlgorithm geneticAlgorithm;
        public GeneticAlgorithmBuilder()
        {
            geneticAlgorithm = new GeneticAlgorithm();
           // rnd = Helpers.Rnd;
        }

        public GeneticAlgorithmBuilder SetItems(IList<Item> _items)
        {
            if (_items == null)
                return this;
            geneticAlgorithm.Items = new List<Item>(_items);
            Helpers.Items = geneticAlgorithm.Items;
            return this;
        }
        public GeneticAlgorithmBuilder SetPopulationSize(int populationSize)
        {
            geneticAlgorithm.PopulatioSize = populationSize;
            return this;
        }

        public GeneticAlgorithmBuilder SetMaxWeight(int maxWeight)
        {
            geneticAlgorithm.MaxWeight = maxWeight;
            return this;
        }
        public GeneticAlgorithmBuilder SetCrossoverProbability(float probabillity)
        {
            geneticAlgorithm.CrossoverProbabillity = probabillity;
            return this;
        }
        public GeneticAlgorithmBuilder SetMutationProbability(float probabillity)
        {
            geneticAlgorithm.MutationProbabillity = probabillity;
            return this;
        }


        public GeneticAlgorithmBuilder SetPopulationGenerator(IPopulationGenerator populationGenerator)
        {
            geneticAlgorithm.PopulationGenerator = populationGenerator;
            return this;
        }
        public GeneticAlgorithmBuilder SetParentSelector(IParentSelector parentSelector)
        {
            geneticAlgorithm.ParentSelector = parentSelector;
            return this;
        }
        public GeneticAlgorithmBuilder SetSelectionOperator(ISelectionOperator selectionOperator)
        {
            geneticAlgorithm.SelectionOperator = selectionOperator;
            return this;
        }
        public GeneticAlgorithmBuilder SetCrossoverOperator(ICrossoverOperator crossoverOperator)
        {
            geneticAlgorithm.CrossoverOperator = crossoverOperator;
            return this;
        }
        public GeneticAlgorithmBuilder SetMutationOperator(IMutationOperator mutationOperator)
        {
            geneticAlgorithm.MutationOperator = mutationOperator;
            return this;
        }
        public GeneticAlgorithmBuilder SetReproductiveGroupGenerator(IReproductiveGroupGenerator reproductiveGroupGenerator)
        {
            geneticAlgorithm.ReproductiveGroupGenerator = reproductiveGroupGenerator;
            return this;
        }
        public GeneticAlgorithmBuilder SetConstraintProcessingOperator(IConstraintsProcessingOperator constraintsProcessingOperator)
        {
            geneticAlgorithm.ConstraintsProcessingOperator = constraintsProcessingOperator;
            return this;
        }

        public GeneticAlgorithmBuilder SetStopConditions(IStopConditionsOperator[] stopConditionsOperators)
        {
            geneticAlgorithm.StopConditionsOperators = stopConditionsOperators;
            return this;
        }

        public GeneticAlgorithmBuilder SetLogger(ILogger logger)
        {
            geneticAlgorithm.Logger = logger;
            return this;
        }

        public bool IsOperatorsInitialized() => geneticAlgorithm.IsOperatorsInitialized();

        public GeneticAlgorithm Build()
        {
            return geneticAlgorithm;
        }
    }
}
