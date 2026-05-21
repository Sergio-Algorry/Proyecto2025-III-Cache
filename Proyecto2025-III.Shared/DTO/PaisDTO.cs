using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.Shared.DTO
{
    public class PaisDTO
    {
        [Required(ErrorMessage = "El codigo de país internacional es obligatorio")]
        [MaxLength(3, ErrorMessage = "Maxima cant. caracteres 3")]
        public string Alpha3Code { get; set; } = "";

        [Required(ErrorMessage = "El Código de País de INDEC de 3 caracteres Numericos  es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string CodIndec { get; set; } = "";

        public string CodLlamada { get; set; } = "";

        [Required(ErrorMessage = "El Nombre del País es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El campo Código del País tiene como máximo {1} caracteres.")]
        public string NombrePais { get; set; } = "";
    }
}
