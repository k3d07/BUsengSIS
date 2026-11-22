using StudentMobileApp.Services;

namespace StudentMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var (items, error) = await new DatabaseService().GetStudentsAsync();
            TestLabel.Text = error ?? $"Connected! {items.Count} students loaded.";
        }
    }
}
