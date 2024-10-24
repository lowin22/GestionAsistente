using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class HorarioRN
    {
        private readonly HorarioAD horarioAD;
        public HorarioRN()
        {
            horarioAD = new HorarioAD();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            if (horario == null)
            {
                throw new ArgumentNullException(nameof(horario));
            }
            return await horarioAD.RegistrarHorario(horario); // Sin await si no es asíncrono
        }
        public async Task<List<Horario>> ListarHorariosPorEstacionTrabajo(int estacionTrabajoID)
        { 
            return await horarioAD.ListarHorariosPorEstacionTrabajo(estacionTrabajoID);
        }
        public async Task<List<Horario>> ListarHorariosPorOficina(int oficinaID)
        {
            return await horarioAD.ListarHorariosPorOficina(oficinaID);
        }
    }
}
