using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoriosHC.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoId { get; set; }
        public int Nacimiento { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string Escolaridad { get; set; }
        public string Ocupacion { get; set; }
        public string Telefono { get; set; }
        public string Regimen { get; set; }
        public int EpsId { get; set; }
        public string Email { get; set; }
        public string ContactoEmergencia { get; set; }
        public string Antecedentes { get; set; }

        public Paciente()
        {
            Id = 0;
            Nombre = "";
            TipoId = "";
            Nacimiento = 0;
            Genero = "";
            EstadoCivil = "";
            Direccion = "";
            Barrio = "";
            Escolaridad = "";
            Ocupacion = "";
            Telefono = "";
            Regimen = "";
            EpsId = 0;
            Email = "";
            Antecedentes = "";
            ContactoEmergencia = "";

        }
    }
}
