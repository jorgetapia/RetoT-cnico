using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PortalRetoTécnico.Pages.Usuarios
{
    [ServiceFilter(typeof(SessionValidationFilter))]
    public class IndexModel(IConfiguration configuration) : PageModel
    {
        private readonly IConfiguration _configuration = configuration;

        public string urlApiUsuarios { get; set; } = "";


       
        public void OnGet()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                // Redirigir al login si la sesión no existe
                HttpContext.Response.Redirect("/Login");
            }
            // Obtener el valor desde appsettings.json
            urlApiUsuarios = _configuration["AppSettings:urlApiUsuarios"];
        }
    }
}
