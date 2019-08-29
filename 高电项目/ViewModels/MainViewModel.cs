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
        public byte TestSpeed { get; set; }
        public float TestIn { get; set; }
        public float TestIx1 { get; set; }
        public float TestIx2 { get; set; }
        public float TestIx3 { get; set; }
        public float TestIx4 { get; set; }
        public float TestPh1 { get; set; }
        public float TestPh2 { get; set; }
        public float TestPh3 { get; set; }
        public float TestPh4 { get; set; }
        public float TestRn { get; set; }
        public float TestRx1 { get; set; }
        public float TestRx2 { get; set; }
        public float TestRx3 { get; set; }
        public float TestRx4 { get; set; }
        public float TestFre { get; set; }
        public float TestVoalte { get; set; }
        public float TestPower { get; set; }

        #endregion

        public void StartTest()
        {
            try
            {
                TestResult.WorkTest.StartTest();
                TestResult.WorkTest.OutTestResult += WorkTest_OutTestResult1;
            }
            catch
            {
            }

        }
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
        private void WorkTest_OutTestResult1(byte[] result)
        {
            if (result.Length == 19)
            {
                ViewSources vs = new ViewSources(result);
                TestFre = vs.TestFre;
                TestVoalte = vs.TestVoalte;
                TestIn = vs.TestIn;
                TestIx1 = vs.TestIx1;
                TestIx2 = vs.TestIx2;
                TestIx3 = vs.TestIx3;
                TestIx4 = vs.TestIx4;
                TestPh1 = vs.TestPh1;
                TestPh2 = vs.TestPh2;
                TestPh3 = vs.TestPh3;
                TestPh4 = vs.TestPh4;
                TestRn = vs.TestRn;
                TestRx1 = vs.TestRx1;
                TestRx2 = vs.TestRx2;
                TestRx3 = vs.TestRx3;
                TestRx4 = vs.TestRx4;
                TestSpeed = vs.TestSpeed;
            }
        }

    }

}




