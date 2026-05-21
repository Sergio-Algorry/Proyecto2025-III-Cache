using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.Shared.DTO
{
    public class ProvinciaResumenDTO
    {
        public int Id { get; set; }
        public string NombrePais { get; set; } = "";
        public string CodProvincia { get; set; } = "";
        public string IGM_Id { get; set; } = "";
        public string NombreProvincia { get; set; } = "";
    }
}
