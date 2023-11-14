
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            SignupCommand = new Command(SignUpCommand);
            this.Navigation = navigation;
            LogInUser = new Command(NavigateToLogIn);
        }

        //private async void PerformSignupOperation(object obj )
        //{
        //    var data = MySignupRequestModel;
        //    Debug.WriteLine(data.Password);
        //    Debug.WriteLine(data.UserName);
           
        //}


        private async void SignUpCommand()
        {       
            try
            {
               
                var config = new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyCIM8KnE9p3feUGAJjk51StTBA8CAwU8Gk",
                    AuthDomain = "loginwith-99aa7.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
        // Add and configure individual providers
        
        new EmailProvider()
                        // ...
                    },
                    // WPF:
                    UserRepository = new FileUserRepository("FirebaseSample") // persist data into %AppData%\FirebaseSample

                };

                // ...and create your FirebaseAuthClient
                var _authClient = new FirebaseAuthClient(config);


                await _authClient.CreateUserWithEmailAndPasswordAsync("chinedu4@email.com", "1234567");

                await Application.Current.MainPage.DisplayAlert("Success", "Successfully signed up!", "Ok");


                await Navigation.PushAsync(new Views.LoginPage());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
            }
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
