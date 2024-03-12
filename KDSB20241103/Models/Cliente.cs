using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KDSB20241103.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            TelefonoCliente = new List<TelefonoCliente>();
        }

        public int IdCliente { get; set; }
        [Display(Name ="Cliente")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string NombreCliente { get; set; } = null!;
        [Display(Name = "Fecha")]
        public DateTime? FechaRegistro { get; set; }

        public virtual IList<TelefonoCliente> TelefonoCliente { get; set; }
    }
}
