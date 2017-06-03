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
    /// 药品剂型维护类
    /// </summary>
    public class DosageMgr : AbstractObjectModel
    {
        /// <summary>
        /// 查询表数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>数据源</returns>
        public DataTable GetDosageData(Dictionary<string, string> query = null)
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

            return NewObject<DG_DosageDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 获取药剂数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>数据源</returns>
        public DataTable GetDosageDataUserOr(Dictionary<string, string> query = null)
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

            return NewObject<DG_DosageDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 保存药品剂型
        /// </summary>
        /// <param name="dosage">药品剂型对象</param>
        /// <returns>是否成功</returns>
        public bool SaveDosage(DG_DosageDic dosage)
        {
            try
            {
                List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
                lst.Add(Tuple.Create("DosageName", dosage.DosageName, SqlOperator.Equal));
                lst.Add(Tuple.Create("TypeID", dosage.TypeID.ToString(), SqlOperator.Equal));
                lst.Add(Tuple.Create("DelFlag", "0", SqlOperator.Equal));
                IEnumerable<DG_DosageDic> objs = NewObject<IDGDao>().GetEntityType<DG_DosageDic>(lst, null);
                this.BindDb(dosage);
                if (objs != null && objs.Any())
                {
                    //同名记录存在
                    return false;
                }

                dosage.save();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dosage">药品剂型对象</param>
        public void UpdateDosage(DG_DosageDic dosage)
        {
            dosage.DelFlag = 1;
            dosage.save();
        }
    }
}
