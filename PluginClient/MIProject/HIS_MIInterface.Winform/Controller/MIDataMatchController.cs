using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MIInterface.Winform.IView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMIDataMatch")]//与系统菜单对应
    [WinformView(Name = "FrmMIDataMatch", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmMIDataMatch")]

    public class MIDataMatchController : WcfClientController
    {
        IFrmMIDataMatch iFrmMIDataMatch; //测试界面
        private Dictionary<string, string> columnNames = new Dictionary<string, string>();
        public override void Init()
        {
            iFrmMIDataMatch = (IFrmMIDataMatch)iBaseView["FrmMIDataMatch"];
        }
        /// <summary>
        /// 获取医保类型
        /// </summary>
        [WinformMethod]
        public void M_GetMIType()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetMIType");
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIDataMatch.LoadMIType(dtMemberInfo);
        }

        [WinformMethod]
        public void M_GetHISDataInfo(int ybid,int dataType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybid);
                request.AddData(dataType);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIDataMatchController", "M_GetHISDataInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIDataMatch.LoadHISDataInfo(dtMemberInfo);
        }
        /// <summary>
        /// 获取医保目录
        /// </summary>
        [WinformMethod]
        public void M_GetMIDataInfo(int ybid, int dataType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybid);
                request.AddData(dataType);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIDataMatchController", "M_GetMIDataInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIDataMatch.LoadMIDataInfo(dtMemberInfo);
        }
        /// <summary>
        /// 获取匹配目录
        /// </summary>
        [WinformMethod]
        public void M_GetMatchDataInfo(int ybid, int dataType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybid);
                request.AddData(dataType);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIDataMatchController", "M_GetMatchDataInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIDataMatch.LoadMatchDataInfo(dtMemberInfo);
        }

        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <param name="id"></param>
        [WinformMethod]
        public void M_DeleteMatchData(string id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIDataMatchController", "M_DeleteMatchData", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("操作成功！");
                iFrmMIDataMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("操作失败！");
            }
        }

        /// <summary>
        /// 保存基础数据匹配
        /// </summary>
        /// <param name="id"></param>
        [WinformMethod]
        public void M_SaveMatchData(int iMIDataID, int iDataType, int iHospDataID, int iMIID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iMIDataID);
                request.AddData(iDataType);
                request.AddData(iHospDataID);
                request.AddData(iMIID);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIDataMatchController", "M_SaveMatchData", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("操作成功！");
                iFrmMIDataMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("操作失败！");
            }
        }
    }
}
