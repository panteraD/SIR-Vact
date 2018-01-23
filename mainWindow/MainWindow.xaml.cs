using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Input;
using System.Windows.Xps.Packaging;



namespace mainWindow
{
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        public MainWindow()
        {
           
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;
                        
            string theoryDocName = "Theory.xps";

            XpsDocument theoryXpsDocument = new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, theoryDocName), FileAccess.Read);

            TheoryDocumentViewer.Document = theoryXpsDocument.GetFixedDocumentSequence();


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
            }
        }
    }
}
