using KioskRestoration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using KioskRestoration.ViewModel;
using System.Runtime.CompilerServices;

namespace KioskRestoration.View
{
    /// <summary>
    /// Логика взаимодействия для Icon.xaml
    /// </summary>
    public partial class Icon : UserControl
    {
        
        public Icon()
        {
            InitializeComponent();
            DataContext = new IconViewModel();
        }

        private void Consultar_Cliked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;

            Exhibit objExhibit = new Exhibit();
            objExhibit.IdExhibit = id;
            objExhibit.Consultar();
            Icons.Content = objExhibit;
            //Button btn = (Button)sender;
            //new ItemObj(btn);
            //DataContext = new ItemObjViewModel(btn);
        }
    }
    

}
