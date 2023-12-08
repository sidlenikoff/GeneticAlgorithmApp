using ModernWpf.Controls;
using System;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    public static class Helpers
    {
        public async static Task<ContentDialogResult> DisplayErrorDialog(Exception ex)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Error",
                Content = $"{ex.Message}",
                PrimaryButtonText = "Ok",
            };

            return await errorDialog.ShowAsync();
        }
    }
}
