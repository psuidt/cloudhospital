using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MIInterface.Winform.IView;
using DevComponents.DotNetBar.Controls;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using HIS_Entity.MIManage.Common;

namespace HIS_MIInterface.Winform.ViewForm
{
    public partial class FrmMITradeQuery : BaseFormBusiness, IFrmMITradeQuery
    {
        Dictionary<string, object> _myDictionary ;
        private int _ybId = 0;
        public FrmMITradeQuery()
        {
            InitializeComponent();
            InitializeGrid();
            
            sGridTradeDetail.PrimaryGrid.CollapseAll();
        }
        private void FrmMITradeQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            InitComBox();
            dptCostDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dptCostDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            //dgvHosp.AutoGenerateColumns = false;
            //dgvMI.AutoGenerateColumns = false;

            cmbMIType.SelectedIndex = 0;
            cmbDept.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;

            _myDictionary = new Dictionary<string, object>();

            _myDictionary.Add("TiTle", "");
            _myDictionary.Add("DateTime", "");
            _myDictionary.Add("DeptName", "");
            _myDictionary.Add("StaffName", "");

            _myDictionary.Add("FeeAll", 0 );
            _myDictionary.Add("FeeFund", 0 );
            _myDictionary.Add("FeeCash", 0 );
            _myDictionary.Add("PersonCountPay", 0 );

            _myDictionary.Add("medicine", 0 );
            _myDictionary.Add("tmedicine", 0 );
            _myDictionary.Add("therb", 0 );
            _myDictionary.Add("examine", 0 );
            _myDictionary.Add("labexam", 0 );
            _myDictionary.Add("treatment", 0 );
            _myDictionary.Add("operation", 0 );
            _myDictionary.Add("material", 0 );
            _myDictionary.Add("other", 0 );
            _myDictionary.Add("xray", 0 );
            _myDictionary.Add("ultrasonic", 0 );
            _myDictionary.Add("CT", 0 );
            _myDictionary.Add("mri", 0 );
            _myDictionary.Add("oxygen",  0 );
            _myDictionary.Add("bloodt", 0 );
            _myDictionary.Add("orthodontics", 0 );
            _myDictionary.Add("prosthesis", 0 );
            _myDictionary.Add("forensic_expertise",0 );

            _myDictionary.Add("diagnosis", 0);
            _myDictionary.Add("medicalservice", 0);
            _myDictionary.Add("commonservice", 0);
            _myDictionary.Add("registfee", 0);

            SetSumInfo();
        }
        /// <summary>
        /// 初始化Grid控件
        /// </summary>
        private void InitializeGrid()
        {
            dgvTradeInfoSummary.AutoGenerateColumns = false;
            GridPanel panel = sGridTradeDetail.PrimaryGrid;

            panel.Name = "Register";
            panel.MinRowHeight = 20;
            panel.AutoGenerateColumns = true;
            panel.ShowCheckBox = false;
            panel.ShowRowInfo = false;
            panel.ShowRowGridIndex = true;
            panel.Caption.Visible = false;
            panel.GroupByRow.Visible = false;

            sGridTradeDetail.DataBindingComplete += SuperGridControl1DataBindingComplete;
        }
        /// <summary>
        /// 初始化下拉控件
        /// </summary>
        private void InitComBox()
        {
            cmbMIType.Items.Clear();
            cmbDept.Items.Clear();

            ComboBoxItem cbxItem1 = new ComboBoxItem();
            cbxItem1.Text = "全部";
            cbxItem1.Tag = -1;
            cmbDept.Items.Add(cbxItem1);            

            InvokeController("M_GetMIType");
        }

        #region 按钮事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            int iPatientType = rbOP.Checked ? 1 : 2;
            string sDeptCode = ((ComboBoxItem)cmbDept.SelectedItem).Text.Trim();
            string sTimeStat = dptCostDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sTimeStop = dptCostDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");

            InvokeController("Mz_GetTradeInfoSummary", _ybId, iPatientType, sDeptCode, sTimeStat, sTimeStop);

            _myDictionary["DateTime"] = sTimeStat+" 到 "+ sTimeStop;
            _myDictionary["DeptName"]= sDeptCode;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            InvokeController("Mz_TradeInfoSummaryprint", _myDictionary);            
            //EfwControls.CustomControl.ReportTool.GetReport("医保收费统计.grf", 0, _myDictionary, null).Print(true);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        #endregion

        /// <summary>
        /// 加载医保类型
        /// </summary>
        /// <param name="dtMemberInfo"></param>
        public void LoadMIType(DataTable dtMemberInfo)
        {
            foreach (DataRow dr in dtMemberInfo.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["MIName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name = dr["MatchMode"].ToString();
                cmbMIType.Items.Add(cbxItem);
            }
        }
        /// <summary>
        /// 加载交易信息
        /// </summary>
        /// <param name="dtTradeInfoSummary"></param>
        public void LoadTradeInfoSummary(DataTable dtTradeInfoSummary)
        {
            DivideTradeInfoSummary(dtTradeInfoSummary);            
        }

        public void LoadTradeRecordInfo(DataTable dtTradeInfo)
        {
            dgvHosp.DataSource = dtTradeInfo;
        }

        public void LoadTradeRecordInfoMI(DataTable dtTradeInfoMI)
        {
            dgvMI.DataSource = dtTradeInfoMI;
        }
        /// <summary>
        /// 将数据分解成4个，用控件的赋值
        /// </summary>
        /// <param name="dtTradeInfoSummary"></param>
        private void DivideTradeInfoSummary(DataTable dtTradeInfoSummary)
        {
            DataView dv = dtTradeInfoSummary.DefaultView;
            string[] colNameRoot1 = { "登记ID", "科室", "HIS流水号", "病人姓名", "医保卡号", "登记时间", "医保登记号", "状态" };
            DataTable Register = dv.ToTable(true, colNameRoot1);
            Register.TableName = "Register";
            //string[] colNameRoot2 = { "RecordId", "RegId", "TradeNO", "TradeTime", "FeeAll", "FeeFund", "FeeCash", "PersonCountPay", "PersonCount", "FeeMIIn", "FeeMIOut", "FeeDeductible", "FeeSelfPay", "FeeBigPay", "FeeBigSelfPay", "FeeOutOFPay", "Feebcbx", "Feejcbz", "FeeNO", "TradeType" };
            string[] colNameRoot2 = { "登记ID", "交易ID",  "交易流水号", "交易类型", "交易总额", "统筹支付", "现金支付", "个帐支付", "个帐余额", "医保内总额", "医保外总额", "起付线", "收费单据号", "交易状态", "交易时间" };
            DataTable PayRecords = dv.ToTable(true, colNameRoot2);
            PayRecords.TableName = "PayRecords";
            string[] colNameRoot3 = { "交易主ID", "项目序号", "处方序号", "HIS编码", "医保编码", "项目名称", "项目类型", "单价", "数量", "总金额", "医保内金额", "医保外金额" };
            DataTable PayRecordDetails = dv.ToTable(true, colNameRoot3);
            PayRecordDetails.TableName = "PayRecordDetails";

            string[] colNameRoot4 = { "交易类型", "登记ID", "交易ID", "交易流水号", "HIS流水号", "收费单据号", "交易状态", "交易总额", "统筹支付", "现金支付", "个帐支付", "报表西药","报表中成药","报表中草药","报表检查费","报表化验","报表治疗费","报表手术费","报表材料费","报表其他","报表放射","报表B超","报表CT","报表核磁","报表输氧费","报表输血费","报表正畸费","报表镶牙费","报表司法鉴定","报表诊察费","报表医事服务费","报表一般诊疗费","报表挂号费"};
            DataTable PayRecordHead = dv.ToTable(true, colNameRoot4);
            PayRecordHead.TableName = "PayRecordHead";

            DataSet ds = new DataSet();

            ds.Tables.Add(Register);
            ds.Tables.Add(PayRecords);
            ds.Tables.Add(PayRecordDetails);

            ds.Relations.Add("1", ds.Tables["Register"].Columns["登记ID"],
                        ds.Tables["PayRecords"].Columns["登记ID"], false);

            ds.Relations.Add("2", ds.Tables["PayRecords"].Columns["交易ID"],
                                   ds.Tables["PayRecordDetails"].Columns["交易主ID"], false);

            dgvTradeInfoSummary.DataSource = PayRecordHead;

            sGridTradeDetail.PrimaryGrid.DataSource = ds;
            sGridTradeDetail.PrimaryGrid.DataMember = "Register";
        }

        #region SuperGridControl1DataBindingComplete
        private void SuperGridControl1DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            GridPanel panel = e.GridPanel;
            //panel.GroupByRow.Visible = true;
            switch (panel.DataMember)
            {
                case "Register":
                    CustomizeRegisterPanel(panel);
                    break;

                case "PayRecords":
                    CustomizePayRecordsPanel(panel);
                    break;

                case "PayRecordDetails":
                    CustomizeDetailsPanel(panel);
                    break;
            }
        }


        /// <summary>
        /// Customizes the RegisterPanel
        /// </summary>
        /// <param name="panel"></param>
        private void CustomizeRegisterPanel(GridPanel panel)
        {
            panel.FrozenColumnCount = 1;
            panel.ShowCheckBox = false;
            panel.ShowRowInfo = false;
            panel.Caption.Visible = false;
            panel.GroupByRow.Visible = false;

            panel.Columns[0].GroupBoxEffects = GroupBoxEffects.None;
            panel.Columns["状态"].NullString = "<no locale>";

            panel.Columns[0].CellStyles.Default.Background =
                new Background(Color.AliceBlue);

            foreach (GridColumn column in panel.Columns)
                column.ColumnSortMode = ColumnSortMode.Multiple;

        }

        /// <summary>
        /// Customizes the OrdersPanel
        /// </summary>
        /// <param name="panel"></param>
        private void CustomizePayRecordsPanel(GridPanel panel)
        {
            panel.ShowRowGridIndex = true;
            panel.ShowRowDirtyMarker = true;
            panel.ColumnHeader.RowHeight = 30;

            panel.Columns[0].CellStyles.Default.Background =
                new Background(Color.Beige);

            //panel.Caption = new GridCaption();

            //panel.Caption.Text = String.Format("PayRecords ({0}) for Register <font color=\"Maroon\"><i>\"{1}</i>\"</font>",
            //    panel.Rows.Count, ((GridRow)panel.Parent)["病人姓名"].Value);

            panel.DefaultVisualStyles.CaptionStyles.Default.Alignment = Alignment.MiddleLeft;
            UpdateDetailsFooter(panel, "交易总额");
        }

        /// <summary>
        /// Customizes the DetailsPanel
        /// </summary>
        /// <param name="panel"></param>
        private void CustomizeDetailsPanel(GridPanel panel)
        {
            panel.ColumnHeader.RowHeight = 30;

            panel.Columns[0].CellStyles.Default.Background =
                new Background(Color.LavenderBlush);

            panel.Columns["交易主ID"].CellStyles.Default.Alignment = Alignment.MiddleLeft;

            panel.DefaultVisualStyles.CaptionStyles.Default.Alignment = Alignment.MiddleLeft;
            panel.DefaultVisualStyles.CellStyles.Default.Alignment = Alignment.MiddleRight;

            UpdateDetailsFooter(panel, "总金额");
        }

        #region UpdateDetailsFooter

        /// <summary>
        /// Updates the Details Footer
        /// </summary>
        /// <param name="panel"></param>
        private void UpdateDetailsFooter(GridPanel panel,string colName)
        {
            if (panel.Footer == null)
                panel.Footer = new GridFooter();

            decimal total = TotalRows(panel.Rows, colName);

            panel.Footer.Text = String.Format(
                "总金额 <font color=\"Green\"><i>{0:C}</i></font>",
                total);
        }

        /// <summary>
        /// Calculates detail rows total
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private decimal TotalRows(IEnumerable<GridElement> rows,string colName)
        {
            decimal total = 0;

            foreach (GridContainer item in rows)
            {
                if (item is GridRow)
                {
                    GridRow row = (GridRow)item;

                    decimal fee = (decimal)(row[colName].Value ?? 0);

                    total += fee;
                }

                if (item.Rows.Count > 0)
                    total += TotalRows(item.Rows, colName);
            }
            return (total);
        }

        #endregion

        #endregion

        /// <summary>
        /// 数据加载完成之后计算总计；以及设置行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTradeInfoSummary_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {            
            foreach (DataGridViewRow dr in dgvTradeInfoSummary.Rows)
            {
                _myDictionary["FeeAll"] = Tools.ToDecimal(_myDictionary["FeeAll"].ToString(), 0) + Tools.ToDecimal(dr.Cells["交易总额"].Value.ToString(), 0);
                _myDictionary["FeeFund"] = Tools.ToDecimal(_myDictionary["FeeFund"].ToString(), 0) + Tools.ToDecimal(dr.Cells["统筹支付"].Value.ToString(), 0);
                _myDictionary["FeeCash"] = Tools.ToDecimal(_myDictionary["FeeCash"].ToString(), 0) + Tools.ToDecimal(dr.Cells["现金支付"].Value.ToString(), 0);
                _myDictionary["PersonCountPay"] = Tools.ToDecimal(_myDictionary["PersonCountPay"].ToString(), 0) + Tools.ToDecimal(dr.Cells["个帐支付"].Value.ToString(), 0);

                _myDictionary["medicine"] = Tools.ToDecimal(_myDictionary["medicine"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表西药"].Value.ToString(), 0);
                _myDictionary["tmedicine"] = Tools.ToDecimal(_myDictionary["tmedicine"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表中成药"].Value.ToString(), 0);
                _myDictionary["therb"] = Tools.ToDecimal(_myDictionary["therb"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表中草药"].Value.ToString(), 0);
                _myDictionary["examine"] = Tools.ToDecimal(_myDictionary["examine"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表检查费"].Value.ToString(), 0);
                _myDictionary["labexam"] = Tools.ToDecimal(_myDictionary["labexam"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表化验"].Value.ToString(), 0);
                _myDictionary["treatment"] = Tools.ToDecimal(_myDictionary["treatment"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表治疗费"].Value.ToString(), 0);
                _myDictionary["operation"] = Tools.ToDecimal(_myDictionary["operation"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表手术费"].Value.ToString(), 0);
                _myDictionary["material"] = Tools.ToDecimal(_myDictionary["material"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表材料费"].Value.ToString(), 0);
                _myDictionary["other"] = Tools.ToDecimal(_myDictionary["other"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表其他"].Value.ToString(), 0);
                _myDictionary["xray"] = Tools.ToDecimal(_myDictionary["xray"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表放射"].Value.ToString(), 0);
                _myDictionary["ultrasonic"] = Tools.ToDecimal(_myDictionary["ultrasonic"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表B超"].Value.ToString(), 0);
                _myDictionary["CT"] = Tools.ToDecimal(_myDictionary["CT"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表CT"].Value.ToString(), 0);
                _myDictionary["mri"] = Tools.ToDecimal(_myDictionary["mri"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表核磁"].Value.ToString(), 0);
                _myDictionary["oxygen"] = Tools.ToDecimal(_myDictionary["oxygen"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表输氧费"].Value.ToString(), 0);
                _myDictionary["bloodt"] = Tools.ToDecimal(_myDictionary["bloodt"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表输血费"].Value.ToString(), 0);
                _myDictionary["orthodontics"] = Tools.ToDecimal(_myDictionary["orthodontics"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表正畸费"].Value.ToString(), 0);
                _myDictionary["prosthesis"] = Tools.ToDecimal(_myDictionary["prosthesis"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表镶牙费"].Value.ToString(), 0);
                _myDictionary["forensic_expertise"] = Tools.ToDecimal(_myDictionary["forensic_expertise"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表司法鉴定"].Value.ToString(), 0);

                _myDictionary["diagnosis"] = Tools.ToDecimal(_myDictionary["diagnosis"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表诊察费"].Value.ToString(), 0);
                _myDictionary["medicalservice"] = Tools.ToDecimal(_myDictionary["medicalservice"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表医事服务费"].Value.ToString(), 0);
                _myDictionary["commonservice"] = Tools.ToDecimal(_myDictionary["commonservice"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表一般诊疗费"].Value.ToString(), 0);
                _myDictionary["registfee"] = Tools.ToDecimal(_myDictionary["registfee"].ToString(), 0) + Tools.ToDecimal(dr.Cells["报表挂号费"].Value.ToString(), 0);

                if (dr.Cells["交易状态"].Value.ToString().Trim() == "被退")
                {
                    dr.DefaultCellStyle.ForeColor = Color.SeaGreen;
                }
                else if (dr.Cells["交易状态"].Value.ToString().Trim() == "退费")
                {
                    dr.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            SetSumInfo();
        }
        /// <summary>
        /// 设置总计费用
        /// </summary>
        private void SetSumInfo()
        {
            lbFeeAll.Text = "交易总额:" + _myDictionary["FeeAll"];
            lbFeeFund.Text = "统筹支付:" + _myDictionary["FeeFund"];
            lbFeeCash.Text = "现金支付:" + _myDictionary["FeeCash"];
            lbPersonCountPay.Text = "个帐支付:" + _myDictionary["PersonCountPay"];
            lbMedicine.Text = "西药费用:" + _myDictionary["medicine"];
            lbTmedicine.Text = "中成药费:" + _myDictionary["tmedicine"];
            lbTherb.Text = "中草药费:" + _myDictionary["therb"];
            lbExamine.Text = "检查费用:" + _myDictionary["examine"];
            lbLabexam.Text = "化验费用:" + _myDictionary["labexam"];
            lbTreatment.Text = "治疗费用:" + _myDictionary["treatment"];
            lbOperation.Text = "手术费用:" + _myDictionary["operation"];
            lbMaterial.Text = "材料费用:" + _myDictionary["material"];
            lbOther.Text = "其他费用:" + _myDictionary["other"];
            lbXray.Text = "放射费用:" + _myDictionary["xray"];
            lbUltrasonic.Text = "B超费用:" + _myDictionary["ultrasonic"];
            lbCT.Text = "CT费用:" + _myDictionary["CT"];
            lbMri.Text = "核磁费用:" + _myDictionary["mri"];
            lbOxygen.Text = "输氧费用:" + _myDictionary["oxygen"];
            lbBloodt.Text = "输血费用:" + _myDictionary["bloodt"];
            lbOrthodontics.Text = "正畸费用:" + _myDictionary["orthodontics"];
            lbProsthesis.Text = "镶牙费用:" + _myDictionary["prosthesis"];
            lbForensic_expertise.Text = "司法鉴定:" + _myDictionary["forensic_expertise"];
            lbDiagnosis.Text = "诊察费用:" + _myDictionary["diagnosis"];
            lbMedicalservice.Text = "医事服务:" + _myDictionary["medicalservice"];
            lbCommonservice.Text = "一般诊疗:" + _myDictionary["commonservice"];
            lbRegistfee.Text = "挂号费用:" + _myDictionary["registfee"];
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            if (dgvTradeInfoSummary.CurrentRow != null)
            {
                int rowindex = dgvTradeInfoSummary.CurrentCell.RowIndex;
                string tradeNo = dgvTradeInfoSummary.Rows[rowindex].Cells["交易流水号"].Value.ToString().Trim();
                string tradeType = dgvTradeInfoSummary.Rows[rowindex].Cells["交易类型"].Value.ToString().Trim();
                string FeeNo = "0";
                if (tradeType.Contains("挂号"))
                {
                    FeeNo = dgvTradeInfoSummary.Rows[rowindex].Cells["HIS流水号"].Value.ToString().Trim();
                }
                else
                {
                    FeeNo = dgvTradeInfoSummary.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                }               
                InvokeController("MZ_PrintInvoice", tradeNo, FeeNo);
            }
        }

        private void InitDgv(int iType)
        {
            if (iType == 0)
            {
                //AddColumns(dgvHosp);
                //AddColumns(dgvMI);
            }
            else
            { }
        }

        private void AddColumns(DataGridView dgv,string sColName, string sDataPropertyName, string sHeaderText)
        {
            DataGridViewColumn c = new DataGridViewColumn();
            c.Name = sColName;
            c.DataPropertyName = sDataPropertyName;
            c.HeaderText = sHeaderText;
            dgv.Columns.Add(c);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iType = cmbType.SelectedIndex;
            InitDgv(iType);

            int iPatientType = rbOP.Checked ? 1 : 2;
            string sDeptCode = ((ComboBoxItem)cmbDept.SelectedItem).Text.Trim();
            string sTimeStat = dptCostDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sTimeStop = dptCostDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            if (iType == 0)
            {
                InvokeController("Mz_GetTradeRecordInfo", _ybId, iPatientType, sDeptCode, sTimeStat, sTimeStop);
            }
            else
            {
                InvokeController("Mz_GetTradeDetailInfo", _ybId, iPatientType, sDeptCode, sTimeStat, sTimeStop);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            InvokeController("M_ImportMIRecord", _ybId, cmbType.SelectedIndex);
        }

        private void btnComp_Click(object sender, EventArgs e)
        {
            //以医院为准，因为医院少的，对医院有利，不重要。医保少的，对医院不利，需找出
            DataTable DataTableH = (dgvHosp.DataSource as DataTable).Copy();
            DataTable DataTableMI = (dgvMI.DataSource as DataTable).Copy();
            int[] arrIcol = new int[DataTableH.Rows.Count];

            DataTableH.Columns.Add("flag", typeof(String));
            DataTableH.Columns["flag"].DefaultValue = "×";
            DataView dvMI = DataTableMI.DefaultView;
            DataView dvH = DataTableH.DefaultView;
            int flag = 0;
            //循环医院表数据
            //foreach (DataRowView drv1 in dvH)
            //{
            for(int i=0;i< dvH.Count;i++)
            { 
                //过滤：主表只需交易号，明细表还需项目号
                if (cmbType.SelectedIndex == 0)
                {
                    dvMI.RowFilter = " tradeno = '" + dvH[i]["tradeno"].ToString() + "'";
                    flag = 0;
                }
                else
                {
                    dvMI.RowFilter = " tradeno = '" + dvH[i]["tradeno"].ToString() + "' and itemno='" + dvH[i]["itemno"].ToString() + "'";
                    flag = 1;
                }
                //如果过滤存在数据则对比数据
                if (dvMI.Count > 0)
                {
                    //比较是否相同  
                    if (CompareUpdate(dvH[i], dvMI[0],flag,out arrIcol[i]))
                    {
                        dvH[i]["flag"] = "√";
                    }
                    else
                    {
                        dvH[i]["flag"] = "×";
                    }
                }
                //如果不存在则医院少了
                else
                {
                    dvH[i]["flag"] = "×";
                }
            }

            dvH.RowFilter = "";

            DataTableH.Columns["flag"].SetOrdinal(0);
            DataTable dt3= MergeDataTable(DataTableMI, DataTableH);
            dgvCompare.DataSource = DataTableH;
            for (int j = 0; j < dgvCompare.Rows.Count; j++)
            {
                if (dgvCompare.Rows[j].Cells["flag"].Value.ToString() == "×" )
                {                    
                    if (arrIcol[j] == 0)
                    {
                        for (int k = 0; k < dgvCompare.Rows[j].Cells.Count; k++)
                        {
                            dgvCompare.Rows[j].Cells[k].Style.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        dgvCompare.Rows[j].Cells[0].Style.ForeColor = System.Drawing.Color.Red;
                        dgvCompare.Rows[j].Cells[arrIcol[j] + 1].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            expandablePanel1.Expanded = true;
        }

        /// <summary>  
        /// 比较数据行是否相同  
        /// </summary>  
        /// <param name="dr1"></param>  
        /// <param name="dr2"></param>  
        /// <returns></returns>  
        private static bool CompareUpdate(DataRowView dr1, DataRowView dr2,int flag,out int iCol)
        {
            //行里只要有一项不一样，整个行就不一样,无需比较其它  
            string val1;
            string val2;
            iCol = 1;
            if (flag == 0)
            {
                for (int i = 1; i < dr2.Row.ItemArray.Length; i++)
                {
                    val1 = dr1[i].ToString();
                    val2 = dr2[i].ToString();
                    if (i < 4)
                    {
                        if (!val1.Equals(val2))
                        {
                            iCol = i;
                            return false;
                        }
                    }
                    else if (i == 5 && val1.Trim().Length == 14)
                    {
                        if (!val1.Substring(0, val1.Trim().Length - 6).Equals(val2.Substring(0, val2.Trim().Length - 6)))
                        {
                            iCol = i;
                            return false;
                        }
                    }
                    else if (i > 5 && i < 21)
                    {
                        if (Convert.ToDecimal(val1) != Convert.ToDecimal(val2))
                        {
                            iCol = i;
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < dr2.Row.ItemArray.Length; i++)
                {
                    val1 = dr1[i].ToString();
                    val2 = dr2[i].ToString();
                    if (i <= 4)
                    {
                        if (!val1.Equals(val2))
                        {
                            iCol = i;
                            return false;
                        }
                    }
                    else
                    {
                        if (Convert.ToDecimal(val1) != Convert.ToDecimal(val2))
                        {
                            iCol = i;
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #region 合并两个DataTable列  
        /// <summary>  
        /// 合并两个DataTable列  
        /// </summary>  
        /// <param name="dt1"></param>  
        /// <param name="dt2"></param>  
        /// <returns></returns>  
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            //定义dt的行数   
            int dtRowCount = 0;
            //dt的行数为dt1或dt2中行数最大的行数   
            if (dt1.Rows.Count > dt2.Rows.Count)
            {
                dtRowCount = dt1.Rows.Count;
            }
            else
            {
                dtRowCount = dt2.Rows.Count;
            }
            DataTable dt = new DataTable();
            //向dt中添加dt1的列名   
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                dt.Columns.Add(dt1.Columns[i].ColumnName + "1");
            }
            //向dt中添加dt2的列名   
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt.Columns.Add(dt2.Columns[i].ColumnName + "2");
            }
            for (int i = 0; i < dtRowCount; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++) { if ((dt1.Rows.Count - 1) >= i) { row[k] = dt1.Rows[i].ItemArray[k]; } }
                    for (int k = 0; k < dt2.Columns.Count; k++) { if ((dt2.Rows.Count - 1) >= i) { row[dt1.Columns.Count + k] = dt2.Rows[i].ItemArray[k]; } }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion
        private void cmbMIType_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cmbMIType.Items.Count > 0)
            {
                _ybId = Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Tag);
            }
        }

    }
}
