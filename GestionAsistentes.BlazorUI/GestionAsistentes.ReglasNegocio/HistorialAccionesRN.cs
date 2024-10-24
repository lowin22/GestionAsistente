﻿using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class HistorialAccionesRN
    {
        private readonly HistorialAccionesAD historialAccionesAD;
        public HistorialAccionesRN()
        {
            this.historialAccionesAD = new HistorialAccionesAD();
        }
        public bool RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            if (historialAcciones == null || string.IsNullOrWhiteSpace(historialAcciones.NombrePersona) || string.IsNullOrWhiteSpace(historialAcciones.Accion))
            {
                throw new ArgumentException("El historial de acciones es inválido.");
            }
            /**/
            //historialAcciones.Accion = $"{historialAcciones.Accion} en la unidad {historialAcciones.NombreUnidad}"; // Concatenación
            return historialAccionesAD.RegistrarHistorialAcciones(historialAcciones);
        }
        public List<HistorialAcciones> ListarHistorialPorFecha(DateTime fecha)
        {
            return historialAccionesAD.listarHistorialAcciones(fecha);
        }
        public List<HistorialAcciones> BuscarAccionPorPersona(string nombrePersona)
        {
            if (string.IsNullOrWhiteSpace(nombrePersona))
            {
                throw new ArgumentException("El nombre de la persona no puede estar vacío.");
            }

            return historialAccionesAD.BuscarAccionPorPersona(nombrePersona);
        }
        public List<HistorialAcciones> BuscarPorAccion(string accion)
        {
            if (string.IsNullOrWhiteSpace(accion))
            {
                throw new ArgumentException("La acción no puede estar vacía.");
            }

            return historialAccionesAD.BuscarPorAccion(accion);
        }
        /*Nuevo*/
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();
            historialAcciones = await this.historialAccionesAD.ListarHistorial();
            return historialAcciones;
        }

    }
}
