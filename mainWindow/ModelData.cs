using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Research.Oslo;
using OxyPlot;

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

        #region Bindings



        public double Beta
        {
            get { return beta; }
            set
            {
                beta = value;
                R0 = Beta / (Gamma + Mu);
                OnPropertyChanged("Beta");
            }
        }

        public double Mu
        {
            get { return mu; }
            set
            {
                mu = value;
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
                gamma = value;
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
                p = value;
                OnPropertyChanged("P");
            }
        }

        public double Step
        {
            get { return step; }
            set
            {
                step = value;
                OnPropertyChanged("Step");
            }
        }

        public double TimeLimit
        {
            get { return timeLimit; }
            set
            {
                timeLimit = value;
                OnPropertyChanged("TimeLimit");
            }
        }

        public double SuseptipleFaction
        {
            get { return suseptipleFaction; }
            set
            {
                suseptipleFaction = value;
                InfectedFaction = 1 - suseptipleFaction;
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

        #endregion

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