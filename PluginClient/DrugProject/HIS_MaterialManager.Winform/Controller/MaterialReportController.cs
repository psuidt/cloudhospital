using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.DrugManage;
using HIS_MaterialManage.Winform.IView.Report;
using HIS_MaterialManage.Winform.ViewForm;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 报表统计控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmGoodsCollection")]//在菜单上显示
    [WinformView(Name = "FrmGoodsCollection", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmGoodsCollection")]//物资汇总
    [WinformView(Name = "FrmGoodsDetailBill", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmGoodsDetailBill")]//物资明细账
    [WinformView(Name = "FrmGoodsSortBill", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmGoodsSortBill")]//物资分类流水账
    [WinformView(Name = "FrmValidAlarm", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmValidAlarm")]//物资报警
    [WinformView(Name = "FrmMaterialInventoryStatistic", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialInventoryStatistic")]//进销存
    [WinformView(Name = "FrmMaterialTypeChoose", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialTypeChoose")]//进销存--物资类型树选择窗体
    public class MaterialReportController : WcfClientController
    {
        /// <summary>
        /// 物资汇总接口
        /// </summary>
        IFrmGoodsCollection frmGoodsCollection;

        /// <summary>
        /// 物资明细查询接口
        /// </summary>
        IFrmGoodsDetailBill frmGoodsDetailBill;

        /// <summary>
        /// 分类流水账接口
        /// </summary>
        IFrmGoodsSortBill frmGoodsSortBill;

        /// <summary>
        /// 物资库存报警接口
        /// </summary>
        IFrmValidAlarm frmValidAlarm;

        /// <summary>
        /// 进销存统计接口
        /// </summary>
        IFrmMaterialInventoryStatistic frmMaterialInventoryStatistic;

        /// <summary>
        /// 物资类型选择接口
        /// </summary>
        IFrmMaterialTypeChoose iFrmMaterialTypeChoose;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmGoodsCollection = (IFrmGoodsCollection)iBaseView["FrmGoodsCollection"];
            frmGoodsDetailBill = (IFrmGoodsDetailBill)iBaseView["FrmGoodsDetailBill"];
            frmGoodsSortBill = (IFrmGoodsSortBill)iBaseView["FrmGoodsSortBill"];
            frmValidAlarm = (IFrmValidAlarm)iBaseView["FrmValidAlarm"];
            frmMaterialInventoryStatistic = (IFrmMaterialInventoryStatistic)iBaseView["FrmMaterialInventoryStatistic"];
            iFrmMaterialTypeChoose = (IFrmMaterialTypeChoose)iBaseView["FrmMaterialTypeChoose"];
        }

        #region 进销存统计
        /// <summary>
        /// 获取库房下拉列表信息
        /// </summary>
        /// <param name="formName">入口界面</param>
        [WinformMethod]
        public void GetStoreRoom(string formName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetStoreRoomData");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (formName == "FrmMaterialInventoryStatistic")
            {
                frmMaterialInventoryStatistic.BindDeptRoom(dt);
            }
            else if (formName == "FrmGoodsDetailBill")
            {
                frmGoodsDetailBill.BindDeptRoom(dt);
            }
        }

        /// <summary>
        /// 取得物资类型
        /// </summary>
        [WinformMethod]
        public void GetMaterialType()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetMaterialTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得会计年
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetAcountYears(string frmName, int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetAcountYears", requestAction);
            DataTable dtYear = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialInventoryStatistic")
            {
                frmMaterialInventoryStatistic.BindYear(dtYear);
            }
            else if (frmName == "FrmGoodsDetailBill")
            {
                frmGoodsDetailBill.BindYear(dtYear);
            }
        }

        /// <summary>
        /// 取得会计月
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        [WinformMethod]
        public void GetAcountMonths(string frmName, int deptId, int year)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(year);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetAcountMonths", requestAction);
            DataTable dtmonth = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialInventoryStatistic")
            {
                frmMaterialInventoryStatistic.BindMonth(dtmonth);
            }
            else if (frmName == "FrmGoodsDetailBill")
            {
                frmGoodsDetailBill.BindMonth(dtmonth);
            }
        }

        /// <summary>
        /// 取得当前用户名称
        /// </summary>
        /// <returns>用户名称</returns>
        [WinformMethod]
        public string GetCurrentUserName()
        {
            return LoginUserInfo.EmpName;
        }

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">类型ID</param>
        /// <returns>统计DataTable</returns>
        [WinformMethod]
        public DataTable InventoryStatistic(int deptId, int queryYear, int queryMonth, int typeId)
        {
            DataTable dt = new DataTable();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(queryYear);
                request.AddData(queryMonth);
                request.AddData(typeId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "InventoryStatistic", requestAction);
            dt = retdata.GetData<DataTable>(0);
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

                        if (i > 0 && i < dt.Rows.Count - 2)
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

        #region 物资明细查询
        /// <summary>
        /// 获取入库单物资数据
        /// </summary>
        [WinformMethod]
        public void GetMaterialInfo()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetMaterialInfo");
            frmGoodsDetailBill.BindMaterialInfoCard(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 取得会计月开始结束日期
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日期信息</returns>
        [WinformMethod]
        public DataTable GetBalanceDate(int deptId, int year, int month)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(year);
                request.AddData(month);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetBalanceDate", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得物资明细账数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="materialId">物资Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>物资明细账数据</returns>
        [WinformMethod]
        public DataTable GetAccountDetail(int deptId, int year, int month, string beginTime, string endTime, int materialId, int queryType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
                request.AddData(year);
                request.AddData(month);
                request.AddData(beginTime);
                request.AddData(endTime);
                request.AddData(materialId);
                request.AddData(queryType);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetAccountDetail", requestAction);
            frmGoodsDetailBill.BindTotalInfo(retdata.GetData<DataTable>(1));
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得表头信息
        /// </summary>
        /// <param name="opType">业务类型</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="detailID">明细Id</param>
        /// <returns>表头信息</returns>
        [WinformMethod]
        public BillMasterShower GetBillHeadInfo(string opType, int deptId, int detailID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(opType);
                request.AddData(deptId);
                request.AddData(detailID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetBillHeadInfo", requestAction);
            return retdata.GetData<BillMasterShower>(0);
        }
        #endregion

        #region 有效期预警

        /// <summary>
        /// 查询物资库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void LoadMaterialStorage(string frmName,string typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmValidAlarm.GetQueryCondition());
                request.AddData(typeId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "LoadMaterialStorages", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmValidAlarm.BindStoreGrid(dtRtn);
        }
        #endregion

        #region 物资汇总

        /// <summary>
        /// 获取月结信息
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <param name="frmName">入口窗体</param>
        [WinformMethod]
        public void GetMWBalance(string deptid, string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptid);
            });

            ServiceResponseData retdata = null;
            retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetMWBalance", requestAction);
            if (frmName == "FrmGoodsSortBill")
            {
                frmGoodsSortBill.BindBalance(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmGoodsCollection.BindBalance(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptRoomData(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetDeptRoomData");
            if (frmName == "FrmGoodsSortBill")
            {
                frmGoodsSortBill.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else
            {
                frmGoodsCollection.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
        }

        /// <summary>
        /// 获取往来科室
        /// </summary>
        /// <param name="frmName">如果窗体</param>
        /// <param name="deptID">科室ID</param>
        [WinformMethod]
        public void GetDeptRelation(string frmName, string deptID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmName);
                request.AddData(deptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetDeptRelation", requestAction);
            if (frmName == "FrmGoodsSortBill")
            {
                frmGoodsSortBill.BindDept(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmGoodsCollection.BindDept(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        [WinformMethod]
        public void GetSupportDic(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmName);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetSupportDic", requestAction);
            if (frmName == "FrmGoodsSortBill")
            {
                frmGoodsSortBill.BindSupport(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmGoodsCollection.BindSupport(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetInStore(string frmName,object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsCollection.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetInStore", requestAction);
            frmGoodsCollection.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetOutStore(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsCollection.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetOutStore", requestAction);
            frmGoodsCollection.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetCheck(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsCollection.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetCheck", requestAction);
            frmGoodsCollection.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetAdjPrice(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsCollection.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetAdjPrice", requestAction);
            frmGoodsCollection.BindDgData(retdata.GetData<DataTable>(0));
        }
        #endregion

        #region 物资流水账表
        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetInStores(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsSortBill.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetInStores", requestAction);
            frmGoodsSortBill.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetOutStores(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsSortBill.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetOutStores", requestAction);
            frmGoodsSortBill.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <param name="frmName">如果窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetChecks(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsSortBill.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetChecks", requestAction);
            frmGoodsSortBill.BindDgData(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <param name="frmName">入口窗体</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void GetAdjPrices(string frmName, object typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmGoodsSortBill.GetQueryCondition());
                request.AddData(frmName);
                if (typeId != null)
                {
                    request.AddData(typeId.ToString());
                }
                else
                {
                    request.AddData(string.Empty);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialReportController", "GetAdjPrices", requestAction);
            frmGoodsSortBill.BindDgData(retdata.GetData<DataTable>(0));
        }
        #endregion

        #region 物资类型选择窗体
        /// <summary>
        /// 获取物资类型列表
        /// </summary>
        [WinformMethod]
        public void GetMaterialTypeList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(new Dictionary<string, string>());
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service",
                "MaterialTypeController",
                "GetMaterialType",
                requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmMaterialTypeChoose.LoadMaterialType(dt);
        }

        /// <summary>
        /// 选择物资类型
        /// </summary>
        /// <param name="fatherFrmname">父窗体名称</param>
        [WinformMethod]
        public void OpenMaterialTypeDialog(string fatherFrmname)
        {
            var dialog = iBaseView["FrmMaterialTypeChoose"] as FrmMaterialTypeChoose;
            dialog.ShowDialog();
            if (dialog.Result == 1)
            {
                if (fatherFrmname == "FrmMaterialInventoryStatistic")
                {
                    frmMaterialInventoryStatistic.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
                }
                else if (fatherFrmname == "FrmGoodsCollection")
                {
                    frmGoodsCollection.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
                }
                else if(fatherFrmname == "FrmValidAlarm")
                {
                    frmValidAlarm.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
                }
                else if (fatherFrmname == "FrmGoodsSortBill")
                {
                    frmGoodsSortBill.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
                }
            }
        }
    }
    #endregion
}