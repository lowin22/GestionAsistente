using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
{
    public class AsistenteAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public AsistenteAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarAsistente(Asistente asistente)
        {
            PersonaEF personaEF = new PersonaEF
            {
                Nombre = asistente.Persona.Nombre,
                PrimerApellido = asistente.Persona.PrimerApellido,
                SegundoApellido = asistente.Persona.SegundoApellido
            };
            AsistenteEF asistenteEF = new AsistenteEF
            {
                Persona = personaEF,
                UnidadID = asistente.UnidadID,
                nombreUsuario = asistente.nombreUsuario,
                Accesos = asistente.Accesos,
                EncargadoID = asistente.EncargadoID,
                Contrasenia = asistente.Contrasenia,
                BadgeID = asistente.BadgeID,
            };
            this._contexto.AsistenteEFs.Add(asistenteEF);
            return this._contexto.SaveChanges() > 0;
        }
        public async Task<List<Asistente>> listarAsistentes()
        {
            List<AsistenteEF> asistenteEFs = _contexto.AsistenteEFs
                .Include(a => a.Persona)
                .Include(a => a.Encargado)          // Incluir Encargado
                    .ThenInclude(e => e.Persona)    // Incluir Persona del Encargado
                .Include(a => a.Unidad)             // Incluir Unidad
                .Include(a => a.Badge)              // Incluir Badge
                .ToList();

            List<Asistente> asistentes = new List<Asistente>();

            foreach (AsistenteEF asistenteEF in asistenteEFs)
            {
                // Mapear las propiedades de Asistente
                Asistente asistente = new Asistente
                {
                    AsistenteID = asistenteEF.AsistenteID,
                    EncargadoID = asistenteEF.Encargado.EncargadoID,
                    Persona = new Persona // Mapeo de Persona asistente
                    {
                        PersonaID = asistenteEF.Persona.PersonaID,
                        Nombre = asistenteEF.Persona.Nombre,
                        PrimerApellido = asistenteEF.Persona.PrimerApellido,
                        SegundoApellido = asistenteEF.Persona.SegundoApellido
                    },

                    UnidadID = asistenteEF.UnidadID,
                    // Mapeo de nombre de la Unidad
                    Unidad = asistenteEF.Unidad != null ? new Unidad // Asegúrate de que tu clase Asistente tenga una propiedad Unidad
                    {
                        UnidadID = asistenteEF.Unidad.UnidadID, // Asumiendo que Unidad tiene UnidadID
                        Nombre = asistenteEF.Unidad.Nombre // Mapeo del nombre de la unidad
                    } : null,
                    nombreUsuario = asistenteEF.nombreUsuario,
                    Accesos = asistenteEF.Accesos,
                    Contrasenia = asistenteEF.Contrasenia,
                    BadgeID = asistenteEF.BadgeID,
                    Encargado = asistenteEF.Encargado != null ? new Encargado // Mapeo de Encargado
                    {
                        EncargadoID = asistenteEF.Encargado.EncargadoID,
                        Persona = new Persona // Mapeo de Persona
                        {
                            Nombre = asistenteEF.Encargado.Persona.Nombre,
                            PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                            SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                        }
                    } : null
                };

                asistentes.Add(asistente);
            }

            return asistentes;
        }

        public async Task<bool> ModificarAsistente(Asistente asistente)
        {
            // Buscar el asistente por ID
            var asistenteEF = await _contexto.AsistenteEFs
                .FirstOrDefaultAsync(a => a.AsistenteID == asistente.AsistenteID);

            // Verificar si se encontró el asistente
            if (asistenteEF == null)
            {
                return false; // Si no se encontró, retorna falso
            }

            PersonaEF personaEF = this._contexto.PersonaEFs
                 .FirstOrDefault(p => p.PersonaID == asistente.Persona.PersonaID);
            if (personaEF == null)
            {
                return false;
            }
            personaEF.Nombre = asistente.Persona.Nombre;
            personaEF.PrimerApellido = asistente.Persona.PrimerApellido;
            personaEF.SegundoApellido = asistente.Persona.SegundoApellido;

            // Actualizar los demás campos del asistente
            asistenteEF.UnidadID = asistente.UnidadID;
            asistenteEF.nombreUsuario = asistente.nombreUsuario;
            asistenteEF.Accesos = asistente.Accesos;
            asistenteEF.EncargadoID = asistente.EncargadoID;
            asistenteEF.Contrasenia = asistente.Contrasenia;
            asistenteEF.BadgeID = asistente.BadgeID;

            // Guardar los cambios en la base de datos
            return await _contexto.SaveChangesAsync() > 0; // Asegúrate de usar SaveChangesAsync para mantener la asynchronía
        }



        public async Task<bool> EliminarAsistente(int? AsistenteID)
        {
            AsistenteEF asistenteEF = _contexto.AsistenteEFs.Find(AsistenteID);
            if (asistenteEF == null)
            {
                return false;
            }
            _contexto.AsistenteEFs.Remove(asistenteEF);
            return _contexto.SaveChanges() > 0;
        }
        public List<Asistente> BuscarAsistentePorNombre(string nombre)
        {
            var asistentesEF = _contexto.AsistenteEFs
                .Include(a => a.Badge)
                .Include(a => a.Unidad)
                .Include(a => a.Persona)
                .Include(a => a.Encargado)
                     .ThenInclude(e => e.Persona)
                .Where(a => a.Persona.Nombre == nombre)
                .ToList();

            if (asistentesEF == null || !asistentesEF.Any())
            {
                return new List<Asistente>();
            }


            return asistentesEF.Select(asistenteEF => new Asistente
            {
                UnidadID = asistenteEF.UnidadID,
                Accesos = asistenteEF.Accesos,
                Encargado = asistenteEF.Encargado != null ? new Encargado
                {
                    Persona = new Persona
                    {
                        Nombre = asistenteEF.Encargado.Persona.Nombre,
                        PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                        SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                    },
                    Unidad = new Unidad // Mapeo de Unidad
                    {
                        Nombre = asistenteEF.Encargado.Unidad.Nombre
                    }
                } : null,
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Badge = asistenteEF.Badge != null ? new Badge
                {
                    BadgeID = asistenteEF.Badge.BadgeID,
                    Accesos = asistenteEF.Badge.Accesos,
                    Horario = asistenteEF.Badge.Horario,
                    Unidad = new Unidad
                    {
                        UnidadID = asistenteEF.Badge.Unidad.UnidadID,
                        Nombre = asistenteEF.Badge.Unidad.Nombre,
                    }
                } : null,
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre,
                } : null,
            }).ToList(); // Convertimos la colección a una lista
        }
        public List<Asistente> BuscarAsistentePorEncargado(string encargado)
        {
            var asistentesEF = _contexto.AsistenteEFs
        .Include(a => a.Badge)
        .Include(a => a.Unidad)
        .Include(a => a.Encargado)
            .ThenInclude(e => e.Persona)
        .Where(a => a.Encargado != null && a.Encargado.Persona != null && a.Encargado.Persona.Nombre == encargado)
        .ToList();

            if (asistentesEF == null || !asistentesEF.Any())
            {
                return new List<Asistente>();
            }


            return asistentesEF.Select(asistenteEF => new Asistente
            {
                UnidadID = asistenteEF.UnidadID,
                Accesos = asistenteEF.Accesos,
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Badge = asistenteEF.Badge != null ? new Badge
                {
                    BadgeID = asistenteEF.Badge.BadgeID,
                    Accesos = asistenteEF.Badge?.Accesos,
                    Horario = asistenteEF.Badge?.Horario,
                    Unidad = asistenteEF.Badge?.Unidad != null ? new Unidad
                    {
                        UnidadID = asistenteEF.Badge.Unidad.UnidadID,
                        Nombre = asistenteEF.Badge.Unidad.Nombre,
                    } : null
                } : null,
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre,
                } : null,
                Encargado = asistenteEF.Encargado != null ? new Encargado
                {
                    Persona = new Persona
                    {
                        Nombre = asistenteEF.Encargado.Persona.Nombre,
                        PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                        SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                    }
                } : null
            }).ToList();
        }

    }
}
