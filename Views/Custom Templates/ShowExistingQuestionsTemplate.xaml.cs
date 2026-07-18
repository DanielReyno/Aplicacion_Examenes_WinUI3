using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using TestsApplication.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestsApplication.Views.Custom_Templates;

public delegate void CancelMethodHandler();
public delegate void CreateQuestion(Pregunta pregunta);


public class MyDataTemplateSelector : DataTemplateSelector
{

    public DataTemplate MultipleChoiceDataTemplate { get; set; }
    public DataTemplate TrueOrFalseDataTemplate { get; set; }
    public DataTemplate FreeAnswerDataTemplate { get; set; }
    public DataTemplate DefaultDataTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item)
    {
        if (item.GetType() == typeof(PreguntaSeleccionMultiple))
        {
            return MultipleChoiceDataTemplate;
        }
        else if (item.GetType() == typeof(PreguntaRespuestaLibre))
        {
            return FreeAnswerDataTemplate;
        }
        else if (item.GetType() == typeof(PreguntaFalsoVerdadero))
        {
            return TrueOrFalseDataTemplate;
        }
        else
        {
            return DefaultDataTemplate;
        }

    }
}
public sealed partial class ShowExistingQuestionsTemplate : UserControl
{
    public static ObservableCollection<Pregunta> SharedPreguntas { get; set; } = [];
  

    public ObservableCollection<Pregunta> PreguntasTemp
    {
        get { return SharedPreguntas; }
        set { SharedPreguntas = value; }
    }
    public ContentControl DynamicLayoutReference { get; set; }
    public ShowTrueOrFalseQuestionTemplate TrueOrFalseQuestion { get; set; }
    public ShowMultipleChoiceQuestionTemplate MultipleChoiceQuestion { get; set; }
    public ShowFreeAnswerQuestionTemplate FreeAnswerQuestion { get; set; }
    private UserControl GoBackUserControl { get; set; }
    private bool UpdateModeEnable { get; set; } = false;
    public int EditingItemIndex { get; set; }

    public ShowExistingQuestionsTemplate()
    {
        InitializeComponent();
    }
    public ShowExistingQuestionsTemplate(ContentControl control)
    {
        InitializeComponent();
        DynamicLayoutReference = control;
        
    }

    private void CancelExamQuestion()
    {
        DynamicLayoutReference.Content = GoBackUserControl;
    }

    private void CrearPreguntaFalsoVerdadero(Pregunta pregunta)
    {
        SharedPreguntas.Add(pregunta);
        DynamicLayoutReference.Content = GoBackUserControl;
    }

    private void CreateMultipleChoiceQuestion(Pregunta pregunta)
    {
        SharedPreguntas.Add(pregunta);
        DynamicLayoutReference.Content = GoBackUserControl;
    }

    private void CrearPreguntaRespuestaLibre(Pregunta pregunta)
    {
        SharedPreguntas.Add(pregunta);
        DynamicLayoutReference.Content = GoBackUserControl;
    }

    private void UpdateQuestion(Pregunta pregunta)
    {
        SharedPreguntas[EditingItemIndex] = pregunta;
        DynamicLayoutReference.Content = GoBackUserControl;
        UpdateModeEnable = false;

    }

    public int CalculateRadioButtonsIndex(PreguntaFalsoVerdadero pregunta)
    {
        if (pregunta.RespuestaCorrecta == true) return 0;
        return 1;
    }

    private void TrueOrFalseQuestion_Button(object sender, RoutedEventArgs e)
    {
        GoBackUserControl = (UserControl)DynamicLayoutReference.Content;

        if (sender is Button boton)
        {
            if (boton.DataContext is PreguntaFalsoVerdadero pregunta)
            {
                UpdateModeEnable = true;
                EditingItemIndex = PreguntasTemp.IndexOf(pregunta);
                TrueOrFalseQuestion = new ShowTrueOrFalseQuestionTemplate(pregunta);
                DynamicLayoutReference.Content = TrueOrFalseQuestion;
                TrueOrFalseQuestion.CancelCallbackFunction += CancelExamQuestion;
                TrueOrFalseQuestion.CreateCallbackFunction += UpdateQuestion;
                return;
            }
        }
        TrueOrFalseQuestion = new ShowTrueOrFalseQuestionTemplate();
        DynamicLayoutReference.Content = TrueOrFalseQuestion;
        TrueOrFalseQuestion.CancelCallbackFunction += CancelExamQuestion;
        TrueOrFalseQuestion.CreateCallbackFunction += CrearPreguntaFalsoVerdadero;

    }

    private void MultipleChoiceQuestion_Button(object sender, RoutedEventArgs e)
    {
        //Guardamos la referencia del controlador anterior para regresarnos posteriormente
        GoBackUserControl = (UserControl)DynamicLayoutReference.Content;

        //Logica para editar la pregunta
        if(sender is Button boton)
        {
            if(boton.DataContext is PreguntaSeleccionMultiple pregunta)
            {
                UpdateModeEnable = true;
                EditingItemIndex = PreguntasTemp.IndexOf(pregunta);
                MultipleChoiceQuestion = new ShowMultipleChoiceQuestionTemplate(pregunta);
                DynamicLayoutReference.Content = MultipleChoiceQuestion;
                MultipleChoiceQuestion.CallbackFunction += CancelExamQuestion;
                MultipleChoiceQuestion.CreateCallback += UpdateQuestion;
                return;
            }
        }

        //Logica para Crear la pregunta
        MultipleChoiceQuestion = new ShowMultipleChoiceQuestionTemplate();
        DynamicLayoutReference.Content = MultipleChoiceQuestion;
        MultipleChoiceQuestion.CallbackFunction += CancelExamQuestion;
        MultipleChoiceQuestion.CreateCallback += CreateMultipleChoiceQuestion;
    }

    private void FreeAnswerQuestion_Button(object sender, RoutedEventArgs e)
    {
        GoBackUserControl = (UserControl)DynamicLayoutReference.Content;

        if (sender is Button boton)
        {
            if (boton.DataContext is PreguntaRespuestaLibre pregunta)
            {
                UpdateModeEnable = true;
                EditingItemIndex = PreguntasTemp.IndexOf(pregunta);
                FreeAnswerQuestion = new ShowFreeAnswerQuestionTemplate(pregunta);
                DynamicLayoutReference.Content = FreeAnswerQuestion;
                FreeAnswerQuestion.CallbackFunction += CancelExamQuestion;
                FreeAnswerQuestion.CreateCallbackFunction += UpdateQuestion;
                return;
            }
        }

        FreeAnswerQuestion = new ShowFreeAnswerQuestionTemplate();
        DynamicLayoutReference.Content = FreeAnswerQuestion;
        FreeAnswerQuestion.CallbackFunction += CancelExamQuestion;
        FreeAnswerQuestion.CreateCallbackFunction += CrearPreguntaRespuestaLibre;
    }

    private void DeleteQuestion_Button(object sender, RoutedEventArgs e)
    {
        if (sender is Button boton) 
        {
            if (boton.DataContext is Pregunta pregunta)
            {
                SharedPreguntas.Remove(pregunta);
            }
        }
    }

    private void Expander_LostFocus(object sender, RoutedEventArgs e)
    {
        if(sender is Expander expander)
        {
            expander.IsExpanded = false;
        }
    }
}
