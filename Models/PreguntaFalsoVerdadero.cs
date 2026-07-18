using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsApplication.Models
{
    public class PreguntaFalsoVerdadero : Pregunta
    {
        public bool RespuestaCorrecta { get; set; }

        public PreguntaFalsoVerdadero(int Id, string titulo, string contenido, bool respuesta)
        {
            base.PreguntaId = Id;
            base.Titulo = titulo;
            base.Contenido = contenido;
            this.RespuestaCorrecta = respuesta;
        }

        public int GetRespuestaCorrectaIndex()
        {
            if (this.RespuestaCorrecta == true) return 0;
            return 1;
        }
    }
}
