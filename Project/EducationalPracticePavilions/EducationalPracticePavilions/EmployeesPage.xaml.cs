using EducationalPracticePavilions.Model;
using EducationalPracticePavilions.View;
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

namespace EducationalPracticePavilions
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {

        public EmployeesPage()
        {
            InitializeComponent();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var employeeForRemoving = DGEmployees.SelectedItems.Cast<Employees>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {employeeForRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                { 
                    PavilionsBase.GetContext().Employees.RemoveRange(employeeForRemoving);
                    PavilionsBase.GetContext().SaveChanges();
                    MessageBox.Show("Элементы удалены");
                    DGEmployees.ItemsSource = PavilionsBase.GetContext().Employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message.ToString()}");
                }
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Employees));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                PavilionsBase.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGEmployees.ItemsSource = PavilionsBase.GetContext().Employees.ToList();
            }
        }
    }
}
