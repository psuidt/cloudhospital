using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmDeptSetType : BaseFormBusiness,IFrmDeptSetType
    {
        /// <summary>
        /// 药品类型集
        /// </summary>
        public List<int> DrugTypeList = new List<int>();

        /// <summary>
        /// 结果
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
        /// 确定操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int addType;
            DrugTypeList.Clear();
            if (dg_TypeDic.Rows.Count == 0)
            {
                MessageBox.Show("没有药品类型可选");
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
                MessageBox.Show("您没有选择任何药品类型");
                return;
            }

            DialogResult = DialogResult.OK;
            Result = 1;
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = 0;
            this.Close();
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDeptSetType_Load(object sender, EventArgs e)
        {
            InvokeController("GetTypeDic");
        }

        /// <summary>
        /// 绑定网格数据
        /// </summary>
        /// <param name="dt">数据源</param>
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
