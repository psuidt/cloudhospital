using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmAccountDetail : BaseFormBusiness, IFrmAccountDetail
    {
        /// <summary>
        /// 积分表
        /// </summary>
        private DataTable scoreTable;

        /// <summary>
        /// Gets or sets 积分表
        /// </summary>
        /// <value>积分表</value>
        public DataTable ScoreTable
        {
            get
            {
                return scoreTable;
            }

            set
            {
                scoreTable = value;
            }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private string bDate;

        /// <summary>
        /// Gets or sets 开始日期
        /// </summary>
        /// <value>开始日期</value>
        public string BDate
        {
            get
            {
                return bDate;
            }

            set
            {
                bDate = value;
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private string eDate;

        /// <summary>
        /// Gets or sets 结束日期
        /// </summary>
        /// <value>结束日期</value>
        public string EDate
        {
            get
            {
                return eDate;
            }

            set
            {
                eDate = value;
            }
        }

        /// <summary>
        /// 帐户ID
        /// </summary>
        private int accountID;

        /// <summary>
        /// Gets or sets 帐户ID
        /// </summary>
        /// <value>帐户ID</value>
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
        /// 绑定积分类型
        /// </summary>
        public void BindScoreType()
        {
            var datasource = new[] 
            {
                new { Text = "全部", Value = 0 },
                new { Text = "帐户现金充值", Value = 1 },
                new { Text = "门诊消费充值", Value = 2 },
                new { Text = "住院消费充值", Value = 3 },
                new { Text = "积分清零", Value = 4 },
                new { Text = "礼品兑换", Value = 5 },
            };

            cbbScoreType.ValueMember = "Value";
            cbbScoreType.DisplayMember = "Text";
            cbbScoreType.DataSource = datasource;
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmAccountDetail" /> class.
        /// </summary>
        public FrmAccountDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmAccountDetail_Load(object sender, EventArgs e)
        {
            BindScoreType();
            statDateTime.Bdate.Text = BDate;
            statDateTime.Edate.Text = EDate;
            dataGrid1.DataSource = ScoreTable;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {             
            DataTable dt= (DataTable)InvokeController("QueryAccountScoreList", AccountID, statDateTime.Bdate.Text, statDateTime.Edate.Text+" 23:59:59", cbbScoreType.SelectedValue);
            dataGrid1.DataSource = dt;
        }
    }
}
