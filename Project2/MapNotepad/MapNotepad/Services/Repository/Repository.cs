using MapNotepad.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapNotepad.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;
            
      
        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
           {
               var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepad.db");
               var database = new SQLiteAsyncConnection(path);
               database.CreateTableAsync<UserModel>().Wait();
               return database;

           });
        }
    }
}
