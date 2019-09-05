using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.ELEC
{
    public class ElecClass
    {

        public delegate void SendData(byte[] bits);
        public event SendData SendMessages;
        private string Port { get; set; }
        private int Bau { get; set; }
        public PortClass PortUser = new PortClass();
        public ElecClass()
        {
            string[] cp = GetPortNames();
            this.Port = cp[cp.Length - 1];
            this.Bau = 115200;
            openPort();
        }


        private bool openPort()
        {
            PortUser.setSerialPort(Port, Bau, 8, 1);
            bool isSuccess = PortUser.openPort();
            if (isSuccess)
            {
                PortUser.DataReceived += PortUser_DataReceived;
                return isSuccess;
            }
            else
                return false;
        }

        public void StartTest(byte channel)
        {
            if (channel == 0x01 || channel == 0x02 || channel == 0x03)
            {
                byte[] TestComman = { 0x41, channel };
                PortUser.SendData(TestComman, 0, 2);
            }
        }
        private void PortUser_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e, byte[] bits)
        {
            SendMessages(bits);
        }
        /// <summary>
        /// 读取波形
        /// </summary>
        public byte[] ReadWaveForm(int needreclength)
        {
            byte[] rec = new byte[needreclength];
            PortUser.SendData(new byte[1] { 0x42 }, 0, 2);
           return PortUser.ReadPortsData(new byte[1] { 0x42 }, rec, needreclength, 50);
        }


        public string[] GetPortNames()
        {
            return PortUser.getSerials();

        }
        private void Closeport()
        {
            PortUser.closePort();
        }
        ~ElecClass()
        {

        }
    }



    public static class TestHighClass
    {
       public static ElecClass ElecClass = new ElecClass();
    }
        

}
