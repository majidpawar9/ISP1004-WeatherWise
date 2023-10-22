using Microsoft.Maui.Controls;
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
        public INavigation Navigation { get; set; }

        public ICommand LogInUser { get; set; }
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

        public SignupViewModel(INavigation navigation)
        {
            SignupCommand = new Command(PerformSignupOperation);
            this.Navigation = navigation;
            LogInUser = new Command(NavigateToLogIn);
        }

        private async void PerformSignupOperation(object obj)
        {
            var data = MySignupRequestModel;
        }

        private async void NavigateToLogIn()
        {
            await Navigation.PushAsync(new Views.LoginPage());
        }

        protected void OnPropertChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
