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
using System.Reflection.Metadata;
using System.Data.Common;
using System.Data;
using KioskRestoration.View;

namespace KioskRestoration.ViewModel
{
    public class MenuViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Category> Categorys { get; set; }

        //RelayCommand? openCommand;

        public MenuViewModel()
        {
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
                    
                    //s.parameter = "Icon";
                }
            }

            //ObjectListViewModel s = new ObjectListViewModel();
            //s.P = "Icon";


        }

        //public RelayCommand OpenCommand
        //{
        //    get
        //    {
        //        return openCommand ??
        //          (openCommand = new RelayCommand((o) =>
        //          {
        //              UserWindow userWindow = new UserWindow(new User());
        //              if (userWindow.ShowDialog() == true)
        //              {
        //                  User user = userWindow.User;
        //                  db.Users.Add(user);
        //                  db.SaveChanges();
        //              }
        //          }));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
