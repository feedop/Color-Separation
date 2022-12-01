using Color_Separation.Separators;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Color_Separation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISeparator separator;
        internal ISeparator Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        private BitmapImage source = new BitmapImage();
        public BitmapImage Source
        {
            get { return source; }
            set { source = value; }
        }

        public WriteableBitmap? Result1 { get; set; }
        public WriteableBitmap? Result2 { get; set; }
        public WriteableBitmap? Result3 { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            separator = new HSVSeparator();
            DataContext = this;
        }
        /// <summary>
        /// Updates the result bitmaps
        /// </summary>
        private void Update()
        {

        }
    }
}
