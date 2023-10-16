using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using Microsoft.Win32;

namespace EducationalPracticePavilions.View
{
    /// <summary>
    /// Логика взаимодействия для AddImage.xaml
    /// </summary>
    public partial class AddImage : Window
    {
        private string con = @"Data Source=WIN-OMJN02Q49QC; Initial Catalog=TestBase; Integrated Security=True";
        public AddImage()
        {
            InitializeComponent();
        }
        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            if (Image1.Source != null)
            {
                var uploader = new ImageUploader(con);
                uploader.Upload(Image1);
            }
            else
            {
                // Обработка случая, когда изображение не выбрано
            }
        }
        private void ViewPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(IdPhotoTextBox.Text) && int.TryParse(IdPhotoTextBox.Text, out int id))
            {
                
                var retriever = new ImageRetriever(con);
                retriever.Retrieve(Image2, id);
              //  MessageBox.Show("работает ViewPhoto");
            }
            else
            {
                // Обработка случая, когда ID не введен или введен некорректно
                MessageBox.Show("Пожалуйста, введите корректный ID изображения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
    class ImageUploader
    {
        private readonly string _connectionString;

        public ImageUploader(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Upload(System.Windows.Controls.Image image)
        {
            if (image.Source is BitmapImage bitmapImage)
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Images (ImageName) VALUES (@image)";

                    using (var stream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        encoder.Save(stream);
                        stream.Position = 0;

                        var sqlParameter = new SqlParameter("@image", SqlDbType.VarBinary, (int)stream.Length)
                        {
                            Value = stream.ToArray()
                        };
                        command.Parameters.Add(sqlParameter);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Работает Upload");
                }
            }
        }
    }
    public class ImageRetriever
    {
        private readonly string _connectionString;

        public ImageRetriever(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Retrieve(System.Windows.Controls.Image image, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT ImageName FROM Images WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var imageData = (byte[])reader["ImageName"];
                        using (var stream = new MemoryStream(imageData))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = stream;
                            bitmapImage.EndInit();

                            // Приводим image к правильному типу
                            var wpfImage = image as System.Windows.Controls.Image;
                            if (wpfImage != null)
                            {
                                wpfImage.Source = bitmapImage;
                            }
                            MessageBox.Show("Показ изображения из Базы данных");

                        }
                    }
                }
            }
        }
    }
}
