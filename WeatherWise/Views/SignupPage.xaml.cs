using WeatherWise.ViewModels;
namespace WeatherWise.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
        BindingContext = new SignupViewModel(Navigation);
    }
}
