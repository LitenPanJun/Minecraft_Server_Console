using Microsoft.AspNetCore.Mvc;
using Minecraft_Server_Console.Models;
using System.Diagnostics;

namespace Minecraft_Server_Console.Controllers
{
    public class PanelController : Controller
    {
        private readonly ILogger<PanelController> _logger;

        public PanelController(ILogger<PanelController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
