using System;
using System.Collections.Generic;

namespace ApiAccesoDatosReto.dbRetoTecnico;

public partial class VMedicamento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Concentracion { get; set; }

    public int? IdFormaFarmaceutica { get; set; }

    public string? FormasFarmaceuticas { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public string? Presentacion { get; set; }

    public string? Estatus { get; set; }

    public int? IdEstatus { get; set; }
}
