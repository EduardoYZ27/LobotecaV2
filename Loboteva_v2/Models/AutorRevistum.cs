using System;
using System.Collections.Generic;

namespace Loboteva_v2.Models
{
    public partial class AutorRevistum
    {
        public int Id { get; set; }
        public int? IdAutor { get; set; }
        public int? IdRevista { get; set; }

        public virtual Autor? IdAutorNavigation { get; set; }
        public virtual Revistum? IdRevistaNavigation { get; set; }
    }
}
