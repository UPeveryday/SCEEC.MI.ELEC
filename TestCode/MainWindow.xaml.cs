using System.Windows;
using SCEEC.Numerics;
namespace TestCode
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            t1.Text = NumericsConverter.Value2Text(1e-10, 2, -23, " ", "V", false, false);
        }
    }
}
