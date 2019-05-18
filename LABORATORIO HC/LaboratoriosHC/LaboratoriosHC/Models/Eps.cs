using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoriosHC.Models
{
    public class Eps
    {
        public int ID { get; set; }
        public string Ciudad { get; set; }
        /*public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string HRS { get; set; }
        public string HRFS { get; set; }
        public string HMS { get; set; }
        public string HMFS { get; set; }*/

        public Eps()
        {
            ID = 0;
            Ciudad = "";
        }
    }
}
