using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using HIS_IPEMR.Winform.IView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.Mongo;
using System.Data;

namespace HIS_IPEMR.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmRecordManager")]//在菜单上显示
    [WinformView(Name = "FrmRecordManager", DllName = "HIS_IPEMR.Winform.dll", ViewTypeName = "HIS_IPEMR.Winform.ViewForm.FrmRecordManager")]

    public class RecordManagerController: WcfClientController
    {
        IFrmRecordManager ifrmRecordManager;
        public override void Init()
        {
            ifrmRecordManager = (IFrmRecordManager)iBaseView["FrmRecordManager"];
        }
        [WinformMethod]
        public void GetTemplateRecordList()
        {
            int deptid = ifrmRecordManager.DeptId;
            if (deptid == 0)
            {
                deptid = LoginUserInfo.DeptId;
            }
            int workId = LoginUserInfo.WorkId;
            int doctorId = LoginUserInfo.UserId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmRecordManager.RoleType);
                request.AddData(deptid);
                request.AddData(ifrmRecordManager.MedicalTreeId);
                request.AddData(ifrmRecordManager.tempTypeId);
                request.AddData(ifrmRecordManager.TemplateName);
                request.AddData(doctorId);
            });
            ServiceResponseData retdata = InvokeWcfService("EMRBaseProject.Service", "RecordManagerController", "GetTemplateRecordList", requestAction);
            DataTable dtMedicalRecord = retdata.GetData<DataTable>(0);
            ifrmRecordManager.BindTemplateRecord(dtMedicalRecord);
        }
    }
}
