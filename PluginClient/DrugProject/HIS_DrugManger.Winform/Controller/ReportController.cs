using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.Report;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 报表统计控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmInventoryRptDS")]//在菜单上显示
    [WinformView(Name = "FrmInventoryRptDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInventoryRpt")]//进销存统计-药房
    [WinformView(Name = "FrmInventoryRptDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInventoryRpt")]//进销存统计-药库
    [WinformView(Name = "FrmOrderRptDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOrderRpt")]//药品明细查询-药房
    [WinformView(Name = "FrmOrderRptDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOrderRpt")]//药品明细查询-药库
    [WinformView(Name = "FrmValidAlarmYF", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmValidAlarm")]//药品有效期预警-药房
    [WinformView(Name = "FrmValidAlarmYK", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmValidAlarm")]//药品有效期预警-药库
    [WinformView(Name = "FrmTotalAccountDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmTotalAccount")]//药品汇总账本-药房
    [WinformView(Name = "FrmTotalAccountDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmTotalAccount")]//药品汇总账本-药库
    [WinformView(Name = "FrmDispRpt", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDispRpt")]//药品发药统计-药房
    [WinformView(Name = "FrmFlowAccountDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmFlowAccount")]//药品分类流水统计-药库
    [WinformView(Name = "FrmFlowAccountDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmFlowAccount")]//药品分类流水统计-药房
    [WinformView(Name = "FrmBuyComparison", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmBuyComparison")]//药品采购入库对比报表-药库
    [WinformView(Name = "FrmNewDrug", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmNewDrug")]//新药入库统计-药库
    public class ReportController : WcfClientController
    {
        /// <summary>
        /// 进销存统计
        /// </summary>
        IFrmInventoryRpt frmInventoryRpt;

        /// <summary>
        /// 药品明细查询
        /// </summary>
        IFrmOrderRpt frmOrderRpt;

        /// <summary>
        /// 药品有效期预警药房
        /// </summary>
        IFrmValidAlarm frmValidAlarm;

        /// <summary>
        /// 药品汇总账本药房
        /// </summary>
        IFrmTotalAccount frmTotalAccount;

        /// <summary>
        /// 药品汇总账本药库
        /// </summary>
        IFrmTotalAccount frmTotalAccountDW;

        /// <summary>
        /// 发药统计
        /// </summary>
        IFrmDispRpt frmDispRpt;

        /// <summary>
        /// 药品分类流水统计药库
        /// </summary>
        IFrmFlowAccount frmFlowAccountDW;

        /// <summary>
        /// 药品有效期预警药库
        /// </summary>
        IFrmValidAlarm frmValidAlarmYK;

        /// <summary>
        /// 药品分类流水统计药房
        /// </summary>
        IFrmFlowAccount frmFlowAccountDS;

        /// <summary>
        /// 药房进销存统计
        /// </summary>
        IFrmInventoryRpt frmInventoryRptDS;

        /// <summary>
        /// 药库进销存统计
        /// </summary>
        IFrmInventoryRpt frmInventoryRptDW;

        /// <summary>
        /// 药房药品明细
        /// </summary>
        IFrmOrderRpt frmOrderRptDS;

        /// <summary>
        /// 药库药品明细
        /// </summary>
        IFrmOrderRpt frmOrderRptDW;

        /// <summary>
        /// 药品采购入库对比报表
        /// </summary>
        IFrmBuyComparison iFrmBuyComparison;

        /// <summary>
        /// 新药入库统计
        /// </summary>
        IFrmNewDrug iFrmNewDrug;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmInventoryRpt = (IFrmInventoryRpt)iBaseView["FrmInventoryRptDS"];
            frmOrderRpt = (IFrmOrderRpt)iBaseView["FrmOrderRptDS"];
            frmValidAlarm = (IFrmValidAlarm)iBaseView["FrmValidAlarmYF"];
            frmTotalAccount = (IFrmTotalAccount)iBaseView["FrmTotalAccountDS"];
            frmTotalAccountDW = (IFrmTotalAccount)iBaseView["FrmTotalAccountDW"];
            frmDispRpt = (IFrmDispRpt)iBaseView["FrmDispRpt"];
            frmFlowAccountDW = (IFrmFlowAccount)iBaseView["FrmFlowAccountDW"];
            frmFlowAccountDS = (IFrmFlowAccount)iBaseView["FrmFlowAccountDS"];
            frmValidAlarmYK = (IFrmValidAlarm)iBaseView["FrmValidAlarmYK"];
            frmInventoryRptDS = (IFrmInventoryRpt)iBaseView["FrmInventoryRptDS"];//药房进销存统计
            frmInventoryRptDW = (IFrmInventoryRpt)iBaseView["FrmInventoryRptDW"];//药库进销存统计
            frmOrderRptDS = (IFrmOrderRpt)iBaseView["FrmOrderRptDS"];//药房药品明细
            frmOrderRptDW = (IFrmOrderRpt)iBaseView["FrmOrderRptDW"];//药库药品明细
            iFrmBuyComparison = (IFrmBuyComparison)iBaseView["FrmBuyComparison"];//采购入库对比报表
            iFrmNewDrug = (IFrmNewDrug)iBaseView["FrmNewDrug"];//新药入库统计
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDrugTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmValidAlarmYF")
            {
                //药房
                frmValidAlarm.BindTypeCombox(dt);
            }
            else if (frmName == "FrmDispRpt")
            {
                frmDispRpt.BindTypeCombox(dt);
            }
            else if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                frmInventoryRptDS.BindDrugTypeDicComboBox(dt);
            }
            else if (frmName == "FrmInventoryRptDW")
            {
                //药库进销存统计
                frmInventoryRptDW.BindDrugTypeDicComboBox(dt);
            }
            else if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindTypeCombox(dt);
            }
            else if (frmName == "FrmTotalAccountDW")
            {
                frmTotalAccountDW.BindTypeCombox(dt);
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindTypeCombox(dt);
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                frmFlowAccountDW.BindTypeCombox(dt);
            }
            else
            {
                frmValidAlarmYK.BindTypeCombox(dt);
            }
        }

        /// <summary>
        /// 获取子药品类型信息   
        /// </summary>
        /// <param name="typeID">父类型ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetChildDrugType(string typeID, string frmName)
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                Dictionary<string, string> QueryCondition = new Dictionary<string, string>();
                QueryCondition.Add("TypeID", typeID);
                request.AddData(QueryCondition);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetChildDrugType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmDispRpt")
            {
                frmDispRpt.BindChildDrugType(dt);
            }
            else if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindChildDrugType(dt);
            }
            else if (frmName == "FrmTotalAccountDW")
            {
                frmTotalAccountDW.BindChildDrugType(dt);
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                frmFlowAccountDW.BindChildDrugType(dt);
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindChildDrugType(dt);
            }
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadDrugStorage(string frmName)
        {
            if (frmName == "FrmValidAlarmYF")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(frmValidAlarm.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmValidAlarm.BindStoreGrid(dtRtn);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(frmValidAlarmYK.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmValidAlarmYK.BindStoreGrid(dtRtn);
            }
        }

        /// <summary>
        /// 获取月结信息
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDGBalance(string deptid, string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptid);
            });
            ServiceResponseData retdata = null;
            if (frmName == "FrmTotalAccountDS")
            {
                retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDSBalance", requestAction);
                frmTotalAccount.BindBalance(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDWBalance", requestAction);
                frmFlowAccountDW.BindBalance(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDSBalance", requestAction);
                frmFlowAccountDS.BindBalance(retdata.GetData<DataTable>(0));
            }
            else
            {
                retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDWBalance", requestAction);
                frmTotalAccountDW.BindBalance(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取库房下拉列表信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptRoomData(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmDispRpt" || frmName == "FrmTotalAccountDS" || frmName == "FrmFlowAccountDS")
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                }
                else
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                }
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRoomData", requestAction);
            if (frmName == "FrmDispRpt")
            {
                frmDispRpt.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmTotalAccountDW")
            {
                frmTotalAccountDW.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                frmFlowAccountDW.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmBuyComparison")
            {
                iFrmBuyComparison.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else if (frmName == "FrmNewDrug")
            {
                iFrmNewDrug.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
        }

        /// <summary>
        /// 获取0住院/1门诊发药记录
        /// </summary>
        /// <param name="type">类型</param>
        [WinformMethod]
        public void GetDispTotal(string type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(type);
                request.AddData(frmDispRpt.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDispTotal", requestAction);
            frmDispRpt.BindDGDrug(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取处方科室信息
        /// </summary>
        /// <param name="type">类型</param>
        [WinformMethod]
        public void GetDGDept(string type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(type);
                if (type == "0")
                {
                    request.AddData(frmDispRpt.CurrentHead["DeptID"].ToString());
                }
                else
                {
                    request.AddData(frmDispRpt.CurrentHead["PresDeptID"].ToString());
                }

                request.AddData(frmDispRpt.CurrentHead["DrugID"].ToString());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDispDetail", requestAction);
            frmDispRpt.BindDGDept(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取往来科室
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="deptID">科室ID</param>
        [WinformMethod]
        public void GetDeptRelation(string frmName, string deptID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmName);
                request.AddData(deptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRelation", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDept(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                frmFlowAccountDW.BindDept(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDept(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindDept(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetSupportDic(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetSupportDic", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindSupport(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDW")
            {
                frmFlowAccountDW.BindSupport(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindSupport(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindSupport(retdata.GetData<DataTable>(0));
            }
        }

        #region 药品汇总表

        /// <summary>
        ///  获取入库查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetInStore(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmTotalAccountDS")
                {
                    request.AddData(frmTotalAccount.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmTotalAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetInStore", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetOutStore(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmTotalAccountDS")
                {
                    request.AddData(frmTotalAccount.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmTotalAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetOutStore", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetCheck(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmTotalAccountDS")
                {
                    request.AddData(frmTotalAccount.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmTotalAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetCheck", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetAdjPrice(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmTotalAccountDS")
                {
                    request.AddData(frmTotalAccount.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmTotalAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAdjPrice", requestAction);
            if (frmName == "FrmTotalAccountDS")
            {
                frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmTotalAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        ///  获取住院发药信息
        /// </summary>
        [WinformMethod]
        public void GetIPDisp()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmTotalAccount.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetIPDisp", requestAction);
            frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        ///  获取门诊发药信息
        /// </summary>
        [WinformMethod]
        public void GetOPDisp()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmTotalAccount.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetOPDisp", requestAction);
            frmTotalAccount.BindDgData(retdata.GetData<DataTable>(0));
        }
        #endregion

        #region 药品流水账表
        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetInStores(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmFlowAccountDS")
                {
                    request.AddData(frmFlowAccountDS.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmFlowAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetInStores", requestAction);
            if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmFlowAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetOutStores(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmFlowAccountDS")
                {
                    request.AddData(frmFlowAccountDS.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmFlowAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetOutStores", requestAction);
            if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmFlowAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetChecks(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmFlowAccountDS")
                {
                    request.AddData(frmFlowAccountDS.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmFlowAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetChecks", requestAction);
            if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmFlowAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetAdjPrices(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmFlowAccountDS")
                {
                    request.AddData(frmFlowAccountDS.GetQueryCondition());
                }
                else
                {
                    request.AddData(frmFlowAccountDW.GetQueryCondition());
                }

                request.AddData(frmName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAdjPrices", requestAction);
            if (frmName == "FrmFlowAccountDS")
            {
                frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmFlowAccountDW.BindDgData(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        ///  获取住院发药信息
        /// </summary>
        [WinformMethod]
        public void GetIPDisps()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmFlowAccountDS.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetIPDisps", requestAction);
            frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        ///  获取门诊发药信息
        /// </summary>
        [WinformMethod]
        public void GetOPDisps()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmFlowAccountDS.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetOPDisps", requestAction);
            frmFlowAccountDS.BindDgData(retdata.GetData<DataTable>(0));
        }
        #endregion

        #region 进销存统计
        /// <summary>
        /// 获取库房下拉列表信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetStoreRoom(string frmName)
        {
            if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRoomData", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmInventoryRptDS.BindDeptRoom(dt);
            }
            else if (frmName == "FrmOrderRptDS")
            {
                //药房药品明细查询
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRoomData", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmOrderRptDS.BindDeptRoom(dt);
            }
            else if (frmName == "FrmOrderRptDW")
            {
                //药库药品明细查询
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRoomData", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmOrderRptDW.BindDeptRoom(dt);
            }
            else if (frmName == "FrmInventoryRptDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDeptRoomData", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmInventoryRptDW.BindDeptRoom(dt);
            }
        }

        /// <summary>
        /// 取得药品类型
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugType(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDrugTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                frmInventoryRptDS.BindDrugTypeDicComboBox(dt);
            }
            else if (frmName == "FrmInventoryRptDW")
            {
                //药库进销存统计
                frmInventoryRptDW.BindDrugTypeDicComboBox(dt);
            }
        }

        /// <summary>
        /// 取得会计年
        /// </summary>
        /// <param name="frmName">窗体入口名</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetAcountYears(string frmName, int deptId)
        {
            if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountYears", requestAction);
                DataTable dtYear = retdata.GetData<DataTable>(0);
                frmInventoryRptDS.BindYear(dtYear);
            }
            else if (frmName == "FrmOrderRptDW")
            {
                //药品明细-药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountYears", requestAction);
                DataTable dtYear = retdata.GetData<DataTable>(0);

                frmOrderRptDW.BindYear(dtYear);
            }
            else if (frmName == "FrmOrderRptDS")
            {
                //药品明细-药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountYears", requestAction);
                DataTable dtYear = retdata.GetData<DataTable>(0);
                frmOrderRptDS.BindYear(dtYear);
            }
            else if (frmName == "FrmInventoryRptDW")
            {
                //药库进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountYears", requestAction);
                DataTable dtYear = retdata.GetData<DataTable>(0);
                frmInventoryRptDW.BindYear(dtYear);
            }
        }

        /// <summary>
        /// 取得会计月
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        [WinformMethod]
        public void GetAcountMonths(string frmName, int deptId, int year)
        {
            if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountMonths", requestAction);
                DataTable dtmonth = retdata.GetData<DataTable>(0);
                frmInventoryRptDS.BindMonth(dtmonth);
            }
            else if (frmName == "FrmOrderRptDW")
            {
                //药品明细-药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountMonths", requestAction);
                DataTable dtmonth = retdata.GetData<DataTable>(0);
                frmOrderRptDW.BindMonth(dtmonth);
            }
            else if (frmName == "FrmOrderRptDS")
            {
                //药品明细-药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountMonths", requestAction);
                DataTable dtmonth = retdata.GetData<DataTable>(0);
                frmOrderRptDS.BindMonth(dtmonth);
            }
            else if (frmName == "FrmInventoryRptDW")
            {
                //药库进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAcountMonths", requestAction);
                DataTable dtmonth = retdata.GetData<DataTable>(0);
                frmInventoryRptDW.BindMonth(dtmonth);
            }
        }

        /// <summary>
        /// 取得当前用户名称
        /// </summary>
        /// <returns>登录人名称</returns>
        [WinformMethod]
        public string GetCurrentUserName()
        {
            return LoginUserInfo.EmpName;
        }

        /// <summary>
        /// 进销存统计报表
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="queryYear">年</param>
        /// <param name="queryMonth">月</param>
        /// <param name="typeId">类型</param>
        /// <returns>进销存统计数据源</returns>
        [WinformMethod]
        public DataTable InventoryStatistic(string frmName, int deptId, int queryYear, int queryMonth, int typeId)
        {
            DataTable dt = new DataTable();
            if (frmName == "FrmInventoryRptDS")
            {
                //药房进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(queryYear);
                    request.AddData(queryMonth);
                    request.AddData(typeId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "InventoryStatistic", requestAction);
                dt = retdata.GetData<DataTable>(0);
            }
            else
            {
                //药库进销存统计
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(queryYear);
                    request.AddData(queryMonth);
                    request.AddData(typeId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "InventoryStatistic", requestAction);
                dt = retdata.GetData<DataTable>(0);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    continue;
                }

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "0")
                    {
                        if (i == 0 && j <= 2)
                        {
                            continue;
                        }

                        if (i == dt.Rows.Count - 2 && j > 2)
                        {
                            continue;
                        }

                        dt.Rows[i][j] = DBNull.Value;
                    }
                }
            }

            return dt;
        }
        #endregion

        #region 药品明细查询
        /// <summary>
        /// 获取入库单药品数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugInfo(string frmName)
        {
            if (frmName == "FrmOrderRptDW")
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDrugInfo", requestAction);
                frmOrderRptDW.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
            else
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetDrugInfo", requestAction);
                frmOrderRptDS.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 取得会计月开始结束日期
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="deptId">科室id</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>月结数据集</returns>
        [WinformMethod]
        public DataTable GetBalanceDate(string frmName, int deptId, int year, int month)
        {
            if (frmName == "FrmOrderRptDW")
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                    request.AddData(month);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetBalanceDate", requestAction);
                return retdata.GetData<DataTable>(0);
            }
            else
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                    request.AddData(month);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetBalanceDate", requestAction);
                return retdata.GetData<DataTable>(0);
            }
        }

        /// <summary>
        /// 取得药品明细账数据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">药品Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>获取药品明细账数据集</returns>
        [WinformMethod]
        public DataTable GetAccountDetail(string frmName, int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType)
        {
            if (frmName == "FrmOrderRptDW")
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                    request.AddData(month);
                    request.AddData(beginTime);
                    request.AddData(endTime);
                    request.AddData(drugId);
                    request.AddData(queryType);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAccountDetail", requestAction);
                frmOrderRptDW.BindTotalInfo(retdata.GetData<DataTable>(1));
                return retdata.GetData<DataTable>(0);
            }
            else
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(deptId);
                    request.AddData(year);
                    request.AddData(month);
                    request.AddData(beginTime);
                    request.AddData(endTime);
                    request.AddData(drugId);
                    request.AddData(queryType);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetAccountDetail", requestAction);
                frmOrderRptDS.BindTotalInfo(retdata.GetData<DataTable>(1));
                return retdata.GetData<DataTable>(0);
            }
        }
        
        /// <summary>
        /// 获取单据头信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="opType">类型</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="detailID">明细ID</param>
        /// <returns>返回实体</returns>
        [WinformMethod]
        public BillMasterShower GetBillHeadInfo(string frmName, string opType, int deptId, int detailID)
        {
            if (frmName == "FrmOrderRptDW")
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(opType);
                    request.AddData(deptId);
                    request.AddData(detailID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetBillHeadInfo", requestAction);
                return retdata.GetData<BillMasterShower>(0);
            }
            else
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(opType);
                    request.AddData(deptId);
                    request.AddData(detailID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetBillHeadInfo", requestAction);
                return retdata.GetData<BillMasterShower>(0);
            }
        }
        #endregion

        #region 采购入库对比
        /// <summary>
        /// 获取采购入库对比表
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        [WinformMethod]
        public void GetBuyComparison(int deptId, string yearMonth, string drugName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(yearMonth);
                request.AddData(drugName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetBuyComparison", requestAction);
            iFrmBuyComparison.BindGridData(retdata.GetData<DataTable>(0));
        }
        #endregion

        #region 新药入库统计
        /// <summary>
        /// 新药入库统计
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        [WinformMethod]
        public void GetNewDrug(int deptId, string yearMonth, string drugName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(yearMonth);
                request.AddData(drugName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ReportController", "GetNewDrug", requestAction);
            iFrmNewDrug.BindGridData(retdata.GetData<DataTable>(0));
        }
        #endregion
    }
}
