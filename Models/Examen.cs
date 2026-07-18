using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class Examen
    {
        public string Titulo { get; set; }
        public TipoExamen ExamenCategoria { get; set; }
        public Profesor Profesor { get; set; }
        public DateTime FechaDeCreacion { get; set; }

        // Properly initialized collections
        public List<Seccion> Secciones { get; set; }
        public List<Pregunta> Preguntas { get; set; }

        // Primary constructor
        public Examen(string titulo, TipoExamen categoria, Profesor profesor, DateTime fecha, List<Seccion> secciones, ObservableCollection<Pregunta> preguntas)
        {
            this.Titulo = titulo;
            this.ExamenCategoria = categoria;
            this.Profesor = profesor;
            this.FechaDeCreacion = fecha;
            this.Preguntas = new List<Pregunta>(preguntas);
            this.Secciones = secciones;

        }

        // Parameterless constructor for convenience
        public Examen()
        {
            this.Titulo = string.Empty;
            this.ExamenCategoria = default!;
            this.Profesor = null;
            this.FechaDeCreacion = DateTime.Now;
        }
    }
}
