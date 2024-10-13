using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class EncargadoRN
    {
        private readonly EncargadoAD encargadoAD;

        public EncargadoRN()
        {
            encargadoAD = new EncargadoAD();
        }
        public async Task<bool> RegistrarEncardado(Encargado encargado)
        {
            if (encargado != null)
            {
                encargado.Persona = new Persona
                {
                    Nombre = encargado.Persona.Nombre,
                    PrimerApellido = encargado.Persona.PrimerApellido,
                    SegundoApellido = encargado.Persona.SegundoApellido
                };
                if (encargado.UnidadID == 0)
                {
                    throw new Exception("La unidad no puede ser nula");
                }
                if (encargado.Persona.SegundoApellido == null)
                {
                    throw new Exception("La persona segundo no puede ser nula");
                }
                if (encargado.Persona.PrimerApellido == null)
                {
                    throw new Exception("La persona primer no puede ser nula");
                }
                if (encargado.Persona.Nombre == null)
                {
                    throw new Exception("La persona nombre no puede ser nula");
                }
            }
            return await encargadoAD.RegistrarEncargado(encargado);
        }
        public async Task<List<Encargado>> ListarEncargados()
        {
            List<Encargado> encargados = new List<Encargado>();
            encargados = await this.encargadoAD.ListarEncargados();
            return encargados;
        }

        public async Task<List<Encargado>> ListarEncargadosPorID(int unidadID)
        {
            List<Encargado> encargadosFiltrados = new List<Encargado>();
            encargadosFiltrados = await this.encargadoAD.ListarEncargadosPorID(unidadID);
            return encargadosFiltrados;
        }
    }
}
