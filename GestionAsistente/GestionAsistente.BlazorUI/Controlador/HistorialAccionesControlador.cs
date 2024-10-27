using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class HistorialAccionesControlador
    {
        private readonly HistorialAccionesRN historialAccionesRN;
        public HistorialAccionesControlador()
        {
            this.historialAccionesRN = new HistorialAccionesRN();
        }
        public async Task<bool> RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            return await historialAccionesRN.RegistrarHistorialAcciones(historialAcciones);
        }
        public List<HistorialAcciones> ListarHistorialPorFecha(DateTime fecha)
        {
            return historialAccionesRN.ListarHistorialPorFecha(fecha);
        }
        public List<HistorialAcciones> BuscarAccionPorPersona(string nombrePersona)
        {
            return historialAccionesRN.BuscarAccionPorPersona(nombrePersona);
        }
        public List<HistorialAcciones> BuscarPorAccion(string accion)
        {
            return historialAccionesRN.BuscarPorAccion(accion);
        }
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            return await historialAccionesRN.ListarHistorial();
        }
    }
}
