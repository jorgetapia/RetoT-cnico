using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace PortalRetoTécnico.Pages
{
    public class LoginModel(IConfiguration configuration) : PageModel
    {
        private readonly IConfiguration _configuration = configuration;
        public string urlApi { get; set; } = "";
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        [BindProperty]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string usuario { get; set; }

        [BindProperty]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Obtener el valor desde appsettings.json
            urlApi = _configuration["AppSettings:urlApiLogin"];
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Crear la solicitud POST a la API de autenticación
            using (HttpClient client = new HttpClient())
            {
                var loginRequest = new { usuario = usuario, Password = Password };
                var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(urlApi, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Leer el token o cualquier otra respuesta
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

                    // Crear sesión si el login es exitoso
                    //HttpContext.Session.SetString("UserToken", authResponse.Token);
                    HttpContext.Session.SetString("Username", usuario);

                    // Redirigir a los módulos (Home page o página de inicio)
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError("datosIncorrectos","Nombre de usuario o contraseña incorrectos.");
                    return Page();
                }
            }
        }

        public class AuthResponse
        {
            public string Token { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }


    }
}

