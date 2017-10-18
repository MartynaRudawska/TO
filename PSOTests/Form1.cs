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
        #region pola - parametry PSO
        int dim = 2; // problem dimensions
        int numParticles = 5;
        int maxEpochs = 1000;
        double exitError = 0.0; // exit early if reach this error
        double minX = -10.0; // problem-dependent
        double maxX = 10.0;
        public static string funkcja = "z = x * exp(-(x^2 + y^2))";
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaxEpochUpDown.Value = maxEpochs;
            ParticleQuantityUpDown.Value = numParticles;

        }

        static double Error(double[] x)
        {
            double trueMin=0,z=0;
            switch (funkcja)
            {
                
                case "2*x^2+x-2":
                case "x^2+sin(3 cos(5x))":
                case "x^4+x^3-7x^2-5x+10":
                case "sin(2 x)+ln(x^2)":
                case "|(log_{10}(x^2)|":break;
                case "z = x * exp(-(x ^ 2 + y ^ 2))":
                    // 0.42888194248035300000 when x0 = -sqrt(2), x1 = 0
                    trueMin = -0.42888194; // true min for z = x * exp(-(x^2 + y^2))
                    z = x[0] * Math.Exp(-((x[0] * x[0]) + (x[1] * x[1])));
                    break;

            }
            return (z - trueMin) * (z - trueMin); // squared diff
            
        }

        static double[] Solve(int dim, int numParticles, double minX, double maxX, int maxEpochs, double exitError)
        {
            // assumes existence of an accessible Error function and a Particle class
            Random rnd = new Random(0);

            Particle[] swarm = new Particle[numParticles];
            double[] bestGlobalPosition = new double[dim]; // best solution found by any particle in the swarm
            double bestGlobalError = double.MaxValue; // smaller values better

            // swarm initialization
            for (int i = 0; i < swarm.Length; ++i)
            {
                double[] randomPosition = new double[dim];
                for (int j = 0; j < randomPosition.Length; ++j)
                    randomPosition[j] = (maxX - minX) * rnd.NextDouble() + minX; // 

                double error = Error(randomPosition);
                double[] randomVelocity = new double[dim];

                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    double lo = minX * 0.1;
                    double hi = maxX * 0.1;
                    randomVelocity[j] = (hi - lo) * rnd.NextDouble() + lo;
                }
                swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

                // does current Particle have global best position/solution?
                if (swarm[i].error < bestGlobalError)
                {
                    bestGlobalError = swarm[i].error;
                    swarm[i].position.CopyTo(bestGlobalPosition, 0);
                }
            } // initialization

            // prepare
            double w = 0.729; // inertia weight. see http://ieeexplore.ieee.org/stamp/stamp.jsp?arnumber=00870279
            double c1 = 1.49445; // cognitive/local weight
            double c2 = 1.49445; // social/global weight
            double r1, r2; // cognitive and social randomizations
            double probDeath = 0.01;
            int epoch = 0;

            double[] newVelocity = new double[dim];
            double[] newPosition = new double[dim];
            double newError;

            // main loop
            while (epoch < maxEpochs)
            {
                for (int i = 0; i < swarm.Length; ++i) // each Particle
                {
                    Particle currP = swarm[i]; // for clarity

                    // new velocity
                    for (int j = 0; j < currP.velocity.Length; ++j) // each component of the velocity
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        newVelocity[j] = (w * currP.velocity[j]) +
                          (c1 * r1 * (currP.bestPosition[j] - currP.position[j])) +
                          (c2 * r2 * (bestGlobalPosition[j] - currP.position[j]));
                    }
                    newVelocity.CopyTo(currP.velocity, 0);

                    // new position
                    for (int j = 0; j < currP.position.Length; ++j)
                    {
                        newPosition[j] = currP.position[j] + newVelocity[j];
                        if (newPosition[j] < minX)
                            newPosition[j] = minX;
                        else if (newPosition[j] > maxX)
                            newPosition[j] = maxX;
                    }
                    newPosition.CopyTo(currP.position, 0);

                    newError = Error(newPosition);
                    currP.error = newError;

                    if (newError < currP.bestError)
                    {
                        newPosition.CopyTo(currP.bestPosition, 0);
                        currP.bestError = newError;
                    }

                    if (newError < bestGlobalError)
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        bestGlobalError = newError;
                    }

                    // death?
                    double die = rnd.NextDouble();
                    if (die < probDeath)
                    {
                        // new position, leave velocity, update error
                        for (int j = 0; j < currP.position.Length; ++j)
                            currP.position[j] = (maxX - minX) * rnd.NextDouble() + minX;
                        currP.error = Error(currP.position);
                        currP.position.CopyTo(currP.bestPosition, 0);
                        currP.bestError = currP.error;

                        if (currP.error < bestGlobalError) // global best by chance?
                        {
                            bestGlobalError = currP.error;
                            currP.position.CopyTo(bestGlobalPosition, 0);
                        }
                    }

                } // each Particle
                ++epoch;
            } // while

            // show final swarm
            Console.WriteLine("\nProcessing complete");
            Console.WriteLine("\nFinal swarm:\n");
            for (int i = 0; i < swarm.Length; ++i)
                Console.WriteLine(swarm[i].ToString());

            double[] result = new double[dim];
            bestGlobalPosition.CopyTo(result, 0);
            return result;
            // Solve
        }


        private void ParticleSwarm()
        {
            //Console.WriteLine("\nBegin Particle Swarm Optimization demo\n");
            //Console.WriteLine("Goal is to minimize f(x0,x1) = x0 * exp( -(x0^2 + x1^2) )");
            //Console.WriteLine("Known solution is at x0 = -0.707107, x1 = 0.000000");

            

            Console.WriteLine("\nSetting problem dimension to " + dim);
            Console.WriteLine("Setting numParticles = " + numParticles);
            Console.WriteLine("Setting maxEpochs = " + maxEpochs);
            Console.WriteLine("Setting early exit error = " + exitError.ToString("F4"));
            Console.WriteLine("Setting minX, maxX = " + minX.ToString("F1") + " " + maxX.ToString("F1"));
            //Console.WriteLine("\nStarting PSO");

            double[] bestPosition = Solve(dim, numParticles, minX, maxX, maxEpochs, exitError);
            double bestError = Error(bestPosition);

            Console.WriteLine("Best position/solution found:");
            for (int i = 0; i < bestPosition.Length; ++i)
            {
                Console.Write("x" + i + " = ");
                Console.WriteLine(bestPosition[i].ToString("F6") + " ");
            }
            Console.WriteLine("");
            Console.Write("Final best error = ");
            Console.WriteLine(bestError.ToString("F5"));

            //Console.WriteLine("\nEnd PSO demo\n");
            //Console.ReadLine();
        } // Main

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ParticleQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            numParticles = Convert.ToInt16(ParticleQuantityUpDown.Value);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            ParticleSwarm();
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
