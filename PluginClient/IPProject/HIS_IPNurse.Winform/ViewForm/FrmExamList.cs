using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 检查检验
    /// </summary>
    public partial class FrmExamList : BaseFormBusiness, IExamList
    {
        /// <summary>
        /// 检查检验List
        /// </summary>
        private DataTable dtExamList = new DataTable();

        /// <summary>
        /// 机构名
        /// </summary>
        private string workName = string.Empty;

        /// <summary>
        /// 操作员ID
        /// </summary>
        private string empName = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmExamList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面打开触发
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmExamList_OpenWindowBefore(object sender, EventArgs e)
        {
            workName = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
            empName = (InvokeController("this") as AbstractController).LoginUserInfo.EmpName;

            grdPatList.DataSource = null;
            grdApplyList.DataSource = null;
            dTApplyDate.Value = DateTime.Now;
            InvokeController("GetDeptList");
            tabControl1.SelectedTabIndex = 0;
        }

        #region 绑定方法

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        /// <param name="iDeptId">默认科室ID</param>
        public void Bind_DeptList(DataTable dtDept, int iDeptId)
        {
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
        /// 加载执行单数据
        /// </summary>
        /// <param name="dtPatList">病人列表</param>
        /// <param name="dtExcuteList">执行单列表</param>
        public void LoadExamList(DataTable dtPatList, DataTable dtExcuteList)
        {
            grdPatList.DataSource = dtPatList;
            cbPatList.Checked = true;
            dtExamList = dtExcuteList.Clone();

            DataView dv = new DataView(dtExcuteList);
            DataTable dtGroup = dv.ToTable(true, "ApplyHeadID");
            foreach (DataRow dr in dtGroup.Rows)
            {
                DataRow[] drs = dtExcuteList.Select(" ApplyHeadID=" + dr["ApplyHeadID"]);
                string sItems = string.Empty;
                decimal dTotalfee = 0;
                for (int i = 0; i < drs.Length; i++)
                {
                    sItems += drs[i]["ItemName"].ToString() + ",";
                    dTotalfee += Convert.ToDecimal(drs[i]["TotalFee"].ToString());
                }

                drs[0]["ItemName"] = sItems.Substring(0, sItems.Length - 1);
                drs[0]["TotalFee"] = dTotalfee;
                dtExamList.ImportRow(drs[0]);
            }

            switch (tabControl1.SelectedTabIndex)
            {
                case 0:
                    grdApplyList.DataSource = dtExamList;
                    break;
                case 1:
                    grdExamList.DataSource = dtExamList;
                    break;
                case 2:
                    grdTreatList.DataSource = dtExamList;
                    break;
                default:
                    grdExamList.DataSource = dtExamList;
                    break;
            }

            cbApply.Checked = true;
        }
        #endregion

        #region 按钮事件

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            RefreshExamList();
        }

        /// <summary>
        /// 打印执行单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtReport = new DataTable();
            List<int> iApplyHeadIDList = new List<int>();
            switch (tabControl1.SelectedTabIndex)
            {
                case 0: //检查
                    dtReport = (DataTable)grdApplyList.DataSource;
                    break;
                case 1: //检验
                    dtReport = (DataTable)grdExamList.DataSource;
                    break;
                case 2: //治疗
                    dtReport = (DataTable)grdTreatList.DataSource;
                    break;
            }

            foreach (DataRow dr in dtReport.Rows)
            {
                int iApplyHeadID = Convert.ToInt32(dr["ApplyHeadID"]);
                string sAppyContent = string.Empty;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();

                myDictionary.Add("EmpName", empName);
                myDictionary.Add("WorkName", workName);

                myDictionary.Add("PatName", dr["PatName"].ToString());
                myDictionary.Add("Age", GetAge(dr["Age"].ToString()));
                myDictionary.Add("PatSex", dr["Sex"].ToString());
                myDictionary.Add("DiseaseName", dr["EnterDiseaseName"].ToString());
                myDictionary.Add("SerialNumber", dr["CaseNumber"].ToString());
                myDictionary.Add("cureno", dr["SerialNumber"].ToString());
                myDictionary.Add("BedNo", dr["BedNo"].ToString());
                myDictionary.Add("ExcuteDeptName", dr["ExecuteDeptName"]);
                myDictionary.Add("ApplyDeptName", dr["ApplyDeptName"]);
                myDictionary.Add("ApplyDeptDoctor", dr["ApplyDeptDoctor"]);
                myDictionary.Add("ApplyDate", dr["ApplyDate"]);
                myDictionary.Add("TotalFee", Convert.ToDecimal(dr["TotalFee"].ToString()));
                myDictionary.Add("ItemName", dr["ItemName"]);
                sAppyContent = dr["ApplyContent"].ToString();

                switch (tabControl1.SelectedTabIndex)
                {
                    case 0:
                        CheckJson check = JsonConvert.DeserializeObject<CheckJson>(sAppyContent);
                        myDictionary.Add("Assay", check.Assay);
                        myDictionary.Add("Assist", check.Assist);
                        myDictionary.Add("Digest", check.Digest);
                        myDictionary.Add("Signs", check.Signs);
                        myDictionary.Add("Xray", check.Xray);
                        myDictionary.Add("Part", check.Part);
                        EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 2013, 0, myDictionary, null).Print(false);
                        break;
                    case 1:
                        TestJson test = JsonConvert.DeserializeObject<TestJson>(sAppyContent);
                        myDictionary.Add("Sample", test.SampleName);
                        myDictionary.Add("Purpose", test.Goal);
                        EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 2012, 0, myDictionary, null).Print(false);
                        break;
                    case 2:
                        EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 2014, 0, myDictionary, null).Print(false);
                        break;
                }

                iApplyHeadIDList.Add(iApplyHeadID);
            }

            UpdateApplyPrint(iApplyHeadIDList);
        }

        /// <summary>
        /// 打印瓶签
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnPrintLab_Click(object sender, EventArgs e)
        {
            if (grdExamList.Rows.Count > 0)
            {
                DataTable dtLab = (DataTable)grdExamList.DataSource;
                DataView dv = new DataView(dtLab);
                dv.RowFilter = "checked = 1";
                dv.Sort = "SerialNumber ASC";
                DataTable dtReport = dv.ToTable();

                // 做成瓶签打印确认数据
                DataTable protDt = new DataTable();
                protDt.Columns.Add("PatName", typeof(string));
                protDt.Columns.Add("Sex", typeof(string));
                protDt.Columns.Add("BedNo", typeof(string));
                protDt.Columns.Add("Age", typeof(string));
                protDt.Columns.Add("SerialNumber", typeof(string));
                protDt.Columns.Add("Num", typeof(int));

                decimal serialNumber = 0;
                for (int i = 0; i < dtReport.Rows.Count; i++)
                {
                    DataRow dr = protDt.NewRow();
                    // 第一行
                    if (i == 0)
                    {
                        serialNumber = Tools.ToDecimal(dtReport.Rows[i]["SerialNumber"]);
                        dr["PatName"] = dtReport.Rows[i]["PatName"];
                        dr["Sex"] = dtReport.Rows[i]["Sex"];
                        dr["BedNo"] = dtReport.Rows[i]["BedNo"];
                        dr["Age"] = SetAge(dtReport.Rows[i]["Age"].ToString());
                        dr["SerialNumber"] = dtReport.Rows[i]["SerialNumber"];
                        dr["Num"] = 1;
                        protDt.Rows.Add(dr);
                        continue;
                    }

                    if (serialNumber != Tools.ToDecimal(dtReport.Rows[i]["SerialNumber"]))
                    {
                        // 处理完上一个病人的数据，开始处理新病人的数据
                        dr["PatName"] = dtReport.Rows[i]["PatName"];
                        dr["Sex"] = dtReport.Rows[i]["Sex"];
                        dr["BedNo"] = dtReport.Rows[i]["BedNo"];
                        dr["Age"] = SetAge(dtReport.Rows[i]["Age"].ToString());
                        dr["SerialNumber"] = dtReport.Rows[i]["SerialNumber"];
                        dr["Num"] = 1;
                        protDt.Rows.Add(dr);
                    }
                    else
                    {
                        protDt.Rows[protDt.Rows.Count - 1]["Num"] = Tools.ToDecimal(protDt.Rows[protDt.Rows.Count - 1]["Num"]) + 1;
                    }
                    serialNumber = Tools.ToDecimal(dtReport.Rows[i]["SerialNumber"]);
                }

                // 打开瓶签打印确认界面
                if (protDt.Rows.Count > 0)
                {
                    InvokeController("ShowFrmPrintExamConfirm", protDt);
                }

                //EfwControls.CustomControl.ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3019, 0, null, dtReport).PrintPreview(true);
            }
        }

        /// <summary>
        /// 年龄转换
        /// </summary>
        /// <param name="age">转换前的年龄</param>
        /// <returns>转换后的年龄</returns>
        private string SetAge(string age)
        {
            string value = string.Empty;
            if (age.Length > 1)
            {
                if (age.Contains("Y"))
                {
                    value = age.Substring(1) + "岁";
                }
                else if (age.Contains("M"))
                {
                    value = age.Substring(1) + "月";
                }
                else if (age.Contains("D"))
                {
                    value = age.Substring(1) + "天";
                }
                else if (age.Contains("H"))
                {
                    value = age.Substring(1) + "时";
                }
            }

            return value;
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
        /// 选项页选择事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            btnPrintLab.Enabled = tabControl1.SelectedTabIndex == 1;
            RefreshExamList();
        }

        #region 勾选事件

        /// <summary>
        /// 病人全选事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbPatList_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtPatList = grdPatList.DataSource as DataTable;
            if (dtPatList != null && dtPatList.Rows.Count > 0)
            {
                for (int i = 0; i < dtPatList.Rows.Count; i++)
                {
                    dtPatList.Rows[i]["checked"] = cbPatList.Checked ? 1 : 0;
                }
            }

            if (cbPatList.Checked)
            {
                switch (tabControl1.SelectedTabIndex)
                {
                    case 0:
                        grdExamList.DataSource = dtExamList;
                        break;
                    case 1:
                        grdApplyList.DataSource = dtExamList;
                        break;
                    case 2:
                        grdTreatList.DataSource = dtExamList;
                        break;
                }
            }
            else
            {
                grdExamList.DataSource = null;
                grdApplyList.DataSource = null;
                grdTreatList.DataSource = null;
            }
        }

        /// <summary>
        /// 检验全选事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbTest_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtList = grdExamList.DataSource as DataTable;
            if (dtList != null && dtList.Rows.Count > 0)
            {
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    dtList.Rows[i]["checked"] = cbTest.Checked ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 检查全选事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbApply_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtList = grdApplyList.DataSource as DataTable;
            if (dtList != null && dtList.Rows.Count > 0)
            {
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    dtList.Rows[i]["checked"] = cbApply.Checked ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 治疗全选事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbTreat_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtList = grdTreatList.DataSource as DataTable;
            if (dtList != null && dtList.Rows.Count > 0)
            {
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    dtList.Rows[i]["checked"] = cbTreat.Checked ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 病人列表点击事件的勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EfwControls.CustomControl.DataGrid grdList = tabControl1.SelectedTabIndex == 0 ? grdApplyList : (tabControl1.SelectedTabIndex == 1 ? grdExamList : grdTreatList);

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
                            DataTable commandDt = grdList.DataSource as DataTable;
                            commandDt.TableName = "DocList";
                            DataView view = new DataView(commandDt);
                            string sqlWhere = string.Format("SerialNumber <> {0}", dt.Rows[rowIndex]["SerialNumber"].ToString());
                            view.RowFilter = sqlWhere;
                            view.Sort = "SerialNumber, ApplyHeadID ASC";
                            grdList.DataSource = view.ToTable();
                        }
                        else
                        {
                            dt.Rows[rowIndex]["checked"] = 1;
                            // 将对应病人的数据追加到医嘱列表中

                            //NotCopiedDocDt.TableName = "DocList";
                            DataView view = new DataView(dtExamList);
                            view.RowFilter = string.Format("SerialNumber = {0}", dt.Rows[rowIndex]["SerialNumber"].ToString());
                            view.Sort = "SerialNumber, ApplyHeadID ASC";
                            DataTable commandDt = grdList.DataSource as DataTable;
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
                                grdList.DataSource = tempDt;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 数据列表的点击勾选事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EfwControls.CustomControl.DataGrid grdList = (EfwControls.CustomControl.DataGrid)sender;

            if (grdList.Rows.Count > 0)
            {
                int rowIndex = grdList.CurrentCell.RowIndex;
                DataTable dt = grdList.DataSource as DataTable;
                int iApplyHeadID = Convert.ToInt32(dt.Rows[rowIndex]["ApplyHeadID"]);

                if (e.ColumnIndex == 0)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string strWhere = string.Format(" ApplyHeadID={0}", iApplyHeadID);
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
                            // 选中数据
                            //dt.Rows[rowIndex]["CheckFlg"] = 1;
                        }
                    }
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 刷新申请单数据
        /// </summary>
        public void RefreshExamList()
        {
            int iDeptId = -1;
            if (cmbDept.SelectedIndex > -1)
            {
                iDeptId = Convert.ToInt32(((BaseItem)cmbDept.SelectedItem).Tag.ToString());
            }

            int iApplyType = tabControl1.SelectedTabIndex;
            DateTime dApplyDate = dTApplyDate.Value;
            int iOrderCategory = cbLong.Checked == cbTemp.Checked ? -1 : (cbLong.Checked ? 0 : 1);
            int iState = cbUnprint.Checked == cbPrinted.Checked ? -1 : (cbUnprint.Checked ? 0 : 1);
            InvokeController("GetExamList", iDeptId, iApplyType, dApplyDate, iOrderCategory, iState);
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">待处理的年龄</param>
        /// <returns>处理后的年龄值</returns>
        public string GetAge(string age)
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
        /// 更新打印状态
        /// </summary>
        /// <param name="iApplyHeadIDList">执行单列表</param>
        private void UpdateApplyPrint(List<int> iApplyHeadIDList)
        {
            if (iApplyHeadIDList.Count > 0)
            {
                InvokeController("UpdateApplyPrint", iApplyHeadIDList);
            }
        }

        #endregion

        /// <summary>
        /// 测试Json
        /// </summary>
        public class TestJson
        {
            /// <summary>
            /// 目标
            /// </summary>
            public string Goal { get; set; }

            /// <summary>
            ///例子
            /// </summary>
            public int Sample { get; set; }

            /// <summary>
            /// 例子名
            /// </summary>
            public string SampleName { get; set; }
        }

        /// <summary>
        /// 检查Json
        /// </summary>
        public class CheckJson
        {
            /// <summary>
            /// 病史
            /// </summary>
            public string Digest { get; set; }

            /// <summary>
            /// 体征
            /// </summary>
            public string Signs { get; set; }

            /// <summary>
            /// X线结果
            /// </summary>
            public string Xray { get; set; }

            /// <summary>
            /// 化验结果
            /// </summary>
            public string Assay { get; set; }

            /// <summary>
            /// 辅助检查
            /// </summary>
            public string Assist { get; set; }

            /// <summary>
            /// 检查部位
            /// </summary>
            public string Part { get; set; }
        }
    }
}
