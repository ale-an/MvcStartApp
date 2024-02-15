using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext context;

        public RequestRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task AddRequest(string url)
        {
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Url = url
            };

            var entry = context.Entry(request);
            if (entry.State == EntityState.Detached)
                await context.Requests.AddAsync(request);

            await context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            return await context.Requests.ToArrayAsync();
        }
    }
}