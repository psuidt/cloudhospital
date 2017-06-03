using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmGetGift  : BaseFormBusiness, IFrmGetGift
    {
        /// <summary>
        /// 礼品id
        /// </summary>
        private int giftID;

        /// <summary>
        /// Gets or sets 礼品id
        /// </summary>
        /// <value>礼品id</value>
        public int GiftID
        {
            get
            {
                if (dgGiftForWork.CurrentCell!=null)
                {
                    int rowIndex = dgGiftForWork.CurrentCell.RowIndex;
                    giftID = Convert.ToInt16((dgGiftForWork.DataSource as DataTable).Rows[rowIndex]["GiftID"]);
                    return giftID;
                }
                else
                {
                    return giftID;
                }               
            }

            set
            {
                giftID = value;
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
        /// 帐户代码
        /// </summary>
        private string accountCode;

        /// <summary>
        /// Gets or sets 帐户代码
        /// </summary>
        /// <value>帐户代码</value>
        public string AccountCode
        {
            get
            {          
                return accountCode;                 
            }

            set
            {
                accountCode = value;
            }
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmGetGift" /> class.
        /// </summary>
        public FrmGetGift()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmGetGift_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(dgGiftForWork);
            bindGridSelectIndex(dgGift);
            BindCardType();
            BindGiftGrid(Convert.ToInt16(cbbCardType.SelectedValue), string.Empty);
            BindGiftGridForWork(Convert.ToInt16(cbbCardType.SelectedValue));
        }

        /// <summary>
        /// 绑定卡类型
        /// </summary>
        public void BindCardType()
        {
            DataTable dt = (DataTable)InvokeController("BindCardTypeInfo");
            dt.Rows.RemoveAt(0);

            cbbCardType.DisplayMember = "CardTypeName";
            cbbCardType.ValueMember = "CardTypeID";
            cbbCardType.DataSource = dt;
        }

        /// <summary>
        /// 绑定礼品网格
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        public void BindGiftGridForWork(int cardTypeID)
        {
            DataTable dt = (DataTable)InvokeController("GetGiftForWorkID", cardTypeID);
            dgGiftForWork.DataSource = dt;
        }

        /// <summary>
        /// 绑定礼品网格
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="code">编码</param>
        public void BindGiftGrid(int cardTypeID,string code)
        {
            DataTable dt = (DataTable)InvokeController("GetExchargeInfo", cardTypeID, code);
            dgGift.DataSource = dt;
            labMember.Text = string.Empty;
            labSource.Text = string.Empty;

            if (dt.Rows.Count>=0)
            {
                DataTable dtMemInfo = (DataTable)InvokeController("GetMemberInfo", Convert.ToInt16(cbbCardType.SelectedValue), txtCardNO.Text.Trim());
                if (dtMemInfo.Rows.Count > 0)
                {
                    AccountID = Convert.ToInt16(dtMemInfo.Rows[0]["AccountID"]);
                    txtScore.Text = Convert.ToString(dtMemInfo.Rows[0]["Score"]);
                    AccountCode = Convert.ToString(dtMemInfo.Rows[0]["CardNO"]);
                    AccountID = Convert.ToInt16(dtMemInfo.Rows[0]["AccountID"]);
                    labMember.Text = Convert.ToString(dtMemInfo.Rows[0]["MemberName"]);
                    labSource.Text = txtScore.Text;
                }
                else
                {
                    AccountID = 0;
                    txtScore.Text = "0";
                }
            }            
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            BindGiftGrid(Convert.ToInt16(cbbCardType.SelectedValue), txtCardNO.Text.Trim());
            BindGiftGridForWork(Convert.ToInt16(cbbCardType.SelectedValue));           
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgGift_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgGift.CurrentCell!=null)
            {
                int rowIndex= dgGift.CurrentCell.RowIndex;
                DataTable dt = dgGift.DataSource as DataTable;
                string scroe = Convert.ToString(dt.Rows[rowIndex]["AccountScore"]);
                txtScore.Text = scroe;
                AccountCode= Convert.ToString(dt.Rows[rowIndex]["CardNO"]);
                AccountID = Convert.ToInt16(dt.Rows[rowIndex]["AccountID"]);
            }
            else
            {
                DataTable dtMemInfo = (DataTable)InvokeController("GetMemberInfo", Convert.ToInt16(cbbCardType.SelectedValue), txtCardNO.Text.Trim());
                if (dtMemInfo.Rows.Count > 0)
                {
                    AccountID = Convert.ToInt16(dtMemInfo.Rows[0]["AccountID"]);
                    txtScore.Text = Convert.ToString(dtMemInfo.Rows[0]["Score"]);
                }
                else
                {
                    AccountID = 0;
                    txtScore.Text = "0";
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void intGift_ValueChanged(object sender, EventArgs e)
        {
            if (dgGiftForWork.CurrentCell!=null)
            {
                int rowIndex = dgGiftForWork.CurrentCell.RowIndex;
                DataTable dt = dgGiftForWork.DataSource as DataTable;

                int score = Convert.ToInt16(dt.Rows[rowIndex]["score"]);

                txtNewScore.Text = (score* intGift.Value).ToString();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void DgGiftForWork_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgGiftForWork.CurrentCell != null)
            {
                int rowIndex = dgGiftForWork.CurrentCell.RowIndex;
                DataTable dt = dgGiftForWork.DataSource as DataTable;

                int score = Convert.ToInt16(dt.Rows[rowIndex]["score"]);

                txtNewScore.Text = (score * intGift.Value).ToString();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnExchange_Click(object sender, EventArgs e)
        {
            if (AccountID==0)
            {
                MessageBoxEx.Show("没有选定要兑换的帐户！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnQuery.Focus();
                return;
            }

            if ((bool)InvokeController("CheckFlag", Convert.ToInt16(cbbCardType.SelectedValue), AccountCode)==false)
            {
                MessageBoxEx.Show("要兑换礼品的会员资格或帐户处于停用状态，请修改成启用再兑换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                intGift.Focus();
                return;
            }

            if (Convert.ToInt16(txtNewScore.Text) > Convert.ToInt16(txtScore.Text))
            {
                MessageBoxEx.Show(" 此次兑换所需积分大于帐户当前积分，请调整数量！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                intGift.Focus();
                return;
            }

            //保存礼品兑换信息
            int accountScore = Convert.ToInt16(txtScore.Text) - Convert.ToInt16(txtNewScore.Text); //帐户剩余积分
            int res = (int)InvokeController("SaveExchange", AccountID, accountScore, GiftID,intGift.Value, Convert.ToInt16(txtNewScore.Text));

            if (res>0)
            {
                BindGiftGrid(Convert.ToInt16(cbbCardType.SelectedValue), txtCardNO.Text.Trim());
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnGift_Click(object sender, EventArgs e)
        {
            BindGiftGridForWork(Convert.ToInt16(cbbCardType.SelectedValue));
        }
    }
}
