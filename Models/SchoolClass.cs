using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LotterySystem.Models
{
    public class SchoolClass
    {
        public string ClassName { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        public SchoolClass(string className)
        {
            ClassName = className;
            Students = new ObservableCollection<Student>();
        }

        public void LoadFromFile()
        {
            string folderPath = Path.Combine(FileSystem.AppDataDirectory, "Classes");
            string filePath = Path.Combine(folderPath, ClassName + ".txt");

            if (!File.Exists(filePath))
                return;

            using StreamReader reader = new StreamReader(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(';');

                if (parts.Length == 3)
                {
                    int number;
                    if (int.TryParse(parts[0], out number))
                    {
                        Students.Add(new Student(number, parts[2], parts[1]));
                    }
                }
            }
        }
    }
}
