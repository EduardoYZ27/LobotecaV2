using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class AutorELibro
    {
        public int Id { get; set; }
        public int? IdAutor { get; set; }
        public int? IdELibro { get; set; }

        public virtual Autor? IdAutorNavigation { get; set; }
        public virtual ELibro? IdELibroNavigation { get; set; }
    }
}
