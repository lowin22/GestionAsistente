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







    }

    



}
