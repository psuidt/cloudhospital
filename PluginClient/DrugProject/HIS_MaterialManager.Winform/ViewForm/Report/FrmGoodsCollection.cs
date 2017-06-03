using System;
using System.Collections.Generic;
using System.Data;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.Report;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资汇总
    /// </summary>
    public partial class FrmGoodsCollection : BaseFormBusiness, IFrmGoodsCollection
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmGoodsCollection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 报表数据
        /// </summary>
        public DataTable PrintDt;

        #region 函数
        /// <summary>
        /// 初始化业务类型
        /// </summary>
        public void BindLogicType()
        {
            var datasource = new[]
            {
                new { Text = "期初入库汇总", Value = "212" },
                new { Text = "采购入库汇总", Value = "211" },
                new { Text = "按退货汇总", Value = "213" },
                new { Text = "按内耗出库汇总", Value = "222" },
                new { Text = "按报损出库汇总", Value = "223" },
                new { Text = "按退库汇总", Value = "224" },
                new { Text = "按盘点汇总", Value = "242" },
                };

            LogicType.ValueMember = "Value";
            LogicType.DisplayMember = "Text";
            LogicType.DataSource = datasource;
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
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        public void BindMaterialTypeTextBox(int typeId, string typeName)
        {
            txtType.Text = typeName;
            txtType.Tag = typeId;
        }

        /// <summary>
        /// 绑定来往科室
        /// </summary>
        /// <param name="dt">来往科室</param>
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
        /// 绑定报表数据
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
            GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 5006, 0, myDictionary, dt);
            axGRDisplayViewer.Report = gridreport.Report;
            axGRDisplayViewer.Start();
            axGRDisplayViewer.Refresh();
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
                    ///采购入库、初期入库
                    case "211":
                    case "212":
                    case "213":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(
                                string.Empty, "a.BillTime between '" +
                                dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" +
                                dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
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
                    case "222":
                    case "223":
                    case "224":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(
                                string.Empty, "a.BillTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") +
                                "' and '" +
                                dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") +
                                "'");
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
                    case "242":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(
                                string.Empty,
                                "a.AuditTime between '" +
                                dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") +
                                "' and '" +
                                dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") +
                                "'");
                        }
                        else
                        {
                            if (cb_Balance.SelectedValue != null)
                            {
                                queryCondition.Add(string.Empty, "a.AuditTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                            }
                        }

                        break;
                    case "251":
                        if (ckNomal.Checked)
                        {
                            queryCondition.Add(
                                string.Empty,
                                "a.ExecTime between '" + 
                                dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + 
                                "' and '" + 
                                dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + 
                                "'");
                        }
                        else
                        {
                            queryCondition.Add(string.Empty, "a.ExecTime between '" + cb_Balance.SelectedValue.ToString() + "'");
                        }

                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtName.Text))
            {
                queryCondition.Add("Search", "(d.CenterMatName like '%" + txtName.Text + "%' or d.PYCode like '%" + txtName.Text + "%')");
            }

            return queryCondition;
        }
        #endregion

        #region 事件

        /// <summary>
        /// 按自然事件
        /// </summary>
        /// <param name="sender">控件</param>
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
                InvokeController("GetMWBalance", DeptRoom.SelectedValue.ToString(), frmName);
            }
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmGoodsCollection_OpenWindowBefore(object sender, EventArgs e)
        {
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetDeptRoomData", frmName);
            BindLogicType();
            ckNomal.Checked = true;
        }

        /// <summary>
        /// 切换科室
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void DeptRoom_TextChanged(object sender, EventArgs e)
        {
            if (DeptRoom.SelectedValue != null)
            {
                int typeid = 0;
                if (int.TryParse(DeptRoom.SelectedValue.ToString(), out typeid))
                {
                    InvokeController("GetMWBalance", DeptRoom.SelectedValue.ToString(), frmName);
                }
            }
        }

        /// <summary>
        /// 切换往来科室
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void LogicType_TextChanged(object sender, EventArgs e)
        {
            if (LogicType.SelectedValue != null)
            {
                switch (LogicType.SelectedValue.ToString())
                {
                    case "211":
                    case "212":
                    case "213":
                        InvokeController("GetSupportDic", frmName);
                        break;
                    case "222":
                    case "223":
                    case "224":
                        InvokeController("GetDeptRelation", frmName, DeptRoom.SelectedValue.ToString());
                        break;
                    default:
                        cmb_dept.DataSource = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer.Stop();
            if (LogicType.SelectedValue != null)
            {
                switch (LogicType.SelectedValue.ToString())
                {
                    ///采购入库、初期入库
                    case "211":
                    case "212":
                    case "213":
                        InvokeController("GetInStore", frmName, txtType.Tag);
                        break;
                    ///内耗出库、报损出库
                    case "222":
                    case "223":
                    case "224":
                        InvokeController("GetOutStore", frmName, txtType.Tag);
                        break;
                    ///盘点
                    case "242":
                        InvokeController("GetCheck", frmName, txtType.Tag);
                        break;
                    ///调价
                    case "251":
                        InvokeController("GetAdjPrice", frmName, txtType.Tag);
                        break;
                    default:
                        cmb_dept.DataSource = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">控件</param>
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
            ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 5006, 0, myDictionary, PrintDt).PrintPreview(true);
        }
        
        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选择类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnShowTypeTree_Click(object sender, EventArgs e)
        {
            InvokeController("OpenMaterialTypeDialog", frmName);
        }
        #endregion
    }
}
