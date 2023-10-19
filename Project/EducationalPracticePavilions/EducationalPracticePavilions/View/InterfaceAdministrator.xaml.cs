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
        private void UpdateMalls()
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
            UpdateMalls();
        }
        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMalls();
        }
        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateMalls();
        }
        private void ComboCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMalls();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Получите выбранный элемент Mall из ListViewMalls
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
                            // Установите IdRole на 4
                            employeeToDelete.IdRole = 4;
                            // Сохраните изменения
                            PavilionsBase.GetContext().SaveChanges();
                            UpdateMalls();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message.ToString()}");
                    }
                }
            }
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
         //   Manager.MainFrame.Navigate(new InterfaceMall(null));
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new InterfaceMall((sender as Button).DataContext as Mall));

        }
    }
}

