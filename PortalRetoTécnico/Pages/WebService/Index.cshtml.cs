using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Xml;


namespace PortalRetoTécnico.Pages.WebService
{
    [ServiceFilter(typeof(SessionValidationFilter))]
    public class IndexModel : PageModel
    {       
        public void  OnGet()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                // Redirigir al login si la sesión no existe
                HttpContext.Response.Redirect("/Login");
            }
        }     

    }
}

