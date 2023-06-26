using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TccApi.ViewModels
{
    public class BaseViewModel : INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
            {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
    }
}
