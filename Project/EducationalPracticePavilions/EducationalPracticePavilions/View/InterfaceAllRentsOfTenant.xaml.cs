using EducationalPracticePavilions.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для InterfaceAllRentsOfTenant.xaml
    /// </summary>
    /// // Создайте класс для хранения данных аренды

    public partial class InterfaceAllRentsOfTenant : Page
    {
        public InterfaceAllRentsOfTenant()
        {
            InitializeComponent();
            ComboTenant.ItemsSource = PavilionsBase.GetContext().Tenants.ToList();
            int numTenant = ComboTenant.SelectedIndex;
            UpdateTenantRents(numTenant);
        }

        private void UpdateTenantRents(int tenantId)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=WIN-OMJN02Q49QC; Initial Catalog=PavilionsBase; Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTenantRents", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenantId", tenantId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        TenantRentsListView.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }
        private void ComboTenant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboTenant.SelectedIndex >= 0)
            {
                // Получаем выбранный арендатор из ComboBox
                Tenant selectedTenant = ComboTenant.SelectedItem as Tenant;

                if (selectedTenant != null)
                {
                    int tenantId = selectedTenant.IdTenant; // Предполагая, что IdTenant - это идентификатор арендатора
                    UpdateTenantRents(tenantId);
                }
            }
        }


        private void DeleteRent_Click(object sender, RoutedEventArgs e)
        {
            // Добавьте код для удаления выбранной аренды
        }
    }
}
