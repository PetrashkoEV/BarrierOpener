using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarrierOpener.Client.ViewModels;

internal class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _buttonEnabled = true;
    public bool ButtonEnabled
    {
        get => _buttonEnabled;
        set
        {
            //_buttonEnabled = value;
            SetField(ref _buttonEnabled, value, nameof(ButtonEnabled));
        }
    }

    private Brush _buttonBrush = Brush.Green;
    public Brush ButtonBrush
    {
        get => _buttonBrush;
        set
        {
            //_buttonBrush = value;
            SetField(ref _buttonBrush, value, nameof(ButtonBrush));
        }
    }


    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}