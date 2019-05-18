using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoriosHC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Login()
        {
            string Usuario = Request.Form["Usuario"];
            string Contrasena = Request.Form["Contrasena"];
            if (Usuario != "")
            {
                var query = "SELECT * FROM Usuario;";
                var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[1].ToString() == Usuario && reader[2].ToString() == Contrasena)
                            {
                                return RedirectToAction("Principal");
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Principal()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
