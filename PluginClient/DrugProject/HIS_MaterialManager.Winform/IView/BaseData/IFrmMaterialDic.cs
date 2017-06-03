using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 物资字典接口
    /// </summary>
    public interface IFrmMaterialDic : IBaseView
    {
        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindDeptParameters(DataTable dt);

        /// <summary>
        /// 获取物资类型
        /// </summary>
        /// <param name="typelist">物资类型</param>
        void LoadMaterialType(DataTable typelist);

        /// <summary>
        /// 绑定物资类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        void BindTypeCombox(DataTable dt);

        /// <summary>
        /// 保存完成后提示
        /// </summary>
        /// <param name="result">保存结果</param>
        void CompleteSave(bool result);

        /// <summary>
        /// 绑定大类型项目
        /// </summary>
        /// <param name="dt">大类型项目</param>
        void LoadStat(DataTable dt);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetQuery(string workId);

        /// <summary>
        /// 绑定物资字典网格信息
        /// </summary>
        /// <param name="dt">物资字典列表</param>
        void BInddgMeter(DataTable dt);

        /// <summary>
        /// 获取当前选中行
        /// </summary>
        MW_CenterSpecDic CurrentData { get; set; }

        /// <summary>
        /// 选中的厂家字典数据
        /// </summary>
        MW_HospMakerDic CurrentHospDic { get; set; }

        /// <summary>
        /// 绑定单位
        /// </summary>
        /// <param name="dt">单位</param>
        void LoadUnit(DataTable dt);

        /// <summary>
        /// 绑定生产厂家
        /// </summary>
        /// <param name="dt">生产厂家</param>
        void LoadProduct(DataTable dt);

        /// <summary>
        /// 绑定医保类型
        /// </summary>
        /// <param name="dt">医保类型</param>
        void LoadMedicare(DataTable dt);

        /// <summary>
        /// 审核完成后提示
        /// </summary>
        /// <param name="result">审核结果</param>
        void CompleteAudit(int result);

        /// <summary>
        /// 绑定厂家网格信息
        /// </summary>
        /// <param name="dt">厂家信息</param>
        void LoadHisDic(DataTable dt);

        /// <summary>
        /// 保存厂家信息后清空数据
        /// </summary>
        void SaveHospSuccess();
    }
}
