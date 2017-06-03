using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药剂科室维护类
    /// </summary>
    public class DrugDeptMgr:EFWCoreLib.CoreFrame.Business.AbstractObjectModel
    {   
        /// <summary>
        /// 药剂科室是否已存在
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>true成功false失败</returns>
        public bool ExistDrugDept(int deptId)
        {
            bool returnVal =  NewDao<IDGDao>().ExistDrugDept(deptId);
            return returnVal;
        }

        /// <summary>
        /// 启用药剂科室
        /// </summary>
        /// <param name="deptID">科室Id</param>
        public void StartDept(int deptID)
        {
            DG_DeptDic deptDic = NewObject<DG_DeptDic>();
            deptDic.StopFlag = 0;
            deptDic.save();
        }

        /// <summary>
        /// 删除药剂科室
        /// </summary>
        /// <param name="deptDicID">药剂科室Id</param>
        /// <returns>小于0失败</returns>
        public int DeleteDeptDic(int deptDicID)
        {
            int retVal = NewObject<DG_DeptDic>().delete(deptDicID);
            return retVal;
        }

        /// <summary>
        /// 取得药剂科室数据
        /// </summary>
        /// <returns>药剂科室数据集</returns>
        public DataTable GetDeptDicData()
        {
            DataTable dt = NewDao<IDGDao>().GetDeptDicData();
            return dt;
        }

        /// <summary>
        /// 按类型获取药剂科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>药剂科室列表</returns>
        public DataTable GetDrugDeptList(int deptType)
        {
            DataTable dt = NewDao<IDGDao>().GetDrugDeptList(deptType);
            return dt;
        }

        /// <summary>
        /// 取得药剂科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室单据集</returns>
        public DataTable GetSerialNumber(int deptId)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        /// <summary>
        /// 获取药剂科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室单据集</returns>
        public DataTable GetDrugDeptBill(int deptId)
        {
            DataTable dt = NewDao<IDGDao>().GetDrugDeptBill(deptId);
            return dt;
        }

        /// <summary>
        /// 初始化单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型Id</param>
        /// <returns>处理结果</returns>
        public bool InitSerialNumber(int deptId, int deptType)
        {
            bool retVal = NewObject<IDGDao>().InitSerialNumber(deptId, deptType);
            return retVal;
        }

        /// <summary>
        /// 修改启用标志
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int UpdateStartStatus(int deptId)
        {
            int retVal = NewObject<IDGDao>().UpdateStartStatus(deptId);
            return retVal;
        }

        /// <summary>
        /// 停用药剂科室
        /// </summary>
        /// <param name="deptDicID">药剂科室Id</param>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int StopUseDrugDept(int deptDicID,int  deptId)
        {
            int retVal = NewObject<IDGDao>().StopUseDrugDept(deptDicID,deptId);
            return retVal;
        }

        /// <summary>
        /// 保存药剂科室管理类型表
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="drugTypeList">药剂科室类型列表</param>
        /// <returns>true成功false失败</returns>
        public bool SaveDeptType(int deptId, List<int> drugTypeList)
        {
            foreach (int drugType in drugTypeList)
            {
                DG_Dept_Type model = NewObject<DG_Dept_Type>();
                model.DeptID = deptId;
                model.DrugTypeID = drugType;
                this.BindDb(model);
                model.save();
            }

            return true;
        }

        /// <summary>
        /// 所属科室 是否处于盘点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>true 非盘点状态 false:盘点状态</returns>
        public bool IsDeptChecked(int deptId,int workId)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DeptID", deptId.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("WorkID", workId.ToString(), SqlOperator.Equal));
            var obj= NewDao<IDGDao>().GetEntityType<DG_DeptDic>(andWhere, null).FirstOrDefault();
            if (obj != null&& obj.CheckStatus==0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取药剂科室对象
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>药剂科室对象</returns>
        public DG_DeptDic GetDepdic(int deptId, int workId)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DeptID", deptId.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("WorkID", workId.ToString(), SqlOperator.Equal));
            var obj = NewDao<IDGDao>().GetEntityType<DG_DeptDic>(andWhere, null).FirstOrDefault();
            return obj;
        }

        /// <summary>
        /// 查询药剂科室
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>药剂科室</returns>
        public DataTable GetDeptDicData(List<Tuple<string, string, SqlOperator>> andWhere)
        {
            return NewDao<IDGDao>().GetDataTable<DG_DeptDic>(andWhere, null);
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型数据集</returns>
        public DataTable GetTypeDic()
        {
            DataTable dt = NewDao<IDGDao>().GetTypeDic();
            return dt;
        }
    }
}
