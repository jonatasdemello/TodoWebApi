using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApi.Models;

namespace TodoWebApi.Repository
{
    public interface ITodoRepository
    {
        Task<IList<TodoItemModel>> GetAllAsync();
        Task<TodoItemModel> GetByIdAsync(int id);
        Task<int> CreateAsync(TodoItemModel model);
        Task<bool> EditAsync(int id, TodoItemModel model);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllAsync();
        Task<bool> CompleteAsync(int id, bool status);
    }
}
