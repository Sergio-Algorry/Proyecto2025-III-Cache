using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.BD.Datos
{
    // #error indices
    public class TablaTipoBase : EntityBase
    {
        [Required(ErrorMessage = "El Código es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public required string Tipo { get; set; }

        [MaxLength(100, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
