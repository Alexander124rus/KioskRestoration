using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KioskRestoration.Model
{
    public class Category: INotifyPropertyChanged
    {
        public int id;
        public string nameEn;
        public string nameRu;
        //public int Id { get; set; }
        //public string NameEn { get; set; }
        //public string NameRu { get; set; }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string NameEn
        {
            get { return nameEn; }
            set
            {
                nameEn = value;
                OnPropertyChanged("NameEn");
            }
        }
        public string NameRu
        {
            get { return nameRu; }
            set
            {
                nameRu = value;
                OnPropertyChanged("NameRu");
            }
        }


        public Category(int id, string nameEn, string nameRu)
        {
            Id = id;
            NameEn = nameEn;
            NameRu = nameRu;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
