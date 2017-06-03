using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.BaseData
{
    /// <summary>
    /// 物资字典管理类
    /// </summary>
    public class MaterialDicMgr : AbstractObjectModel
    {
        /// <summary>
        /// 保存物资字典信息
        /// </summary>
        /// <param name="dic">物资中心字典实体</param>
        /// <returns>true存在</returns>
        public bool SaveMaterialDic(MW_CenterSpecDic dic)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("CenterMatName", dic.CenterMatName, SqlOperator.Equal));
            andWhere.Add(Tuple.Create("Spec", dic.Spec, SqlOperator.Equal));
            var t = NewObject<IMWDao>().GetDataTable<MW_CenterSpecDic>(andWhere, null);
            bool isExit = false;
            if (dic.CenterMatID == 0)
            {
                if (t.Rows.Count > 0)
                {
                    isExit = true;
                }
            }

            if (!isExit)
            {
                this.BindDb(dic);
                int id = dic.save();
            }

            return isExit;
        }

        /// <summary>
        /// 获取物资字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="level">级别</param>
        /// <returns>物资字典数据</returns>
        public DataTable GetMaterialDicByChild(Dictionary<string, string> query = null, int level = 0)
        {
            string strSql = @"SELECT a.*,
                                        case when (a.AuditStatus=1) then '已审核' else '未审核' end as AuditStatusName,
                                        case when (a.IsStop=1) then '已停用' else '已启用' end as IsStopName,
                                        b.TypeName as ThreeLevel,c.TypeName as TwoLevel,d.TypeName as OneLevel,d.TypeID as OneID,c.TypeID as TwoID,a.TypeID as ThreeID,a.UnitID
                                        from MW_CenterSpecDic a left join MW_TypeDic b on a.TypeID=b.TypeID
                                        left join MW_TypeDic c on c.TypeID=b.ParentID
                                        left join MW_TypeDic d on d.TypeID=c.ParentID WHERE";
            StringBuilder stb = new StringBuilder();
            if (query != null)
            {
                foreach (var pair in query)
                {
                    if (pair.Key == "Search")
                    {
                        stb.AppendFormat(" and (a.CenterMatName like '%{0}%' or a.PyCode like '%{0}%' or a.WbCode like '%{0}%' )", pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" and {0}={1}", pair.Key, pair.Value);
                    }
                }
            }

            return NewDao<IMWDao>().GetMaterialDic(strSql + stb.ToString());
        }

        /// <summary>
        /// 获取物资字典
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资字典数据</returns>
        public DataTable GetMaterialDic(Dictionary<string, string> query = null)
        {
            string strSql = @"SELECT a.*,
                                        case when (a.AuditStatus=1) then '已审核' else '未审核' end as AuditStatusName,
                                        case when (a.IsStop=1) then '已停用' else '已启用' end as IsStopName,
                                        b.TypeName as ThreeLevel,c.TypeName as TwoLevel,d.TypeName as OneLevel,d.TypeID as OneID,c.TypeID as TwoID,a.UnitID
                                        from MW_CenterSpecDic a left join MW_TypeDic b on a.TypeID=b.TypeID
                                        left join MW_TypeDic c on c.TypeID=b.ParentID
                                        left join MW_TypeDic d on d.TypeID=c.ParentID WHERE 1=1";
            StringBuilder stb = new StringBuilder();
            if (query != null)
            {
                foreach (var pair in query)
                {
                    if (pair.Key == "Search")
                    {
                        stb.AppendFormat(" and (a.CenterMatName like '%{0}%' or a.PyCode like '%{0}%' or a.WbCode like '%{0}%' )", pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" and {0}={1}", pair.Key, pair.Value);
                    }
                }
            }

            return NewDao<IMWDao>().GetMaterialDic(strSql + stb.ToString());
        }

        /// <summary>
        /// 获取物资类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资类型列表</returns>
        public List<MW_TypeDic> GetMaterialType(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            if (query != null && query.Count > 0)
            {
                stb.AppendFormat(" 1=1 ");
                stb.Append(" and ( ");
                var i = 0;
                foreach (var pair in query)
                {
                    if (i == 0)
                    {
                        stb.AppendFormat(" {0} like '%{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" OR {0} like '%{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            return NewObject<MW_TypeDic>().getlist<MW_TypeDic>(stb.ToString());
        }

        /// <summary>
        /// 获取物资子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资子类型数据</returns>
        public DataTable GetMaterialListType(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            if (query != null && query.Count > 0)
            {
                stb.AppendFormat(" 1=1 ");
                stb.Append(" and ( ");
                foreach (var pair in query)
                {
                    if (pair.Key != string.Empty)
                    {
                        stb.AppendFormat(" {0}={1} ", pair.Key, pair.Value);
                    }
                }

                stb.Append(" ) ");
            }

            return NewObject<MW_TypeDic>().gettable(stb.ToString());
        }
    }
}
