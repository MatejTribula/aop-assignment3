using Avalonia.Controls;
using DataAnalysisApp.ViewModels;

namespace DataAnalysisApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Set the DataContext here
        }
    }
}
