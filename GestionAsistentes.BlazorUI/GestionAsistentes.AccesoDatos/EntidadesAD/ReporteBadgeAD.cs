using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> RegistrarReporteBadge(ReporteBadge reporteBadge)
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

        public async Task<List<ReporteBadge>> listarReporte()
        {
            List<ReporteBadgeEF> reporteEFs = _contexto.ReporteBadgeEFs.ToList();
            List<ReporteBadge> reportes = new List<ReporteBadge>();

            foreach (ReporteBadgeEF reporteEF in reporteEFs) // Cambiado a UnidadEF
            {
                reportes.Add(new ReporteBadge
                {
                    NombreUsuario = "Usuario",
                    NumeroBadge = reporteEF.NumeroBadge,
                    NombreAsistente = reporteEF.NombreAsistente, 
                    Fecha = DateTime.Now
                });
            }

            return reportes;
        }





    }
}
