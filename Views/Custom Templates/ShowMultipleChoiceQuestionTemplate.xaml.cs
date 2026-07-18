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
    public sealed partial class ShowMultipleChoiceQuestionTemplate : UserControl
    {

        public CancelMethodHandler CallbackFunction { get; set; }
        public CreateQuestion CreateCallback { get; set; }
        private PreguntaSeleccionMultiple PreguntaModel { get; set; }
        public ShowMultipleChoiceQuestionTemplate()
        {
            InitializeComponent();
        }
        public ShowMultipleChoiceQuestionTemplate(PreguntaSeleccionMultiple pregunta)
        {
            InitializeComponent();
            RichEditBoxContent.TextDocument.SetText(Microsoft.UI.Text.TextSetOptions.None, pregunta.Contenido);
            Opcion1.Text = pregunta.Opciones[0];
            Opcion2.Text = pregunta.Opciones[1];
            Opcion3.Text = pregunta.Opciones[2];
            Opcion4.Text = pregunta.Opciones[3];
            RadioButtonsOptions.SelectedIndex = pregunta.Opciones.IndexOf(pregunta.RespuestaCorrecta);
            PreguntaModel = pregunta;
        }

        private void CancelExamQuestion(object sender, RoutedEventArgs e)
        {

            CallbackFunction?.Invoke();
        }

        private void CreateQuestion_Button(object sender, RoutedEventArgs e)
        {
            var titulo = "Hola mundo";
            string contenido;
            RichEditBoxContent.TextDocument.GetText(Microsoft.UI.Text.TextGetOptions.None,out contenido);
            var respuesta = RadioButtonsOptions.SelectedIndex;
            List<string> opciones = [Opcion1.Text, Opcion2.Text, Opcion3.Text, Opcion4.Text];

            if(PreguntaModel != null)
            {
                PreguntaModel.Titulo = titulo;
                PreguntaModel.Contenido = contenido;
                PreguntaModel.Opciones = opciones;
                PreguntaModel.RespuestaCorrecta = opciones[respuesta];
                CreateCallback.Invoke(PreguntaModel);
                return;
            }
            CreateCallback.Invoke(new PreguntaSeleccionMultiple(1,titulo,contenido, opciones, opciones[respuesta]));
        }

        private void RadioButtonsOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateButton.IsEnabled = true;
        }
    }
}
