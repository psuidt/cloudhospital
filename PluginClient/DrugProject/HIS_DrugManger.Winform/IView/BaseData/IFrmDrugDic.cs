using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药品字典
    /// </summary>
    interface IFrmDrugDic : IBaseView
    {
        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室参数数据源</param>
        void BindDeptParameters(DataTable dt);

        /// <summary>
        /// 当前中心字典对象
        /// </summary>
        DG_CenterSpecDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> OrWhere { get; set; }

        /// <summary>
        /// 当前本院字典对象
        /// </summary>
        DG_HospMakerDic CurrentHospDic { get; set; }

        /// <summary>
        /// 本院查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> HospAndWhere { get; set; }

        /// <summary>
        /// 当前本院字典对象
        /// </summary>
        Dictionary<string, string> DrugType { get; set; }

        /// <summary>
        /// 药理数据树
        /// </summary>
        /// <param name="dt"> 药理数据集</param>
        void LoadPharms(DataTable dt);

        /// <summary>
        /// 药品类型树数据
        /// </summary>
        /// <param name="dt">药品类型数据集</param>
        void LoadDrugType(DataTable dt);

        /// <summary>
        /// 字典列表
        /// </summary>
        /// <param name="dt">字典数据集</param>
        void LoadDrugDic(DataTable dt);

        /// <summary>
        /// 药品子类型CARD
        /// </summary>
        /// <param name="dt">药品子类型数据集</param>
        void LoadDrugCType(DataTable dt);

        /// <summary>
        /// 药品类型CARD
        /// </summary>
        /// <param name="dt">药品类型数据集</param>
        void LoadDrugTypeForTb(DataTable dt);

        /// <summary>
        /// 药品单位
        /// </summary>
        /// <param name="dt">药品单位数据集</param>
        void LoadCommUnit(DataTable dt);

        /// <summary>
        /// 统计大项目
        /// </summary>
        /// <param name="dt">统计大项目数据集</param>
        void LoadStat(DataTable dt);

        /// <summary>
        /// 药品剂型
        /// </summary>
        /// <param name="dt">药品药剂数据集</param>
        void LoadDosage(DataTable dt);

        /// <summary>
        /// 抗生素级别
        /// </summary>
        /// <param name="dt">抗生素级别数据集</param>   
        void LoadAnt(DataTable dt);

        /// <summary>
        /// 保存成功后执行的函数
        /// </summary>
        /// <param name="isNew">是否是新增</param>
        void SaveSuccess(bool isNew);

        /// <summary>
        /// 保存成功后执行的函数
        /// </summary>
        void SaveHospSuccess();

        #region 本院典
        /// <summary>
        /// 生产厂家
        /// </summary>
        /// <param name="dt">生产厂家数据集</param>
        void LoadProduct(DataTable dt);

        /// <summary>
        /// 医保类型
        /// </summary>
        /// <param name="dt">医保类型数据集</param>
        void LoadMedicare(DataTable dt);

        /// <summary>
        ///  本院典列表
        /// </summary>
        /// <param name="dt">本院典列表数据集</param>
        void LoadHisDic(DataTable dt);

        #endregion
    }
}
