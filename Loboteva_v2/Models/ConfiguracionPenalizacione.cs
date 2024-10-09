using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class ConfiguracionPenalizacione
    {
        public ConfiguracionPenalizacione()
        {
            Sanciones = new HashSet<Sancione>();
        }

        public int IdPenalizacion { get; set; }
        public string? Nombre { get; set; }
        public decimal? Monto { get; set; }

        public virtual ICollection<Sancione> Sanciones { get; set; }
    }
}
