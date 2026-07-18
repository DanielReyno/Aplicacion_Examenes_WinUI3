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

namespace TestsApplication.Views.Custom_Templates;


public sealed partial class ShowTrueOrFalseQuestionTemplate : UserControl
{

    public CancelMethodHandler CancelCallbackFunction { get; set; }
    public CreateQuestion CreateCallbackFunction { get; set; }
    private PreguntaFalsoVerdadero PreguntaModel { get; set; }
    public ShowTrueOrFalseQuestionTemplate()
    {
        InitializeComponent();
    }

    public ShowTrueOrFalseQuestionTemplate(PreguntaFalsoVerdadero pregunta)
    {
        InitializeComponent();
        RichEditBoxContent.TextDocument.SetText(Microsoft.UI.Text.TextSetOptions.None, pregunta.Contenido);
        if (pregunta.RespuestaCorrecta == true) RadioButtonsOptions.SelectedIndex = 0;
        else RadioButtonsOptions.SelectedIndex = 1;

        PreguntaModel = pregunta;
    }


    private void CancelExamQuestion(object sender, RoutedEventArgs e)
    {
        CancelCallbackFunction.Invoke();
    }

    private void CreateExamQuestion(object sender, RoutedEventArgs e)
    {

        var titulo = "Hola mundo";
        string contenido = "";
        bool respuesta = false;
        RichEditBoxContent.TextDocument.GetText(Microsoft.UI.Text.TextGetOptions.None, out contenido);
        if( RadioButtonsOptions.SelectedItem is RadioButton rb)
        {
            if (rb.Content.ToString() == "Verdadero") respuesta = true;
            respuesta = false;
        }

        if(PreguntaModel != null)
        {
            PreguntaModel.Titulo = titulo;
            PreguntaModel.Contenido = contenido;
            PreguntaModel.RespuestaCorrecta = respuesta;
            CreateCallbackFunction.Invoke(PreguntaModel);
            return;
        }

        CreateCallbackFunction.Invoke(new PreguntaFalsoVerdadero(1, titulo, contenido, respuesta));
    }
       
}
