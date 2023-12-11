using System.Windows.Input;
using WeatherWise.MVVM.ViewModels;
namespace WeatherWise.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
    }
}