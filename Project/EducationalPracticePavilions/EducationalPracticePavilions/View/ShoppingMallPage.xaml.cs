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
            
            var allStatuses = PavilionsBase.GetContext().StatusShoppingMalls.ToList();
            allStatuses.Insert(0, new StatusShoppingMalls
            {
                StatusTicketName = "Все статусы"
            });
            ComboStatus.ItemsSource = allStatuses;

            CheckActual.IsChecked= true; 
            ComboStatus.SelectedIndex = 0;

            var currentMall = PavilionsBase.GetContext().ShoppingMalls.ToList();
            ListViewPavilions.ItemsSource = currentMall;
            
        }
        //ошибка
        private void UpdateMalls()
        {
            var currentMall = PavilionsBase.GetContext().ShoppingMalls.ToList();
            if (ComboStatus.SelectedIndex > 0)
                currentMall = currentMall.Where(p => p.StatusShoppingMalls == (ComboStatus.SelectedItem as StatusShoppingMalls)).ToList();
            
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}