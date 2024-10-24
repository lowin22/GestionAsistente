using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class BadgeRN
    {
        private readonly BadgeAD badgeAD;

        public BadgeRN()
        {
            badgeAD = new BadgeAD();
        }

        public async Task<bool> RegistrarBadge(Badge badge)
        {
            if (badge == null)
            {
                throw new ArgumentNullException(nameof(badge));
            }
            return await badgeAD.RegistrarBadge(badge); // Sin await si no es asíncrono
        }

        public async Task<List<Badge>> ListarBadge()
        {
            List<Badge> encargados = new List<Badge>();
            encargados = await this.badgeAD.ListarBadge();
            return encargados;
        }

        public async Task<bool> EliminarBadge(int badgeID)
        {
            if (badgeID == null)
            {
                throw new ArgumentNullException(nameof(badgeID));
            }
            return await badgeAD.EliminarBadge(badgeID); // Sin await si no es asíncrono
        }

        public async Task<bool> ActualizarBadge(Badge badge)
        {
            if (badge == null)
            {
                throw new ArgumentNullException(nameof(badge));
            }
            return await badgeAD.ModificarBadge(badge);
        }
        public async Task<List<Badge>> ListarBadgePorUnidad(int unidadID)
        {
            if (unidadID == null)
            {
                throw new ArgumentNullException(nameof(unidadID));
            }
            return await badgeAD.listarBadgePorUnidad(unidadID);
        }

    }





}
