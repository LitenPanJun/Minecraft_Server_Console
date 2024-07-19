using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Minecraft_Server_Console.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Minecraft_Server_Console.Controllers
{
    public class AccountController : Controller
    {
        private List<AccountViewModel> users = [];
        public IActionResult Register()
        {
            if (System.IO.File.Exists("./User.json") != true)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Register(AccountViewModel user)
        {
            // 加密密码  
            user.Password = EncryptPassword(user.Password);
            users.Add(user);
            SaveUsers(users);
            return RedirectToAction("Signin");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountViewModel user)
        {
            users = GetUsers();
            var password = EncryptPassword(user.Password);
            var user2 = users.FirstOrDefault(u => u.Username == user.Username);
            if (user2 != null && password == user2.Password && user.Password != null && user.Password != "")
            {
                return RedirectToAction("Index", "Panel");
            }
            ModelState.AddModelError("", "用户名或密码错误。");
            return View(user);
        }
        private string EncryptPassword(string password)
        {
            // 使用SHA256加密  
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void SaveUsers(List<AccountViewModel> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            System.IO.File.WriteAllText("./User.json", json);
        }
        private List<AccountViewModel> GetUsers()
        {
            var json = System.IO.File.ReadAllText("./User.json");
            return JsonConvert.DeserializeObject<List<AccountViewModel>>(json) ?? new List<AccountViewModel>();
        }
    }
}
