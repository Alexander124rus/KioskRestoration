using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using KioskRestoration.Model;
using KioskRestoration.View;
using KioskRestoration.ViewModel;

namespace KioskRestoration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Category> Categorys { get; set; }
        
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.DataContext = new IconViewModel();

            //Таймер переключения шаблона
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
         
            MyControl.ContentTemplate = (DataTemplate)this.Resources["startViewTemplate"];
            MyControl.Style = (Style)this.Resources["ContentControlStyleStart"];
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //Вернуться к стартовой странице
            MyControl.ContentTemplate = (DataTemplate)this.Resources["startViewTemplate"];
            MyControl.Style = (Style)this.Resources["ContentControlStyleStart"];
        }

        private void Open_Cliked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "Icon" :
                    DataContext = new IconViewModel();
                    break;
                case "Derava":
                    DataContext = new DerevaViewModel();
                    break;
                case "Izdelie_is_tkany":
                    DataContext = new TkanyViewModel();
                    break;
                case "Izdelie_is_stekla":
                    DataContext = new StekloViewModel();
                    break;

            }

            timer.Interval = TimeSpan.FromSeconds(900);
            timer.Start();
        }

        private void Start_Cliked(object sender, RoutedEventArgs e)
        { 
            MyControl.ContentTemplate = (DataTemplate)this.Resources["iconViewTemplate"];
            MyControl.Style = (Style)this.Resources["ContentControlStyleContent"];
        }

    }
}
