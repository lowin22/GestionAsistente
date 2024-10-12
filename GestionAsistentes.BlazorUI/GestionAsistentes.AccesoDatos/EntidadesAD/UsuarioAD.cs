using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
{
    public class UsuarioAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public UsuarioAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarUsuario(Usuario usuario)
        {
            UsuarioEF usuarioEF = new UsuarioEF
            {
               Contrasenia = usuario.Contrasenia,
               RolID = usuario.RolID,
               UnidadID = usuario.UnidadID,
               PersonaID = usuario.PersonaID
            };
            this._contexto.UsuariosEF.Add(usuarioEF);
            return this._contexto.SaveChanges() > 0;
        }
      public bool VerificarContrasenia(Usuario usuario)
        {
            UsuarioEF usuarioEF = this._contexto.UsuariosEF
          .FirstOrDefault(u => u.Contrasenia == usuario.Contrasenia);

            return usuarioEF != null;
        }

    }
}
