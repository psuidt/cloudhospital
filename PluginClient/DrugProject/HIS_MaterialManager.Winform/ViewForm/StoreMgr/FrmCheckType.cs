using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 药品类型选择
    /// </summary>
    public partial class FrmCheckType : BaseFormBusiness, IFrmCheckType
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCheckType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="fatherFrmname">父窗体名</param>
        public FrmCheckType(string fatherFrmname)
        {
            InitializeComponent();
            FatherFrmname = fatherFrmname;
        }

        #region 自定义属性和方法
        /// <summary>
        /// 父窗体名称
        /// </summary>
        public string FatherFrmname { get; set; }

        #endregion

        #region 接口
        /// <summary>
        /// 绑定药品类型
        /// </summary>
        /// <param name="dtDrugType">药品类型</param>
        public void BindMaterialType(DataTable dtDrugType)
        {
            cmb_Type.DataSource = dtDrugType;
            cmb_Type.ValueMember = "TypeID";
            cmb_Type.DisplayMember = "TypeName";
            if (dtDrugType.Rows.Count > 0)
            {
                cmb_Type.SelectedIndex = 0;
                int typeId = Convert.ToInt32(cmb_Type.SelectedValue);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (chkMaterialType.Checked)
            {
                if (cmb_Type.SelectedValue != null)
                {
                    queryCondition.Add("a.TypeID", cmb_Type.SelectedValue.ToString());
                }
            }

            if(chkIsHaveStore.Checked)
            { 
                queryCondition.Add("d.Amount>", "0");
            }

            queryCondition.Add("d.DeptID", deptID.ToString());
            return queryCondition;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 确定选择
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            InvokeController("GetCheckStorageData", FatherFrmname);
            this.Close();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmCheckType_OpenWindowBefore(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 物资类型选择
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkDrugType_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Type.Enabled = chkMaterialType.Checked;
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmCheckType_Load(object sender, EventArgs e)
        {
            InvokeController("GetDrugTypeDic", frmName);
        }
        #endregion
    }
}
