﻿using KioskRestoration.ViewModel;
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
    /// Логика взаимодействия для Koji.xaml
    /// </summary>
    public partial class Koji : UserControl
    {
        public Koji()
        {
            InitializeComponent();
            DataContext = new KojiViewModel();
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
