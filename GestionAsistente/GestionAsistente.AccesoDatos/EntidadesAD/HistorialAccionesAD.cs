using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class HistorialAccionesAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public HistorialAccionesAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            HistorialAccionesEF historialAccionesEF = new HistorialAccionesEF
            {
                Fecha = historialAcciones.Fecha,
                NombrePersona = historialAcciones.NombrePersona,
                Accion = historialAcciones.Accion 
            
            };
            this._contexto.HistoriaAccionesEFs.Add(historialAccionesEF);

            return this._contexto.SaveChanges() > 0;
        }
        public List<HistorialAcciones> listarHistorialAcciones(DateTime fecha)
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.Where(a => a.Fecha == fecha).ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs)
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    Fecha = historialAccionesEF.Fecha,
                    NombrePersona = historialAccionesEF.NombrePersona,
                    Accion = historialAccionesEF.Accion
                });
            }

            return historialAcciones;
        }
        public List<HistorialAcciones> BuscarAccionPorPersona(string NombrePersona)
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.Where(a => a.NombrePersona==NombrePersona).ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs)
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    Fecha = historialAccionesEF.Fecha,
                    NombrePersona = historialAccionesEF.NombrePersona,
                    Accion = historialAccionesEF.Accion
                });
            }

            return historialAcciones;
        }
        public List<HistorialAcciones> BuscarPorAccion(string Accion)
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.Where(a => a.Accion == Accion).ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs)
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    Fecha = historialAccionesEF.Fecha,
                    NombrePersona = historialAccionesEF.NombrePersona,
                    Accion = historialAccionesEF.Accion
                });
            }

            return historialAcciones;
        }
        /*Nuevo*/
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs) // Cambiado a UnidadEF
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    HistoriaAccionesID = historialAccionesEF.HistoriaAccionesID,//añadir el id tambien
                    NombrePersona = historialAccionesEF.NombrePersona, // Cambiado a unidadEF
                    Accion = historialAccionesEF.Accion,
                    Fecha = historialAccionesEF.Fecha,
                });
            }

            return historialAcciones;
        }

        public async Task LimpiarHistorialCompleto()
        {
            var todosLosRegistros = _contexto.HistoriaAccionesEFs.ToList();
            _contexto.HistoriaAccionesEFs.RemoveRange(todosLosRegistros);

            await _contexto.SaveChangesAsync();
        }

    }
}
