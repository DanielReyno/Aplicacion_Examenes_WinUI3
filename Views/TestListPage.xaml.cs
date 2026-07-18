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
using TestsApplication.Models;

namespace TestsApplication.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TestListPage : Page
{
    // Shared list so new tests added from other pages are visible here
    public static readonly ObservableCollection<Examen> SharedTests = new ObservableCollection<Examen>();

    // Instance property used by x:Bind in XAML
    public ObservableCollection<Examen> TestsList { get; set; }

    public TestListPage()
    {
        // expose the shared collection before XAML binds
        TestsList = SharedTests;
        InitializeComponent();
    }

    

    private void TestItemView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if(e.ClickedItem is Examen examen)
        {
            Frame.Navigate(typeof(NewTestPage), examen);
        }
    }
}
