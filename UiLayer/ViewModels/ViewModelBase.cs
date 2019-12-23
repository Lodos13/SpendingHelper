using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UiLayer
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
