using LotterySystem.Models;

namespace LotterySystem.Views;

public partial class LotteryPage : ContentPage
{

    SchoolClass schoolClass;
    Random random = new Random();

    public LotteryPage(string className)
    {
        InitializeComponent();
        schoolClass = new SchoolClass(className);
        schoolClass.LoadFromFile();
    }

    private void DrawStudent(object sender, EventArgs e)
    {
        if (schoolClass.Students.Count == 0)
        {
            ResultLabel.Text = "Brak uczniów";
            return;
        }

        var student = schoolClass.Students[random.Next(schoolClass.Students.Count)];
        ResultLabel.Text = student.Name;
    }
}