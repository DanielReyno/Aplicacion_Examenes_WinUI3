using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class Profesor
    {
        public string Nombre { get; set; }
        public ObservableCollection<Seccion> Secciones { get; set; }
        public ObservableCollection<Examen> Examenes { get; set; }
        public ObservableCollection<Estudiante>  Estudiantes { get; set; }
    }
}
