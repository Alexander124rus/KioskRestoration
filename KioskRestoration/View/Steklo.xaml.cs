using KioskRestoration.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KioskRestoration.View
{
    /// <summary>
    /// Логика взаимодействия для Steklo.xaml
    /// </summary>
    public partial class Steklo : UserControl
    {
        public Steklo()
        {
            InitializeComponent();
            DataContext = new StekloViewModel();
        }
        private void Consultar_Cliked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;

            Exhibit objExhibit = new Exhibit();
            objExhibit.IdExhibit = id;
            objExhibit.Consultar();
            ObjectItem.Content = objExhibit;
        }
    }
    
}
