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

    private void DrawRandomStudent(object sender, EventArgs e)
    {
        if (schoolClass.Students.Count == 0)
        {
            ResultLabel.Text = "Brak uczniów";
            return;
        }

        int index = random.Next(schoolClass.Students.Count);
        ResultLabel.Text = schoolClass.Students[index].ToString();
    }
}