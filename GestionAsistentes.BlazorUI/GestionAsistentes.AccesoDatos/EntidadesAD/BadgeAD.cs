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
                UnidadID = badge.UnidadID
            };
            this._contexto.BadgesEF.Add(badgeEF);
            return this._contexto.SaveChanges() > 0;
        }
        //public List<Badge> listarBadges()
        //{
        //    List<BadgeEF> badgeEFs = _contexto.BadgesEF.ToList();
        //    List<Badge> badges = new List<Badge>();

        //    foreach (BadgeEF badgeEF in badgeEFs)
        //    {
        //        badges.Add(new Badge
        //        {
        //            BadgeID = badgeEF.BadgeID,
        //            Accesos = badgeEF.Accesos,
        //            Horario = badgeEF.Horario,
        //            UnidadID = badgeEF.UnidadID
        //        });
        //    }

        //    return badges;
        //}


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


        public bool ModificarBadge(Badge badge)
        {
            BadgeEF badgeEF = _contexto.BadgesEF.Find(badge.BadgeID);

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
            BadgeEF badgeEF = _contexto.BadgesEF.Find(BadgeID);
            if (badgeEF == null)
            {
                return false;
            }
            _contexto.BadgesEF.Remove(badgeEF);
            return _contexto.SaveChanges() > 0;
        }

    }
}
