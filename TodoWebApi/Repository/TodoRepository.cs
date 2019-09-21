using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApi.Models;

namespace TodoWebApi.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IList<TodoItemModel>> GetAllAsync()
        {
            var result = await _context.TodoItems.ToListAsync();
            return result;
        }

        public async Task<TodoItemModel> GetByIdAsync(int id)
        {
            var result = await _context.TodoItems.FindAsync(id);
            return result;
        }

        public async Task<int> CreateAsync(TodoItemModel model)
        {
            var res = await _context.TodoItems.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> EditAsync(int id, TodoItemModel model)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.Complete = model.Complete;
                item.Title = model.Title;
                item.Priority = model.Priority;

                _context.Entry(item).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //TodoItemModel itemToDelete = new TodoItemModel() { Id= id };
            //_context.Entry(itemToDelete).State = EntityState.Deleted;
            // or
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAllAsync()
        {
            // warning - this may be slow for large dataset
            _context.TodoItems.RemoveRange(_context.TodoItems);
            var result = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteAsync(int id, bool status)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.Complete = status;
                _context.Entry(item).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
