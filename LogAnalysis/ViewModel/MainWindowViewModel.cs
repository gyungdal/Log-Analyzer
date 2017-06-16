using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LogAnalysis.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _processValue;
        public event PropertyChangedEventHandler PropertyChanged;

        public double ProcessValue {
            get { return _processValue; }
            set {
                if (value == _processValue)
                    return;

                _processValue = value;
                OnPropertyChanged("ProcessValue");
            }
        }
        public MainWindowViewModel()
        {

        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
