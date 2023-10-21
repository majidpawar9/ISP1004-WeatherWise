using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherWise.Models;

namespace WeatherWise.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginRequestModel myloginRequestModel = new LoginRequestModel();
        public LoginRequestModel MyLoginRequestModel
        {
            get { return myloginRequestModel; }
            set { myloginRequestModel = value; 
            
            OnPropertChanged(nameof(MyLoginRequestModel));} 
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(PerformLoginOperation);
        }

        private async void PerformLoginOperation(object obj)
        {
            var data = MyLoginRequestModel;
        }

        protected void OnPropertChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
