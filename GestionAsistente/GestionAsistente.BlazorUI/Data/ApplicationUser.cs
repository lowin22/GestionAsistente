using GestionAsistente.AccesoDatos.Modelos;
using Microsoft.AspNetCore.Identity;

namespace GestionAsistente.BlazorUI.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? UnidadID { get; set; }
        public virtual UnidadEF Unidad { get; set; }

    }

}
