using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药品字典控制器
    /// </summary>
    [WCFController]
    public class DrugDicController : WcfServerController
    {
        /// <summary>
        /// 中心典各个列表填充
        /// </summary>
        /// <returns>中心典各个列表数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAllCenterTable()
        {
            //药理
            DataTable dtPharm = NewObject<PharmacyMgr>().GetAllPharmacy();
            responseData.AddData(dtPharm);

            //药品类型 子类型
            DataTable dtDrugTypeAndCType = NewObject<IDGDao>().GetDrugTypeAndChild();
            responseData.AddData(dtDrugTypeAndCType);

            #region Card控件
            //药品类型
            DataTable dtDrugType = NewDao<IDGDao>().GetTypeDic();
            responseData.AddData(dtDrugType);

            //药品子类型
            DataTable dtDrugCType = NewObject<DrugTypeMgr>().GetChildDrugType(null);
            responseData.AddData(dtDrugCType);

            //统计大项目
            responseData.AddData(NewObject<BasicDataManagement>().GetStatItem(false));

            //通用单位
            responseData.AddData(NewObject<BasicDataManagement>().GetUnit());

            //抗生素
            responseData.AddData(NewObject<DG_AntDic>().gettable(" DelFlag=0 "));

            //药剂
            DataTable dt = NewObject<IDGDao>().GetDosageUserOr(null);
            responseData.AddData(dt);
            #endregion

            return responseData;
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <returns>类型名称</returns>
        [WCFMethod]
        public ServiceResponseData GetTypeName()
        {
            var cTypeId = requestData.GetData<string>(0);
            string typeName = NewObject<IDGDao>().GetTypeName(cTypeId);
            responseData.AddData(typeName);
            return responseData;
        }

        /// <summary>
        /// 获取中心药品字典数据
        /// </summary>
        /// <returns>药品字典数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDic()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            var oW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(1);
            DataTable dt = NewObject<DrugSpecDicMgr>().GetDurgDic(andW, oW);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取本院药品字典数据
        /// </summary>
        /// <returns>本院药品字典数据</returns>
        [WCFMethod]
        public ServiceResponseData GetHospDrugDic()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            var oW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(1);
            int createWordId = requestData.GetData<int>(2);
            DataTable dt = NewObject<DrugSpecDicMgr>().GetHospDrugDic(andW, oW, createWordId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取统计大项目数据
        /// </summary>
        /// <returns>统计大项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStatItem()
        {
            responseData.AddData(NewObject<BasicDataManagement>().GetStatItem(false));
            return responseData;
        }

        /// <summary>
        /// 获取通用单位
        /// </summary>
        /// <returns>通用单位</returns>
        [WCFMethod]
        public ServiceResponseData GetComonUnit()
        {
            responseData.AddData(NewObject<BasicDataManagement>().GetUnit());
            return responseData;
        }

        /// <summary>
        /// 获取抗生素
        /// </summary>
        /// <returns>抗生素</returns>
        [WCFMethod]
        public ServiceResponseData GetAnt()
        {
            responseData.AddData(NewObject<DG_AntDic>().gettable(" DelFlag=0 "));
            return responseData;
        }

        /// <summary>
        /// 保存字典
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveDrugDic()
        {
            DGBillResult result = new DGBillResult();
            var p = requestData.GetData<DG_CenterSpecDic>(0);
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("ChemName", p.ChemName, SqlOperator.Equal));
            andWhere.Add(Tuple.Create("Spec", p.Spec, SqlOperator.Equal));
            var t = NewObject<IDGDao>().GetDataTable<DG_CenterSpecDic>(andWhere, null);
            if (p.CenteDrugID != 0)
            {
                //修改药品
                if (CheckDrugStore(p))
                {
                    if (t != null && t.Rows.Count > 1)
                    {
                        result.Result = 1;
                        result.ErrMsg = "同名 同规格的药品已经存在";
                        responseData.AddData(result);
                        return responseData;
                    }
                }
                else
                {
                    result.Result = 1;
                    result.ErrMsg = "已经有库存的药品 不能更改药品关键属性 包装单位,包装数量,最小单位,含量系数,含量单位,是否停用";
                    responseData.AddData(result);
                    return responseData;
                }
            }
            else
            {
                if (t != null && t.Rows.Count > 0)
                {
                    result.Result = 1;
                    result.ErrMsg = "同名 同规格的药品已经存在";
                    responseData.AddData(result);
                    return responseData;
                }
            }

            SetWorkId(oleDb.WorkId);
            this.BindDb(p);
            NewObject<DrugSpecDicMgr>().SaveDrugDic(p);
            result.Result = 0;
            result.ErrMsg = p.CenteDrugID.ToString();
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 中心药典的药品是否有库存
        /// </summary>
        /// <param name="dic">药品中心字典对象</param>
        /// <returns>是否有库存</returns>
        private bool CheckDrugStore(DG_CenterSpecDic dic)
        {
            var t = oleDb.Query<DG_CenterSpecDic>(string.Format("select * from DG_CenterSpecDic where CenteDrugID={0}", dic.CenteDrugID), null).FirstOrDefault();
            if (t.PackUnitID == dic.PackUnitID &&
                t.DoseUnitID == dic.DoseUnitID &&
                t.PackAmount == dic.PackAmount &&
                t.MiniUnitID == dic.MiniUnitID
                && t.DoseAmount == dic.DoseAmount
                && t.IsStop == dic.IsStop
                )
            {
                //没有更改药品的系数属性 
                return true;
            }
            else
            {
                return NewObject<IDGDao>().StoreExsitDrug(dic.CenteDrugID);//中心药典没有库存返回true；
            }
        }

        /// <summary>
        /// 本院字典所有要绑定的数据
        /// </summary>
        /// <returns>本院字典数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAllHisDic()
        {
            DataTable tableProduct = NewObject<DrugProductMgr>().GetProducts(null);
            DataTable tableMedicare = NewObject<IPublicManageDao>().GetMedicareDic();
            responseData.AddData(tableProduct);
            responseData.AddData(tableMedicare);
            return responseData;
        }

        /// <summary>
        /// 获取本院字典数据
        /// </summary>
        /// <returns>本院字典数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadHisDic()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            DataTable dt = NewObject<DrugSpecDicMgr>().GetDrugDicHisDataTable(andW, null);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存本院字典数据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveHisDic()
        {
            var p = requestData.GetData<DG_HospMakerDic>(0);
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("CenteDrugID", p.CenteDrugID.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("ProductID", p.ProductID.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("IsStop","0", SqlOperator.Equal));
            var t = NewObject<IDGDao>().GetDataTable<DG_HospMakerDic>(andWhere, null);
            if (p.DrugID != 0)
            {
                //修改
                if (t != null && t.Rows.Count > 1)
                {
                    responseData.AddData(false);
                    responseData.AddData("错误:存在相同记录");
                    return responseData;
                }
            }
            else
            {
                if (t != null && t.Rows.Count > 0)
                {
                    responseData.AddData(false);
                    responseData.AddData("错误:存在相同记录");
                    return responseData;
                }
            }

            this.BindDb(p);
            NewObject<DrugSpecDicMgr>().SaveHospDic(p);
            responseData.AddData(true);
            return responseData;
        }
    }
}
