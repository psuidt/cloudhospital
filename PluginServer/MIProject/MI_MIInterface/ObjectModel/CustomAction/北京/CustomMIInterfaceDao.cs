using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using MI_MIInterface.ObjectModel.BaseClass;
using MI_MIInterface.ObjectModel.Common;
using MedicareComLib;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace MI_MIInterface.ObjectModel.CustomAction.beijing
{
    public class CustomMIInterfaceDao: AbstractMIInterfaceDao
    {
        private SiInterface siInterface = new SiInterface();
        private string sUserCode = "";//操作员编号
        private string sHosCode = "";//医疗机构编码
        private string sSignInCode = "";//业务周期号（社保系统交易流水号）
        private string sCenterCode = "441400";//中心编码
        public string m_ErrorMsg = "";//社保返回信息
        public StringBuilder c_Msg = new StringBuilder(100000);

        public override void BeginInitData(Hashtable param)
        {
            //根据ActionMapping.xml里面配置
            //string sValue = ActionMappingConfig.GetHospitalDataValue("GreatCommunicationLib.Server.CustomAction.广东梅州市.MeiZhouAction", Convert.ToString(param["HospitalID"]), "YBUserName");
            //string sHosValue = ActionMappingConfig.GetHospitalDataValue("GreatCommunicationLib.Server.CustomAction.广东梅州市.MeiZhouAction", Convert.ToString(param["HospitalID"]), "HospitalAttribute");
            OutpatientClass hObj = new OutpatientClass();
        }
        public override void EndInitData()
        {
            base.EndInitData();
        }

        //签到
        public DataTable MZ_GetCardInfo()
        {
            DataTable dt = null;
            return dt;
        }

        public  DataTable MZ_GetPersonInfo()
        {
            throw new NotImplementedException();
        }

        public  bool MZ_Register()
        {
            throw new NotImplementedException();
        }

        public  bool Mz_CancelRegister()
        {
            throw new NotImplementedException();
        }

        public  DataTable MZ_PreviewCharge()
        {
            throw new NotImplementedException();
        }

        public  DataTable MZ_Charge()
        {
            throw new NotImplementedException();
        }

        public  bool MZ_CancelCharge()
        {
            throw new NotImplementedException();
        }

        public  bool MZ_UploadzyPatFee()
        {
            throw new NotImplementedException();
        }

        public  DataTable MZ_DownloadzyPatFee()
        {
            throw new NotImplementedException();
        }

        public  DataTable MZ_LoadFeeDetailByTicketNum()
        {
            throw new NotImplementedException();
        }


        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
    }
}
