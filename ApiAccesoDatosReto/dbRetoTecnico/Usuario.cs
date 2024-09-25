using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAccesoDatosReto.dbRetoTecnico;

public partial class Usuario
{
    public int Idusuario { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public string? Nombre { get; set; }

    public DateTime? Fechacreacion { get; set; }

    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public string? Usuario1 { get; set; }

    [Required(ErrorMessage = "El dato {0} es requerido.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
     ErrorMessage = "El dato {0} debe tener al menos 8 caracteres, incluir una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
    public string Password { get; set; }

    public int? Idperfil { get; set; }

    public int? Estatus { get; set; }
}
