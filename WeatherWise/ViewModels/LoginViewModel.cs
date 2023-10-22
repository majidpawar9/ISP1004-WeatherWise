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
        public INavigation Navigation { get; set; }
        public LoginRequestModel MyLoginRequestModel
        {
            get { return myloginRequestModel; }
            set { myloginRequestModel = value; 
            
            OnPropertChanged(nameof(MyLoginRequestModel));} 
        }

        public ICommand LoginCommand { get; set; }
        public ICommand SignupUser { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            LoginCommand = new Command(PerformLoginOperation);
            this.Navigation = navigation;
            SignupUser = new Command(NavigateToSignUp);
        }

        private async void PerformLoginOperation(object obj)
        {
            var data = MyLoginRequestModel;
        }

        private async void NavigateToSignUp()
        {
            await Navigation.PushAsync(new Views.SignupPage());
        }

        protected void OnPropertChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
