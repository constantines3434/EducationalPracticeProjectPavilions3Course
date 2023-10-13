using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для ShoppingMallPage.xaml
    /// </summary>
    public partial class ShoppingMallPage : Page
    {
        public ShoppingMallPage()
        {
            InitializeComponent();
            
            var allStatuses = PavilionsBase.GetContext().StatusMalls.ToList();
            allStatuses.Insert(0, new StatusMall
            {
                StatusMallName = "Все статусы"
            });
            ComboStatus.ItemsSource = allStatuses;

            CheckActual.IsChecked= true; 
            ComboStatus.SelectedIndex = 0;

            var currentMalls = PavilionsBase.GetContext().Malls.ToList();
            ListViewPavilions.ItemsSource = currentMalls;
            UpdateMalls();
        }
        //ошибка
        private void UpdateMalls()
        {
            var currentMalls = PavilionsBase.GetContext().Malls.ToList();
            if (ComboStatus.SelectedIndex > 0)
            {
                var selectedStatus = ComboStatus.SelectedItem as StatusMall;
                if (selectedStatus != null)
                    currentMalls = currentMalls.Where(p => p.StatusMall == (ComboStatus.SelectedItem as StatusMall)).ToList();
                currentMalls = currentMalls.Where(p=>p.NameMalls.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }

            //if(CheckActual.IsChecked.Value)
            //    currentMalls = currentMalls.Where(p=>p.StatusMall.StatusMallName).ToList();
                        
            ListViewPavilions.ItemsSource= currentMalls.OrderBy(p=>p.Price).ToList(); //сортировка по свойству
                
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
    }
}