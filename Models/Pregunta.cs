using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public abstract class Pregunta
    {
        public int PreguntaId { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}
