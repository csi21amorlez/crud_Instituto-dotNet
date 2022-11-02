using System;
using System.Collections.Generic;

namespace DALcrudInstituto.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            AlumnHasAsigs = new HashSet<AlumnHasAsig>();
        }

        public long IdAlumno { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public virtual ICollection<AlumnHasAsig> AlumnHasAsigs { get; set; }
    }
}
