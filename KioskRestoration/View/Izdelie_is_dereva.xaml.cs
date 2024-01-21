using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Izdelie_is_dereva.xaml
    /// </summary>
    public partial class Izdelie_is_dereva : UserControl
    {
        public Izdelie_is_dereva()
        {
            InitializeComponent();
        }

        private void Consultar_Cliked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;

            Exhibit objExhibit = new Exhibit();
            objExhibit.IdExhibit = id;
            objExhibit.Consultar();
            Derevo.Content = objExhibit;
            //Button btn = (Button)sender;
            //new ItemObj(btn);
            //DataContext = new ItemObjViewModel(btn);
        }
    }
}
