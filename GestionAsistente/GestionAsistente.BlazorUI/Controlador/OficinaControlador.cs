using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class OficinaControlador
    {

        private readonly OficinaRN oficinaRN;
        public OficinaControlador()
        {
            oficinaRN = new OficinaRN();
        }
        public async Task<int> RegistrarOficina(Oficina oficina)
        {
            return await oficinaRN.RegistrarOficina(oficina);
        }

        public async Task<List<Oficina>> ListarOficinas()
        {
            return await oficinaRN.ListarOficinas();
        }

        public async Task<bool> EliminarOficinas(int oficinaID)
        {
            return await oficinaRN.EliminarOficina(oficinaID);
        }

        public async Task<(string, bool)> ActualizarOficina(Oficina oficina)
        {
            return await oficinaRN.ActualizarOficina(oficina);
        }

    }
}
