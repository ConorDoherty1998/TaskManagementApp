using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Foop2CA1
{

    public class Task
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public Catagory TaskCatagory { get; set; }
        public DateTime DueDate { get; set; }
        public Priority TaskPriority { get; set; }
        public List<string> Labels { get; set; }
        public User Responsibility { get; set; }
        public bool IsOverdue { get; set; }

        
        public Task() { }

        public Task(string title, string desc, string catagory, DateTime? duedate, string priority)
        {
            Title = title;
            Desc = desc;
            TaskCatagory = GetCatagory(catagory);
            DueDate = duedate.Value;
            TaskPriority = GetPriority(priority);
            Console.WriteLine($"Built Task {title}");
            Labels = getLabels();
        }

        //enums
        public enum Catagory
        {
            Database,
            Servers,
            BackEnd,
            FrontEnd
        };
        public enum Priority
        {
            Low,
            Mid,
            High,
        }

        public Priority GetPriority(string priorityText)
        {
            Priority temp = new Priority();
            switch(priorityText)
            {
                case "High":
                    temp = Priority.High;
                    break;
                case "Mid":
                    temp = Priority.Mid;
                    break;
                case "Low":
                    temp = Priority.Low;
                    break;
            }
            return temp;
        }
        public Catagory GetCatagory(string catagoryText)
        {
            Catagory temp = new Catagory();
            switch(catagoryText)
            {
                case "Database":
                    temp = Catagory.Database;
                    break;
                case "Front End":
                    temp = Catagory.FrontEnd;
                    break;
                case "Back End":
                    temp = Catagory.BackEnd;
                    break;
                case "Server Management":
                    temp = Catagory.Servers;
                    break;
            }
            return temp;
        }

        //Method to find and save labels from description
        public List<string> getLabels()
        {
            string tempDesc = Desc;
            bool HasLabelLeft = true;
            List<string> Temp = new List<string>();
            string labelPattern = @"#\b\S+?\b";
            
            while (HasLabelLeft)
            {
                Match label = Regex.Match(tempDesc, labelPattern);
                if (label.Value == "")
                    HasLabelLeft = false;
                else if (!Temp.Contains(label.Value))
                {
                    Temp.Add(label.Value.ToLower());
                    Console.WriteLine($"Added label {label.Value}");
                    tempDesc = tempDesc.Remove(0, (label.Index + label.Value.Length));
                }
            }
            return Temp;
        }
    }
}
