using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestsApplication.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestsApplication.Views.Custom_Templates
{
    public sealed partial class ShowFreeAnswerQuestionTemplate : UserControl
    {

        public CancelMethodHandler CallbackFunction { get; set; }
        public CreateQuestion CreateCallbackFunction { get; set; }
        private PreguntaRespuestaLibre PreguntaModel { get; set; }
        public ShowFreeAnswerQuestionTemplate()
        {
            InitializeComponent();
        }

        public ShowFreeAnswerQuestionTemplate(PreguntaRespuestaLibre pregunta)
        {
            InitializeComponent();
            RichEditBoxContent.TextDocument.SetText(Microsoft.UI.Text.TextSetOptions.None, pregunta.Contenido);
            TextBoxContent.Text = pregunta.RespuestaCorrecta;
            PreguntaModel = pregunta;

        }

        private void CancelExamQuestion(object sender, RoutedEventArgs e)
        {
            CallbackFunction.Invoke();
        }

        private void CreateQuestion_Button(object sender, RoutedEventArgs e)
        {
            var titulo = "Hola mundo";
            string contenido = "";
            RichEditBoxContent.TextDocument.GetText(Microsoft.UI.Text.TextGetOptions.None, out contenido);
            string respuesta = TextBoxContent.Text;

            if(PreguntaModel != null)
            {
                PreguntaModel.Titulo = titulo;
                PreguntaModel.Contenido = contenido;
                PreguntaModel.RespuestaCorrecta = respuesta;
                CreateCallbackFunction.Invoke(PreguntaModel);
                return;
            }
            CreateCallbackFunction.Invoke(new PreguntaRespuestaLibre(1,titulo,contenido,respuesta));
        }
    }
}
