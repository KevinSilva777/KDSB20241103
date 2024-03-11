using System;
using System.Collections.Generic;

namespace KDSB20241103.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            TelefonoClientes = new HashSet<TelefonoCliente>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;

        public virtual ICollection<TelefonoCliente> TelefonoClientes { get; set; }
    }
}
