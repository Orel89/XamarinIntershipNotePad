using MapNotepad.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;
            
      
        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
           {
               var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepad.db2");
               var database = new SQLiteAsyncConnection(path);
               database.CreateTableAsync<UserModel>().Wait();
               database.CreateTableAsync<PinModel>().Wait();
               database.CreateTableAsync<PhotoPinModel>().Wait();
               database.CreateTableAsync<EventModel>().Wait();
               return database;

           });
        }


        #region ---common methods---
        public Task<int> DeleteAsync<T>(T Entity) where T : IEntityBase, new()
        {
            return _database.Value.DeleteAsync(Entity);
        }

        public Task<List<T>> GetAllPinsAsync<T>(T Entity) where T : IEntityBase, new()
        {
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T Entity) where T : IEntityBase, new()
        {
            return _database.Value.InsertAsync(Entity);
        }

        public Task<int> UpdateAsync<T>(T Entity) where T : IEntityBase, new()
        {
            return _database.Value.UpdateAsync(Entity);
        }
        #endregion

    }
}
