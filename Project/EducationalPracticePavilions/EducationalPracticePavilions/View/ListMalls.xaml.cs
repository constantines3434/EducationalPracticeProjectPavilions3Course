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
using System.Data.Entity;

namespace EducationalPracticePavilions.View
{
    /// <summary>
    /// Логика взаимодействия для ListMalls.xaml
    /// </summary>
    public partial class ListMalls : Page
    {
        public ListMalls()
        {
            InitializeComponent();

            //статус тц
            var allStatuses = PavilionsBase.GetContext().StatusMalls.ToList();
            allStatuses.Insert(0, new StatusMall
            {
                StatusMallName = "Все статусы"
            });
            ComboStatus.ItemsSource = allStatuses;
            ComboStatus.SelectedIndex = 0;

            //статус тц
            var allCities = PavilionsBase.GetContext().Cities.ToList();
            allCities.Insert(0, new City
            {
               NameCity  = "Все города"
            });
            ComboCities.ItemsSource = allCities;
            ComboCities.SelectedIndex = 0;

            //число павильонов
            var currentMalls = PavilionsBase.GetContext().Malls
                .Include(m => m.Pavilions)
                .ToList();

            // Изначальная сортировка по городам, затем по статусу
            currentMalls = currentMalls
                .OrderBy(m => m.City.NameCity)
                .ThenBy(m => m.StatusMall.StatusMallName)
                .ToList();

            ListViewMalls.ItemsSource = currentMalls;
        }
        private void UpdateMalls()
        {
            var currentMalls = PavilionsBase.GetContext().Malls
                .Include(m => m.Pavilions)
                .ToList();

            if (ComboStatus.SelectedIndex > 0)
            {
                var selectedStatus = ComboStatus.SelectedItem as StatusMall;
                if (selectedStatus != null)
                    currentMalls = currentMalls.Where(p => p.StatusMall == (ComboStatus.SelectedItem as StatusMall)).ToList();
            }
            if (ComboCities.SelectedIndex > 0)
            {
                var selectedCity = ComboCities.SelectedItem as City;
                if (selectedCity != null)
                {
                    currentMalls = currentMalls
                        .Where(p => p.City == selectedCity && p.StatusMall.StatusMallName != "Удалён")
                        .ToList();
                }
            }
            ListViewMalls.ItemsSource = currentMalls;
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
            Mall selectedMall = (Mall)ListViewMalls.SelectedItem;

            if (selectedMall != null)
            {
                // Удалите выбранный Mall из исходного списка
                PavilionsBase.GetContext().Malls.Remove(selectedMall);
                PavilionsBase.GetContext().SaveChanges();

                // Обновите отображение после удаления
                UpdateMalls();
            }
        }
    }
}