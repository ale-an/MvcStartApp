using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext context;

        public BlogRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            var entry = context.Entry(user);
            if (entry.State == EntityState.Detached)
                await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
            return await context.Users.ToArrayAsync();
        }
    }
}