using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionAsistentes.ReglasNegocio;
using GestionAsistentes.Entidades;
using Microsoft.SqlServer.Server;
namespace GestionAsistentes.BlazorUI.Controlador
{

    public class AsistenteControlador : Controller
    {
        private readonly AsistenteRN asistenteRN;

        public AsistenteControlador()
        {
            asistenteRN = new AsistenteRN();
        }
        public async Task<bool> RegistrarAsistente(Asistente asistente)
        {
            return await asistenteRN.GuardarAsistente(asistente);
        }

        public async Task<List<Asistente>> ListarAsistentes()
        {
            return await asistenteRN.ListarAsistentes();
        }

        public async Task<bool> EliminarAsistente(int? asistenteID)
        {
            return await asistenteRN.EliminarAsistente(asistenteID);
        }

        public async Task<bool> ActualizarAsistente(Asistente asistente)
        {
            return await asistenteRN.ActualizarAsistente(asistente);
        }

    }
}