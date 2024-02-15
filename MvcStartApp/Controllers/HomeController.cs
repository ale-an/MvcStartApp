using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models;
using MvcStartApp.Models.Db;
using MvcStartApp.Repositories;

namespace MvcStartApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository repo;

        public HomeController(IBlogRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andrey",
                LastName = "Petrov",
                JoinDate = DateTime.Now
            };

            await repo.AddUser(newUser);

            Console.WriteLine(
                $"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}