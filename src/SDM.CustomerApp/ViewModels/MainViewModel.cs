namespace SDM.CustomerApp.ViewModels;

/// <summary>
/// ViewModel for the main application window.
/// </summary>
public class MainViewModel : BaseViewModel
{
    private string _title = "Smart Device Manager";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
