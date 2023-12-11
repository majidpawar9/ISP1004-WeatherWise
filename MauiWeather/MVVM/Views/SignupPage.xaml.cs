using WeatherWise.MVVM.ViewModels;

namespace WeatherWise.MVVM.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
        BindingContext = new SignupViewModel(Navigation);
    }
}