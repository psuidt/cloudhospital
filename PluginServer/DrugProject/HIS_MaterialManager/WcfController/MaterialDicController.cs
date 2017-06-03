using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_PublicManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资字典维护控制器
    /// </summary>
    [WCFController]
    public class MaterialDicController : WcfServerController
    {
        /// <summary>
        /// 获取物资类型
        /// </summary>
        /// <returns>物资类型</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialType()
        {
            var type = requestData.GetData<string>(0);
            var pruduct = requestData.GetData<Dictionary<string, string>>(1);
            DataTable dt = NewObject<MaterialDicMgr>().GetMaterialListType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialTypeDic()
        {
            DataTable dtTypeDic = NewObject<IMWDao>().GetMaterialTypeShowCard();
            responseData.AddData(dtTypeDic);
            return responseData;
        }

        /// <summary>
        /// 获取大项目统计
        /// </summary>
        /// <returns>大项目统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStatItem()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetStatItem(false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存物资字典信息
        /// </summary>
        /// <returns>物资字典信息</returns>
        [WCFMethod]
        public ServiceResponseData SaveMaterialDic()
        {
            var dic = requestData.GetData<MW_CenterSpecDic>(0);
            bool isExit = NewObject<MaterialDicMgr>().SaveMaterialDic(dic);
            responseData.AddData(isExit);
            return responseData;
        }

        /// <summary>
        /// 获取物资字典信息
        /// </summary>
        /// <returns>物资字典信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialDic()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var level = requestData.GetData<int>(1);
            DataTable dt = new DataTable();
            dt = NewObject<MaterialDicMgr>().GetMaterialDic(query);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns>单位</returns>
        [WCFMethod]
        public ServiceResponseData GetUnit()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetUnit();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取生产厂家
        /// </summary>
        /// <returns>生产厂家</returns>
        [WCFMethod]
        public ServiceResponseData GetHisDic()
        {
            DataTable dt = NewObject<MaterialProductMgr>().GetProducts(null);
            responseData.AddData(dt);
            DataTable tableMedicare = NewObject<IPublicManageDao>().GetMedicareDic();
            responseData.AddData(tableMedicare);
            return responseData;
        }

        /// <summary>
        /// 审核物资字典信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData AuditDic()
        {
            var id = requestData.GetData<int>(0);
            int result = NewDao<IMWDao>().AuditDic(id);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 读取物资字典厂家信息
        /// </summary>
        /// <returns>物资字典厂家信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadHisDic()
        {
            var centerMatID = requestData.GetData<int>(0);
            DataTable dt = NewDao<IMWDao>().LoadHisDic(centerMatID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存厂家
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveHisDic()
        {
            var p = requestData.GetData<MW_HospMakerDic>(0);
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("CenterMatID", p.CenterMatID.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("ProductID", p.ProductID.ToString(), SqlOperator.Equal));
            var t = NewObject<IMWDao>().GetDataTable<MW_HospMakerDic>(andWhere, null);

            //修改
            if (p.MaterialID != 0)
            {
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
            int result = p.save();
            if (result > 0)
            {
                responseData.AddData(true);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }
    }
}
