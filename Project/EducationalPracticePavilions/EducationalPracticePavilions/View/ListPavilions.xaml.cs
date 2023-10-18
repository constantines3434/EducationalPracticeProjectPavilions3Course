using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EducationalPracticePavilions.View
{
    public partial class ListPavilions : Page
    {
        private List<Pavilion> _pavilions;
        private Mall _currentMall = null;

        public ListPavilions(Mall selectedMall)
        {
            InitializeComponent();
            if (selectedMall != null)
                _currentMall = selectedMall;
            // Загрузка всех павильонов
            _pavilions = PavilionsBase.GetContext().Pavilions.ToList();

            // Загрузка статусов ТЦ в ComboStatus
            var allStatuses = PavilionsBase.GetContext().StatusPavilions.ToList();
            allStatuses.Insert(0, new StatusPavilion
            {
                StatusName = "Все статусы"
            });
            ComboStatus.ItemsSource = allStatuses;
            ComboStatus.SelectedIndex = 0;

            // Загрузка этажей в ComboFloor
            List<int> allFloors = new List<int> { 1, 2, 3, 4 /* Добавьте сколько угодно значений */ };
            allFloors.Insert(0, 0); // Добавьте "Все этажи" в начало списка
            ComboFloor.ItemsSource = allFloors;
            ComboFloor.SelectedIndex = 0;

            
            _currentMall = selectedMall;
            UpdateMalls();
        }

        private void UpdateMalls()
        {
            var currentPavilion = _pavilions.Where(p => p.IdShoppingMall == _currentMall.IdShoppingMall).ToList();

            if (ComboFloor.SelectedIndex > 0)
            {
                currentPavilion = currentPavilion.Where(p => p.FloorPavilion == (int)ComboFloor.SelectedItem).ToList();
            }

            // Применяем фильтрацию по коэффициенту добавочной стоимости
            if (!string.IsNullOrEmpty(TBoxSearch.Text) && double.TryParse(TBoxSearch.Text, out double searchValue))
            {
                currentPavilion = currentPavilion.Where(p => p.ValueAddedFactor > 0.1 && p.ValueAddedFactor <= searchValue).ToList();
            }

            // Применяем фильтрацию по статусу Павильона
            var selectedStatus = ComboStatus.SelectedItem as StatusPavilion;
            if (selectedStatus != null && selectedStatus.StatusName != "Все статусы")
            {
                currentPavilion = currentPavilion.Where(p => p.StatusPavilion.StatusName == selectedStatus.StatusName).ToList();
            }

            ListViewPavilions.ItemsSource = currentPavilion;
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMalls();
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMalls();
        }

        private void ComboFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMalls();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Pavilion selectedPavilion = (Pavilion)ListViewPavilions.SelectedItem;

            if (selectedPavilion != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить выбранный элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        PavilionsBase.GetContext().Pavilions.Remove(selectedPavilion);
                        PavilionsBase.GetContext().SaveChanges();

                        _pavilions.Remove(selectedPavilion);
                        UpdateMalls();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        //переименовать
        private void AddMall_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InterfaceMall(null));
        }

        private void EditMall_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InterfaceMall((sender as Button).DataContext as Mall));
        }
    }
}
