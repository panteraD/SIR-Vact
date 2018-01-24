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


            this.SetLanguageDictionary();
            redrawPlot();
        }

       




        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }


        private void redrawPlot()
        {
            if (viewModel != null)
            {
                string timeTitle = (string)this.TryFindResource("Time");
                string PopulationFractionTitle = (string)this.TryFindResource("PopulationFraction");
                string SuseptibleTitle = (string)this.TryFindResource("Suseptible");
                string InfectedTitle = (string)this.TryFindResource("Infected");
                string RecoverdTitle = (string)this.TryFindResource("Released");
                viewModel.ShowPMass(SuseptibleTitle, InfectedTitle, RecoverdTitle, timeTitle, PopulationFractionTitle);
            }
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redrawPlot();
        }

        private void UIElement_OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            redrawPlot();
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
            redrawPlot();
        }


    }
}