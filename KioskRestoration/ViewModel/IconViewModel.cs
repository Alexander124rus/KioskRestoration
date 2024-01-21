using KioskRestoration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace KioskRestoration.ViewModel
{
    public class IconViewModel : INotifyPropertyChanged
    {
        
        public IconViewModel()
        {
            DataContext db = new DataContext();
            string sqlExpression = "SELECT * FROM exhibit WHERE category_name='Icon'";           
            SQLiteCommand command = new SQLiteCommand(sqlExpression, db.Connect());
            SQLiteDataReader reader = command.ExecuteReader();

            Icon = new ObservableCollection<Obj>();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var category_name = reader.GetString(1);
                    var name = reader.GetString(2);
                    var text = reader.GetString(3);
                    var sourceurl = reader.GetString(4);

                    string com = Path.Combine("exhibit\\", category_name, name, sourceurl);
                    Uri uriAddress = new Uri(com, UriKind.Relative);

                    ImageBrush ElipsImage = new ImageBrush();
                    ElipsImage.ImageSource = new BitmapImage(new Uri(Path.Combine("exhibit\\", category_name, name, sourceurl), UriKind.Relative));
                    //var com2 = new ImageSourceConverter().ConvertFromString(uriAddress.ToString());
                    //Uri uriAddress = new Uri("/KioskRestoration/View/assets/exhibit/"+ reader.GetString(1)+"/"+ reader.GetString(2), UriKind.Relative);

                    //var combined = Path.Combine(@"C:\Users\admin\Desktop\KioskRestoration\KioskRestoration\View\assets\exhibit\", category_name, name, sourceurl);
                    Obj obj = new Obj(id, category_name, name, text, Path.GetFullPath(Convert.ToString(ElipsImage.ImageSource)) );
                    Icon.Add(obj);
                }
            }
        }

        public ObservableCollection<Obj> Icon { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}   
