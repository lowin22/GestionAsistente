using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class OficinaAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public OficinaAD()
        {
            _contexto = new GestionAsistenteContexto();
        }
        public List<OficinaEF> ObtenerOficinas()
        {
            return _contexto.OficinaEFs.ToList();
        }
        public async Task<int> RegistrarOficina(Oficina oficina)
        {
            OficinaEF oficinaEF = new OficinaEF
            {
                Nombre = oficina.Nombre
            };
            _contexto.OficinaEFs.Add(oficinaEF);
            _contexto.SaveChanges();
            return oficinaEF.OficinaID;
            //obtener el ID del ultimo
        }

        //public async Task<bool> RegistrarOficina(EstacionTrabajo estacion)
        //{
        //    // Crear el objeto EstacionEF
        //    EstacionTrabajoEF estacionEF = new EstacionTrabajoEF
        //    {
        //        Numero = estacion.Numero,
        //        Oficina= estacion.Oficina,
        //        SegundoApellido = encargado.Persona.SegundoApellido
        //    };

        //    // Crear el objeto OficinaEF y asociarlo con la EstacionEF
        //    EncargadoEF encargadoEF = new EncargadoEF
        //    {
        //        Persona = estacionEF, // Aquí asignamos el objeto persona
        //        UnidadID = encargado.UnidadID
        //    };

        //    // Agregar el objeto EncargadoEF al contexto
        //    this._contexto.EncargadoEFs.Add(encargadoEF);

        //    // Guardar los cambios
        //    return this._contexto.SaveChanges() > 0;
        //}



        public async Task<bool> ExisteOficina(string nombre)
        {
            var oficina = _contexto.OficinaEFs
              .Where(x => x.Nombre == nombre)
                .Select(x => new Oficina
                {
                    OficinaID = x.OficinaID,
                    Nombre = x.Nombre
                }).FirstOrDefault();
            if (oficina == null)
            {
                return false;
            }
            return true;
        }

        public async Task<(string, bool)> ModificarOficina(Oficina oficina, int cantidadEstaciones)
        {
            // Buscar la oficina en la base de datos
            OficinaEF oficinaEF = await _contexto.OficinaEFs.FindAsync(oficina.OficinaID);
            if (oficinaEF == null)
            {
                return ("La oficina no se pudo actualizar", false);
            }

            // Actualizar el nombre de la oficina
            oficinaEF.Nombre = oficina.Nombre;

            // Obtener estaciones de trabajo actuales asociadas a esta oficina
            var estacionesActuales = _contexto.EstacionTrabajoEFs
                .Where(et => et.OficinaID == oficina.OficinaID).ToList();

            int cantidadActual = estacionesActuales.Count;
            int diferencia = cantidadEstaciones - cantidadActual;

            if (diferencia > 0)
            {
                // Agregar nuevas estaciones de trabajo
                var nuevasEstaciones = Enumerable.Range(0, diferencia)
                    .Select(_ => new EstacionTrabajoEF
                    {
                        Numero = cantidadActual + 1, // O generar otro identificador si es necesario
                        Estado = 0,       // Puedes definir un estado inicial aquí
                        OficinaID = oficina.OficinaID
                    }).ToList();

                _contexto.EstacionTrabajoEFs.AddRange(nuevasEstaciones);
            }
            else if (diferencia < 0)
            {
                // Eliminar exceso de estaciones de trabajo
                var estacionesAEliminar = estacionesActuales
                    .Take(Math.Abs(diferencia)).ToList();

                _contexto.EstacionTrabajoEFs.RemoveRange(estacionesAEliminar);
            }

            // Guardar cambios en la base de datos
            bool guardadoExitoso = await _contexto.SaveChangesAsync() > 0;

            return guardadoExitoso
                ? ("Oficina y estaciones de trabajo actualizadas correctamente", true)
                : ("Error al actualizar la oficina y estaciones de trabajo", false);
        }


        public async Task<bool> EliminarOficina(int OficinaID)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(OficinaID);
            if (oficinaEF == null)
            {
                return false;
            }
            _contexto.OficinaEFs.Remove(oficinaEF);
            return _contexto.SaveChanges() > 0;
        }

        //public async Task<(string, bool)> EliminarOficina(int OficinaID)
        //{
        //    // Obtén la oficina junto con sus estaciones de trabajo
        //    var estacionesEF = _contexto.EstacionTrabajoEFs;
        //    var oficinaEF = await _contexto.OficinaEFs
        //                                   .Include(o => estacionesEF)
        //                                   .FirstOrDefaultAsync(o => o.OficinaID == OficinaID);

        //    if (oficinaEF == null)
        //    {
        //        return ("Oficina no encontrada", false);
        //    }

        //    // Eliminar todas las estaciones de trabajo relacionadas
        //    _contexto.EstacionTrabajoEFs.RemoveRange(estacionesEF);

        //    // Eliminar la oficina
        //    _contexto.OficinaEFs.Remove(oficinaEF);

        //    // Guardar los cambios
        //    return await _contexto.SaveChangesAsync() > 0
        //        ? ("Eliminado correctamente", true)
        //        : ("Error al eliminar", false);
        //}


    }
}
