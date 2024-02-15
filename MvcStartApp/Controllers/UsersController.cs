using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Repositories;

namespace MvcStartApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository repo;

        public UsersController(IBlogRepository repo)
        {
            this.repo = repo;
        }

        [Route("/users")]
        public async Task<IActionResult> Users()
        {
            var users = await repo.GetUsers();
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}