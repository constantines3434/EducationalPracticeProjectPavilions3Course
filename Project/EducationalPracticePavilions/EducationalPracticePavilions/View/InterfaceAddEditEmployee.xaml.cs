using EducationalPracticePavilions.Model;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace EducationalPracticePavilions.View
{
    /// <summary>
    /// Логика взаимодействия для InterfaceAddEditEmployee.xaml
    /// </summary>
    public partial class InterfaceAddEditEmployee : Page
    {
        private string con = @"Data Source=WIN-OMJN02Q49QC; Initial Catalog=PavilionsBase; Integrated Security=True";

        private Employee _currentEmployee = new Employee();
        public InterfaceAddEditEmployee(Employee selectedEmployee)
        {
            InitializeComponent();
            if (selectedEmployee != null)
                _currentEmployee = selectedEmployee;

            DataContext = _currentEmployee;
            //ComboRole.ItemsSource = PavilionsBase.GetContext().Employees.ToList();
            //ComboSex.ItemsSource = PavilionsBase.GetContext().Employees.ToList();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Присвоение значения Роли
            _currentEmployee.Role = ComboRole.SelectedItem as Role;
            // Присвоение значения статуса
            _currentEmployee.Gender = ComboSex.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(_currentEmployee.Surname))
                errors.AppendLine("Укажите корректное Фамилию");
            if (string.IsNullOrWhiteSpace(_currentEmployee.NameEmployee))
                errors.AppendLine("Укажите корректное Имя");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Patronimic))
                errors.AppendLine("Укажите корректное Отчество");
            if (string.IsNullOrWhiteSpace(_currentEmployee.PasswordEmployee))
                errors.AppendLine("Укажите корректное Пароль");
            if (string.IsNullOrWhiteSpace(_currentEmployee.PhoneNumber))
                errors.AppendLine("Укажите корректное Номер телефона");
            if (_currentEmployee.Role == null)
                errors.AppendLine("Корректно выберите роль сотрудника");
            if (_currentEmployee.Gender == null)
                errors.AppendLine("Корректно выберите пол сотрудника");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавление
            if (_currentEmployee.IdEmployee == 0)
                PavilionsBase.GetContext().Employees.Add(_currentEmployee);
            try
            {
                // Попробуйте сохранить изменения
                PavilionsBase.GetContext().SaveChanges();

                // Теперь вызовите метод Upload с идентификатором магазина
                var uploader = new ImageUploader(con);
                uploader.Upload(Image1, _currentEmployee.IdEmployee);

                MessageBox.Show("Информация сохранена");
                //   Manager.MainFrame.GoBack();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("Запрещено изменять статус ТЦ на \"план\", так как есть забронированные павильоны."))
                    {
                        MessageBox.Show("Запрещено изменять статус ТЦ на 'план', так как есть забронированные павильоны.");
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при сохранении изменений.");
                    }
                }
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

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();

                Image1.Source = bitmap;

                // Освобождаем ресурсы OpenFileDialog после использования
                openFileDialog = null;
            }
        }
        private void TransitionToThePavilionsButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ListPavilions((sender as Button).DataContext as Mall));
        }
    }
    class ImageUploaderEmployee
    {
        private readonly string _connectionString;
        public ImageUploaderEmployee(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Загрузка изображения в БД
        /// </summary>
        public void Upload(System.Windows.Controls.Image image, int Id)
        {
            if (image.Source is BitmapImage bitmapImage)
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Employees SET Photo = @image WHERE IdEmployee = @Id";
                    using (var stream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        encoder.Save(stream);
                        stream.Position = 0;

                        var sqlParameterId = new SqlParameter("@Id", SqlDbType.Int)
                        {
                            Value = Id
                        };
                        command.Parameters.Add(sqlParameterId);

                        var sqlParameterImage = new SqlParameter("@image", SqlDbType.VarBinary, (int)stream.Length)
                        {
                            Value = stream.ToArray()
                        };
                        command.Parameters.Add(sqlParameterImage);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Работает Upload");
                }
            }
        }
    }
}
