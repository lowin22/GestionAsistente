using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
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
        //Nuevo
        public async Task<int> ContarUsuarios()
        {
            return await usuarioAD.ContarUsuarios();
        }

        public async Task<List<Usuario>> ObtenerUsuariosPaginados(int currentPage, int pageSize)
        {
            currentPage = Math.Max(currentPage, 1);

            int skip = (currentPage - 1) * pageSize;
            return await usuarioAD.ObtenerUsuariosPaginados(skip, pageSize);
        }
    }
}
