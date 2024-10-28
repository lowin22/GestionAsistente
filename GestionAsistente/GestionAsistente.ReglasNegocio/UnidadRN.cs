using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class UnidadRN
    {
        private readonly UnidadAD unidadAD;
        public UnidadRN() { 
            this.unidadAD = new UnidadAD();
        }
        public async Task<bool> RegistrarUnidad(Unidad unidad)
        {
            if (unidad == null)
            {
                throw new ArgumentNullException(nameof(unidad));
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                throw new Exception("El nombre de la unidad es obligatorio.");
            }
            if (int.TryParse(unidad.Nombre, out _))
            {
                throw new Exception("El nombre de la unidad no puede ser solo numérico.");
            }
            //var unidadesExistentes = await unidadAD.listarUnidades();
            //if (unidadesExistentes.Any(u => u.Nombre.Equals(unidad.Nombre, StringComparison.OrdinalIgnoreCase)))
            //{
            //    throw new Exception("Ya existe una unidad con el mismo nombre.");
            //}
            var unidadesExistentes = await unidadAD.listarUnidades();
            bool existeNombreRepetido = unidadesExistentes.Any(u => u.Nombre.ToLower() == unidad.Nombre.ToLower());

            if (existeNombreRepetido)
            {
                throw new Exception("Ya existe una unidad con este nombre.");
            }
            return await unidadAD.RegistrarUnidad(unidad);
        }
        public async Task<List<Unidad>> ListarUnidades()
        {
            List<Unidad> unidades = new List<Unidad>();
            unidades = await this.unidadAD.listarUnidades();
            return unidades;
        }
        public async Task<(string, bool)> ActualizarUnidad(Unidad unidad)
        {
            if (unidad != null)
            {
                if (unidad.UnidadID == 0)
                {
                    return ("La unidad es erronea", false);
                }
                if (unidad.Nombre == null)
                {
                    return ("El nombre de la unidad es necesario", false);
                }
                if (unidad.Nombre == null)
                {
                    return ("La unidad ya existe", false);
                }
            }
            return await unidadAD.ActualizarUnidad(unidad);
        }
        public async Task<(string, bool)> EliminarUnidad(int unidadID)
        {
            return await unidadAD.EliminarUnidad(unidadID);
        }
    }
}
