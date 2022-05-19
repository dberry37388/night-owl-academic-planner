using System;
using System.IO;
using NightOwl.Droid;
using NightOwl.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace NightOwl.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "NightOwl.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}