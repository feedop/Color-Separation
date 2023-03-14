using Color_Separation.Separators;
using MathNet.Numerics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Color_Separation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private ColorSeparationViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ColorSeparationViewModel();
            DataContext = viewModel;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Image files|*.jpeg;*.jpg;*.png;*.gif;*.bmp";
            if (op.ShowDialog() == true)
            {
                viewModel.SourceImage = new WriteableBitmap(new BitmapImage(new Uri(op.FileName)));
            }
        }
    }
}
