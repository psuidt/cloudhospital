using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmPackBed : BaseFormBusiness, IPackBed
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPackBed()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定已分配床位病人列表
        /// </summary>
        /// <param name="patListDt">已分配床位病人列表</param>
        public void Bind_InHospitalPatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        /// <summary>
        /// 包床数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable dt = grdPatList.DataSource as DataTable;
                int wardId = int.Parse(dt.Rows[rowIndex]["WardID"].ToString());
                string bedNo = dt.Rows[rowIndex]["BedNo"].ToString();
                // 保存床位分配
                InvokeController("SavePackBedData", wardId, bedNo);
            }
            else
            {
                // 提示选择病人Msg
                InvokeController("MessageShow", "请选择一个需要包床的病人！");
            }
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">FrmPackBed</param>
        /// <param name="e">事件参数</param>
        private void FrmPackBed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                btnSave_Click(sender, e);
            }
        }

        /// <summary>
        /// 双击选中病人包床
        /// </summary>
        /// <param name="sender"> grdPatList</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave_Click(null, null);
        }
    }
}
