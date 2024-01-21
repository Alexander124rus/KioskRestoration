using KioskRestoration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Markup;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
//using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Controls.Primitives;
using System.Reflection.Metadata;

namespace KioskRestoration.ViewModel
{
    public class ObjectListViewModel : INotifyPropertyChanged
    {
        public string p;
        public string P
        {
            get { return p; }
            set
            {
                p = value;
                OnPropertyChanged("P");
            }
        }
        
        public ObjectListViewModel()
        {
            DataContext db = new DataContext();
            string sqlExpression = "SELECT * FROM exhibit WHERE category_name='" + "Icon" + "'";
            //string sqlExpression = "SELECT * FROM exhibit";


            SQLiteCommand command = new SQLiteCommand(sqlExpression, db.Connect());
            SQLiteDataReader reader = command.ExecuteReader();

            Objs = new ObservableCollection<Obj>();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var category_name = reader.GetString(1);
                    var name = reader.GetString(2);
                    var text = reader.GetString(3);
                    var sourceurl = reader.GetString(4);
                    //var url = @"~\..\Resources\Icon\PokrovBogomatery\PokrovBogomatery1.jpg";
                    //var path = @"/View/";
                    
                    //sourceDirectory = @"H:\KioskRestoration\KioskRestoration\View\assets\exhibit\";
                    //sourceDirectory = Path.GetFullPath("Resources");
                    //sourceDirectory = new Uri("/Resources/Icon/PokrovBogomatery/PokrovBogomatery1.jpg");

                    //var combined = Path.Combine(@"H:\KioskRestoration\KioskRestoration\View\assets\exhibit\", category_name, name, sourceurl);
                    //Obj obj = new Obj(id, category_name, name, text, combined);
                    //Objs.Add(obj);
                }
            }
        }

        public ObservableCollection<Obj> Objs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    
}
