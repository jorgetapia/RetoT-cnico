using System;
using System.Collections.Generic;

namespace ApiAccesoDatosReto.dbRetoTecnico;

public partial class VUsuario
{
    public int Idusuario { get; set; }

    public string? Nombre { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public string? Usuario { get; set; }

    public string? Password { get; set; }

    public int? Idperfil { get; set; }

    public string? Estatus { get; set; }

    public int? Idestatus { get; set; }
}
