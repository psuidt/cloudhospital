using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 会员信息接口
    /// </summary>
    public interface IFrmMemberInfo: IBaseView
    {
        /// <summary>
        /// Gets or sets 查询条件
        /// </summary>
        /// <value>查询条件</value>
        string SqlCondition { get; set; }

        /// <summary>
        /// Gets or sets 基础数据集
        /// </summary>
        /// <value>基础数据集</value>
        DataSet BaseDataSet {get;set;}

        /// <summary>
        /// Gets or sets 页号
        /// </summary>
        /// <value>页号</value>
        int PageNO { get; set; }

        /// <summary>
        /// Gets or sets 每页条数
        /// </summary>
        /// <value>每页条数</value>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets 会员网格索引
        /// </summary>
        /// <value>会员网格索引</value>
        int MemberGridIndex { get; set; }

        /// <summary>
        /// Gets or sets 帐户网格索引
        /// </summary>
        /// <value>帐户网格索引</value>
        int AccountGridInex { get; set; }

        /// <summary>
        /// Gets or sets 新增标识
        /// </summary>
        /// <value>新增标识</value>
        int NewFlag { get; set; }

        /// <summary>
        /// Gets or sets 会员信息实体类
        /// </summary>
        /// <value>会员信息实体类</value>
        ME_MemberInfo MemberInfoEntity { get; set; }

        /// <summary>
        /// Gets or sets 会员帐户类型
        /// </summary>
        /// <value>会员帐户类型</value>
        int CardTypeID { get; set; }

        /// <summary>
        /// Gets or sets 帐户号码
        /// </summary>
        /// <value>帐户号码</value>
        string CardNO { get; set; }

        /// <summary>
        /// Gets or sets 账号ID
        /// </summary>
        /// <value>账号ID</value>
        int AccountID { get; set; }

        /// <summary>
        /// Gets or sets 保存界面信息时返回
        /// </summary>
        /// <value>保存界面信息时返回</value>
        int SaveResult { get; set; }

        /// <summary>
        /// 绑定基础数据
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        /// <param name="dtSex">性别</param>
        /// <param name="dtNation">民族</param>
        /// <param name="dtCity">城市</param>
        /// <param name="dtDegree">学历</param>
        /// <param name="dtRelation">关系</param>
        /// <param name="dtCardType">卡类型</param>
        /// <param name="dtOccupation">职业</param>
        /// <param name="dtMatrimony">婚姻状况</param>
        /// <param name="dtRoute">知晓途径</param>
        /// <param name="dtNationality">国籍</param>
        void BindAllInfo(DataTable dtPatType, DataTable dtSex, DataTable dtNation, DataTable dtCity, DataTable dtDegree, DataTable dtRelation, DataTable dtCardType, DataTable dtOccupation, DataTable dtMatrimony,DataTable dtRoute,DataTable dtNationality);

        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <param name="ds">基础数据集</param>
        void SetDefaultValue(DataSet ds);

        /// <summary>
        /// 电话号码
        /// </summary>

        string strTel { get; set; }
    }
}
