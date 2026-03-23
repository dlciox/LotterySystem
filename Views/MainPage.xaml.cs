namespace LotterySystem.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadClasses();
    }

    private void LoadClasses()
    {
        string folderPath = ClassFolderPath();

        ClassPicker.Items.Clear();

        var files = Directory.GetFiles(folderPath, "*.txt");

        foreach (var file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            ClassPicker.Items.Add(name);
        }

        if (ClassPicker.Items.Count > 0)
        {
            ClassPicker.SelectedIndex = 0;
        }
    }

    private async void CreateClass(object sender, EventArgs e)
    {
        string className = NewClassEntry.Text;

        if (string.IsNullOrWhiteSpace(className))
        {
            await DisplayAlert("Błąd", "Podaj nazwę klasy", "OK");
            return;
        }

        string folderPath = ClassFolderPath();

        string filePath = Path.Combine(folderPath, className + ".txt");

        if (File.Exists(filePath))
        {
            await DisplayAlert("Błąd", "Klasa o tej nazwie juz istnieje", "OK");
            return;
        }

        File.WriteAllText(filePath, "");
        NewClassEntry.Text = "";
        LoadClasses();

        ClassPicker.SelectedItem = className;
    }

    private async void OpenClassEditor(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedItem == null) return;

        var className = ClassPicker.SelectedItem.ToString();
        await Navigation.PushAsync(new ClassEditPage(className));
    }

    private async void OpenLotteryPage(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedItem == null) return;

        var className = ClassPicker.SelectedItem.ToString();
        await Navigation.PushAsync(new LotteryPage(className));
    }

    private string ClassFolderPath()
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "Classes");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }
}