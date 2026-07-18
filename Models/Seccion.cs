using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class Seccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Profesor Profesor { get; set; }
        public List<Estudiante> Estudiantes { get; set; }
        public List<Examen> Examenes { get; set; }

        public Seccion(int id, string nombre, Profesor profesor, List<Estudiante> estudiantes, List<Examen> examenes)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Profesor = profesor;
            this.Estudiantes = estudiantes;
            this.Examenes = examenes;
        }
    }
}
