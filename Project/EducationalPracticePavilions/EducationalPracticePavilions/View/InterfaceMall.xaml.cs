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
    /// <summary>
    /// Логика взаимодействия для InterfaceMall.xaml
    /// </summary>
    public partial class InterfaceMall : Page
    {
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
                PavilionsBase.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
             //   Manager.MainFrame.GoBack();
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
