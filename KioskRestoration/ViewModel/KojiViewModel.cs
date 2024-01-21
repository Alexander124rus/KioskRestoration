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
    public class KojiViewModel
    {
        public KojiViewModel()
        {
            DataContext db = new DataContext();
            string sqlExpression = "SELECT * FROM exhibit WHERE category_name='Izdelie_is_koji'";
            SQLiteCommand command = new SQLiteCommand(sqlExpression, db.Connect());
            SQLiteDataReader reader = command.ExecuteReader();

            ObjList = new ObservableCollection<Obj>();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())   // построчно считываем данные
                {
                    var id = reader.GetInt32(0);
                    var category_name = reader.GetString(1);
                    var name = reader.GetString(2);
                    var text = reader.GetString(3);
                    var sourceurl = reader.GetString(4);

                    ImageBrush ElipsImage = new ImageBrush();
                    ElipsImage.ImageSource = new BitmapImage(new Uri(Path.Combine("exhibit\\", category_name, name, sourceurl), UriKind.Relative));

                    //var combined = Path.Combine(@"View\assets\exhibit\", category_name, name, sourceurl);
                    Obj obj = new Obj(id, category_name, name, text, Path.GetFullPath(Convert.ToString(ElipsImage.ImageSource)));
                    ObjList.Add(obj);
                }
            }
        }

        public ObservableCollection<Obj> ObjList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
