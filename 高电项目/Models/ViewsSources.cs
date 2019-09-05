using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高电项目.Models
{
    public class ViewSources
    {
        #region Base Data
        public float Fre { get; set; }
        public float An { get; set; }
        public float Ax { get; set; }
        public float Ph { get; set; }
        public float Umean { get; set; }
        public double Cn { get; set; }
        public double Temper { get; set; }

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
        public double _Rp_CR { get; set; }
        public double _Rs_CR { get; set; }
        public double _Cs { get; set; }
        public double _Ls { get; set; }
        public double _Rs_LR { get; set; }
        public double _Lp { get; set; }
        public double _Rp_LR { get; set; }
        #endregion
        public ViewSources(float _FRE, float _An, float _Ax, float _Ph, float _Umean, double _Cn=1e-10, double _temper = 75)
        {
            _An /= 1000;
            _Ax /= 1000;
            this.Fre = _FRE;
            this.An = _An;
            this.Ax = _Ax;
            this.Ph = _Ph;
            this.Umean = _Umean;
            this.Cn = _Cn;
            this.Temper = _temper;
            double w = 2 * Math.PI * _FRE;


            this._Df_tan = Math.Tan(_Ph);
            this._Df_tan_75 = _Df_tan * Math.Pow(1.3, (Convert.ToDouble((75 - _temper)) / 10));//t1为设定的值
            //                                                                                                              // string result = string.Format("{0:0.00%}", percent);//得到5.88%
            this._Df_tan_Per100 = _Df_tan * 100;
            //this._Df_tan_75_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Df_tan_75));
            this._Df_tan_75_Per100 = _Df_tan_75 * 100;

            this._Pf_cos = Math.Cos(Math.PI / 2 - _Ph);
            this._Pf_cos_75 = Math.Sqrt((_Df_tan_75 * _Df_tan_75) / (_Df_tan_75 * _Df_tan_75 + 1));
            //this._Pf_cos_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Pf_cos));
            //this._Pf_cos_75_Per100 = Convert.ToDouble(string.Format("{0:0.00%}", _Pf_cos_75));
            this._Pf_cos_Per100 = _Pf_cos * 100;
            this._Pf_cos_75_Per100 = _Pf_cos_75 * 100;
            this._Urms = _An / (2 * Math.PI * _FRE * _Cn);/// Math.Sqrt(2)
            this._Urms_sqrt3 = _Urms / Math.Sqrt(3);
            this._Urect_mean = _Umean;
            this._In_rms = _An;
            this._Ix_rms = _Ax;

            this._Fre = _FRE;
            this._Fre_database = _FRE;

            this._Zx = _An * (2 * Math.PI * _FRE * _Cn * _Ax);//提供接口
            this._Yx = (2 * Math.PI * _FRE * _Cn * _Ax) / _An;//提供接口
            this._sita_Zx = _Ph;

            this._Cx = _Ax * _Cn / (_An * Math.Cos(_Ph));
            this._Cn = _Cn;//提供的值，修改

            this._S = _Ax * _An / (2 * Math.PI * _FRE * _Cn);//Cn
            this._P = _Ax * _An / (2 * Math.PI * _FRE * _Cn) * Math.Cos(_Ph);
            this._Q = _Ax * _An / (2 * Math.PI * _FRE * _Cn) * Math.Sin(_Ph);
            this._temper = _temper;//提供的值，修改
            this._P_10 = Math.PI;//提供的值，修改
            this._P_25 = Math.PI;//提供的值，修改

            //this._sita_Zx = Math.PI / 2 - _Ph;
            this._I_mag_LP = _Ax * Math.Cos(_Ph);
            this._I_fe_rp = _Ax * Math.Sin(_Ph);
            this._Cp = _Cx;
            this._Rp_CR = _An * Math.Sin(_Ph) / (w * _Cn * _Ax);
            this._Cs = _Cn * (1 + Math.Tan(_Ph) * Math.Tan(_Ph));
            this._Rs_CR = _Cp * (Math.Tan(_Ph) * Math.Tan(_Ph) / (1 + Math.Tan(_Ph) * Math.Tan(_Ph)));
            this._Ls = Math.Sqrt(1 / (_Cs * w * w));
            this._Rs_LR = _Rs_CR;
            this._Lp = Math.Sqrt(1 / (_Cp * w * w));
            this._Rp_LR = _Rp_CR;


        }
        ~ViewSources()
        {
             GC.Collect();
        }
    }
}
