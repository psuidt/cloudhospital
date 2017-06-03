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
    /// <summary>
    /// 药品汇总帐
    /// </summary>
    public partial class FrmTotalAccount : BaseFormBusiness, IFrmTotalAccount
    {
        /// <summary>
        /// 构造 函数
        /// </summary>
        public FrmTotalAccount()
        {
            InitializeComponent();
            frmCommon.AddItem(dtpBillDate, "dtpBillDate");
            frmCommon.AddItem(cb_Balance, "cb_Balance");
            frmCommon.AddItem(ckNomal, "ckNomal");
            frmCommon.AddItem(ckMonth, "ckMonth");
            frmCommon.AddItem(DeptRoom, "DeptRoom");
            frmCommon.AddItem(LogicType, "LogicType");
            frmCommon.AddItem(cmb_dept, "cmb_dept");
            frmCommon.AddItem(cmb_Type, "cmb_Type");
            frmCommon.AddItem(cmb_CType, "cmb_CType");
            frmCommon.AddItem(txtName, "txtName");
            frmCommon.AddItem(btnQuery, "btnQuery");
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        public DataTable PrintDt;

        #region 函数
        /// <summary>
        /// 绑定业务类型
        /// </summary>
        public void BindLogicType()
        {
            var datasource = new[]
            {
                new { Text = "采购入库汇总", Value = "011" },
                new { Text = "期初入库汇总", Value = "012" },
                new { Text = "返库汇总", Value = "015" },
                new { Text = "内耗出库汇总", Value = "021" },
                new { Text = "报损出库汇总", Value = "022" },
                new { Text = "盘点汇总", Value = "042" },
                new { Text = "调价汇总", Value = "051" },
                new { Text = "门诊发药汇总", Value = "031" },
                new { Text = "门诊退药汇总", Value = "032" },
                new { Text = "住院发药汇总", Value = "033" },
            };

            if (frmName == "FrmTotalAccountDW")
            {
                datasource = new[]
                {
                new { Text = "期初入库汇总", Value = "112" },
                new { Text = "采购入库汇总", Value = "111" },
                new { Text = "按退货汇总", Value = "113" },
                new { Text = "按流通出库汇总", Value = "121" },
                new { Text = "按内耗出库汇总", Value = "122" },
                new { Text = "按报损出库汇总", Value = "123" },
                new { Text = "按退库汇总", Value = "124" },
                new { Text = "按盘点汇总", Value = "142" },
                new { Text = "按调价汇总", Value = "151" },
                };
            }

            LogicType.ValueMember = "Value";
            LogicType.DisplayMember = "Text";
            LogicType.DataSource = datasource;
        }

        /// <summary>
        /// 绑定科室选择框控件
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        public void BindDeptRoom(DataTable dtDept, int loginDeptID)
        {
            DeptRoom.DataSource = dtDept;
            DeptRoom.ValueMember = "DeptID";
            DeptRoom.DisplayMember = "DeptName";
            DataRow[] rows = dtDept.Select("DeptID=" + loginDeptID);
            if (rows.Length > 0)
            {
                DeptRoom.SelectedValue = loginDeptID;
                return;
            }

            if (dtDept.Rows.Count > 0)
            {
                DeptRoom.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        public void BindTypeCombox(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["TypeID"] = 0;
            dr["TypeName"] = "所有药品类型";
            dt.Rows.InsertAt(dr, 0);
            cmb_Type.DataSource = dt;
            cmb_Type.ValueMember = "TypeID";
            cmb_Type.DisplayMember = "TypeName";
            if (dt.Rows.Count > 0)
            {
                cmb_Type.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定月结信息
        /// </summary>
        /// <param name="dt">月结信息</param>
        public void BindBalance(DataTable dt)
        {
            DataTable newdt = new DataTable();
            newdt.Columns.Add("BalanceTime");
            newdt.Columns.Add("BalanceID");
            foreach (DataRow dr in dt.Rows)
            {
                DataRow newdr = newdt.NewRow();
                newdr["BalanceTime"] = dr["BeginTime"] + "' and '" + dr["EndTime"];
                newdr["BalanceID"] = dr["BalanceYear"] + "年" + dr["BalanceMonth"] + "月会计月结";
                newdt.Rows.Add(newdr);
            }

            cb_Balance.ValueMember = "BalanceTime";
            cb_Balance.DisplayMember = "BalanceID";
            cb_Balance.DataSource = newdt;
            if (newdt.Rows.Count > 0)
            {
                cb_Balance.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定来往科室
        /// </summary>
        /// <param name="dt">来往科室数据</param>
        public void BindDept(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["RelationDeptID"] = 0;
            dr["RelationDeptName"] = "所有来往科室";
            dt.Rows.InsertAt(dr, 0);
            cmb_dept.DataSource = dt;
            cmb_dept.ValueMember = "RelationDeptID";
            cmb_dept.DisplayMember = "RelationDeptName";
            if (dt.Rows.Count > 0)
            {
                cmb_dept.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品网格信息
        /// </summary>
        /// <param name="dt">报表数据</param>
        public void BindDgData(DataTable dt)
        {
            PrintDt = dt;
            string currentUserName = (string)InvokeController("GetCurrentUserName");
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", dtpBillDate.Bdate.Value);
            myDictionary.Add("结束时间", dtpBillDate.Edate.Value);
            myDictionary.Add("科室", DeptRoom.Text);
            myDictionary.Add("查询人", currentUserName);
            myDictionary.Add("查询时间", DateTime.Now);
            GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4018, 0, myDictionary, dt);
            axGRDisplayViewer.Report = gridreport.Report;
            axGRDisplayViewer.Start();
            axGRDisplayViewer.Refresh();
        }

        /// <summary>
        /// 绑定供应商
        /// </summary>
        /// <param name="dt">供应商</param>
        public void BindSupport(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["SupplierID"] = 0;
            dr["SupportName"] = "所有供应商";
            dt.Rows.InsertAt(dr, 0);
            cmb_dept.DataSource = dt;
            cmb_dept.ValueMember = "SupplierID";
            cmb_dept.DisplayMember = "SupportName";
            if (dt.Rows.Count > 0)
            {
                cmb_dept.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品子类型下拉框控件
        /// </summary>
        /// <param name="dt">药品子类型</param>
        public void BindChildDrugType(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["CTypeID"] = 0;
            dr["CTypeName"] = "所有药品子类型";
            dt.Rows.InsertAt(dr, 0);
            cmb_CType.DataSource = dt;
            cmb_CType.ValueMember = "CTypeID";
            cmb_CType.DisplayMember = "CTypeName";
            if (dt.Rows.Count > 0)
            {
                cmb_CType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (DeptRoom.SelectedValue != null)
            {
                queryCondition.Add("a.DeptID", DeptRoom.SelectedValue.ToString());
            }

            if (LogicType.SelectedValue != null)
            {
                queryCondition.Add("a.BusiType", LogicType.SelectedValue.ToString());
                switch (LogicType.SelectedValue.ToString())
                {
                    ///采购入库、初期入库、反库
                    case "011":
                    case "012":
                    case "015":
                    case "111":
                    case "112":
                    case "113":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(string.Empty, "a.BillTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                        }
                        else
                        {
                            if (cb_Balance.SelectedValue != null)
                            {
                                queryCondition.Add(string.Empty, "a.BillTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                            }
                        }

                        if (cmb_dept.SelectedValue != null)
                        {
                            if (cmb_dept.SelectedValue.ToString() != "0")
                            {
                                queryCondition.Add("a.SupplierID", cmb_dept.SelectedValue.ToString());
                            }
                        }

                        break;

                    ///内耗出库、报损出库
                    case "021":
                    case "022":
                    case "122":
                    case "123":
                    case "124":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(string.Empty, "a.BillTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                        }
                        else
                        {
                            if (cb_Balance.SelectedValue != null)
                            {
                                queryCondition.Add(string.Empty, "a.BillTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                            }
                        }

                        if (cmb_dept.SelectedValue != null)
                        {
                            if (cmb_dept.SelectedValue.ToString() != "0")
                            {
                                queryCondition.Add("a.ToDeptID", cmb_dept.SelectedValue.ToString());
                            }
                        }

                        break;
                    case "042":
                    case "142":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(string.Empty, "a.AuditTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                        }
                        else
                        {
                            if (cb_Balance.SelectedValue != null)
                            {
                                queryCondition.Add(string.Empty, "a.AuditTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                            }
                        }

                        break;
                    case "051":
                    case "151":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(string.Empty, "a.ExecTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                        }
                        else
                        {
                            queryCondition.Add(string.Empty, "a.ExecTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                        }

                        break;
                    case "031":
                    case "032":
                    case "033":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(string.Empty, "a.DispTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                        }
                        else
                        {
                            if (cb_Balance.SelectedValue != null)
                            {
                                queryCondition.Add(string.Empty, "a.DispTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                            }
                        }

                        break;
                }
            }

            if (cmb_CType.SelectedValue != null)
            {
                if (cmb_CType.SelectedValue.ToString() != "0")
                {
                    queryCondition.Add("d.CTypeID", cmb_CType.SelectedValue.ToString());
                }
            }

            if (!string.IsNullOrEmpty(txtName.Text))
            {
                queryCondition.Add("Search", "(d.ChemName like '%" + txtName.Text + "%' or d.PYCode like '%" + txtName.Text + "%')");
            }

            return queryCondition;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer.Stop();
            if (LogicType.SelectedValue != null)
            {
                switch (LogicType.SelectedValue.ToString())
                {
                    ///采购入库、初期入库、反库
                    case "011":
                    case "012":
                    case "015":
                    case "111":
                    case "112":
                    case "113":
                        InvokeController("GetInStore", frmName);
                        break;
                    ///内耗出库、报损出库
                    case "021":
                    case "022":
                    case "122":
                    case "123":
                    case "124":
                        InvokeController("GetOutStore", frmName);
                        break;
                    ///盘点
                    case "042":
                    case "142":
                        InvokeController("GetCheck", frmName);
                        break;
                    ///调价
                    case "051":
                    case "151":
                        InvokeController("GetAdjPrice", frmName);
                        break;
                    ///门诊发药/退药
                    case "031":
                    case "032":
                        InvokeController("GetOPDisp");
                        break;
                    ///住院发药/退药
                    case "033":
                        InvokeController("GetIPDisp");
                        break;
                    default:
                        cmb_dept.DataSource = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void LogicType_TextChanged(object sender, EventArgs e)
        {
            if (LogicType.SelectedValue != null)
            {
                switch (LogicType.SelectedValue.ToString())
                {
                    case "011":
                    case "012":
                    case "015":
                    case "111":
                    case "112":
                    case "113":
                        InvokeController("GetSupportDic", frmName);
                        break;
                    case "021":
                    case "022":
                    case "122":
                    case "123":
                    case "124":
                        InvokeController("GetDeptRelation", frmName, DeptRoom.SelectedValue.ToString());
                        break;
                    default:
                        cmb_dept.DataSource = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_Type_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Type.SelectedValue != null)
            {
                int typeid = 0;
                if (int.TryParse(cmb_Type.SelectedValue.ToString(), out typeid))
                {
                    InvokeController("GetChildDrugType", cmb_Type.SelectedValue.ToString(), frmName);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ckNomal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNomal.Checked)
            {
                dtpBillDate.Visible = true;
                cb_Balance.Visible = false;
            }
            else
            {
                dtpBillDate.Visible = false;
                cb_Balance.Visible = true;
                InvokeController("GetDGBalance", DeptRoom.SelectedValue.ToString(), frmName);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void DeptRoom_TextChanged(object sender, EventArgs e)
        {
            if (DeptRoom.SelectedValue != null)
            {
                int typeid = 0;
                if (int.TryParse(DeptRoom.SelectedValue.ToString(), out typeid))
                {
                    InvokeController("GetDGBalance", DeptRoom.SelectedValue.ToString(), frmName);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string currentUserName = (string)InvokeController("GetCurrentUserName");
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", dtpBillDate.Bdate.Value);
            myDictionary.Add("结束时间", dtpBillDate.Edate.Value);
            myDictionary.Add("科室", DeptRoom.Text);
            myDictionary.Add("查询人", currentUserName);
            myDictionary.Add("查询时间", DateTime.Now);
            ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4018, 0, myDictionary, PrintDt).PrintPreview(true);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugCollection_OpenWindowBefore(object sender, EventArgs e)
        {
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetDeptRoomData", frmName);
            InvokeController("GetDrugTypeDic", frmName);
            if (cmb_Type.SelectedValue != null)
            {
                InvokeController("GetChildDrugType", cmb_Type.SelectedValue.ToString(), frmName);
            }

            BindLogicType();
            ckNomal.Checked = true;
        }
        #endregion
    }
}
