using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    public partial class FrmBloodGlucose : BaseFormBusiness, IFrmBloodGlucose
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBloodGlucose()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择的科室ID
        /// </summary>
        public int DeptId
        {
            get
            {
                return Convert.ToInt32(cmbDept.SelectedValue);
            }

            set
            {
                cmbDept.SelectedValue = value;
            }
        }

        /// <summary>
        /// 当前病人ID
        /// </summary>
        private int curpatlistid;

        /// <summary>
        /// 当前病人ID
        /// </summary>
        public int CurPatListId
        {
            get
            {
                return curpatlistid;
            }

            set
            {
                curpatlistid = value;
            }
        }

        /// <summary>
        /// 科室绑定
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void BindDept(DataTable dtDept)
        {
            cmbDept.DisplayMember = "Name";
            cmbDept.ValueMember = "deptID";
            cmbDept.DataSource = dtDept;
        }

        /// <summary>
        /// 病人绑定
        /// </summary>
        /// <param name="dtPatInfo">病人信息</param>
        public void BindPatInfo(DataTable dtPatInfo)
        {
            dgvPatInfo.DataSource = dtPatInfo;
        }

        /// <summary>
        /// 报表对象
        /// </summary>
        GridReport gridreport;

        /// <summary>
        /// 血糖数据绑定
        /// </summary>
        /// <param name="dtBloodGluRecord">血糖数据</param>
        public void BindBloodGluRecord(DataTable dtBloodGluRecord)
        {            
            try
            {
                GridViewResult.Stop();
                this.Cursor = Cursors.WaitCursor;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                int rowindex = dgvPatInfo.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgvPatInfo.DataSource;
                myDictionary.Add("医院名称", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName);
                myDictionary.Add("科室", dt.Rows[rowindex]["deptName"]);
                myDictionary.Add("姓名", dt.Rows[rowindex]["PatName"]);
                myDictionary.Add("性别", dt.Rows[rowindex]["PatSex"]);
                myDictionary.Add("年龄", GetAge(dt.Rows[rowindex]["Age"].ToString()));
                myDictionary.Add("床号", dt.Rows[rowindex]["BedNo"]);
                myDictionary.Add("住院号", dt.Rows[rowindex]["SerialNumber"]);

                gridreport = new GridReport();
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3205, 0, myDictionary, dtBloodGluRecord);
                GridViewResult.Report = gridreport.Report;
                GridViewResult.Start();
                GridViewResult.Refresh();              
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">年龄</param>
        /// <returns>转换后年龄</returns>
        private string GetAge(string age)
        {
            string tempAge = string.Empty;
            if (!string.IsNullOrEmpty(age))
            {
                switch (age.Substring(0, 1))
                {
                    // 岁
                    case "Y":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "岁";
                        }

                        break;
                    // 月
                    case "M":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "月";
                        }

                        break;
                    // 天
                    case "D":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "天";
                        }

                        break;
                    // 时
                    case "H":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "时";
                        }

                        break;
                }
            }

            return tempAge;
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmBloodGlucose_OpenWindowBefore(object sender, EventArgs e)
        {
            radInHosp.Checked = true;
            sdtDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 00:00:00"));
            sdtDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            sdtDate.Enabled = false;
            InvokeController("GetDeptList",frmName);
        }

        /// <summary>
        /// 在院单选框选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radInHosp_CheckedChanged(object sender, EventArgs e)
        {
            sdtDate.Enabled = !radInHosp.Checked;
        }

        /// <summary>
        /// 查找病人列表
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetPatInfoList", sdtDate.Bdate.Value, sdtDate.Edate.Value, radOutHosp.Checked, DeptId, frmName);
            GridViewResult.Stop();
            GridViewResult.Report = null;
            GridViewResult.Stop();
            GridViewResult.Report = null;
        }

        /// <summary>
        /// 查看病人血糖数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgvPatInfo_Click(object sender, EventArgs e)
        {
            if (dgvPatInfo != null && dgvPatInfo.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgvPatInfo.DataSource;
                int rowindex = dgvPatInfo.CurrentCell.RowIndex;
                CurPatListId = Convert.ToInt32(dt.Rows[rowindex]["patlistid"]);
                InvokeController("GetBloodGluRecord", CurPatListId);
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewResult.PostColumnLayout();
                GridViewResult.Report.PrintPreview(true);
            }
            catch (Exception error)
            {
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
    }
}
