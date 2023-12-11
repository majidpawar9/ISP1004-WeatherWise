using WeatherWise.MVVM.ViewModels;

namespace WeatherWise.MVVM.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
		  BindingContext = new WeatherViewModel();
	}
}