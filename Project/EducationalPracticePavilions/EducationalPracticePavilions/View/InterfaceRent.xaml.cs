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
    /// Логика взаимодействия для InterfaceRent.xaml
    /// </summary>
    public partial class InterfaceRent : Page
    {
        private Rent currentRent = new Rent();
        public InterfaceRent()
        {
            InitializeComponent();
            ComboMallsName.ItemsSource = PavilionsBase.GetContext().Malls.ToList();
            ComboMallsName.SelectionChanged += ComboMallsName_SelectionChanged; // Добавьте обработчик события
            ComboTenant.ItemsSource = PavilionsBase.GetContext().Tenants.ToList();
            ComboStatusRent.ItemsSource = PavilionsBase.GetContext().StatusRents.ToList();
        }

        private void ComboMallsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный Mall
            Mall selectedMall = ComboMallsName.SelectedItem as Mall;

            if (selectedMall != null)
            {
                // Выполните запрос к базе данных для получения Pavilions, относящихся к этому Mall
                List<Pavilion> pavilionsInMall = PavilionsBase.GetContext().Pavilions
                    .Where(p => p.IdShoppingMall == selectedMall.IdShoppingMall)
                    .ToList();
                // Установите pavilionsInMall в качестве источника данных для ComboPavilionsInMall
                ComboPavilionsInMall.ItemsSource = pavilionsInMall;
            }
        }

        private void ButtonСonfirmMall_Click(object sender, RoutedEventArgs e)
        {
            // Обработка выбора Mall
        }

        private void TransitionToThePavilionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход к аренде с выбранным Mall и Pavilion
        }
        /// <summary>
        /// Сохранение аренды
        /// </summary>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            currentRent.StatusRent = ComboStatusRent.SelectedItem as StatusRent;

            StringBuilder errors = new StringBuilder();

            // Проверьте, что элементы ComboMallsName и ComboPavilionsInMall выбраны
            if (ComboMallsName.SelectedItem == null || ComboPavilionsInMall.SelectedItem == null)
            {
                errors.AppendLine("Выберите Торговый центр и Павильон.");
            }

            if (string.IsNullOrWhiteSpace(currentRent.StartOfLease.ToString()))
            {
                errors.AppendLine("Укажите корректно начало аренды.");
            }
            if (string.IsNullOrWhiteSpace(currentRent.EndOfLease.ToString()))
            {
                errors.AppendLine("Укажите корректно конец аренды.");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Получите выбранные элементы ComboMallsName и ComboPavilionsInMall
            Mall selectedMall = ComboMallsName.SelectedItem as Mall;
            Pavilion selectedPavilion = ComboPavilionsInMall.SelectedItem as Pavilion;

            // Получите идентификаторы для вызова хранимой процедуры
            int idShoppingMall = selectedMall.IdShoppingMall;
            int idPavilion = selectedPavilion.IdPavilion;

            // Вызов хранимой процедуры с актуальными параметрами
            try
            {
                PavilionsBase.GetContext().RentOrReservePavilion(idShoppingMall, idPavilion,
                    currentRent.Tenant.IdTenant, currentRent.StartOfLease, currentRent.EndOfLease,
                    currentRent.StatusRent.IdStatusRent);

                Console.WriteLine("Процедура выполнена успешно.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения процедуры: " + ex.Message);
            }
        }

    }

}
