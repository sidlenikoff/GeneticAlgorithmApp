using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneticAlgorithm_App
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public AlgorithmParameters AlgorithmParameters { get; private set; }

        public SettingsWindow(AlgorithmParameters parameters = null)
        {
            InitializeComponent();

            this.DataContext = new SettingsViewModel(parameters);
            (this.DataContext as SettingsViewModel).CloseWindowAction += SettingsWindow_CloseWindowAction;
            (this.DataContext as SettingsViewModel).ErrorOccuredAction += DisplayErrorDialog;
        }

        private void SettingsWindow_CloseWindowAction()
        {
            AlgorithmParameters = (this.DataContext as SettingsViewModel).AlgorithmParameters;

            this.DialogResult = true;
            Close();
        }

        private async void DisplayErrorDialog(Exception ex)
        {
            var result = await Helpers.DisplayErrorDialog(ex);
        }
    }
}
