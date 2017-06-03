using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmConvertPointsInfo : BaseFormBusiness, IFrmConvertPointsInfo
    {
        /// <summary>
        /// 兑换ID
        /// </summary>
        private int convertID;

        /// <summary>
        /// Gets or sets 兑换ID
        /// </summary>
        /// <value>兑换ID</value>
        public int ConvertID
        {
            get
            {
                return convertID;
            }

            set
            {
                convertID = value;
            }
        }

        /// <summary>
        /// 行索引
        /// </summary>
        private int rowIndwx;

        /// <summary>
        /// Gets or sets 行索引
        /// </summary>
        /// <value>行索引</value>
        public int RowIndex
        {
            get
            {
                return rowIndwx;
            }

            set
            {
                rowIndwx = value;
            }
        }

        /// <summary>
        /// 机构id
        /// </summary>
        private int workID;

        /// <summary>
        /// Gets or sets 机构id
        /// </summary>
        /// <value>机构id</value>
        public int WorkID
        {
            get
            {
                return workID;
            }

            set
            {
                workID = value;
            }
        }

        /// <summary>
        /// 卡类型id
        /// </summary>
        private int cardTypeID;

        /// <summary>
        /// Gets or sets 卡类型id
        /// </summary>
        /// <value>卡类型id</value>
        public int CardTypeID
        {
            get
            {
                return cardTypeID;
            }

            set
            {
                cardTypeID = value;
            }
        }

        /// <summary>
        /// 现金
        /// </summary>
        private int cash;

        /// <summary>
        /// Gets or sets 现金
        /// </summary>
        /// <value>现金</value>
        public int Cash
        {
            get
            {
                return cash;
            }

            set
            {
                cash = value;
            }
        }

        /// <summary>
        /// 积分
        /// </summary>
        private int score;

        /// <summary>
        /// Gets or sets 积分
        /// </summary>
        /// <value>积分</value>
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        private string cardTypeName;

        /// <summary>
        /// Gets or sets 卡类型名称
        /// </summary>
        /// <value>卡类型名称</value>
        public string CardTypeName
        {
            get
            {
                return cardTypeName;
            }

            set
            {
                cardTypeName = value;
            }
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmConvertPointsInfo" /> class.
        /// </summary>
        public FrmConvertPointsInfo()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmConvertPointsInfo_Load(object sender, EventArgs e)
        {
            DataTable dt= (DataTable)InvokeController("BindWorkInfo");
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dt;

            txtCardTypeName.Text = CardTypeName;
            if (WorkID > 0)
            {
                cbbWork.SelectedValue = WorkID;
                cbbWork.Enabled = false;
            }
            else
            {
                cbbWork.Enabled = true;
            }

            intCash.Value = Cash;
            intScore.Value = Score;

            cbbWork.SelectedValue = (int)InvokeController("GetUserWorkID");
            cbbWork.Enabled = false;
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
        /// 校验电话号码
        /// </summary>
        /// <param name="txt">电话号码字符串</param>
        /// <returns>true存在特殊字符</returns>
        public bool RegexTelPhone(string txt)
        {
            return new Regex(@"[`~!@#$%^&*()+=|{}':;',\[\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]+").IsMatch(txt);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check = (bool)InvokeController("CheckPointsForADD", CardTypeID, cbbWork.SelectedValue);

            if (WorkID == 0)
            {
                if (check == false)
                {
                    MessageBoxEx.Show(" 此帐户类型积分兑换设置已存在！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int res=(int)InvokeController("SavePoints", CardTypeID,intCash.Value,intScore.Value,1, RowIndex,ConvertID,cbbWork.SelectedValue);
            if (res>0)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void labelX11_Click(object sender, EventArgs e)
        {
        }
    }
}
