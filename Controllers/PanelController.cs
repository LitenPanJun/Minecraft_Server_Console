using Microsoft.AspNetCore.Mvc;
using Minecraft_Server_Console.Models;
using System.Diagnostics;

namespace Minecraft_Server_Console.Controllers
{
    public class PanelController : Controller
    {
        private Server server = new("./App_Data/Server");

        private readonly ILogger<PanelController> _logger;

        public PanelController(ILogger<PanelController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new ConsoleViewModel
            {
                TextAreaContent = "等待服务器电波ing"
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Index(string action, ConsoleViewModel model)
        {
            if (action == "RunServer")
            {
                model.TextAreaContent = "";
                //server.Run("./App_Data/Jre/bin/java.exe", "-jar Server.jar -Xms1G -Xmx16G nogui");
                return View(model);
            }
            if (action == "StopServer")
            {
                //server.Stop();
                return View();
            }
            if (action == "KillServer")
            {
                //server.Kill();
                model.TextAreaContent += "\n服务器已强制关闭";
                return View(model);
            }
            else return View();
        }


    }
}
