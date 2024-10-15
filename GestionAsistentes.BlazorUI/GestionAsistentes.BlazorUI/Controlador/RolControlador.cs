using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class RolControlador
    {

        private readonly RolRN rolRN;
        public RolControlador()
        {
            this.rolRN = new RolRN();
        }

        public async Task<List<Rol>> ListarRoles()
        {

            return await this.rolRN.ListarRoles();
        }

    }
}
