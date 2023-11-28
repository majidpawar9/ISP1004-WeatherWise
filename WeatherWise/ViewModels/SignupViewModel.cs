using AndroidX.Lifecycle;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherWise.Models;

/* ViewModel to handle signup */

namespace WeatherWise.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        #region Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }


        private bool _showButton = true;
        public bool ShowButton
        {
            get => _showButton;
            set => SetProperty(ref _showButton, value);
        }

        #endregion
        protected bool SetProperty<T>(ref T backingStore, T value,
             [CallerMemberName] string propertyName = "",
             Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private SignupRequestModel mySignupRequestModel = new SignupRequestModel();
        public INavigation Navigation { get; set; }

        public ICommand LogInUser { get; set; }
        public SignupRequestModel MySignupRequestModel
        {
            get => mySignupRequestModel;
            set => SetProperty(ref mySignupRequestModel, value);
            //get { return mySignupRequestModel; }
            //set
            //{
            //    mySignupRequestModel = value;

            //    OnPropertChanged(nameof(MySignupRequestModel));
            //}
        }

        public ICommand SignupCommand { get; set; }

        public SignupViewModel(INavigation navigation)
        {

            SignupCommand = new Command(SignUpCommand);
            this.Navigation = navigation;
            LogInUser = new Command(NavigateToLogIn);
        }

        private async void SignUpCommand()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                ShowButton = false;
                // Validate user input before submission
                if (MySignupRequestModel.UserName == null || MySignupRequestModel.Password == null)
                {
                    IsBusy = false;
                    ShowButton = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "username and password cannot be empty", "Ok");
                }
                else
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


                    await _authClient.CreateUserWithEmailAndPasswordAsync(MySignupRequestModel.UserName, MySignupRequestModel.Password);
                    IsBusy = false;
                    ShowButton = true;
                    await Application.Current.MainPage.DisplayAlert("Success", "Successfully signed up!", "Ok");


                    await Navigation.PushAsync(new Views.WeatherWisePage());
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                IsBusy = false;
                ShowButton = true;
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