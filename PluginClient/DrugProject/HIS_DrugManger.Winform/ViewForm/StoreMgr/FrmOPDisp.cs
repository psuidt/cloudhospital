using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.OPManage;
using HIS_Entity.OPManage.BusiEntity;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 门诊发药
    /// </summary>
    public partial class FrmOPDisp : BaseFormBusiness, IFrmOPDisp
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOPDisp()
        {
            InitializeComponent();
        }

        #region 自定义属性方法
        /// <summary>
        /// 获取当前行病人信息
        /// </summary>
        private DataRow drPatList { get; set; }

        /// <summary>
        /// 获取当前病人信息
        /// </summary>
        private DataTable dtPatList { get; set; }

        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        private string strDiseases { get; set; }

        /// <summary>
        /// 发药人
        /// </summary>
        public string SendEmployeeName
        {
            get
            {
                return txtSendName.Text;
            }

            set
            {
                txtSendName.Text = value;
            }
        }

        /// <summary>
        /// 当前科室Id
        /// </summary>
        public int DeptId
        {
            get
            {
                return Convert.ToInt32(cmbStoreRoom.SelectedValue);
            }

            set
            {
                cmbStoreRoom.SelectedValue = value;
            }
        }

        /// <summary>
        /// 取得选择的科室ID
        /// </summary>
        /// <returns>选择的科室ID</returns>
        private int GetSelectedDeptId()
        {
            int deptId = 0;
            if (cmbStoreRoom.SelectedValue != null)
            {
                deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
            }

            return deptId;
        }

        /// <summary>
        /// 转换年龄去显示
        /// </summary>
        /// <param name="age">年龄</param>
        /// <returns>返回年龄字符串</returns>
        private string ConvertAgeToShow(string age)
        {
            if (age == string.Empty)
            {
                return string.Empty;
            }

            string ageNum = age.Substring(1, age.Length - 1);
            string ageUnit = string.Empty;
            string strAge = age.Substring(0, 1);
            if (strAge == "Y")
            {
                ageUnit = "岁";
            }
            else if (strAge == "M")
            {
                ageUnit = "月";
            }
            else if (strAge == "D")
            {
                ageUnit = "天";
            }

            return ageNum + ageUnit;
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="dtPatient">病人信息</param>
        /// <param name="dtDiseases">病人诊断信息</param>
        public void BindPatientInfo(DataTable dtPatient, string dtDiseases)
        {
            dtPatList = dtPatient;
            strDiseases = dtDiseases;
        }

        /// <summary>
        /// 西药/中成药打印
        /// </summary>
        private void PresPrint()
        {
            DataTable dt = new DataTable();
            int rowIndex = -1;
            dt = dgFeeHead.DataSource as DataTable;
            rowIndex = dgFeeHead.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                DataRow dr = dt.Rows[rowIndex];
                DataTable presData = InvokeController("GetPrintPresData", Convert.ToInt32(dr["DocPresHeadID"]), Convert.ToInt32(dr["DocPresNO"])) as DataTable;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                if (presData.Rows.Count > 0)
                {
                    PresPrint presPrint = new PresPrint();
                    InvokeController("GetPatInfo", Tools.ToInt32(dr["PatListID"]));
                    presPrint.PatType = Tools.ToString(dtPatList.Rows[0]["PatTypeName"]);
                    presPrint.PatName = txtName.Text;
                    presPrint.VisitNO = Tools.ToString(dtPatList.Rows[0]["VisitNO"]);
                    presPrint.Sex = txtSex.Text;
                    presPrint.Age = txtAge.Text;
                    presPrint.Diagnosis = strDiseases;
                    presPrint.Address = Tools.ToString(dtPatList.Rows[0]["Address"]);
                    presPrint.TelPhone = Tools.ToString(dtPatList.Rows[0]["Mobile"]);
                    presPrint.PresNO = Tools.ToString(dr["DocPresNO"]);
                    presPrint.PresType = GetTypeName(Tools.ToString(presData.Rows[0]["IsEmergency"]), Tools.ToString(presData.Rows[0]["IsLunacyPosion"]));
                    presPrint.WorkerName = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
                    presPrint.WorkerID = (InvokeController("this") as AbstractController).LoginUserInfo.WorkId.ToString();
                    presPrint.DeptName = Tools.ToString(presData.Rows[0]["DeptName"]);
                    presPrint.PresDate = Convert.ToDateTime(presData.Rows[0]["PresDate"]).ToString("yyyy-MM-dd hh:mm:ss");
                    presPrint.DoctorName = Tools.ToString(presData.Rows[0]["DoctorName"]);
                    presPrint.ChannelName = Tools.ToString(presData.Rows[0]["ChannelName"]);
                    presPrint.FrequencyName = Tools.ToString(presData.Rows[0]["FrequencyName"]);
                    presPrint.DoseNum = Tools.ToString(presData.Rows[0]["DoseNum"]);
                    presPrint.PrintType = Tools.ToString(Tools.ToInt32(dr["PresType"]) - 1);
                    presPrint.PatId = Tools.ToString(dtPatList.Rows[0]["PatListID"]);
                    InvokeController("PrintPres", presPrint, presData);
                }
                else
                {
                    MessageBoxEx.Show("没有可打印的数据");
                }
            }
            else
            {
                MessageBoxEx.Show("没有可打印的数据");
            }
        }

        /// <summary>
        /// 获取打印处方类型
        /// </summary>
        /// <param name="isEmergency">是否急</param>
        /// <param name="isLunacyPosion">是否精</param>
        /// <returns>打印处方类型</returns>
        private string GetTypeName(string isEmergency, string isLunacyPosion)
        {
            string typeName = string.Empty;
            if (isEmergency == "1")
            {
                typeName = "急";
            }
            else if (isLunacyPosion == "1")
            {
                typeName = "精";
            }
            else
            {
                typeName = "普通";
            }

            return typeName;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadInvoiceData()
        {
            int deptId = GetSelectedDeptId();
            string code = txtQuery.Text.Trim();
            InvokeController("QueryPatientInfo", code, deptId, radSended.Checked == true ? 1 : 0);
        }

        /// <summary>
        /// 根据当前行显示病人信息
        /// </summary>
        private void SetPatientInfo()
        {
            DataTable dt = (DataTable)dgInvoice.DataSource;
            DataRow row = dt.Rows[dgInvoice.CurrentCell.RowIndex];
            drPatList = row;

            //病人姓名
            txtName.Text = row["PatName"].ToString();

            //病人性别
            txtSex.Text = row["PatSex"].ToString();

            //年龄
            txtAge.Text = ConvertAgeToShow(row["Age"].ToString());

            //就诊科室
            txtDeptName.Text = row["MedicalDeptName"].ToString();
        }

        /// <summary>
        /// 清除病人信息
        /// </summary>
        private void ClearPatientInfo()
        {
            //病人姓名
            txtName.Text = string.Empty;

            //病人性别
            txtSex.Text = string.Empty;

            //年龄
            txtAge.Text = string.Empty;

            //就诊科室
            txtDeptName.Text = string.Empty;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定病人信息和发票表格
        /// </summary>
        /// <param name="dt">发票表</param>
        public void BindPatientAndInvoceGrid(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                //病人姓名
                txtName.Text = dt.Rows[0]["PatName"].ToString();

                //病人性别
                txtSex.Text = dt.Rows[0]["PatSex"].ToString();

                //年龄
                txtAge.Text = ConvertAgeToShow(dt.Rows[0]["Age"].ToString());

                //就诊科室
                txtDeptName.Text = dt.Rows[0]["MedicalDeptName"].ToString();
                dgInvoice.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgInvoice.Rows[0].Selected = true;
                    dgInvoice.CurrentCell = dgInvoice.Rows[0].Cells[1];
                }
            }
            else
            {
                dgInvoice.DataSource = dt;
                DataTable dtFeeHead = dgFeeHead.DataSource as DataTable;
                if (dtFeeHead != null)
                {
                    dtFeeHead.Rows.Clear();
                    dgFeeHead.DataSource = dtFeeHead;
                }

                DataTable dtFeedDetail = dgFeeDetail.DataSource as DataTable;
                if (dtFeedDetail != null)
                {
                    dtFeedDetail.Rows.Clear();
                    dgFeeDetail.DataSource = dtFeedDetail;
                }
            }
        }

        /// <summary>
        /// 绑定处方表头表格
        /// </summary>
        /// <param name="dt">处方表头</param>
        public void BindFeeHeadGrid(DataTable dt)
        {
            dgFeeHead.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                dgFeeHead.Rows[0].Selected = true;
                dgFeeHead.CurrentCell = dgFeeHead.Rows[0].Cells[1];
            }
        }

        /// <summary>
        /// 绑定处方明细表格
        /// </summary>
        /// <param name="dt">处方明细</param>
        public void BindFeeDetailGrid(DataTable dt)
        {
            dgFeeDetail.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                dgFeeDetail.Rows[0].Selected = true;
                dgFeeDetail.CurrentCell = dgFeeDetail.Rows[0].Cells[1];
            }
        }

        /// <summary>
        /// 绑定药房下拉列表
        /// </summary>
        /// <param name="dt">药房数据</param>
        public void BindStoreRoomComboxList(DataTable dt)
        {
            cmbStoreRoom.DataSource = dt;
            cmbStoreRoom.ValueMember = "DeptID";
            cmbStoreRoom.DisplayMember = "DeptName";
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbStoreRoom.SelectedIndex = 0;
            }
        }
        #endregion

        #region 事件       
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOPDisp_OpenWindowBefore(object sender, EventArgs e)
        {
            //获取药房数据
            InvokeController("GetDrugStoreData");
            LoadInvoiceData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgInvoice.CurrentRow != null && dgFeeHead.Rows.Count > 0 && dgFeeDetail.Rows.Count > 0)
                {
                    if (txtName.Text.Trim() == string.Empty)
                    {
                        MessageBoxEx.Show("请点击单据列表，选择一条结算单");
                        return;
                    }

                    DataTable dtFeeHeads = dgFeeHead.DataSource as DataTable;
                    InvokeController("OPDisp", dtFeeHeads, GetSelectedDeptId());
                    if (chkRefresh.Checked)
                    {
                        LoadInvoiceData();
                    }
                    else
                    {
                        DataTable dtInvoice = dgInvoice.DataSource as DataTable;

                        //移除当前行
                        dtInvoice.Rows[dgInvoice.CurrentRow.Index].Delete();
                        dtInvoice.AcceptChanges();
                        dgInvoice.DataSource = dtInvoice;
                        if (dgInvoice.Rows.Count > 0)
                        {
                            dgInvoice.Rows[0].Selected = true;
                            dgInvoice.CurrentCell = dgInvoice.Rows[0].Cells[1];
                        }
                        else
                        {
                            DataTable dtFeeHead = dgFeeHead.DataSource as DataTable;
                            if (dtFeeHead != null)
                            {
                                dtFeeHead.Rows.Clear();
                                dgFeeHead.DataSource = dtFeeHead;
                            }

                            DataTable dtFeedDetail = dgFeeDetail.DataSource as DataTable;
                            if (dtFeedDetail != null)
                            {
                                dtFeedDetail.Rows.Clear();
                                dgFeeDetail.DataSource = dtFeedDetail;
                            }
                        }
                    }

                    if (dgInvoice.Rows.Count == 0)
                    {
                        //清除病人信息
                        ClearPatientInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            DataTable excuteDt = InvokeController("GetExecuteBills") as DataTable;
            DataTable dt = new DataTable();
            int rowIndex = -1;
            dt = dgFeeHead.DataSource as DataTable;
            rowIndex = dgFeeHead.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                DataRow dr = dt.Rows[rowIndex];
                DataTable presData = InvokeController("GetPrintPresData", Convert.ToInt32(dr["DocPresHeadID"]), Convert.ToInt32(dr["DocPresNO"])) as DataTable;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                if (presData.Rows.Count > 0)
                {
                    DataRow exdr = excuteDt.Select("ChannelID=" + Tools.ToString(presData.Rows[0]["ChannelID"]) + string.Empty).FirstOrDefault();
                    if (exdr != null)
                    {
                        presData.Columns.Add("PatName");
                        presData.Columns.Add("VisitNO");
                        presData.Columns.Add("ExecDate");
                        InvokeController("GetPatInfo", Tools.ToInt32(dr["PatListID"]));
                        for (int i = 0; i < presData.Rows.Count; i++)
                        {
                            presData.Rows[i]["PatName"] = txtName.Text;
                            presData.Rows[i]["VisitNO"] = Tools.ToString(dtPatList.Rows[0]["VisitNO"]);
                            presData.Rows[i]["ExecDate"] = Convert.ToDateTime(presData.Rows[0]["PresDate"]).ToString("yyyy-MM-dd hh:mm:ss");
                        }

                        myDictionary.Add("WorkerName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName);
                        myDictionary.Add("DoseNum", Tools.ToString(presData.Rows[0]["DoseNum"]));
                        myDictionary.Add("Printer", (InvokeController("this") as AbstractController).LoginUserInfo.EmpName);
                        ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.输液卡, 0, myDictionary, presData).PrintPreview(true);
                    }
                    else
                    {
                        MessageBoxEx.Show("没有可打印的输液卡");
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可打印的数据");
                }
            }
            else
            {
                MessageBoxEx.Show("没有可打印的数据");
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            PresPrint();
        }

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
        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int deptId = GetSelectedDeptId();
                string code = txtQuery.Text.Trim();
                InvokeController("QueryPatientInfo", code, deptId, radSended.Checked == true ? 1 : 0);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void radWaitSend_CheckedChanged(object sender, EventArgs e)
        {
            if (radWaitSend.Checked)
            {
                btnSend.Enabled = true;
            }

            if (radSended.Checked)
            {
                btnSend.Enabled = false;
            }

            int deptId = GetSelectedDeptId();
            string code = txtQuery.Text.Trim();
            InvokeController("QueryPatientInfo", code, deptId, radSended.Checked == true ? 1 : 0);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgInvoice_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgInvoice.CurrentCell != null)
            {
                if (dgInvoice.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgInvoice.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgInvoice.DataSource)).Rows[currentIndex];
                    if (currentRow.RowState == DataRowState.Detached || currentRow.RowState == DataRowState.Deleted)
                    {
                        return;
                    }

                    string invoiceNO = currentRow["InvoiceNO"].ToString();
                    InvokeController("GetFeeItemHead", invoiceNO, GetSelectedDeptId());
                    SetPatientInfo();
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgFeeHead_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgFeeHead.CurrentCell != null)
            {
                if (dgFeeHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgFeeHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgFeeHead.DataSource)).Rows[currentIndex];
                    int feeItemHeadID = Convert.ToInt32(currentRow["FeeItemHeadID"]);
                    InvokeController("GetFeeItemDetail", feeItemHeadID);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbStoreRoom_DropDownClosed(object sender, EventArgs e)
        {
            LoadInvoiceData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {
            if (dgInvoice.CurrentRow != null)
            {
                SetPatientInfo();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            //设置选中的部门
            InvokeController("SetSelectedDept", GetSelectedDeptId(), cmbStoreRoom.Text);
            InvokeController("Show", "FrmOPRefund");
        }
        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefrush_Click(object sender, EventArgs e)
        {
            int deptId = GetSelectedDeptId();
            string code = txtQuery.Text.Trim();
            InvokeController("QueryPatientInfo", code, deptId, radSended.Checked == true ? 1 : 0);
        }
    }
}
