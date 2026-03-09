using System;
using System.Collections.Generic;
using System.Text;

namespace LotterySystem.Models
{
    public class Student
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int number, string firstName, string lastName)
        {
            Number = number;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{Number}. {LastName} {FirstName}";
        }

    }
}
