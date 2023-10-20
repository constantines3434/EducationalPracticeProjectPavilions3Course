using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace EducationalPracticePavilions.View
{
    public partial class InterfaceAdministrator : Page
    {
        public InterfaceAdministrator()
        {
            InitializeComponent();
            ListViewEmployees.ItemsSource = PavilionsBase.GetContext().Employees.ToList();
        }
        private void UpdateEmployees()
        {
            var currentEmployee = PavilionsBase.GetContext().Employees.ToList();
            // Поиск по фамилии
            if (!string.IsNullOrEmpty(TBoxSearch.Text))
            {
                // Отфильтровать список сотрудников по фамилии
                currentEmployee = currentEmployee
                    .Where(p => p.Surname == TBoxSearch.Text)
                    .ToList();
            }

            ListViewEmployees.ItemsSource = currentEmployee;
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateEmployees();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)ListViewEmployees.SelectedItem;

            if (selectedEmployee != null)
            {
                if (MessageBox.Show($"Вы точно хотите удалить выбранного сотрудника?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Employee employeeToDelete = PavilionsBase.GetContext().Employees.Find(selectedEmployee.IdEmployee);

                        if (employeeToDelete != null)
                        {
                            employeeToDelete.IdRole = 4;
                            PavilionsBase.GetContext().SaveChanges();
                            UpdateEmployees();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message.ToString()}");
                    }
                }
            }
        }
        private void HandleDataUpdated(object sender, EventArgs e)
        {
            UpdateEmployees();
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            InterfaceAddEditEmployee addEditEmployee = new InterfaceAddEditEmployee(null);
            addEditEmployee.DataUpdated += HandleDataUpdated; // Подпишитесь на событие
            Manager.MainFrame.Navigate(addEditEmployee);
        }
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)(sender as Button).DataContext;
            InterfaceAddEditEmployee addEditEmployee = new InterfaceAddEditEmployee(selectedEmployee);
            addEditEmployee.DataUpdated += HandleDataUpdated; // Подпишитесь на событие
            Manager.MainFrame.Navigate(addEditEmployee);
        }
    }
}

