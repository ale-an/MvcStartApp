using System.Threading.Tasks;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Repositories
{
    public interface IRequestRepository
    {
        Task AddRequest(string url);
        Task<Request[]> GetRequests();
    }
}