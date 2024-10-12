using GestionAsistentes.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class PersonaControlador : Controller
    {
        public void guardarPersona(Persona persona)
        {

            ReglasNegocio.PersonaRN personaRN = new ReglasNegocio.PersonaRN();
            personaRN.guardarPersona(persona);
        }
    }
}
