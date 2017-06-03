using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmMember : BaseFormBusiness, IFrmMember
    {
        #region 接口实现 
        /// <summary>
        /// 查询条件
        /// </summary>
        private string sqlCondition;

        /// <summary>
        /// Gets or sets 查询条件
        /// </summary>
        /// <value>查询条件</value>
        public string SqlCondition
        {
            get
            {
                return sqlCondition;
            }

            set
            {
                sqlCondition = value;
            }
        }

        /// <summary>
        /// 总数
        /// </summary>
        private int total;

        /// <summary>
        /// Gets or sets 总数
        /// </summary>
        /// <value>总数</value>
        public int Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        /// <value>会员名称</value>
        public string MemberName
        {
            get
            {
                return txtName.Text;
            }

            set
            {
                txtName.Text = value;
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
        /// Gets or sets 手机
        /// </summary>
        /// <value>手机</value>
        public string Mobile
        {
            get
            {
                return textMobile.Text;
            }

            set
            {
                textMobile.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets 开始日期
        /// </summary>
        /// <value>开始日期</value>
        public string StDate
        {
            get
            {
                return statRegDate.Bdate.Text;
            }

            set
            {
                statRegDate.Bdate.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets 用于绑定帐户信息网络
        /// </summary>
        /// <value>用于绑定帐户信息网络</value>
        public DataTable AccountTable
        {
            get
            {
                return (DataTable)dgAccount.DataSource;
            }

            set
            {
                dgAccount.DataSource = value;
            }
        }

        /// <summary>
        /// Gets or sets 会员表
        /// </summary>
        /// <value>会员表</value>
        public DataTable MemberTable
        {
            get
            {
                return (DataTable)dgMemInfo.DataSource;
            }

            set
            {
                dgMemInfo.DataSource = value;
            }
        }

        /// <summary>
        /// 会员信息网络选中行索引
        /// </summary>
        private int selectMemberIndex;

        /// <summary>
        /// Gets or sets 会员信息网络选中行索引
        /// </summary>
        /// <value>会员信息网络选中行索引</value>
        public int SelectMemberIndex
        {
            get
            {
                return selectMemberIndex;
            }

            set
            {
                selectMemberIndex = value;
            }
        }

        /// <summary>
        /// 选中的帐户索引
        /// </summary>
        private int selectAccountIndex;

        /// <summary>
        /// Gets or sets 选中的帐户索引
        /// </summary>
        /// <value>选中的帐户索引</value>
        public int SelectAccountIndex
        {
            get
            {
                return selectAccountIndex;
            }

            set
            {
                selectAccountIndex = value;
            }
        }

        /// <summary>
        ///Gets or sets  结束日期
        /// </summary>
        /// <value>结束日期</value>
        public string EndDate
        {
            get
            {
                return statRegDate.Edate.Text;
            }

            set
            {
                statRegDate.Edate.Text = value;
            }
        }

        /// <summary>
        /// 基础数据
        /// </summary>
        private DataSet baseData;

        /// <summary>
        /// Gets or sets 基础数据
        /// </summary>
        /// <value>基础数据</value>
        public DataSet BaseData
        {
            get
            {
                return baseData;
            }

            set
            {
                baseData = value;
            }
        }

        /// <summary>
        /// 绑定组织机构信息
        /// </summary>
        /// <param name="dtWorkInfo">组织机构</param>
        public void BindWorkInfo(DataTable dtWorkInfo)
        {
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dtWorkInfo;
        }

        #endregion

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmAllRevenuReport" /> class.
        /// </summary>
        public FrmMember()
        {
            InitializeComponent();
            bindGridSelectIndex(dgMemInfo);
            bindGridSelectIndex(dgAccount);
            statRegDate.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statRegDate.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
            BaseData = new DataSet();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmMember_OpenWindowBefore(object sender, EventArgs e)
        {
            dgMemInfo.DataSource = null;
            dgAccount.DataSource = null;
            InvokeController("BindWorkInfo");

            WorkID = (int)InvokeController("GetUserWorkID");
            cbbWork.SelectedIndexChanged -= cbbWork_SelectedIndexChanged;
            SqlCondition = SetCondition(WorkID, statRegDate.Bdate.Text, statRegDate.Edate.Text, txtName.Text, textMobile.Text);
            InvokeController("BindMemberInfo", SqlCondition, 1, pagerMember.pageSize, 0);
            BaseData = (DataSet)InvokeController("GetBaseDataOne", BaseData);
            cbbWork.SelectedIndexChanged += cbbWork_SelectedIndexChanged;
            cbbWork.SelectedValue = (int)InvokeController("GetUserWorkID");
        }

        /// <summary>
        /// 绑定会员信息网格
        /// </summary>
        /// <param name="dt">会员信息</param>
        /// <param name="pageNO">页号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="index">索引</param>
        public void BindMemberInfo(DataTable dt, int pageNO, int pageSize, int index)
        {
            pagerMember.SetPagerDataSource(Total, dt);
            setGridSelectIndex(dgMemInfo, index);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            WorkID = Convert.ToInt32(cbbWork.SelectedValue);
            SqlCondition = SetCondition(WorkID, statRegDate.Bdate.Text, statRegDate.Edate.Text, txtName.Text, textMobile.Text);
            InvokeController("BindMemberInfo", SqlCondition, 1, pagerMember.pageSize, 0);          
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
        private void dgMemInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgMemInfo.CurrentCell!=null)
            {
                SelectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                int memberID = Convert.ToInt32(MemberTable.Rows[SelectMemberIndex]["MemberID"]);
                int flag = Convert.ToInt32(MemberTable.Rows[SelectMemberIndex]["useflag"]);
                memberID = (memberID == 0) ? 0 : memberID;
                btnStopMember.Text = (flag > 0) ? "停用会员资格" : "启用会员资格";
                InvokeController("GetAccountInfo", memberID);
                setGridSelectIndex(dgAccount);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">每页条数</param>
        private void pagerMember_PageNoChanged(object sender, int pageNo, int pageSize)
        {
            SqlCondition = SetCondition(WorkID, statRegDate.Bdate.Text, statRegDate.Edate.Text, txtName.Text, textMobile.Text);
            InvokeController("BindMemberInfo", SqlCondition, pageNo, pageSize, 0);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void NewMem_Click(object sender, EventArgs e)
        {
            if (BaseData.Tables.Contains("dtgj") == false)
            {
                BaseData = (DataSet)InvokeController("GetBaseDataTwo", BaseData);
            }

            InvokeController("ShowMemberInfo", 1, 0, string.Empty, 0, pagerMember.pageNo, pagerMember.pageSize, 1);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnEditMem_Click(object sender, EventArgs e)
        {
            if (BaseData.Tables.Contains("dtgj")==false)
            {
                BaseData = (DataSet)InvokeController("GetBaseDataTwo", BaseData);
            }

            if (dgMemInfo.CurrentCell!=null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memberID = Convert.ToInt32(dt.Rows[rowIndex]["MemberID"]);
                SelectMemberIndex = rowIndex;
                InvokeController("ShowMemberInfo", 2, 0, string.Empty, 0, pagerMember.pageNo, pagerMember.pageSize, dgMemInfo.CurrentCell.RowIndex);
            }         
        }

        /// <summary>
        /// 生成SQL查询条件
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="memberName">会员名称</param>
        /// <param name="mobile">手机</param>
        /// <returns>查询语句</returns>
        public string SetCondition(int workID, string stDate, string endDate, string memberName, string mobile)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(stDate) == false)
            {
                sql = " and OpenDate>='" + stDate + "' and OpenDate<='" + endDate + " 23:59:59'";
            }

            if (workID != -1)
            {
                sql = sql + " and RegisterWork=" + workID;
            }

            if (string.IsNullOrEmpty(memberName) == false)
            {
                sql = sql + " and Name like '%" + memberName + "%'";
            }

            if (string.IsNullOrEmpty(mobile) == false)
            {
                sql = sql + " and Mobile = '" + mobile + "'";
            }

            return sql;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnStopMember_Click(object sender, EventArgs e)
        {
            if (dgMemInfo.CurrentCell != null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memberID = Convert.ToInt32(dt.Rows[rowIndex]["MemberID"]);
                int flag = Convert.ToInt32(dt.Rows[rowIndex]["useflag"]);
                if (flag == 0)
                {
                    if (MessageBoxEx.Show(" 会员资格将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    int use = (int)InvokeController("UpdateMemberUseFlag", memberID, 1);
                    if (use == 1)
                    {
                        dt.Rows[rowIndex]["UseFlag"] = 1;
                        dt.Rows[rowIndex]["memFLAGDESC"] = "有效";
                        dt.AcceptChanges();
                    }
                }
                else
                {
                    if (MessageBoxEx.Show(" 会员资格将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    int use = (int)InvokeController("UpdateMemberUseFlag", memberID, 0);
                    if (use == 1)
                    {
                        dt.Rows[rowIndex]["UseFlag"] = 0;
                        dt.Rows[rowIndex]["memFLAGDESC"] = "停用";
                        dt.AcceptChanges();
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgMemInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgMemInfo.Rows[e.RowIndex].Cells["memFLAGDESC"].Value);
            if (stopFlag == "停用")
            {
                dgMemInfo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            WorkID = Convert.ToInt32(cbbWork.SelectedValue);

            if (WorkID < 0)
            {
                MessageBoxEx.Show("请选择具体机构！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBoxEx.Show("帐户积分清零后将不能还原，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            int use = (int)InvokeController("ClearAllAccountScore", WorkID);

            if (use > 0)
            {
                if (dgAccount.CurrentCell != null)
                {
                    DataTable dtAccount = dgAccount.DataSource as DataTable;
                    for (int i = 0; i < dtAccount.Rows.Count; i++)
                    {
                        dtAccount.Rows[i]["score"] = 0;
                    }

                    dtAccount.AcceptChanges();
                }
            }
        }

        #region 帐户管理
        /// <summary>
        /// 绑定帐户信息
        /// </summary>
        /// <param name="index">行索引</param>
        public void BindAccountInfo(int index)
        {
            setGridSelectIndex(dgAccount, index);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiNewAccount_Click(object sender, EventArgs e)
        {
            if (BaseData.Tables.Contains("dtgj") == false)
            {
                BaseData = (DataSet)InvokeController("GetBaseDataTwo", BaseData);
            }

            //新增帐户信息
            if (dgMemInfo.CurrentCell != null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                MemberTable = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt32(MemberTable.Rows[rowIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int memberID = Convert.ToInt32(MemberTable.Rows[rowIndex]["MemberID"]);
                SelectMemberIndex = rowIndex;
                SelectAccountIndex = dgAccount.Rows.Count;
                InvokeController("ShowMemberInfo", 3, 0, string.Empty, 0, 0, 0, SelectAccountIndex);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiEditAccount_Click(object sender, EventArgs e)
        {
            if (BaseData.Tables.Contains("dtgj") == false)
            {
                BaseData = (DataSet)InvokeController("GetBaseDataTwo", BaseData);
            }

            //修改帐户信息
            if (dgAccount.CurrentCell != null)
            {
                SelectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt32(dt.Rows[SelectMemberIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SelectAccountIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int cardTypeID = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["CardTypeID"]); //帐户类型ID
                string cardNO = Convert.ToString(dtAccount.Rows[SelectAccountIndex]["CardNO"]);    //卡号
                int accountID = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["AccountID"]);   //帐户ID
                int accFlag = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["UseFlag"]);

                if (accFlag == 0)
                {
                    MessageBoxEx.Show("帐户处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (accFlag == 2)
                {
                    MessageBoxEx.Show("帐户处于注销状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InvokeController("ShowMemberInfo", 4, cardTypeID, cardNO, accountID, 0, 0, SelectAccountIndex);
            }
        }

        #endregion
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiStopAccount_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                SelectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt32(dt.Rows[SelectMemberIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int cardTypeID = Convert.ToInt32(dtAccount.Rows[accIndex]["CardTypeID"]);
                string cardNO = Convert.ToString(dtAccount.Rows[accIndex]["CardNO"]);
                int accountID = Convert.ToInt32(dtAccount.Rows[accIndex]["AccountID"]);
                int flag = Convert.ToInt32(dtAccount.Rows[accIndex]["useflag"]);
                if (Convert.ToInt32(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
                {
                    MessageBoxEx.Show("帐户已处于注销状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (flag == 0)
                {
                    if (MessageBoxEx.Show("你选择的会员帐户将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    SelectAccountIndex = accIndex;
                    int use = (int)InvokeController("UpdateAccountUseFlag", accountID, 1);
                    if (use == 1)
                    {
                        dtAccount.Rows[accIndex]["UseFlag"] = 1;
                        dtAccount.Rows[accIndex]["accFLAGDESC"] = "启用";
                        dtAccount.AcceptChanges();
                    }
                }
                else if (flag == 1)
                {
                    if (MessageBoxEx.Show(" 会员帐户将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    SelectAccountIndex = accIndex;
                    int use = (int)InvokeController("UpdateAccountUseFlag", accountID, 0);
                    if (use == 1)
                    {
                        dtAccount.Rows[accIndex]["UseFlag"] = 0;
                        dtAccount.Rows[accIndex]["accFLAGDESC"] = "停用";
                        dtAccount.AcceptChanges();
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgAccount_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;

                int flag = Convert.ToInt32(dtAccount.Rows[accIndex]["useflag"]);
                btiStopAccount.Text = (flag > 0) ? "停用帐户" : "启用帐户";
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgAccount_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgAccount.Rows[e.RowIndex].Cells["accFLAGDESC"].Value);
            if (stopFlag == "停用")
            {
                dgAccount.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiRecharge_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                SelectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dtMemInfo = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt32(dtMemInfo.Rows[SelectMemberIndex]["useflag"]);
                int workID= Convert.ToInt32(dtMemInfo.Rows[SelectMemberIndex]["WorkID"]);

                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                DataRow accountRow = dtAccount.Rows[accIndex];
                int cardtypeid = Convert.ToInt32(dtAccount.Rows[accIndex]["cardtypeid"]);

                string memberName = Convert.ToString(dtMemInfo.Rows[SelectMemberIndex]["name"]);
                if (Convert.ToInt32(dtAccount.Rows[accIndex]["UseFlag"]) == 0)
                {
                    MessageBoxEx.Show("帐户已处于停用状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Convert.ToInt32(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
                {
                    MessageBoxEx.Show("帐户已处于注销状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow dr = (DataRow)InvokeController("GetConvertPoints", workID, cardtypeid);

                if (dr == null)                 
                {
                    MessageBoxEx.Show("此帐户类型没有设置积分规则不能充值！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InvokeController("OpenAddScore", accountRow, memberName);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiQueryScore_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int accountID = Convert.ToInt32(dtAccount.Rows[accIndex]["AccountID"]);
                DataTable dt = (DataTable)InvokeController("ShowAccountDetail", accountID, statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59", 0);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void ButClear_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("帐户积分清零后将不能还原，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int accountID = Convert.ToInt32(dtAccount.Rows[accIndex]["AccountID"]);
                int score = Convert.ToInt32(dtAccount.Rows[accIndex]["score"]);
                SelectAccountIndex = accIndex;

                int use = (int)InvokeController("ClearAccountScore", accountID, score);
                if (use == 1)
                {
                    dtAccount.Rows[accIndex]["score"] = 0;
                    dtAccount.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiOff_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(" 帐户注销后将不能再启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int accountID = Convert.ToInt32(dtAccount.Rows[accIndex]["AccountID"]);
                SelectAccountIndex = accIndex;

                if (Convert.ToInt32(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
                {
                    MessageBoxEx.Show("帐户已处于注销状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int use = (int)InvokeController("UpdateAccountUseFlag", accountID, 2);
                if (use == 1)
                {
                    dtAccount.Rows[accIndex]["UseFlag"] = 2;
                    dtAccount.Rows[accIndex]["accFLAGDESC"] = "注销";
                    dtAccount.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbWork.SelectedValue) == (int)InvokeController("GetUserWorkID"))
            {
                NewMem.Enabled = true;
                btnEditMem.Enabled = true;
                btnStopMember.Enabled = true;
                btnClearAll.Enabled = true;
                btiNewAccount.Enabled = true;
                btiEditAccount.Enabled = true;
                btiStopAccount.Enabled = true;
                btiRecharge.Enabled = true;
                butClear.Enabled = true;
                btiOff.Enabled = true;
            }
            else
            {
                NewMem.Enabled = false;
                btnEditMem.Enabled = false;
                btnStopMember.Enabled = false;
                btnClearAll.Enabled = false;
                btiNewAccount.Enabled = false;
                btiEditAccount.Enabled = false;
                btiStopAccount.Enabled = false;
                btiRecharge.Enabled = false;
                butClear.Enabled = false;
                btiOff.Enabled = false;
            }

            WorkID = Convert.ToInt32(cbbWork.SelectedValue);
            SqlCondition = SetCondition(WorkID, statRegDate.Bdate.Text, statRegDate.Edate.Text, txtName.Text, textMobile.Text);
            InvokeController("BindMemberInfo", SqlCondition, 1, pagerMember.pageSize, 0);
        }
        
        /// <summary>
        /// 设置控件状态
        /// </summary>
        /// <param name="flag">可用标识</param>
        private void SetControlState(bool flag)
        {
            foreach (DevComponents.DotNetBar.ButtonItem control in this.bar1.Controls)
            {
                if (control is DevComponents.DotNetBar.ButtonItem)
                {
                    ButtonItem tb = (ButtonItem)control;

                    tb.Enabled = flag;
                }                
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (BaseData.Tables.Contains("dtgj") == false)
            {
                BaseData = (DataSet)InvokeController("GetBaseDataTwo", BaseData);
            }

            //修改帐户信息
            if (dgAccount.CurrentCell != null)
            {
                SelectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt32(dt.Rows[SelectMemberIndex]["useflag"]);
                string memberName= Convert.ToString(dt.Rows[SelectMemberIndex]["Name"]);  //会员姓名
                int memberID = Convert.ToInt32(dt.Rows[SelectMemberIndex]["MemberID"]);

                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SelectAccountIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int cardTypeID = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["CardTypeID"]); //帐户类型ID
                string cardNO = Convert.ToString(dtAccount.Rows[SelectAccountIndex]["CardNO"]);    //卡号
                int accountID = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["AccountID"]);   //帐户ID
                int accFlag = Convert.ToInt32(dtAccount.Rows[SelectAccountIndex]["UseFlag"]);
                string accountTypeName= Convert.ToString(dtAccount.Rows[SelectAccountIndex]["CardTypeName"]);

                if (accFlag == 0)
                {
                    MessageBoxEx.Show("帐户处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (accFlag == 2)
                {
                    MessageBoxEx.Show("帐户处于注销状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InvokeController("ShowChangeCardList", memberName, accountTypeName, accountID, cardNO,  cardTypeID, memberID);
            }
        }
    }
}
