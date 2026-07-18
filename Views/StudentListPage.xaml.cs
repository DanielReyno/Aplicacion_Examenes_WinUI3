using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using TestsApplication.Models;

namespace TestsApplication.Views
{
    public sealed partial class StudentListPage : Page
    {
        // Shared collection so students persist across navigations
        // Made public so other pages (AddSectionPage) can read the current students.
        public static readonly ObservableCollection<Estudiante> SharedEstudiantes = new ObservableCollection<Estudiante>()
        {
            new Estudiante("Daniel","Reynoso"),
            new Estudiante("Jose", "Calderon"),
            new Estudiante("Maria","Porras")
        };

        public ObservableCollection<Estudiante> Estudiantes => SharedEstudiantes;

        public StudentListPage()
        {
            InitializeComponent();
        }

        private void AddNewStudentButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to AddStudentPage
            Frame.Navigate(typeof(AddStudentPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Estudiante estudiante)
            {
                // Add the passed student to the shared collection
                SharedEstudiantes.Add(estudiante);
            }
        }
    }
}
