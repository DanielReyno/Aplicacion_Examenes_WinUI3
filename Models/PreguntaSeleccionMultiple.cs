using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class PreguntaSeleccionMultiple : Pregunta
    {
        public List<string> Opciones { get; set; }
        public string RespuestaCorrecta { get; set; }

        public PreguntaSeleccionMultiple(int id, string titulo, string contenido, List<string> opciones, string respuesta)
        {
            base.PreguntaId = id;
            base.Titulo = titulo;
            base.Contenido = contenido;
            this.Opciones = opciones;
            this.RespuestaCorrecta = respuesta;
        }
    }
}
