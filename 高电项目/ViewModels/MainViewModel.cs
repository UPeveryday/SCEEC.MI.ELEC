using Stylet;
using System;
using System.Linq;
using SCEEC.MI.High_Precision;
using System.IO.Ports;
using SCEEC.Numerics;

namespace 高电项目.ViewModels
{

    public class MainViewModel : Screen
    {
        #region Ui parameter data
        public string TestSpeed { get; set; }
        public string TestIn { get; set; }
        public string TestIx1 { get; set; }
        public string TestIx2 { get; set; }
        public string TestIx3 { get; set; }
        public string TestIx4 { get; set; }
        public string TestPh1 { get; set; }
        public string TestPh2 { get; set; }
        public string TestPh3 { get; set; }
        public string TestPh4 { get; set; }
        public string TestRn { get; set; }
        public string TestRx1 { get; set; }
        public string TestRx2 { get; set; }
        public string TestRx3 { get; set; }
        public string TestRx4 { get; set; }
        public string TestFre { get; set; }
        public string TestVoalte { get; set; }
        public string TestPower { get; set; }
        public string TestU0 { get; set; }
        public string TestCn { get; set; }
        public string TestCx1 { get; set; }
        public string TestCx2 { get; set; }
        public string TestCx3 { get; set; }
        public string TestCx4 { get; set; }


        public string Testθ0 { get; set; }
        public string TestTan1 { get; set; }
        public string TestTan2 { get; set; }
        public string TestTan3 { get; set; }
        public string TestTan4 { get; set; }
        public string TestChannel { get; set; }
        #endregion

        #region 开始测试
        public void StartTest()
        {
            byte IsPort;
            IsPort = TestResult.WorkTest.StartTest();
            if (IsPort == 0)
                throw new Exception("串口打开失败");
            TestResult.WorkTest.OutTestResult += WorkTest_OutTestResult1;
        }
        #endregion

        #region Stylet实现跳出窗口，可用于单元测试
        private readonly IWindowManager windowManager;

        public MainViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        public void ShowMessagebox()
        {
            var result = this.windowManager.ShowMessageBox("Hello");
        }

        #endregion

        #region 接受测试数据
        private void WorkTest_OutTestResult1(byte[] result)
        {
            if (result.Length == 55)
            {
                ViewSources vs = new ViewSources(result);
                TestFre = vs.TestFre.ToString();
                TestVoalte = vs.TestVoalte.ToString();
                TestIn = vs.TestIn.ToString();
                TestIx1 = vs.TestIx1.ToString();
                TestIx2 = vs.TestIx2.ToString();
                TestIx3 = vs.TestIx3.ToString();
                TestIx4 = vs.TestIx4.ToString();
                TestPh1 = vs.TestPh1.ToString();
                TestPh2 = vs.TestPh2.ToString();
                TestPh3 = vs.TestPh3.ToString();
                TestPh4 = vs.TestPh4.ToString();
                TestRn = vs.TestRn.ToString();
                TestRx1 = vs.TestRx1.ToString();
                TestRx2 = vs.TestRx2.ToString();
                TestRx3 = vs.TestRx3.ToString();
                TestRx4 = vs.TestRx4.ToString();
                TestSpeed = vs.TestSpeed.ToString();
                TestU0 = vs.TestU0.ToString();
                TestCx1 = vs.TestCx1.ToString();
                TestCx2 = vs.TestCx2.ToString();
                TestCx3 = vs.TestCx3.ToString();
                TestCx4 = vs.TestCx4.ToString();
                Testθ0 = vs.Testθ0.ToString();
                TestTan1 = vs.TestTan1.ToString();
                TestTan2 = vs.TestTan2.ToString();
                TestTan3 = vs.TestTan3.ToString();
                TestTan4 = vs.TestTan4.ToString();
            }
        }
        #endregion

        #region 改变Cn
        public void ChangeCn()
        {
            double fcn;//= Convert.ToDouble(TestCn);
            bool isf = double.TryParse(TestCn, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo, out fcn);
            if (isf)
                TestResult.WorkTest.ChangeTestCn((float)fcn);
        }
        #endregion

        #region 改变Cn角度
        public void ChangeCnTan()
        {

        }
        #endregion

        #region 改变测量通道
        public void ChangeChannel()
        {
            byte fcn = Convert.ToByte(TestChannel);
            TestResult.WorkTest.ChangeTestChannel((byte)fcn);

        }
        #endregion

        #region 改变测量速度
        public void ChangSpeed()
        {
            int fcn = Convert.ToInt32(TestSpeed);
            TestResult.WorkTest.ChangeTestSpeed((byte)fcn);
        }
        #endregion

       
    }

}




