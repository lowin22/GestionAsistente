using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
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



        public Oficina BuscarOficina(string nombre)
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
                return null;
            }
            return oficina;
        }
        public async Task<(string, bool)> ModificarOficina(Oficina oficina)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(oficina.OficinaID);
            if (oficinaEF == null)
            {
                return ("El encargado no se pudo actualizar", false);
            }
            oficinaEF.Nombre = oficina.Nombre;
            return this._contexto.SaveChanges() > 0
        ? ("Actualizado correctamente", true)
        : ("Error al actualizar", false);
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
