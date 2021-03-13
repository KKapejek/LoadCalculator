using System.Collections.Generic;
using System.Windows;

namespace LoadCalculator
{
    public class LoadCase
    {
        public string Case { get; set; }
        public float Pz { get; set; }
        public float Mx { get; set; }
        public float My { get; set; }
    }

    public class Label
    {
        public string Name { get; set; }
        public float FactorA { get; set; }
        public float FactorB { get; set; }
        public float FactorC { get; set; }
        public float FactorD { get; set; }
    }

    public class Factored
    {
        public string Name { get; set; }
        public float FactoredLoad { get; set; }
        public float FactoredMomentX { get; set; }
        public float FactoredMomentY { get; set; }
    }



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<LoadCase> loadCases = new List<LoadCase>();
            loadCases.Add(new LoadCase { Case = "A", Pz = 0, Mx = 0, My = 0 });
            loadCases.Add(new LoadCase { Case = "B", Pz = -100, Mx = 20, My = 40 });
            loadCases.Add(new LoadCase { Case = "C", Pz = 0, Mx = 150, My = 200 });
            loadCases.Add(new LoadCase { Case = "D", Pz = 0, Mx = 0, My = 0 });
            LoadCases.ItemsSource = loadCases;

            List<Label> labels = new List<Label>();
            labels.Add(new Label { Name = "U1", FactorA = 1.1F, FactorB = 1.2F, FactorC = 1.3F, FactorD = 1.4F });
            labels.Add(new Label { Name = "U2", FactorA = 1.1F, FactorB = 1.2F, FactorC = 1.3F, FactorD = 0F });
            labels.Add(new Label { Name = "U3", FactorA = -1.1F, FactorB = -1.2F, FactorC = -1.3F, FactorD = 0F });
            Labels.ItemsSource = labels;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        public void Calculate()
        {
            List<LoadCase> loadCases = GetLoadCases();
            List<Label> labels = GetLabels();

            List<Factored> factoreds = new List<Factored>();

            foreach (var x in labels)
            {
                var load = loadCases[0].Pz * x.FactorA + loadCases[1].Pz * x.FactorB + loadCases[2].Pz * x.FactorC + loadCases[3].Pz * x.FactorD;
                var factoredMomentX = loadCases[0].Mx * x.FactorA + loadCases[1].Mx * x.FactorB + loadCases[2].Mx * x.FactorC + loadCases[3].Mx * x.FactorD;
                var factoredMomentY = loadCases[0].My * x.FactorA + loadCases[1].My * x.FactorB + loadCases[2].My * x.FactorC + loadCases[3].My * x.FactorD;

                factoreds.Add(new Factored { Name = x.Name, FactoredLoad = load, FactoredMomentX = factoredMomentX, FactoredMomentY = factoredMomentY });
            }

            Output.ItemsSource = factoreds;
        }

        private List<LoadCase> GetLoadCases()
        {
            List<LoadCase> loadCases = new List<LoadCase>();
            for (int i = 0; i < LoadCases.Items.Count; i++)
            {
                LoadCase row = (LoadCase)LoadCases.Items[i];
                loadCases.Add(row);
            }

            return loadCases;
        }

        private List<Label> GetLabels()
        {
            List<Label> labels = new List<Label>();
            for (int i = 0; i < Labels.Items.Count; i++)
            {
                Label row = (Label)Labels.Items[i];
                labels.Add(row);
            }

            return labels;
        }
    }
}
