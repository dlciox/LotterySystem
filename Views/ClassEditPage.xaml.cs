using LotterySystem.Models;

namespace LotterySystem.Views;

public partial class ClassEditPage : ContentPage
{
    SchoolClass schoolClass;
    Student selectedStudent;

    public ClassEditPage(string className)
    {
        InitializeComponent();
        schoolClass = new SchoolClass(className);
        schoolClass.LoadFromFile();
        SortStudents();
        RenumberStudents();
        BindingContext = schoolClass;
    }

    private void AddStudent(object sender, EventArgs e)
    {
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return;

        schoolClass.Students.Add(new Student(0, firstName, lastName));
        SortStudents();
        RenumberStudents();
        RefreshList();

        FirstNameEntry.Text = "";
        LastNameEntry.Text = "";
    }

    private void SelectStudent(object sender, SelectionChangedEventArgs e)
    {
        selectedStudent = e.CurrentSelection.FirstOrDefault() as Student;
    }

    private async void EditStudent(object sender, EventArgs e)
    {
        if (selectedStudent == null)
        {
            await DisplayAlert("Błąd", "Wybierz ucznia z listy", "OK");
            return;
        }

        string newFirstName = await DisplayPromptAsync("Edycja", "Nowe imię:", initialValue: selectedStudent.FirstName);
        if (string.IsNullOrWhiteSpace(newFirstName))
            return;

        string newLastName = await DisplayPromptAsync("Edycja", "Nowe nazwisko:", initialValue: selectedStudent.LastName);
        if (string.IsNullOrWhiteSpace(newLastName))
            return;

        selectedStudent.FirstName = newFirstName;
        selectedStudent.LastName = newLastName;

        SortStudents();
        RenumberStudents();
        RefreshList();
    }

    private async void RemoveStudent(object sender, EventArgs e)
    {
        if (selectedStudent == null)
        {
            await DisplayAlert("Błąd", "Wybierz ucznia z listy", "OK");
            return;
        }

        schoolClass.Students.Remove(selectedStudent);
        selectedStudent = null;
        SortStudents();
        RenumberStudents();
        RefreshList();
    }

    private void SortStudents()
    {
        var sorted = schoolClass.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();

        schoolClass.Students.Clear();

        foreach (var s in sorted)
        {
            schoolClass.Students.Add(s);
        }
    }

    private void RenumberStudents()
    {
        int num = 1;
        foreach (var s in schoolClass.Students)
        {
            s.Number = num;
            num++;
        }
    }

    private void RefreshList()
    {
        StudentsListView.ItemsSource = null;
        StudentsListView.ItemsSource = schoolClass.Students;
    }

    private async void SaveClassToFile(object sender, EventArgs e)
    {
        string folderPath = Path.Combine(FileSystem.AppDataDirectory, "Classes");
        string filePath = Path.Combine(folderPath, schoolClass.ClassName + ".txt");

        if (!Directory.Exists(folderPath))
        {   
            Directory.CreateDirectory(folderPath);
        }

        using StreamWriter writer = new StreamWriter(filePath);

        foreach (var s in schoolClass.Students)
        {
            writer.WriteLine(s.Number + ";" + s.LastName + ";" + s.FirstName);
        }

        await DisplayAlert("Sukces", $"Lista została zapisana do pliku", "OK");
    }
}