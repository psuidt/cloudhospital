using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 本院厂家典维护类
    /// </summary>
    public class DrugMakerDicMgr: AbstractObjectModel
    {
        /// <summary>
        /// 获取科室可以出库的药品
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="bussType">业务类型</param>
        /// <param name="toDeptId">往来科室ID</param>
        /// <returns>科室可以出库的药品</returns>
        public DataTable GetDurgDic(int deptId, string bussType, int toDeptId)
        {
            //if (bussType == "124")//退库
            //{
            //    List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            //    andWhere.Add(Tuple.Create("DrugDeptID", deptId.ToString(), SqlOperator.Equal));
            //    andWhere.Add(Tuple.Create("RelationDeptID", toDeptId.ToString(), SqlOperator.Equal));
            //    var list = NewObject<IDGDao>().GetEntityType<DG_DeptRelation>(andWhere, null);
            //    var obj = list.FirstOrDefault();

            //    if (obj != null)//当前往来科室存在
            //    {
            //        if (obj.RelationDeptType == 1)//当前往来科室的类型是药房
            //        {
            //            return NewDao<IDGDao>().GetStoreDrugInFo(toDeptId);//查药房药剂信息
            //        }
            //        else
            //        {
            //            return NewDao<IDGDao>().GetStoreDrugInFo(deptId);
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception("往来科室配置数据错误");
            //    }
            //}
            return NewDao<IDWDao>().GetStoreDrugInFo(deptId);
        }
    }
}
