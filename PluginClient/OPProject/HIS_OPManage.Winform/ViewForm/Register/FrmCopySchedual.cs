using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmCopySchedual : BaseFormBusiness, IFrmCopySchedual
    {
        /// <summary>
        /// 复制排班的科室ID
        /// </summary>
        public int CopyDeptid
        {
            get
            {
                return txtCopyDept.MemberValue == null ? 0 : Convert.ToInt32(txtCopyDept.MemberValue);
            }

            set
            {
                txtCopyDept.MemberValue = value;
            }
        }

        /// <summary>
        /// 复制排班的医生ID
        /// </summary>
        public int CopyDocid
        {
            get
            {
                return txtCopyDoc.MemberValue == null ? 0 : Convert.ToInt32(txtCopyDoc.MemberValue);
            }

            set
            {
                txtCopyDoc.MemberValue = value;
            }
        }

        /// <summary>
        /// 历史排班最大日期
        /// </summary>
        private DateTime maxCopyDate;

        /// <summary>
        /// 历史排班最大日期
        /// </summary>
        public DateTime MaxCopyDate
        {
            get
            {
                return maxCopyDate;
            }

            set
            {
                maxCopyDate = value;
            }
        }

        /// <summary>
        /// 新排班开始最小日期
        /// </summary>
        private DateTime minNewcopyDate;

        /// <summary>
        /// 新排班开始最小日期
        /// </summary>
        public DateTime MinNewCopyDate
        {
            get
            {
                return minNewcopyDate;
            }

            set
            {
                minNewcopyDate = value;
            }
        }

        /// <summary>
        /// 原开始日期
        /// </summary>
        public DateTime OldBdate
        {
            get
            {
                return dtpCopyBdate.Value;
            }
        }

        /// <summary>
        ///  原结束日期
        /// </summary>
        public DateTime OldEdate
        {
            get
            {
                return dtpCopyEndDate.Value;
            }
        }

        /// <summary>
        /// 新开始日期
        /// </summary>
        public DateTime NewBdate
        {
            get
            {
                return dtpNewScheBdate.Value;
            }
        }

        /// <summary>
        /// 新结束日期
        /// </summary>
        public DateTime NewEdate
        {
            get
            {
                return dtpNewScheEdate.Value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCopySchedual()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 找出一个日期所在星期中星期一的日期
        /// </summary>
        /// <param name="dt">数据</param>
        /// <returns>星期一日期</returns>
        private DateTime GetMondayDate(DateTime dt)
        {
            int dayOfWeek = Convert.ToInt32(dt.DayOfWeek);
            if (dayOfWeek > 0)
            {
                dayOfWeek -= 1;
            }
            else
            {
                //星期日返回的是0
                dayOfWeek = 6;  
            }

            return dt.Add(new TimeSpan(dayOfWeek * (-1), 0, 0, 0, 0));
        }

        /// <summary>
        /// 星期ValueChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtWeek_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int addDays = ((int)(txtWeek.Value - 1)) * (-7); //向前追溯
                DateTime bDate = GetMondayDate(DateTime.Now).AddDays(-7);
                dtpCopyBdate.Value = bDate.Add(new TimeSpan(addDays, 0, 0, 0, 0));
                DateTime time = Convert.ToDateTime(dtpCopyBdate.Value);
            }
            catch (Exception )
            {
                MessageBoxEx.Show("周次选择过大，请重新选择周次");
                txtWeek.Text = "0";
                txtWeek.Focus();
            }
        }

        /// <summary>
        /// 开始日期ValueChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dtpCopyBdate_ValueChanged(object sender, EventArgs e)
        {
            dtpCopyBdate.Value = GetMondayDate(dtpCopyBdate.Value);
            dtpCopyEndDate.Value = dtpCopyBdate.Value.AddDays(6);
        }

        /// <summary>
        /// 排班新开始日期ValueChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dtpNewScheBdate_ValueChanged(object sender, EventArgs e)
        {
            dtpNewScheBdate.Value = GetMondayDate(dtpNewScheBdate.Value);
            dtpNewScheEdate.Value = dtpNewScheBdate.Value.AddDays(6);
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加载科室
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void LoadDept(DataTable dtDept)
        {
            txtCopyDept.DisplayField = "Name";
            txtCopyDept.MemberField = "deptid";
            txtCopyDept.CardColumn = "Name|名称|auto";
            txtCopyDept.QueryFieldsString = "Name,pym,wbm";
            txtCopyDept.ShowCardHeight = 130;
            txtCopyDept.ShowCardWidth = 140;
            txtCopyDept.ShowCardDataSource = dtDept;
        }

        /// <summary>
        /// 加载医生
        /// </summary>
        /// <param name="dtDoctor">医生数据</param>
        public void LoadDoctor(DataTable dtDoctor)
        {
            txtCopyDoc.DisplayField = "Name";
            txtCopyDoc.MemberField = "EmpID";
            txtCopyDoc.CardColumn = "Name|姓名|auto";
            txtCopyDoc.QueryFieldsString = "Name,pym,wbm";
            txtCopyDoc.ShowCardHeight = 130;
            txtCopyDoc.ShowCardWidth = 140;
            txtCopyDoc.ShowCardDataSource = dtDoctor;
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmCopySchedual_Load(object sender, EventArgs e)
        {
            InvokeController("CopySchedualDataInit");
            dtpCopyBdate.Value = DateTime.Now;
            dtpNewScheBdate.Value = MinNewCopyDate;
            txtWeek.Text = "0";
            dtpCopyBdate.MaxDate= GetMondayDate(MaxCopyDate);
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((bool)InvokeController("GetOldSchedualCount"))
            {
                if ((bool)InvokeController("GetCopySchedualCount"))
                {
                    if (MessageBoxEx.Show("新排班日期段内已经存在排班信息，确定复制后原来排班信息将被替换，确定要重新复制吗？",string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                InvokeController("SaveCopySchedualData");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }           
        }

        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmCopySchedual_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
