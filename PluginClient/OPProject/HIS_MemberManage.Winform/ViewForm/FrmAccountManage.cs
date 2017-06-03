using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.WinformFrame.Controller;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;
using DevComponents.DotNetBar;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmAccountManage : BaseFormBusiness,IFrmAccountManage
    {
        #region 接口实现 
        private DataSet _baseData;
        public DataSet BaseData
        {
            get
            {
                return _baseData;
            }
            set
            {
                _baseData = value;
            }
        }
        public string memberName
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

        private string _workID;
        public string workID
        {
            get
            {
                return _workID;
            }
            set
            {
                _workID = value;
            }
        }

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

        public string stDate
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

        public DataTable memberTable
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

        private int _selectMemberIndex;
        public int selectMemberIndex
        {
            get
            {
                return _selectMemberIndex;
            }
            set
            {
                _selectMemberIndex = value;
            }
        }
        private int _selectAccountIndex;
        public int selectAccountIndex
        {
            get
            {
                return _selectAccountIndex;
            }
            set
            {
                _selectAccountIndex = value;
            }
        }

        public string endDate
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

       
        #endregion

        public FrmAccountManage()
        {
            InitializeComponent();
            bindGridSelectIndex(dgMemInfo);
            bindGridSelectIndex(dgAccount);
        }

        public void BindWorkInfo(DataTable dtWorkInfo)
        {
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dtWorkInfo;
                    
        }

        public void BindMemberInfo(DataTable dt,int total)
        {
            //pagerMember.DataSource = dt;
            //pagerMember.totalRecord = total;
            pagerMember.SetPagerDataSource(total, dt);
            setGridSelectIndex(dgMemInfo);
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            dgAccount.DataSource = null;
            InvokeController("BindMemberInfo",1,pagerMember.pageSize);
        }

        private void FrmAccountManage_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmAccountManage_OpenWindowBefore(object sender, EventArgs e)
        {          
            InvokeController("BindWorkInfo");
            InvokeController("BindMemberInfo", 1, pagerMember.pageSize);
            cbbWork.SelectedIndex = 0;
            workID = "-1";
            statRegDate.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statRegDate.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
        }

        private void pagerMember_PageNoChanged(object sender, int pageNo, int pageSize)
        {
            InvokeController("BindMemberInfo", pageNo, pageSize);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        private void NewMem_Click(object sender, EventArgs e)
        {
            InvokeController("ShowMemberInfo", 1, 0, "", 0, pagerMember.pageNo, pagerMember.pageSize, 1);        
        }

        private void btnEditMem_Click(object sender, EventArgs e)
        {
            int rowIndex = dgMemInfo.CurrentCell.RowIndex;
            DataTable dt = dgMemInfo.DataSource as DataTable;
            int MemberID = Convert.ToInt16(dt.Rows[rowIndex]["MemberID"]);
            selectMemberIndex = rowIndex;
            InvokeController("ShowMemberInfo", 2,0,"",0, pagerMember.pageNo, pagerMember.pageSize, dgMemInfo.CurrentCell.RowIndex);
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            if (dgMemInfo.CurrentCell!=null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memberID = Convert.ToInt16(dt.Rows[rowIndex]["MemberID"]);
                int use = (int)InvokeController("UpdateMemberUseFlag", memberID, 1);
                int flag= Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
                if (flag == 1)
                {
                    return;
                }            
            }
             
        }

        private void btnStopMember_Click(object sender, EventArgs e)
        {
            if (dgMemInfo.CurrentCell != null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memberID = Convert.ToInt16(dt.Rows[rowIndex]["MemberID"]);            
                int flag = Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
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

        private void dgMemInfo_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btiNewAccount_Click(object sender, EventArgs e)
        {
            //新增帐户信息
            if (dgMemInfo.CurrentCell != null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
                if (memuseflag==0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int MemberID = Convert.ToInt16(dt.Rows[rowIndex]["MemberID"]);
                selectMemberIndex = rowIndex;
                InvokeController("ShowMemberInfo", 3,0,"",0,0,0,0);
            }


            //panelEx4.Enabled = true;
        }

        private void btiEditAccount_Click(object sender, EventArgs e)
        {
            //修改帐户信息
            if (dgAccount.CurrentCell != null)
            {
                 
                selectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt16(dt.Rows[selectMemberIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accIndex=dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int cardTypeID = Convert.ToInt16(dtAccount.Rows[accIndex]["CardTypeID"]);
                string cardNO = Convert.ToString(dtAccount.Rows[accIndex]["CardNO"]);
                int AccountID= Convert.ToInt16(dtAccount.Rows[accIndex]["AccountID"]);
                int accFlag= Convert.ToInt16(dtAccount.Rows[accIndex]["UseFlag"]);

                if (accFlag==0)
                {
                    MessageBoxEx.Show("帐户处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (accFlag == 2)
                {
                    MessageBoxEx.Show("帐户处于注销状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                selectAccountIndex = accIndex;
                InvokeController("ShowMemberInfo", 4, cardTypeID, cardNO, AccountID, 0, 0, 0);
            }
        }

        private void btiStopAccount_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                selectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt16(dt.Rows[selectMemberIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int cardTypeID = Convert.ToInt16(dtAccount.Rows[accIndex]["CardTypeID"]);
                string cardNO = Convert.ToString(dtAccount.Rows[accIndex]["CardNO"]);
                int AccountID = Convert.ToInt16(dtAccount.Rows[accIndex]["AccountID"]);
                int flag = Convert.ToInt16(dtAccount.Rows[accIndex]["useflag"]);
                if (Convert.ToInt16(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
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
                    selectAccountIndex = accIndex;
                    int use = (int)InvokeController("UpdateAccountUseFlag", AccountID, 1);
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
                    selectAccountIndex = accIndex;
                    int use = (int)InvokeController("UpdateAccountUseFlag", AccountID, 0);
                    if (use == 1)
                    {
                        dtAccount.Rows[accIndex]["UseFlag"] = 0;
                        dtAccount.Rows[accIndex]["accFLAGDESC"] = "停用";
                        dtAccount.AcceptChanges();
                    }
                }
            }
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {

               

            }
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("帐户积分清零后将不能还原，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int AccountID = Convert.ToInt16(dtAccount.Rows[accIndex]["AccountID"]);
                int score= Convert.ToInt16(dtAccount.Rows[accIndex]["score"]);
                selectAccountIndex = accIndex;

                int use = (int)InvokeController("ClearAccountScore", AccountID, score);
                if (use == 1)
                {
                    dtAccount.Rows[accIndex]["score"] = 0;
                    dtAccount.AcceptChanges();
                }

            }
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            int workID = Convert.ToInt16(cbbWork.SelectedValue);

            if (workID < 0)
            {
                MessageBoxEx.Show("请选择具体机构！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBoxEx.Show("帐户积分清零后将不能还原，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
           
            

            int use = (int)InvokeController("ClearAllAccountScore", workID);

            if (use >0)
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

        private void btiOff_Click(object sender, EventArgs e)
        {

            if (MessageBoxEx.Show(" 帐户注销后将不能再启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int AccountID = Convert.ToInt16(dtAccount.Rows[accIndex]["AccountID"]);
                selectAccountIndex = accIndex;

                if (Convert.ToInt16(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
                {
                    MessageBoxEx.Show("帐户已处于注销状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                int use = (int)InvokeController("UpdateAccountUseFlag", AccountID, 2);
                if (use == 1)
                {
                    dtAccount.Rows[accIndex]["UseFlag"] = 2;
                    dtAccount.Rows[accIndex]["accFLAGDESC"] = "注销";
                    dtAccount.AcceptChanges();
                }
            }
        }

        private void btiRecharge_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                selectMemberIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dtMemInfo = dgMemInfo.DataSource as DataTable;
                int memuseflag = Convert.ToInt16(dtMemInfo.Rows[selectMemberIndex]["useflag"]);
                if (memuseflag == 0)
                {
                    MessageBoxEx.Show("会员资格处于停用状态，不能对帐户进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                DataRow accountRow = dtAccount.Rows[accIndex];

                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                string memberName = Convert.ToString(dt.Rows[rowIndex]["name"]);
                if (Convert.ToInt16(dtAccount.Rows[accIndex]["UseFlag"]) == 0)
                {
                    MessageBoxEx.Show("帐户已处于停用状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Convert.ToInt16(dtAccount.Rows[accIndex]["UseFlag"]) == 2)
                {
                    MessageBoxEx.Show("帐户已处于注销状态，不能再进行其他操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                InvokeController("OpenAddScore", accountRow, memberName);
            }
        }

        private void btiQueryScore_Click(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell != null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                int AccountID = Convert.ToInt16(dtAccount.Rows[accIndex]["AccountID"]);
                DataTable dt=(DataTable)InvokeController("ShowAccountDetail", AccountID, statRegDate.Bdate.Text, statRegDate.Edate.Text+" 23:59:59", 0);
            }
        }

        private void cbbWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            workID = Convert.ToString(cbbWork.SelectedValue);
        }

        private void dgMemInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgMemInfo.Rows[e.RowIndex].Cells["memFLAGDESC"].Value);
            if (stopFlag == "停用")
            {
                dgMemInfo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void dgAccount_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgAccount.Rows[e.RowIndex].Cells["accFLAGDESC"].Value);
            if (stopFlag == "停用")
            {
                dgAccount.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;              
            }           
        }

        private void dgMemInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgMemInfo.CurrentCell != null)
            {
                int rowIndex = dgMemInfo.CurrentCell.RowIndex;
                DataTable dt = dgMemInfo.DataSource as DataTable;
                int memberID = Convert.ToInt16(dt.Rows[rowIndex]["MemberID"]);
                int flag = Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
                InvokeController("GetMemberAccount", memberID);
                btnStopMember.Text = (flag > 0) ? "停用会员资格" : "启用会员资格";
                dgAccount.DataSource = AccountTable;
                setGridSelectIndex(dgAccount);
            }
        }

        private void dgAccount_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgAccount.CurrentCell!=null)
            {
                int accIndex = dgAccount.CurrentCell.RowIndex;
                DataTable dtAccount = dgAccount.DataSource as DataTable;
                 
                int flag = Convert.ToInt16(dtAccount.Rows[accIndex]["useflag"]);
                btiStopAccount.Text = (flag > 0) ? "停用帐户" : "启用帐户";
            }
        }
    }
}
