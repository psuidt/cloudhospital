using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 执行单打印
    /// </summary>
    public partial class FrmExecBillRecord : BaseFormBusiness, IExecBillRecord
    {
        /// <summary>
        /// 执行单数据
        /// </summary>
        private DataTable dtExcuteLis = new DataTable();

        /// <summary>
        /// 报表参数集合
        /// </summary>
        private Dictionary<string, object> myDictionary;

        /// <summary>
        /// 报表编码
        /// </summary>
        private int iReportType = -1;

        /// <summary>
        /// 机构名
        /// </summary>
        private string workName = string.Empty;

        /// <summary>
        /// 操作员名
        /// </summary>
        private string empName = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmExecBillRecord()
        {
            InitializeComponent();
            grdOrderList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintGroupLine);
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmExecBillRecord_OpenWindowBefore(object sender, EventArgs e)
        {
            grdPatList.DataSource = null;
            grdOrderList.DataSource = null;

            dTimeFee.Value = DateTime.Now;
            InvokeController("GetDeptList");
            InvokeController("GetReportTypeList");
            workName = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
            empName = (InvokeController("this") as AbstractController).LoginUserInfo.EmpName;
            myDictionary = new Dictionary<string, object>();
            myDictionary.Add("HospitalName", workName);
            myDictionary.Add("Printer", empName);

            // 打开界面默认执行查询操作
            btnQuery_Click(null, null);
        }

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        /// <param name="iDeptId">默认科室ID</param>
        public void Bind_DeptList(DataTable dtDept, int iDeptId)
        {
            cmbDept.Items.Clear();
            foreach (DataRow dr in dtDept.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["Name"].ToString();
                cbxItem.Tag = dr["DeptId"];
                cmbDept.Items.Add(cbxItem);
            }

            if (dtDept.Rows.Count > 0)
            {
                cmbDept.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定执行单类型
        /// </summary>
        /// <param name="dtReport">执行单列表</param>
        public void Bind_ReportTypeList(DataTable dtReport)
        {
            cmbExecuteType.Items.Clear();
            foreach (DataRow dr in dtReport.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["BillName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name = dr["ReportFile"].ToString();
                cmbExecuteType.Items.Add(cbxItem);
            }

            if (dtReport.Rows.Count > 0)
            {
                cmbExecuteType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定执行单数据
        /// </summary>
        /// <param name="dtPatList">病人列表</param>
        /// <param name="dtExcuteLis">执行单数据</param>
        public void Bind_ExcuteList(DataTable dtPatList, DataTable dtExcuteLis)
        {
            grdPatList.DataSource = dtPatList;
            cbPatList.Checked = true;

            this.dtExcuteLis = dtExcuteLis;
            // 合并同组部分数据数据
            if (dtExcuteLis != null && dtExcuteLis.Rows.Count >= 1)
            {
                int tempGroupID = 0;
                DateTime tempExecDate = DateTime.Now;
                for (int i = 0; i < dtExcuteLis.Rows.Count; i++)
                {
                    //if (Convert.ToDateTime(dtExcuteLis.Rows[i]["EOrderDate"]).ToString("yyyy-MM-dd").Contains("1900-01-01"))
                    //{
                    //    dtExcuteLis.Rows[i]["EOrderDate"] = DBNull.Value;
                    //}
                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(dtExcuteLis.Rows[i]["GroupID"]);
                        tempExecDate = Convert.ToDateTime(dtExcuteLis.Rows[i]["ExecDate"]);
                        continue;
                    }

                    if (Convert.ToInt32(dtExcuteLis.Rows[i]["GroupID"]) == tempGroupID && Convert.ToDateTime(dtExcuteLis.Rows[i]["ExecDate"]) == tempExecDate)
                    {
                        dtExcuteLis.Rows[i]["OrderCategory"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["BedCode"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["PatName"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["SerialNumber"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["DocName"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["Usage"] = DBNull.Value;
                        dtExcuteLis.Rows[i]["Frequency"] = DBNull.Value;
                    }

                    tempGroupID = Convert.ToInt32(dtExcuteLis.Rows[i]["GroupID"]);
                    tempExecDate = Convert.ToDateTime(dtExcuteLis.Rows[i]["ExecDate"]);
                }
            }

            grdOrderList.DataSource = dtExcuteLis;
            cbOrder.Checked = true;
        }

        #region 按钮事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            RefreshExcuteList();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                List<PrintData> iGroupIdList = new List<PrintData>();
                DataTable dt = grdOrderList.DataSource as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["checked"].ToString().Equals("1"))
                    {
                        PrintData printData = new PrintData();
                        printData.groupId = Convert.ToInt32(dr["GroupID"]);
                        printData.execDate = Convert.ToDateTime(dr["ExecDate"]);

                        if (!iGroupIdList.Exists(x => x.groupId == printData.groupId && x.execDate == printData.execDate))
                        {
                            iGroupIdList.Add(printData);
                        }
                    }
                }

                if (iGroupIdList.Count <= 0)
                {
                    MessageBox.Show("无可打印的执行单！");
                }

                PrinterExeCard(iGroupIdList);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        /// <summary>
        /// 刷新执行单数据
        /// </summary>
        public void RefreshExcuteList()
        {
            int iDeptId = -1;
            if (cmbDept.SelectedIndex > -1)
            {
                iDeptId = Convert.ToInt32(((BaseItem)cmbDept.SelectedItem).Tag.ToString());
            }

            iReportType = -1;
            int iTypeId = -1;
            bool typeName = false;
            if (cmbExecuteType.SelectedIndex > -1)
            {
                iTypeId = Convert.ToInt32(((BaseItem)cmbExecuteType.SelectedItem).Tag.ToString());
                iReportType = Convert.ToInt32(((BaseItem)cmbExecuteType.SelectedItem).Name.ToString());
                if (cmbExecuteType.Text.Contains("口服"))
                {
                    typeName = true;
                }
            }

            DateTime dFeeDate = dTimeFee.Value;
            int iOrderCategory = cbLong.Checked == cbTemp.Checked ? -1 : (cbLong.Checked ? 0 : 1);
            int iState = cbUnprint.Checked == cbPrinted.Checked ? -1 : (cbUnprint.Checked ? 0 : 1);
            InvokeController("GetExcuteList", iDeptId, iTypeId, dFeeDate, iOrderCategory, iState, typeName);
        }

        /// <summary>
        /// 打印执行卡
        /// </summary>
        /// <param name="iGroupIdList">勾选的需要打印的组Id</param>
        public void PrinterExeCard(List<PrintData> iGroupIdList)
        {
            //同组的在一个框内  但是每列都要有数据 
            //_dtExcuteLis  报表数据源
            //_iReportType  执行单类型            
            //3015.住院护士执行单服药单   OrderContents   BedCode  PatName    GroupID     Unit    Usage   Frequency
            //3016.住院护士执行单输液卡   OrderContents   BedCode  PatName    GroupID     Unit    Usage   Frequency   OrderContent   Amount   DropSpec    --ordertype
            //3017.住院护士执行单注射单   OrderContents   BedCode  PatName    GroupID     Unit    Usage   Frequency
            //3018.住院护士执行单治疗卡   OrderContents   BedCode  PatName    GroupID     
            DataTable printTable = CreatePrintTable(iGroupIdList);

            if (printTable.Rows.Count > 0)
            {
                GridReport gridreport = new GridReport();
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, iReportType, 0, myDictionary, printTable);
                gridreport.PrintPreview(true);

                List<int> iExecIdList = new List<int>();
                foreach (PrintData printData in iGroupIdList)
                {
                    DataRow[] drs = dtExcuteLis.Select(" GroupId=" + printData.groupId + " and ExecDate='" + printData.execDate + "'");
                    foreach (DataRow dr in drs)
                    {
                        int iExecId = Convert.ToInt32(dr["ID"]);
                        iExecIdList.Add(iExecId);
                    }
                }

                if (iExecIdList.Count > 0)
                {
                    SetPrintState(iExecIdList, 1);
                }
            }
        }

        /// <summary>
        /// 创建打印数据内容DataTable
        /// </summary>
        /// <param name="iGroupIdList">组号List</param>
        /// <returns>打印数据格式</returns>
        private DataTable CreatePrintTable(List<PrintData> iGroupIdList)
        {
            DataTable printTable = dtExcuteLis.Clone();
            //口服
            if (iReportType == 3015)
            {
                foreach (PrintData printData in iGroupIdList)
                {
                    string orderContents = string.Empty;
                    DataRow[] drs = dtExcuteLis.Select(" GroupId=" + printData.groupId + " and ExecDate='" + printData.execDate + "'");

                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            if (i != drs.Length - 1)
                            {
                                orderContents += drs[i]["OrderContent"].ToString() + " \n";
                            }
                            else
                            {
                                orderContents += drs[i]["OrderContent"].ToString();
                            }
                        }

                        drs[0]["OrderContents"] = orderContents;
                        printTable.ImportRow(drs[0]);
                    }
                }
            }
            else if (iReportType == 3016)
            {
                //输液
                foreach (PrintData printData in iGroupIdList)
                {
                    string orderContents = string.Empty;
                    string sAmount = string.Empty;
                    string sEntrust = string.Empty;
                    DataRow[] drs = dtExcuteLis.Select(" GroupId=" + printData.groupId + " and ExecDate='" + printData.execDate + "'");

                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            if (i != drs.Length - 1)
                            {
                                orderContents += drs[i]["OrderContent"].ToString() + " \n" + drs[i]["Amount"].ToString() + " \n" + " \n";
                                sAmount += drs[i]["Amount"].ToString() + " \n";
                                sEntrust += drs[i]["Entrust"].ToString() + " \n";
                            }
                            else
                            {
                                orderContents += drs[i]["OrderContent"].ToString()+"\n"+ drs[i]["Amount"].ToString();
                                sAmount += drs[i]["Amount"].ToString();
                                sEntrust += drs[i]["Entrust"].ToString();
                            }
                        }

                        drs[0]["OrderContents"] = orderContents;
                        drs[0]["Amount"] = sAmount;
                        drs[0]["Entrust"] = sEntrust;
                        printTable.ImportRow(drs[0]);
                    }
                }
            }
            else if (iReportType == 3017)
            {
                //注射
                foreach (PrintData printData in iGroupIdList)
                {
                    string orderContents = string.Empty;
                    string sAmount = string.Empty;
                    string sEntrust = string.Empty;
                    string sUsage = string.Empty;
                    DataRow[] drs = dtExcuteLis.Select(" GroupId=" + printData.groupId + " and ExecDate='" + printData.execDate + "'");

                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            if (i != drs.Length - 1)
                            {
                                orderContents += drs[i]["OrderContent"].ToString() + " \n";
                                sAmount += drs[i]["Amount"].ToString() + " \n";
                                sEntrust += drs[i]["Entrust"].ToString() + " \n";
                                sUsage += drs[i]["Usage"].ToString() + " \n";
                            }
                            else
                            {
                                orderContents += drs[i]["OrderContent"].ToString();
                                sAmount += drs[i]["Amount"].ToString();
                                sEntrust += drs[i]["Entrust"].ToString();
                                sUsage += drs[i]["Usage"].ToString();
                            }
                        }

                        drs[0]["OrderContents"] = orderContents;
                        drs[0]["Amount"] = sAmount;
                        drs[0]["Entrust"] = sEntrust;
                        drs[0]["Usage"] = sUsage;
                        printTable.ImportRow(drs[0]);
                    }
                }
            }
            else if (iReportType == 3018)
            {
                //治疗
                foreach (PrintData printData in iGroupIdList)
                {
                    string orderContents = string.Empty;
                    string sAmount = string.Empty;
                    string sEntrust = string.Empty;
                    string sUsage = string.Empty;
                    DataRow[] drs = dtExcuteLis.Select(" GroupId=" + printData.groupId + " and ExecDate='" + printData.execDate + "'");

                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            if (i != drs.Length - 1)
                            {
                                orderContents += drs[i]["OrderContent"].ToString() + " \n";
                                sAmount += drs[i]["Amount"].ToString() + " \n";
                                sEntrust += drs[i]["Entrust"].ToString() + " \n";
                                sUsage += drs[i]["Usage"].ToString() + " \n";
                            }
                            else
                            {
                                orderContents += drs[i]["OrderContent"].ToString();
                                sAmount += drs[i]["Amount"].ToString();
                                sEntrust += drs[i]["Entrust"].ToString();
                                sUsage += drs[i]["Usage"].ToString();
                            }
                        }

                        drs[0]["OrderContents"] = orderContents;
                        drs[0]["Amount"] = sAmount;
                        drs[0]["Entrust"] = sEntrust;
                        drs[0]["Usage"] = sUsage;
                        printTable.ImportRow(drs[0]);
                    }
                }
            }

            return printTable;
        }

        /// <summary>
        /// 设置执行单打印状态
        /// </summary>
        /// <param name="iExecIdList">待打印的执行单</param>
        /// <param name="state">1-设置已打 0-设置未打</param>
        private void SetPrintState(List<int> iExecIdList, int state)
        {
            if (iExecIdList.Count > 0)
            {
                InvokeController("SetExcuteList", iExecIdList, state);
            }
        }

        #region 方法

        /// <summary>
        /// 绘制分组线
        /// </summary>
        /// <param name="rowIndex">行</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void PaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = colOrderName.Index;
            DataTable docList = grdOrderList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 画线标示
        /// </summary>
        private int mLastDocGroupFlag = 0;

        /// <summary>
        /// 获取分组线符号
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="docList">数据源</param>
        /// <returns>分组线符号</returns>
        private int GetGroupFlag(int rowIndex, DataTable docList)
        {
            int groupID = Convert.ToInt32(docList.Rows[rowIndex]["GroupID"]);
            DateTime exeTime = Convert.ToDateTime(docList.Rows[rowIndex]["ExecDate"]);
            // 判断是否为第一行
            if ((rowIndex - 1) == -1)
            {
                // 如果下一行和第一行是同组
                // 判断是否存在多行数据
                if (rowIndex < docList.Rows.Count - 1)
                {
                    if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID && exeTime == Convert.ToDateTime(docList.Rows[rowIndex + 1]["ExecDate"]))
                    {
                        mLastDocGroupFlag = 1;
                    }
                    else
                    {
                        mLastDocGroupFlag = 0;
                    }
                }
                else
                {
                    mLastDocGroupFlag = 0;
                }
            }
            else
            {
                // 判断是否为最后一行
                if ((rowIndex + 1) == docList.Rows.Count)
                {
                    // 如果上一行和最后一行是同组
                    if (Convert.ToInt32(docList.Rows[rowIndex - 1]["GroupID"]) == groupID && exeTime == Convert.ToDateTime(docList.Rows[rowIndex - 1]["ExecDate"]))
                    {
                        mLastDocGroupFlag = 3;
                    }
                    else
                    {
                        mLastDocGroupFlag = 0;
                    }
                }
                else
                {
                    // 中间的行
                    // 如果上一行绘制的是开始线或者上一行绘制的是中间竖线
                    if (mLastDocGroupFlag == 1 || mLastDocGroupFlag == 2)
                    {
                        // 判断下一行是否还是同组
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID && exeTime == Convert.ToDateTime(docList.Rows[rowIndex + 1]["ExecDate"]))
                        {
                            mLastDocGroupFlag = 2;
                        }
                        else
                        {
                            mLastDocGroupFlag = 3;
                        }
                    }
                    else if (mLastDocGroupFlag == 3 || mLastDocGroupFlag == 0)
                    {
                        // 如果上一行绘制的是结束线，或者没有绘制分组线
                        // 判断下一行是否还是同组
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID && exeTime == Convert.ToDateTime(docList.Rows[rowIndex + 1]["ExecDate"]))
                        {
                            mLastDocGroupFlag = 1;
                        }
                        else
                        {
                            mLastDocGroupFlag = 0;
                        }
                    }
                }
            }

            return mLastDocGroupFlag;
        }
        #endregion

        #region 右键事件

        /// <summary>
        /// 右键的子菜单显示控制
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                string sPrinter = grdOrderList.CurrentRow.Cells["Printer"].Value.ToString();

                if (sPrinter.Equals(string.Empty))
                {
                    设置已打印ToolStripMenuItem.Visible = true;
                    设置未打印ToolStripMenuItem.Visible = false;
                }
                else
                {
                    设置已打印ToolStripMenuItem.Visible = false;
                    设置未打印ToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                设置已打印ToolStripMenuItem.Visible = false;
                设置未打印ToolStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 设置已打印
        /// </summary>
        /// <param name="sender">设置已打印ToolStripMenuItem</param>
        /// <param name="e">事件参数</param>
        private void 设置已打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                DataTable dt = grdOrderList.DataSource as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    int iPatListID = Convert.ToInt32(dt.Rows[grdOrderList.CurrentCell.RowIndex]["PatListID"]);
                    int iGroupID = Convert.ToInt32(dt.Rows[grdOrderList.CurrentCell.RowIndex]["GroupID"]);
                    DateTime execDate = Convert.ToDateTime(dt.Rows[grdOrderList.CurrentCell.RowIndex]["ExecDate"]);
                    string strWhere = string.Format(
                        "PatListID={0} AND GroupID={1} AND ExecDate='{2}'",
                            iPatListID,
                            iGroupID,
                            execDate);
                    DataRow[] drs = dt.Select(strWhere);
                    List<int> iExecIdList = new List<int>();
                    foreach (DataRow dr in drs)
                    {
                        int iExecId = Convert.ToInt32(dr["ID"]);
                        iExecIdList.Add(iExecId);
                    }

                    SetPrintState(iExecIdList, 1);
                }
            }
        }

        /// <summary>
        /// 设置未打印
        /// </summary>
        /// <param name="sender">设置未打印ToolStripMenuItem</param>
        /// <param name="e">事件参数</param>
        private void 设置未打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                DataTable dt = grdOrderList.DataSource as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    int iPatListID = Convert.ToInt32(dt.Rows[grdOrderList.CurrentCell.RowIndex]["PatListID"]);
                    int iGroupID = Convert.ToInt32(dt.Rows[grdOrderList.CurrentCell.RowIndex]["GroupID"]);
                    DateTime execDate = Convert.ToDateTime(dt.Rows[grdOrderList.CurrentCell.RowIndex]["ExecDate"]);
                    string strWhere = string.Format(
                        "PatListID={0} AND GroupID={1} AND ExecDate='{2}'",
                            iPatListID,
                            iGroupID,
                            execDate);
                    DataRow[] drs = dt.Select(strWhere);
                    List<int> iExecIdList = new List<int>();
                    foreach (DataRow dr in drs)
                    {
                        int iExecId = Convert.ToInt32(dr["ID"]);
                        iExecIdList.Add(iExecId);
                    }

                    SetPrintState(iExecIdList, 0);
                }
            }
        }
        #endregion

        #region 网格的选择事件

        /// <summary>
        /// 选中病人
        /// </summary>
        /// <param name="sender">grdPatList</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPatList.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[rowIndex]["checked"]) == 1)
                        {
                            dt.Rows[rowIndex]["checked"] = 0;
                            // 从医嘱列表中去掉对应的数据
                            DataTable commandDt = grdOrderList.DataSource as DataTable;
                            commandDt.TableName = "DocList";
                            DataView view = new DataView(commandDt);
                            string sqlWhere = string.Format("PatListID <> {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListID"]));
                            view.RowFilter = sqlWhere;
                            view.Sort = "ExecDate, GroupID ASC";
                            grdOrderList.DataSource = view.ToTable();
                        }
                        else
                        {
                            dt.Rows[rowIndex]["checked"] = 1;
                            // 将对应病人的数据追加到医嘱列表中

                            //NotCopiedDocDt.TableName = "DocList";
                            DataView view = new DataView(dtExcuteLis);
                            view.RowFilter = string.Format("PatListID = {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListID"]));
                            view.Sort = "ExecDate, GroupID ASC";
                            DataTable commandDt = grdOrderList.DataSource as DataTable;
                            DataTable tempDt = view.ToTable();
                            for (int i = 0; i < tempDt.Rows.Count; i++)
                            {
                                tempDt.Rows[i]["checked"] = 1;
                            }

                            if (commandDt != null)
                            {
                                commandDt.Merge(tempDt);
                            }
                            else
                            {
                                grdOrderList.DataSource = tempDt;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行单表点击事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdOrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                int rowIndex = grdOrderList.CurrentCell.RowIndex;
                DataTable dt = grdOrderList.DataSource as DataTable;
                int iPatListID = Convert.ToInt32(dt.Rows[rowIndex]["PatListID"]);
                int iGroupID = Convert.ToInt32(dt.Rows[rowIndex]["GroupID"]);
                DateTime execDate = Convert.ToDateTime(dt.Rows[rowIndex]["ExecDate"]);
                if (e.ColumnIndex == 0)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string strWhere = string.Format(
                            "PatListID={0} AND GroupID={1} AND ExecDate='{2}'",
                                iPatListID,
                                iGroupID,
                                execDate);
                        DataRow[] arrayDr = dt.Select(strWhere);
                        // 去掉选中
                        if (Convert.ToInt32(dt.Rows[rowIndex]["checked"]) == 1)
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["checked"] = 0;
                                }
                            }
                        }
                        else
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["checked"] = 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 全选病人
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbPatList_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = grdPatList.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["checked"] = cbPatList.Checked ? 1 : 0;
                }
            }

            if (cbPatList.Checked)
            {
                grdOrderList.DataSource = dtExcuteLis;
            }
            else
            {
                grdOrderList.DataSource = null;
            }
        }

        /// <summary>
        /// 全选执行单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbOrder_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = grdOrderList.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["checked"] = cbOrder.Checked ? 1 : 0;
                }
            }
        }
        #endregion

        /// <summary>
        /// 打印数据
        /// </summary>
        public class PrintData
        {
            /// <summary>
            /// 组号ID
            /// </summary>
            public int groupId { get; set; }

            /// <summary>
            /// 执行时间
            /// </summary>
            public DateTime execDate { get; set; }
        }
    }
}
