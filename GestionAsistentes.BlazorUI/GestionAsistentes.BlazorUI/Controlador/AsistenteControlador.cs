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

    }
}