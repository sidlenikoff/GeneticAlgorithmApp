using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ega_console_app;
using ega_console_app.CrossoverOperators;
using ega_console_app.Loggers;
using ega_console_app.MutationOperators;
using ega_console_app.ParentSelectors;
using ega_console_app.PopulationGenerators;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace GeneticAlgorithm_App
{
    public partial class MainViewModel : ObservableObject
    {
        private GeneticAlgorithmBuilder geneticAlgorithmBuilder;
        private GeneticAlgorithm geneticAlgorithm;

        [ObservableProperty]
        ObservableCollection<Item> items;

        [ObservableProperty]
        FlowDocument loggerDocument;

        [ObservableProperty]
        int backpackCapacity;

        [ObservableProperty]
        ObservableCollection<Item> itemsInResult;

        [ObservableProperty]
        int resultBackpackWeight;

        [ObservableProperty]
        int resultBackpackPrice;

        [ObservableProperty]
        AlgorithmParameters algorithmParameters;

        [ObservableProperty]
        Visibility elitistSelectionVisibility;

        [ObservableProperty]
        Visibility crossoverPointsNumberVisibility;


        ILogger logger;

        public event Action<Exception> ErrorOccuredAction;

        public MainViewModel()
        {
            LoggerDocument = new FlowDocument();
            logger = new FlowDocumentLogger(LoggerDocument);
            AlgorithmParameters = new();
            ElitistSelectionVisibility = Visibility.Collapsed;
            CrossoverPointsNumberVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        private void OpenFile()
        {
            var opf = new OpenFileDialog();
            opf.Filter = "txt files(*.txt) | *.txt";

            if (opf.ShowDialog() == true)
            {
                try
                {
                    string filePath = opf.FileName;
                    StreamReader reader = new StreamReader(filePath);
                    BackpackCapacity = int.Parse(reader.ReadLine());
                    Items = new ObservableCollection<Item>();
                    int N = int.Parse(reader.ReadLine());
                    for (int i = 0; i < N; i++)
                    {
                        var inp = reader.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                        Items.Add(new Item(inp[0], inp[1], inp[2]));
                    }
                }
                catch (Exception ex)
                {
                    ErrorOccuredAction?.Invoke(ex);
                }
            }
        }

        [RelayCommand]
        private void OpenSettings()
        {
            var settings = new SettingsWindow(AlgorithmParameters);
            if (settings.ShowDialog() == true)
            {
                AlgorithmParameters = settings.AlgorithmParameters;
                AlgorithmParameters.StopConditions = settings.AlgorithmParameters.StopConditions.Where(a => a.Operator.IsPeeked).ToArray();
                ElitistSelectionVisibility = AlgorithmParameters.IsChosenSelectionElitist ? Visibility.Visible : Visibility.Collapsed;
                CrossoverPointsNumberVisibility = AlgorithmParameters.CrossoverOperator.Type == typeof(MultiPointCrossoverOperator) ?
                                                Visibility.Visible : Visibility.Collapsed;
                geneticAlgorithmBuilder = settings.AlgorithmParameters.Builder;
            }
        }

        [RelayCommand]
        private void RunAlgorithm()
        {
            try
            {
                geneticAlgorithm = geneticAlgorithmBuilder.SetItems(Items)
                                                           .SetMaxWeight(BackpackCapacity)
                                                           .SetLogger(logger)
                                                           .Build();


                var result = geneticAlgorithm.PerformAlgorithm();
                ItemsInResult = new ObservableCollection<Item>(ega_console_app.Helpers.GetItemsInBackpack(result.Combination));
                ResultBackpackWeight = ega_console_app.Helpers.GetWeight(result.Combination);
                ResultBackpackPrice = result.Suitability;
            }
            catch(Exception ex)
            {
                ErrorOccuredAction?.Invoke(ex);
            }
        }

        [RelayCommand]
        private void SaveLogFile()
        {
            try
            {
                TextRange range;
                System.IO.FileStream fStream;
                range = new TextRange(LoggerDocument.ContentStart, LoggerDocument.ContentEnd);
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "txt file(*.txt) | *.txt";
                if (saveFile.ShowDialog() == true)
                {
                    fStream = new System.IO.FileStream(saveFile.FileName, System.IO.FileMode.Create);
                    range.Save(fStream, DataFormats.Text);
                    fStream.Close();
                }

            }
            catch (Exception ex)
            {
                ErrorOccuredAction?.Invoke(ex);
            }
        }

        [RelayCommand]
        private void ClearLog() => LoggerDocument.Blocks.Clear();
    }
}
