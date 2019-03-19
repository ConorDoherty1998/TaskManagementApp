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
using System.Windows.Shapes;

namespace Foop2CA1
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Window
    {
        public Task TaskToEdit = new Task();
        public TaskPage()
        {
            InitializeComponent();
        }

        public TaskPage(Task tempTask) : this()
        {
            TaskToEdit = tempTask;
            txtbxTitle.Text = tempTask.Title;
            txtbxDesc.Text = tempTask.Desc;
            dtDueDate.SelectedDate = tempTask.DueDate;
        }

        //button to cancel adding/editing task
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Method to confirm task details
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (txtbxTitle.Text != "" && txtbxDesc.Text != "")
            {
                if (dtDueDate.SelectedDate != null)
                {
                    if (MainWindow.Tasks.Contains(TaskToEdit))
                        MainWindow.Tasks.Remove(TaskToEdit);

                    Task Temp = new Task(txtbxTitle.Text, txtbxDesc.Text, cmbxCategory.Text.ToString(), dtDueDate.SelectedDate, cmbxPriority.Text.ToString());
                    MainWindow.Tasks.Add(Temp);
                    this.Close();
                }
                else
                    MessageBox.Show("The Date you entered is invalid");
            }
            else
                MessageBox.Show("Please fill out all textboxes");
            
        }
    }
}
