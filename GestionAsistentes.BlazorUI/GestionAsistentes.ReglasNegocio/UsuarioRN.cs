using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class UsuarioRN
    {

        private readonly UsuarioAD usuarioAD;

        public UsuarioRN()
        {
            usuarioAD = new UsuarioAD();
        }
    
        public async Task<List<Usuario>> ListarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = await this.usuarioAD.ListarUsuarios();
            return usuarios;
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                if (usuario.Persona.PrimerApellido == null)
                {
                    throw new Exception("La persona primer no puede ser nula");
                }
                if (usuario.Persona.Nombre == null)
                {
                    throw new Exception("La persona nombre no puede ser nula");
                }
            }

            return await usuarioAD.RegistrarUsuario(usuario);
        }

        public async Task<(string, bool)> ActualizarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                if (usuario.Persona.PrimerApellido == null)
                {
                    return ("El primer apellido es necesario", false);
                }
                if (usuario.Persona.Nombre == null)
                {
                    return ("El nombre es necesario", false);
                }
            }
            return await usuarioAD.ActualizarUsuario(usuario);
        }

        public async Task<(string, bool)> EliminarUsuario(int encargadoID)
        {
            return await usuarioAD.EliminarUsuario(encargadoID);
        }

    }
}
