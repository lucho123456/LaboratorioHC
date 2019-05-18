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
    public class RegistroExamen : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            List<TipoExamen> tiposExamenes = new List<TipoExamen>();
            var query = "SELECT * FROM TipoExamen;";
            var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoExamen nuevoTipoExamen = new TipoExamen();
                        nuevoTipoExamen.Id = (int)reader[0];
                        nuevoTipoExamen.Nombre = reader[1].ToString();
                        nuevoTipoExamen.Costo = (int)reader[2];
                        tiposExamenes.Add(nuevoTipoExamen);
                    }
                }
                ViewBag.tiposExamenes = tiposExamenes;
            }
            return View();
        }

        public IActionResult Examen()
        {
            int descuento =0;
            var query = "SELECT * FROM Paciente;";
            var connectionString = "Data Source=LAPTOP-R88EAA98\\SQLEXPRESS;Initial Catalog=Laboratorio;User ID=sa;Password=fnsp;Pooling=False";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ((int)reader[3]<=15 || (int)reader[3]>=60)
                        {
                            descuento = 20;
                        }
                        else if (16<=(int)reader[3]&&(int)reader[3]<=30)
                        {
                            descuento = 15;
                        }
                        else if (31 <= (int)reader[3] && (int)reader[3] <= 59)
                        {
                            descuento = 10;
                        }
                    }
                }
            }


            string NumeroIdentificacion = Request.Form["NumeroIdentificacion"];
            string TipoExamenIdentificacion = Request.Form["Examen"];
            string TipoExamenIdentificacion0 = Request.Form["Examen0"];
            string TipoExamenIdentificacion1 = Request.Form["Examen1"];
            string TipoExamenIdentificacion2 = Request.Form["Exame2"];
            int costo=0;
            int costo0=0;
            int costo1=0;
            int costo2=0;

            var tipoQuery = "SELECT * FROM TipoExamen;";
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(tipoQuery, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (TipoExamenIdentificacion==reader[0].ToString())
                        {
                            costo = (int)reader[2];
                        }
                        if (TipoExamenIdentificacion0!=null)
                        {
                            if (TipoExamenIdentificacion0 == reader[0].ToString())
                            {
                                costo0 = (int)reader[2];
                            }
                        }
                        if (TipoExamenIdentificacion1 != null)
                        {
                            if (TipoExamenIdentificacion1 == reader[0].ToString())
                            {
                                costo0 = (int)reader[2];
                            }
                        }
                        if (TipoExamenIdentificacion2 != null)
                        {
                            if (TipoExamenIdentificacion2 == reader[0].ToString())
                            {
                                costo0 = (int)reader[2];
                            }
                        }
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Examen (TipoExamenId, PacienteId, CostoEfectivo)" + "VALUES (@0, @1, @2);";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.Add("@0", System.Data.SqlDbType.VarChar).Value = TipoExamenIdentificacion;
                    cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar).Value = NumeroIdentificacion;
                    int costoEfectivo = costo * (1 - (descuento / 100));
                    cmd.Parameters.Add("@2", System.Data.SqlDbType.Int).Value = costoEfectivo;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if (TipoExamenIdentificacion0 != null)
                    {
                        cmd.Parameters.Add("@0", System.Data.SqlDbType.VarChar).Value = TipoExamenIdentificacion0;
                        cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar).Value = NumeroIdentificacion;
                        costoEfectivo = costo0 * (1 - (descuento / 100));
                        cmd.Parameters.Add("@2", System.Data.SqlDbType.Int).Value = costoEfectivo;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    if (TipoExamenIdentificacion1 != null)
                    {
                        cmd.Parameters.Add("@0", System.Data.SqlDbType.VarChar).Value = TipoExamenIdentificacion1;
                        cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar).Value = NumeroIdentificacion;
                        costoEfectivo = costo1 * (1 - (descuento / 100));
                        cmd.Parameters.Add("@2", System.Data.SqlDbType.Int).Value = costoEfectivo;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    if (TipoExamenIdentificacion2 != null)
                    {
                        cmd.Parameters.Add("@0", System.Data.SqlDbType.VarChar).Value = TipoExamenIdentificacion2;
                        cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar).Value = NumeroIdentificacion;
                        costoEfectivo = costo2 * (1 - (descuento / 100));
                        cmd.Parameters.Add("@2", System.Data.SqlDbType.Int).Value = costoEfectivo;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return RedirectToAction("Reporte/Reporte");

        }

}
}
