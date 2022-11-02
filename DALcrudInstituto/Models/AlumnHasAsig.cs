using System;
using System.Collections.Generic;

namespace DALcrudInstituto.Models
{
    /// <summary>
    /// Tabla relacional entre alumno y asignaturas
    /// </summary>
    public partial class AlumnHasAsig
    {
        public long IdRel { get; set; }
        public long IdAlumno { get; set; }
        public long IdAsignatura { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
    }
}
