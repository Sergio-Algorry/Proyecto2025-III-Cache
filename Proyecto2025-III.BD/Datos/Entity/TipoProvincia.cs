using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025_III.BD.Datos.Entity
{
    [Index(nameof(Codigo), Name = "TipoProvincia_UQ", IsUnique = true)]
    public class TipoProvincia : TablaTipoBase
    {

    }
}
