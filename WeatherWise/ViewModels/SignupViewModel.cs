using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherWise.Models;

/* ViewModel to handle signup */

namespace WeatherWise.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private SignupRequestModel mySignupRequestModel = new SignupRequestModel();
        public SignupRequestModel MySignupRequestModel
        {
            get { return mySignupRequestModel; }
            set
            {
                mySignupRequestModel = value;

                OnPropertChanged(nameof(MySignupRequestModel));
            }
        }

        public ICommand SignupCommand { get; set; }

        public SignupViewModel()
        {
            SignupCommand = new Command(PerformSignupOperation);
        }

        private async void PerformSignupOperation(object obj)
        {
            var data = mySignupRequestModel;
        }

        protected void OnPropertChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
