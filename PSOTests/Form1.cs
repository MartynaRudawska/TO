using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSOTests
{
    public partial class Form1 : Form
    {
        PSO optymalizacja;
        private double maxX;
        private double minX;
        private short ileCzastek;
        private short maxEpochs;
        private string funkcja;
        private Dictionary<string, Tuple<double, double>> dziedzinyFunkcji = new Dictionary<string, Tuple<double, double>>();
        //;
        public Form1()
        {
            InitializeComponent();
            dziedzinyFunkcji.Add("y=2*x^2+x-2", new Tuple<double, double>(-3, 3));
            dziedzinyFunkcji.Add("x^2+sin(3 cos(5x))", new Tuple<double, double>(-1, 1));
            dziedzinyFunkcji.Add("x^4+x^3-7x^2-5x+10", new Tuple<double, double>(-5, 5));
            dziedzinyFunkcji.Add("sin(2 x)+ln(x^2)", new Tuple<double, double>(-3, 3));
            dziedzinyFunkcji.Add("|(log_{10}(x^2)|", new Tuple<double, double>(0, 2));
    }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaxEpochUpDown.Value = maxEpochs;
            ParticleQuantityUpDown.Value = ileCzastek;

        }

        /*
        static double Error(double x)
        {
            double trueMin = 0, y = 0;
            switch (funkcja)
            {

                case "2*x^2+x-2":
                    trueMin = -2.125; // @x=-0.25
                    y = 2 * Math.Pow(x, 2) + x - 2;
                    break;
                case "x^2+sin(3 cos(5x))":
                    trueMin = 0.14112;//@x=0  
                    //trueMin=-0.82205 @x =~= -0.41935
                    y = Math.Pow(x, 2) + Math.Sin(3 * Math.Cos(5 * x));
                    break;
                case "x^4+x^3-7x^2-5x+10":
                    trueMin = -5.4686;//@x=1.7153
                    y = Math.Pow(x, 4) + Math.Pow(x, 3) - 7 * Math.Pow(x, 2) - 5 * x + 10;
                    break;
                case "sin(2 x)+ln(x^2)":
                    //BRAK GLOBALNEGO MINIMUM !!!
                    trueMin = double.MaxValue;
                    y = Math.Sin(2 * x) + Math.Log(Math.Pow(x, 2));
                    break;
                case "|(log_{10}(x^2)|":
                    //BRAK GLOBALNEGO MINIMUM !!!
                    trueMin = double.MaxValue;
                    y = Math.Abs(Math.Log10(Math.Pow(x, 2)));
                    break;


            }
            return (y - trueMin) * (y - trueMin);
        }
        */
        /*
        static double Solve(int numParticles, double minX, double maxX, int maxEpochs, double exitError)
        {
            
            Random rnd = new Random(0);

            Particle[] swarm = new Particle[numParticles];
            double bestGlobalPosition= double.MaxValue; ; // Najlepsza pozycja znaleziona przez którąkolwiek cząstkę roju
            double bestGlobalError = double.MaxValue; // im mniejsza tym lepsza

            // Inicjalizacja roju
            for (int i = 0; i < swarm.Length; ++i)
            {
                double randomPosition;
                    randomPosition = (maxX - minX) * rnd.NextDouble() + minX; // 

                double error = Error(randomPosition);
                double randomVelocity;

                double lo = minX * 0.1;
                double hi = maxX * 0.1;
                randomVelocity = (hi - lo) * rnd.NextDouble() + lo;
                
                swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

                // Sprawdzenie, czy cząstka ma najlepszą pozycję / rozwiązanie
                if (swarm[i].error < bestGlobalError)
                {
                    bestGlobalError = swarm[i].error;
                    bestGlobalPosition=swarm[i].pozycja;
                }
            }

            // parametry literaturowe
            double w = 0.729;
            double c1 = 1.49445;
            double c2 = 1.49445;
            double r1, r2;
            double probDeath = 0.01;
            int epoch = 0;

            double newVelocity;
            double newPosition;
            double newError;

            // główna pętla PSO
            while (epoch < maxEpochs)
            {
                for (int i = 0; i < swarm.Length; ++i) // dla każdej cząstki
                {
                    Particle currP = swarm[i]; 

                    
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        newVelocity = (w * currP.predkosc) +
                          (c1 * r1 * (currP.najlPozycja - currP.pozycja)) +
                          (c2 * r2 * (bestGlobalPosition - currP.pozycja));
                    
                  currP.predkosc = newVelocity;

                    // new position
                    
                        newPosition = currP.pozycja + newVelocity;
                        if (newPosition < minX)
                            newPosition = minX;
                        else if (newPosition > maxX)
                            newPosition = maxX;

                    currP.pozycja = newPosition;

                    newError = Error(newPosition);
                    currP.error = newError;

                    if (newError < currP.bestError)
                    {
                        currP.najlPozycja=newPosition;
                        currP.bestError = newError;
                    }

                    if (newError < bestGlobalError)
                    {
                        bestGlobalPosition = newPosition;
                        bestGlobalError = newError;
                    }

                    // Czy uśmiercić cząstkę?
                    double die = rnd.NextDouble();
                    if (die < probDeath)
                    {
                        
                        for (int j = 0; j < currP.position.Length; ++j)
                            currP.position[j] = (maxX - minX) * rnd.NextDouble() + minX;
                        currP.error = Error(currP.pozycja);
                        currP.position.CopyTo(currP.bestPosition, 0);
                        currP.bestError = currP.error;

                        if (currP.error < bestGlobalError) //przypadkowe trafienie w globalne minimum?
                        {
                            bestGlobalError = currP.error;
                            bestGlobalPosition=currP.pozycja;
                        }
                    }

                } 
                ++epoch;
            } // while

            // Rój po przejściu wszystkich epok
            for (int i = 0; i < swarm.Length; ++i)
                Console.WriteLine(swarm[i].ToString());

            double result;
            result=bestGlobalPosition;
            return result;
            // Solve
        }


        private void ParticleSwarm()
        {
            Console.WriteLine("\nSetting problem dimension to " + dim);
            Console.WriteLine("Setting numParticles = " + numParticles);
            Console.WriteLine("Setting maxEpochs = " + maxEpochs);
            Console.WriteLine("Setting early exit error = " + exitError.ToString("F4"));
            Console.WriteLine("Setting minX, maxX = " + minX.ToString("F1") + " " + maxX.ToString("F1"));

            double bestPosition = Solve(dim, numParticles, minX, maxX, maxEpochs, exitError);
            double bestError = Error(bestPosition);

            Console.WriteLine("Best position/solution found:");
            
                Console.Write("x" + " = ");
                Console.WriteLine(bestPosition.ToString("F6") + " ");
            
            Console.WriteLine("");
            Console.Write("Final best error = ");
            Console.WriteLine(bestError.ToString("F5"));
        }
        */
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ParticleQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            ileCzastek = Convert.ToInt16(ParticleQuantityUpDown.Value);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            optymalizacja = new PSO(dziedzinyFunkcji[funkcja], ileCzastek,maxEpochs,funkcja);
            MessageBox.Show(string.Format("Znalezione minimum to {0} z błędem {1}",PSO.PSOSolution().Item1,PSO.PSOSolution().Item2));
        }

        private void MaxEpochUpDown_ValueChanged(object sender, EventArgs e)
        {
            maxEpochs = Convert.ToInt16(MaxEpochUpDown.Value);
        }

        private void FunctionSelectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            funkcja = FunctionSelectionCombo.SelectedItem.ToString();
        }
    }
}
