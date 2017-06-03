using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EfwControls.HISControl.Prescription.Controls;
using EfwControls.HISControl.Prescription.Controls.Entity;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 处方控件数据库接口实现类
    /// </summary>
    public class OPDPresDbHelper : WcfClientController, IPrescripttionDbHelper
    {
        #region 属性
        /// <summary>
        /// 药品项目数据
        /// </summary>
        private DataTable dtDrugItems = null;

        /// <summary>
        /// 嘱托数据
        /// </summary>
        private List<Basic_Entrust> ltEntrust = null;

        /// <summary>
        /// 频次数据
        /// </summary>
        private List<Basic_Frequency> ltFrequency = null;

        /// <summary>
        /// 用法数据
        /// </summary>
        private List<Basic_Channel> ltUsage = null;
        #endregion

        /// <summary>
        /// 刷新药品数据
        /// </summary>
        public void RefreshDrugData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(1);
                request.AddData(100);
                request.AddData(SelectedDrugRoomID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetShowCardData", requestAction);
            dtDrugItems = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 选中药房ID
        /// </summary>
        public int SelectedDrugRoomID { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public int SelectedMemberID { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="selectedDrugRoomID">药房Id</param>
        public OPDPresDbHelper(int selectedDrugRoomID)
        {
            SelectedDrugRoomID = selectedDrugRoomID;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public OPDPresDbHelper()
        {
        }

        /// <summary>
        /// 模板另存为
        /// </summary>
        /// <param name="pId">节点ID</param>
        /// <param name="level">模板等级</param>
        /// <param name="mName">模板名称</param>
        /// <param name="presType">模板类型</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="doctorId">医生ID</param>
        /// <param name="data">保存数据</param>
        public void AsSavePresTemplate(int pId, int level, string mName, int presType, int deptId, int doctorId, List<Prescription> data)
        {
            HIS_Entity.ClinicManage.OPD_PresMouldHead newMould = new HIS_Entity.ClinicManage.OPD_PresMouldHead();
            newMould.CreateDate = DateTime.Now;
            newMould.CreateDeptID = deptId;
            newMould.CreateEmpID = doctorId;
            newMould.PresType = presType;
            newMould.MouldType = 1;
            newMould.ModulLevel = level;
            newMould.ModuldName = mName;
            newMould.DelFlag = 0;
            newMould.PID = pId;
            List<OPD_PresDetail> presList = new List<OPD_PresDetail>();
            foreach (Prescription pres in data)
            {
                OPD_PresDetail newDetail = new OPD_PresDetail();
                newDetail.PresDetailID = pres.PresListId;
                presList.Add(newDetail);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mName);
                request.AddData(presType);
                request.AddData(level);
                request.AddData(pId);
                request.AddData(0);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "FeeTemplateController", "CheckName", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result == false)
            {
                MessageBoxEx.Show("同级别模板名称不能重复！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(newMould);
                    request.AddData(presList);
                });
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "AsSavePresTemplate", requestAction);
                MessageBoxShowSimple("另存模板成功!");
            }
        }

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="presListId">处方明细Id</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int presListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presListId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "DeletePrescriptionData", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int patListId, int presType, int presNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
                request.AddData(presType);
                request.AddData(presNo);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "DeleteGroupPrescriptionData", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 获取药品信息
        /// </summary>
        /// <param name="itemId">药品Id</param>
        /// <returns>药品模型</returns>
        public CardDataSourceDrugItem GetDrugItem(int itemId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(itemId);
                request.AddData(SelectedDrugRoomID);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugItem", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            List<CardDataSourceDrugItem> list_DrugItem = new List<CardDataSourceDrugItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CardDataSourceDrugItem mDrugItem = new CardDataSourceDrugItem();
                mDrugItem.ItemId = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemID"], 0);
                mDrugItem.ItemName = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.TradeName = dt.Rows[i]["TradeName"].ToString();
                mDrugItem.ItemName_Print = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.Standard = dt.Rows[i]["Standard"].ToString();

                mDrugItem.Scale = string.Empty;//补偿比例
                mDrugItem.StoreNum = ConvertDataExtend.ToDecimal(dt.Rows[i]["StoreAmount"], 0);//库存量
                mDrugItem.UnPickUnit = dt.Rows[i]["UnPickUnit"].ToString();//包装单位(销售单位)
                mDrugItem.PresFactor = ConvertDataExtend.ToInt32(dt.Rows[i]["MiniConvertNum"], 1);
                mDrugItem.MiniUnit = dt.Rows[i]["MiniUnitName"].ToString();
                mDrugItem.SellPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["UnitPrice"], 0);//销售价格?
                mDrugItem.BuyPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["InPrice"], 0);//进价
                mDrugItem.ExecDeptName = dt.Rows[i]["ExecDeptName"].ToString();//执行科室?
                mDrugItem.Pym = dt.Rows[i]["Pym"].ToString();
                mDrugItem.Wbm = dt.Rows[i]["Wbm"].ToString();
                mDrugItem.Address = dt.Rows[i]["Address"].ToString();//生产厂家

                mDrugItem.DoseUnitName = dt.Rows[i]["DoseUnitName"].ToString();
                mDrugItem.DoseConvertNum = ConvertDataExtend.ToDecimal(dt.Rows[i]["DoseConvertNum"], 0);//剂量对应包装单位系数
                int statID = ConvertDataExtend.ToInt32(dt.Rows[i]["StatID"], 0);
                int itemClass = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemClass"], 0);
                int itemClassId = GetItemType(itemClass, statID);
                mDrugItem.ItemType = itemClassId;//项目类型 0-所有 1-西药 2-中药  3-处方可开的物品 4-收费项目
                mDrugItem.StatItemCode = statID.ToString();//大项目代码

                mDrugItem.ExecDeptId = ConvertDataExtend.ToInt32(dt.Rows[i]["ExecDeptId"], 0);//执行科室ID?
                mDrugItem.FloatFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["FloatFlag"], 0);// dt.Rows[i][""].ToString();//按含量取整1 按剂量取整0
                mDrugItem.VirulentFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["VirulentFlag"], 0); //剧毒标识
                mDrugItem.NarcoticFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["NarcoticFlag"], 0); //麻醉标识
                mDrugItem.SkinTestFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["SkinTestFlag"], 0);//皮试标识
                mDrugItem.RecipeFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["RecipeFlag"], 0);//处方标识
                mDrugItem.LunacyFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["LunacyFlag"], 0);//精神药品标识
                mDrugItem.CostlyFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["CostlyFlag"], 0);//贵重药品标识
                mDrugItem.default_Usage_Amount = 0;//默认用量
                mDrugItem.default_Usage_Id = 0;//默认用法
                mDrugItem.default_Usage_Name = string.Empty;
                mDrugItem.default_Frequency_Id = 0;//默认频次
                mDrugItem.default_Frequency_Name = string.Empty;
                //西药和成药是否可拆零
                if (statID == 100 || statID == 101)
                {
                    mDrugItem.ResolveFlag = 0;
                }
                else
                {
                    mDrugItem.ResolveFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["ResolveFlag"], 0);
                }

                mDrugItem.ChemPym = dt.Rows[i]["ChemPym"].ToString();
                mDrugItem.ChemWbm = dt.Rows[i]["ChemWbm"].ToString();
                list_DrugItem.Add(mDrugItem);
            }

            return list_DrugItem.Count > 0 ? list_DrugItem[0] : null;
        }

        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <param name="itemClass">项目类型</param>
        /// <param name="statId">统计大项目Id</param>
        /// <returns>项目类型id</returns>
        private int GetItemType(int itemClass, int statId)
        {
            int iRtn = 0;
            if (itemClass == 1)
            {
                if (statId == 100 || statId == 101)
                {
                    iRtn = 1;
                }
                else
                {
                    iRtn = 2;
                }
            }
            else if (itemClass == 2)
            {
                iRtn = 3;
            }
            else if (itemClass == 2)
            {
                iRtn = 4;
            }

            return iRtn;
        }

        /// <summary>
        /// 获取药品项目信息
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>药品项目信息</returns>
        public List<CardDataSourceDrugItem> GetDrugItem(int type, int pageNo, int pageSize, string filter)
        {
            // 全部 = 0, 西药与中成药 = 1, 中草药 = 2, 收费项目 = 3
            int statId = 100;
            if (type == 1)
            {
                statId = 100;
            }
            else if (type == 2)
            {
                statId = 102;
            }
            else if (type == 3)
            {
                statId = 0;
            }

            List<CardDataSourceDrugItem> list_DrugItem = new List<CardDataSourceDrugItem>();

            if (dtDrugItems == null || dtDrugItems.Rows.Count == 0)
            {
                if (SelectedDrugRoomID == 0)
                {
                    Action<ClientRequestData> requestActionTemp = ((ClientRequestData request) =>
                    {
                        request.AddData(0);
                    });
                    ServiceResponseData retdataTemp = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugStoreRoom", requestActionTemp);
                    DataTable dtWest = retdataTemp.GetData<DataTable>(0);
                    if (dtWest != null && dtWest.Rows.Count > 0)
                    {
                        SelectedDrugRoomID = Convert.ToInt32(dtWest.Rows[0]["DeptID"]);
                    }
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(type);
                    request.AddData(statId);
                    request.AddData(SelectedDrugRoomID);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetShowCardData", requestAction);
                dtDrugItems = retdata.GetData<DataTable>(0);
            }

            //过滤处方数据
            DataTable dt = FilterPresData(type, statId);

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Empty;
            dv.RowFilter = "Pym like '%" + filter + "%' or Wbm like '%" + filter + "%' or ItemName like '%" + filter + "%'";
            dt = dv.ToTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CardDataSourceDrugItem mDrugItem = new CardDataSourceDrugItem();
                mDrugItem.ItemId = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemID"], 0);
                mDrugItem.ItemName = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.TradeName = dt.Rows[i]["TradeName"].ToString();
                mDrugItem.ItemName_Print = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.Standard = dt.Rows[i]["Standard"].ToString();

                mDrugItem.Scale = string.Empty;//补偿比例
                mDrugItem.StoreNum = ConvertDataExtend.ToDecimal(dt.Rows[i]["StoreAmount"], 0);//库存量
                mDrugItem.UnPickUnit = dt.Rows[i]["UnPickUnit"].ToString();//包装单位(销售单位)
                mDrugItem.PresFactor = ConvertDataExtend.ToInt32(dt.Rows[i]["MiniConvertNum"], 1);
                mDrugItem.MiniUnit = dt.Rows[i]["MiniUnitName"].ToString();
                mDrugItem.SellPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["UnitPrice"], 0);//基本单位价格?
                mDrugItem.BuyPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["InPrice"], 0);//进价
                mDrugItem.ExecDeptName = dt.Rows[i]["ExecDeptName"].ToString();//执行科室?
                mDrugItem.ExecDeptId = ConvertDataExtend.ToInt32(dt.Rows[i]["ExecDeptId"], 0);
                mDrugItem.StatItemCode = dt.Rows[i]["StatID"].ToString();
                mDrugItem.Pym = dt.Rows[i]["Pym"].ToString();
                mDrugItem.Wbm = dt.Rows[i]["Wbm"].ToString();
                mDrugItem.Address = dt.Rows[i]["Address"].ToString();//生产厂家
                //mDrugItem.DoseUnitId = dt.Rows[i][""].ToString();//剂量单位
                mDrugItem.DoseUnitName = dt.Rows[i]["DoseUnitName"].ToString();
                mDrugItem.DoseConvertNum = ConvertDataExtend.ToDecimal(dt.Rows[i]["DoseConvertNum"], 0);//剂量对应包装单位系数
                int statID = ConvertDataExtend.ToInt32(dt.Rows[i]["StatID"], 0);
                int itemClass = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemClass"], 0);
                int itemClassId = GetItemType(itemClass, statID);
                mDrugItem.ItemType = itemClassId;//项目类型 0-所有 1-西药 2-中药  3-处方可开的物品 4-收费项目
                mDrugItem.StatItemCode = statID.ToString();//大项目代码
                mDrugItem.ExecDeptId = ConvertDataExtend.ToInt32(dt.Rows[i]["ExecDeptId"], 0);//执行科室ID?
                mDrugItem.FloatFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["FloatFlag"], 0);// dt.Rows[i][""].ToString();//1按含量取整 0按总量取整
                mDrugItem.VirulentFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["VirulentFlag"], 0); //剧毒标识
                mDrugItem.NarcoticFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["NarcoticFlag"], 0); //麻醉标识
                mDrugItem.SkinTestFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["SkinTestFlag"], 0);//皮试标识
                mDrugItem.RecipeFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["RecipeFlag"], 0);//处方标识
                mDrugItem.LunacyFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["LunacyFlag"], 0);//精神药品标识
                mDrugItem.CostlyFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["CostlyFlag"], 0);//贵重药品标识
                mDrugItem.MedicareID = ConvertDataExtend.ToInt32(dt.Rows[i]["MedicareID"], 0);//医保类型ID
                mDrugItem.MedicareName = dt.Rows[i]["MedicareName"].ToString();//医保类型名称
                mDrugItem.MedicarePer = dt.Rows[i]["MedicarePer"].ToString();//医保报销比例
                mDrugItem.default_Usage_Amount = 0;//默认用量
                mDrugItem.default_Usage_Id = 0;//默认用法
                mDrugItem.default_Usage_Name = string.Empty;
                mDrugItem.default_Frequency_Id = 0;//默认频次
                mDrugItem.default_Frequency_Name = string.Empty;
                mDrugItem.TradeName = dt.Rows[i]["TradeName"].ToString();
                mDrugItem.ChemPym = dt.Rows[i]["ChemPym"].ToString();
                mDrugItem.ChemWbm = dt.Rows[i]["ChemWbm"].ToString();
                if (statID == 100 || statID == 101)
                {
                    mDrugItem.ResolveFlag = 0;
                }
                else
                {
                    mDrugItem.ResolveFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["ResolveFlag"], 0);
                }
                list_DrugItem.Add(mDrugItem);
            }

            return list_DrugItem;
        }

        /// <summary>
        /// 过滤处方数据
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="statId">统计大项目id</param>
        /// <returns>处方数据</returns>
        private DataTable FilterPresData(int type, int statId)
        {
            DataTable dtTemp = new DataTable();
            dtTemp = dtDrugItems.Copy();
            string filterCondition = string.Empty;
            if (type == 1 || type == 2)
            {
                filterCondition = "ItemClass =1";
                if (statId == 100 || statId == 101)
                {
                    filterCondition = filterCondition + " and StatID in(100,101)";
                }
                else
                {
                    filterCondition = filterCondition + " and StatID=102";
                }

                if (SelectedDrugRoomID > -1)
                {
                    filterCondition = filterCondition + " and ExecDeptId =" + SelectedDrugRoomID.ToString();
                }
            }
            else
            {
                filterCondition = "ItemClass in(2,3)";
            }

            DataView dvFilter = dtTemp.DefaultView;
            dvFilter.RowFilter = filterCondition;
            DataTable dt = dvFilter.ToTable();
            return dt;
        }

        /// <summary>
        /// 获取嘱托
        /// </summary>
        /// <returns>嘱托数据</returns>
        public List<CardDataSourceEntrust> GetEntrust()
        {
            if (ltEntrust == null || ltEntrust.Count == 0)
            {
                ServiceResponseData retEntrustData = InvokeWcfService("OPProject.Service", "PresManageController", "GetEntrustData");
                List<Basic_Entrust> listEntrust = retEntrustData.GetData<List<Basic_Entrust>>(0);
                ltEntrust = listEntrust;
            }

            List<CardDataSourceEntrust> lists = new List<CardDataSourceEntrust>();
            foreach (Basic_Entrust entrust in ltEntrust)
            {
                CardDataSourceEntrust mEnturst = new CardDataSourceEntrust();
                mEnturst.Name = entrust.EntrustName;
                mEnturst.Id = entrust.EntrustID;
                mEnturst.Pym = entrust.PYCode;
                mEnturst.Wbm = entrust.WBCode;
                lists.Add(mEnturst);
            }

            return lists;
        }

        /// <summary>
        /// 获取频次数据
        /// </summary>
        /// <returns>频次数据</returns>
        public List<CardDataSourceFrequency> GetFrequency()
        {
            if (ltFrequency == null || ltFrequency.Count == 0)
            {
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetFrequencyData");
                List<Basic_Frequency> list = retData.GetData<List<Basic_Frequency>>(0);
                ltFrequency = list;
            }

            List<CardDataSourceFrequency> lists = new List<CardDataSourceFrequency>();
            foreach (Basic_Frequency model in ltFrequency)
            {
                CardDataSourceFrequency myFrequency = new CardDataSourceFrequency();
                myFrequency.FrequencyId = model.FrequencyID;
                myFrequency.Name = model.FrequencyName;
                myFrequency.Caption = model.ExecuteCode;
                myFrequency.Pym = model.PYCode;
                myFrequency.Wbm = model.WBCode;
                myFrequency.Szm = string.Empty;
                myFrequency.ExecuteType = model.ExecuteType;
                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(myFrequency.Caption, out execNum, out cycleDay);
                myFrequency.ExecNum = execNum;//执行次数
                myFrequency.CycleDay = cycleDay;//执行周期
                lists.Add(myFrequency);
            }

            return lists;
        }

        /// <summary>
        /// 获取单位
        /// </summary>
        /// <param name="stockId">药品id</param>
        /// <param name="type">类型0剂量1总量</param>
        /// <returns>单位</returns>
        public List<CardDataSourceUnit> GetUnit(int stockId, int type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stockId);
                request.AddData(SelectedDrugRoomID);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugItem", requestAction);
            DataTable dtunit = retData.GetData<DataTable>(0);
            List<CardDataSourceUnit> list_unit = new List<CardDataSourceUnit>();

            //剂量
            if (type == 0)
            {
                CardDataSourceUnit munit = new CardDataSourceUnit();
                munit.UnitDicId = 0;
                munit.UnitName = dtunit.Rows[0]["DoseUnitName"].ToString();
                munit.Pym = string.Empty;
                munit.Wbm = string.Empty;
                munit.Factor = Convert.ToDecimal(dtunit.Rows[0]["DoseConvertNum"]);
                list_unit.Add(munit);

                munit = new CardDataSourceUnit();
                munit.UnitDicId = 0;
                munit.UnitName = dtunit.Rows[0]["MiniUnitName"].ToString();
                munit.Pym = string.Empty;
                munit.Wbm = string.Empty;
                munit.Factor = 1;
                list_unit.Add(munit);
            }
            else
            {
                //总量              
               int statid= Convert.ToInt32(dtunit.Rows[0]["StatID"]);
                int resolveFlag = 0;
                if (statid == 100 || statid == 101)
                {
                    resolveFlag = 0;
                }
                else
                {
                    resolveFlag = Convert.ToInt32(dtunit.Rows[0]["ResolveFlag"]);
                }
                if (resolveFlag == 1)
                {
                    CardDataSourceUnit munit = new CardDataSourceUnit();
                    munit.UnitDicId = 0;
                    munit.UnitName = dtunit.Rows[0]["MiniUnitName"].ToString();
                    munit.Pym = string.Empty;
                    munit.Wbm = string.Empty;
                    munit.Factor = 1;
                    list_unit.Add(munit);

                    munit = new CardDataSourceUnit();
                    munit.UnitDicId = 0;
                    munit.UnitName = dtunit.Rows[0]["UnPickUnit"].ToString();
                    munit.Pym = string.Empty;
                    munit.Wbm = string.Empty;
                    munit.Factor = Convert.ToDecimal(dtunit.Rows[0]["MiniConvertNum"]);
                    list_unit.Add(munit);
                }
                else
                {
                    CardDataSourceUnit munit = new CardDataSourceUnit();
                    munit.UnitDicId = 0;
                    munit.UnitName = dtunit.Rows[0]["UnPickUnit"].ToString();
                    munit.Pym = string.Empty;
                    munit.Wbm = string.Empty;
                    munit.Factor = Convert.ToDecimal(dtunit.Rows[0]["MiniConvertNum"]);
                    list_unit.Add(munit);

                    munit = new CardDataSourceUnit();
                    munit.UnitDicId = 0;
                    munit.UnitName = dtunit.Rows[0]["UnPickUnit"].ToString();
                    munit.Pym = string.Empty;
                    munit.Wbm = string.Empty;
                    munit.Factor = Convert.ToDecimal(dtunit.Rows[0]["MiniConvertNum"]);
                    list_unit.Add(munit);
                }
            }

            return list_unit;
        }

        /// <summary>
        /// 获取用法
        /// </summary>
        /// <returns>用法数据</returns>
        public List<CardDataSourceUsage> GetUsage()
        {
            if (ltUsage == null || ltUsage.Count == 0)
            {
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetChannelData");
                List<Basic_Channel> list = retData.GetData<List<Basic_Channel>>(0);
                ltUsage = list;
            }

            List<CardDataSourceUsage> lists = new List<CardDataSourceUsage>();
            foreach (Basic_Channel model in ltUsage)
            {
                CardDataSourceUsage myUsage = new CardDataSourceUsage();
                myUsage.UsageName = model.ChannelName;
                myUsage.UsageId = model.ID;
                myUsage.Pym = model.PYCode;
                myUsage.Wbm = model.WBCode;
                lists.Add(myUsage);
            }

            return lists;
        }

        /// <summary>
        /// 获取处方数据
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方数据</returns>
        public List<Prescription> GetPrescriptionData(int patListId, int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
                request.AddData(presType);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetPrescriptionData", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            List<Prescription> list_Prescription = new List<Prescription>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prescription mPres = new Prescription();
                mPres.PresListId = Convert.ToInt32(dt.Rows[i]["PresDetailID"]);
                mPres.PresHeadId = Convert.ToInt32(dt.Rows[i]["PresHeadID"]);
                //mPres.OrderNo = i + 1;//行号
                mPres.Item_Id = Convert.ToInt32(dt.Rows[i]["ItemID"]);
                mPres.Item_Name = dt.Rows[i]["ItemName"].ToString();
                mPres.Item_Type = presType;//1西药 2中药 3项目材料
                mPres.StatItem_Code = dt.Rows[i]["StatID"].ToString();
                mPres.Sell_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Buy_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Item_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Standard = dt.Rows[i]["Spec"].ToString();
                mPres.Usage_Amount = Convert.ToDecimal(dt.Rows[i]["Dosage"]);//剂量
                mPres.Usage_Unit = dt.Rows[i]["DosageUnit"].ToString();//剂量单位
                mPres.Usage_Rate = Convert.ToDecimal(dt.Rows[i]["Factor"]);//剂量系数
                mPres.Dosage = Convert.ToInt32(dt.Rows[i]["DoseNum"]); //付数
                mPres.Usage_Id = Convert.ToInt32(dt.Rows[i]["ChannelID"]);
                mPres.Frequency_Id = Convert.ToInt32(dt.Rows[i]["FrequencyID"]);
                mPres.Days = Convert.ToInt32(dt.Rows[i]["Days"]);

                mPres.Amount = Convert.ToDecimal(dt.Rows[i]["ChargeAmount"].ToString());//发药数量
                mPres.Unit = dt.Rows[i]["ChargeUnit"].ToString();//发药单位

                mPres.Item_Amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresAmount"]));//开药数量
                mPres.Item_Unit = dt.Rows[i]["PresAmountUnit"].ToString();//开药单位
                mPres.Item_Rate = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresFactor"]));//系数

                mPres.Group_Id = Convert.ToInt32(dt.Rows[i]["GroupID"]);//分组组号
                mPres.SkinTest_Flag = Convert.ToInt32(dt.Rows[i]["IsAst"]);//皮试
                mPres.SelfDrug_Flag = Convert.ToInt32(dt.Rows[i]["IsTake"]);//自备
                mPres.IsReimbursement = Convert.ToInt32(dt.Rows[i]["IsReimbursement"]);//医保报销标识
                mPres.Entrust = dt.Rows[i]["Entrust"].ToString();//嘱托
                mPres.Additional = dt.Rows[i]["Additional"].ToString();//附加
                mPres.MedicareID = Convert.ToInt32(dt.Rows[i]["MedicareID"]); //医保类型ID
                mPres.FootNote = string.Empty;
                mPres.Tc_Flag = 0;//套餐

                mPres.PresNo = Convert.ToInt32(dt.Rows[i]["PresNO"]);//方号
                mPres.Dept_Id = Convert.ToInt32(dt.Rows[i]["ExecDeptID"]);//执行科室
                mPres.Dept_Name = dt.Rows[i]["ExecDeptName"].ToString();
                mPres.Pres_Dept = Convert.ToInt32(dt.Rows[i]["PresDeptID"]);
                mPres.Pres_DeptName = dt.Rows[i]["PresDeptName"].ToString();
                mPres.Pres_Doc = Convert.ToInt32(dt.Rows[i]["PresDoctorID"]);
                mPres.Pres_DocName = dt.Rows[i]["PresDoctorName"].ToString();
                if (Convert.ToInt32(dt.Rows[i]["IsCancel"]) == 1)
                {
                    mPres.Status = PresStatus.退费状态;
                }
                else if (Convert.ToInt32(dt.Rows[i]["IsCharged"]) == 1)
                {
                    mPres.Status = PresStatus.收费状态;
                }
                else
                {
                    mPres.Status = PresStatus.保存状态;
                }

                mPres.Usage_Name = dt.Rows[i]["ChannelName"].ToString();//用法名称
                mPres.Frequency_Name = dt.Rows[i]["FrequencyName"].ToString();//频次名称
                mPres.Frequency_Caption = dt.Rows[i]["ExecuteCode"].ToString();//频次名称
                mPres.IsFloat = Convert.ToInt32(dt.Rows[i]["FloatFlag"]) == 0 ? true : false;
                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(dt.Rows[i]["ExecuteCode"].ToString(), out execNum, out cycleDay);
                mPres.Frequency_ExecNum = execNum;//执行次数
                mPres.Frequency_CycleDay = cycleDay;//执行周期

                //精毒标志
                mPres.IsLunacyPosion = Convert.ToInt32(dt.Rows[i]["IsLunacyPosion"]);
                //本院执行次数
                mPres.ExecNum = Convert.ToInt32(dt.Rows[i]["ExecNum"]);
                mPres.DropSpec = dt.Rows[i]["DropSpec"].ToString();
                mPres.GroupSortNO = Convert.ToInt32(dt.Rows[i]["GroupSortNO"]);
                mPres.Memo = dt.Rows[i]["Memo"].ToString();
                mPres.CalculateItemMoney();

                list_Prescription.Add(mPres);
            }

            return list_Prescription;
        }

        /// <summary>
        /// 获取处方模板
        /// </summary>
        /// <param name="type">处方类型1西成2中草药3费用</param>
        /// <param name="tplId">处方模板Id</param>
        /// <returns>处方数据</returns>
        public List<Prescription> GetPresTemplate(int type, int tplId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tplId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetPresTemplate", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            List<Prescription> list_Prescription = new List<Prescription>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prescription mPres = new Prescription();
                mPres.PresListId = 0;
                mPres.PresHeadId = 0;
                mPres.PresNo = Convert.ToInt32(dt.Rows[i]["PresNO"]);//方号
                mPres.Group_Id = Convert.ToInt32(dt.Rows[i]["GroupID"]);//分组组号
                mPres.GroupSortNO = ConvertDataExtend.ToInt32(dt.Rows[i]["GroupSortNO"], 0);//组内序号
                mPres.Item_Id = Convert.ToInt32(dt.Rows[i]["ItemID"]);
                mPres.Item_Name = dt.Rows[i]["ItemName"].ToString();
                mPres.Item_Type = type;//1西药 2中药 3项目材料
                mPres.StatItem_Code = dt.Rows[i]["StatID"].ToString();//大项目Id
                mPres.Sell_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Buy_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Item_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Standard = dt.Rows[i]["Spec"].ToString();
                mPres.Usage_Amount = Convert.ToDecimal(dt.Rows[i]["Dosage"]);//剂量
                mPres.Usage_Unit = dt.Rows[i]["DosageUnit"].ToString();//剂量单位
                mPres.Usage_Rate = ConvertDataExtend.ToDecimal(dt.Rows[i]["Factor"], 1);//剂量系数
                mPres.Dosage = Convert.ToInt32(dt.Rows[i]["DoseNum"]);//付数
                mPres.Usage_Id = Convert.ToInt32(dt.Rows[i]["ChannelID"]);
                mPres.Frequency_Id = Convert.ToInt32(dt.Rows[i]["FrequencyID"]);
                mPres.Days = Convert.ToInt32(dt.Rows[i]["Days"]);
                int floatFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["FloatFlag"], 0);
                if (floatFlag == 0)
                {
                    mPres.IsFloat = true;
                }
                else
                {
                    mPres.IsFloat = false;
                }

                mPres.Amount = Convert.ToDecimal(dt.Rows[i]["ChargeAmount"].ToString());//发药数量
                mPres.Unit = dt.Rows[i]["ChargeUnit"].ToString();//发药单位

                mPres.Item_Amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresAmount"]));//开药数量
                mPres.Item_Unit = dt.Rows[i]["PresAmountUnit"].ToString();//开药单位
                mPres.Item_Rate = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresFactor"]));//系数

                mPres.SkinTest_Flag = ConvertDataExtend.ToInt32(dt.Rows[i]["IsAst"], 0);//皮试
                mPres.SelfDrug_Flag = 0;//自备

                mPres.Entrust = dt.Rows[i]["Entrust"].ToString();//嘱托

                mPres.FootNote = string.Empty;
                mPres.Tc_Flag = 0;//套餐

                mPres.Usage_Name = dt.Rows[i]["ChannelName"].ToString();//用法名称
                mPres.Frequency_Name = dt.Rows[i]["FrequencyName"].ToString();//频次名称
                mPres.Frequency_Caption = dt.Rows[i]["ExecuteCode"].ToString();//频次表达式

                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(dt.Rows[i]["ExecuteCode"].ToString(), out execNum, out cycleDay);
                mPres.Frequency_ExecNum = execNum;//执行次数
                mPres.Frequency_CycleDay = cycleDay;//执行周期
                //精毒标志
                mPres.IsLunacyPosion = Convert.ToInt32(dt.Rows[i]["IsLunacyPosion"]);
                //本院执行次数
                mPres.ExecNum = 0;
                mPres.DropSpec = string.Empty;
                mPres.Dept_Id = Convert.ToInt32(dt.Rows[i]["ExecDeptID"]);//执行科室
                mPres.Dept_Name = dt.Rows[i]["ExecDeptName"].ToString();
                int newItemRate = mPres.Item_Rate;
                string newUnitName = GetTmpUnit(mPres.Item_Id, 1, mPres.Item_Unit, mPres.Item_Rate, out newItemRate);
                if (newUnitName != mPres.Item_Unit)
                {
                    mPres.Item_Unit = newUnitName;
                    mPres.Item_Rate = newItemRate;
                }

                mPres.CalculateAmount(null);

                list_Prescription.Add(mPres);
            }

            return list_Prescription;
        }

        /// <summary>
        /// 获取处方模板行
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="tpldetailIds">处方明细ID数组</param>
        /// <returns>处方模板记录</returns>
        public List<Prescription> GetPresTemplateRow(int type, int[] tpldetailIds)
        {
            if (tpldetailIds.Length == 0)
            {
                return new List<Prescription>();
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tpldetailIds);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetPresTemplateRow", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            List<Prescription> list_Prescription = new List<Prescription>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prescription mPres = new Prescription();

                mPres.PresListId = 0;
                mPres.PresHeadId = 0;
                mPres.PresNo = Convert.ToInt32(dt.Rows[i]["PresNO"]);//方号
                mPres.Group_Id = Convert.ToInt32(dt.Rows[i]["GroupID"]);//分组组号
                mPres.GroupSortNO = ConvertDataExtend.ToInt32(dt.Rows[i]["GroupSortNO"], 0);//组内序号
                mPres.Item_Id = Convert.ToInt32(dt.Rows[i]["ItemID"]);
                mPres.Item_Name = dt.Rows[i]["ItemName"].ToString();
                mPres.Item_Type = type;//1西药 2中药 3项目材料
                mPres.StatItem_Code = dt.Rows[i]["StatID"].ToString();//大项目Id
                mPres.Sell_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Buy_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Item_Price = ConvertDataExtend.ToDecimal(dt.Rows[i]["Price"], 0);
                mPres.Standard = dt.Rows[i]["Spec"].ToString();
                mPres.Usage_Amount = Convert.ToDecimal(dt.Rows[i]["Dosage"]);//剂量
                mPres.Usage_Unit = dt.Rows[i]["DosageUnit"].ToString();//剂量单位
                mPres.Usage_Rate = ConvertDataExtend.ToDecimal(dt.Rows[i]["Factor"], 1);//剂量系数
                mPres.Dosage = Convert.ToInt32(dt.Rows[i]["DoseNum"]);//付数
                mPres.Usage_Id = Convert.ToInt32(dt.Rows[i]["ChannelID"]);
                mPres.Frequency_Id = Convert.ToInt32(dt.Rows[i]["FrequencyID"]);
                mPres.Days = Convert.ToInt32(dt.Rows[i]["Days"]);

                mPres.Amount = Convert.ToDecimal(dt.Rows[i]["ChargeAmount"].ToString());//发药数量
                mPres.Unit = dt.Rows[i]["ChargeUnit"].ToString();//发药单位

                mPres.Item_Amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresAmount"]));//开药数量
                mPres.Item_Unit = dt.Rows[i]["PresAmountUnit"].ToString();//开药单位
                mPres.Item_Rate = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresFactor"]));//系数

                int floatFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["FloatFlag"], 0);
                if (floatFlag == 0)
                {
                    mPres.IsFloat = true;
                }
                else
                {
                    mPres.IsFloat = false;
                }

                mPres.SkinTest_Flag = ConvertDataExtend.ToInt32(dt.Rows[i]["IsAst"], 0);//皮试
                mPres.SelfDrug_Flag = 0;//自备

                mPres.Entrust = dt.Rows[i]["Entrust"].ToString();//嘱托

                mPres.FootNote = string.Empty;
                mPres.Tc_Flag = 0;//套餐

                mPres.Usage_Name = dt.Rows[i]["ChannelName"].ToString();//用法名称
                mPres.Frequency_Name = dt.Rows[i]["FrequencyName"].ToString();//频次名称
                mPres.Frequency_Caption = dt.Rows[i]["ExecuteCode"].ToString();//频次表达式

                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(dt.Rows[i]["ExecuteCode"].ToString(), out execNum, out cycleDay);
                mPres.Frequency_ExecNum = execNum;//执行次数
                mPres.Frequency_CycleDay = cycleDay;//执行周期
                //精毒标志
                mPres.IsLunacyPosion = Convert.ToInt32(dt.Rows[i]["IsLunacyPosion"]);
                //本院执行次数
                mPres.ExecNum = 0;
                mPres.DropSpec = string.Empty;
                mPres.Dept_Id = Convert.ToInt32(dt.Rows[i]["ExecDeptID"]);//执行科室
                mPres.Dept_Name = dt.Rows[i]["ExecDeptName"].ToString();
                int newItemRate = mPres.Item_Rate;
                string newUnitName = GetTmpUnit(mPres.Item_Id, 1, mPres.Item_Unit, mPres.Item_Rate, out newItemRate);
                if (newUnitName != mPres.Item_Unit)
                {
                    mPres.Item_Unit = newUnitName;
                    mPres.Item_Rate = newItemRate;
                }

                mPres.CalculateAmount(null);

                list_Prescription.Add(mPres);
            }

            return list_Prescription;
        }

        /// <summary>
        /// 判断是否收费
        /// </summary>
        /// <param name="list">处方列表</param>
        /// <returns>true是收费</returns>
        public bool IsCostPres(List<Prescription> list)
        {
            if (list.Count == 0)
            {
                return false;
            }

            string ids = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (ids == null)
                {
                    ids = list[i].PresListId.ToString();
                }
                else
                {
                    ids += "," + list[i].PresListId.ToString();
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ids);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "IsCostPres", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 加载处方模板头信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方模板头信息</returns>
        public List<EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead> LoadTemplateHead(int intLevel, int presType)
        {
            List<EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead> headList = new List<EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead>();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(presType);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "FeeTemplateController", "GetPresTemplate", requestAction);
            List<HIS_Entity.ClinicManage.OPD_PresMouldHead> tempList = retData.GetData<List<HIS_Entity.ClinicManage.OPD_PresMouldHead>>(0);
            foreach (HIS_Entity.ClinicManage.OPD_PresMouldHead presHead in tempList)
            {
                EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead newPreHead = new EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead();
                newPreHead.CreateDate = presHead.CreateDate;
                newPreHead.CreateDeptID = presHead.CreateDeptID;
                newPreHead.CreateEmpID = presHead.CreateEmpID;
                newPreHead.DelFlag = presHead.DelFlag;
                newPreHead.ModuldName = presHead.ModuldName;
                newPreHead.ModulLevel = presHead.ModulLevel;
                newPreHead.MouldType = presHead.MouldType;
                newPreHead.PID = presHead.PID;
                newPreHead.PresMouldHeadID = presHead.PresMouldHeadID;
                newPreHead.PresType = presHead.PresType;
                headList.Add(newPreHead);
            }

            EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead head = new EfwControls.HISControl.Prescription.Controls.Entity.OPD_PresMouldHead();

            head.ModuldName = "全部模板";
            head.ModulLevel = intLevel;
            head.MouldType = 0;
            head.PresMouldHeadID = 0;
            head.PID = 99999;
            head.PresType = presType;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;

            headList.Add(head);
            return headList;
        }

        /// <summary>
        /// 判断药品库存单条
        /// </summary>
        /// <param name="pres">处方实体</param>
        /// <returns>true有库存</returns>
        public bool IsDrugStore(Prescription pres)
        {
            if (pres.Item_Id > 0 && pres.IsDrug == true)
            {
                int deptId = pres.Dept_Id;
                int itemId = pres.Item_Id;
                decimal qty = pres.Amount;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(deptId);
                    request.AddData(itemId);
                    request.AddData(qty);
                });
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "IsDrugStore", requestAction);
                bool bRtn = retData.GetData<bool>(0);
                return bRtn;
            }

            return true;
        }

        /// <summary>
        /// 判断药品库存，多条
        /// </summary>
        /// <param name="list">药品列表</param>
        /// <param name="errlist">错误列表</param>
        /// <returns>true有库存</returns>
        public bool IsDrugStore(List<Prescription> list, List<Prescription> errlist)
        {
            list = list.FindAll(x => x.IsDrug == true);
            if (list.Count == 0)
            {
                return true;
            }

            string ids = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (ids == null)
                {
                    ids = list[i].Item_Id.ToString();
                }
                else
                {
                    ids += "," + list[i].Item_Id.ToString();
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ids);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "IsDrugAllStore", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            for (int i = 0; i < list.Count; i++)
            {
                DataRow[] drs = dt.Select("ItemID=" + list[i].Item_Id + " and ExecDeptId=" + list[i].Dept_Id);
                if (drs.Length == 0 || ConvertDataExtend.ToDecimal(drs[0]["StoreAmount"], 0) < list[i].Amount)
                {
                    errlist.Add(list[i]);
                }
            }

            if (errlist.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得导入模板单位
        /// </summary>
        /// <param name="stockId">药品id</param>
        /// <param name="type">类型</param>
        /// <param name="oldUnitName">原单位名称</param>
        /// <param name="oldItemRate">原转换系数</param>
        /// <param name="newItemRate">新转换系数</param>
        /// <returns>单位名称</returns>
        public string GetTmpUnit(int stockId, int type, string oldUnitName, int oldItemRate, out int newItemRate)
        {
            string newUnitName = oldUnitName;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stockId);
                request.AddData(SelectedDrugRoomID);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugItem", requestAction);
            DataTable dtunit = retData.GetData<DataTable>(0);
            if (dtunit.Rows.Count <= 0)
            {
                newItemRate = 1;
                return "-1";
            }
            int statid = Convert.ToInt32(dtunit.Rows[0]["StatID"]);
            int resolveFlag = 0;
            if (statid == 100 || statid == 101)
            {
                resolveFlag = 0;
            }
            else
            {
                resolveFlag = Convert.ToInt32(dtunit.Rows[0]["ResolveFlag"]);
            }
            if (resolveFlag != 1)
            {
                if (oldUnitName != dtunit.Rows[0]["UnPickUnit"].ToString())
                {
                    newUnitName = dtunit.Rows[0]["UnPickUnit"].ToString();
                    newItemRate = Convert.ToInt32(dtunit.Rows[0]["MiniConvertNum"]);
                }
                else
                {
                    newItemRate = oldItemRate;
                }
            }
            else
            {
                newItemRate = oldItemRate;
            }

            return newUnitName;
        }

        /// <summary>
        /// 加载处方模板明细
        /// </summary>
        /// <param name="tplId">模板Id</param>
        /// <returns>模板明细记录</returns>
        public DataTable LoadTemplateDetail(int tplId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载处方模板
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="doctorId">医生Id</param>
        /// <param name="mealCls">显示列字段集合</param>
        /// <returns>模板明细数据</returns>
        public DataTable LoadTemplateList(int deptId, int doctorId, int mealCls)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存处方数据
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="list">处方列表</param>
        /// <param name="presType">处方类型</param>
        /// <returns>true成功</returns>
        public bool SavePrescriptionData(int patListId, List<Prescription> list, int presType)
        {
            List<OPD_PresDetail> detailList = new List<OPD_PresDetail>();
            //构造处方明细表
            foreach (Prescription pres in list)
            {
                OPD_PresDetail detailModel = new OPD_PresDetail();
                detailModel.PresDetailID = pres.PresListId;//?
                detailModel.PresHeadID = pres.PresHeadId;//?
                detailModel.PresNO = pres.PresNo;
                detailModel.GroupID = pres.Group_Id;
                detailModel.GroupSortNO = pres.GroupSortNO;//?
                detailModel.ItemID = pres.Item_Id;
                detailModel.ItemName = pres.Item_Name;
                detailModel.StatID = ConvertDataExtend.ToInt32(pres.StatItem_Code, 0);//?
                detailModel.Spec = pres.Standard;
                detailModel.Dosage = pres.Usage_Amount;
                detailModel.DosageUnit = pres.Usage_Unit;
                detailModel.Factor = pres.Usage_Rate;
                detailModel.ChannelID = pres.Usage_Id;
                detailModel.FrequencyID = pres.Frequency_Id;
                detailModel.Entrust = pres.Entrust;
                detailModel.DoseNum = pres.Dosage;
                if (presType == 2)
                {
                    detailModel.ChargeAmount = pres.Amount / pres.Dosage;
                }
                else
                {
                    detailModel.ChargeAmount = pres.Amount;
                }

                detailModel.ChargeUnit = pres.Unit;
                detailModel.Price = pres.Sell_Price;
                detailModel.Days = pres.Days;
                detailModel.DropSpec = string.Empty;//?
                detailModel.IsAst = pres.SkinTest_Flag;

                detailModel.IsTake = pres.SelfDrug_Flag;

                detailModel.PresAmount = pres.Item_Amount;
                detailModel.PresAmountUnit = pres.Item_Unit;
                detailModel.PresFactor = pres.Item_Rate;

                //西药注射次数
                if (presType == 1)
                {
                    if (pres.ExecNum > 0)
                    {
                        detailModel.ExecNum = pres.ExecNum;
                    }
                    else
                    {
                        detailModel.ExecNum = 0;
                    }
                }
                else
                {
                    detailModel.ExecNum = pres.ExecNum;
                }

                detailModel.Memo = pres.Memo;
                detailModel.PresDoctorID = pres.Pres_Doc;
                detailModel.PresDeptID = pres.Pres_Dept;
                if (pres.Dept_Id == 0)
                {
                    detailModel.ExecDeptID = pres.Pres_Dept;
                }
                else
                {
                    detailModel.ExecDeptID = pres.Dept_Id;
                }

                detailModel.PresDate = DateTime.Now;
                detailModel.IsEmergency = 0;
                detailModel.IsLunacyPosion = pres.IsLunacyPosion;
                detailModel.IsReimbursement = pres.IsReimbursement;

                detailList.Add(detailModel);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
                request.AddData(SelectedMemberID);
                request.AddData(presType);
                request.AddData(detailList);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "SavePrescriptionData", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 更改处方号和组号
        /// </summary>
        /// <param name="data">处方列表</param>
        /// <returns>true成功</returns>
        public bool UpdatePresNoAndGroupId(List<Prescription> data)
        {
            List<OPD_PresDetail> list = new List<OPD_PresDetail>();
            for (int i = 0; i < data.Count; i++)
            {
                OPD_PresDetail model = new OPD_PresDetail();
                model.PresDetailID = data[i].PresListId;
                model.PresNO = data[i].PresNo;
                model.GroupID = data[i].Group_Id;
                list.Add(model);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(list);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "UpdatePresNoAndGroupId", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 更新自备药品状态
        /// </summary>
        /// <param name="presListId">处方明细Id</param>
        /// <param name="flag">自备标志</param>
        /// <returns>true成功</returns>
        public bool UpdatePresSelfDrugFlag(int presListId, int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presListId);
                request.AddData(flag);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "UpdatePresSelfDrugFlag", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 更新医保报销状态
        /// </summary>
        /// <param name="data">处方明细列表</param>
        /// <param name="flag">报销标识0不报销，1报销</param>
        /// <returns>true成功</returns>
        public bool UpdatePresReimbursementFlag(List<Prescription> data, int flag)
        {
            List<OPD_PresDetail> list = new List<OPD_PresDetail>();
            for (int i = 0; i < data.Count; i++)
            {
                OPD_PresDetail model = new OPD_PresDetail();
                model.PresDetailID = data[i].PresListId;
                model.PresNO = data[i].PresNo;
                model.GroupID = data[i].Group_Id;
                list.Add(model);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(list);
                request.AddData(flag);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "UpdatePresReimbursementFlag", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 获取皮试药品ID
        /// </summary>
        /// <returns>皮试药品id</returns>
        public int GetActDrugID()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetActDrugId");
            string drugid = retdata.GetData<string>(0);
            if (string.IsNullOrEmpty(drugid))
            {
                return 0;
            }

            return Convert.ToInt32(drugid);
        }

        /// <summary>
        /// 更改注射次数
        /// </summary>
        /// <param name="presListId">病人Id</param>
        /// <param name="menuText">注射次数说明</param>
        /// <param name="execTimes">执行次数</param>
        /// <returns>true成功</returns>
        public bool UpdatePresInjectTimes(int presListId, string menuText, int execTimes)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presListId);
                request.AddData(menuText);
                request.AddData(execTimes);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "UpdatePresInjectTimes", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 处方退费
        /// </summary>
        /// <param name="presHeadID">处方头id</param>
        /// <param name="presNO">处方号</param>
        public void RefundPresFee(int presHeadID, int presNO)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(presHeadID);
                    request.AddData(presNO);
                });
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "RefundController", "GetInvoiceNOByPres", requestAction);
                string invoiceNO = retData.GetData<string>(0);
                InvokeController("OPProject.UI", "RefundController", "ShowRefundMessage", invoiceNO);//调用退费界面
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
    }
}
