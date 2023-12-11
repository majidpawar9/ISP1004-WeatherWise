using WeatherWise.MVVM.Views;

namespace WeatherWise;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new NavigationPage(new LoginPage());

    }
}
