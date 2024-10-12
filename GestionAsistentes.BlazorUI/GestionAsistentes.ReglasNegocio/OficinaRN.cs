using GestionAsistentes.AccesoDatos.EntidadesAD;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.ReglasNegocio
{
    public class OficinaRN
    {
        private readonly OficinaAD oficinaAD;
        public OficinaRN()
        {
            oficinaAD = new OficinaAD();
        }
        public List<Oficina> getOficinas() { 
            List<OficinaEF> oficinasEF= this.oficinaAD.ObtenerOficinas();
            List<Oficina> oficinas = new List<Oficina>();
            foreach (var oficinaEF in oficinasEF)
            {
                Oficina oficina = new Oficina
                {
                    Nombre = oficinaEF.Nombre,
                    OficinaID = oficinaEF.OficinaID
                };
                oficinas.Add(oficina);
            }
           return oficinas;

            
        }
    }
}
