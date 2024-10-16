using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionAsistentes.ReglasNegocio
{
    public class AsistenteRN
    {
        private readonly AsistenteAD asistenteAD;
        public AsistenteRN()
        {
            asistenteAD = new AsistenteAD();
        }

        public async Task<bool> GuardarAsistente(Asistente asistente)
        {
            if (asistente != null)
            {
               
                if (asistente.Persona.SegundoApellido == null)
                {
                    throw new Exception("El segundo apellido no puede ser nulo");
                }
                if (asistente.Persona.PrimerApellido == null)
                {
                    throw new Exception("El primer apellido no puede ser nula");
                }
                if (asistente.Persona.Nombre == null)
                {
                    throw new Exception("La persona nombre no puede ser nula");
                }
                if (asistente.Contrasenia == null)
                {
                    throw new Exception("La contrasenia asistente no puede ser nula");
                }
            }
            return await asistenteAD.RegistrarAsistente(asistente);
        }

        public async Task<List<Asistente>> ListarAsistentes()
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.listarAsistentes();
            return asistentes;
        }

        public async Task<bool> EliminarAsistente(int? asistenteID)
        {
            return await asistenteAD.EliminarAsistente(asistenteID);
        }

        public async Task<bool> ActualizarAsistente(Asistente asistente)
        {
            return await asistenteAD.ModificarAsistente(asistente);
        }
    }
}
