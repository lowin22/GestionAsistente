using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class HorarioControlador
    {
        private readonly HorarioRN horarioRN;
        public HorarioControlador()
        {
            horarioRN = new HorarioRN();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            return await horarioRN.RegistrarHorario(horario);
        }
    }
}
