using WeatherWise.ViewModels;

namespace WeatherWise.Views;

public partial class WeatherWisePage : ContentPage
{
	public WeatherWisePage()
	{
		InitializeComponent();
		BindingContext = new WeatherWiseViewModel();
	}
}