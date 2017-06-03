using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_MIInterface.Winform.IView;
using System.Windows.Forms;
using EFWCoreLib.WcfFrame.DataSerialize;
using System.Data;

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMICostUpload")]//与系统菜单对应
    [WinformView(Name = "FrmMICostUpload", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmMICostUpload")]
    public class MICostUploadController : WcfClientController
    {
        IFrmMICostUpload iFrmMICostUpload; //测试界面
        private Dictionary<string, string> columnNames = new Dictionary<string, string>();
        public override void Init()
        {
            iFrmMICostUpload = (IFrmMICostUpload)iBaseView["FrmMICostUpload"];
        }

        [WinformMethod]
        public void M_GetMIType()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "M_GetMIType");
            
            DataTable dtMemberInfoMz = retdataMember.GetData<DataTable>(0);
            DataTable dtMemberInfoZy = retdataMember.GetData<DataTable>(1);
            iFrmMICostUpload.LoadMIType(dtMemberInfoMz, dtMemberInfoZy);
        }

        [WinformMethod]
        public void M_GetMIDept()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "M_GetMIDept");
            DataTable dtDeptInfo = retdataMember.GetData<DataTable>(0);

            iFrmMICostUpload.LoadDept(dtDeptInfo);
        }

        [WinformMethod]
        public void Zy_GetMIPatient(int iMiType, int iDeptCode )
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iMiType);
                request.AddData(iDeptCode);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "Zy_GetMIPatient", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMICostUpload.LoadMIPatient(dtMemberInfo);
        }
        
        [WinformMethod]
        public void Zy_GetPatientInfo(int iPatientId,  int iFeeType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iPatientId);
                request.AddData(iFeeType);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "Zy_GetPatientInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMICostUpload.LoadPatientInfo(dtMemberInfo);


        }

        [WinformMethod]
        public void Zy_ExportPatientFee(int ybId, List<DataTable> dtList,string CaseNumber)
        {
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    MessageBox.Show("数据源为空！");
            //    return;
            //}

            string sColumnName = "";
            try
            {
                string sType = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "filetype");
               
                sColumnName = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "columnNames");

                string sSeparator = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "separator");

                List<Dictionary<string, string>> columnNamesList = new List<Dictionary<string, string>>();

                string[] sColumnNames = sColumnName.Split('^');
                for (int i = 0; i < sColumnNames.Length; i++)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    string[] sColumnNamess = sColumnNames[i].Split('|');

                    foreach (string s in sColumnNamess)
                    {
                        dss.Add(s.Split(',')[0], s.Split(',')[1]);
                    }
                    columnNamesList.Add(dss);
                }

                ExportFile doExport = new ExportFile("zyfj"+CaseNumber, columnNamesList, sSeparator);
                ExportType _exportType = sType.Trim() == "1" ? ExportType.Excel : ExportType.Txt;
                if (doExport.InitShowDialog(_exportType))
                {
                    if (doExport.DoExportWork(dtList))
                    {
                        doExport.OpenFile();
                        Zy_UploadzyPatFee(Convert.ToInt32(dtList[1].Rows[0]["PatListID"].ToString()), 1);
                    }
                }
            }
            catch
            {
                MessageBox.Show("导出失败！");
                return;
            }
        }
        [WinformMethod]
        public bool Zy_UploadzyPatFee(int iPatientId,int iFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iPatientId);
                request.AddData(iFlag);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIZyController", "Zy_UploadzyPatFee", requestAction);
            bool bMemberInfo = retdataMember.GetData<bool>(0);
            if (bMemberInfo)
            {
                iFrmMICostUpload.Refresh();
            }
            return bMemberInfo;
        }
        [WinformMethod]
        public bool Zy_ReSetzyPatFee(int feeRecordID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(feeRecordID);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "Zy_ReSetzyPatFee", requestAction);
            bool bMemberInfo = retdataMember.GetData<bool>(0);
            if (bMemberInfo)
            {
                iFrmMICostUpload.Refresh();
            }
            return bMemberInfo;
        }
        public string M_GetMedicalInsuranceData(int typeId, string method, string name)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
                request.AddData(method);
                request.AddData(name);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetMedicalInsuranceData", requestAction);
            string sMemberInfo = retdataMember.GetData<string>(0);
            return sMemberInfo;
        }


        [WinformMethod]
        public void Mz_GetOutPatientFee(int iPatType, DateTime dDate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iPatType);
                request.AddData(dDate);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "Mz_GetOutPatientFee", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMICostUpload.LoadOutPatientFee(dtMemberInfo);
        }
        [WinformMethod]
        public void Mz_ExportPatientFee(int ybId, DataTable dt)
        {

            string sColumnName = "";
            try
            {
                string sType = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "filetype");

                sColumnName = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "columnNames");

                string sSeparator = M_GetMedicalInsuranceData(ybId, "ExportPatientFee", "separator");

                List<Dictionary<string, string>> columnNamesList = new List<Dictionary<string, string>>();

                string[] sColumnNames = sColumnName.Split('^');
                for (int i = 0; i < sColumnNames.Length; i++)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    string[] sColumnNamess = sColumnNames[i].Split('|');

                    foreach (string s in sColumnNamess)
                    {
                        dss.Add(s.Split(',')[0], s.Split(',')[1]);
                    }
                    columnNamesList.Add(dss);
                }

                ExportFile doExport = new ExportFile("HIS病人费用目录", columnNamesList, sSeparator);
                ExportType _exportType = sType.Trim() == "1" ? ExportType.Excel : ExportType.Txt;
                if (doExport.InitShowDialog(_exportType))
                {
                    //if (doExport.DoExportWork(dt))
                    //    doExport.OpenFile();
                }

            }
            catch
            {
                MessageBox.Show("导出失败！");
                return;
            }

            //if (Zy_UploadzyPatFee(Convert.ToInt32(dtList[1].Rows[0]["PatListID"].ToString())))
            //{
            //    iFrmMICostUpload.Refresh();
            //}

        }
    }
}
