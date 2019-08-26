using System;
using Stylet;
using SCEEC.MI.High_Precision;
using System.Threading;
using System.IO.Ports;
using System.Linq;

namespace 高电项目.Pages
{
    public class ShellViewModel : Screen
    {
        // static High_PrecisionClass Testclass = new High_PrecisionClass("COM9");
        public string TestPanel { get; set; }
        public string TestSpeed { get; set; }
        public string TestCurrent { get; set; }
        public string TestMeasuredCurrent { get; set; }
        public string TestAngle { get; set; }
        public string TestFre { get; set; }



        //public double Pan { get; set; } = Testclass.TestPanel;//需要测试
        //private void OpentestPort()
        //{
        //    Testclass.LocalPrecision.closePort();
        //    Testclass.LocalPrecision.setSerialPort("COM9", 9600, 8, 1);
        //    Testclass.LocalPrecision.openPort();
        //    Testclass.LocalPrecision.DataReceived += new PortClass.SerialPortDataReceiveEventArgs(DataReceiveClient);
        //}
        //public void DataReceiveClient(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        //{
        //    if (bits.Length == 19)
        //    {
        //        TestPanel = bits[0].ToString();
        //        TestSpeed = bits[1].ToString();
        //        TestCurrent = BitConverter.ToSingle(bits.Skip(2).Take(4).ToArray(), 0).ToString();
        //        TestMeasuredCurrent = BitConverter.ToSingle(bits.Skip(6).Take(4).ToArray(), 0).ToString();
        //        TestAngle = BitConverter.ToSingle(bits.Skip(10).Take(4).ToArray(), 0).ToString();
        //        TestFre = BitConverter.ToSingle(bits.Skip(14).Take(4).ToArray(), 0).ToString();
        //    }
        //}
        //private void StartComman()
        //{
        //    byte[] Issuccss = new byte[1];
        //    byte[] TestComman = { 0x69, 0x6A, 0xd3 };
        //    Testclass.LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
        //}
        public void StartTest()
        {
            TestResult.WorkTest.StartTest();
        }
    }

}
