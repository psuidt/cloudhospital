using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    /// <summary>
    /// 门诊收费
    /// </summary>
    public partial class FrmBalance : BaseFormBusiness,IFrmBalance
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 0收费 1退费
        /// </summary>
        private int balanceMode ;

        /// <summary>
        /// 0收费 1退费
        /// </summary>
        public int BalanceMode
        {
            get
            {
                return balanceMode;
            }

            set
            {
                balanceMode = value;
            }
        }

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
        private string medicardReadInfo;

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
        public string MedicardReadInfo
        {
            get
            {
                return medicardReadInfo;
            }

            set
            {
                medicardReadInfo = value;
            }
        }

        /// <summary>
        /// 显示医保刷卡显示信息
        /// </summary>
        public string SetMedicardReadInfo
        {
            set
            {
                lblMedicardInfo.Text = value;
            }
        }

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="enabled">是否可以true可用false不可用</param>
        public void SetBarEnabled(bool enabled)
        {
            bar1.Enabled = enabled;
        }

        #region 接口参数  
        /// <summary>
        /// 医生给病人下的诊断列表
        /// </summary>
        private List<OPD_DiagnosisRecord> diagnosisList;

        /// <summary>
        /// 医生给病人下的诊断列表
        /// </summary>
        public List<OPD_DiagnosisRecord> DiagnosisList
        {
            get
            {
                return diagnosisList;
            }

            set
            {
                diagnosisList = value;
            }
        }

        /// <summary>
        /// 是否是医生处方
        /// </summary>
        private int isaddOpdDocotr;

        /// <summary>
        /// 是否是医生处方
        /// </summary>
        public int IsAddOpdoctor
        {
            get
            {
                return isaddOpdDocotr;
            }

            set
            {
                isaddOpdDocotr = value;
            }
        }

        /// <summary>
        /// 上次收费信息
        /// </summary>
        private string strpayinfo;

        /// <summary>
        /// 上次收费信息
        /// </summary>
        public string StrPayInfo
        {
            get
            {
                return strpayinfo;
            }

            set
            {
                strpayinfo = value;
            }
        }

        /// <summary>
        /// 设置上次收费信息面板是否可见
        /// </summary>
        public bool SetexPlLastPayExpanded
        {
            set
            {
                lblPayInfo.Text = string.IsNullOrEmpty(strpayinfo) ? string.Empty: strpayinfo;
                exPlLastPay.Expanded = value;
            }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get
            {
                return txtAge.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtAge.Text);
            }

            set
            {
                txtAge.Text = value.ToString();
            }
        }

        /// <summary>
        /// 年龄单位
        /// </summary>
        public string AgeUnit
        {
            get
            {
                return cmbAgeUnit.Text;
            }

            set
            {
                cmbAgeUnit.Text = value;
            }
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
        {
            if (dgPrescription != null && dgPrescription.Rows.Count > 0)
            {
                for (int i = 0; i < dgPrescription.Rows.Count; i++)
                {
                    try
                    {
                        int subtotal_flag = Convert.IsDBNull(dgPrescription[SubTotalFlag.Name, i].Value) ? 0 : Convert.ToInt32(dgPrescription[SubTotalFlag.Name, i].Value);
                        int modify_flag = Convert.IsDBNull(dgPrescription[ModifyFlag.Name, i].Value) ? 0 : Convert.ToInt32(dgPrescription[ModifyFlag.Name, i].Value);
                        int feeitemheadid = Convert.IsDBNull(dgPrescription[FeeItemHeadID.Name, i].Value) ? 0 : Convert.ToInt32(dgPrescription[FeeItemHeadID.Name, i].Value);
                        if (subtotal_flag == 1)
                        {
                            dgPrescription.SetRowColor(i, Color.Blue, Color.LightYellow);
                            dgPrescription[TotalFee.Name, i].Style.Font = new Font("宋体", 10F, FontStyle.Bold);
                            dgPrescription[ExecDetpName.Name, i].Style.Font = new Font("宋体", 10F, FontStyle.Bold);
                            dgPrescription[RefundFee.Name, i].Style.Font = new Font("宋体", 10F, FontStyle.Bold);
                        }
                        else
                        {
                            int doc_pres_flag = Convert.ToInt32(dgPrescription[DocPresHeadID.Name, i].Value);
                            if (doc_pres_flag != 0)
                            {
                                dgPrescription.SetRowColor(i, Color.Gray, Color.White);
                            }
                            else
                            {
                                if (modify_flag == 1 && feeitemheadid > 0)
                                {
                                    dgPrescription.SetRowColor(i, Color.LightSalmon, true);
                                }
                                else if (modify_flag == 1)
                                {
                                    dgPrescription.SetRowColor(i, Color.Black, true);
                                }
                            }
                        }
                    }
                    catch
                    {
                        dgPrescription.SetRowColor(i, Color.Black, true);
                    }
                }
            }
        }

        /// <summary>
        /// 病人对象
        /// </summary>
        private OP_PatList curPatList;

        /// <summary>
        /// 当前操作病人对象
        /// </summary>
        public OP_PatList CurPatList
        {
            get
            {                
                curPatList.DiseaseName = txtDialog.Text.Trim();
                curPatList.DiseaseCode = txtDialog.MemberValue == null ? "0" : txtDialog.MemberValue.ToString();
                return curPatList;
            }

            set
            {
                curPatList = value;
                txtVistitNo.Text = curPatList.VisitNO;
                txtPatName.Text = curPatList.PatName;              
                cmbPatSex.Text = curPatList.PatSex;
                cmbPatType.SelectedValue = curPatList.PatTypeID;
                txtWorkUnit.Text = curPatList.WorkUnit;              
                txtDialog.Text = curPatList.DiseaseName;
                txtAllergic.Text = curPatList.Allergies;
                if (curPatList.PatListID == 0)
                {
                    txtPresDept.MemberValue = string.Empty;
                    txtPresDoc.MemberValue = string.Empty;
                    txtDialog.MemberValue = string.Empty;
                }
                else
                {
                    txtPresDept.MemberValue = curPatList.CureDeptID;
                    txtPresDoc.MemberValue = curPatList.CureEmpID;
                    txtDialog.MemberValue = curPatList.DiseaseCode;
                }
            }
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        public void CloseBalanceForm()
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 获取选择的结算的病人类型Id
        /// </summary>
        public int CostPatTypeid
        {
            get
            {
                return cmbPatType.SelectedValue == null ? 0 : Convert.ToInt32(cmbPatType.SelectedValue);
            }

            set
            {
                cmbPatType.SelectedValue = value;
            }
        }

        /// <summary>
        /// 获取开方科室ID
        /// </summary>
        public int GetPresDeptid
        {
            get
            {
                return txtPresDept.MemberValue == null ? 0 : Convert.ToInt32(txtPresDept.MemberValue);
            }

            set
            {
                txtPresDept.MemberValue = value;
            }
        }

        /// <summary>
        /// 获取开方医生ID
        /// </summary>
        public int GetPresDocid
        {
            get
            {
                return txtPresDoc.MemberValue == null ? 0 : Convert.ToInt32(txtPresDoc.MemberValue);
            }

            set
            {
                txtPresDoc.MemberValue = value;
            }
        }

        /// <summary>
        /// 开方医生姓名
        /// </summary>
        public string GetPresDocName
        {
            get
            {
                return txtPresDoc.Text.Trim();
            }
        }

        /// <summary>
        /// 开方科室名称
        /// </summary>
        public string GetPresDeptName
        {
            get
            {
                return txtPresDept.Text.Trim();
            }
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="dtCardType">卡类型</param>
        public void LoadCardType(DataTable dtCardType)
        {
            cmbCardType.DataSource = dtCardType;
            cmbCardType.ValueMember = "cardtype";
            cmbCardType.DisplayMember = "cardtypename";
        }

        /// <summary>
        /// 设置卡号焦点
        /// </summary>
        public void CardNoFocus()
        {
            txtCardNO.Focus();
        }

        /// <summary>
        /// 分方后设置当前行
        /// </summary>
        /// <param name="rowIndex">当前选中行</param>
        public void SetDgPresfocus(int rowIndex)
        {
            dgPrescription.CurrentCell = dgPrescription[ItemID.Index, rowIndex];
        }

        /// <summary>
        /// 初始化卡类型下拉项
        /// </summary>
        public void SetCardTypeSelIndex()
        {
            if (cmbCardType != null)
            {
                if (cmbCardType.DataSource != null)
                {
                    cmbCardType.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 获取病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        public void LoadPatType(DataTable dtPatType)
        {
            cmbPatType.DataSource = dtPatType;
            cmbPatType.ValueMember = "PatTypeID";
            cmbPatType.DisplayMember = "PatTypeName";
        }

        /// <summary>
        /// 绑定诊断输入
        /// </summary>
        /// <param name="dtDisease">诊断数据</param>
        public void LoadDiagnose(DataTable dtDisease)
        {
            txtDialog.DisplayField = "Name";
            txtDialog.MemberField = "ICDCode";
            txtDialog.CardColumn = "Name|名称|auto";
            txtDialog.QueryFieldsString = "Name,pycode,wbcode";
            txtDialog.ShowCardHeight = 160;
            txtDialog.ShowCardWidth = 140;
            txtDialog.ShowCardDataSource = dtDisease;
        }

        /// <summary>
        /// 绑定开方科室
        /// </summary>
        /// <param name="dtPrescDepts">科室数据</param>
        public void LoadPrescDepts(DataTable dtPrescDepts)
        {
            txtPresDept.DisplayField = "Name";
            txtPresDept.MemberField = "deptid";
            txtPresDept.CardColumn = "Name|名称|auto";
            txtPresDept.QueryFieldsString = "Name,pym,wbm";
            txtPresDept.ShowCardHeight = 130;
            txtPresDept.ShowCardWidth = 140;
            txtPresDept.ShowCardDataSource = dtPrescDepts;
        }

        /// <summary>
        /// 绑定开方医生
        /// </summary>
        /// <param name="dtPrescDoctors">医生数据</param>
        public void LoadPrescDoctors(DataTable dtPrescDoctors)
        {
            txtPresDoc.DisplayField = "Name";
            txtPresDoc.MemberField = "EmpID";
            txtPresDoc.CardColumn = "Name|姓名|auto";
            txtPresDoc.QueryFieldsString = "Name,pym,wbm";
            txtPresDoc.ShowCardHeight = 160;
            txtPresDoc.ShowCardWidth = 140;
            txtPresDoc.ShowCardDataSource = dtPrescDoctors;
        }

        /// <summary>
        /// 诊疗卡号
        /// </summary>
        public string CardNo
        {
            get
            {
                return txtCardNO.Text.Trim();
            }

            set
            {
                txtCardNO.Text = value;
            }
        }

        /// <summary>
        /// 当前票据号
        /// </summary>
        public string CurInvoiceNO
        {
            get
            {
                return txtCurInvoice.Text.Trim();
            }

            set
            {
                txtCurInvoice.Text = value;
            }
        }

        /// <summary>
        /// 所有处方总金额
        /// </summary>
        public decimal AllPrescriptionTotalFee
        {
            get
            {
                return Convert.ToDecimal(lblAllTotalFee.Text);
            }

            set
            {
                lblAllTotalFee.Text = value.ToString("0.00");
            }
        }

        /// <summary>
        /// 当前所有处方
        /// </summary>
        public DataTable Prescriptions
        {
            get
            {
                return (DataTable)dgPrescription.DataSource;
            }          
        }

        /// <summary>
        /// 绑定处方数据
        /// </summary>
        /// <param name="dt">处方数据</param>
        public void SetPresDataSource(DataTable dt)
        {
            dgPrescription.EndEdit();           
            dgPrescription.DataSource = dt;
            SetGridColor();
        }

        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="readonlyType">只读类型</param>
        public void SetReadOnly(ReadOnlyType readonlyType)
        {
            if (readonlyType == ReadOnlyType.全部只读)
            {
                dgPrescription.ReadOnly = true;
            }
            else
            {
                dgPrescription.ReadOnly = false;
                presNO.ReadOnly = true;
                ItemID.ReadOnly = false;
                ItemName.ReadOnly = true;
                Spec.ReadOnly = true;
                RetailPrice.ReadOnly = true;
                PackUnit.ReadOnly = true;
                MiniUnit.ReadOnly = true;
                PresDocName.ReadOnly = true;
                ExecDetpName.ReadOnly = true;               
                TotalFee.ReadOnly = true;
                Selected.ReadOnly = true;
                PresAmount.ReadOnly = true;
                ItemID1.ReadOnly = true;
                if (readonlyType == ReadOnlyType.新开)
                {
                    PackAmount.ReadOnly = true;
                    MiniAmount.ReadOnly = true;
                } 

                if (readonlyType == ReadOnlyType.中草药)
                {
                    PresAmount.ReadOnly = false;
                    PackAmount.ReadOnly = false;
                    MiniAmount.ReadOnly = false;
                }

                if (readonlyType == ReadOnlyType.药品不可拆零)
                {
                    PackAmount.ReadOnly = false;
                    MiniAmount.ReadOnly = true;
                }

                if (readonlyType == ReadOnlyType.药品可拆零)
                {
                    PackAmount.ReadOnly = false;
                    MiniAmount.ReadOnly = false;
                }

                if (readonlyType == ReadOnlyType.项目)
                {
                    PackAmount.ReadOnly = true;
                    MiniAmount.ReadOnly = false;
                }  
                            
                if (readonlyType == ReadOnlyType.不能修改)
                {
                    ItemID.ReadOnly = true;
                    PackAmount.ReadOnly = true;
                    MiniAmount.ReadOnly = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 设置处方费用录入选项卡数据源
        /// </summary>
        /// <param name="dtFeeSource">药品项目数据</param>
        public void BindDrugItemShowCard(DataTable dtFeeSource)
        {
            dgPrescription.SelectionCards[0].BindColumnIndex = ItemID.Index;
            dgPrescription.SelectionCards[0].CardColumn = "ItemCode|编码|100,ItemName|项目名称|auto,Standard|规格|130,UnPickUnit|包装单位|70,SellPrice|单价|50,Address|生产厂家|160,ItemClassName|项目类型|65,StoreAmount|库存数|80,ExecDeptName|执行科室|80";
            dgPrescription.SelectionCards[0].CardSize = new System.Drawing.Size(1050,260);
            dgPrescription.SelectionCards[0].QueryFieldsString = "ItemName,ItemCode,pym,wbm";
            dgPrescription.BindSelectionCardDataSource(0, dtFeeSource);
        }

        #region 按钮事件
        /// <summary>
        /// 收费按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnBalance_Click(object sender, EventArgs e)
        {
            if (SavePrescriptions())
            {
                InvokeController("PayInfo", 0);
            }
        }

        /// <summary>
        /// 调整票据号按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnAdjustInvoice_Click(object sender, EventArgs e)
        {
            InvokeController("AdjustInvoice");
        }

        /// <summary>
        /// 病人查询按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnGetOutpatient_Click(object sender, EventArgs e)
        {
            InvokeController("GetOutPatient");
        }

        /// <summary>
        /// 退费按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (balanceMode == 1)
            {
                if (dgPrescription != null && dgPrescription.Rows.Count > 0)
                {
                    InvokeController("GetRefundInfo");
                }
            }
        }
        #endregion

        #region 其他事件
        /// <summary>
        /// 窗体打开时加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void FrmBalance_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("BalanceDataInit");
            balanceMode = 0;
            if (cmbCardType.DataSource != null)
            {
                cmbCardType.SelectedIndex = 0;
            }
        } 

        /// <summary>
        /// 设置焦点方法
        /// </summary>
        private void FocusSet()
        {
            if (isaddOpdDocotr==0 && txtDialog.Text.Trim() == string.Empty)
            {
                txtDialog.Focus();
            }
            else
            {
                if (dgPrescription.Rows.Count == 0)
                {
                    btnNew_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 就诊号回车事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void txtVistitNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtVistitNo.Text.Trim() != string.Empty)
                {
                    InvokeController("GetPatListByAccountNO", OP_Enum.MemberQueryType.门诊就诊号,txtVistitNo.Text.Trim());
                    if (txtVistitNo.Text.Trim() == string.Empty)
                    {
                        txtVistitNo.Focus();
                    }
                    else
                    {
                        FocusSet();
                    }
                }
            }
        }

        /// <summary>
        /// 医保读卡按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnReadCard_Click(object sender, EventArgs e)
        {
            InvokeController("GetPatListByAccountNO", OP_Enum.MemberQueryType.医保卡号,string.Empty);
            FocusSet();
        }

        /// <summary>
        /// 卡类型索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void cmbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCardType.Text.Trim() == "发票号")
            {
                txtCardNO.Clear();
                CurPatList = new OP_PatList();
                dgPrescription.DataSource = new DataTable();
                txtVistitNo.ReadOnly = true;
                btnGetOutpatient.Enabled = false;              
                txtDialog.ReadOnly = true;
                balanceMode = 1;              
                btnNew.Enabled = false;
                btnSave.Enabled = false;
                btnRefresh.Enabled = false;
                btnBalance.Enabled = false;
                btnRefund.Enabled = true;
                RefundPackAmount.Visible = true;
                RefundMiniAmount.Visible = true;
                txtPresDoc.ReadOnly = true;
                txtPresDept.ReadOnly = true;
                RefundFee.Visible = true;
                Selected.Visible = false;
                btnReadCard.Enabled = false;
                dgPrescription.ContextMenuStrip = null;
            }
            else
            {
                txtCardNO.Clear();
                CurPatList = new OP_PatList();
                dgPrescription.DataSource = new DataTable();
                txtVistitNo.ReadOnly = false;
                btnGetOutpatient.Enabled = true;               
                txtDialog.ReadOnly = false;
                balanceMode = 0;
                btnNew.Enabled = true;
                btnSave.Enabled = true;
                btnRefresh.Enabled = true;
                btnBalance.Enabled = true;
                btnRefund.Enabled = false;
                RefundPackAmount.Visible = false;
                RefundMiniAmount.Visible = false;
                txtPresDoc.ReadOnly = false;
                txtPresDept.ReadOnly = false;
                RefundFee.Visible = false;
                Selected.Visible = true;
                btnReadCard.Enabled = true;
                dgPrescription.ContextMenuStrip = contextMenuStrip1;
            }
            
            txtCardNO.Focus();
        }

        #endregion

        #region 网格事件
        /// <summary>
        /// 选项卡选择行事件
        /// </summary>
        /// <param name="SelectedValue">选中行</param>
        /// <param name="stop">是否向后跳</param>
        /// <param name="customNextColumnIndex">下一列</param>
        private void dgPrescription_SelectCardRowSelected(object SelectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            dgPrescription.CurrentCellChanged -= dgPrescription_CurrentCellChanged;
            dgPrescription.EndEdit();
            if (!(bool)InvokeController("FillSelectedRowData", (DataRow)SelectedValue, dgPrescription.CurrentCell.RowIndex))
            {
                stop = true;
                dgPrescription.CurrentCell = dgPrescription[ItemID.Index, dgPrescription.CurrentCell.RowIndex];
            }

            dgPrescription.CurrentCellChanged += new EventHandler(dgPrescription_CurrentCellChanged);
            dgPrescription.Refresh();          
        }   

        /// <summary>
        /// 网格单元格改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void dgPrescription_CurrentCellChanged(object sender, EventArgs e)
        {          
            //控制网格读写状态
            if (dgPrescription.CurrentCell != null)
            {
                int currentRowIndex = dgPrescription.CurrentCell.RowIndex;
                InvokeController("SetReadOnly", currentRowIndex);
                if (!this.dgPrescription.CurrentCell.ReadOnly
                   && this.dgPrescription.CurrentCell.ColumnIndex != this.ItemID.Index)
                {
                    this.dgPrescription.BeginEdit(true);
                }
            }
        }

        /// <summary>
        /// 网格单元格回车事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="colIndex">列号</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="jumpStop">是否往后跳</param>
        private void dgPrescription_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (Convert.ToInt32(dgPrescription["SubTotalFlag", rowIndex].Value) != 1)
            {
                if (rowIndex == dgPrescription.Rows.Count - 1)
                {
                    if (colIndex == dgPrescription.Columns[PackAmount.Name].Index)
                    {
                        //如果基本数可输
                        if (!MiniAmount.ReadOnly)
                        {
                            jumpStop = true;
                            dgPrescription.CurrentCell = dgPrescription[MiniAmount.Name, rowIndex];
                        }
                        else
                        {                          
                            //增加一行
                            if ((bool)InvokeController("AllowAddEmptyPrescriptionDetail", rowIndex))
                            {
                                InvokeController("AddEmptyPrescriptionDetail", dgPrescription.Rows.Count);
                               
                                //设置录入焦点
                                jumpStop = true;
                                dgPrescription.CurrentCell = dgPrescription[ItemID.Name, rowIndex + 1];
                            }
                        }
                    }
                   
                    //如果是最后一行
                    if (colIndex == dgPrescription.Columns["MiniAmount"].Index)
                    {
                        if (!PresAmount.ReadOnly)
                        {                           
                            //如果是基本数列,并且是中草药第一行，跳转到付数
                            jumpStop = true;
                            dgPrescription.CurrentCell = dgPrescription["PresAmount", rowIndex];
                        }
                        else
                        {                           
                            //增加一行
                            if ((bool)InvokeController("AllowAddEmptyPrescriptionDetail", rowIndex))
                            {
                                InvokeController("AddEmptyPrescriptionDetail", dgPrescription.Rows.Count);
                              
                                //设置录入焦点
                                jumpStop = true;
                                dgPrescription.CurrentCell = dgPrescription[ItemID.Name, rowIndex + 1];
                            }
                        }
                    }

                    if (colIndex == dgPrescription.Columns[PresAmount.Name].Index)
                    {
                        //增加一行
                        if ((bool)InvokeController("AllowAddEmptyPrescriptionDetail", rowIndex))
                        {
                            InvokeController("AddEmptyPrescriptionDetail", dgPrescription.Rows.Count);
                          
                            //设置录入焦点
                            jumpStop = true;
                            dgPrescription.CurrentCell = dgPrescription[ItemID.Name, rowIndex + 1];
                        }
                    }
                    else if (colIndex == dgPrescription.Columns[ItemID.Name].Index)
                    {
                        //如果是检索列
                        if (Convert.ToInt32(dgPrescription[ItemID.Name, rowIndex].Value) == 0)
                        {
                            jumpStop = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 网格单元格值改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPrescription_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPrescription.CurrentCell != null)
            {
                if (e.ColumnIndex == dgPrescription.Columns[PackAmount.Name].Index || e.ColumnIndex == dgPrescription.Columns[MiniAmount.Name].Index || e.ColumnIndex == dgPrescription.Columns[PresAmount.Name].Index)
                {
                    InvokeController("CalculateRowTotalFee", e.RowIndex);
                    dgPrescription.Refresh();
                }
            }
        }

        /// <summary>
        /// 点击选择列
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPrescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPrescription.CurrentCell.ColumnIndex == dgPrescription.Columns[Selected.Name].Index && e.RowIndex>-1)
            {
                InvokeController("SetPrescriptionSelectedStatus", e.RowIndex);
                InvokeController("CalculateAllPrescriptionFee");
            }
        }
        #endregion

        #region 右键菜单
        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TsAddRow_Click(object sender, EventArgs e)
        {
            if (balanceMode == 0)
            {
                if (dgPrescription == null || dgPrescription.CurrentCell == null)
                {
                    return;
                }

                if (!(bool)InvokeController("AllowBeginPrescriptionHandle"))
                {
                    return;
                }

                if ((bool)InvokeController("AllowAddEmptyPrescriptionDetail", dgPrescription.CurrentCell.RowIndex))
                {
                    InvokeController("AddEmptyPrescriptionDetail", dgPrescription.CurrentCell.RowIndex + 1);
                    dgPrescription.CurrentCell = dgPrescription[ItemID.Name, dgPrescription.CurrentCell.RowIndex + 1];
                }
            }
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TsDeleteRow_Click(object sender, EventArgs e)
        {
            if (balanceMode == 0)
            {
                if (dgPrescription.CurrentCell == null)
                {
                    return;
                }

                if (!(bool)InvokeController("AllowBeginPrescriptionHandle"))
                {
                    return;
                }

                //删除处方明细前判断
                if ((bool)InvokeController("AllowDeletePrescriptionDetail", dgPrescription.CurrentCell.RowIndex))
                {
                    //删除处方明细
                    dgPrescription.CurrentCellChanged -= dgPrescription_CurrentCellChanged;
                    InvokeController("DeletePrescriptionDetail", dgPrescription.CurrentCell.RowIndex);
                    if (dgPrescription.CurrentCell != null)
                    {
                        InvokeController("CalculateRowTotalFee", dgPrescription.CurrentCell.RowIndex);
                    }

                    dgPrescription.CurrentCellChanged += new EventHandler(dgPrescription_CurrentCellChanged);
                }
            }
        }

        /// <summary>
        /// 删除一张处方
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TsDeleteAll_Click(object sender, EventArgs e)
        {
            if (balanceMode == 0)
            {
                if (dgPrescription.CurrentCell == null)
                {
                    return;
                }

                if (!(bool)InvokeController("AllowBeginPrescriptionHandle"))
                {
                    return;
                }

                //删除处方明细前判断
                if ((bool)InvokeController("AllowDeletePrescriptionDetail", dgPrescription.CurrentCell.RowIndex))
                {
                    //删除处方
                    dgPrescription.CurrentCellChanged -= dgPrescription_CurrentCellChanged;
                    InvokeController("DeletePrescription", dgPrescription.CurrentCell.RowIndex);
                    InvokeController("CalculateAllPrescriptionFee");
                    dgPrescription.CurrentCellChanged += new EventHandler(dgPrescription_CurrentCellChanged);
                }
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TsUpdate_Click(object sender, EventArgs e)
        {
            if (balanceMode == 0)
            {
                if (dgPrescription.CurrentCell != null)
                {
                    InvokeController("SetPrescriptionModifyStatus", dgPrescription.CurrentCell.RowIndex);
                    dgPrescription.Refresh();  
                }
            }
        }
        #endregion

        #region 工具栏按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 新开按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (curPatList == null)
            {
                MessageBoxEx.Show("请先选择病人");
                return;
            }

            if (txtPresDoc.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请输入开方医生");
                return;
            }

            if (txtPresDept.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入开方科室");
                return;
            }

            //结束界面编辑模式
            bool bOk = dgPrescription.EndEdit();
            if (bOk)
            {
                dgPrescription.CurrentCellChanged -= dgPrescription_CurrentCellChanged;

                //新开处方前结束原来未结束的处方
                InvokeController("EndPrescription");
                          
                //增加空的处方明细行供录入
                InvokeController("AddEmptyPrescriptionDetail", dgPrescription.Rows.Count);

                //重新绑定事件
                dgPrescription.CurrentCellChanged += new EventHandler(dgPrescription_CurrentCellChanged);

                //定位焦点
                dgPrescription.CurrentCell = dgPrescription[ItemID.Name, dgPrescription.Rows.Count - 1];
            }
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (curPatList == null || curPatList.PatListID == 0)
            {
                return;
            }

            InvokeController("GetPrescriptions");
            InvokeController("CalculateAllPrescriptionFee");
        }

        /// <summary>
        /// 清屏按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClearInfo_Click(object sender, EventArgs e)
        {
            InvokeController("ClearInfo");
            txtCardNO.Focus();
        }

        /// <summary>
        /// 窗体键盘事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmBalance_KeyUp(object sender, KeyEventArgs e)
        {           
            switch (e.KeyCode)
            {
                case Keys.F2: //新开
                    btnNew_Click(sender, e);
                    break;
                case Keys.F3://保存
                   btnSave_Click(sender, e);
                    break;
                case Keys.F5://刷新
                    btnRefresh_Click(sender, e);
                    break;
                case Keys.F8://收银
                    btnBalance_Click(sender, e);
                    break;
                case Keys.F9:
                    btnRefund_Click(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 刷新选项卡按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefreshItem_Click(object sender, EventArgs e)
        {
            dgPrescription.EndEdit();
            InvokeController("GetDrugItemShowCard");
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePrescriptions();          
        }

        /// <summary>
        /// 保存处方方法
        /// </summary>
        /// <returns>bool</returns>
        private bool SavePrescriptions()
        {
            if (dgPrescription.CurrentCell == null || dgPrescription.Rows.Count == 0)
            {
                return false;
            }

            //上了门诊医生站可以不输入诊断
            if (isaddOpdDocotr == 0)
            {
                if (txtDialog.Text.Trim() == string.Empty)
                {
                    MessageBoxEx.Show("请输入诊断");
                    txtDialog.Focus();
                    return false;
                }
            }

            if (!(bool)InvokeController("AllowBeginPrescriptionHandle"))
            {
                return false;
            }

            //结束界面编辑模式
            bool bOk = dgPrescription.EndEdit();
            if (bOk)
            {
                //结束未结束的处方
                InvokeController("EndPrescription");
                if ((bool)InvokeController("CheckDataValidity"))
                {
                    txtCardNO.Focus();

                    //保存处方
                    if ((bool)InvokeController("SavePrescription"))
                    {
                        if (dgPrescription.Rows.Count > 0)
                        {
                            dgPrescription.CurrentCell = dgPrescription[ItemID.Name, dgPrescription.Rows.Count - 1];                            
                        }

                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        #endregion

        /// <summary>
        /// 诊疗卡号keydown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCardNO.Text.Trim() != string.Empty)
                {
                    if (cmbCardType.Text == "发票号")
                    {
                        InvokeController("GetPatListByAccountNO", OP_Enum.MemberQueryType.退费发票号, txtCardNO.Text.Trim());
                    }
                    else
                    {
                        InvokeController("GetPatListByAccountNO", OP_Enum.MemberQueryType.账户号码, txtCardNO.Text.Trim());
                    }

                    if (txtVistitNo.Text.Trim() == string.Empty)
                    {
                        txtCardNO.Clear();
                        txtCardNO.Focus();
                        dgPrescription.DataSource = new DataTable();
                    }
                    else
                    {
                        FocusSet();
                    }
                }
            }
        }

        /// <summary>
        /// 病人类型下拉框改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbPatType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CurPatList != null && CurPatList.PatListID > 0)
            {
                InvokeController("GetPrescriptions");
            }
        }
    }
}
