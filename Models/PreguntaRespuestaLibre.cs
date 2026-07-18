using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class PreguntaRespuestaLibre: Pregunta
    {
        public string RespuestaCorrecta { get; set; }

        public PreguntaRespuestaLibre(int id, string titulo, string contenido, string respuesta)
        {
            base.PreguntaId = id;
            base.Titulo = titulo;
            base.Contenido = contenido;
            this.RespuestaCorrecta = respuesta;
        }

        
    }
}
