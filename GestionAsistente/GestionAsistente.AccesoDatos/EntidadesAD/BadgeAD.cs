using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class BadgeAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public BadgeAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarBadge(Badge badge)
        {
            BadgeEF badgeEF = new BadgeEF
            {
                Accesos = badge.Accesos,
                Horario = badge.Horario,
                UnidadID = badge.UnidadID,
                Ocupado = badge.Ocupado
            };
            this._contexto.BadgesEF.Add(badgeEF);
            return this._contexto.SaveChanges() > 0;
        }

        public async Task<List<Badge>> ListarBadge()
        {
            List<Badge> badges = new List<Badge>();
            List<BadgeEF> badgesEF = this._contexto.BadgesEF
                .Include(e => e.Unidad)  // Incluir Unidad
                .ToList();
            
            foreach (BadgeEF badgeEF in badgesEF)
            {

                Badge badge = new Badge
                {
                    BadgeID = badgeEF.BadgeID,
                    Horario = badgeEF.Horario,
                    UnidadID = badgeEF.UnidadID,
                    Accesos = badgeEF.Accesos,
                    Ocupado = badgeEF.Ocupado,
                    Unidad = badgeEF.Unidad != null ? new Unidad
                    {
                        UnidadID = badgeEF.Unidad.UnidadID,
                        Nombre = badgeEF.Unidad.Nombre
                    } : null
                };

                badges.Add(badge);
            }

            return badges;
        }


        public async Task<bool> ModificarBadge(Badge badge)
        {
            BadgeEF badgeEF = _contexto.BadgesEF.FirstOrDefault(b => b.BadgeID == badge.BadgeID);

            if (badgeEF == null)
            {
                return false;
            }

            badgeEF.BadgeID = badge.BadgeID;
            badgeEF.Accesos = badge.Accesos;
            badgeEF.Horario = badge.Horario;
            badgeEF.UnidadID = badge.UnidadID;
            return _contexto.SaveChanges() > 0;
        }
        public async Task<bool> EliminarBadge(int BadgeID)
        {
            BadgeEF badgeEF = this._contexto.BadgesEF.FirstOrDefault(b => b.BadgeID == BadgeID);
            if (badgeEF == null)
            {
                return false;
            }
            this._contexto.BadgesEF.Remove(badgeEF);
            return _contexto.SaveChanges() > 0;
        }
        public async Task<List<Badge>> listarBadgePorUnidad(int unidadId)
        {
            // Filtrar los badges por UnidadID antes de traerlos desde la base de datos
            List<BadgeEF> badgesEF = await this._contexto.BadgesEF
                .Include(e => e.Unidad)  // Incluir Unidad
                .Where(b => b.UnidadID == unidadId && b.Ocupado != true)  // Filtrar por UnidadID
                .ToListAsync();

            List<Badge> badges = new List<Badge>();

            foreach (BadgeEF badgeEF in badgesEF)
            {
                Badge badge = new Badge
                {
                    BadgeID = badgeEF.BadgeID,
                    Horario = badgeEF.Horario,
                    UnidadID = badgeEF.UnidadID,
                    Accesos = badgeEF.Accesos,
                    Unidad = badgeEF.Unidad != null ? new Unidad
                    {
                        UnidadID = badgeEF.Unidad.UnidadID,
                        Nombre = badgeEF.Unidad.Nombre
                    } : null
                };

                badges.Add(badge);
            }

            return badges;
        }
    }
}
