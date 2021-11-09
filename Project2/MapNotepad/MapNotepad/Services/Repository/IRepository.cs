using MapNotepad.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services.Repository
{
    public interface IRepository
    {
        Task<int> InsertAsync<T>(T Entity) where T : IEntityBase, new();
        Task<int> UpdateAsync<T>(T Entity) where T : IEntityBase, new();
        Task<int> DeleteAsync<T>(T Entity) where T : IEntityBase, new();
        Task<List<T>> GetAllItemsAsync<T>() where T : IEntityBase, new();

    }
}
