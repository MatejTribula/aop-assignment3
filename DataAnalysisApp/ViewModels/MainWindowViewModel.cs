namespace DataAnalysisApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    private GameManager gameManager = new GameManager();

    public string TestData { get; set; }
    public string Greeting { get; } = "Welcome to Avalonia!";



    public MainWindowViewModel()
    {
        TestData = gameManager.Games[0].Name;
    }

}
