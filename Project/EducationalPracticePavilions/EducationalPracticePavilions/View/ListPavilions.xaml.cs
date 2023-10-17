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
    /// Логика взаимодействия для ListPavilions.xaml
    /// </summary>
    public partial class ListPavilions : Page
    {
        private Mall _currentMall;
        private List<Pavilion> _pavilions;

        public ListPavilions(Mall currentMall)
        {
            InitializeComponent();
            _currentMall = currentMall;
            _pavilions = new List<Pavilion>();
        }

        private void LoadPavilions()
        {
            using (var context = new PavilionsBase())
            {
                // Загружаем павильоны, принадлежащие выбранному ТЦ,
                // и включаем связанные данные
                _pavilions = context.Pavilions
                    .Where(p => p.IdShoppingMall == _currentMall.IdShoppingMall)
                    .Include(p => p.Mall.StatusMall)
                    .Include(p => p.StatusPavilion)
                    .ToList();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadPavilions();
                DGPavilions.ItemsSource = _pavilions;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // Добавление нового павильона
            // ...
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранного павильона
            // ...
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            // Редактирование выбранного павильона
            // ...
        }
    }
}
