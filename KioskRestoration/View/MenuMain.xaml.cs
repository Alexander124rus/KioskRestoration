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
using System.Windows.Shapes;
using KioskRestoration.Model;
using KioskRestoration.ViewModel;

namespace KioskRestoration.View
{
    /// <summary>
    /// Логика взаимодействия для MenuMain.xaml
    /// </summary>
    public partial class MenuMain : UserControl
    {

        public ObservableCollection<Category> Categorys { get; set; }
        public MenuMain()
        {
            InitializeComponent();
            this.DataContext = this;

            DataContext db = new DataContext();
            string sqlExpression = "SELECT * FROM category";
            SQLiteCommand command = new SQLiteCommand(sqlExpression, db.Connect());
            SQLiteDataReader reader = command.ExecuteReader();

            Categorys = new ObservableCollection<Category>();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var nameEn = reader.GetString(1);
                    var nameRu = reader.GetString(2);
                    Category cat = new Category(id, nameEn, nameRu);
                    Categorys.Add(cat);
                }
            }
        }

        private void openListCat(object sender, RoutedEventArgs e)
        {
            string name = "Icon";

            ObjectList vm = new ObjectList();
            vm.Text = name;

            //Obj vm = new Obj();
            //vm.Category_name = name;
        }
    }

}
