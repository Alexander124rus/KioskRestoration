using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;

namespace KioskRestoration.Model
{
    public class Obj:INotifyPropertyChanged
    {
        public int id;
        public string category_name;
        public string name;
        public string text;
        public string sourceurl;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Category_name
        {
            get { return category_name; }
            set
            {
                category_name = value;
                OnPropertyChanged("Category_name");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public string SourceUrl
        {
            get { return sourceurl; }
            set
            {
                sourceurl = value;
                OnPropertyChanged("SourceUrl");
            }
        }
        public Obj() { }

        public Obj(int id, string category_name, string name, string text, string sourceurl)
        {
            Id = id;
            Category_name = category_name;
            Name = name;
            Text = text;
            SourceUrl = sourceurl;
        }
        //public Obj(int id, string category_name, string name)
        //{
        //    Id = id;
        //    Category_name = category_name;
        //    Name = name;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
