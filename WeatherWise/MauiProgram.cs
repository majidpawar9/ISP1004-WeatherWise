using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace WeatherWise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();

            builder.Services.AddSingleton(services => new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyCIM8KnE9p3feUGAJjk51StTBA8CAwU8Gk",
                AuthDomain = "loginwith-99aa7.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
                UserRepository = services.GetRequiredService<IUserRepository>()
            }));
#endif

            return builder.Build();
        }
    }
}