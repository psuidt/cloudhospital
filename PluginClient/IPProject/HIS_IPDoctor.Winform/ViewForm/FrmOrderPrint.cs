using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    public partial class FrmOrderPrint : BaseFormBusiness,IFrmOrderPrint
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOrderPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 科室ID
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
        /// 当前选择病人ID
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
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtPatInfo">病人列表信息</param>
        public void BindPatInfo(DataTable dtPatInfo)
        {
            dgvPatInfo.DataSource = dtPatInfo;
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmOrderPrint_OpenWindowBefore(object sender, EventArgs e)
        {
            radInHosp.Checked = true;
            sdtDate.Bdate.Value =Convert.ToDateTime( DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd 00:00:00"));
            sdtDate.Edate.Value= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            sdtDate.Enabled = false;
            InvokeController("GetDeptList",frmName);
        }

        /// <summary>
        /// 在院复选框CheckedChanged事件 
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void radInHosp_CheckedChanged(object sender, EventArgs e)
        {
            sdtDate.Enabled = !radInHosp.Checked;
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetPatInfoList", sdtDate.Bdate.Value, sdtDate.Edate.Value, radOutHosp.Checked,DeptId,frmName);
            LongGridViewResult.Stop();
            LongGridViewResult.Report = null;
            TempGridViewResult.Stop();
            TempGridViewResult.Report = null;
        }

        /// <summary>
        /// 病人列表双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void dgvPatInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPatInfo != null && dgvPatInfo.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgvPatInfo.DataSource;
                int rowindex = dgvPatInfo.CurrentCell.RowIndex;
                CurPatListId = Convert.ToInt32(dt.Rows[rowindex]["patlistid"]);              
                InvokeController("GetPatOrders",CurPatListId);             
            }
        }

        /// <summary>
        /// 报表对象
        /// </summary>
        GridReport gridreport;

        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="dtLongOrder">长期医嘱数据</param>
        /// <param name="dtTempOrder">临时医嘱数据</param>
        public void BindOrderDetail(DataTable dtLongOrder, DataTable dtTempOrder)
        {
            superTabControl1.SelectedTabIndex = 0;
            try
            {
                LongGridViewResult.Stop();
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
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3201, 0, myDictionary, dtLongOrder);
                LongGridViewResult.Report = gridreport.Report;
                LongGridViewResult.Start();
                LongGridViewResult.Refresh();

                TempGridViewResult.Stop();
                GridReport gridreportTemp = new GridReport();
                gridreportTemp = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3203, 0, myDictionary, dtTempOrder);
                TempGridViewResult.Report = gridreportTemp.Report;
                TempGridViewResult.Start();
                TempGridViewResult.Refresh();
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
        /// 获取年龄转换
        /// </summary>
        /// <param name="age">年龄</param>
        /// <returns>转换的形式</returns>
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
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (superTabControl1.SelectedTabIndex == 0)
                {                   
                    LongGridViewResult.PostColumnLayout();
                    LongGridViewResult.Report.PrintPreview(true);
                }
                else
                {
                    TempGridViewResult.PostColumnLayout();
                    TempGridViewResult.Report.PrintPreview(true);
                }
            }
            catch (Exception error)
            {
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 全选/反选事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void chkPat_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = dgvPatInfo.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["Sel"] = chkPat.Checked ? 1 : 0;
                }
            }            
        }

        /// <summary>
        /// 病人列表CellClick事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void dgvPatInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPatInfo.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    int rowIndex = dgvPatInfo.CurrentCell.RowIndex;
                    DataTable dt = dgvPatInfo.DataSource as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[rowIndex]["Sel"]) == 1)
                        {
                            dt.Rows[rowIndex]["Sel"] = 0;                           
                        }
                        else
                        {
                            dt.Rows[rowIndex]["Sel"] = 1; 
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 批量打印按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (dgvPatInfo.Rows.Count > 0)
            {
                DataTable dt = dgvPatInfo.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Sel"]) == 1)
                    {
                        Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                        myDictionary.Add("医院名称", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName);
                        myDictionary.Add("科室", dt.Rows[i]["deptName"]);
                        myDictionary.Add("姓名", dt.Rows[i]["PatName"]);
                        myDictionary.Add("性别", dt.Rows[i]["PatSex"]);
                        myDictionary.Add("年龄", GetAge(dt.Rows[i]["Age"].ToString()));
                        myDictionary.Add("床号", dt.Rows[i]["BedNo"]);
                        myDictionary.Add("住院号", dt.Rows[i]["SerialNumber"]);
                        InvokeController("GetPatOrdersPrint", Convert.ToInt32(dt.Rows[i]["PatListID"]), myDictionary);
                    }
                }
            }
        }

        /// <summary>
        /// 医嘱批量打印
        /// </summary>
        /// <param name="myDictionary">打印参数</param>
        /// <param name="dtLongOrder">长期医嘱数据</param>
        /// <param name="dtTempOrder">临时医嘱数据</param>
        public void PrintAll(Dictionary<string, object> myDictionary, DataTable dtLongOrder, DataTable dtTempOrder)
        {
            EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3201, 0, myDictionary, dtLongOrder).Print(false);
            EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3203, 0, myDictionary, dtTempOrder).Print(false);
        }
    }
}
