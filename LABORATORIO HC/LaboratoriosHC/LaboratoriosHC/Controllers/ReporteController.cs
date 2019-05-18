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
    public class ReporteController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reporte()
        {
            List<Paciente> pacientes = new List<Paciente>();
            string ordenQueryString = "SELECT * FROM Examen;";
            string examenQueryString = "SELECT * FROM TipoExamen;";
            string pacienteQueryString = "SELECT * FROM Paciente;";
            int acidoUrico = 0;
            int descuentoAcidoUrico = 0;
            int examen0 = 0;
            int descuentoExamen0 = 0;
            int examen1 = 0;
            int descuentoExamen1 = 0;
            int examen2 = 0;
            int descuentoExamen2 = 0;

            int contador = 0;
            int[] idExamenes = new int[4];
            List<int> idPacientes = new List<int>();
            var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(examenQueryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idExamenes[contador] = (int)reader[0];
                    }
                }
            }


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(ordenQueryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ((int)reader[1]==idExamenes[0])
                        {
                            acidoUrico = acidoUrico + 1;
                            descuentoAcidoUrico = descuentoAcidoUrico + (int)reader[3];
                            idPacientes.Add((int)reader[2]);
                        }
                        else if ((int)reader[1] == idExamenes[1])
                        {
                            examen0 = examen0 + 1;
                            descuentoExamen0 = descuentoExamen0 + (int)reader[3];
                        }
                        else if ((int)reader[1] == idExamenes[2])
                        {
                            examen1 = examen1 + 1;
                            descuentoExamen1 = descuentoExamen1 + (int)reader[3];
                        }
                        else if ((int)reader[1] == idExamenes[3])
                        {
                            examen2 = examen2 + 1;
                            descuentoExamen2 = descuentoExamen2 + (int)reader[3];
                        }
                    }
                }
                ViewBag.acidoUrico = acidoUrico;
                ViewBag.examen0 = examen0;
                ViewBag.examen1 = examen1;
                ViewBag.examen2 = examen2;
                ViewBag.descuentoAcidoUrico = descuentoAcidoUrico;
                ViewBag.descuentoExamen0 = descuentoExamen0;
                ViewBag.descuentoExamen1 = descuentoExamen1;
                ViewBag.descuentoExamen2 = descuentoExamen2;
            }


            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(pacienteQueryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (idPacientes.Contains((int)reader[0]))
                        {
                            Paciente nuevoPaciente = new Paciente();
                            nuevoPaciente.Id = (int)reader[0];
                            nuevoPaciente.Nombre = reader[1].ToString();
                            nuevoPaciente.TipoId = reader[2].ToString();
                            nuevoPaciente.Nacimiento = (int)reader[3];
                            nuevoPaciente.Genero = reader[4].ToString();
                            nuevoPaciente.EstadoCivil = reader[5].ToString();
                            nuevoPaciente.Direccion = reader[6].ToString();
                            nuevoPaciente.Barrio = reader[7].ToString();
                            nuevoPaciente.Escolaridad = reader[8].ToString();
                            nuevoPaciente.Ocupacion = reader[9].ToString();
                            nuevoPaciente.Telefono = reader[10].ToString();
                            nuevoPaciente.Regimen = reader[11].ToString();
                            nuevoPaciente.EpsId = (int)reader[12];
                            nuevoPaciente.Email = reader[13].ToString();
                            nuevoPaciente.ContactoEmergencia = reader[14].ToString();
                            nuevoPaciente.Antecedentes = reader[15].ToString();
                            pacientes.Add(nuevoPaciente);
                        }
                    }
                }
                ViewBag.pacientes = pacientes;
            }
            return View();
        }

    }
}
