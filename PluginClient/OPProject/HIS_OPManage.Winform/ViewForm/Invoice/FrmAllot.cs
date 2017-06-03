using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmAllot : BaseFormBusiness,IFrmAllot
    {
        /// <summary>
        /// 操作员ID
        /// </summary>
        int opempid;

        /// <summary>
        /// 是否新增成功
        /// </summary>
        private bool isAddok;

        /// <summary>
        /// 是否新增成功
        /// </summary>
        public bool isAddOK
        {
            get
            {
                return isAddok;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAllot()
        {
            InitializeComponent();
            isAddok = false;
        }

        /// <summary>
        /// 当前操作的票据对象
        /// </summary>
        private HIS_Entity.BasicData.Basic_Invoice currInvoice;

        /// <summary>
        /// 当前操作的票据对象
        /// </summary>
        public HIS_Entity.BasicData.Basic_Invoice CurrInvoice
        {
            get
            {
                currInvoice.EmpID = Convert.ToInt32(txtEmp.MemberValue);
                currInvoice.InvoiceType = txtInvoiceType.SelectedIndex;
                currInvoice.PerfChar = txtPerfChar.Text.Trim();
                currInvoice.StartNO = Convert.ToInt32(txtStartNO.Text.Trim());
                currInvoice.EndNO = Convert.ToInt32(txtEndNO.Text.Trim());
                currInvoice.AllotEmpID = opempid;
                currInvoice.Status = 2;
                currInvoice.CurrentNO = currInvoice.StartNO;
                return currInvoice;
            }
        }

        /// <summary>
        ///  获取收费员列表和设置当前操作员ID
        /// </summary>
        /// <param name="dt">收费员列表</param>
        /// <param name="opempid">当前操作员ID</param>
        public void LoadAllotView(DataTable dt,int opempid)
        {
            txtEmp.DisplayField = "Name";
            txtEmp.MemberField = "EmpID";
            txtEmp.CardColumn = "Name|姓名|auto";
            txtEmp.QueryFieldsString = "Name,pym,wbm";
            txtEmp.ShowCardHeight = 130;
            txtEmp.ShowCardWidth = 160;
            txtEmp.ShowCardDataSource = dt;
            this.opempid = opempid;
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtEmp.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("领用人不能为空!");
                txtEmp.Focus();
                return;
            }          
            else if (txtPerfChar.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请输入票据前缀");
                txtPerfChar.Focus();
                return;
            }
            else if (txtStartNO.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("开始号段不能为空");
                txtStartNO.Focus();
                return;
            }
            else if (txtEndNO.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("结束号段不能为空");
                txtEndNO.Focus();
                return;
            }

            try
            {
                if (txtPerfChar.Text.Trim().Length > 5)
                {
                    MessageBoxEx.Show("前缀长度超过5个字符");
                    txtPerfChar.Focus();
                    return;
                }

                int start = Convert.ToInt32(txtStartNO.Text);
                int end = Convert.ToInt32(txtEndNO.Text);
                string perfChar = txtPerfChar.Text.Trim().ToUpper();
                if (end < start)
                {
                    MessageBoxEx.Show("结束号不能小于开始号", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEndNO.Focus();
                    return;
                }

                if ((bool)InvokeController("CheckInvoiceExsist",txtInvoiceType.SelectedIndex,perfChar, start, end))
                {
                    if ((bool)InvokeController("SaveNewAllot"))
                    {
                        isAddok = true;
                        this.Close();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void FrmAllot_Load(object sender, EventArgs e)
        {
            txtInvoiceType.SelectedIndex = 0;
            HIS_Entity.BasicData.Basic_Invoice invoice = new HIS_Entity.BasicData.Basic_Invoice();
            currInvoice = invoice;
             txtEmp.MemberValue=string.Empty;           
            txtPerfChar.Text=string.Empty;
            txtStartNO.Text=string.Empty;
            txtEndNO.Text=string.Empty;
            isAddok = false;       
        }

        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void FrmAllot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
