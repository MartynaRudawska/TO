﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticSharp.Domain;
using GeneticSharp.Extensions;
using GeneticSharp.Infrastructure;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Reinsertions;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Populations;
using System.Threading;
using GeneticSharp.Runner.GtkApp.Samples;

namespace TechnikiOptymalizacjiAG
{
    public partial class TechnikiOptymalizacjiAGMainWindow : Form
    {
        #region pola
        private GeneticAlgorithm m_ga;
        private IFitness m_fitness;
        private ISelection m_selection;
        private ICrossover m_crossover;
        private IMutation m_mutation;
        private IReinsertion m_reinsertion;
        private ITermination m_termination;
        private IGenerationStrategy m_generationStrategy;
        private ISampleController m_sampleController;
        private SampleContext m_sampleContext;
        private Thread m_evolvingThread;
        #endregion
        #region konstruktor
        /// <summary>
        /// Konstruktor
        /// </summary>
        public TechnikiOptymalizacjiAGMainWindow()
        {
            InitializeComponent();
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Metody Algorytmu Genetycznego
        /// <summary>
        /// Uruchamianie wątku, w którym pracuje Algorytm Genetyczny
        /// </summary>
        /// <param name="isResuming">Czy praca algorytmu jest właśnie wznawiana?</param>
        private void RunGAThread(bool isResuming)
        {
            vbxSample.Sensitive = false;
            vbxGA.Sensitive = false;
            m_evolvingThread = isResuming ? new Thread(ResumeGA) : new Thread(StartGA);
            m_evolvingThread.Start();
        }
        /// <summary>
        /// Uruchamianie Algorytmu Genetycznego
        /// </summary>
        private void StartGA()
        {
            RunGA(() =>
            {
                m_sampleController.Reset();
                m_sampleContext.Population = new Population(
                    Convert.ToInt32(sbtPopulationMinSize.Value),
                    Convert.ToInt32(sbtPopulationMaxSize.Value),
                    m_sampleController.CreateChromosome());

                m_sampleContext.Population.GenerationStrategy = m_generationStrategy;

                m_ga = new GeneticAlgorithm(
                    m_sampleContext.Population,
                    m_fitness,
                    m_selection,
                    m_crossover,
                    m_mutation);

                m_ga.CrossoverProbability = Convert.ToSingle(hslCrossoverProbability.Value);
                m_ga.MutationProbability = Convert.ToSingle(hslMutationProbability.Value);
                m_ga.Reinsertion = m_reinsertion;
                m_ga.Termination = m_termination;

                m_sampleContext.GA = m_ga;
                m_ga.GenerationRan += delegate
                {
                    Application.Invoke(delegate
                    {
                        m_sampleController.Update();
                    });
                };

                m_sampleController.ConfigGA(m_ga);
                m_ga.Start();
            });
        }
        /// <summary>
        /// Wznawianie Algorytmu Genetycznego
        /// </summary>
        private void ResumeGA()
        {
            RunGA(() =>
            {
                m_ga.Population.MinSize = Convert.ToInt32(sbtPopulationMinSize.Value);
                m_ga.Population.MaxSize = Convert.ToInt32(sbtPopulationMaxSize.Value);
                m_ga.Selection = m_selection;
                m_ga.Crossover = m_crossover;
                m_ga.Mutation = m_mutation;
                m_ga.CrossoverProbability = Convert.ToSingle(hslCrossoverProbability.Value);
                m_ga.MutationProbability = Convert.ToSingle(hslMutationProbability.Value);
                m_ga.Reinsertion = m_reinsertion;
                m_ga.Termination = m_termination;

                m_ga.Resume();
            });
        }

        private void RunGA(System.Action runAction)
        {
            try
            {
                runAction();
            }
            catch (Exception ex)
            {
                Application.Invoke(delegate
                {
                    var msg = new MessageDialog(
                        this,
                        DialogFlags.Modal,
                        MessageType.Error,
                        ButtonsType.YesNo,
                        "{0}\n\nDo you want to see more details about this error?",
                        ex.Message);

                    if (msg.Run() == (int)ResponseType.Yes)
                    {
                        var details = new MessageDialog(
                            this,
                            DialogFlags.Modal,
                            MessageType.Info,
                            ButtonsType.Ok,
                            "StackTrace");

                        details.SecondaryText = ex.StackTrace;
                        details.Run();
                        details.Destroy();
                    }

                    msg.Destroy();
                });
            }

            Application.Invoke(delegate
            {
                btnNew.Visible = true;
                btnResume.Visible = true;
                btnStop.Visible = false;
                vbxGA.Sensitive = true;
            });
        }
        #endregion

        #region Event Handlers
        private void StartBtn_Click(object sender, EventArgs e)
        {

        }

        private void CompareBtn_Click(object sender, EventArgs e)
        {

        }

        private void TimeThresholdUpDown_Click(object sender, EventArgs e)
        {

        }

        private void IterationThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(IterationThresholdUpDown.Value.ToString());
        }

        private void TimeThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void IterationThresholdRadioBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TimeThresholdRadioBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MutationProbTrackbar_Scroll(object sender, EventArgs e)
        {

        }

        private void CrossingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SelectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulationMinUpDown_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(PopulationMinUpDown.Value.ToString());
        }

        private void PopulationMaxUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TechnikiOptymalizacjiAGMainWindow_Load(object sender, EventArgs e)
        {

        }
        private void FunctionSelectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(FunctionSelectionCombo.SelectedItem.ToString());
        }
        #endregion
    }
}
