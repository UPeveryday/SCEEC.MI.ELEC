using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCEEC.MI.ELEC;


namespace 高电项目.ViewModels
{
    public class MainHignViewModel : Screen
    {


        #region Base Data
        public float Fre { get; set; }
        public float An { get; set; }
        public float Ax { get; set; }
        public float Ph { get; set; }
        public float Umean { get; set; }
        public double Cn { get; set; } = 1e-10;
        public double Text { get; set; } = 1e-10;
        public double Temper { get; set; } = 75;

        public double _Df_tan { get; set; }
        public double _Df_tan_75 { get; set; }
        public double _Df_tan_Per100 { get; set; }
        public double _Df_tan_75_Per100 { get; set; }
        public double _Pf_cos { get; set; }
        public double _Pf_cos_75 { get; set; }
        public double _Pf_cos_Per100 { get; set; }
        public double _Pf_cos_75_Per100 { get; set; }
        public double _Urms { get; set; }
        public double _Urms_sqrt3 { get; set; }
        public double _Urect_mean { get; set; }
        public double _In_rms { get; set; }
        public double _Ix_rms { get; set; }
        public double _Fre { get; set; }
        public double _Fre_database { get; set; }
        public double _Zx { get; set; }
        public double _Yx { get; set; }
        public double _I_mag_LP { get; set; }
        public double _sita_Zx { get; set; }
        public double _Cx { get; set; }
        public double _S { get; set; }
        public double _P { get; set; }
        public double _Q { get; set; }
        public double _P_25 { get; set; }
        public double _P_10 { get; set; }
        public double _Cn { get; set; }
        public double _temper { get; set; }

        public double _I_fe_rp { get; set; }
        public double _Cp { get; set; }
        private double _Rp_CR { get; set; }
        public double _Rs_CR { get; set; }
        public double _Cs { get; set; }
        public double _Ls { get; set; }
        public double _Rs_LR { get; set; }
        public double _Lp { get; set; }
        public double _Rp_LR { get; set; }
        public List<string> Message { get; set; }
        #endregion


        public byte Channel { get; set; }
        public void StartTest()
        {
            TestHighClass.ElecClass.StartTest(Channel);
            TestHighClass.ElecClass.SendMessages += ElecClass_SendMessages;
        }

        private void ElecClass_SendMessages(byte[] bits)
        {
            float fre = BitConverter.ToSingle(bits, 2);
            float an = BitConverter.ToSingle(bits, 6);
            float ax = BitConverter.ToSingle(bits, 10);
            float ph = BitConverter.ToSingle(bits, 14);
            float umean = BitConverter.ToSingle(bits, 18);
            Models.ViewSources vs = new Models.ViewSources(fre, an, ax, ph, umean);
            this.An = vs.An;
            this.Ax = vs.Ax;
            this.Ph = vs.Ph;
            this.Umean = vs.Umean;
            this._Df_tan = vs._Df_tan;
            this._Df_tan_75 = vs._Df_tan_75;
            this._Df_tan_75_Per100 = vs._Df_tan_75_Per100;
            this._Df_tan_Per100 = vs._Df_tan_Per100;
            this._Pf_cos = vs._Pf_cos;
            this._Pf_cos_75 = vs._Pf_cos_75;
            this._Pf_cos_75_Per100 = vs._Pf_cos_75_Per100;
            this._Pf_cos_Per100 = vs._Pf_cos_Per100;
            this._Urms = vs._Urms;
            this._Urms_sqrt3 = vs._Urms_sqrt3;
            this._Urect_mean = vs._Urect_mean;
            this._In_rms = vs._In_rms;
            this._Ix_rms = vs._Ix_rms;
            this._Fre = vs._Fre;
            this._Fre_database = vs._Fre_database;
            this._Zx = vs._Zx;
            this._Yx = vs._Yx;
            this._sita_Zx = vs._sita_Zx;
            this._Cx = vs._Cx;
            this._Cn = vs._Cn;
            this._S = vs._S;
            this._P = vs._P;
            this._Q = vs._Q;
            this._temper = vs._temper;
            this._P_10 = vs._P_10;
            this._P_25 = vs._P_25;
            // this._sita_Zx = vs._Df_tan;
            this._I_mag_LP = vs._I_mag_LP;
            this._I_fe_rp = vs._I_fe_rp;
            this._Cp = vs._Cp;
            this._Rp_CR = vs._Rp_CR;
            this._Cs = vs._Cs;
            this._Rs_CR = vs._Rs_CR;
            this._Rs_LR = vs._Rs_LR;
            this._Ls = vs._Ls;
            this._Lp = vs._Lp;
            this._Rp_LR = vs._Rp_LR;
        }

        public void Loading()
        {
            string[] data = { "data1", "data2", "data3", "data1", "data2", "data3" ,"data1", "data2", "data3", "data1", "data2", "data3", "data1", "data2", "data3", "data1", "data2", "data3" };
            Message = new List<string>();
            Message.AddRange(data);
        }
        
    }
}
