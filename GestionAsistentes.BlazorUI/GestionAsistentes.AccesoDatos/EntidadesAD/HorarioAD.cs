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
    public class HorarioAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public HorarioAD()
        {
            _contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            HorarioEF horarioEF = new HorarioEF
            {
                AsistenteID = horario.Asistente.AsistenteID,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Dia = horario.Dia,
                EstacionTrabajoID = horario.EstacionTrabajoID
            };
            _contexto.HorarioEFs.Add(horarioEF);
            return _contexto.SaveChanges() > 0;
        }
        public Horario BuscarHorarioPorAsistente(int AsistenteID)
        {
            var horario = _contexto.HorarioEFs
              .Where(x => x.AsistenteID == AsistenteID)
                .Select(x => new Horario
                {
                    HorarioID = x.HorarioID,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin,
                    Dia = x.Dia,
                    EstacionTrabajoID = x.EstacionTrabajoID
                }).FirstOrDefault();
            if (horario == null)
            {
                return null;
            }
            return horario;
        }
        public List<Horario> ListarHorarios()
        {
            return _contexto.HorarioEFs.Select(x => new Horario
            {
                HorarioID = x.HorarioID,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                Dia = x.Dia,
                EstacionTrabajoID = x.EstacionTrabajoID
            }).ToList();
        }
    }
}
