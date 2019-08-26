using Stylet;
using System;
using System.Linq;
using SCEEC.MI.High_Precision;
using System.IO.Ports;
using SCEEC.Numerics;

namespace 高电项目.ViewModels
{
    public enum TestKind
    {
        SetTestChannel,
        SetTestSpeed,
        SetTestCn,
        SetTestConfireVolate,
        SetTestConfireFre,
        StartBooster,
        StartBuck
    }

    public enum MisTakKind
    {
        MachineSuccess = 0,
        MachineFalse = 1,
        MachineLengthFalse = 2,
        MachineCheckedFalse = 3,
        SetTestPanFalse = 4,
        SetTestSpeedFalse = 5,
        SetTestCnFalse = 6,
        SetTestConfireVolateFalse = 7,
        SetTestConfireFreFalse = 8,
        UpVolateFalse = 9,
        DownVolateFalse = 10,

    }
    public class MainViewModel : Screen
    {

        private PortClass LocalPrecision = new PortClass();
        public byte TestPanel { get; set; }
        public byte TestSpeed { get; set; }
        public string TestCurrent { get; set; }
        public string TestMeasuredCurrent { get; set; }
        public string TestAngle { get; set; }
        public string TestFre { get; set; }
        public string Volate { get; set; }
        public readonly string VolateData;



        public string Pan { get; set; }
        public string Spe { get; set; }
        public string Vol { get; set; }
        public string Cur { get; set; }
        public string Cn { get; set; }
        public string Fre { get; set; }
        public MainViewModel()
        {
            OpenPort("COM9", 9600, 8, 1);
        }
        public MainViewModel(string comPortName, int baudRate, int dataBits, int stopBits)
        {
            OpenPort(comPortName, baudRate, dataBits, stopBits);
        }
        public bool OpenPort(string comPortName, int baudRate, int dataBits, int stopBits)
        {
            bool IsSuccess = true;
            LocalPrecision.closePort();
            try
            {
                LocalPrecision.setSerialPort(comPortName, baudRate = 9600, dataBits = 8, stopBits = 1);
                LocalPrecision.openPort();
                LocalPrecision.DataReceived += new PortClass.SerialPortDataReceiveEventArgs(DataReceive);
            }
            catch (Exception)
            {
                return !IsSuccess;
            }
            return IsSuccess;
        }

        //public MainViewModel OpenMyport(string portname,int Bau, int data, int stopbit)
        //{
        //    return new MainViewModel(portname, Bau, data, stopbit);
        //}




        public void ShowSheelWindow()
        {
            高电项目.Pages.ShellView shellView = new Pages.ShellView();
            shellView.ShowDialog();
        }
        private byte CheckData(byte[] checkdata)
        {
            byte[] tempD = new byte[checkdata.Length];
            for (int i = 0; i < checkdata.Length; i++)
            {
                tempD[i] = checkdata[i];
            }
            // byte[] tempD = checkdata;

            byte Endcheckdata = 0;
            foreach (byte outd in tempD)
            {
                Endcheckdata += outd;
            }
            return Endcheckdata;
        }

        private bool IsCheckData(byte[] checkdata)
        {
            byte[] tempD = new byte[checkdata.Length - 1];
            for (int i = 0; i < checkdata.Length - 1; i++)
            {
                tempD[i] = checkdata[i];
            }
            byte Endcheckdata = 0;
            foreach (byte outd in tempD)
            {
                Endcheckdata += outd;
            }
            return Endcheckdata == checkdata[checkdata.Length - 1];
        }
        public string[] GetPortNames()
        {
            return LocalPrecision.getSerials();

        }
        public void Closeport()
        {
            LocalPrecision.closePort();
        }

        public void DataReceive(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            if (bits.Length == 19)
            {
                TestPanel = bits[0];
                TestSpeed = bits[1];
                float TestCurrent1 = BitConverter.ToSingle(bits.Skip(2).Take(4).ToArray(), 0);
                float TestMeasuredCurrent1 = BitConverter.ToSingle(bits.Skip(6).Take(4).ToArray(), 0);
                float TestAngle1 = BitConverter.ToSingle(bits.Skip(10).Take(4).ToArray(), 0);
                float TestFre1 = BitConverter.ToSingle(bits.Skip(14).Take(4).ToArray(), 0);
                TestMeasuredCurrent = NumericsConverter.Value2Text(TestMeasuredCurrent1, 2, -23, " ", "A", false, false);
                TestAngle = NumericsConverter.Value2Text(TestAngle1, 2, -23, " ", " ", true, false);
                TestFre = NumericsConverter.Value2Text(TestFre1, 4, -23, " ", "Hz", false, false);
                Volate = NumericsConverter.Value2Text((TestCurrent1 / (2 * TestFre1 * Math.PI * (1e-10))), 2, -23, " ", "V", false, false);


            }
        }

        public byte StartTest()
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0x69, 0x6A, CheckData(new byte[2] { 0x69, 0x6A }) };
            // LocalPrecision.SendDataByte(TestComman, 0, TestComman.Length);
            if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                return Issuccss[0];
            else
                return 0x04;
        }

        public byte ChangeTestChannel(byte testChannel)
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0x80, testChannel, CheckData(new byte[2] { 0x80, testChannel }) };
            // LocalPrecision.SendDataByte(TestComman, 0, TestComman.Length);
            if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                return Issuccss[0];
            else
                return 0x04;
        }
        public void ChangePan()
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0x80, Convert.ToByte(Pan), CheckData(new byte[2] { 0x80, Convert.ToByte(Pan) }) };
            LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
        }


        public byte ChangeTestSpeed(byte testSpeed)
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0x88, testSpeed, CheckData(new byte[2] { 0x88, testSpeed }) };
            // LocalPrecision.SendDataByte(TestComman, 0, TestComman.Length);
            if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                return Issuccss[0];
            else
                return 0x05;
        }

        public void ChangeSpe()
        {
            byte[] testCnBuffer = BitConverter.GetBytes((float)Convert.ToInt32(Spe));
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x88, Convert.ToByte(Spe), CheckData(new byte[2] { 0x88, Convert.ToByte(Spe) }) };
                LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
            }
        }

        public byte ChangeTestCn(float testCn)
        {
            byte[] testCnBuffer = BitConverter.GetBytes(testCn);
            if (testCnBuffer.Length == 4)
            {

                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x8A, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x8A, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                    return Issuccss[0];
                else
                    return 0x06;//通讯命令出错
            }
            return 0x06;

        }

        public void ChangeCn()
        {
            byte[] testCnBuffer = BitConverter.GetBytes((float)Convert.ToInt32(Cn));
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x8A, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x8A, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
            }
        }

        public byte ChangeVolate(float TestVolate)
        {
            byte[] testCnBuffer = BitConverter.GetBytes(TestVolate);
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x50, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x50, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                    return Issuccss[0];
                else
                    return 0x07;//通讯命令出错
            }
            return 0x07;
        }

        public void ChangeVol()
        {
            byte[] testCnBuffer = BitConverter.GetBytes((float)Convert.ToInt32(Vol));
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x50, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x50, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
            }
        }

        public byte ChangeFre(float TestFre)
        {
            byte[] testCnBuffer = BitConverter.GetBytes(TestFre);
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x55, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x55, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                    return Issuccss[0];
                else
                    return 0x08;//通讯命令出错
            }
            return 0x08;
        }

        public void ChangeFreq()
        {
            byte[] testCnBuffer = BitConverter.GetBytes((float)Convert.ToInt32(Fre));
            if (testCnBuffer.Length == 4)
            {
                byte[] Issuccss = new byte[1];
                byte[] TestComman = { 0x55, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3],
                    CheckData(new byte[5] { 0x55, testCnBuffer[0], testCnBuffer[1], testCnBuffer[2], testCnBuffer[3] }) };
                LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
            }
        }

        public byte startUpVolate()
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0xA0, 0xA0, CheckData(new byte[2] { 0xA0, 0xA0 }) };
            if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                return Issuccss[0];
            else
                return 0x09;//通讯命令出错
        }

        public byte startDownVolate()
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0xAA, 0xAA, CheckData(new byte[2] { 0xAA, 0xAA }) };
            if (0 < LocalPrecision.SendCommand(TestComman, ref Issuccss, 10))
                return Issuccss[0];
            else
                return 0x0A;//通讯命令出错
        }

        public void ModifyMeasurementParameters(byte TestChannel, byte TestSpeed, byte Cn, byte Volate, byte Fre, TestKind kind)
        {
            if (kind == TestKind.SetTestChannel)
                ChangeTestChannel(TestChannel);
            if (kind == TestKind.SetTestSpeed)
                ChangeTestSpeed(TestSpeed);
            if (kind == TestKind.SetTestCn)
                ChangeTestCn(Cn);
            if (kind == TestKind.SetTestConfireVolate)
                ChangeVolate(Volate);
            if (kind == TestKind.SetTestConfireFre)
                ChangeFre(Fre);
            if (kind == TestKind.StartBooster)
                startUpVolate();
            if (kind == TestKind.StartBuck)
                startDownVolate();
        }


        ~MainViewModel()
        {
            GC.Collect();
            Closeport();
        }
    }

}




