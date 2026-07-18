using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class Estudiante
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Estudiante(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}
