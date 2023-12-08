using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ega_console_app;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ega_console_app.StopConditionsOperators;
using ega_console_app.ConstraintsProcessingOperators;
using ega_console_app.CrossoverOperators;
using ega_console_app.MutationOperators;
using ega_console_app.ParentSelectors;
using ega_console_app.PopulationGenerators;
using ega_console_app.ReproductiveGroupGenerators;
using ega_console_app.SelectionOperators;

namespace GeneticAlgorithm_App
{
    public class AlgorithmParameters
    {
        public GeneticAlgorithmBuilder Builder { get; set; }

        public OperatorWithType<StopConditionItem>[] StopConditions { get; set; }
        public OperatorWithType<string> ConstrintProcessor { get; set; }
        public OperatorWithType<string> CrossoverOperator { get; set; }
        public int PointsInCrossover { get; set; }
        public OperatorWithType<string> MutationOperator { get; set; }
        public OperatorWithType<string> ParentSelector { get; set; }
        public OperatorWithType<string> PopulationGenerator { get; set; }
        public OperatorWithType<string> ReproductiveGroupGenerator { get; set; }
        public OperatorWithType<string> SelectionOperator { get; set; }
        public bool IsChosenSelectionElitist { get; set; }

        public int PopulationSize { get; set; }
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
    }

    public class StopConditionItem
    {
        public string Name { get; set; }
        public bool IsPeeked { get; set; }
        public int Limit { get; set; }

        public StopConditionItem(string name)
        {
            Name = name;
            IsPeeked = false;
            Limit = 0;
        }
    }

    public class OperatorWithType<T>
    {
        public T Operator { get; set; }
        public Type Type { get; set; }

        public OperatorWithType(T _operator, Type type)
        {
            Operator = _operator;
            Type = type;
        }

        public static implicit operator T(OperatorWithType<T> d) => d.Operator;

        public override string ToString()
        {
            return Operator.ToString();
        }
    }

    public partial class SettingsViewModel : ObservableObject
    {

        public GeneticAlgorithmBuilder Builder { get; set; }

        [ObservableProperty]
        AlgorithmParameters algorithmParameters;

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<StopConditionItem>> stopConditions = new ObservableCollection<OperatorWithType<StopConditionItem>>
            {
            new OperatorWithType<StopConditionItem>(new StopConditionItem("Ограничить максимальное число популяций"),
                typeof(LimitPopulationNumberOperator)),
            new OperatorWithType<StopConditionItem>(new StopConditionItem("Ограничить число популяций без улучшения максимальной приспособленности"),
                typeof(LimitNumberPopulationsNonImprovingSolution)),
            new OperatorWithType<StopConditionItem>(new StopConditionItem("Ограничить число популяций без улучшения средней приспособленности"),
                typeof(LimitNumberPopulationsNonImprovingAvg)),
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> constringProcessingOperators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType<string>("Элиминицая", typeof(EliminationConstraintProcessingOperator)),
                new OperatorWithType <string>("Модификация (случайная)", typeof(ModificationConstraintProcessingOperator))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> crossoverOperators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType < string >("Одноточечный", typeof(OnePointCtossoverOperator)),
                new OperatorWithType < string >("Многоточечный", typeof(MultiPointCrossoverOperator)),
                new OperatorWithType < string >("Однородный", typeof(SmoothCrossoverOperator))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> mutationOperators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType < string >("Дополнение", typeof(FullInversionMutationOperator)),
                new OperatorWithType < string >("Инверсия участка", typeof(LocalInversionMutationOperator)),
                new OperatorWithType < string >("Точечная", typeof(GenPointMutationOperator))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> parentsSelectors = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType < string >("Случайно", typeof(RandomParetntsSelector)),
                new OperatorWithType < string >("Инбридинг по генотипу", typeof(GenotypicInbreedingParentsSelector)),
                new OperatorWithType < string >("Аутбрилинг по генотипу", typeof(GenotypicOutbreedingParentsSelector)),
                new OperatorWithType < string >("Инбридинг по фенотипу", typeof(PhenotypicInbreedingParentsSelector)),
                new OperatorWithType < string >("Аутбрилинг по фенотипу", typeof(PhenotypicOutbreedingParentsSelector))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> populationGenerators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType < string >("Случайно", typeof(RandomPopulationGenerator)),
                new OperatorWithType < string >("Случайно с контролем", typeof(RandomWithControlPopulationGenerator)),
                new OperatorWithType < string >("Жадный алгоритм по стоимости", typeof(GreedyPricePopulationGenerator)),
                new OperatorWithType < string >("Жадный алгоритм по удельной стоимости", typeof(GreedyUtilityPricePopultaionGenerator))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> reproductiveGroupGenerators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType<string>("Предки + (потомки + мутанты)", typeof(ParentsPlusChildrenReproductiveGroupGenerator)),
                new OperatorWithType<string>("Потомки + мутанты", typeof(ChildrenReproductiveGroupGenerator))
            };

        [ObservableProperty]
        static ObservableCollection<OperatorWithType<string>> selectionOperators = new ObservableCollection<OperatorWithType<string>>
            {
                new OperatorWithType<string>("Ранговый", typeof(RankSelectionOperator)),
                new OperatorWithType<string>("Пропорциональный", typeof(RoulleteSelectionOperator)),
                new OperatorWithType<string>("Турнирный", typeof(TournamentSelectionOperator))
            };

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CrossoverPointsVisibility))]
        OperatorWithType<string> chosenCrossoverOperator;


        Visibility crossoverPointsVisibility;
        public Visibility CrossoverPointsVisibility
        {
            get => crossoverPointsVisibility;
            set
            {
                AlgorithmParameters.CrossoverOperator = ChosenCrossoverOperator;
                if (ChosenCrossoverOperator != null && ChosenCrossoverOperator.Type == typeof(MultiPointCrossoverOperator))
                    SetProperty<Visibility>(ref crossoverPointsVisibility, Visibility.Visible);
                else
                    SetProperty<Visibility>(ref crossoverPointsVisibility, Visibility.Collapsed);
            }
        }


        public event Action CloseWindowAction;
        public event Action<Exception> ErrorOccuredAction;

        public SettingsViewModel(AlgorithmParameters parameters = null)
        {
            Builder = GeneticAlgorithm.CreateBuilder();
            AlgorithmParameters = new AlgorithmParameters();
            AlgorithmParameters.PointsInCrossover = 2;
            CrossoverPointsVisibility = Visibility.Collapsed;
            PropertyChanged += SettingsViewModel_PropertyChanged;
            if (parameters != null)
            {
                AlgorithmParameters.PopulationSize = parameters.PopulationSize;
                AlgorithmParameters.CrossoverProbability = parameters.CrossoverProbability;
                AlgorithmParameters.MutationProbability = parameters.MutationProbability;

                AlgorithmParameters.PopulationGenerator = parameters.PopulationGenerator;

                AlgorithmParameters.ParentSelector = parameters.ParentSelector;
                ChosenCrossoverOperator = parameters.CrossoverOperator;
                if(parameters.PointsInCrossover > 0)
                    AlgorithmParameters.PointsInCrossover = parameters.PointsInCrossover;
                AlgorithmParameters.CrossoverOperator = parameters.CrossoverOperator;
                AlgorithmParameters.ConstrintProcessor = parameters.ConstrintProcessor;

                AlgorithmParameters.MutationOperator = parameters.MutationOperator;

                AlgorithmParameters.SelectionOperator = parameters.SelectionOperator;
                AlgorithmParameters.IsChosenSelectionElitist = parameters.IsChosenSelectionElitist;

                AlgorithmParameters.ReproductiveGroupGenerator = parameters.ReproductiveGroupGenerator;
            }
        }

        private void SettingsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChosenCrossoverOperator))
            {
                AlgorithmParameters.CrossoverOperator = ChosenCrossoverOperator;
                if (ChosenCrossoverOperator != null && ChosenCrossoverOperator.Type == typeof(MultiPointCrossoverOperator))
                    CrossoverPointsVisibility = Visibility.Visible;
                else
                    CrossoverPointsVisibility = Visibility.Collapsed;
            }
        }

        [RelayCommand]
        private void Save()
        {
            try
            {
                AlgorithmParameters.Builder = Builder;
                AlgorithmParameters.StopConditions = StopConditions.Where(a => a.Operator.IsPeeked).ToArray();
                var stopList = new List<IStopConditionsOperator>();
                foreach (var cond in AlgorithmParameters.StopConditions)
                {
                    var inst = (IStopConditionsOperator)Activator.CreateInstance(cond.Type);
                    inst.Limit = cond.Operator.Limit;
                    stopList.Add(inst);
                }

                var populationGenerator = (IPopulationGenerator)Activator.CreateInstance(AlgorithmParameters.PopulationGenerator.Type);

                var parentSelector = (IParentSelector)Activator.CreateInstance(AlgorithmParameters.ParentSelector.Type);

                var crossoverOperator = (ICrossoverOperator)Activator.CreateInstance(AlgorithmParameters.CrossoverOperator.Type);
                if (crossoverOperator.GetType() == typeof(MultiPointCrossoverOperator))
                    (crossoverOperator as MultiPointCrossoverOperator).NumberOfPoints = AlgorithmParameters.PointsInCrossover;

                var mutationOperator = (IMutationOperator)Activator.CreateInstance(AlgorithmParameters.MutationOperator.Type);

                var reproductiveGroupGenerator = (IReproductiveGroupGenerator)Activator.CreateInstance(AlgorithmParameters.ReproductiveGroupGenerator.Type);

                var selectionOperator = (ISelectionOperator)Activator.CreateInstance(AlgorithmParameters.SelectionOperator.Type);
                if (AlgorithmParameters.IsChosenSelectionElitist)
                    selectionOperator.ElitistSelection = new ElitistSelection();
                else
                    selectionOperator.ElitistSelection = new NonElitistSelection();

                var constraintsProcessingOperator = (IConstraintsProcessingOperator)Activator.CreateInstance(AlgorithmParameters.ConstrintProcessor.Type);

                Builder = Builder.SetPopulationSize(AlgorithmParameters.PopulationSize)
                    .SetCrossoverProbability(AlgorithmParameters.CrossoverProbability)
                    .SetMutationProbability(AlgorithmParameters.MutationProbability)
                    .SetPopulationGenerator(populationGenerator)
                    .SetParentSelector(parentSelector)
                    .SetCrossoverOperator(crossoverOperator)
                    .SetMutationOperator(mutationOperator)
                    .SetReproductiveGroupGenerator(reproductiveGroupGenerator)
                    .SetSelectionOperator(selectionOperator)
                    .SetConstraintProcessingOperator(constraintsProcessingOperator)
                    .SetStopConditions(stopList.ToArray());

                if (Builder.IsOperatorsInitialized())
                    CloseWindowAction?.Invoke();
            }
            catch(Exception ex)
            {
                ErrorOccuredAction?.Invoke(ex);
            }
        }
    }
}
