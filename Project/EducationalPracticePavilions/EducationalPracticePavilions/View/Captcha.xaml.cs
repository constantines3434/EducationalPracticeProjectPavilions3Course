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
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public static MainWindow mainWindow;
        public Captcha()
        {
            InitializeComponent();
            CaptchaTextLoad();
        }

        private void CaptchaTextLoad()
        {
            Random random = new Random();
            int num = random.Next(6, 8);
            string cap = "";
            int totl = 0;
            do
            {
                int chr = random.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    cap += (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                }
            } while (true);
            CaptchaBlock.Text = cap;
        }

        private void CaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTextBox.Text == CaptchaBlock.Text)
            {
                if (CaptchaFrame != null)
                {
                    if (mainWindow == null)
                    {
                        mainWindow = new MainWindow();
                        mainWindow.Show();
                        Captcha.GetWindow(this)?.Close();
                    }
                    else
                        mainWindow.Activate();
                }
                else
                {
                    MessageBox.Show("Ошибка: CaptchaFrame не настроен.");
                }
            }
            else
            {
                MessageBox.Show("Неправильная CAPTCHA");
                CaptchaTextBox.Text = "";
                CaptchaTextLoad();
            }
        }
    }
}
