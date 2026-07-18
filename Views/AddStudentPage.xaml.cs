using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using TestsApplication.Models;

namespace TestsApplication.Views
{
    public sealed partial class AddStudentPage : Page
    {
        public AddStudentPage()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var nombre = NombreTextBox.Text?.Trim() ?? string.Empty;
            var apellido = ApellidoTextBox.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(apellido))
            {
                // Minimal validation: do not create empty student
                var dialog = new ContentDialog
                {
                    Title = "Validación",
                    Content = "Ingresa al menos un nombre o apellido.",
                    CloseButtonText = "Ok"
                };

                _ = dialog.ShowAsync();
                return;
            }

            var estudiante = new Estudiante(nombre, apellido);

            // Navigate back to StudentListPage and pass the new student as a parameter.
            // StudentListPage will add it to the shared collection in OnNavigatedTo.
            Frame?.Navigate(typeof(StudentListPage), estudiante);
            Frame?.BackStack.Remove(Frame.BackStack.LastOrDefault());
            Frame?.BackStack.Remove(Frame.BackStack.LastOrDefault());// Remove AddStudentPage from back stack

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame?.CanGoBack == true)
                Frame.GoBack();
            else
                Frame?.Navigate(typeof(StudentListPage));
        }
    }
}
