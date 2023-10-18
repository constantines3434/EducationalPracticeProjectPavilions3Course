using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
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
    /// <summary>
    /// Логика взаимодействия для InterfacePavilions.xaml
    /// </summary>
    public partial class InterfacePavilions : Page
    {
        private Pavilion _currentPavilion = new Pavilion();
        public InterfacePavilions(Pavilion selectedPavilion, ref Mall selectedMall)
        {
            InitializeComponent();

            if (selectedPavilion != null)
                _currentPavilion = selectedPavilion;
            else
                _currentPavilion.Mall = selectedMall; // Устанавливаем Mall для _currentPavilion

            DataContext = _currentPavilion;
            ComboStatus.ItemsSource = PavilionsBase.GetContext().StatusPavilions.ToList();
        }


        private void TransitionToThePavilionsButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Присвоение значения статуса
            _currentPavilion.StatusPavilion = ComboStatus.SelectedItem as StatusPavilion;

            if (_currentPavilion.FloorPavilion <= 0)
                errors.AppendLine("Укажите корректный этаж");
            if (string.IsNullOrWhiteSpace(_currentPavilion.NamePavilions))
                errors.AppendLine("Укажите корректное название Павильона");
            if (_currentPavilion.SquarePavilions <= 0)
                errors.AppendLine("Укажите корректн площадь");
            if (_currentPavilion.ValueAddedFactor < 0.1)
                errors.AppendLine("Укажите корректный коэффициент добавочной стоимости ТЦ");
            if (_currentPavilion.CostPerMeter <= 0)
                errors.AppendLine("Укажите корректные затраты на строительство торгового центра");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавление павильона
            if (_currentPavilion.IdPavilion == 0)
                PavilionsBase.GetContext().Pavilions.Add(_currentPavilion);
                //проверка статуса на Забронировано или Арендовано
                try
                {
                    int MaxPavilionsCount = _currentPavilion.Mall.PavilionCount.Value;
                    
                    PavilionsBase.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");




                    // Call the stored procedure to add the Pavilion
                    //PavilionsBase.GetContext().AddPavilionToMall(
                    //    _currentPavilion.IdShoppingMall,
                    //    _currentPavilion.NamePavilions,
                    //    _currentPavilion.FloorPavilion,
                    //    _currentPavilion.IdStatusPavilion,
                    //    _currentPavilion.SquarePavilions,
                    //    _currentPavilion.CostPerMeter,
                    //    MaxPavilionsCount, // You may need to adjust this parameter as needed
                    //    _currentPavilion.ValueAddedFactor
                    //);

                    // The stored procedure will handle adding the Pavilion to the database
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
