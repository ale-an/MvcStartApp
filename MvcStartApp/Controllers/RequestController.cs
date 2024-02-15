using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Repositories;

namespace MvcStartApp.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestRepository repo;

        public RequestController(IRequestRepository repo)
        {
            this.repo = repo;
        }

        [Route("logs")]
        public async Task<IActionResult> Logs()
        {
            var requests = await repo.GetRequests();
            return View(requests);
        }
    }
}