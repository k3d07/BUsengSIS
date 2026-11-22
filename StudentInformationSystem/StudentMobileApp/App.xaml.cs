using Microsoft.Extensions.DependencyInjection;

namespace StudentMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromArgb("#1F6FEB"),
                BarTextColor = Colors.White
            });
        }
    }
}
