using System.Text.RegularExpressions;
using StudentMobileApp.Models;
using StudentMobileApp.Services;

namespace StudentMobileApp;

public partial class AddEditPage : ContentPage
{
    private static readonly string[] Genders = { "Male", "Female", "Other" };
    private static readonly string[] YearLevels = { "1st Year", "2nd Year", "3rd Year", "4th Year" };
    private static readonly string[] Programs =
    {
        "BS Computer Science",
        "BS Information Technology",
        "BS Information Systems",
        "BS Computer Engineering"
    };

    private static readonly Regex IdPattern = new("^[A-Za-z0-9-]+$", RegexOptions.Compiled);
    private static readonly Regex NamePattern = new("^[\\p{L} .,'\\-]+$", RegexOptions.Compiled);
    private static readonly Regex ContactPattern = new("^(09|\\+639)\\d{9}$", RegexOptions.Compiled);

    private readonly DatabaseService _db = new();
    private readonly bool _isEdit;

    public AddEditPage(Student? existing)
    {
        InitializeComponent();

        PickGender.ItemsSource = Genders;
        PickYear.ItemsSource = YearLevels;
        PickProgram.ItemsSource = Programs;

        _isEdit = existing is not null;
        if (existing is not null)
        {
            Title = "Edit student";
            PageHeading.Text = "Edit student";
            PageDescription.Text = "Update the student details below.";
            SaveButton.Text = "Save changes";

            EntryId.Text = existing.StudentId;
            EntryId.IsEnabled = false;
            EntryName.Text = existing.Name;
            EntryContact.Text = existing.ContactNumber;
            PickGender.SelectedItem = existing.Gender;
            PickYear.SelectedItem = existing.YearLevel;
            PickProgram.SelectedItem = existing.Program;
        }
        else
        {
            Title = "Add student";
        }
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        string id = EntryId.Text?.Trim() ?? string.Empty;
        string name = EntryName.Text?.Trim() ?? string.Empty;
        string contact = EntryContact.Text?.Trim() ?? string.Empty;

        string? validationError = Validate(id, name, contact);
        if (validationError is not null)
        {
            await DisplayAlert("Check student details", validationError, "OK");
            return;
        }

        var student = new Student
        {
            StudentId = id,
            Name = name,
            Gender = PickGender.SelectedItem!.ToString()!,
            YearLevel = PickYear.SelectedItem!.ToString()!,
            Program = PickProgram.SelectedItem!.ToString()!,
            ContactNumber = contact
        };

        SaveButton.IsEnabled = false;
        SaveButton.Text = _isEdit ? "Saving changes..." : "Saving student...";
        try
        {
            string? error = _isEdit
                ? await _db.UpdateStudentAsync(student)
                : await _db.AddStudentAsync(student);

            if (error is not null)
            {
                await DisplayAlert("Could not save student", error, "OK");
                return;
            }

            await DisplayAlert("Saved", _isEdit ? "Student details were updated." : "Student was added.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Could not save student", "Please check the connection and try again.", "OK");
        }
        finally
        {
            SaveButton.IsEnabled = true;
            SaveButton.Text = _isEdit ? "Save changes" : "Save student";
        }
    }

    private string? Validate(string id, string name, string contact)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(contact) || PickGender.SelectedItem is null ||
            PickYear.SelectedItem is null || PickProgram.SelectedItem is null)
            return "Please fill in all required fields.";

        if (id.Length > 20)
            return "Student ID must be 20 characters or fewer.";
        if (!IdPattern.IsMatch(id))
            return "Student ID may only contain letters, numbers, and dashes.";

        if (name.Length > 100)
            return "Name must be 100 characters or fewer.";
        if (!NamePattern.IsMatch(name))
            return "Name may only contain letters, spaces, and . , ' -";

        if (!Genders.Contains(PickGender.SelectedItem.ToString(), StringComparer.Ordinal))
            return "Please choose a gender from the list.";
        if (!YearLevels.Contains(PickYear.SelectedItem.ToString(), StringComparer.Ordinal))
            return "Please choose a year level from the list.";
        if (!Programs.Contains(PickProgram.SelectedItem.ToString(), StringComparer.Ordinal))
            return "Please choose a program from the list.";

        if (!ContactPattern.IsMatch(contact))
            return "Enter a valid Philippine mobile number, for example 09171234567.";

        return null;
    }
}
