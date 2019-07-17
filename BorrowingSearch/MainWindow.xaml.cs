using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;

namespace BorrowingSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFirstProg_Click(object sender, RoutedEventArgs e)
        {
            txtFirstProg.Text = getFromFile();
        }

        private void btnOpenSecondProg_Click(object sender, RoutedEventArgs e)
        {
            txtSecondProg.Text = getFromFile();
        }
        private static string getFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CS files (*.cs)|*.cs|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog()==true)
            {
                return File.ReadAllText(openFileDialog.FileName);
            }
            return null;
        }

        private void btnRunAnalize_Click(object sender, RoutedEventArgs e)
        {
            StepMethod sm = new StepMethod();
            Statement state = sm.Analise(txtFirstProg.Text, txtSecondProg.Text, int.Parse(txtBoxNgramm.Text));
            lblShingle.Content =(Math.Round(state.ShingleMethod * 100,2)).ToString() + "%";
            lblVagnerFischer.Content = (Math.Round(state.LevenshteynMethod * 100,2)).ToString() + "%";
        }
    }
}
