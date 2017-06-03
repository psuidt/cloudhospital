using System;
using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmDeptOrderCheck : BaseFormBusiness, IDeptOrderCheck
    {
        /// <summary>
        /// 进度条
        /// </summary>
        public int Progress
        {
            set { progress.Value = value; }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        private List<string> txtError = new List<string>();

        /// <summary>
        /// 错误消息
        /// </summary>
        public List<string> RTxtError
        {
            set
            {
                txtError.AddRange(value);
                foreach (string s in txtError)
                {
                    rTxtError.Text += s + "\n";
                }
            }
        }

        /// <summary>
        /// 病人列表
        /// </summary>
        private List<int> patientList;

        /// <summary>
        /// 病人列表
        /// </summary>
        public List<int> iPatientList
        {
            get
            {
                return patientList;
            }

            set
            {
                patientList = value;
            }
        }

        /// <summary>
        /// 发送之后刷新界面
        /// </summary>
        /// <param name="iType">0- 发送完成无错误；1-发送完成，有错误 2-发送失败</param>
        public void FreshSendState(int iType)
        {
            switch (iType)
            {
                case 0:
                    btnCancel.Enabled = true;
                    break;
                case 1:
                    btnCancel.Enabled = true;
                    btnLook.Enabled = true;
                    break;
                case 2:
                    btnCancel.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        public void InitWindow()
        {
            endDate.Value = DateTime.Now;
            progress.Value = 0;
            txtError.Clear();
            rTxtError.Text = string.Empty;
            panelEx2.Visible = true;
            cbAll.Checked = true;
            btnSend.Enabled = true;
            btnLook.Enabled = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDeptOrderCheck()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender">btnSend</param>
        /// <param name="e">事件参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            int iOrderCategory = (cbLongOrder.Checked == cbTempOrder.Checked) ? -1 : (cbLongOrder.Checked ? 0 : 1);
            bool bAccountCategory = cbLong.Checked;
            bool bBed = cbBed.Checked;
            DateTime enddate = endDate.Value;
            btnSend.Enabled = false;
            btnCancel.Enabled = false;

            InvokeController("DoDeptOrderCheck", iOrderCategory, bAccountCategory, bBed, enddate);
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">btnCancel</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择全部
        /// </summary>
        /// <param name="sender">cdAll</param>
        /// <param name="e">事件参数</param>
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            cbLongOrder.Checked = cbAll.Checked;
            cbTempOrder.Checked = cbAll.Checked;
            cbLong.Checked = cbAll.Checked;
            cbBed.Checked = cbAll.Checked;
        }

        /// <summary>
        /// 查看结果
        /// </summary>
        /// <param name="sender">btnLook</param>
        /// <param name="e">事件参数</param>
        private void btnLook_Click(object sender, EventArgs e)
        {
            panelEx2.Visible = false;
        }
    }
}
