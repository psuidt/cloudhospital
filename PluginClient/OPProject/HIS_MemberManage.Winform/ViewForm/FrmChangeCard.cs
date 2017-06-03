using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmChangeCard : BaseFormBusiness, IFrmChangeCard
    {
        #region 接口实现 
        /// <summary>
        /// 会员名称
        /// </summary>
        private string memberName;

        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        ///  <value>会员名称</value>
        public string MemberName
        {
            get
            {
                return memberName;
            }

            set
            {
                memberName = value;
            }
        }

        /// <summary>
        /// 帐户类型名称
        /// </summary>
        private string accountTypeName;

        /// <summary>
        /// Gets or sets 帐户类型名称
        /// </summary>
        /// <value>帐户类型名称</value>
        public string AccountTypeName
        {
            get
            {
                return accountTypeName;
            }

            set
            {
                accountTypeName = value;
            }
        }

        /// <summary>
        /// 原卡号
        /// </summary>
        private string oldCardNO;

        /// <summary>
        /// Gets or sets 原卡号
        /// </summary>
        /// <value>原卡号</value>
        public string OldCardNO
        {
            get
            {
                return oldCardNO;
            }

            set
            {
                oldCardNO = value;
            }
        }

        /// <summary>
        /// 会员id
        /// </summary>
        private int memberID;

        /// <summary>
        /// Gets or sets 会员id
        /// </summary>
        /// <value>会员id</value>
        public int MemberID
        {
            get
            {
                return memberID;
            }

            set
            {
                memberID = value;
            }
        }

        /// <summary>
        /// 帐户id
        /// </summary>
        private int accountID;

        /// <summary>
        /// Gets or sets 帐户id
        /// </summary>
        /// <value>帐户id</value>
        public int AccountID
        {
            get
            {
                return accountID;
            }

            set
            {
                accountID = value;
            }
        }

        /// <summary>
        /// 帐户类型id
        /// </summary>
        private int accountTypeID;

        /// <summary>
        /// Gets or sets 帐户类型id
        /// </summary>
        /// <value>帐户类型id</value>
        public int AccountTypeID
        {
            get
            {
                return accountTypeID;
            }

            set
            {
                accountTypeID = value;
            }
        }

        /// <summary>
        /// 设置老卡
        /// </summary>
        /// <param name="code">卡号</param>
        public void SetOldCard(string code)
        {
            txtOldCard.Text = code;
            txtNewCard.Text = string.Empty;
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        public void BindList()
        {
            DataTable dt = InvokeController("GetChangeCardList", AccountID) as DataTable;
            dgChargeList.DataSource = dt;
        }

        #endregion

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmChangeCard" /> class.
        /// </summary>
        public FrmChangeCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmChangeCard_Load(object sender, EventArgs e)
        {
            txtMemberName.Text = MemberName;
            txtType.Text = AccountTypeName;
            txtOldCard.Text = OldCardNO;
            txtNewCard.Text = string.Empty;

            txtAmount.Text = string.Format(Convert.ToString(InvokeController("GetSystemConfig")), "0.00");

            //获取换卡信息列表
            DataTable dt = InvokeController("GetChangeCardList", AccountID) as DataTable;
            dgChargeList.DataSource = dt;
            cbCash.Checked = true;
            txtNewCard.Focus();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewCard.Text)==true)
            {
                MessageBoxEx.Show( "卡号不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isCheck = (bool)InvokeController("CheckCardNOForEdit", AccountID, AccountTypeID, txtNewCard.Text.Trim());

            if (isCheck == false)
            {
                MessageBoxEx.Show("卡号：" + txtNewCard.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ME_ChangeCardList list = new ME_ChangeCardList();
            list.AccountID = AccountID;
            list.Amount = Convert.ToDecimal(txtAmount.Text);
            list.NewCardNO = txtNewCard.Text;
            list.OldCardNO= txtOldCard.Text;
            list.OperateDate = System.DateTime.Now;

            int payType = 0;
            if (cbCash.Checked == true)
            {
                payType = 1;
            }

            if (cbPOS.Checked == true)
            {
                payType = 2;
            }

            InvokeController("SaveChangeCard", list, payType,MemberID);
        }
    }
}
