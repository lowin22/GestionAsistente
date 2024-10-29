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
    public class AsistenteAD
    {
        private readonly GestionAsistenteContexto _contexto;
        private readonly BadgeAD badgeAD;
        private readonly ReporteBadgeAD reporteBadgeAD;
        public AsistenteAD()
        {
            this._contexto = new GestionAsistenteContexto();
            this.reporteBadgeAD = new ReporteBadgeAD();
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
                BadgeID = asistente.BadgeID
            };
            BadgeEF badgeEF = _contexto.BadgesEF.Find(asistente.BadgeID);
            if (badgeEF != null)
            {
                ReporteBadge reporteBadge = new ReporteBadge
                {
                    NombreUsuario = "Usuario prueba",
                    NumeroBadge = badgeEF.BadgeID,
                    NombreAsistente = asistente.Persona.Nombre,
                    Accion = "Asignación de tarjeta"
                };
                await reporteBadgeAD.RegistrarReporteBadge(reporteBadge);
                badgeEF.Ocupado = true;

            }

            this._contexto.AsistenteEFs.Add(asistenteEF);
            return this._contexto.SaveChanges() > 0;
        }
        public async Task<List<Asistente>> listarAsistentes()
        {
            try
            {
                List<AsistenteEF> asistenteEFs = await _contexto.AsistenteEFs
                    .Include(a => a.Persona)
                    .Include(a => a.Encargado)
                    .Include(a => a.Encargado.Persona)
                    .Include(a => a.Unidad)
                    .Include(a => a.Badge)
                    .ToListAsync(); // Usar versión asíncrona

                List<Asistente> asistentes = new List<Asistente>();

                foreach (AsistenteEF asistenteEF in asistenteEFs)
                {
                    if (asistenteEF == null) continue;

                    var asistente = new Asistente
                    {
                        AsistenteID = asistenteEF.AsistenteID,
                        EncargadoID = asistenteEF.Encargado?.EncargadoID,
                        nombreUsuario = asistenteEF.nombreUsuario,
                        Accesos = asistenteEF.Accesos,
                        Contrasenia = asistenteEF.Contrasenia,
                        BadgeID = asistenteEF.BadgeID,
                        UnidadID = asistenteEF.UnidadID,

                        // Mapeo seguro de Persona
                        Persona = asistenteEF.Persona != null ? new Persona
                        {
                            PersonaID = asistenteEF.Persona.PersonaID,
                            Nombre = asistenteEF.Persona.Nombre ?? string.Empty,
                            PrimerApellido = asistenteEF.Persona.PrimerApellido ?? string.Empty,
                            SegundoApellido = asistenteEF.Persona.SegundoApellido ?? string.Empty
                        } : null,

                        // Mapeo seguro de Unidad
                        Unidad = asistenteEF.Unidad != null ? new Unidad
                        {
                            UnidadID = asistenteEF.Unidad.UnidadID,
                            Nombre = asistenteEF.Unidad.Nombre ?? string.Empty
                        } : null,

                        // Mapeo seguro de Encargado
                        Encargado = asistenteEF.Encargado != null && asistenteEF.Encargado.Persona != null ?
                            new Encargado
                            {
                                EncargadoID = asistenteEF.Encargado.EncargadoID,
                                Persona = new Persona
                                {
                                    Nombre = asistenteEF.Encargado.Persona.Nombre ?? string.Empty,
                                    PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido ?? string.Empty,
                                    SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido ?? string.Empty
                                }
                            } : null
                    };

                    asistentes.Add(asistente);
                }

                return asistentes;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción como prefieras
                throw new Exception($"Error al listar asistentes: {ex.Message}");
            }
        }

        public async Task<bool> ModificarAsistente(Asistente asistente)
        {
            // Buscar el asistente por ID
            var asistenteEF = await _contexto.AsistenteEFs
                .FirstOrDefaultAsync(a => a.AsistenteID == asistente.AsistenteID);
            if (asistenteEF.BadgeID != null)
            {
                int? idBadge = asistenteEF.BadgeID;
                BadgeEF badgeEF = _contexto.BadgesEF.Find(idBadge);
                badgeEF.Ocupado = false;

                ReporteBadge reporteBadge = new ReporteBadge
                {
                    NombreUsuario = "Usuario prueba",
                    NumeroBadge = badgeEF.BadgeID,
                    NombreAsistente = asistente.Persona.Nombre,
                    Accion = "Eliminar asignación de tarjeta"
                };
                await reporteBadgeAD.RegistrarReporteBadge(reporteBadge);

            }

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
            BadgeEF badgeEFActual = _contexto.BadgesEF.Find(asistente.BadgeID);

            if (badgeEFActual != null)
            {
                ReporteBadge reporteBadge = new ReporteBadge
                {
                    NombreUsuario = "Usuario prueba",
                    NumeroBadge = badgeEFActual.BadgeID,
                    NombreAsistente = asistente.Persona.Nombre,
                    Accion = "Asignación de tarjeta"
                };
                await reporteBadgeAD.RegistrarReporteBadge(reporteBadge);

                badgeEFActual.Ocupado = true;

            }

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
        public async Task<List<Asistente>> BuscarAsistentePorNombre(string nombre)
        {
            var asistentesEF = _contexto.AsistenteEFs
            .Include(a => a.Badge)
            .Include(a => a.Unidad)
            .Include(a => a.Persona)
            .Include(a => a.Encargado)
                .ThenInclude(e => e.Persona)
            .Where(a => a.Persona.Nombre.Contains(nombre))
            .ToList();


            if (asistentesEF == null || !asistentesEF.Any())
            {
                return new List<Asistente>();
            }

            return asistentesEF.Select(asistenteEF => new Asistente
            {
                AsistenteID = asistenteEF.AsistenteID,
                EncargadoID = asistenteEF.Encargado?.EncargadoID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Accesos = asistenteEF.Accesos,
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                UnidadID = asistenteEF.UnidadID,

                // Mapeo seguro de Persona
                Persona = asistenteEF.Persona != null ? new Persona
                {
                    PersonaID = asistenteEF.Persona.PersonaID,
                    Nombre = asistenteEF.Persona.Nombre ?? string.Empty,
                    PrimerApellido = asistenteEF.Persona.PrimerApellido ?? string.Empty,
                    SegundoApellido = asistenteEF.Persona.SegundoApellido ?? string.Empty
                } : null,

                // Mapeo seguro de Unidad
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre ?? string.Empty
                } : null,

                // Mapeo seguro de Encargado
                Encargado = asistenteEF.Encargado != null && asistenteEF.Encargado.Persona != null ?
                            new Encargado
                            {
                                EncargadoID = asistenteEF.Encargado.EncargadoID,
                                Persona = new Persona
                                {
                                    Nombre = asistenteEF.Encargado.Persona.Nombre ?? string.Empty,
                                    PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido ?? string.Empty,
                                    SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido ?? string.Empty
                                }
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
