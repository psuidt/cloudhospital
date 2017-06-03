using System;
using System.Collections.Generic;
using System.Data;
using EfwControls.Common;
using EfwControls.HISControl.PrescriptionTmp.Controls;
using EfwControls.HISControl.PrescriptionTmp.Controls.Entity;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 处方模板控件数据库接口实现类
    /// </summary>
    public class PresTemplateDbHelper : WcfClientController, IPrescripttionDbHelper
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
        /// 选中药房ID
        /// </summary>
        public int SelectedDrugRoomID { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public int SelectedMemberID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="selectedDrugRoomID">选择的药房id</param>
        public PresTemplateDbHelper(int selectedDrugRoomID)
        {
            SelectedDrugRoomID = selectedDrugRoomID;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PresTemplateDbHelper()
        {
        }

        /// <summary>
        /// 处方另存为模板
        /// </summary>
        /// <param name="level">权限级别</param>
        /// <param name="mName">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="doctorId">医生id</param>
        /// <param name="data">处方数据</param>
        public void AsSavePresTemplate(int level, string mName, int presType, int deptId, int doctorId, List<Prescription> data)
        {
            throw new NotImplementedException();
        }

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
        /// 获取药品信息
        /// </summary>
        /// <param name="itemId">药品Id</param>
        /// <returns>药品实体模型</returns>
        public CardDataSourceDrugItem  GetDrugItem(int itemId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(itemId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugItemTpl", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            List<CardDataSourceDrugItem> list_DrugItem = new List<CardDataSourceDrugItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CardDataSourceDrugItem mDrugItem = new CardDataSourceDrugItem();
                mDrugItem.ItemId = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemID"], 0);
                mDrugItem.ItemName = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.ItemName_Print = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.Standard = dt.Rows[i]["Standard"].ToString();

                mDrugItem.Scale = string.Empty;//补偿比例
                mDrugItem.StoreNum = ConvertDataExtend.ToDecimal(dt.Rows[i]["StoreAmount"], 0);//库存量
                mDrugItem.UnPickUnit = dt.Rows[i]["UnPickUnit"].ToString();//包装单位(销售单位)
                mDrugItem.SellPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["UnitPrice"], 0);//销售价格?
                mDrugItem.BuyPrice = ConvertDataExtend.ToDecimal(dt.Rows[i]["InPrice"], 0);//进价
                mDrugItem.ExecDeptName = dt.Rows[i]["ExecDeptName"].ToString();//执行科室?
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
                //mDrugItem.UnPickUnitId = dt.Rows[i][""].ToString();//包装单位ID
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
                list_DrugItem.Add(mDrugItem);
            }

            return list_DrugItem.Count > 0 ? list_DrugItem[0] : null;
        }

        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <param name="itemClass">项目类型</param>
        /// <param name="statId">统计大项目id</param>
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
        /// 获取用法信息
        /// </summary>
        /// <returns>用法列表实体模型</returns>
        public List<CardDataSourceUsage>  GetUsage()
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
        /// 频次
        /// </summary>
        /// <returns>频次列表实体</returns>
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
        /// 嘱托
        /// </summary>
        /// <returns>嘱托列表实体</returns>
        public List<CardDataSourceEntrust>  GetEntrust()
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
        /// 单位
        /// </summary>
        /// <param name="stockId">药品id</param>
        /// <param name="type">0剂型单位1总量单位</param>
        /// <returns>单位列表</returns>
        public List<CardDataSourceUnit> GetUnit(int stockId, int type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stockId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugItemTpl", requestAction);
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
                int resolveFlag = Convert.ToInt32(dtunit.Rows[0]["ResolveFlag"]);
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
        #region 不使用的接口

        /// <summary>
        /// 自备
        /// </summary>
        /// <param name="presListId">处方id</param>
        /// <param name="flag">自备标识</param>
        /// <returns>true成功</returns>
        bool IPrescripttionDbHelper.UpdatePresSelfDrugFlag(int presListId, int flag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处方另存为模板
        /// </summary>
        /// <param name="level">权限级别</param>
        /// <param name="mName">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="doctorId">医生id</param>
        /// <param name="data">处方数据</param>
        void IPrescripttionDbHelper.AsSavePresTemplate(int level, string mName, int presType, int deptId, int doctorId, List<Prescription> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 是否收费
        /// </summary>
        /// <param name="list">处方数据</param>
        /// <returns>true是收费</returns>
        bool IPrescripttionDbHelper.IsCostPres(List<Prescription> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载模板数据
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="doctorId">医生id</param>
        /// <param name="mealCls">显示列字段集合</param>
        /// <returns>模板数据</returns>
        public DataTable  LoadTemplateList(int deptId, int doctorId, int mealCls)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载模板明细
        /// </summary>
        /// <param name="tplId">处方明细id</param>
        /// <returns>处方明细数据</returns>
        public DataTable  LoadTemplateDetail(int tplId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取处方模板
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="tplId">处方明细id</param>
        /// <returns>处方明细数据</returns>
        public List<Prescription>  GetPresTemplate(int type, int tplId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取处方模板数据行
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="tpldetailIds">处方明细id数组</param>
        /// <returns>处方列表</returns>
        List<Prescription> IPrescripttionDbHelper.GetPresTemplateRow(int type, int[] tpldetailIds)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 获取处方模板信息
        /// </summary>
        /// <param name="presHeadID">模板ID</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方模板数据</returns>
        public List<Prescription> GetPrescriptionData(int presHeadID, int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presHeadID);
                request.AddData(presType);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresTemplateController", "GetPresTemplateData", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);

            //int _orderNo = 1;//行号
            List<Prescription> list_Prescription = new List<Prescription>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prescription mPres = new Prescription();
                mPres.PresListId = Convert.ToInt32(dt.Rows[i]["PresMouldDetailID"]);
                mPres.PresHeadId = Convert.ToInt32(dt.Rows[i]["PresMouldHeadID"]);
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
 
                mPres.Entrust = dt.Rows[i]["Entrust"].ToString();//嘱托

                mPres.FootNote = string.Empty;
                mPres.Tc_Flag = 0;//套餐

                mPres.PresNo = Convert.ToInt32(dt.Rows[i]["PresNO"]);//方号
                mPres.Dept_Id = Convert.ToInt32(dt.Rows[i]["ExecDeptID"]);//执行科室
                mPres.Dept_Name = dt.Rows[i]["ExecDeptName"].ToString();
                mPres.Pres_Dept = Convert.ToInt32(dt.Rows[i]["PresDeptID"]);
                mPres.Pres_DeptName = dt.Rows[i]["PresDeptName"].ToString();
                mPres.Pres_Doc = Convert.ToInt32(dt.Rows[i]["PresDoctorID"]);
                mPres.Pres_DocName = dt.Rows[i]["PresDoctorName"].ToString();

                mPres.Status = PresStatus.保存状态;  //? 

                mPres.Usage_Name = dt.Rows[i]["ChannelName"].ToString();//用法名称
                mPres.Frequency_Name = dt.Rows[i]["FrequencyName"].ToString();//频次名称
                mPres.Frequency_Caption = dt.Rows[i]["ExecuteCode"].ToString();//频次名称

                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(dt.Rows[i]["ExecuteCode"].ToString(), out execNum, out cycleDay);
                mPres.Frequency_ExecNum = execNum;//执行次数
                mPres.Frequency_CycleDay = cycleDay;//执行周期
 
                mPres.GroupSortNO = Convert.ToInt32(dt.Rows[i]["GroupSortNO"]);
 
                mPres.CalculateItemMoney();

                list_Prescription.Add(mPres);
            }

            return list_Prescription;
        }
        
        /// <summary>
        /// 保存处方信息
        /// </summary>
        /// <param name="patListId">处方头id</param>
        /// <param name="list">处方数据</param>
        /// <param name="presType">处方类型</param>
        /// <returns>true成功</returns>
        public bool SavePrescriptionData(int patListId, List<Prescription> list, int presType)
        {
            //List<CardDataSourceFrequency> FrequencyList = GetFrequency();
            List<OPD_PresMouldDetail> detailList = new List<OPD_PresMouldDetail>();
            //构造处方明细表
            foreach (Prescription pres in list)
            {
                OPD_PresMouldDetail detailModel = new OPD_PresMouldDetail();
                detailModel.PresMouldDetailID = pres.PresListId;//?
                detailModel.PresMouldHeadID = patListId;//?  //处方头ID
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
                detailModel.ChargeAmount = pres.Amount;
                detailModel.ChargeUnit = pres.Unit;
                detailModel.Price = pres.Sell_Price;
                detailModel.Days = pres.Days;

                detailModel.PresAmount = pres.Item_Amount;
                detailModel.PresAmountUnit = pres.Item_Unit;
                detailModel.PresFactor = pres.Item_Rate;
                 
                detailModel.ExecDeptID = pres.Dept_Id; 

                detailList.Add(detailModel);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(detailList);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresTemplateController", "SavePresTemplateData", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 删除处方的一条信息
        /// </summary>
        /// <param name="presListId">模板明细ID</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int presListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presListId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresTemplateController", "DeletePrescriptionData", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 删除处方整组信息
        /// </summary>
        /// <param name="patListId">模板明细ID</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        public bool  DeletePrescriptionData(int patListId, int presType, int presNo)
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
        /// 更改处方号和组号
        /// </summary>
        /// <param name="data">处方列表</param>
        /// <returns>true成功</returns>
        public bool  UpdatePresNoAndGroupId(List<Prescription> data)
        {
            List<OPD_PresMouldDetail> list = new List<OPD_PresMouldDetail>();
            for (int i = 0; i < data.Count; i++)
            {
                OPD_PresMouldDetail model = new OPD_PresMouldDetail();
                model.PresMouldDetailID = data[i].PresListId;
                model.PresNO = data[i].PresNo;
                model.GroupID = data[i].Group_Id;
                list.Add(model);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(list);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresTemplateController", "UpdatePresNoAndGroupId", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
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
        /// 获取药品信息
        /// </summary>
        /// <param name="type">处方类型</param>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>药品列表</returns>
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
            dv.RowFilter = "Pym like '%" + filter + "%' or Wbm like '%" + filter + "%' or ItemName like '%" + filter + "%'";
            dt = dv.ToTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CardDataSourceDrugItem mDrugItem = new CardDataSourceDrugItem();
                mDrugItem.ItemId = ConvertDataExtend.ToInt32(dt.Rows[i]["ItemID"], 0);
                mDrugItem.ItemName = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.ItemName_Print = dt.Rows[i]["ItemName"].ToString();
                mDrugItem.Standard = dt.Rows[i]["Standard"].ToString();
                mDrugItem.PresFactor = ConvertDataExtend.ToInt32(dt.Rows[i]["MiniConvertNum"], 1);
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
                //mDrugItem.UnPickUnitId = dt.Rows[i][""].ToString();//包装单位ID
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
                mDrugItem.ResolveFlag = ConvertDataExtend.ToInt32(dt.Rows[i]["ResolveFlag"], 0);
                list_DrugItem.Add(mDrugItem);
            }

            return list_DrugItem;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public void CloseWindow()
        {
            //InvokeController("Close", this);
        }

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <param name="pres">处方实体数据</param>
        /// <returns>true存在</returns>
        public bool IsDrugStore(Prescription pres)
        {
            //throw new NotImplementedException();
            return true;
        }

        /// <summary>
        /// 批量判断库存
        /// </summary>
        /// <param name="list">处方列表数据</param>
        /// <param name="errlist">错误列表</param>
        /// <returns>true有库存false库存不足</returns>
        public bool IsDrugStore(List<Prescription> list, List<Prescription> errlist)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}
