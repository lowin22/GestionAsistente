using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class UsuarioAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public UsuarioAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            PersonaEF personaEF = new PersonaEF
            {
                Nombre = usuario.Persona.Nombre,
                PrimerApellido = usuario.Persona.PrimerApellido,
                SegundoApellido = usuario.Persona.SegundoApellido
            };

            UsuarioEF usuarioEF = new UsuarioEF
            {
                Contrasenia = usuario.Contrasenia,
                RolID = usuario.RolID,
                UnidadID = usuario.UnidadID,
                Persona = personaEF
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

        public async Task<List<Usuario>> ListarUsuarios()
        {
            // Cargar los datos con las relaciones
            List<UsuarioEF> usuarioEFs = _contexto.UsuariosEF
                .Include(u => u.Rol)
                .Include(u => u.Unidad)
                .Include(u => u.Persona)
                .ToList();

            List<Usuario> usuarios = new List<Usuario>();

            foreach (UsuarioEF usuarioEF in usuarioEFs)
            {
                // Verificar si las entidades relacionadas no son null antes de acceder a ellas
                Usuario usuario = new Usuario
                {
                    UsuarioID = usuarioEF.UsuarioID,
                    RolID = usuarioEF.RolID,
                    UnidadID = usuarioEF.UnidadID,
                    Rol = usuarioEF.Rol != null ? new Rol
                    {
                        RolID = usuarioEF.Rol.RolID,
                        Nombre = usuarioEF.Rol.Nombre
                    } : null, //Si Rol es null se asigna null a Rol

                    Unidad = usuarioEF.Unidad != null ? new Unidad
                    {
                        UnidadID = usuarioEF.Unidad.UnidadID,
                        Nombre = usuarioEF.Unidad.Nombre
                    } : null, //Si Unidad es null se asigna null a Unidad

                    Persona = new Persona
                    {
                        PersonaID = usuarioEF.Persona.PersonaID,
                        Nombre = usuarioEF.Persona.Nombre,
                        PrimerApellido = usuarioEF.Persona.PrimerApellido,
                        SegundoApellido = usuarioEF.Persona.SegundoApellido
                    },
                    Contrasenia = usuarioEF.Contrasenia

                };

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public async Task<(string, bool)> ActualizarUsuario(Usuario usuario)
        {
            UsuarioEF usuarioEF = this._contexto.UsuariosEF
                .FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);
            if (usuarioEF == null)
            {
                return ("El usuario no se pudo actualizar", false);
            }
            PersonaEF personaEF = this._contexto.PersonaEFs
                .FirstOrDefault(p => p.PersonaID == usuario.PersonaID);
            if (personaEF == null)
            {
                return ("La persona no se pudo actualizar", false);
            }
            personaEF.Nombre = usuario.Persona.Nombre;
            personaEF.PrimerApellido = usuario.Persona.PrimerApellido;
            personaEF.SegundoApellido = usuario.Persona.SegundoApellido;
            usuarioEF.PersonaID = usuario.PersonaID;
            usuarioEF.UnidadID = usuario.UnidadID;
            usuarioEF.RolID = usuario.RolID;
            usuarioEF.Contrasenia = usuario.Contrasenia;

            return this._contexto.SaveChanges() > 0
        ? ("Actualizado correctamente", true)
        : ("Error al actualizar", false);
        }

        public async Task<(string, bool)> EliminarUsuario(int usuarioID)
        {
            UsuarioEF usuarioEF = this._contexto.UsuariosEF
                .FirstOrDefault(e => e.UsuarioID == usuarioID);
            if (usuarioEF == null)
            {
                return ("No existe el registro de usuario", false);
            }
            PersonaEF personaEF = this._contexto.PersonaEFs
                .FirstOrDefault(p => p.PersonaID == usuarioEF.PersonaID);
            if (personaEF == null)
            {
                return ("No existe el registro de persona", false);
            }
            this._contexto.UsuariosEF.Remove(usuarioEF);
            this._contexto.PersonaEFs.Remove(personaEF);
            return this._contexto.SaveChanges() > 0
       ? ("Eliminado correctamente", true)
       : ("Error al Eliminar", false);
        }
        //Nuevo
        public async Task<int> ContarUsuarios()
        {
            return await _contexto.UsuariosEF.CountAsync();
        }

        public async Task<List<Usuario>> ObtenerUsuariosPaginados(int skip, int pageSize)
        {
            var usuarioEFs = await _contexto.UsuariosEF
                .OrderBy(u => u.UsuarioID)
                .Skip(skip)
                .Take(pageSize)
                .Include(u => u.Rol)
                .Include(u => u.Unidad)
                .Include(u => u.Persona)
                .ToListAsync();
            return usuarioEFs.Select(usuarioEF => new Usuario
            {
                UsuarioID = usuarioEF.UsuarioID,
                RolID = usuarioEF.RolID,
                UnidadID = usuarioEF.UnidadID,
                Rol = usuarioEF.Rol != null ? new Rol
                {
                    RolID = usuarioEF.Rol.RolID,
                    Nombre = usuarioEF.Rol.Nombre
                } : null,
                Unidad = usuarioEF.Unidad != null ? new Unidad
                {
                    UnidadID = usuarioEF.Unidad.UnidadID,
                    Nombre = usuarioEF.Unidad.Nombre
                } : null,
                Persona = new Persona
                {
                    PersonaID = usuarioEF.Persona.PersonaID,
                    Nombre = usuarioEF.Persona.Nombre,
                    PrimerApellido = usuarioEF.Persona.PrimerApellido,
                    SegundoApellido = usuarioEF.Persona.SegundoApellido
                },
                Contrasenia = usuarioEF.Contrasenia
            }).ToList();
        }
    }
}
