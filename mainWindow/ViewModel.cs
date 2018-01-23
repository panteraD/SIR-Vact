using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GalaSoft.MvvmLight.CommandWpf;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SelectionMode = OxyPlot.SelectionMode;

namespace mainWindow
{
    class ViewModel : INotifyPropertyChanged
    {

        #region binding stuff
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        private ModelData _data;
        private PlotModel _plotModel;
        private PlotController _customPlotController;

        #region binding properties


        public ModelData Data
        {
            get { return _data; }
            set { _data = value;  OnPropertyChanged("Data"); }
        }



        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { _plotModel = value; OnPropertyChanged("PlotModel"); }
        }


        //     public PlotController CustomPlotController
        //    {
        //       get { return _customPlotController; }
        //      set { _customPlotController = value; OnPropertyChanged("CustomPlotController"); }
        // }

        #endregion




        public ViewModel()
        {
            _data = new ModelData();
            InitData(_data);
            //Show point legend when hovering
            //_customPlotController = new PlotController();
            //_customPlotController.UnbindMouseDown(OxyMouseButton.Left);
          //  _customPlotController.BindMouseEnter(PlotCommands.HoverSnapTrack);
            _plotModel = new PlotModel();
            ShowPMass();
        }

       


        private void InitData(ModelData data)
        {
            _data.SuseptipleFaction = 0.999;
            _data.InfectedFaction = 0.001;
            _data.Beta = 0.5;
            _data.Mu = (double)1 / (70 * 365);
            _data.Gamma= (double) 1 / 7;
            _data.P = 0.7;
            _data.Step = 0.01;
            _data.TimeLimit = 5;
        }




        public void ShowPMass()
        {
            UpdatePlot("Time", "Population fraction", "Time", "Population fraction");
        }


        #region Plottting methods


        public void UpdatePlot(String xProp, String yProp, String xAxisTitle, String yAxisTitle)
        {

            LoadData(xProp, yProp);
            SetUpAxes(xAxisTitle, yAxisTitle);
        }


        public void LoadData(String param1, String param2)
        {
            if (PlotModel != null)
            {
                PlotModel.Series.Clear();
            }
            else
            {
                return;
            }
            var lineSerie = new LineSeries
            {
                StrokeThickness = 5,
                MarkerSize = 6,
                //default if false
                CanTrackerInterpolatePoints = true,
                Title = "Suseptible",
            };

            var lineSerie2 = new LineSeries
            {
                StrokeThickness = 5,
                MarkerSize = 6,
                //default if false
                CanTrackerInterpolatePoints = true,
                Title = "Infected",
                //default if false
                Color = OxyColors.Red
            };

            var lineSerie3 = new LineSeries
            {
                StrokeThickness = 5,
                MarkerSize = 6,
                //default if false
                CanTrackerInterpolatePoints = true,
                Title = "Infected",
                //default if false
                Color = OxyColors.Blue
            };


            List<DataPoint> suseptipleDataPoints = new List<DataPoint>();
            List<DataPoint> infectedDataPoints = new List<DataPoint>();

            List<DataPoint> recoveredDataPoints = new List<DataPoint>();


            var points = _data.calcSIR();
            foreach (var sp in points)
            {
                suseptipleDataPoints.Add(new DataPoint(sp.T, sp.X[0]));
                infectedDataPoints.Add(new DataPoint(sp.T, sp.X[1]));
                recoveredDataPoints.Add(new DataPoint(sp.T, 1 - sp.X[0] - sp.X[1]));
            }
            lineSerie.Points.AddRange(suseptipleDataPoints);
            lineSerie2.Points.AddRange(infectedDataPoints);
            lineSerie3.Points.AddRange(recoveredDataPoints);

            PlotModel.Series.Add(lineSerie);
            PlotModel.Series.Add(lineSerie2);
            PlotModel.Series.Add(lineSerie3);
        }

        private void SetUpAxes(String xAxisTitle, String yAxisTitle)
        {
            PlotModel.Axes.Clear();
            var yAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = yAxisTitle,
                TitlePosition = 0.5,
                TitleFontSize = 28,
                AxisTitleDistance = 12,
                FontSize = 20,
                StringFormat = "0.######",
                Maximum = 1

            };

            var xAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Title = xAxisTitle,
                TitlePosition = 0.5,
                TitleFontSize = 28,
                FontSize = 20,
                StringFormat = "0.######"

            };



            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            //PlotModel.PlotMargins = new OxyThickness(80, 60, 60, 60);
            PlotModel.LegendFontSize = 15;
            PlotModel.SubtitleFontSize = 15;

        }



        #endregion

        #region click handlers

        private ICommand _calc;

        public ICommand Calc => _calc ?? (_calc = new RelayCommand(ShowPMass));


        #endregion
    }
}
