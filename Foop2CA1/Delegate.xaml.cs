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
    /// Interaction logic for Delegate.xaml
    /// </summary>
    public partial class Delegate : Window
    {
        public Task TaskToBeDelegated = new Task();
        public Delegate()
        {
            InitializeComponent();
            lstbxUsers.ItemsSource = MainWindow.Users;
        }
        public Delegate(Task tempTask) : this()
        {
            TaskToBeDelegated = tempTask;
        }
        //Method to cancel the delegation
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Method to confirm the delegation
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(lstbxUsers.SelectedItem != null)
            {
                User temp = lstbxUsers.SelectedItem as User;
                MainWindow.Tasks.Remove(TaskToBeDelegated);
                TaskToBeDelegated.Responsibility = temp;
                MainWindow.Tasks.Add(TaskToBeDelegated);
                this.Close();
            }
        }
    }
}
