using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace SCEEC.MI.High_Precision
{
    public delegate void ResultDelegate(byte[] result);
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
    public class High_PrecisionClass
    {
        public PortClass LocalPrecision = new PortClass();



        public readonly string ComPort;


        public High_PrecisionClass()
        {
            string[] cp = GetPortNames();
            this.ComPort = cp[cp.Length - 1];
            OpenPort();
        }
        public High_PrecisionClass(string comPortName)
        {
            this.ComPort = comPortName;
            LocalPrecision.setSerialPort(comPortName, 460800, 8, 1);

        }
        public bool OpenPort()
        {
            bool IsSuccess = true;
            try
            {
                LocalPrecision.setSerialPort(ComPort, 460800, 8, 1);
                LocalPrecision.openPort();
                LocalPrecision.DataReceived += new PortClass.SerialPortDataReceiveEventArgs(DataReceive);
            }
            catch (Exception)
            {
                return !IsSuccess;
            }
            return IsSuccess;


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
        // ResultDelegate testresults = ReturnTestResult;
        public event ResultDelegate OutTestResult;
        private static byte[] ReturnTestResult(byte[] bits)
        {
            return bits;
        }
        public void DataReceive(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            if (bits.Length == 55)
            {
                //testresults(bits);
                OutTestResult(bits);
            }
        }

        public byte StartTest()
        {
            byte[] Issuccss = new byte[1];
            byte[] TestComman = { 0x69, 0x6A, CheckData(new byte[2] { 0x69, 0x6A }) };
            LocalPrecision.SendCommand(TestComman, ref Issuccss, 10);
            if (Issuccss[0] == 0x01)
                return Issuccss[0];
            else return 0x04;
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


        ~High_PrecisionClass()
        {
            GC.Collect();
            Closeport();
        }
    }

    public static class TestResult
    {
        public static High_PrecisionClass WorkTest = new High_PrecisionClass();

    }


}
