using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmDocSchedual :BaseFormBusiness,IFrmDocSchedual
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDocSchedual()
        {
            InitializeComponent();
        }

        #region 接口实现
        /// <summary>
        /// 复制排班原最大排班日期
        /// </summary>
        private DateTime copymaxdate;

        /// <summary>
        /// 复制排班原最大排班日期
        /// </summary>
        public DateTime CopyMaxDate
        {
            get
            {
                return copymaxdate;
            }

            set
            {
                copymaxdate = value;
            }
        }

        /// <summary>
        /// 获取和设置新排班开始日期
        /// </summary>
        private DateTime copyNewbdate;

        /// <summary>
        /// 获取和设置新排班开始日期
        /// </summary>
        public DateTime CopyNewBDate
        {
            get
            {
                return copyNewbdate;
            }

            set
            {
                copyNewbdate = value;
            }
        }

        /// <summary>
        /// 获取查询的开始日期
        /// </summary>
        public DateTime GetStatDate
        {
            get
            {
                return dtDate.Bdate.Value;
            }
        }

        /// <summary>
        /// 获取查询的结束日期
        /// </summary>
        public DateTime GetEndDate
        {
            get
            {
                return dtDate.Edate.Value;
            }
        }

        /// <summary>
        /// 获取查询科室Id
        /// </summary>
        public int QueryDeptid
        {
            get
            {
                return  txtQueryDept.MemberValue==null?0:Convert.ToInt32(txtQueryDept.MemberValue);
            }
        }

        /// <summary>
        /// 获取查询医生ID
        /// </summary>
        public int QueryDocid
        {
            get
            {
              return   txtQueryDoc.MemberValue == null ? 0 : Convert.ToInt32(txtQueryDoc.MemberValue);
            }
        }

        /// <summary>
        /// 所有医生
        /// </summary>
        private DataTable dtAlldoctor;

        /// <summary>
        /// 所有医生数据
        /// </summary>
        public DataTable DtAlldoctor
        {
            get { return dtAlldoctor; }
            set { dtAlldoctor = value; }
        }

        /// <summary>
        /// 当前当个排班对象
        /// </summary>
        private OP_DocSchedual curDocSchedual;

        /// <summary>
        /// 当前当个排班对象
        /// </summary>
        public OP_DocSchedual CurDocSchedual
        {
            get
            {
                curDocSchedual.SchedualDate = Convert.ToDateTime(dtpSchedualDate.Value.ToString("yyyy-MM-dd"));
                curDocSchedual.SchedualTimeRange = cmbSchedualTime.SelectedIndex + 1;
                curDocSchedual.DocProfessionName = txtDocProfessor.Text.Trim();
                curDocSchedual.DocEmpID = Convert.ToInt32(txtSchedualDoc.MemberValue);
                curDocSchedual.DeptID = Convert.ToInt32(txtSchdualDept.MemberValue);
                curDocSchedual.Flag = chkSchedual.Checked ? 1 : 0;
                return curDocSchedual;
            }

            set
            {
                curDocSchedual = value;
                dtpSchedualDate.Value = Convert.ToDateTime(curDocSchedual.SchedualDate.ToString("yyyy-MM-dd"));
                cmbSchedualTime.SelectedIndex = curDocSchedual.SchedualTimeRange - 1;
                txtDocProfessor.Text = curDocSchedual.DocProfessionName;
                txtSchdualDept.MemberValue = curDocSchedual.DeptID;
                txtSchedualDoc.MemberValue = curDocSchedual.DocEmpID;
                chkSchedual.Checked = curDocSchedual.Flag == 1 ? true : false;
            }
        }

        /// <summary>
        /// 科室数据绑定
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void loadDept(DataTable dtDept)
        {
            txtQueryDept.DisplayField = "Name";
            txtQueryDept.MemberField = "deptid";
            txtQueryDept.CardColumn = "Name|名称|auto";
            txtQueryDept.QueryFieldsString = "Name,pym,wbm";
            txtQueryDept.ShowCardHeight = 130;
            txtQueryDept.ShowCardWidth = 140;
            txtQueryDept.ShowCardDataSource = dtDept;

            txtSchdualDept.DisplayField = "Name";
            txtSchdualDept.MemberField = "deptid";
            txtSchdualDept.CardColumn = "Name|名称|auto";
            txtSchdualDept.QueryFieldsString = "Name,pym,wbm";
            txtSchdualDept.ShowCardHeight = 130;
            txtSchdualDept.ShowCardWidth = 140;
            txtSchdualDept.ShowCardDataSource = dtDept;
        }

        /// <summary>
        /// 医生数据绑定
        /// </summary>
        /// <param name="dtDoc">医生数据</param>
        public void loadDoctor(DataTable dtDoc)
        {
            txtQueryDoc.DisplayField = "Name";
            txtQueryDoc.MemberField = "EmpID";
            txtQueryDoc.CardColumn = "Name|姓名|auto";
            txtQueryDoc.QueryFieldsString = "Name,pym,wbm";
            txtQueryDoc.ShowCardHeight = 130;
            txtQueryDoc.ShowCardWidth = 140;
            txtQueryDoc.ShowCardDataSource = dtDoc;

            txtSchedualDoc.DisplayField = "Name";
            txtSchedualDoc.MemberField = "EmpID";
            txtSchedualDoc.CardColumn = "Name|姓名|60,JobTitle|职称|auto";
            txtSchedualDoc.QueryFieldsString = "Name,pym,wbm";
            txtSchedualDoc.ShowCardHeight = 130;
            txtSchedualDoc.ShowCardWidth = 140;
            txtSchedualDoc.ShowCardDataSource = dtDoc;
        }

        /// <summary>
        /// 根据查询科室ID过滤科室医生
        /// </summary>
        /// <param name="schedualDeptID">查询科室ID</param>
        private void BindSchedualDoc(int schedualDeptID)
        {
            txtSchedualDoc.DisplayField = "Name";
            txtSchedualDoc.MemberField = "EmpID";
            txtSchedualDoc.CardColumn = "Name|姓名|60,JobTitle|职称|auto";
            txtSchedualDoc.QueryFieldsString = "Name,pym,wbm";
            txtSchedualDoc.ShowCardHeight = 130;
            txtSchedualDoc.ShowCardWidth = 140;
            if (schedualDeptID > 0)
            {
                // 过滤数据
                DataTable dtDoc = dtAlldoctor.Clone();
                dtAlldoctor.TableName = "_dtAlldoctor";

                // 过滤明细数据
                DataView docView = new DataView(dtAlldoctor);
                string sqlWhere = " DeptId = " + schedualDeptID + string.Empty;
                docView.RowFilter = sqlWhere;
                dtDoc.Merge(docView.ToTable());
                txtSchedualDoc.ShowCardDataSource = dtDoc;
            }
            else
            {
                txtSchedualDoc.ShowCardDataSource = dtAlldoctor;
            }           
        }

        /// <summary>
        /// 根据排班科室ID过滤医生
        /// </summary>
        /// <param name="schedualDeptID">排班科室ID</param>
        private void BindQueryDoc(int schedualDeptID)
        {
            // 过滤数据
            DataTable dtDoc = dtAlldoctor.Clone();
            dtAlldoctor.TableName = "_dtAlldoctor";

            // 过滤明细数据
            DataView docView = new DataView(dtAlldoctor);
            string sqlWhere = " DeptId = " + schedualDeptID + string.Empty;
            docView.RowFilter = sqlWhere;
            dtDoc.Merge(docView.ToTable());
            txtQueryDoc.DisplayField = "Name";
            txtQueryDoc.MemberField = "EmpID";
            txtQueryDoc.CardColumn = "Name|姓名|auto";
            txtQueryDoc.QueryFieldsString = "Name,pym,wbm";
            txtQueryDoc.ShowCardHeight = 130;
            txtQueryDoc.ShowCardWidth = 140;
            txtQueryDoc.ShowCardDataSource = dtDoc;
        }

        /// <summary>
        /// 绑定日期
        /// </summary>
        /// <param name="dtDate">日期</param>
        public void loadSchedualDate(DataTable dtDate)
        {
            dgScheDate.DataSource = dtDate;
            SetGridColor();
        }

        /// <summary>
        /// 获取排班信息
        /// </summary>
        /// <param name="dtSchedual">排班信息</param>
        public void LoadSchedual(DataTable dtSchedual)
        {
            dgSchedualData.DataSource = dtSchedual.DefaultView;
            if (dtSchedual == null || dtSchedual.Rows.Count == 0)
            {
                curDocSchedual = new OP_DocSchedual();
                CurDocSchedual = curDocSchedual;
            }
        }
        #endregion

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
        {
            DataTable dt = (DataTable)dgScheDate.DataSource;
            List<int> sundayRows = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["WeekDay"].ToString() == "星期日")
                {
                    sundayRows.Add(i);
                }               
            }

            if (sundayRows.Count > 1)
            {
                for (int i = 0; i < sundayRows.Count; i += 2)
                {
                    int startindex = 0;
                    int endindex = sundayRows[i];
                    if (i > 1 && i + 1 < sundayRows.Count)
                    {
                        startindex = sundayRows[i] + 1;
                        endindex = sundayRows[i + 1];
                    }

                    if (i + 1 < sundayRows.Count)
                    {
                        for (int rowindex = startindex; rowindex <= endindex; rowindex++)
                        {
                            dgScheDate.SetRowColor(rowindex, Color.Blue, true);
                        }
                    }

                    if (i + 2 < sundayRows.Count)
                    {
                        for (int rowindex = sundayRows[i+1] + 1; rowindex <= sundayRows[i + 2]; rowindex++)
                        {
                            dgScheDate.SetRowColor(rowindex, Color.OrangeRed, true);
                        }
                    }
                }
            }  
        }

        /// <summary>
        /// 复制排班
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCopySchedual_Click(object sender, EventArgs e)
        {
            if ((bool)InvokeController("CopySchedualCheck"))
            {
                if ((DialogResult)(InvokeController("ShowDialog", "FrmCopySchedual")) == DialogResult.OK)
                {
                }
            }
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmDocSchedual_OpenWindowBefore(object sender, EventArgs e)
        {          
            DateTime date = DateTime.Now;
            DateTime startWeek = date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));  //本周周一
            dtDate.Bdate.Value = startWeek;
            dtDate.Edate.Value = startWeek.AddDays(6);  //本周周日     
            InvokeController("GetSchedualDates");//构造排班日期列表        
            InvokeController("SchedualDataInit");
            InvokeController("GetSchedualMoreDate");//获取排班信息
        }

        /// <summary>
        /// 进询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetSchedualDates");//构造排班日期列表
            InvokeController("GetSchedualMoreDate");//获取排班信息
        }

        /// <summary>
        /// 日期网格Click事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgScheDate_Click(object sender, EventArgs e)
        {
            if (dgScheDate.CurrentCell != null)
            {
                int rowid = this.dgScheDate.CurrentCell.RowIndex;
                DateTime date = Convert.ToDateTime(dgScheDate["SchedualDate", rowid].Value);
                InvokeController("GetSchedualOneDate",date);//获取排班信息
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 新增排班按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnAddSchedual_Click(object sender, EventArgs e)
        {
            curDocSchedual  = new OP_DocSchedual();
            curDocSchedual.SchedualID = 0;
            curDocSchedual.SchedualTimeRange = 1;
            curDocSchedual.Flag = 1;
            DateTime date=DateTime.Now;
            DateTime selDate=DateTime.Now;
            if (dgScheDate.CurrentCell != null)
            {
                int rowid = this.dgScheDate.CurrentCell.RowIndex;
                selDate = Convert.ToDateTime(dgScheDate["SchedualDate", rowid].Value);
            }

            if (selDate.Date < date.Date)
            {
                curDocSchedual.SchedualDate = DateTime.Now;
            }
            else
            {
                curDocSchedual.SchedualDate = selDate;
            }

            curDocSchedual.DocProfessionName = string.Empty;
            CurDocSchedual = curDocSchedual;
            dtpSchedualDate.Focus();
        }

        /// <summary>
        /// 保存排班按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnSaveSchedual_Click(object sender, EventArgs e)
        {
            if (dtpSchedualDate.Value == null)
            {
                MessageBoxEx.Show("请选择排班日期");
                return;
            }

            if (cmbSchedualTime.SelectedItem.ToString() == string.Empty)
            {
                MessageBoxEx.Show("请选择出诊班次");
                return;
            }

            if (txtSchdualDept.Text == string.Empty)
            {
                MessageBoxEx.Show("请选择排班科室");
                return;
            }

            if (txtSchedualDoc.Text == string.Empty)
            {
                MessageBoxEx.Show("请选择排班医生");
                return;
            }

            if ((bool)InvokeController("SaveSchedual"))
            {
                dgScheDate_Click(null, null);
                btnAddSchedual.Focus();
                BindSchedualDoc(-1);
            }
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("GetSchedualDates");//构造排班日期列表
            InvokeController("GetSchedualMoreDate");//获取排班信息
        }

        /// <summary>
        /// 删除排班按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDelSchedual_Click(object sender, EventArgs e)
        {
            if (CurDocSchedual.Flag==1 && CurDocSchedual.SchedualDate.Date < DateTime.Now.Date)
            {               
                MessageBoxEx.Show("不能删除以往出诊排班记录");
                return;
            }

            if (MessageBoxEx.Show("确定要删除该排班记录吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if ((bool)InvokeController("DeleteSchedual"))
            {
                InvokeController("GetSchedualMoreDate");//获取排班信息
            }
        }

        /// <summary>
        /// 排班科室选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtSchdualDept_AfterSelectedRow(object sender, object SelectedValue)
        {
            DataRow dr = (DataRow)SelectedValue;
            BindSchedualDoc(Convert.ToInt32( dr["DeptID"]));
            txtSchedualDoc.Focus();
        }

        /// <summary>
        /// 排班医生选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtSchedualDoc_AfterSelectedRow(object sender, object SelectedValue)
        {
            btnSaveSchedual.Focus();
            DataRow dr = (DataRow)SelectedValue;           
            txtDocProfessor.Text = dr["JobTitle"]==null ?"未知" : dr["JobTitle"].ToString();
            if (txtSchdualDept.Text.Trim() == string.Empty)
            {
                txtSchdualDept.MemberValue = dr["DeptID"];
            }
        }

        /// <summary>
        /// 排班时段KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbSchedualTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSchdualDept.Focus();
            }
        }

        /// <summary>
        /// 排班日期PreviewKeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dtpSchedualDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dtpSchedualDate.Text != string.Empty)
                {
                    cmbSchedualTime.Focus();
                }
            }
        }

        /// <summary>
        /// 查询科室AfterSelectedRow事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtQueryDept_AfterSelectedRow(object sender, object SelectedValue)
        {
            DataRow dr = (DataRow)SelectedValue;
            BindQueryDoc(Convert.ToInt32(dr["DeptID"]));
        }

        /// <summary>
        /// 排班信息网格CurrentCellChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgSchedualData_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgSchedualData.CurrentCell != null)
            {
                BindSchedualDoc(-1);
                int rowindex = dgSchedualData.CurrentCell.RowIndex;
                DataView dv = (DataView)dgSchedualData.DataSource;
                DataTable dt = dv.ToTable();
                OP_DocSchedual docschedual = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<OP_DocSchedual>(dt, rowindex);
                CurDocSchedual = docschedual;
            }
        }
    }
}
