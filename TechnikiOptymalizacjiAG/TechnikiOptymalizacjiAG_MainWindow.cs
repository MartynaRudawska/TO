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
    }
}
