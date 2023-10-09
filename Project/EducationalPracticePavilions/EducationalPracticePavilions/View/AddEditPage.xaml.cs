using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace EducationalPracticePavilions.View
{
    public partial class AddEditPage : Page
    {
        private Employees _currentEmployee = new Employees();
        public AddEditPage(Employees selectedEmployee)
        {
            InitializeComponent();

            if(selectedEmployee != null)
            {
                _currentEmployee= selectedEmployee;
            }
            DataContext = _currentEmployee;
            ComboBoxStatusEmployee.ItemsSource = PavilionsBase.GetContext().StatusEmployee.ToList();
            ComboBoxRoleEmployee.ItemsSource = PavilionsBase.GetContext().Roles.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmployee.Surname))
                errors.AppendLine("Укажите корректно фамилию работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.NameEmployee))
                errors.AppendLine("Укажите корректно имя работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Patronimic))
                errors.AppendLine("Укажите корректно отчество работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.LoginEmployee))
                errors.AppendLine("Укажите корректно логин работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.PasswordEmployee))
                errors.AppendLine("Укажите корректно пароль работника");
            if (_currentEmployee.StatusEmployee == null)
                errors.AppendLine("Корректно выберите статус работника");
            if (_currentEmployee.Roles == null)
                errors.AppendLine("Корректно выберите роль работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.PhoneNumber))
                errors.AppendLine("Укажите корректно номер телефона работника");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Gender))
                errors.AppendLine("Укажите корректно номер телефона работника");
            //if ((_currentEmployee.Gender != "М") || (_currentEmployee.Gender != "Ж"))
            //    errors.AppendLine("Укажите корректно пол работника");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentEmployee.IdEmployee == 0)
                PavilionsBase.GetContext().Employees.Add(_currentEmployee);

            try
            {
                PavilionsBase.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }

        }
    }
}
