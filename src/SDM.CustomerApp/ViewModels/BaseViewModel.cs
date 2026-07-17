namespace SDM.CustomerApp.ViewModels;

/// <summary>
/// Base ViewModel implementing INotifyPropertyChanged.
/// All ViewModels in the Customer App inherit from this.
/// </summary>
public abstract class BaseViewModel : System.ComponentModel.INotifyPropertyChanged
{
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value,
        [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
