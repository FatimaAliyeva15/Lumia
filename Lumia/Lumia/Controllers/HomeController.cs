
using Lumia_Business.Services.Abstracts;
using Lumia_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _service;

        public HomeController(ITeamService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<Team> teams = _service.GetAllTeams();
            return View(teams);
        }
    }
}
