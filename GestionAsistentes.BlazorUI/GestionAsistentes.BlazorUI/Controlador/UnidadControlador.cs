using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class UnidadControlador
    {
        private readonly UnidadRN unidadRN;
        public UnidadControlador()
        {
            this.unidadRN = new UnidadRN();
        }
        public async Task<bool> RegistrarUnidad(Unidad unidad)
        {
            return await this.unidadRN.RegistrarUnidad(unidad);
        }
        public async Task<List<Unidad>> ListarUnidades()
        {
            return await this.unidadRN.ListasUnidades();
        }
    }
}
