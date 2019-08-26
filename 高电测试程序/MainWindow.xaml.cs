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
using SCEEC.MI.High_Precision;

namespace 高电测试程序
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
       static PortClass hs = new PortClass("COM9",9600,System.IO.Ports.Parity.None,8,System.IO.Ports.StopBits.One);

        public byte TestChannelData { get; private set; }
        public byte TestSpeedData { get; private set; }
        public float TestVolateData { get; private set; }
        public float TestFreData { get; private set; }
        public float TestCor { get; private set; }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            byte[] rec = new byte[1];
            hs.PortName = "COM9";
            hs.BaudRate = 9600;
            hs.openPort();
            hs.DataReceived += new PortClass.SerialPortDataReceiveEventArgs(Hs_DataReceived);
            hs.SendCommand(new byte[] { 0x69, 0x6a, 0xd3 },ref rec,0);
        }

        private void Hs_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e, byte[] bits)
        {
            if (bits.Length > 0)
            {
                this.Dispatcher.BeginInvoke(new Action(()=>{
                    t1.Text = bits[0].ToString();
                    t2.Text = bits[1].ToString();
                    t3.Text = BitConverter.ToSingle(bits.Skip(2).Take(4).ToArray(), 0).ToString();
                    t4.Text = BitConverter.ToSingle(bits.Skip(6).Take(4).ToArray(), 0).ToString();
                    t5.Text = BitConverter.ToSingle(bits.Skip(10).Take(4).ToArray(), 0).ToString();
                }));
               
            }
        }
    }
}
