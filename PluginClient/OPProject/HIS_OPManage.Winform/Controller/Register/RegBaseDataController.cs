using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmRegBaseDataSet")]//与系统菜单对应
    [WinformView(Name = "FrmRegBaseDataSet", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRegBaseDataSet")]
   
    /// <summary>
    /// 挂号基础数据维护界面控制器
    /// </summary>
    public class RegBaseDataController:WcfClientController
    {
        /// <summary>
        /// 挂号基础数据维护界面接口
        /// </summary>
        IFrmRegBaseDataSet ifrmRegBaseData;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmRegBaseData = (IFrmRegBaseDataSet)iBaseView["FrmRegBaseDataSet"];
        }

        /// <summary>
        /// 加载所有挂号类别
        /// </summary>
        [WinformMethod]
        public void LoadRegType()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "GetRegTypes");
            DataTable regtypes = retdata.GetData<DataTable>(0);           
            ifrmRegBaseData.loadRegTypes(regtypes);
        }

        /// <summary>
        /// 加载挂号类别对应的收费项目明细
        /// </summary>
        /// <param name="regtypeid">挂号类别ID</param>
        [WinformMethod]
        public void LoadRegItems(int  regtypeid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(regtypeid);                
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "GetRegItems",requestAction);
            DataTable  dtRegItems = retdata.GetData<DataTable>(0);
            ifrmRegBaseData.GetDgRegItemFees(dtRegItems);
        }

        /// <summary>
        /// 保存和修改挂号类别
        /// </summary>
        [WinformMethod]
        public void SaveRegType()
        {
            try
            {
                OP_RegType regtype = ifrmRegBaseData.CurRegtype;
                regtype.PyCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(regtype.RegTypeName);
                regtype.WbCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(regtype.RegTypeName);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(regtype);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "SaveRegtype", requestAction);

                LoadRegType();
            }
            catch (Exception err)
            {
                LoadRegType();
                MessageBoxShowError(err.Message);
            }
        }

        /// <summary>
        /// 选项卡数据源
        /// </summary>
        DataTable dtItems = new DataTable();

        /// <summary>
        /// 挂号类别
        /// </summary>
        DataTable regtypes = new DataTable();

        /// <summary>
        /// 异步加载信息
        /// </summary>
        public override void AsynInit()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "GetRegItemShowCard");
            dtItems = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 异步完成
        /// </summary>
        public override void AsynInitCompleted()
        {
            ifrmRegBaseData.SetRegItemShowCard(dtItems);
        }

        /// <summary>
        /// 获取收费项目录入选项卡数据源
        /// </summary>
        [WinformMethod]
        public void GetRegItemShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "GetRegItemShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            ifrmRegBaseData.SetRegItemShowCard(dt);
        }

        /// <summary>
        /// 保存项目明细
        /// </summary>
        [WinformMethod]
        public void SaveRegItemFees()
        {
            try
            {
                OP_RegType regtype = ifrmRegBaseData.CurRegtype;
                DataTable dt = ifrmRegBaseData.DtRegTypeFees;
                DataView dv = dt.DefaultView;
                dv.RowFilter = " itemname<>'' or itemname is not null";
                DataTable dtclo = dv.ToTable();
                List<OP_RegItemFee> regItemFees = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_RegItemFee>(dtclo);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(regtype);
                    request.AddData(regItemFees);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "SaveRegItemFees", requestAction);
                MessageBoxShowSimple("保存成功");
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }

        /// <summary>
        /// 删除费用明细
        /// </summary>
        /// <param name="id">挂号明细ID</param>
        [WinformMethod]
        public void DeleteRegItemFees(int id)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(id);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegBaseDataController", "DeleteRegItemFees", requestAction);
                MessageBoxShowSimple("删除成功");
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
    }
}
