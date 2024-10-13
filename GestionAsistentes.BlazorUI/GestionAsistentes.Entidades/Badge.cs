using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.Entidades
{
    public class Badge
    {
        public int BadgeID { get; set; }
        public string Accesos { get; set; }
        public string Horario { get; set; }
        public int UnidadID { get; set; }
        public Unidad Unidad { get; set; }
    }
}
