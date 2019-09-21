using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Repository
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public TodoContext()
        {
        }

        public DbSet<TodoItemModel> TodoItems { get; set; }
    }

}
