using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class BadgeControlador
    {

        private readonly BadgeRN badgeRN;
        public BadgeControlador()
        {
            badgeRN = new BadgeRN();
        }
        public async Task<bool> RegistrarBadge(Badge badge)
        {
            return await badgeRN.RegistrarBadge(badge);
        }





    }
}
