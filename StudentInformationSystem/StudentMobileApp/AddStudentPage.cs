using StudentMobileApp.Models;
using StudentMobileApp.Services;
using Microsoft.Maui.Controls.Shapes;

namespace StudentMobileApp;

public sealed class AddStudentPage : ContentPage
{
    private readonly DatabaseService _db;
    private readonly Entry _studentId = new() { Placeholder = "e.g. 2026-001" };
    private readonly Entry _name = new() { Placeholder = "Enter full name" };
    private readonly Entry _contact = new() { Placeholder = "Enter contact number", Keyboard = Keyboard.Telephone };
    private readonly Picker _gender = new() { Title = "Select gender" };
    private readonly Picker _yearLevel = new() { Title = "Select year level" };
    private readonly Picker _program = new() { Title = "Select program" };
    private readonly Label _error = new()
    {
        IsVisible = false,
        FontSize = 13,
        TextColor = Color.FromArgb("#CF222E")
    };
    private readonly Button _saveButton = new()
    {
        Text = "Save student",
        BackgroundColor = Color.FromArgb("#1F6FEB"),
        TextColor = Colors.White,
        CornerRadius = 12,
        HeightRequest = 50,
        FontAttributes = FontAttributes.Bold
    };

    public AddStudentPage(DatabaseService db)
    {
        _db = db;
        Title = "Add student";

        _gender.ItemsSource = new[] { "Male", "Female", "Other" };
        _yearLevel.ItemsSource = new[] { "1st Year", "2nd Year", "3rd Year", "4th Year" };
        _program.ItemsSource = new[]
        {
            "BS Computer Science",
            "BS Information Technology",
            "BS Information Systems",
            "BS Computer Engineering"
        };

        var form = new VerticalStackLayout
        {
            Spacing = 16,
            Padding = new Thickness(20),
            Children =
            {
                new Label
                {
                    Text = "Student details",
                    FontSize = 22,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#1F2328")
                },
                new Label
                {
                    Text = "Enter the student information below.",
                    FontSize = 13,
                    TextColor = Color.FromArgb("#656D76")
                },
                Field("Student ID", _studentId),
                Field("Full name", _name),
                Field("Gender", _gender),
                Field("Year level", _yearLevel),
                Field("Program", _program),
                Field("Contact number", _contact),
                _error,
                _saveButton
            }
        };

        Content = new ScrollView
        {
            Content = new Border
            {
                Margin = new Thickness(16, 14),
                Padding = 0,
                Stroke = Color.FromArgb("#E3E8EF"),
                StrokeShape = new RoundRectangle { CornerRadius = 18 },
                BackgroundColor = Colors.White,
                Content = form
            }
        };

        _saveButton.Clicked += OnSaveClicked;
        ApplyTheme();
        Application.Current!.RequestedThemeChanged += OnRequestedThemeChanged;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (Application.Current is not null)
            Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
    }

    private static View Field(string title, View input)
    {
        input.HeightRequest = 48;
        if (input is InputView inputControl)
        {
            inputControl.BackgroundColor = Color.FromArgb("#F6F8FA");
            inputControl.TextColor = Color.FromArgb("#1F2328");
            inputControl.FontSize = 14;
        }
        else if (input is Picker picker)
        {
            picker.BackgroundColor = Color.FromArgb("#F6F8FA");
            picker.TextColor = Color.FromArgb("#1F2328");
            picker.FontSize = 14;
        }

        return new VerticalStackLayout
        {
            Spacing = 5,
            Children =
            {
                new Label
                {
                    Text = title,
                    FontSize = 13,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#656D76")
                },
                new Border
                {
                    Padding = new Thickness(10, 0),
                    Stroke = Color.FromArgb("#D0D7DE"),
                    StrokeShape = new RoundRectangle { CornerRadius = 10 },
                    Content = input
                }
            }
        };
    }

    private void ApplyTheme()
    {
        bool dark = Application.Current?.RequestedTheme == AppTheme.Dark;
        BackgroundColor = dark ? Color.FromArgb("#0D1117") : Color.FromArgb("#F6F8FA");
        Color surface = dark ? Color.FromArgb("#161B22") : Colors.White;
        Color text = dark ? Color.FromArgb("#E6EDF3") : Color.FromArgb("#1F2328");
        Color muted = dark ? Color.FromArgb("#8B949E") : Color.FromArgb("#656D76");
        Color inputBackground = dark ? Color.FromArgb("#0D1117") : Color.FromArgb("#F6F8FA");
        Color borderColor = dark ? Color.FromArgb("#30363D") : Color.FromArgb("#D0D7DE");

        if (Content is ScrollView scroll && scroll.Content is Border card && card.Content is Layout form)
        {
            card.BackgroundColor = surface;
            card.Stroke = borderColor;
            foreach (View view in form.Children)
            {
                if (view is Label label && label != _error)
                    label.TextColor = label.FontSize >= 20 ? text : muted;
                if (view is VerticalStackLayout fieldLayout && fieldLayout.Children.Count > 1 && fieldLayout.Children[1] is Border inputBorder)
                {
                    inputBorder.Stroke = borderColor;
                    if (inputBorder.Content is InputView input)
                    {
                        input.BackgroundColor = inputBackground;
                        input.TextColor = text;
                    }
                    else if (inputBorder.Content is Picker picker)
                    {
                        picker.BackgroundColor = inputBackground;
                        picker.TextColor = text;
                    }
                    if (fieldLayout.Children[0] is Label fieldLabel)
                        fieldLabel.TextColor = muted;
                }
            }
        }
    }

    private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e) => ApplyTheme();

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        string id = _studentId.Text?.Trim() ?? string.Empty;
        string name = _name.Text?.Trim() ?? string.Empty;
        string contact = _contact.Text?.Trim() ?? string.Empty;

        if (id.Length == 0 || name.Length == 0 || contact.Length == 0 ||
            _gender.SelectedItem is null || _yearLevel.SelectedItem is null || _program.SelectedItem is null)
        {
            ShowError("Complete all fields before saving.");
            return;
        }

        _saveButton.IsEnabled = false;
        _saveButton.Text = "Saving...";
        _error.IsVisible = false;
        try
        {
            var student = new Student
            {
                StudentId = id,
                Name = name,
                ContactNumber = contact,
                Gender = _gender.SelectedItem.ToString()!,
                YearLevel = _yearLevel.SelectedItem.ToString()!,
                Program = _program.SelectedItem.ToString()!
            };

            string? error = await _db.AddStudentAsync(student);
            if (error is not null)
            {
                ShowError(error);
                return;
            }

            await DisplayAlert("Student added", "The student record was saved.", "Done");
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            ShowError("Could not save the student. Check the connection and try again.");
        }
        finally
        {
            _saveButton.IsEnabled = true;
            _saveButton.Text = "Save student";
        }
    }

    private void ShowError(string message)
    {
        _error.Text = message;
        _error.IsVisible = true;
    }
}
