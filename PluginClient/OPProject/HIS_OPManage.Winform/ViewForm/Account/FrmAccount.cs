using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    /// <summary>
    /// 门诊缴款界面
    /// </summary>
    public partial class FrmAccount : BaseFormBusiness,IFrmAccount
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAccount()
        {
            InitializeComponent();          
        }

        #region 接口参数
        /// <summary>
        /// 缴款票据详细信息
        /// </summary>
        private DataTable dtaccountinvoicedata;

        /// <summary>
        /// 缴款票据详细信息
        /// </summary>
        public DataTable dtAccountInvoiceData
        {
            get
            {
                return dtaccountinvoicedata;
            }

            set
            {
                dtaccountinvoicedata = value;
            }
        }

        /// <summary>
        /// 缴款支付详细信息
        /// </summary>
        private DataTable dtaccountpaymentdata;

        /// <summary>
        /// 缴款支付详细信息
        /// </summary>
        public DataTable dtAccountPaymentData
        {
            get
            {
                return dtaccountpaymentdata;
            }

            set
            {
                dtaccountpaymentdata = value;
            }
        }

        /// <summary>
        /// 缴款项目详细信息
        /// </summary>
        private DataTable dtaccountitemdata;

        /// <summary>
        /// 缴款项目详细信息
        /// </summary>
        public DataTable dtAccountItemData
        {
            get
            {
                return dtaccountitemdata;
            }

            set
            {
                dtaccountitemdata = value;
            }
        }

        /// <summary>
        /// 会员充值明细
        /// </summary>
        private DataTable dtaccountItemDataME;

        /// <summary>
        /// 会员充值明细
        /// </summary>
        public DataTable dtAccountItemDataME
        {
            get
            {
                return dtaccountItemDataME;
            }

            set
            {
                dtaccountItemDataME = value;
            }
        }

        /// <summary>
        /// 缴款大写金额
        /// </summary>
        /// <param name="dxFee">大写金额</param>
        public void SetAccountDxTotalFee(string dxFee)
        {
            txtTotalDx.Text = dxFee;
        }

        /// <summary>
        /// 挂号费用
        /// </summary>
        public string regFee
        {
            get
            {
                return txtRegFee.Text.Trim();
            }

            set
            {
                txtRegFee.Text = value;
            }
        }

        /// <summary>
        /// 收费费用
        /// </summary>
        public string balanceFee
        {
            get
            {
                return txtBalanceFee.Text.Trim();
            }

            set
            {
                txtBalanceFee.Text = value;
            }
        }

        /// <summary>
        /// 查询开始日期
        /// </summary>
        public DateTime bdate
        {
            get
            {
                return Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            set
            {
                sdtDate.Bdate.Value = value;
            }
        }

        /// <summary>
        /// 查询结束日期
        /// </summary>
        public DateTime edate
        {
            get
            {
                return Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            set
            {
                sdtDate.Edate.Value = value;
            }
        }

        /// <summary>
        /// 当前缴款的人员ID
        /// </summary>
        public int GetQueryEmpID
        {
            get
            {
                return Convert.ToInt32(txtEmp.MemberValue);
            }

            set
            {
                txtEmp.MemberValue = value;
            }
        }

        /// <summary>
        /// 当前缴款Id
        /// </summary>
        private int queryaccountid = -1;

        /// <summary>
        /// 当前缴款Id
        /// </summary>
        public int QueryAccountId
        {
            get
            {
                return queryaccountid;
            }

            set
            {
                queryaccountid = value;
            }
        }
        #endregion

        /// <summary>
        /// 缴款列表绑定
        /// </summary>
        /// <param name="notAccountList">未缴款列表</param>
        /// <param name="historyAccountList">已经缴款列表</param>
        public void BindTree(List<OP_Account> notAccountList, List<OP_Account> historyAccountList)
        {
            btnAccount.Enabled = false;
            btnPrint.Enabled = false;

            TreeAccount.Nodes.Clear();
            #region 未交款
            Node notAccountPnode = new Node();
            notAccountPnode.Text ="未缴款";
            notAccountPnode.Name ="未缴款";
            notAccountPnode.Tag = null;

            List<OP_Account> opAccountNo = notAccountList.Where((x, i) => notAccountList.FindIndex(z => z.AccountEmpID == x.AccountEmpID) == i).ToList();
            foreach (OP_Account notAccount in opAccountNo)
            {
                Node node = new Node();
                node.Text = notAccount.AccountEmpName;
                node.ImageKey = "Parent";
                node.DataKey = notAccount.AccountEmpID;
                //node.Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                node.Tag = null;
                notAccountPnode.Nodes.Add(node);

                List<OP_Account> opAccountNoDetail = notAccountList.Where(p=>p.AccountEmpID == notAccount.AccountEmpID ).ToList();
                foreach (OP_Account op in opAccountNoDetail)
                {
                    Node sNode = new Node();
                    sNode.Text = op.AccountType==0?"挂号收费":"会员充值";
                    sNode.ImageKey = "Child";
                    sNode.DataKey = op.AccountEmpID + "|" + op.AccountType;
                    sNode.Tag = op;
                    node.Nodes.Add(sNode);
                }                
            }

            TreeAccount.Nodes.Add(notAccountPnode);
            notAccountPnode.ExpandAll();
            #endregion

            #region 已交款
            Node historyAccountPnode = new Node();
            historyAccountPnode.Text = "已缴款";
            historyAccountPnode.Name = "已缴款";
            historyAccountPnode.Tag = null;
            List<OP_Account> opAccountYes = historyAccountList.Where((x, i) => historyAccountList.FindIndex(z => z.AccountEmpID == x.AccountEmpID) == i).ToList();
            foreach (OP_Account hasAccount in opAccountYes)
            {
                Node node = new Node();
                node.Text = hasAccount.AccountEmpName;
                node.ImageKey = "Parent";
                node.DataKey = hasAccount.AccountEmpID;

                List<OP_Account> opAccountYesType = historyAccountList.Where((x, i) => historyAccountList.FindIndex(z => z.AccountEmpID == x.AccountEmpID && z.AccountType == x.AccountType) == i).ToList();
                foreach (OP_Account hasAccountTy in opAccountYesType)
                {
                    Node sNode = new Node();
                    sNode.Text = hasAccountTy.AccountType == 0 ? "挂号收费" : "会员充值";
                    sNode.Tag = null;
                    sNode.ImageKey = "Parent";
                    sNode.DataKey = hasAccountTy.AccountEmpID + "|" + hasAccountTy.AccountType;

                    List<OP_Account> opAccountYesDetail = historyAccountList.Where(p => p.AccountEmpID == hasAccountTy.AccountEmpID && p.AccountType == hasAccountTy.AccountType).ToList();
                    foreach (OP_Account op in opAccountYesDetail)
                    {
                        Node cNode = new Node();
                        cNode.Text = op.AccountDate.ToString("yyyy-MM-dd HH:mm:ss");
                        cNode.Tag = op;
                        cNode.ImageKey = "Child";
                        cNode.DataKey = op.AccountEmpID + "|" + op.AccountType;
                        sNode.Nodes.Add(cNode);
                    }

                    node.Nodes.Add(sNode);
                }

                historyAccountPnode.Nodes.Add(node);
            }
            //foreach (OP_Account account in historyAccountList)
            //{
            //    Node node = new Node();
            //    node.Text = account.AccountDate.ToString("yyyy-MM-dd HH:mm:ss");
            //    node.Name = account.AccountDate.ToString("yyyy-MM-dd HH:mm:ss");
            //    node.Tag = account;                
            //    historyAccountPnode.Nodes.Add(node);
            //}
            TreeAccount.Nodes.Add(historyAccountPnode);
            #endregion

            //默认选择次序  未交款->已交款->赋空
            if (notAccountList.Count > 0)
            {
                CurAccount = notAccountList[0] as OP_Account;

                //窗体变换，个人缴款和门诊缴款查询
                InvokeController("ChangeValue", frmName);

                //获取结账各项数据
                InvokeController("GetAccountData");
                TreeAccount.SelectedNode = TreeAccount.Nodes[0].Nodes[0].Nodes[0];
                btnAccount.Enabled = true;
                btnPrint.Enabled = false;
            }
            else if (historyAccountList.Count > 0)
            {
                CurAccount = historyAccountList[0] as OP_Account;

                //窗体变换，个人缴款和门诊缴款查询
                InvokeController("ChangeValue", frmName);

                //获取结账各项数据
                InvokeController("GetAccountData");
                TreeAccount.SelectedNode = TreeAccount.Nodes[1].Nodes[0].Nodes[0];
                historyAccountPnode.ExpandAll();
                btnAccount.Enabled = false;
                btnPrint.Enabled = true;
            }
            else
            {
                CurAccount = new OP_Account();
                BindAccountData(new DataTable(), new DataTable(), new DataTable());
                btnAccount.Enabled = false;
                btnPrint.Enabled = false;
                tabControl1.SelectedTabIndex = 0;
            }
        }

        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtCashier">收费员数据</param>
        public void loadCashier(DataTable dtCashier)
        {
            InvokeController("ChangeValue", frmName);
            txtEmp.DisplayField = "Name";
            txtEmp.MemberField = "EmpID";
            txtEmp.CardColumn = "Name|姓名|auto";
            txtEmp.QueryFieldsString = "Name,pym,wbm";
            txtEmp.ShowCardHeight = 130;
            txtEmp.ShowCardWidth = 160;
            txtEmp.ShowCardDataSource = dtCashier;            
        }

        /// <summary>
        /// 当前缴款对象
        /// </summary>
        private OP_Account curAccount;

        /// <summary>
        /// 当前缴款对象
        /// </summary>
        public OP_Account CurAccount
        {
            get
            {
                return curAccount;
            }

            set
            {
                curAccount = value;
                txtPromFee.Text = curAccount.PromFee.ToString("0.00");
                txtCashFee.Text = curAccount.CashFee.ToString("0.00");
                txtPosFee.Text = curAccount.PosFee.ToString("0.00");
                txtTotalXx.Text = curAccount.TotalFee.ToString("0.00");
                txtRoundFee.Text = curAccount.RoundingFee.ToString("0.00");
                txtShoudAccount.Text = curAccount.CashFee.ToString("0.00");
            }
        }

        /// <summary>
        /// 绑定结账数据
        /// </summary>
        /// <param name="dtInvoiceData">发票数据</param>
        /// <param name="dtPaymentData">支付方式数据</param>
        /// <param name="dtItemData">项目分类数据</param>
        public void BindAccountData(DataTable dtInvoiceData, DataTable dtPaymentData, DataTable dtItemData)
        {
            tabControl1.SelectedTabIndex = 0;
            InvokeController("ChangeValue", frmName);
            dgInvoiceInfo.DataSource = dtInvoiceData;
            dgItemInfo.DataSource = dtItemData;
            dgPayInfo.DataSource = dtPaymentData;
            SetGridColor();
        }

        /// <summary>
        /// 绑定充值数据
        /// </summary>
        /// <param name="dtME">充值数据</param>
        public void BindAccountDataME(DataTable dtME)
        {
            tabControl1.SelectedTabIndex = 1;
            if (dtME != null && dtME.Rows.Count > 0)
            {
                string totalFee = dtME.Compute("sum(Money)", string.Empty).ToString();
                string cashFee = dtME.Compute("sum(Money)", " PayType = '现金'").ToString();

                DataRow dr = dtME.NewRow();
                dr["RechargeCode"] = "合计";
                dr["Money"] = totalFee;
                dtME.Rows.Add(dr);

                dgvMEList.DataSource = dtME;
                txtTotalDe.Text = totalFee;
                txtDeCash.Text = cashFee;
                CurAccount.TotalFee = Convert.ToDecimal(totalFee);
                CurAccount.CashFee = Convert.ToDecimal(cashFee);
                CurAccount.PosFee = CurAccount.TotalFee - CurAccount.CashFee;
            }
            else
            {
                dgvMEList.DataSource = dtME;
                txtTotalDe.Text = "0";
                txtDeCash.Text = "0";
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in dgItemInfo.Rows)
            {
                string status_flag = row.Cells["fpItemName"].Value.ToString();
                Color foreColor = Color.Black;
                if (status_flag == "合计")
                {
                    foreColor = Color.Blue;
                }

                dgItemInfo.SetRowColor(row.Index, foreColor, true);
            }

            foreach (DataGridViewRow row in dgPayInfo.Rows)
            {
                string status_flag = row.Cells["paymentname"].Value.ToString();
                Color foreColor = Color.Black;
                if (status_flag == "合计")
                {
                    foreColor = Color.Blue;
                }

                dgPayInfo.SetRowColor(row.Index, foreColor, true);
            }
        }

        /// <summary>
        /// 窗体打开前初始化数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmAccount_OpenWindowBefore(object sender, EventArgs e)
        {
            tabControl1.SelectedTabIndex = 1;
            
            InvokeController("ChangeValue", frmName);
            if (QueryAccountId < 0)
            {
                sdtDate.Bdate.Value = DateTime.Now.AddDays(-3);
                sdtDate.Edate.Value = DateTime.Now;
                InvokeController("AccountInit", frmName);
            }
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnStatic_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            InvokeController("AccountQuery");
        }

        /// <summary>
        /// 缴款树点击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TreeAccount_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            if (!e.Node.HasChildNodes && e.Node.Tag!=null)
            {
                OP_Account curAccount = (OP_Account)e.Node.Tag;
                if (curAccount.AccountFlag == 0)
                {
                    btnAccount.Enabled = true;
                    btnPrint.Enabled = false;
                }
                else
                {
                    btnAccount.Enabled = false;
                    btnPrint.Enabled = true;
                }
                //tabControl1.SelectedTabIndex = curAccount.AccountType;
                CurAccount = curAccount;
                InvokeController("GetAccountData");
            }
        }

        /// <summary>
        /// 票据网格点击查看票据明细
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgInvoiceInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            if (dgInvoiceInfo.CurrentCell != null && e.RowIndex>-1)
            {
                if (e.ColumnIndex == invoiceAllcount.Index || e.ColumnIndex == refundinvoicecount.Index)
                {
                    if (Convert.ToInt32(dgInvoiceInfo[dgInvoiceInfo.CurrentCell.ColumnIndex, dgInvoiceInfo.CurrentCell.RowIndex].Value) != 0)
                    {
                        int invoiceID = Convert.ToInt32(dgInvoiceInfo["InvoiceID", e.RowIndex].Value);
                        int invoiceType = 0;

                        //退票
                        if (e.ColumnIndex == refundinvoicecount.Index)
                        {
                            invoiceType = 1;
                        }

                        InvokeController("GetInvoiceDetail", invoiceID, invoiceType);
                    }
                }
            }
        }

        /// <summary>
        /// 缴款按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnAccount_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            if ((bool)InvokeController("SubmitAccount"))
            {
                //1调用打印
                InvokeController("AccountPrint");

                //2刷新界面
                InvokeController("AccountQuery");               
            }
        }

        /// <summary>
        /// 刷新按钮事件，界面数据刷新
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            if (CurAccount != null)
            {
                InvokeController("GetAccountData");
            }
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmAccount_Load(object sender, EventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            if (QueryAccountId>0)
            {
                panelEx2.Visible = false;
                panelEx1.Visible = false;
                this.Width = 950;
                this.Height = 480;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Text = "缴款明细";
                this.StartPosition = FormStartPosition.CenterScreen;
                InvokeController("AccountInit", frmName);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            QueryAccountId = 0;
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeValue", frmName);
            InvokeController("AccountPrint");
        }

        /// <summary>
        /// Tab切换事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            tabControl1.TabsVisible = false;
            tabControl1.SelectedTab.Visible = true;
        }
    }
}
