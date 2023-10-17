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
    /// Логика взаимодействия для InterfaceMall.xaml
    /// </summary>
    public partial class InterfaceMall : Page
    {
        private string con = @"Data Source=WIN-OMJN02Q49QC; Initial Catalog=PavilionsBase; Integrated Security=True";

        private Mall _currentMall = new Mall();
        public InterfaceMall(Mall selectedMall)
        {
            InitializeComponent();
            if (selectedMall != null)
                _currentMall = selectedMall;

            DataContext = _currentMall;

            //статусы ТЦ
            var statusMalls = PavilionsBase.GetContext().StatusMalls.ToList();
            var statusToRemove = statusMalls.SingleOrDefault(s => s.StatusMallName == "Удалён");
            if (statusToRemove != null)
                statusMalls.Remove(statusToRemove);

            ComboStatus.ItemsSource = statusMalls;

            ComboCities.ItemsSource = PavilionsBase.GetContext().Cities.ToList();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Присвоение значения статуса
            _currentMall.StatusMall = ComboStatus.SelectedItem as StatusMall;
            // Присвоение значения статуса
            _currentMall.City = ComboCities.SelectedItem as City;

            if (string.IsNullOrWhiteSpace(_currentMall.NameMalls))
                errors.AppendLine("Укажите корректное название ТЦ");
            if (_currentMall.ValueAddedFactor <= 0)
                errors.AppendLine("Укажите корректный коэффициент добавочной стоимости ТЦ");
            if (_currentMall.StatusMall == null)
                errors.AppendLine("Корректно выберите статус");
            if (_currentMall.Price <= 0)
                errors.AppendLine("Укажите корректные затраты на строительство торгового центра");
            if (_currentMall.City == null)
                errors.AppendLine("Корректно выберите город");
            if (_currentMall.NumberOfStoreys <= 0)
                errors.AppendLine("Укажите корректное количество этажей в ТЦ");
            if (_currentMall.PavilionCount < 0)
                errors.AppendLine("Укажите корректное количество павильонов в ТЦ");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMall.IdShoppingMall == 0)
                PavilionsBase.GetContext().Malls.Add(_currentMall);
            try
            {
                // Попробуйте сохранить изменения
                PavilionsBase.GetContext().SaveChanges();

                // Теперь вызовите метод Upload с идентификатором магазина
                var uploader = new ImageUploader(con);
                uploader.Upload(Image1, _currentMall.IdShoppingMall);

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
            Manager.MainFrame.Navigate(new ListPavilions(_currentMall));
        }
    }
    class ImageUploader
    {
        private readonly string _connectionString;

        public ImageUploader(string connectionString)
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
                    // Вставка изображения в базу данных для конкретной записи Malls (указываем IdShoppingMall)
                    command.CommandText = "UPDATE Malls SET ImageShoppingMall = @image WHERE IdShoppingMall = @Id";
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