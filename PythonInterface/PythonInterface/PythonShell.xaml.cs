using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PythonInterface
{
    /// <summary>
    /// Interaction logic for PythonShell.xaml
    /// </summary>
    public partial class PythonShell : UserControl
    {
        Paragraph myParagraph = new Paragraph();

        public PythonShell()
        {
            InitializeComponent();
            PyShell.Document = new FlowDocument(myParagraph);
            myParagraph.Inlines.Add(new Bold(new Run("Hello :"))
            {
                Foreground = Brushes.Red
            });
            this.DataContext = this;
            PyShell.TextChanged += PyShell_TextChanged;


        }

        void PyShell_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
