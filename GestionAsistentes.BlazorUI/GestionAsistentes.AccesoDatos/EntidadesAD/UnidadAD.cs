using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
{
    public class UnidadAD
    {
        private readonly GestionAsistenteContexto _contexto;
        // Instancia de HistorialAccionesAD
        private readonly HistorialAccionesAD _historialAccionesAD;
        public UnidadAD() 
        { 
            this._contexto = new GestionAsistenteContexto();
            this._historialAccionesAD = new HistorialAccionesAD();
        }
        public async Task<bool> RegistrarUnidad(Unidad unidad)
        {
            UnidadEF unidadEF = new UnidadEF
            {
                Nombre = unidad.Nombre
            };
            this._contexto.UnidadEFs.Add(unidadEF);
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;

            if (resultado)
            {
                // Registrar acción en Historial
                _historialAccionesAD.RegistrarHistorialAcciones(new HistorialAcciones
                {
                    Fecha = DateTime.Now,
                    NombrePersona = "", // NombrePersona vacío por el momento
                    Accion = "Registrar Unidad" 
                });
            }

            return resultado;
            /**/

            //return this._contexto.SaveChanges() >0;
        }
        public async Task<List<Unidad>> listarUnidades()
        {
            List<UnidadEF> unidadEFs = _contexto.UnidadEFs.ToList();
            List<Unidad> unidads = new List<Unidad>();

            foreach (UnidadEF unidadEF in unidadEFs) // Cambiado a UnidadEF
            {
                unidads.Add(new Unidad
                {
                    UnidadID = unidadEF.UnidadID,//añadir el id tambien
                    Nombre = unidadEF.Nombre, // Cambiado a unidadEF
                });
            }

            return unidads;
        }
        public async Task<(string, bool)> ActualizarUnidad(Unidad unidad)
        {
            UnidadEF unidadEF = _contexto.UnidadEFs.Find(unidad.UnidadID);

            if (unidadEF == null)
            {
                return ("La unidad no se pudo actualizar", false);
            }
            unidadEF.Nombre = unidad.Nombre;
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;

            if (resultado)
            {
                // Registrar acción en Historial
                _historialAccionesAD.RegistrarHistorialAcciones(new HistorialAcciones
                {
                    Fecha = DateTime.Now,
                    NombrePersona = "", // NombrePersona vacío por el momento
                    Accion = "Actualizar Unidad"
                });
            }

            return resultado
                ? ("Actualizado correctamente", true)
                : ("Error al actualizar", false);
            /**/

            //return this._contexto.SaveChanges() > 0
            //    ? ("Actualizado correctamente", true)
            //    : ("Error al actualizar", false);
        }
        public async Task<(string, bool)> EliminarUnidad(int unidadID)
        {
            UnidadEF unidadEF = this._contexto.UnidadEFs.FirstOrDefault(e => e.UnidadID == unidadID);
            if (unidadEF == null)
            {
               return ("No existe el registro de unidad", false);
            }
            this._contexto.UnidadEFs.Remove(unidadEF);
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;

            if (resultado)
            {
                // Registrar acción en Historial
                _historialAccionesAD.RegistrarHistorialAcciones(new HistorialAcciones
                {
                    Fecha = DateTime.Now,
                    NombrePersona = "", // Nombre vacío por el momento
                    Accion = "Eliminar Unidad"
                });
            }

            return resultado
                ? ("Eliminado correctamente", true)
                : ("Error al eliminar", false);
            /**/

            //return this._contexto.SaveChanges() > 0
            //    ? ("Eliminado correctamente", true)
            //    : ("Error al eliminar", false);
        }
    }
}
