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
    public class EstacionTrabajoAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public EstacionTrabajoAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarEstacionTrabajo(EstacionTrabajo estacionTrabajo)
        {
            EstacionTrabajoEF estacionTrabajoEF = new EstacionTrabajoEF
            {
                Numero = estacionTrabajo.Numero,
                Estado = estacionTrabajo.Estado,
                OficinaID = estacionTrabajo.OficinaID
            };
            this._contexto.EstacionTrabajoEFs.Add(estacionTrabajoEF);
            return this._contexto.SaveChanges() > 0;
        }
        public List<EstacionTrabajo> listarEstacionesTrabajo()
        {
            List<EstacionTrabajoEF> estacionTrabajoEFs = _contexto.EstacionTrabajoEFs.ToList();
            List<EstacionTrabajo> estacionesTrabajo = new List<EstacionTrabajo>();

            foreach (EstacionTrabajoEF estacionTrabajoEF in estacionTrabajoEFs)
            {
                estacionesTrabajo.Add(new EstacionTrabajo
                {
                    Numero = estacionTrabajoEF.Numero,
                    Estado = estacionTrabajoEF.Estado,
                    OficinaID = estacionTrabajoEF.OficinaID
                });
            }

            return estacionesTrabajo;
        }
        public EstacionTrabajo BuscarEstacion(int numero)
        {
            var estacionTrabajo = _contexto.EstacionTrabajoEFs
              .Where(x => x.Numero == numero)
                .Select(x => new EstacionTrabajo
                {
                    EstacionTrabajoID = x.EstacionTrabajoID,
                    Numero = x.Numero,
                    Estado = x.Estado,
                    OficinaID = x.OficinaID
                }).FirstOrDefault();
            if (estacionTrabajo == null)
            {
                return null;
            }
            return estacionTrabajo;
        }
        public bool EliminarEstacionTrabajo(int EstacionTrabajoID)
        {
            EstacionTrabajoEF estacionTrabajoEF = _contexto.EstacionTrabajoEFs.Find(EstacionTrabajoID);
            if (estacionTrabajoEF == null)
            {
                return false;
            }
            _contexto.EstacionTrabajoEFs.Remove(estacionTrabajoEF);
            return _contexto.SaveChanges() > 0;
        }
    }
}
