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
            LoadShoppingMallData();
        }

        private void LoadShoppingMallData()
        {
            using (var context = PavilionsBase.GetContext())
            {
                var shoppingMallItems = (from mall in context.ShoppingMalls
                                         select new ShoppingMall
                                         {
                                             IdShoppingMall = mall.IdShoppingMall,
                                             ImageShoppingMall = GetImageFromDatabase(mall.ImageShoppingMall)
                                         }).ToList();

                ListViewPavilions.ItemsSource = shoppingMallItems;
            }
        }

        private BitmapImage GetImageFromDatabase(byte[] imageData)
        {
            // Вставьте код из первого шага сюда
        }
    }
}