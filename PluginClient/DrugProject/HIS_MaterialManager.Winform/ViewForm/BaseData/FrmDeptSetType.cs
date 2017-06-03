using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 药品类型选择
    /// </summary>
    public partial class FrmDeptSetType : BaseFormBusiness,IFrmDeptSetType
    {
        /// <summary>
        /// 药品类型集合
        /// </summary>
        public List<int> DrugTypeList = new List<int>();

        /// <summary>
        /// 选择结构0-关闭界面  1-选择后关闭
        /// </summary>
        public int Result = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDeptSetType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确认选择
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int addType;
            DrugTypeList.Clear();
            if (dg_TypeDic.Rows.Count == 0)
            {
                MessageBox.Show("没有物资类型可选");
                return;
            }

            foreach (DataGridViewRow row in dg_TypeDic.Rows)
            {
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells["selected"]).Value) == true)
                {
                    addType = Convert.ToInt32(row.Cells["TypeID"].Value);
                    DrugTypeList.Add(addType);
                }
            }

            if (DrugTypeList.Count <= 0)
            {
                MessageBox.Show("您没有选择任何物资类型");
                return;
            }

            DialogResult = DialogResult.OK;
            Result = 1;
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = 0;
            this.Close();
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmDeptSetType_Load(object sender, EventArgs e)
        {
            InvokeController("GetTypeDic");
        }

        /// <summary>
        /// 绑定类型列表
        /// </summary>
        /// <param name="dt">类型列表</param>
        public void BindGrid(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = 1;
                }
            }

            dg_TypeDic.DataSource = dt;
        }
    }
}
