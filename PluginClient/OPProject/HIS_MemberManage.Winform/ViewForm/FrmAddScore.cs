using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmAddScore : BaseFormBusiness, IFrmAddScore
    {
        #region  接口实现
        /// <summary>
        /// 现金
        /// </summary>
        private int cash=0;

        /// <summary>
        /// 现金基数
        /// </summary>
        private int baseCash = 0;

        /// <summary>
        /// 会员帐户行
        /// </summary>
        private DataRow accountDr;

        /// <summary>
        /// Gets or sets 会员帐户行
        /// </summary>
        /// <value>会员帐户行</value>
        public DataRow AccountDr
        {
            get
            {
                return accountDr;
            }

            set
            {
                accountDr = value;
            }
        }

        /// <summary>
        /// 会员名称
        /// </summary>
        private string memberName;

        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        /// <value>会员名称</value>
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
        /// 优惠卷代码
        /// </summary>
        private string tiecketCode;

        /// <summary>
        /// Gets or sets 优惠卷代码
        /// </summary>
        /// <value>优惠卷代码</value>
        public string TiecketCode
        {
            get
            {
                return tiecketCode;
            }

            set
            {
                tiecketCode = value;
            }
        }

        /// <summary>
        /// 添加分数
        /// </summary>
        public int AddScore = 0;

        #endregion

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmAddScore" /> class.
        /// </summary>
        public FrmAddScore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmAddScore_Load(object sender, EventArgs e)
        {
            txtCardType.Text = Convert.ToString(AccountDr["CardTypeName"]);
            txtMemberName.Text = MemberName;
            txtCardNO.Text = Convert.ToString(AccountDr["CardNO"]);
            txtOldScore.Text = Convert.ToString(AccountDr["Score"]);

            intCash.Focus();
            DataRow dr = (DataRow)InvokeController("GetConvertPoints", Convert.ToInt16(AccountDr["workID"]), Convert.ToInt16(AccountDr["cardtypeid"]));

            if (dr != null)
            {
                intCash.Text = Convert.ToString(dr["Cash"]);
                txtAddScore.Text = Convert.ToString(dr["Score"]);
                AddScore = Convert.ToInt16(dr["Score"]);
                intCash.MinValue = Convert.ToInt16(dr["Cash"]);
                intCash.Increment = Convert.ToInt16(dr["Cash"]);
                baseCash = Convert.ToInt16(dr["Cash"]);
                cash = Convert.ToInt16(dr["Cash"]);
                txtNewScore.Text = Convert.ToString(Convert.ToInt16(AccountDr["Score"]) + Convert.ToInt16(dr["Score"]));
            }

            cbCash.Checked = true;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if  (string.IsNullOrEmpty(txtNewScore.Text))
            {
                MessageBoxEx.Show("充值后金额大于零！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToDouble(intCash.Text) < baseCash)
            {
                MessageBoxEx.Show("充值金额不能小于最小充值金额：" + baseCash + "元！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ME_ScoreList scoreList = new ME_ScoreList();
            scoreList.AccountID = Convert.ToInt32(AccountDr["AccountID"]);
            scoreList.ScoreType = 1;
            scoreList.DocumentNo = string.Empty;
            scoreList.OperateDate = System.DateTime.Now;
            scoreList.Score = Convert.ToInt32(txtAddScore.Text);
            scoreList.OperateDate = DateTime.Now;
            int payType = 0;
            if (cbCash.Checked == true)
            {
                payType = 1;
            }

            if (cbPOS.Checked == true)
            {
                payType = 2;
            }
            
            if ((int)InvokeController("SaveAddScore", scoreList, Convert.ToInt16(txtNewScore.Text), payType, intCash.Value) >0)
            {
                AccountDr["score"] = Convert.ToInt16(txtNewScore.Text);

                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("PatName", MemberName);
                myDictionary.Add("InvoiceNO", TiecketCode);
                myDictionary.Add("CardNO", txtCardNO.Text);  //卡号
                myDictionary.Add("Operator", (string)InvokeController("GetUserName"));
                myDictionary.Add("ChargeDate", System.DateTime.Now);
                myDictionary.Add("TotalFee", intCash.Text);
                myDictionary.Add("WtotalFee", (string)InvokeController("CmycurD",Convert.ToDecimal(intCash.Text)));
                myDictionary.Add("HospitalName", (string)InvokeController("GetUserWorkName"));
                myDictionary.Add("TypeName", "积分充值收费收据");

                EfwControls.CustomControl.ReportTool.GetReport((int)InvokeController("GetUserWorkID"), 2020, 0, myDictionary, null).Print(true);

                this.Close();
            }
            else
            {
                MessageBoxEx.Show("充值后失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void intCash_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(intCash.Text) == false && string.IsNullOrEmpty(txtAddScore.Text) == false && cash>0 )
            {
                txtNewScore.Text = string.Empty;
                double res = Math.Floor(Convert.ToDouble(intCash.Text) / Convert.ToDouble(cash)); //?
                txtAddScore.Text = Convert.ToString(res * AddScore);
                txtNewScore.Text = Convert.ToString(Convert.ToInt16(txtOldScore.Text) + Convert.ToInt16(txtAddScore.Text));
            }
        }
    }
}
