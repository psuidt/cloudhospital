using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManager.ObjectModel.BaseData
{
    /// <summary>
    /// 物资生产商管理类
    /// </summary>
    public class MaterialProductMgr : AbstractObjectModel
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="product">生产厂家实体</param>
        /// <param name="workId">机构id</param>
        /// <returns>true成功</returns>
        public bool SaveProduct(MW_ProductDic product, int workId)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("ProductName", product.ProductName);
                dictionary.Add("WorkId", workId.ToString());
                DataTable dt = GetProducts(dictionary);

                //新增有重名数据
                if (product.ProductID == 0 && dt.Rows.Count > 0)
                {
                    return false;
                }
                else if (product.ProductID != 0 && dt.Rows.Count > 0)
                {
                    if (dt.Select("ProductID=" + product.ProductID).Length == 1 && dt.Rows.Count > 1)
                    {
                        return false;
                    }

                    if (dt.Select("ProductID=" + product.ProductID).Length == 0 && dt.Rows.Count > 0)
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
        /// 获取生产商
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产商数据</returns>
        public DataTable GetProducts(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" DelFlag=0 ");
            if (query != null && query.Count > 0)
            {
                foreach (var pair in query)
                {
                    stb.AppendFormat("and {0}='{1}' ", pair.Key, pair.Value);
                }
            }

            stb.Append(" ORDER BY ProductID DESC ");
            return NewObject<MW_ProductDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 更新生产商数据
        /// </summary>
        /// <param name="productDic">生产商实体</param>
        public void UpdateProduct(MW_ProductDic productDic)
        {
            productDic.DelFlag = 1;
            productDic.save();
        }

        /// <summary>
        /// 使用OR 语句构造
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产厂商数据</returns>
        public DataTable GetProductsUserOr(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" DelFlag=0 ");
            if (query != null && query.Count > 0)
            {
                stb.Append(" and ( ");
                var i = 0;
                foreach (var pair in query)
                {
                    if (i == 0)
                    {
                        stb.AppendFormat(" {0} like'{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" OR {0} like '{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            stb.Append(" ORDER BY ProductID DESC ");
            return NewObject<MW_ProductDic>().gettable(stb.ToString());
        }
    }
}
