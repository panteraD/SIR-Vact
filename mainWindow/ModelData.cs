using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using Microsoft.Research.Oslo;
using OxyPlot;
using Vector = Microsoft.Research.Oslo.Vector;

namespace mainWindow
{
    public class ModelData : INotifyPropertyChanged
    {
        private double beta; // transmission rate
        private double f;
        private double gamma; // recovery rate

        private double mu; // birth/death rate
        private double p;
        private double r0; // basic recovery rate

        private double infectedFaction;
        private double suseptipleFaction;

        private double step;
        private double timeLimit;

        private string helpText;

        #region Bindings





        public double Beta
        {
            get { return beta; }
            set
            {
                double oldValue = beta;
                beta = value;
                HelpText = (oldValue - beta) > 0 ? getLocalizedHelpMessage("EventBetaDec") : getLocalizedHelpMessage("EventBetaInc");

                R0 = Beta / (Gamma + Mu);
                OnPropertyChanged("Beta");
            }
        }

        public double Mu
        {
            get { return mu; }
            set
            {
                double oldValue = mu;
                mu = value;
                HelpText = (oldValue - mu) > 0 ? getLocalizedHelpMessage("EventMuDec") : getLocalizedHelpMessage("EventMuInc");
                R0 = Beta / (Gamma + Mu);
                F = (double)Mu / (Gamma + Mu);
                OnPropertyChanged("Mu");
            }
        }

        public double Gamma
        {
            get { return gamma; }
            set
            {
                double oldValue = gamma;
                gamma = value;
                HelpText = (oldValue - gamma) > 0 ? getLocalizedHelpMessage("EventGammaDec") : getLocalizedHelpMessage("EventGammaInc");
                R0 = Beta / (Gamma + Mu);
                F = (double)Mu / (Gamma + Mu);
                OnPropertyChanged("Gamma");
            }
        }

        public double R0
        {
            get { return r0; }
            set
            {
                r0 = value;
                OnPropertyChanged("R0");
            }
        }

        public double F
        {
            get { return f; }
            set
            {
                f = value;
                OnPropertyChanged("F");
            }
        }

        public double P
        {
            get { return p; }
            set
            {
                double oldValue = p;
                p = value;
                HelpText = (oldValue - p) > 0 ? getLocalizedHelpMessage("EventPDec") : getLocalizedHelpMessage("EventPInc");
                OnPropertyChanged("P");
            }
        }

        public double Step
        {
            get { return step; }
            set
            {
                double oldValue = step;
                step = value;
                HelpText = (oldValue - step) > 0 ? getLocalizedHelpMessage("EventTimeStepDec") : getLocalizedHelpMessage("EventTimeStepInc");
                OnPropertyChanged("Step");
            }
        }

        public double TimeLimit
        {
            get { return timeLimit; }
            set
            {
                double oldValue = timeLimit;
                timeLimit = value;
                HelpText = (oldValue - timeLimit) > 0 ? getLocalizedHelpMessage("EventTimeLimitDec") : getLocalizedHelpMessage("EventTimeLimitInc");
                OnPropertyChanged("TimeLimit");
            }
        }

        public double SuseptipleFaction
        {
            get { return suseptipleFaction; }
            set
            {
                double oldValue = suseptipleFaction;
                suseptipleFaction = value;
                InfectedFaction = 1 - suseptipleFaction;
                HelpText = (oldValue - suseptipleFaction) > 0 ? getLocalizedHelpMessage("EventSDec") : getLocalizedHelpMessage("EventSInc");
                OnPropertyChanged("SuseptipleFaction");
            }
        }

        public double InfectedFaction
        {
            get { return infectedFaction; }
            set
            {
                infectedFaction = value;
                OnPropertyChanged("InfectedFaction");
            }
        }

        public string HelpText
        {
            get { return helpText; }
            set
            {
                helpText = value;
                OnPropertyChanged("HelpText");
            }
        }

        #endregion

        public string getLocalizedHelpMessage(String key)
        {
            CultureInfo currectCulture = Thread.CurrentThread.CurrentCulture;
            if (currectCulture.ToString().Equals(mainWindow.MainWindow.ENGLISH))
            {
                ResourceDictionary rd = System.Windows.Application.Current.Resources.MergedDictionaries[0];
                object theValue = rd[key];
                return (string)theValue;
            }
            else
            {
                ResourceDictionary rd = System.Windows.Application.Current.Resources.MergedDictionaries[1];
                object theValue = rd[key];
                return (string)theValue;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public SolPoint[] calcSIR()
        {
            var sol = Ode.RK547M(
                0,
                new Vector(SuseptipleFaction, InfectedFaction),
                (t, x) => new Vector(
                    (1 - p) * f - r0 * x[0] * x[1] - f * x[0],
                    r0 * x[0] * x[1] - x[1]));
            var points = sol.SolveFromToStep(0, TimeLimit, Step).ToArray();
            return points;
        }


    }
}