
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;


using WeatherWise.MVVM.Models;

namespace WeatherWise.MVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
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
        private LoginRequestModel myloginRequestModel = new LoginRequestModel();
        public INavigation Navigation { get; set; }
        public LoginRequestModel MyLoginRequestModel
        {
            get { return myloginRequestModel; }
            set
            {
                myloginRequestModel = value;

                OnPropertChanged(nameof(MyLoginRequestModel));
            }
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


            {
                try
                {
                    if (IsBusy) return;
                    IsBusy = true;
                    ShowButton = false;

                    if (MyLoginRequestModel.UserName == null || MyLoginRequestModel.Password == null)
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


                        await _authClient.SignInWithEmailAndPasswordAsync(MyLoginRequestModel.UserName, MyLoginRequestModel.Password);
                        IsBusy = false;
                        ShowButton = true;
                        await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "Ok");
                       

                        await Navigation.PushAsync(new Views.WeatherView());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    IsBusy = false;
                    ShowButton = true;
                    await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
                }
            }

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