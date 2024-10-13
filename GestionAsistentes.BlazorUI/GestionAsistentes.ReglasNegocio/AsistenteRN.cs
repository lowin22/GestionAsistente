﻿using GestionAsistentes.AccesoDatos.EntidadesAD;
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
                if (asistente.UnidadID == 0)
                {
                    throw new Exception("La unidad no puede ser nula");
                };
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

    }
}