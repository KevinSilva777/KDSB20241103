using System;
using System.Collections.Generic;

namespace KDSB20241103.Models
{
    public partial class TelefonoCliente
    {
        public int IdTelefono { get; set; }
        public int IdCliente { get; set; }
        public string NumeroTelefono { get; set; } = null!;

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
    }
}
