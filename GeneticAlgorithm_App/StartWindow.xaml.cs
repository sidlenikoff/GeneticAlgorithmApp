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
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
            (this.DataContext as MainViewModel).ErrorOccuredAction += DisplayErrorDialog;
        }

        private void DisplayErrorDialog(Exception ex)
        {
            var result = Helpers.DisplayErrorDialog(ex);
        }
    }
}
