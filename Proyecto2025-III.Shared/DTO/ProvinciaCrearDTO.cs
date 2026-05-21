using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.Shared.DTO
{
    public class ProvinciaCrearDTO
    {
        /// <summary>
        /// Gets or sets the identifier of the country.
        /// </summary>
        [Required(ErrorMessage = "El País es obligatorio.")]
        public int PaisId { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the type of province.
        /// </summary>
        [Required(ErrorMessage = "El tipo de provincia es obligatorio.")]
        public int TipoProvinciaId { get; set; }

        [Required(ErrorMessage = "El Código IGM es obligatorio.")]
        [MaxLength(2, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string IGM_Id { get; set; } = "";

        [Required(ErrorMessage = "El Nombre de la Provincia es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string NombreProvincia { get; set; } = "";
    }
}
