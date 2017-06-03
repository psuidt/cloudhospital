using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药品供应商维护类
    /// </summary>
    public class DrugSupplyMgr : AbstractObjectModel
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="product">供应商对象</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果对象</returns>
        public DGBillResult SaveSupply(DG_SupportDic product, int workId)
        {
            DGBillResult result = new DGBillResult();
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("SupportName", product.SupportName, SqlOperator.Equal));
            lst.Add(Tuple.Create("delflag", "0", SqlOperator.Equal));
            lst.Add(Tuple.Create("WorkId", workId.ToString(), SqlOperator.Equal));
            IEnumerable<DG_SupportDic> objs = NewObject<IDGDao>().GetEntityType<DG_SupportDic>(lst, null);
            var vp = objs.FirstOrDefault(i => i.SupplierID == product.SupplierID);
            if (vp != null)
            {
                this.BindDb(product);
                SetWorkId(workId);
                product.save();
                result.Result = 0;
                return result;
            }

            if (objs != null && objs.Any())
            {
                result.Result = 1;
                result.ErrMsg = "已经存在同名的生产厂厂家名称";
                return result;
            }
            else
            {
                this.BindDb(product);
                product.save();
                result.Result = 0;
                return result;
            }
        }

        /// <summary>
        /// 获取生产商
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产商</returns>
        public DataTable GetSupplys(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" DelFlag=0 ");
            if (query != null)
            {
                foreach (var pair in query)
                {
                    stb.AppendFormat("and {0} like '%{1}%' ", pair.Key, pair.Value);
                }
            }

            stb.Append(" ORDER BY SupplierID DESC ");
            return NewObject<DG_SupportDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 分页方式获取生产商
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页面数量</param>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <param name="pageInfo">页面对象</param>
        /// <returns>生产商数据源</returns>
        public DataTable GetSupplys(int pageNo, int pageSize, List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, out PageInfo pageInfo)
        {
            PageInfo page = new PageInfo(pageSize, pageNo);
            page.KeyName = "SupplierID";
            pageInfo = page;
            return NewObject<IDGDao>().GetDataTable<DG_SupportDic>(andWhere, orWhere, pageInfo);
        }

        /// <summary>
        /// 更新生产商数据
        /// </summary>
        /// <param name="productDic">生产商对象</param>
        public void UpdateSupply(DG_SupportDic productDic)
        {
            productDic.DelFlag = 1;
            productDic.save();
        }

        /// <summary>
        /// 使用OR 语句构造
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产商数据源</returns>
        public DataTable GetSupplysUserOr(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" DelFlag=0 ");
            if (query != null && query.Count > 0)
            {
                stb.Append("and ( ");
                var i = 0;
                foreach (var pair in query)
                {
                    if (i == 0)
                    {
                        stb.AppendFormat(" {0} LIKE '{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat("OR {0} LIKE '{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            stb.Append(" ORDER BY SupplierID DESC ");
            return NewObject<DG_SupportDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 返回供应商列表【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        public DataTable GetSupplyForShowCard()
        {
            return NewDao<IDGDao>().GetSupplyForShowCard();
        }
    }
}
