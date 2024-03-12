using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KDSB20241103.Models
{
    public partial class TelefonoCliente
    {
        public int IdTelefono { get; set; }
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Telefono")]
        public string NumeroTelefono { get; set; } = null!;

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
    }
}
