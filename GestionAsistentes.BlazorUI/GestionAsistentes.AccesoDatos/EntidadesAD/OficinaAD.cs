using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
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
        public bool RegistrarOficina(Oficina oficina)
        {
            OficinaEF oficinaEF = new OficinaEF
            {
                Nombre = oficina.Nombre
            };
            _contexto.OficinaEFs.Add(oficinaEF);

            return _contexto.SaveChanges() > 0;
        }
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
        public bool ModificarOficina(Oficina oficina)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(oficina.OficinaID);
            if (oficinaEF == null)
            {
                return false;
            }
            oficinaEF.Nombre = oficina.Nombre;
            return _contexto.SaveChanges() > 0;
        }
        public bool EliminarOficina(int OficinaID)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(OficinaID);
            if (oficinaEF == null)
            {
                return false;
            }
            _contexto.OficinaEFs.Remove(oficinaEF);
            return _contexto.SaveChanges() > 0;
        }
    }
}
