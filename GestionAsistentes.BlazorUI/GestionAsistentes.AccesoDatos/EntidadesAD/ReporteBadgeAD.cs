using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
{
    public class ReporteBadgeAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public ReporteBadgeAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarReporteBadge(ReporteBadge reporteBadge)
        {

            ReporteBadgeEF reporteBadgeEF = new ReporteBadgeEF
            {
                NombreUsuario = reporteBadge.NombreUsuario,
                NumeroBadge = reporteBadge.NumeroBadge,
                NombreAsistente = reporteBadge.NombreAsistente,
                Fecha = reporteBadge.Fecha
            };
            this._contexto.ReporteBadgeEFs.Add(reporteBadgeEF);
            return this._contexto.SaveChanges() > 0;
        }
    }
}
