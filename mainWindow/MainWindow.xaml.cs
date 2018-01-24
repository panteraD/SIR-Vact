using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using mainWindow.Properties;


namespace mainWindow
{
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;

        private String currentLang;

        private const String ENGLISH = "en-US";
        private const String RUSSIAN = "ru-RU";

        public MainWindow()
        {
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;

            string theoryDocName = "Theory.xps";
            XpsDocument theoryXpsDocument =
                new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, theoryDocName), FileAccess.Read);
            TheoryDocumentViewer.Document = theoryXpsDocument.GetFixedDocumentSequence();

            //old
            this.SetLanguageDictionary();



        }


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }


        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (viewModel != null)
            {
                viewModel.ShowPMass();
            }
        }

        private void UIElement_OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.ShowPMass();
                MessageBox.Show((string)FindResource("Time"));
            }
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case ENGLISH:
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);

                    break;
                case RUSSIAN:
                    dict.Source = new Uri("..\\Resources\\lang.ru-RU.xaml", UriKind.Relative);

                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);
                    break;
            }

            this.Resources.MergedDictionaries.Add(dict);
        }

        private void ChangeLanguage_OnClick(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();


            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case ENGLISH:
                    dict.Source = new Uri("..\\Resources\\lang.ru-RU.xaml", UriKind.Relative);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(RUSSIAN);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(RUSSIAN);
                    break;
                case RUSSIAN:
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(ENGLISH);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(ENGLISH);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\lang.xaml", UriKind.Relative);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(ENGLISH);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(ENGLISH);
                    break;
            }


            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dict);

        }


    }
}