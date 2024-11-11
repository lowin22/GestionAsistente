using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionAsistente.ReglasNegocio
{
    public class AsistenteRN
    {
        private readonly AsistenteAD asistenteAD;
        public AsistenteRN()
        {
            asistenteAD = new AsistenteAD();
        }

        private class ValidacionResultado
        {
            public bool EsValido { get; }
            public string Mensaje { get; }

            public ValidacionResultado(bool esValido, string mensaje)
            {
                EsValido = esValido;
                Mensaje = mensaje;
            }
        }

        public async Task<(bool exito, string mensaje)> RegistrarAsistente(Asistente asistente)
        {
            const string MENSAJE_EXITO = "Se registró con éxito";

            if (asistente == null)
            {
                return (false, "Llene los campos para poder registrar.");
            }

            var validacion = ValidarAsistente(asistente);
            if (!validacion.EsValido)
            {
                return (false, validacion.Mensaje);
            }

            var registroExitoso = await asistenteAD.RegistrarAsistente(asistente);
            return (registroExitoso, MENSAJE_EXITO);
        }

        private ValidacionResultado ValidarAsistente(Asistente asistente)
        {
            var reglasDeNegocio = new Dictionary<Func<Asistente, bool>, string>
        {
            // Validaciones básicas del Asistente
            { a => !string.IsNullOrWhiteSpace(a.nombreUsuario),
              "El nombre de usuario no puede estar en blanco" },

            { a => !string.IsNullOrWhiteSpace(a.Contrasenia),
              "La contraseña no puede estar en blanco" },

            { a => a.Accesos != null,
              "Los accesos no pueden estar en blanco" },

            // Validaciones de la Persona
            { a => a.Persona != null,
              "Los datos personales son requeridos" },

            { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.Nombre),
              "El nombre no puede estar en blanco" },

            { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.PrimerApellido),
              "El primer apellido no puede estar en blanco" },

            { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.SegundoApellido),
              "El segundo apellido no puede estar en blanco" }
        };

            foreach (var regla in reglasDeNegocio)
            {
                if (!regla.Key(asistente))
                {
                    return new ValidacionResultado(false, regla.Value);
                }
            }

            return new ValidacionResultado(true, string.Empty);
        }

        public async Task<List<Asistente>> ListarAsistentes()
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.listarAsistentes();
            return asistentes;
        }

        public async Task<List<Asistente>> ListarAsistentesPorUnidadID(int? unidad)
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.ListarAsistentesPorUnidadID(unidad);
            return asistentes;
        }
        public async Task<List<Asistente>> BuscarAsistentePorNombre(string nombre)
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.BuscarAsistentePorNombre(nombre);
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
