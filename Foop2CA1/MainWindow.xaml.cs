using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace Foop2CA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        //Lists
        public static List<Task> CompletedTasks = new List<Task>();
        public static List<Task> Tasks = new List<Task>();
        public static List<User> Users = new List<User>();
        
        public MainWindow()
        {
            InitializeComponent();

            //Timer Event Handler to Check the due dates every second
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += IsOverdueEvent;
            dt.Interval = new TimeSpan(0,0,1);
            dt.Start();

            //declaring objects and adding them to listboxes
            User u1 = new User()
            {
                FirstName = "Conor",
                LastName = "Doherty",
                JobTitle = "Software Developer"
            };
            Users.Add(u1);
            User u2 = new User()
            {
                FirstName = "Mark",
                LastName = "Maguire",
                JobTitle = "Systems Admin"
            };
            Users.Add(u2);
            User u3 = new User()
            {
                FirstName = "Brian",
                LastName = "Keaveney",
                JobTitle = "Database Developer"
            };
            Users.Add(u3);

            Task T1 = new Task("Test Task", "This is only a test to see how the template will format a description that is too long. My name is conor I am studying Honours computing in it sligo #ITsligo #Test ", "BackEnd", new DateTime(2019, 4, 20), "Low");
            T1.Responsibility = u1;
            Task T2 = T1;
            Task T3 = T1;
            Task T4 = T1;
            Tasks.Add(T1);
            Tasks.Add(T2);
            Tasks.Add(T3);
            Tasks.Add(T4);
            lstbxTasks.ItemsSource = Tasks;
            lstbxCompletedTasks.ItemsSource = CompletedTasks;
        }
        //Listbox sources are reset every time a window is closed
        private void Window_Activated(object sender, EventArgs e)
        {
            lstbxTasks.ItemsSource = null;
            lstbxTasks.ItemsSource = Tasks;
        }

        #region Buttons
        //Method to Complete Task
        private void BtnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxTasks.SelectedItem != null)
            {
                Task SelectedTask = lstbxTasks.SelectedItem as Task;
                if (!SelectedTask.IsOverdue)
                {
                    if (SelectedTask.Responsibility != null)
                    {
                        CompletedTask ctTemp = new CompletedTask(SelectedTask);
                        Tasks.Remove(SelectedTask);
                        CompletedTasks.Add(ctTemp);
                        lstbxTasks.ItemsSource = null;
                        lstbxTasks.ItemsSource = Tasks;
                        lstbxCompletedTasks.ItemsSource = null;
                        lstbxCompletedTasks.ItemsSource = CompletedTasks;
                    }
                    else
                        MessageBox.Show("This Task has not yet been delegated to someone");
                }
                else
                    MessageBox.Show("This Task is overdue, and therefore cannot be completed");
            }
            else
                MessageBox.Show("Please select a task to be completed");
        }

        //Method to add task
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskPage TaskpageTemp = new TaskPage();
            TaskpageTemp.Owner = this;
            TaskpageTemp.ShowDialog();
        }
        //Method to delete task
        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxTasks.SelectedItem != null)
            {
                Task SelectedTask = lstbxTasks.SelectedItem as Task;
                Tasks.Remove(SelectedTask);
                lstbxTasks.ItemsSource = null;
                lstbxTasks.ItemsSource = Tasks;
            }
            else
                MessageBox.Show("Please select a Task to be deleted");
        }
        //Method to edit task
        private void BtnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxTasks.SelectedItem != null)
            {
                Task SelectedTask = lstbxTasks.SelectedItem as Task;
                TaskPage TaskpageTemp = new TaskPage(SelectedTask);
                TaskpageTemp.Owner = this;
                TaskpageTemp.ShowDialog();
            }
            else
                MessageBox.Show("Please Select a Task to be edited");
        }
        //Method to delegate task
        private void BtnDelegate_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxTasks.SelectedItem != null)
            {
                Task SelectedTask = lstbxTasks.SelectedItem as Task;
                Delegate TempDelegate = new Delegate(SelectedTask);
                TempDelegate.Owner = this;
                TempDelegate.ShowDialog();
            }
            else
                MessageBox.Show("Please Select a Task to be delegated");
        }
        #endregion


        #region Filters
        private void SearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchName.Text.ToLower();
            if (filter != "Search...")
            {
                lstbxTasks.ItemsSource = null;
                lstbxTasks.ItemsSource = Tasks.Where(t => t.Title.ToLower().Contains(filter));
            }
        }

        private void RadioButtonCategory_Checked(object sender, RoutedEventArgs e)
        {
            lstbxTasks.ItemsSource = Tasks;
            RadioButton SelectedRB = sender as RadioButton;
            string filter = SelectedRB.Content as string;
            switch(filter)
            {
                case "All":
                    lstbxTasks.ItemsSource = Tasks;
                    break;
                case "Database":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskCatagory == Task.Catagory.Database);
                    break;
                case "Front End":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskCatagory == Task.Catagory.FrontEnd);
                    break;
                case "Back End":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskCatagory == Task.Catagory.BackEnd);
                    break;
                case "Servers":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskCatagory == Task.Catagory.Servers);
                    break;
            }
        }

        private void RadioButtonDate_Checked(object sender, RoutedEventArgs e)
        {
            lstbxTasks.ItemsSource = Tasks;
            RadioButton SelectedRB = sender as RadioButton;
            string filter = SelectedRB.Content as string;
            switch (filter)
            {
                case "All":
                    lstbxTasks.ItemsSource = Tasks;
                    break;
                case "Tomorrow":
                    lstbxTasks.ItemsSource = Tasks.Where(t => (t.DueDate - DateTime.Now) <= new TimeSpan(1,0,0,0));
                    break;
                case "This Week":
                    lstbxTasks.ItemsSource = Tasks.Where(t => (t.DueDate - DateTime.Now) <= new TimeSpan(7,0,0,0));
                    break;
                case "This Month":
                    lstbxTasks.ItemsSource = Tasks.Where(t => (t.DueDate - DateTime.Now) <= new TimeSpan(30,0,0,0));
                    break;
                case "Servers":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskCatagory == Task.Catagory.Servers);
                    break;
            }
        }

        private void RadioButtonPriority_Checked(object sender, RoutedEventArgs e)
        {
            lstbxTasks.ItemsSource = Tasks;
            RadioButton SelectedRB = sender as RadioButton;
            string filter = SelectedRB.Content as string;
            switch (filter)
            {
                case "All":
                    lstbxTasks.ItemsSource = Tasks;
                    break;
                case "High":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskPriority == Task.Priority.High);
                    break;
                case "Mid":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskPriority == Task.Priority.Mid);
                    break;
                case "Low":
                    lstbxTasks.ItemsSource = Tasks.Where(t => t.TaskPriority == Task.Priority.Low);
                    break;
            }
        }

        private void SearchLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchLabel.Text.ToLower();
            if (filter != "#Label...")
            {

                lstbxTasks.ItemsSource = null;
                lstbxTasks.ItemsSource = Tasks.Where(t => t.Labels.Contains(filter));
            }
        }



        #endregion

        //button to save the list of Tasks in JSON format
        private void BtnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Tasks, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(@"Tasks.json"))
            {
                sw.Write(json);
            }
            MessageBox.Show("Saved all tasks to a json file");
        }

        //Event Handler Method to check the DueDate property every second
        public void IsOverdueEvent(object Sender, System.EventArgs e)
        {
            foreach (Task t in Tasks)
            {
                if (t.DueDate <= DateTime.Now)
                {
                    t.IsOverdue = true;
                }
            }
        }
    }
}
