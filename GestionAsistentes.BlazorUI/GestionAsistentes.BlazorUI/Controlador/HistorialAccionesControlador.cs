using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class HistorialAccionesControlador
    {
        private readonly HistorialAccionesRN historialAccionesRN;
        public HistorialAccionesControlador()
        {
            this.historialAccionesRN = new HistorialAccionesRN();
        }
        public bool RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            return historialAccionesRN.RegistrarHistorialAcciones(historialAcciones);
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
