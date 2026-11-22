using System.Collections.ObjectModel;
using StudentMobileApp.Models;
using StudentMobileApp.Services;

namespace StudentMobileApp;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _db = new();
    private readonly ObservableCollection<Student> _view = new();
    private List<Student> _all = new();
    private string _yearFilter = "All";
    private bool _isDarkTheme;
    private bool _isAnimatingTheme;

    public MainPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        StudentsView.ItemsSource = _view;
        _isDarkTheme = Application.Current?.RequestedTheme == AppTheme.Dark;
        UpdateThemeIcon();
        UpdateFilterStyles();
        UpdateResultsCount();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        ResultsLabel.Text = "Loading student records...";
        EmptySpinner.IsRunning = true;
        EmptySpinner.IsVisible = true;
        EmptyMessage.Text = "Loading students…";
        EmptyHint.Text = "Connecting to the student database.";

        try
        {
            var (items, error) = await _db.GetStudentsAsync();
            if (error is not null)
            {
                ResultsLabel.Text = "Student records unavailable";
                EmptyMessage.Text = "Students could not be loaded";
                EmptyHint.Text = "Check the connection and pull down to try again.";
                await DisplayAlert("Connection problem", error, "OK");
                return;
            }

            _all = items;
            ApplyFilter();
        }
        catch (Exception)
        {
            ResultsLabel.Text = "Student records unavailable";
            EmptyMessage.Text = "Students could not be loaded";
            EmptyHint.Text = "Check the connection and pull down to try again.";
            await DisplayAlert("Unable to load students", "Please check your connection and try again.", "OK");
        }
        finally
        {
            EmptySpinner.IsRunning = false;
            EmptySpinner.IsVisible = false;
            Refresher.IsRefreshing = false;
        }
    }

    private void ApplyFilter()
    {
        string query = Search.Text?.Trim() ?? string.Empty;
        IEnumerable<Student> filtered = _all.Where(student =>
            (_yearFilter == "All" || student.YearLevel == _yearFilter) &&
            (query.Length == 0 ||
             student.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
             student.StudentId.Contains(query, StringComparison.OrdinalIgnoreCase)));

        _view.Clear();
        foreach (Student student in filtered)
            _view.Add(student);

        EmptyMessage.Text = query.Length > 0 || _yearFilter != "All"
            ? "No matching students"
            : "No students found";
        EmptyHint.Text = query.Length > 0 || _yearFilter != "All"
            ? "Try another search or choose a different year level."
            : "Add a student to get started.";
        UpdateResultsCount();
    }

    private void UpdateResultsCount() => ResultsLabel.Text =
        $"Showing {_view.Count} of {_all.Count} students";

    private void OnSearchChanged(object? sender, TextChangedEventArgs e) => ApplyFilter();

    private void OnFilterClicked(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.CommandParameter is not string year)
            return;

        _yearFilter = year;
        UpdateFilterStyles();
        ApplyFilter();
    }

    private void UpdateFilterStyles()
    {
        Color selectedText = Colors.White;
        Color unselectedBackground = _isDarkTheme
            ? Color.FromArgb("#161B22")
            : Colors.White;
        Color unselectedBorder = _isDarkTheme
            ? Color.FromArgb("#30363D")
            : Color.FromArgb("#D0D7DE");

        foreach (Button filter in YearFilters.Children.OfType<Button>())
        {
            bool selected = Equals(filter.CommandParameter, _yearFilter);
            filter.BackgroundColor = selected
                ? Color.FromArgb("#1F6FEB")
                : unselectedBackground;
            filter.TextColor = selected
                ? selectedText
                : Color.FromArgb("#1F6FEB");
            filter.BorderColor = selected
                ? Color.FromArgb("#1F6FEB")
                : unselectedBorder;
        }
    }

    private async void OnPullRefresh(object? sender, EventArgs e) => await LoadAsync();

    private async void OnStudentSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Student student)
            return;

        StudentsView.SelectedItem = null;
        try
        {
            string action = await DisplayActionSheet(
                $"{student.Name}\nID: {student.StudentId}\n{student.Program} | {student.YearLevel}\nGender: {student.Gender}\nContact: {student.ContactNumber}",
                "Close", null, "Edit");

            if (action == "Edit")
                await Navigation.PushAsync(new AddEditPage(student));
        }
        catch (Exception)
        {
            await DisplayAlert("Unable to open student", "Please try again.", "OK");
        }
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new AddEditPage(null));
        }
        catch (Exception)
        {
            await DisplayAlert("Unable to open form", "Please try again.", "OK");
        }
    }

    private async void OnToggleTheme(object? sender, EventArgs e)
    {
        if (_isAnimatingTheme)
            return;

        _isAnimatingTheme = true;
        try
        {
            await Task.WhenAll(
                ThemeButton.ScaleTo(0.78, 90, Easing.CubicIn),
                ThemeButton.RotateTo(-35, 90, Easing.CubicIn));

            _isDarkTheme = !_isDarkTheme;
            if (Application.Current is not null)
                Application.Current.UserAppTheme = _isDarkTheme ? AppTheme.Dark : AppTheme.Light;

            UpdateThemeIcon();
            UpdateFilterStyles();

            await Task.WhenAll(
                ThemeButton.RotateTo(0, 180, Easing.CubicOut),
                ThemeButton.ScaleTo(1, 180, Easing.SpringOut));
        }
        finally
        {
            _isAnimatingTheme = false;
            ThemeButton.Rotation = 0;
            ThemeButton.Scale = 1;
        }
    }

    private void UpdateThemeIcon() =>
        ThemeButton.Source = ImageSource.FromFile(_isDarkTheme ? "sun.svg" : "moon.svg");
}
