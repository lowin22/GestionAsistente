﻿using GestionAsistentes.Entidades;
using GestionAsistentes.ReglasNegocio;

namespace GestionAsistentes.BlazorUI.Controlador
{
    public class EncargadoControlador
    {
        private readonly EncargadoRN encargadoRN;
        public EncargadoControlador()
        {
            encargadoRN = new EncargadoRN();
        }
        public async Task<bool> RegistrarEncargado(Encargado encargado)
        {
            return await encargadoRN.RegistrarEncardado(encargado);
        }

        public async Task<List<Encargado>> ListarEncargados()
        {
            return await encargadoRN.ListarEncargados();
        }

        public async Task<List<Encargado>> ListarEncargadosPorID(int? unidadID)
        {
            return await encargadoRN.ListarEncargadosPorID(unidadID);
        }
        public async Task<(string, bool)> ActualizarEncargado(Encargado encargado)
        {
            return await encargadoRN.ActualizarEncargado(encargado);
        }
        public async Task<(string, bool)> EliminarEncargado(int encargadoID)
        {
            return await encargadoRN.EliminarEncargado(encargadoID);
        }
    }
}
