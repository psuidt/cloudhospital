using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmSinglePaymentManage : BaseFormBusiness, ISinglePaymentManage
    {
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        SysLoginRight sysLoginRight;

        /// <summary>
        /// 0为预交金 1为结算
        /// </summary>
        private int iAccountType = 1;

        /// <summary>
        /// 操作员ID
        /// </summary>
        private int iStaffId = 0;

        /// <summary>
        /// 缴款ID
        /// </summary>
        private int iAccountId = 0;

        /// <summary>
        /// 发票总数
        /// </summary>
        private DataTable dtFPSum;

        /// <summary>
        /// 发票类型
        /// </summary>
        private DataTable dtFPClass;

        /// <summary>
        /// 缴款类型
        /// </summary>
        private DataTable dtAccountClass;

        /// <summary>
        /// 预交金数据
        /// </summary>
        private DataTable dtDepositList;

        /// <summary>
        /// 缴款数据
        /// </summary>
        private IP_Account curAccount;

        /// <summary>
        /// 缴款数据
        /// </summary>
        public IP_Account CurAccount
        {
            get
            {
                return curAccount;
            }

            set
            {
                curAccount = value;
            }
        }

        /// <summary>
        /// 缴款单报表数据
        /// </summary>
        private Dictionary<string, object> myDictionary;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmSinglePaymentManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">FrmSinglePaymentManage</param>
        /// <param name="e">事件参数</param>
        private void FrmSinglePaymentManage_OpenWindowBefore(object sender, EventArgs e)
        {
            myDictionary = new Dictionary<string, object>();
            sysLoginRight = (InvokeController("this") as AbstractController).LoginUserInfo;
            statDTime.Bdate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00"));
            statDTime.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetStaff");
            AccountRefresh();
            ucAccountTab1.InitUC();
            ucAccountTab1.InvokeController = InvokeController;
        }

        #region 按钮事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">btnQuery</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            AccountRefresh();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void AccountRefresh()
        {
            iAccountType = 1;
            iStaffId = 0;
            dtFPSum = null;
            dtFPClass = null;
            dtAccountClass = null;
            dtDepositList = null;
            CurAccount = null;
            ucAccountTab1.InitUC();
            btnPayMent.Enabled = false;
            btnPrint.Enabled = false;
            UnUpload.Nodes.Clear();
            Uploaded.Nodes.Clear();
            string sDTBegin = statDTime.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sDTEnd = statDTime.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            int iEmpId = ConvertExtend.ToInt32(tbcStaff.MemberValue, -1);

            InvokeController("GetPayInfoList", sDTBegin, sDTEnd, iEmpId);
        }

        /// <summary>
        /// 缴款
        /// </summary>
        /// <param name="sender">btnPayMent</param>
        /// <param name="e">事件参数</param>
        private void btnPayMent_Click(object sender, EventArgs e)
        {
            decimal dTotalFee = ucAccountTab1.DTotalFee;
            decimal dTotalPaymentFee = ucAccountTab1.DTotalPaymentFee;

            //如果成功 打印并刷新
            if ((bool)InvokeController("DoAccountPayment", iStaffId, iAccountType, dTotalFee, dTotalPaymentFee))
            {
                if (iAccountType == 1)
                {
                    AccountPrint();
                }
                else
                {
                    DepositPrint();
                }

                AccountRefresh();
            }
        }

        /// <summary>
        /// 打印缴款单
        /// </summary>
        /// <param name="sender">btnPrint</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (iAccountType == 0)
            {
                DepositPrint();
            }
            else
            {
                AccountPrint();
            }
        }

        /// <summary>
        /// 打印预交金缴款单
        /// </summary>
        private void DepositPrint()
        {
            myDictionary.Clear();
            if (CurAccount != null)
            {
                myDictionary.Add("Title", sysLoginRight.WorkName + "预交金缴款单");
                myDictionary.Add("ReceivBillNO", CurAccount.ReceivBillNO);
                myDictionary.Add("DateScope", CurAccount.LastDate.ToString("yyyy-MM-dd HH:mm:ss") + "至" + CurAccount.AccountDate.ToString("yyyy-MM-dd HH:mm:ss"));
                string invoiceScope = string.Empty;
                for (int i = 0; i < dtDepositList.Rows.Count; i++)
                {
                    invoiceScope += dtDepositList.Rows[i]["InvoiceNO"] + "  ";
                }

                myDictionary.Add("InvoiceScope", invoiceScope);
                myDictionary.Add("AccountOperator", sysLoginRight.EmpName);
                myDictionary.Add("TotalFee", ucAccountTab1.DTotalFee);// curAccount.TotalFee);
                myDictionary.Add("ShouldAccountFee", CurAccount.TotalFee);// curAccount.CashFee);

                InvokeController("DepositPrint", myDictionary, dtDepositList);
            }
            //住院预交金缴款表                
        }

        /// <summary>
        /// 打印缴款单
        /// </summary>
        private void AccountPrint()
        {
            myDictionary.Clear();
            if (CurAccount != null)
            {
                myDictionary.Add("Title", sysLoginRight.WorkName + "住院缴款单");
                myDictionary.Add("ReceivBillNO", CurAccount.ReceivBillNO);
                myDictionary.Add("DateScope", CurAccount.LastDate.ToString("yyyy-MM-dd HH:mm:ss") + "至" + CurAccount.AccountDate.ToString("yyyy-MM-dd HH:mm:ss"));
                string invoiceScope = string.Empty;
                for (int i = 0; i < dtFPSum.Rows.Count; i++)
                {
                    invoiceScope += dtFPSum.Rows[i]["Num"] + "  ";
                }

                myDictionary.Add("InvoiceScope", invoiceScope);
                myDictionary.Add("AccountOperator", sysLoginRight.EmpName);
                myDictionary.Add("RoundingFee", ucAccountTab1.DTotalRoundFee);//,curAccount.RoundingFee);
                myDictionary.Add("TotalFee", ucAccountTab1.DTotalFee);// curAccount.TotalFee);
                myDictionary.Add("ShouldAccountFee", CurAccount.TotalFee);// curAccount.CashFee);
                for (int i = 0; i < dtFPClass.Rows.Count; i++)
                {
                    myDictionary.Add(dtFPClass.Rows[i]["fpItemName"].ToString(), dtFPClass.Rows[i]["ItemFee"]);
                }

                for (int i = 0; i < dtAccountClass.Rows.Count - 1; i++)
                {
                    myDictionary.Add(dtAccountClass.Rows[i]["payname"].ToString(), dtAccountClass.Rows[i]["payname"]);
                }

                InvokeController("AccountPrint", myDictionary);
            }
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">btnClose</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        /// <summary>
        /// 缴款列表选中事件
        /// </summary>
        /// <param name="sender">TreePayInfoList</param>
        /// <param name="e">事件参数</param>
        private void TreePayInfoList_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            Node node = e.Node;
            if (node.ImageKey == "Child")
            {
                if (Convert.ToInt32(node.Tag) == 0)
                {
                    btnPayMent.Enabled = true;
                    btnPrint.Enabled = false;
                }
                else
                {
                    btnPayMent.Enabled = false;
                    btnPrint.Enabled = true;
                }

                if (node.DataKey != null)
                {
                    string[] sArray = node.DataKey.ToString().Split('|');
                    if (sArray.Length == 2)
                    {
                        iAccountId = Convert.ToInt32(node.Tag);
                        ucAccountTab1.AccountId = iAccountId;
                        iStaffId = Convert.ToInt32(sArray[0]);

                        if (sysLoginRight.EmpId != iStaffId)
                        {
                            btnPayMent.Enabled = false;
                        }

                        if (sArray[1].Trim().Contains("预交金"))
                        {
                            iAccountType = 0;
                            ucAccountTab1.TabSelectIndex = 1;
                            InvokeController("GetDepositList", iStaffId, iAccountId);
                        }
                        else
                        {
                            iAccountType = 1;
                            ucAccountTab1.TabSelectIndex = 0;
                            InvokeController("GetPayMentItem", iStaffId, iAccountId);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定Tree
        /// </summary>
        /// <param name="dtUnUpload">未缴款</param>
        /// <param name="dtUploaded">已缴款</param>
        public void BindPayInfoList(DataTable dtUnUpload, DataTable dtUploaded)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                #region 未缴款             

                DataView dvUnUpload = dtUnUpload.DefaultView;
                DataTable dtUnUploaded2 = dvUnUpload.ToTable(true, "EmpID", "NAME");
                foreach (DataRow drUnUploaded2 in dtUnUploaded2.Rows)
                {
                    Node node = new Node();
                    node.Text = drUnUploaded2["NAME"].ToString();
                    node.ImageKey = "Parent";
                    node.DataKey = drUnUploaded2["EmpID"].ToString();

                    foreach (DataRow drUnUploaded3 in dtUnUpload.Rows)
                    {
                        if (drUnUploaded3["NAME"].ToString().Equals(drUnUploaded2["NAME"]))
                        {
                            Node sNode = new Node();
                            sNode.Text = drUnUploaded3["AccountType"].ToString();
                            sNode.Tag = "0";
                            sNode.ImageKey = "Child";
                            sNode.DataKey = drUnUploaded3["EmpID"].ToString() + "|" + drUnUploaded3["AccountType"].ToString();

                            node.Nodes.Add(sNode);
                        }
                    }

                    UnUpload.Nodes.Add(node);
                }
                #endregion

                #region 已缴款
                DataView dv = dtUploaded.DefaultView;
                DataTable dtUploaded2 = dv.ToTable(true, "EmpID", "NAME");
                DataTable dtUploaded3 = dv.ToTable(true, "EmpID", "NAME", "AccountType");
                foreach (DataRow drUploaded2 in dtUploaded2.Rows)
                {
                    Node node = new Node();
                    node.Text = drUploaded2["NAME"].ToString();
                    node.ImageKey = "Parent";
                    node.DataKey = drUploaded2["EmpID"].ToString();

                    foreach (DataRow drUploaded3 in dtUploaded3.Rows)
                    {
                        if (drUploaded3["NAME"].ToString().Equals(drUploaded2["NAME"]))
                        {
                            Node sNode = new Node();
                            sNode.Text = drUploaded3["AccountType"].ToString();
                            sNode.Tag = drUploaded3["EmpID"].ToString();
                            sNode.ImageKey = "Parent";
                            sNode.DataKey = drUploaded3["EmpID"].ToString() + "|" + drUploaded3["AccountType"].ToString();
                            foreach (DataRow drUploaded in dtUploaded.Rows)
                            {
                                if (drUploaded["NAME"].ToString().Equals(drUploaded3["NAME"]) && drUploaded["AccountType"].ToString().Equals(drUploaded3["AccountType"]))
                                {
                                    Node cNode = new Node();
                                    cNode.Text = drUploaded["AccountDate"].ToString();
                                    cNode.Tag = drUploaded["AccountID"].ToString();
                                    cNode.ImageKey = "Child";
                                    cNode.DataKey = drUploaded["EmpID"].ToString() + "|" + drUploaded["AccountType"].ToString();
                                    sNode.Nodes.Add(cNode);
                                }
                            }

                            node.Nodes.Add(sNode);
                        }
                    }

                    Uploaded.Nodes.Add(node);
                }
                #endregion

                TreePayInfoList.ExpandAll();
            }
            catch (Exception e)
            {
                InvokeController("MessageShow", e.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtStaff">收费员列表</param>
        public void BindStaffInfo(DataTable dtStaff)
        {
            tbcStaff.CardColumn = "EmpId|编号|50,Name|名称|auto";
            tbcStaff.DisplayField = "Name";
            tbcStaff.MemberField = "EmpId";
            tbcStaff.QueryFields = new string[] { "EmpId", "Name", "Pym", "Wbm" };
            tbcStaff.IsShowSeq = false;
            tbcStaff.ShowCardDataSource = dtStaff;
            if (tbcStaff.ShowCardDataSource.Rows.Count > 0 && dtStaff.Select("EmpId=" + sysLoginRight.EmpId).Length > 0)
            {
                tbcStaff.MemberValue = sysLoginRight.EmpId;
            }
        }

        /// <summary>
        /// 绑定三个数据框的数据
        /// </summary>
        /// <param name="dtFPSum">发票总数</param>
        /// <param name="dtFPClass">发票分类</param>
        /// <param name="dtAccountClass">账目分类</param>
        public void ShowPayMentItem(DataTable dtFPSum, DataTable dtFPClass, DataTable dtAccountClass)
        {
            this.dtFPSum = dtFPSum;
            this.dtFPClass = dtFPClass;
            this.dtAccountClass = dtAccountClass;
            ucAccountTab1.DTInvCount = dtFPSum;
            ucAccountTab1.DTInvoiceDetail = dtFPClass;
            ucAccountTab1.DTAccount = dtAccountClass;
        }

        /// <summary>
        /// 绑定预交金数据
        /// </summary>
        /// <param name="dtDepositList">预交金数据列表</param>
        public void ShowDepositItem(DataTable dtDepositList)
        {
            this.dtDepositList = dtDepositList;
            ucAccountTab1.DTDepositList = dtDepositList;
        }
    }
}