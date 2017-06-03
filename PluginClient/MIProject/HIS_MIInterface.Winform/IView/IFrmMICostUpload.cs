using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using System.Data;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmMICostUpload : IBaseView
    {
        void LoadMIType(DataTable dtMz, DataTable dtZy);

        void LoadDept(DataTable dtDept);
        void LoadMIPatient(DataTable dt);
        void LoadPatientInfo(DataTable dt);

        void Refresh();


        /// <summary>
        /// 加载门诊数据
        /// </summary>
        /// <param name="dt"></param>
        void LoadOutPatientFee(DataTable dt);
    }
}
