using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoriosHC.Models
{
    public class TipoExamen
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }

        public TipoExamen()
        {
            Id = 0;
            Nombre = "";
            Costo = 0;
        }
    }
}
