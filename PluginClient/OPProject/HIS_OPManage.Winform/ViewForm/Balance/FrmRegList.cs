using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRegList : BaseFormBusiness, IFrmRegList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRegList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示病人列表
        /// </summary>
        /// <param name="oplist">病人对象列表</param>
        public void SetPatLists(List<OP_PatList> oplist)
        {
            dgRegInfo.DataSource = null;
            dgRegInfo.EndEdit();
            dgRegInfo.DataSource = oplist;

            if (dgRegInfo != null && dgRegInfo.Rows.Count > 0)
            {
                dgRegInfo.CurrentCell = dgRegInfo[0, 0];
            }
        }
   
        /// <summary>
        /// 当前病人对象
        /// </summary>
        private OP_PatList curPatlist;

        /// <summary>
        /// 当前病人对象
        /// </summary>
        public OP_PatList GetcurPatlist
        {
            get
            {
                if (dgRegInfo.CurrentCell != null)
                {
                    int rowindex = dgRegInfo.CurrentCell.RowIndex;
                    List<OP_PatList> listregtype = (List<OP_PatList>)dgRegInfo.DataSource;
                    curPatlist = listregtype[rowindex];
                }
                else
                {
                    curPatlist = new OP_PatList();
                }

                return curPatlist;
            }           
        }

        /// <summary>
        /// 网格双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void dgRegInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgRegInfo.CurrentCell != null)
            {
                InvokeController("GetSelectPatlist");
                this.Close();
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgRegInfo.CurrentCell != null)
            {
                InvokeController("GetSelectPatlist");
                this.Close();
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        /// <summary>
        /// 窗体KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegList_Load(object sender, EventArgs e)
        {
            //if (dgRegInfo!=null  && dgRegInfo.Rows.Count>0)
            //{
            //    dgRegInfo.CurrentCell = dgRegInfo[0, 0];
            //}
        }     

        /// <summary>
        /// 窗体KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgRegInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgRegInfo.CurrentCell != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //if (dgRegInfo.CurrentCell.RowIndex != 0)//因回车会自动跳到下一行，所以作处理
                    //{
                    //    dgRegInfo.CurrentCell = dgRegInfo[0, dgRegInfo.CurrentCell.RowIndex - 1];
                    //}
                    InvokeController("GetSelectPatlist");
                    this.Close();
                }
            }
        }
    }
}
