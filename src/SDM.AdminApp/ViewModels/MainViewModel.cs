namespace SDM.AdminApp.ViewModels;

/// <summary>
/// ViewModel for the main admin window.
/// </summary>
public class MainViewModel : BaseViewModel
{
    private string _title = "Smart Device Manager Admin";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
