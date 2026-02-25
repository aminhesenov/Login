using System.Diagnostics;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
   
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index(string Username, string Password)
        {
            int say = 0;

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\ASP.NET\\User.accdb";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM [User] WHERE Username=? AND Password=?";
                OleDbCommand cmd = new OleDbCommand(query, conn);

                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);

                say = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }

            if (say > 0)
                ViewBag.Message = "Login success";
            else
                ViewBag.Message = "Username or Password incorrect";

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
