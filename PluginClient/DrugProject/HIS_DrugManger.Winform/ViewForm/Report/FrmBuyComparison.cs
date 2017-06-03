using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.Report;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmBuyComparison : BaseFormBusiness, IFrmBuyComparison
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBuyComparison()
        {
            InitializeComponent();           
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        public DataTable PrintDt;

        #region 接口 
        /// <summary>
        /// 绑定科室选择框控件
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        public void BindDeptRoom(DataTable dtDept, int loginDeptID)
        {
            CmbDeptRoom.DataSource = dtDept;
            CmbDeptRoom.ValueMember = "DeptID";
            CmbDeptRoom.DisplayMember = "DeptName";
            DataRow[] rows = dtDept.Select("DeptID=" + loginDeptID);
            if (rows.Length > 0)
            {
                CmbDeptRoom.SelectedValue = loginDeptID;
                return;
            }

            if (dtDept.Rows.Count > 0)
            {
                CmbDeptRoom.SelectedIndex = 0;
            }
        }                  

        /// <summary>
        /// 绑定报表数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindGridData(DataTable dt)
        {
            PrintDt = dt;
            DateTime dtTemp = Convert.ToDateTime(dtYearMonth.Text + "-01");
            //本月第一天时间    
            DateTime dtFirst = dtTemp.AddDays(-(dtTemp.Day) + 1);

            //将本月月数+1  
            DateTime dt2 = dtTemp.AddMonths(1);

            //本月最后一天时间  
            DateTime dtLast = dt2.AddDays(-(dtTemp.Day));
            string currentUserName = (string)InvokeController("GetCurrentUserName");
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", dtFirst);
            myDictionary.Add("结束时间", dtLast);
            myDictionary.Add("科室", CmbDeptRoom.Text);
            myDictionary.Add("查询人", currentUserName);
            myDictionary.Add("查询时间", DateTime.Now);
            GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4028, 0, myDictionary, dt);
            axGRDisplayViewer.Report = gridreport.Report;
            axGRDisplayViewer.Start();
            axGRDisplayViewer.Refresh();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        ///查询事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            int deptId = CmbDeptRoom.SelectedValue!=null?Convert.ToInt32(CmbDeptRoom.SelectedValue):0;
            string yearMonth = dtYearMonth.Text;
            string drugName = txtDrugName.Text.Trim();
            axGRDisplayViewer.Stop();
            InvokeController("GetBuyComparison", deptId, yearMonth, drugName);
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime dtTemp = Convert.ToDateTime(dtYearMonth.Text + "-01");
            //本月第一天时间    
            DateTime dtFirst = dtTemp.AddDays(-(dtTemp.Day) + 1);

            //将本月月数+1  
            DateTime dt2 = dtTemp.AddMonths(1);

            //本月最后一天时间  
            DateTime dtLast = dt2.AddDays(-(dtTemp.Day));
            string currentUserName = (string)InvokeController("GetCurrentUserName");
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", dtFirst);
            myDictionary.Add("结束时间", dtLast);
            myDictionary.Add("科室", CmbDeptRoom.Text);
            myDictionary.Add("查询人", currentUserName);
            myDictionary.Add("查询时间", DateTime.Now);
            ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4028, 0, myDictionary, PrintDt).PrintPreview(true);
        }

        /// <summary>
        /// 打开窗体加载事件
        /// </summary>
        /// <param name="sender">事件</param>
        /// <param name="e">参数</param>
        private void FrmDrugSortBill_OpenWindowBefore(object sender, EventArgs e)
        {            
            InvokeController("GetDeptRoomData", frmName);
            dtYearMonth.Value = DateTime.Now;
        }
        #endregion
    }
}
