using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class EncargadoControlador
    {
        private readonly EncargadoRN encargadoRN;
        public UnidadControlador unidadControlador = new UnidadControlador();
        public List<Unidad> listaUnidades = new List<Unidad>();
        public List<Encargado> listaEncargados = new List<Encargado>();
        public string mensajeExito;
        public string mensajeError;
        public Encargado encargado = new Encargado
        {
            Persona = new Persona(),// Inicializa la persona dentro del encargado
            Unidad = new Unidad()// Inicializa la unidad dentro del encargado
        };
        public Encargado crearEncargado = new Encargado
        {
            Persona = new Persona(),
            Unidad = new Unidad()
        };

        public Encargado eliminarEncargado = new Encargado
        {
            Persona = new Persona(),
            Unidad = new Unidad()
        };

        public EncargadoControlador()
        {
            encargadoRN = new EncargadoRN();
        }
        public async Task EditarEncargado(int encargadoID)

        {
            Encargado actaulizarEncargado = new Encargado
            {
                Persona = new Persona()
            };

            actaulizarEncargado = listaEncargados.Find(x => x.EncargadoID == encargadoID);

            encargado.EncargadoID = actaulizarEncargado.EncargadoID;
            encargado.PersonaID = actaulizarEncargado.PersonaID;
            encargado.Persona.Nombre = actaulizarEncargado.Persona.Nombre;
            encargado.Persona.PrimerApellido = actaulizarEncargado.Persona.PrimerApellido;
            encargado.Persona.SegundoApellido = actaulizarEncargado.Persona.SegundoApellido;
            encargado.UnidadID = actaulizarEncargado.UnidadID;

        }
        public async Task seleccionarEncargadoEliminar(int eliminarEncargadoS)

        {

            eliminarEncargado = listaEncargados.Find(x => x.EncargadoID == eliminarEncargadoS);

        }
        public async Task listarEncargados()
        {
        
            this.listaEncargados = await encargadoRN.ListarEncargados();
        }
        public async Task listarUnidades()
        {
         this.listaUnidades= await unidadControlador.ListarUnidades();
        }
        public async Task<bool> registrarEncargado(Encargado encargado) { 
            return await encargadoRN.RegistrarEncardado(encargado);
        }
        public async Task<bool> RegistrarEncargado(Encargado encargado)
        {
            bool exito= false;
            try
            {
                 exito =  await encargadoRN.RegistrarEncardado(encargado);

                if (exito)
                {
                    mensajeExito = "Encargado registrado con éxito.";
                    mensajeError = null;
                    encargado = new Encargado
                    {
                        Persona = new Persona(),
                        Unidad = new Unidad()
                    };
                }
                else
                {
                    mensajeExito = null;
                    mensajeError = "Hubo un problema al registrar el encargado.";
                }
            }
            catch (Exception ex)
            {
                mensajeError = $"Error: {ex.Message}";
                mensajeExito = null;
            }
           return exito;
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
