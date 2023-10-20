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
            _pavilions = PavilionsBase.GetContext().Pavilions.ToList();

            var allStatuses = PavilionsBase.GetContext().StatusPavilions.ToList();
            allStatuses.Insert(0, new StatusPavilion
            {
                StatusName = "Все статусы"
            });
            ComboStatus.ItemsSource = allStatuses;
            ComboStatus.SelectedIndex = 0;

            List<int> allFloors = new List<int> { 1, 2, 3, 4 };
            allFloors.Insert(0, 0);
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
            if (!string.IsNullOrEmpty(TBoxSearch.Text) && double.TryParse(TBoxSearch.Text, out double searchValue))
            {
                currentPavilion = currentPavilion.Where(p => p.ValueAddedFactor > 0.1 && p.ValueAddedFactor <= searchValue).ToList();
            }
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
                        MessageBox.Show("Нельзя удалить: статус Забронировано или арендован");
                    }
                }
            }
        }
        private void AddPavilion_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InterfacePavilions(null, ref _currentMall));
        }
        private void EditPavilion_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InterfacePavilions((sender as Button).DataContext as Pavilion, ref _currentMall));
        }
    }
}
