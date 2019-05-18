using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratoriosHC.Controllers
{
    public class InformacionSedesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Sede()
        {
            var query = "SELECT * FROM EPS;";
            var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ViewData[reader[1].ToString()] =
                            "SEDE" + reader[1] + "\n" +
                reader[2] +
                "\n" +
                "Tel: " + reader[3]+
                "\n" +
                "Correo: " + reader[4]+
                 "\n" +
                 "Horario entrega de resultados semana: " + reader[5]+
                "\n" +
                "Horario entrega de resultados fin de semana: " + reader[6]+
                "\n" +
                "Horario toma de muestras semana: " +reader[7]+
                "\n" +
                "Horario toma de muestras fin de semana: "+reader[8];
                    }
                }
            }
            return View();
        }
    }
}
