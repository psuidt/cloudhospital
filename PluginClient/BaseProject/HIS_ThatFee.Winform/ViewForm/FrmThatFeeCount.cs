using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_ThatFee.Winform.IView;

namespace HIS_ThatFee.Winform.ViewForm
{
    public partial class FrmThatFeeCount : BaseFormBusiness, IFrmThatFeeCount
    {
        /// <summary>
        /// 获取执行科室ID
        /// </summary>
        public string ConfirDeptID { get; set; }

        /// <summary>
        /// ///获取开始日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 获取结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 获取打印数据
        /// </summary>
        private DataTable PrintData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmThatFeeCount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        public void BindDept(DataTable dtDept)
        {
            DataRow dr = dtDept.NewRow();
            dr["DeptId"] = 0;
            dr["Name"] = "所有科室";
            dtDept.Rows.InsertAt(dr, 0);
            cmbDept.DataSource = dtDept;
            if (dtDept != null)
            {
                if (dtDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 获取医技确费统计信息
        /// </summary>
        /// <param name="dt">医技确费统计数据</param>
        public void BindThatFeeCount(DataTable dt)
        {
            PrintData = dt;
            Dictionary<string, object> myDictionary = GetDictionary();
            GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.医技工作量统计, 0, myDictionary, dt);
            axGRDisplayViewer.Report = gridreport.Report;
            axGRDisplayViewer.Start();
            axGRDisplayViewer.Refresh();
        }

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        public void GetQueryWhere()
        {
            if (cmbDept.SelectedIndex > 0)
            {
                ConfirDeptID = Tools.ToString(cmbDept.SelectedValue);
            }
            else
            {
                ConfirDeptID = null;
            }

            BeginDate = timeDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            EndDate = timeDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
        }

        /// <summary>
        /// 获取报表参数集合
        /// </summary>
        /// <returns>报表参数集合</returns>
        private Dictionary<string, object> GetDictionary()
        {
            string currentUserName = (InvokeController("this") as AbstractController).LoginUserInfo.EmpName;
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", timeDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            myDictionary.Add("结束时间", timeDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            myDictionary.Add("科室", cmbDept.Text);
            myDictionary.Add("机构名称", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName);
            myDictionary.Add("查询人", currentUserName);
            myDictionary.Add("查询时间", DateTime.Now);
            return myDictionary;
        }

        /// <summary>
        /// 打开窗体前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmThatFeeCount_OpenWindowBefore(object sender, EventArgs e)
        {
            timeDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            timeDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetDept", frmName);
            InvokeController("GetThatFeeCount");
        }

        /// <summary>
        /// 查询点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer.Stop();
            InvokeController("GetThatFeeCount");
        }

        /// <summary>
        /// 关闭点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 打印点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> myDictionary = GetDictionary();
            ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.医技工作量统计, 0, myDictionary, PrintData).PrintPreview(true);
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmThatFeeCount_FormClosed(object sender, FormClosedEventArgs e)
        {
            axGRDisplayViewer.Stop();
        }
    }
}
