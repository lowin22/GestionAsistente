﻿using GestionAsistentes.AccesoDatos.Contexto;
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
        public List<Asistente> listarAsistentes()
        {
            List<AsistenteEF> asistenteEFs = _contexto.AsistenteEFs
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
                    UnidadID = asistenteEF.UnidadID,
                    nombreUsuario = asistenteEF.nombreUsuario,
                    Accesos = asistenteEF.Accesos,
                    Contrasenia = asistenteEF.Contrasenia,
                    BadgeID = asistenteEF.BadgeID,
                    Encargado = new Encargado // Mapeo de Encargado
                    {
                        Persona = new Persona // Mapeo de Persona
                        {
                            Nombre = asistenteEF.Encargado.Persona.Nombre,
                            PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                            SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                        },
                        Unidad = new Unidad // Mapeo de Unidad
                        {
                            Nombre = asistenteEF.Encargado.Unidad.Nombre
                        }
                    }
                };

                asistentes.Add(asistente);
            }

            return asistentes;
        }
        public bool ModificarAsistente(Asistente asistente)
        {
            AsistenteEF asistenteEF = _contexto.AsistenteEFs.Find(asistente.AsistenteID);

            if (asistenteEF == null)
            {
                return false;
            }
            asistenteEF.UnidadID = asistente.UnidadID;
            asistenteEF.nombreUsuario = asistente.nombreUsuario;
            asistenteEF.Accesos = asistente.Accesos;
            asistenteEF.EncargadoID = asistente.EncargadoID;
            asistenteEF.Contrasenia = asistente.Contrasenia;
            asistenteEF.BadgeID = asistente.BadgeID;
            return _contexto.SaveChanges() > 0;
        }
        public bool EliminarAsistente(int AsistenteID)
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
                Encargado = new Encargado
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
                },
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Badge = new Badge
                {
                    BadgeID = asistenteEF.Badge.BadgeID,
                    Accesos = asistenteEF.Badge.Accesos,
                    Horario = asistenteEF.Badge.Horario,
                    Unidad = new Unidad
                    {
                        UnidadID = asistenteEF.Badge.Unidad.UnidadID,
                        Nombre = asistenteEF.Badge.Unidad.Nombre,
                    }
                },
                Unidad = new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre,
                }
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
                Badge = new Badge
                {
                    BadgeID = asistenteEF.Badge.BadgeID,
                    Accesos = asistenteEF.Badge?.Accesos,
                    Horario = asistenteEF.Badge?.Horario,
                    Unidad = asistenteEF.Badge?.Unidad != null ? new Unidad
                    {
                        UnidadID = asistenteEF.Badge.Unidad.UnidadID,
                        Nombre = asistenteEF.Badge.Unidad.Nombre,
                    } : null
                },
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre,
                } : null,
                Encargado = new Encargado
                {
                    Persona = new Persona
                    {
                        Nombre = asistenteEF.Encargado.Persona.Nombre,
                        PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                        SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                    }
                }
            }).ToList();
        }

    }
}