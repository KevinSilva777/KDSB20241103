using System;
using System.Collections.Generic;

namespace KDSB20241103.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            TelefonoCliente = new List<TelefonoCliente>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }

        public virtual IList<TelefonoCliente> TelefonoCliente { get; set; }
    }
}
