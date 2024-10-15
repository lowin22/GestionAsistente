using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class UsuarioControlador
    {

        private readonly UsuarioRN usuarioRN;

        public UsuarioControlador()
        {
            usuarioRN = new UsuarioRN();
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            return await usuarioRN.RegistrarUsuario(usuario);
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await usuarioRN.ListarUsuarios();
        }

        public async Task<(string, bool)> ActualizarUsuario(Usuario usuario)
        {
            return await usuarioRN.ActualizarUsuario(usuario);
        }
        public async Task<(string, bool)> EliminarUsuario(int usuarioID)
        {
            return await usuarioRN.EliminarUsuario(usuarioID);
        }
    }
}
