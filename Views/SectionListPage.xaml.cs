using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using TestsApplication.Models;

namespace TestsApplication.Views
{
    public sealed partial class SectionListPage : Page
    {
        // Shared collection of sections so they persist across navigations
        public static ObservableCollection<Seccion> SharedSections = new ObservableCollection<Seccion>() 
        {
            new Seccion(1,"Seccion #1", new Profesor(), [], []),
            new Seccion(2,"Seccion #2", new Profesor(), [], [])
        };

        // Instance property used by x:Bind in XAML
        public ObservableCollection<Seccion> ItemListModel { get; set; }

        public SectionListPage()
        {
            // set the ItemListModel before XAML binds
            ItemListModel = SharedSections;
            InitializeComponent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddSectionPage));
        }
    }
}
