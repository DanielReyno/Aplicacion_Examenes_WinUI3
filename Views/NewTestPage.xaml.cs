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
using System.Collections.ObjectModel;
using TestsApplication.Views.Custom_Templates;
using TestsApplication.Models;
using Windows.System.Profile;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestsApplication.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NewTestPage : Page
{

    public UserControl Testing { get; set; }
    public ShowExistingQuestionsTemplate ExistingQuestionsTemplate { get; set; } = new ShowExistingQuestionsTemplate();
    public ShowTrueOrFalseQuestionTemplate TrueOrFalseQuestionTemplate { get; set; }
    public ObservableCollection<string> ExamQuestions { get; set; } = ["Pregunta #1", "Pregunta #2", "Pregunta #3", "Pregunta #4"];
    public ObservableCollection<Seccion> ListadoSecciones { get; set; } = new ObservableCollection<Seccion>(SectionListPage.SharedSections);
    private Examen? EditingExam;
    public NewTestPage()
    {
        InitializeComponent();
        //ExistingQuestionsTemplate = new ShowExistingQuestionsTemplate(DynamicContentControl);
        DynamicContentControl.Content = new ShowExistingQuestionsTemplate(DynamicContentControl);
    }

    private async void CancelExamQuestion(object sender, RoutedEventArgs e)
    {

        ContentDialog dialog = new ContentDialog()
        {
            XamlRoot = this.XamlRoot,
            Title = "Confirmar accion",
            Content = "Desea continuar con la operacion ? los cambios realizados no seran guardados.",
            PrimaryButtonText = "Aceptar",
            CloseButtonText = "Cancelar"
        };

        ContentDialogResult result = await dialog.ShowAsync();

        if(result == ContentDialogResult.Primary)
        {
            ShowExistingQuestionsTemplate.SharedPreguntas.Clear();
            Frame.GoBack();
        }
    }

    private void CreateExamButton_Click(object sender, RoutedEventArgs e)
    {
        //if (SectionsListView.SelectedItems.Count == 0)
        //{
        //    TeachingTipBox.Target = SectionsListView;
        //    TeachingTipBox.Title = "Campo Secciones vacio";
        //    TeachingTipBox.Subtitle = "Seleccione las secciones correspondiente al examen";
        //    SectionsListView.Focus(FocusState.Pointer);
        //    TeachingTipBox.IsOpen = true;
        //    return;
        //}
        if (ExamTitle.Text == "")
        {
            TeachingTipBox.Target = ExamTitle;
            TeachingTipBox.Title = "Campo Titulo vacio";
            ExamTitle.Focus(FocusState.Pointer);
            TeachingTipBox.IsOpen = true;
            return;
        }
        else if (ComboBoxCategory.SelectedIndex == -1)
        {
            TeachingTipBox.Target = ComboBoxCategory;
            TeachingTipBox.Title = "Campo categoria vacio";
            ComboBoxCategory.Focus(FocusState.Pointer);
            TeachingTipBox.IsOpen = true;
            return;
        }
        else if (ShowExistingQuestionsTemplate.SharedPreguntas.Count == 0)
        {
            TeachingTipBox.Target = DynamicContentControl;
            TeachingTipBox.Title = "Listado de preguntas vacio";
            TeachingTipBox.Subtitle = "Agregue unas cuantas preguntas";
            TeachingTipBox.PreferredPlacement = TeachingTipPlacementMode.Center;
            TeachingTipBox.IsOpen = true;
            return;
        }

        var titulo = ExamTitle.Text;
        var categoria = (TipoExamen)ComboBoxCategory.SelectedIndex;
        var profesor = new Profesor();
        var fechaDeCreacion = DateTime.Now;
        var preguntas = ShowExistingQuestionsTemplate.SharedPreguntas;
        var secciones = SectionsListView.SelectedItems.Cast<Seccion>().ToList();

        if (EditingExam != null)
        {
            EditingExam.Titulo = titulo;
            EditingExam.ExamenCategoria = categoria;
            EditingExam.Profesor = profesor;
            EditingExam.FechaDeCreacion = fechaDeCreacion;
            EditingExam.Preguntas = new List<Pregunta>(preguntas);
            EditingExam.Secciones = secciones;
            EditingExam = null;

            Frame.GoBack();
            ShowExistingQuestionsTemplate.SharedPreguntas.Clear();
            return;
        }

        TestListPage.SharedTests.Add(new Examen(ExamTitle.Text, (TipoExamen)ComboBoxCategory.SelectedIndex, new Profesor(), DateTime.Now, preguntas: ShowExistingQuestionsTemplate.SharedPreguntas, secciones: SectionsListView.SelectedItems.Cast<Seccion>().ToList()));
      
        
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
            ShowExistingQuestionsTemplate.SharedPreguntas.Clear();
        }

    }

    override protected void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is Examen examen)
        {
            ExamTitle.Text = examen.Titulo;
            ComboBoxCategory.SelectedIndex = (int)examen.ExamenCategoria;
            ShowExistingQuestionsTemplate.SharedPreguntas = new ObservableCollection<Pregunta>(examen.Preguntas);
            EditingExam = examen;
            CreateExamButton.Content = "Editar";
        }
    }
}
