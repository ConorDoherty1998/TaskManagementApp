using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foop2CA1
{
    class CompletedTask : Task
    {
        public DateTime DateCompleted { get; set; }
        //Constructor
        public CompletedTask(Task tempTask)
        {
            Title = tempTask.Title;
            Desc = tempTask.Desc;
            TaskCatagory = tempTask.TaskCatagory;
            DueDate = tempTask.DueDate;
            TaskPriority = tempTask.TaskPriority;
            Labels = tempTask.Labels;
            Responsibility = tempTask.Responsibility;
            DateCompleted = DateTime.Now;
        }
    }
}
