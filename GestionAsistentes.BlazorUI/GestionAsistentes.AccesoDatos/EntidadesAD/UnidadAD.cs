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
        public UnidadAD() { 
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarUnidad(Unidad unidad)
        {

            UnidadEF unidadEF = new UnidadEF
            {
                Nombre = unidad.Nombre
            };
            this._contexto.UnidadEFs.Add(unidadEF);
            return this._contexto.SaveChanges() >0;
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
        public bool ModificarUnidad(Unidad unidad)
        {
            UnidadEF unidadEF = _contexto.UnidadEFs.Find(unidad.UnidadID);

            if (unidadEF == null)
            {
                return false;
            }
            unidadEF.Nombre = unidad.Nombre; 
           return _contexto.SaveChanges() > 0;
        }
        public bool EliminarUnidad(int UnidadID)
        {
            UnidadEF unidadEF = _contexto.UnidadEFs.Find(UnidadID);
            if (unidadEF == null)
            {
                return false;
            }
            _contexto.UnidadEFs.Remove(unidadEF);
            return _contexto.SaveChanges() > 0;
        }
    }
}
