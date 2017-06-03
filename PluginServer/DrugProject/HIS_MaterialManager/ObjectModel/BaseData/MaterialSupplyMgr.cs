using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.BaseData
{
    /// <summary>
    /// 物资供应商管理类
    /// </summary>
    public class MaterialSupplyMgr : AbstractObjectModel
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="product">供应商实体</param>
        /// <param name="workId">机构id</param>
        /// <returns>true保存成功</returns>
        public bool SaveSupply(MW_SupportDic product, int workId)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("SupportName", product.SupportName);
                dictionary.Add("WorkId", workId.ToString());
                DataTable dt = GetSupplys(dictionary);

                //新增有重名数据
                if (product.SupplierID == 0 && dt.Rows.Count > 0)
                {
                    return false;
                }
                else if (product.SupplierID != 0 && dt.Rows.Count > 0)
                {
                    if (dt.Select("SupplierID=" + product.SupplierID).Length == 1 && dt.Rows.Count > 1)
                    {
                        return false;
                    }

                    if (dt.Select("SupplierID=" + product.SupplierID).Length == 0 && dt.Rows.Count > 0)
                    {
                        return false;
                    }
                }

                this.BindDb(product);
                SetWorkId(workId);
                product.save();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>供应商数据</returns>
        public DataTable GetSupplys(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" DelFlag=0 ");
            if (query != null)
            {
                foreach (var pair in query)
                {
                    stb.AppendFormat("and {0}='{1}' ", pair.Key, pair.Value);
                }
            }

            stb.Append(" ORDER BY SupplierID DESC ");
            return NewObject<MW_SupportDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 分页方式获取供应商
        /// </summary>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <param name="pageInfo">页面信息</param>
        /// <returns>供应商数据</returns>
        public DataTable GetSupplys(int pageNo, int pageSize, List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, out PageInfo pageInfo)
        {
            PageInfo page = new PageInfo(pageSize, pageNo);
            page.KeyName = "SupplierID";
            pageInfo = page;
            return NewObject<IMWDao>().GetDataTable<MW_SupportDic>(andWhere, orWhere, pageInfo);
        }

        /// <summary>
        /// 更新供应商数据
        /// </summary>
        /// <param name="productDic">供应商实体</param>
        public void UpdateSupply(MW_SupportDic productDic)
        {
            productDic.DelFlag = 1;
            productDic.save();
        }

        /// <summary>
        /// 使用OR 语句构造
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>供应商数据</returns>
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
            return NewObject<MW_SupportDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 返回供应商列表【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        public DataTable GetSupplyForShowCard()
        {
            return NewDao<IMWDao>().GetSupplyForShowCard();
        }
    }
}
