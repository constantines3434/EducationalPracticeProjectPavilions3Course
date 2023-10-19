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

            //города
            var allCities = PavilionsBase.GetContext().Cities.ToList();
            allCities.Insert(0, new City
            {
               NameCity  = "Все города"
            });
            ComboCities.ItemsSource = allCities;
            ComboCities.SelectedIndex = 0;

            //число павильонов
            var currentMalls = PavilionsBase.GetContext().Malls.ToList();

            // Изначальная сортировка по городам, затем по статусу
            currentMalls = currentMalls
                .OrderBy(m => m.City.NameCity)
                .ThenBy(m => m.StatusMall.StatusMallName)
                .ToList();

            ListViewMalls.ItemsSource = currentMalls;
        }
        private void UpdateMalls()
        {
            var currentMalls = PavilionsBase.GetContext().Malls.ToList();

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
            // Применяем фильтрацию по коэффициенту добавочной стоимости
            if (!string.IsNullOrEmpty(TBoxSearch.Text) && double.TryParse(TBoxSearch.Text, out double searchValue))
            {
                currentMalls = currentMalls
                    .Where(p => p.ValueAddedFactor == searchValue)
                    .ToList();
            }
            // Изначальная сортировка по городам, затем по статусу
            currentMalls = currentMalls
                .OrderBy(m => m.City.NameCity)
                .ThenBy(m => m.StatusMall.StatusMallName)
                .ToList();

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
                if (MessageBox.Show($"Вы точно хотите удалить выбранный элемент?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Удаление выбранного Mall из исходного списка
                        PavilionsBase.GetContext().Malls.Remove(selectedMall);
                        PavilionsBase.GetContext().SaveChanges();

                        UpdateMalls();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message.ToString()}");
                    }
                }       
            }
        }
        private void HandleDataUpdated(object sender, EventArgs e)
        {
            UpdateMalls();
        }
        private void AddMall_Click(object sender, RoutedEventArgs e)
        {
            InterfaceMall interfaceMall = new InterfaceMall(null);
            interfaceMall.DataUpdated += HandleDataUpdated;
            Manager.MainFrame.Navigate(interfaceMall);
        }

        private void EditMall_Click(object sender, RoutedEventArgs e)
        {
            Mall selectedMall = (Mall)(sender as Button).DataContext;
            InterfaceMall addEditMall = new InterfaceMall(selectedMall);
            addEditMall.DataUpdated += HandleDataUpdated; // Подпишитесь на событие
            Manager.MainFrame.Navigate(addEditMall); ;   

        }
    }
}