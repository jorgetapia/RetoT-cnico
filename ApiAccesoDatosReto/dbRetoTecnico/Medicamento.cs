using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAccesoDatosReto.dbRetoTecnico;

public partial class Medicamento
{
    public int Idmedicamento { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public string? Nombre { get; set; }

    [Display(Name = "Concentración")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public string? Concentracion { get; set; }

    [Display(Name = "Forma")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public int? Idformafarmaceutica { get; set; }

    [Display(Name = "Precio")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public decimal? Precio { get; set; }

    [Display(Name = "Stock")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public int? Stock { get; set; }

    [Display(Name = "Presentación")]
    [Required(ErrorMessage = "El dato {0} es requerido.")]
    public string? Presentacion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual Formasfarmaceutica? IdformafarmaceuticaNavigation { get; set; }
}
