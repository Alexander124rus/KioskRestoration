using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace KioskRestoration.Model
{
    internal class DataContext
    {
        public SQLiteConnection Connect()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = KioskRestoration.db");
            connection.Open();
            return connection;
        }
    }
}
