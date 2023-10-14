using EducationalPracticePavilions.Model;
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

namespace EducationalPracticePavilions.View
{
    public partial class Authorization : Page
    {
        public static Captcha captcha;
        public Authorization()
        {
            InitializeComponent();
            RoleEmployee.ItemsSource = PavilionsBase.GetContext().Roles.ToList();
            RoleEmployee.SelectedIndex = 0;
        }
        //преобразовать в ref
        private ref int countOfAttempt = 0; // Объявляем счетчик попыток как поле класса
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string role = RoleEmployee.Text;
            string login = LoginBox.Text.ToLower();
            string password = PasswordBox.Password;
            
            if ((login != "") && (password != ""))
            {
                Employee authorizationEmployee = null;
                EmoloyeeAuthorization(authorizationEmployee, role, login, password);
            }
            else
            {
                GoToCaptcha(countOfAttempt);
            }
        }
        private void GoToCaptcha(ref int countOfAttempt)
        {
            MessageBox.Show("Ошибка входа. Пожалуйста, проверьте свои учетные данные.");
            countOfAttempt++;
            if (countOfAttempt >= 3)
            {
                //переход на капчу
                if (captcha == null)
                {
                    captcha = new Captcha();
                    captcha.Show();
                    countOfAttempt = 0;
                    MainWindow.GetWindow(this)?.Close();
                }
                else
                    captcha.Activate();
                countOfAttempt = 0;
            }
        }
        private void EmoloyeeAuthorization(Employee authorizationEmployee, string role, string login, string password)
        {
            authorizationEmployee = PavilionsBase.GetContext().Employees.Where(p => p.LoginEmployee.ToLower() == login
                                                       && p.PasswordEmployee == password).FirstOrDefault();
            if (authorizationEmployee != null) //сделать зависимость от роли пользователя
            {
                MessageBox.Show("Вход выполнен успешно!");
                Manager.MainFrame.Navigate(new EmployeesPage()); //переход на view соответствующей роли
            }
            else
            {
                GoToCaptcha(countOfAttempt);
            }
        }
    }
}
