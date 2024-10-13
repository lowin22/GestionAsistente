using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
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
            return await unidadAD.RegistrarUnidad(unidad);
        }
        public async Task<List<Unidad>> ListasUnidades()
        {
            List<Unidad> unidades = new List<Unidad>();
            unidades = await this.unidadAD.listarUnidades();
            return unidades;
        }
    }
}
