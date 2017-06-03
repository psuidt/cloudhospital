using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
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
        /// 构造函数
        /// </summary>
        /// <param name="fatherFrmname">父级窗体</param>
        public FrmCheckType(string fatherFrmname)
        {
            InitializeComponent();
            FatherFrmname = fatherFrmname;
        }

        #region 自定义属性和方法
        /// <summary>
        /// Gets or sets父窗体名称
        /// </summary>
        public string FatherFrmname { get; set; }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定药品剂型ShowCard
        /// </summary>
        /// <param name="dtDrugDosage">药品剂型</param>
        public void BindDrugDosageShowCard(DataTable dtDrugDosage)
        {
            cbxDosage.DataSource = dtDrugDosage;
            cbxDosage.ValueMember = "DosageID";
            cbxDosage.DisplayMember = "DosageName";          
        }

        /// <summary>
        /// 绑定药品类型
        /// </summary>
        /// <param name="dtDrugType">药品类型</param>
        public void BindDrugType(DataTable dtDrugType)
        {
            cmb_Type.DataSource = dtDrugType;
            cmb_Type.ValueMember = "TypeID";
            cmb_Type.DisplayMember = "TypeName";
            if (dtDrugType.Rows.Count > 0)
            {
                cmb_Type.SelectedIndex = 0;
                int typeId = Convert.ToInt32(cmb_Type.SelectedValue);
                InvokeController("GetDosageDic", frmName, typeId);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (chkDrugType.Checked)
            {
                if (cmb_Type.SelectedValue != null)
                {
                    queryCondition.Add("a.TypeID", cmb_Type.SelectedValue.ToString());
                }
            }

            if (chkDrugDose.Checked)
            {
                if (cbxDosage.SelectedValue != null)
                {
                    queryCondition.Add("a.DosageID", cbxDosage.SelectedValue.ToString());
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            InvokeController("GetCheckStorageData", FatherFrmname);
            this.Close();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheckType_OpenWindowBefore(object sender, EventArgs e)
        {        
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_Type_DropDownClosed(object sender, EventArgs e)
        {
            int typeId = Convert.ToInt32(cmb_Type.SelectedValue);
            InvokeController("GetDosageDic", frmName, typeId);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkDrugType_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Type.Enabled = chkDrugType.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkDrugDose_CheckedChanged(object sender, EventArgs e)
        {
            cbxDosage.Enabled = chkDrugDose.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheckType_Load(object sender, EventArgs e)
        {
            InvokeController("GetDrugTypeDic", frmName);
        }
        #endregion
    }
}
