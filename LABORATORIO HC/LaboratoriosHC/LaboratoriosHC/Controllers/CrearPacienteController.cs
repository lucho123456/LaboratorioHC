using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaboratoriosHC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaboratoriosHC.Controllers
{
    public class CrearPacienteController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Paciente()
        {
            int contador = 0;
            List<Eps> epss = new List<Eps>();
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
                        Eps nuevoEps = new Eps();
                        nuevoEps.ID = (int)reader[0];
                        nuevoEps.Ciudad = reader[1].ToString();
                        epss.Add(nuevoEps);
                        ViewData["Eps" + contador.ToString()] = reader[1];
                        contador = contador + 1;
                    }
                    ViewBag.epss = epss;
                }
            }
            return View();
        }

        public IActionResult CrearPaciente()
        {
            string NombreCompleto = Request.Form["NombreCompleto"];
            string TipoIdentificacion = Request.Form["TipoIdentificacion"];
            string NumeroIdentificacion = Request.Form["NumeroIdentificacion"];
            string FechaDeNacimiento = Request.Form["FechaDeNacimiento"];
            string Genero = Request.Form["Genero"];
            string EstadoCivil = Request.Form["EstadoCivil"];
            string Direccion = Request.Form["Direccion"];
            string BarrioResidencia = Request.Form["BarrioResidencia"];
            string NivelEscolaridad = Request.Form["NivelEscolaridad"];
            string Ocupacion = Request.Form["Ocupacion"];
            string TelefonoContacto = Request.Form["TelefonoContacto"];
            string Regimen = Request.Form["Regimen"];
            string EPS = Request.Form["EPS"];
            string Email = Request.Form["Email"];
            string ContactoEmergencia = Request.Form["ContactoEmergencia"];
            string AntecedentesMedicos = Request.Form["AntecedentesMedicos"];

            var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Paciente (Nombre, TipoId, Id, Nacimiento, " +
                    "Genero, EstadoCivil, Direccion, Barrio, Escolaridad, Ocupacion, Telefono, Regimen, " +
                    "EpsId, Email, ContactoEmergencia, Antecedentes)" + "VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15);";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.Add("@0", System.Data.SqlDbType.VarChar).Value = NombreCompleto;
                    cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar).Value = TipoIdentificacion;
                    cmd.Parameters.Add("@2", System.Data.SqlDbType.Int).Value = Int32.Parse(NumeroIdentificacion);
                    cmd.Parameters.Add("@3", System.Data.SqlDbType.Int).Value = Int32.Parse(FechaDeNacimiento);
                    cmd.Parameters.Add("@4", System.Data.SqlDbType.VarChar).Value = Genero;
                    cmd.Parameters.Add("@5", System.Data.SqlDbType.VarChar).Value = EstadoCivil;
                    cmd.Parameters.Add("@6", System.Data.SqlDbType.VarChar).Value = Direccion;
                    cmd.Parameters.Add("@7", System.Data.SqlDbType.VarChar).Value = BarrioResidencia;
                    cmd.Parameters.Add("@8", System.Data.SqlDbType.VarChar).Value = NivelEscolaridad;
                    cmd.Parameters.Add("@9", System.Data.SqlDbType.VarChar).Value = Ocupacion;
                    cmd.Parameters.Add("@10", System.Data.SqlDbType.VarChar).Value = TelefonoContacto;
                    cmd.Parameters.Add("@11", System.Data.SqlDbType.VarChar).Value = Regimen;
                    cmd.Parameters.Add("@12", System.Data.SqlDbType.Int).Value = Int32.Parse(EPS);
                    cmd.Parameters.Add("@13", System.Data.SqlDbType.VarChar).Value = Email;
                    cmd.Parameters.Add("@14", System.Data.SqlDbType.VarChar).Value = ContactoEmergencia;
                    cmd.Parameters.Add("@15", System.Data.SqlDbType.VarChar).Value = AntecedentesMedicos;


                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            if (EPS == "Monteria")
            {
                return View("~/RegistroExamen/Registro.cshtml");
            }
            else
            {
                return RedirectToAction("~/Home/Principal");

            }

        }

    }
}
