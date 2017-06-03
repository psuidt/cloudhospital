using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 入库单编号
    /// </summary>
    public partial class FrmSelectBillNo : BaseFormBusiness
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmSelectBillNo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCacel_Click(object sender, EventArgs e)
        {
            //InvokeController("Close", this);
            this.Close();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!RegexText(txtBillNo.Text))
            {
                MessageBoxEx.Show("只能输入数字");
                txtBillNo.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtBillNo.Text.Trim()))
            {
                InvokeController("ConvertDwOutFromDwIn", txtBillNo.Text.Trim());
            }
        }

        /// <summary>
        /// 验证字符合法性
        /// </summary>
        /// <param name="txt">字符</param>
        /// <returns>是否合法</returns>
        public bool RegexText(string txt)
        {
            return new Regex(@"^\d*$").IsMatch(txt);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmSelectBillNo_OpenWindowBefore(object sender, EventArgs e)
        {        
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmSelectBillNo_Load(object sender, EventArgs e)
        {
            this.txtBillNo.Text = string.Empty;
        }
    }
}
