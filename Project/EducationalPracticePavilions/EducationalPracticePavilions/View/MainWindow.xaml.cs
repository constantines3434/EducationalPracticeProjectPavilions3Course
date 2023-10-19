﻿using EducationalPracticePavilions.Model;
using EducationalPracticePavilions.View;
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

namespace EducationalPracticePavilions
{   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            //MainFrame.Navigate(new Authorization());
            //MainFrame.Navigate(new EmployeesPage());
            //MainFrame.Navigate(new InterfaceMall(null));
            //MainFrame.Navigate(new ListMalls());
            // MainFrame.Navigate(new ListPavilions());
            //MainFrame.Navigate(new InterfaceRent());
            //MainFrame.Navigate(new InterfaceAdministrator());
            //MainFrame.Navigate(new InterfaceAddEditEmployee());
            MainFrame.Navigate(new InterfaceAllRentsOfTenant());        
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                ButtonBackPage.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonBackPage.Visibility = Visibility.Hidden;
            }
        }
    }
}
