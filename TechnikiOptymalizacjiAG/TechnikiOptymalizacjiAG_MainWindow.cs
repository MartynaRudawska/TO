using System;
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
namespace TechnikiOptymalizacjiAG
{
    public partial class TechnikiOptymalizacjiAGMainWindow : Form
    {
        public TechnikiOptymalizacjiAGMainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

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




        private void PrepareComboBoxes()
        {
            PrepareEditComboBox(
               cmbSelection,
               btnEditSelection,
               SelectionService.GetSelectionNames,
               SelectionService.GetSelectionTypeByName,
               SelectionService.CreateSelectionByName,
               () => m_selection,
               (i) => m_selection = i);

            PrepareEditComboBox(
                cmbCrossover,
                btnEditCrossover,
                CrossoverService.GetCrossoverNames,
                CrossoverService.GetCrossoverTypeByName,
                CrossoverService.CreateCrossoverByName,
                () => m_crossover,
                (i) => m_crossover = i);

            PrepareEditComboBox(
                cmbMutation,
                btnEditMutation,
                MutationService.GetMutationNames,
                MutationService.GetMutationTypeByName,
                MutationService.CreateMutationByName,
                () => m_mutation,
                (i) => m_mutation = i);

            PrepareEditComboBox(
                cmbTermination,
                btnEditTermination,
                () =>
                {
                    return TerminationService.GetTerminationNames();
                },
                TerminationService.GetTerminationTypeByName,
                TerminationService.CreateTerminationByName,
                () => m_termination,
                (i) => m_termination = i);

            PrepareEditComboBox(
                cmbTermination1,
                btnEditReinsertion,
                ReinsertionService.GetReinsertionNames,
                ReinsertionService.GetReinsertionTypeByName,
                ReinsertionService.CreateReinsertionByName,
                () => m_reinsertion,
                (i) => m_reinsertion = i);

            PrepareEditComboBox(
                cmbGenerationStrategy,
                btnEditGenerationStrategy,
                PopulationService.GetGenerationStrategyNames,
                PopulationService.GetGenerationStrategyTypeByName,
                PopulationService.CreateGenerationStrategyByName,
                () => m_generationStrategy,
                (i) => m_generationStrategy = i);
        }





        private void CrossingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SelectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PopulationMinUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PopulationMaxUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TechnikiOptymalizacjiAGMainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
