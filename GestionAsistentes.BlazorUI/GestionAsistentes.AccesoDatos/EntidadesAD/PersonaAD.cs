using GestionAsistentes.AccesoDatos.Contexto;
using GestionAsistentes.AccesoDatos.Modelos;
using GestionAsistentes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistentes.AccesoDatos.EntidadesAD
{
    public class PersonaAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public PersonaAD() { 
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarPersona(Persona persona)
        {

            PersonaEF personaEF = new PersonaEF
            {
                Nombre = persona.Nombre,
                PrimerApellido = persona.PrimerApellido,
                SegundoApellido = persona.SegundoApellido
            };
            this._contexto.PersonaEFs.Add(personaEF);
            return this._contexto.SaveChanges() >0;
        }
        public List<Persona> listarPersonas()
        {
            List<PersonaEF> personaEFs = _contexto.PersonaEFs.ToList();
            List<Persona> personas = new List<Persona>();

            foreach (PersonaEF personaEF in personaEFs) // Cambiado a PersonaEF
            {
                personas.Add(new Persona
                {
                    Nombre = personaEF.Nombre, // Cambiado a personaEF
                    PrimerApellido = personaEF.PrimerApellido,
                    SegundoApellido = personaEF.SegundoApellido
                });
            }

            return personas;
        }
        public bool ModificarPersona(Persona persona)
        {
            PersonaEF personaEF = _contexto.PersonaEFs.Find(persona.PersonaID);

            if (personaEF == null)
            {
                return false;
            }
            personaEF.Nombre = persona.Nombre; 
            personaEF.PrimerApellido = persona.PrimerApellido;
            personaEF.SegundoApellido = persona.SegundoApellido;
            return _contexto.SaveChanges() > 0;
        }
        public bool EliminarPersona(int PersonaID)
        {
            PersonaEF personaEF = _contexto.PersonaEFs.Find(PersonaID);
            if (personaEF == null)
            {
                return false;
            }
            _contexto.PersonaEFs.Remove(personaEF);
            return _contexto.SaveChanges() > 0;
        }
    }
}
