using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.BaseData
{
    /// <summary>
    /// 物资库房管理类
    /// </summary>
    public class MaterialDeptMgr : AbstractObjectModel
    {
        /// <summary>
        /// 物资科室是否已存在
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>true成功false失败</returns>
        public bool ExistMaterialDept(int deptId)
        {
            bool returnVal = NewDao<IMWDao>().ExistMaterialDept(deptId);
            return returnVal;
        }

        /// <summary>
        /// 启用物资科室
        /// </summary>
        /// <param name="deptID">科室Id</param>
        public void StartDept(int deptID)
        {
            MW_DeptDic deptDic = NewObject<MW_DeptDic>();
            deptDic.StopFlag = 0;
            deptDic.save();
        }

        /// <summary>
        /// 删除物资科室
        /// </summary>
        /// <param name="deptDicID">物资科室Id</param>
        /// <returns>小于0失败</returns>
        public int DeleteDeptDic(int deptDicID)
        {
            int retVal = NewObject<MW_DeptDic>().delete(deptDicID);
            return retVal;
        }

        /// <summary>
        /// 取得物资科室数据
        /// </summary>
        /// <returns>物资科室数据集</returns>
        public DataTable GetDeptDicData()
        {
            DataTable dt = NewDao<IMWDao>().GetDeptDicData();
            return dt;
        }

        /// <summary>
        /// 按类型获取物资科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>物资科室列表</returns>
        public DataTable GetDrugDeptList(int deptType)
        {
            DataTable dt = NewDao<IMWDao>().GetDrugDeptList(deptType);
            return dt;
        }

        /// <summary>
        /// 取得物资科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>物资科室单据集</returns>
        public DataTable GetSerialNumber(int deptId)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        /// <summary>
        /// 获取物资科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>物资科室单据集</returns>
        public DataTable GetDrugDeptBill(int deptId)
        {
            DataTable dt = NewDao<IMWDao>().GetDrugDeptBill(deptId);
            return dt;
        }

        /// <summary>
        /// 初始化单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型Id</param>
        /// <returns>1成功</returns>
        public bool InitSerialNumber(int deptId, int deptType)
        {
            bool retVal = NewObject<IMWDao>().InitSerialNumber(deptId, deptType);
            return retVal;
        }

        /// <summary>
        /// 修改启用标志
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int UpdateStartStatus(int deptId)
        {
            int retVal = NewObject<IMWDao>().UpdateStartStatus(deptId);
            return retVal;
        }

        /// <summary>
        /// 停用物资科室
        /// </summary>
        /// <param name="deptDicID">物资科室Id</param>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int StopUseDrugDept(int deptDicID, int deptId)
        {
            int retVal = NewObject<IMWDao>().StopUseDrugDept(deptDicID, deptId);
            return retVal;
        }

        /// <summary>
        /// 保存物资科室管理类型表
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="drugTypeList">物资科室类型列表</param>
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
        /// <param name="deptId">科室id</param>
        /// <returns>true 非盘点状态 false:盘点状态</returns>
        public bool IsDeptChecked(int deptId)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DeptID", deptId.ToString(), SqlOperator.Equal));
            var obj = NewDao<IMWDao>().GetEntityType<MW_DeptDic>(andWhere, null).FirstOrDefault();
            if (obj != null && obj.CheckStatus == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取药剂科室字典数据
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="workId">机构id</param>
        /// <returns>药剂科室实体</returns>
        public MW_DeptDic GetDepdic(int deptId, int workId)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DeptID", deptId.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("WorkID", workId.ToString(), SqlOperator.Equal));
            var obj = NewDao<IMWDao>().GetEntityType<MW_DeptDic>(andWhere, null).FirstOrDefault();
            return obj;
        }

        /// <summary>
        /// 查询物资科室
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>物资科室数据</returns>
        public DataTable GetDeptDicData(List<Tuple<string, string, SqlOperator>> andWhere)
        {
            return NewDao<IMWDao>().GetDataTable<DG_DeptDic>(andWhere, null);
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型数据集</returns>
        public DataTable GetTypeDic()
        {
            DataTable dt = NewDao<IMWDao>().GetMWTypeDic();
            return dt;
        }

        /// <summary>
        /// 取得物资科室
        /// </summary>
        /// <returns>物资科室数据</returns>
        public DataTable GetMaterialDept()
        {
            DataTable dt = NewDao<IMWDao>().GetMaterialDept();
            return dt;
        }
    }
}
