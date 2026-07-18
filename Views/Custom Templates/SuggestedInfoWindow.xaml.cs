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
using System.Collections.Specialized;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestsApplication.Views.Custom_Templates;

public sealed partial class SuggestedInfoWindow : UserControl
{
    // Fixed ObservableCollection initializer (valid C#)
    public ObservableCollection<string> TestingNames { get; set; } = new ObservableCollection<string> { "Seccion 1", "Seccion 2", "Seccion 3" };

    public string WindowsName { get; set; }
    public string SelectionMode { get; set; }

    public SuggestedInfoWindow()
    {
        InitializeComponent();

        // Ensure UI reflects the current collection state and stays in sync
        

    }
}
