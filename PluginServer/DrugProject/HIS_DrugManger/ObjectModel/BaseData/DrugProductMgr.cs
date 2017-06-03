using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药品生产商维护类
    /// </summary>
    public class DrugProductMgr : AbstractObjectModel
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="product">生产商对象</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果对象</returns>
        public DGBillResult SaveProduct(DG_ProductDic product,int workId)
        {
            DGBillResult result = new DGBillResult();

            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("ProductName", product.ProductName, SqlOperator.Equal));
            lst.Add(Tuple.Create("delflag", "0", SqlOperator.Equal));
            lst.Add(Tuple.Create("WorkId", workId.ToString(), SqlOperator.Equal));
            IEnumerable<DG_ProductDic> objs = NewObject<IDGDao>().GetEntityType<DG_ProductDic>(lst, null);

            var p = objs.FirstOrDefault(i => i.ProductID == product.ProductID);
            if (p != null)
            {
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
                result.Result = 0;
                product.save();
                return result;
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
                    stb.AppendFormat("and {0} like '%{1}%' ", pair.Key, pair.Value);
                }
            }

            stb.Append(" ORDER BY ProductID DESC ");
            return NewObject<DG_ProductDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 更新生产商数据
        /// </summary>
        /// <param name="productDic">生产商对象</param>
        public void UpdateProduct(DG_ProductDic productDic)
        {
            productDic.DelFlag = 1;
            productDic.save();
        }

        /// <summary>
        /// 使用OR 语句构造
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>获取生产商</returns>
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
            return NewObject<DG_ProductDic>().gettable(stb.ToString());
        }
    }
}
