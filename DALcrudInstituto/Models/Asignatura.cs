using System;
using System.Collections.Generic;

namespace DALcrudInstituto.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            AlumnHasAsigs = new HashSet<AlumnHasAsig>();
        }

        public long IdAsignatura { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<AlumnHasAsig> AlumnHasAsigs { get; set; }
    }
}
