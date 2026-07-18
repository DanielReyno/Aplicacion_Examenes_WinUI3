using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestsApplication.Models;

namespace TestsApplication.Views
{
    public sealed partial class AddSectionPage : Page
    {
        // Bindable collections used by XAML
        public ObservableCollection<Estudiante> estudiantes { get; set; }
        public ObservableCollection<string> examenes { get; set; }

        public AddSectionPage()
        {
            // Load current students from StudentListPage shared collection
            estudiantes = new ObservableCollection<Estudiante>(StudentListPage.SharedEstudiantes);
            examenes = new ObservableCollection<string>(); // populate if you have exam models

            InitializeComponent();
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var nombre = SectionNameTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                var dialog = new ContentDialog
                {
                    Title = "Validación",
                    Content = "Ingresa un nombre para la sección.",
                    CloseButtonText = "Ok"
                };
                _ = await dialog.ShowAsync();
                return;
            }

            // get selected students
            var selectedStudents = StudentsListView.SelectedItems.Cast<Estudiante>().ToList();

            // create Seccion model (Id generation simple: count + 1)
            var newSection = new Seccion(
                id: SectionListPage.SharedSections.Count + 1,
                nombre: nombre,
                profesor: null,
                estudiantes: selectedStudents,
                examenes: new List<Examen>() // empty for now; adapt if you have exam models
            );

            // add to shared sections collection used by SectionListPage
            SectionListPage.SharedSections.Add(newSection);

            // go back to the list page (preserve existing instance)
            if (Frame?.CanGoBack == true)
                Frame.GoBack();
            else
                Frame?.Navigate(typeof(SectionListPage));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame?.CanGoBack == true)
                Frame.GoBack();
            else
                Frame?.Navigate(typeof(SectionListPage));
        }
    }
}
