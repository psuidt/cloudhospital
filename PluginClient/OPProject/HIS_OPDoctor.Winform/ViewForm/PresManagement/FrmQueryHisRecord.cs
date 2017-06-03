using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.CustomControl;
using EfwControls.HISControl.Prescription.Controls;
using EfwControls.HISControl.Prescription.Controls.Entity;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 查看历史诊疗记录
    /// </summary>
    public partial class FrmQueryHisRecord : BaseFormBusiness, IFrmQueryHisRecord
    {
        #region 属性
        /// <summary>
        /// 复制标志
        /// </summary>
        public bool CopyFlag { get; set; }

        /// <summary>
        /// 当前加载网格
        /// </summary>
        private GridBoxCard iPrescription;

        /// <summary>
        /// 当前选中病人
        /// </summary>
        public DataTable DtPatient { get; set; }

        /// <summary>
        /// 当前加载网格DataTable
        /// </summary>
        private DataTable iPrescriptionDt;

        /// <summary>
        /// 当前病人ID
        /// </summary>
        public int PatListId { get; set; }

        /// <summary>
        /// 当前科室ID
        /// </summary>
        public int DeptId { get; set; }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmQueryHisRecord()
        {
            InitializeComponent();
            frmCommon.AddItem(txtCardNo, "txtCardNo");
            frmCommon.AddItem(txtVisitNO, "txtVisitNO");
            frmCommon.AddItem(txtName, "txtName");
            frmCommon.AddItem(cmbDept, "cmbDept");
            frmCommon.AddItem(cmbDoctor, "cmbDoctor");

            dgWestPres.Paint += new System.Windows.Forms.PaintEventHandler(gridPresDetail_Paint);
            dgChinesePres.Paint += new System.Windows.Forms.PaintEventHandler(gridChinesePresDetail_Paint);
        }

        #region 接口
        /// <summary>
        /// 绑定就诊记录网格
        /// </summary>
        /// <param name="dtRecord">就诊记录数据源</param>
        public void BindHisRecord(DataTable dtRecord)
        {
            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                if (dtRecord.Rows[i]["Age"] != null)
                {
                    dtRecord.Rows[i]["Age"] = GetAge(dtRecord.Rows[i]["Age"].ToString());
                }
            }

            dgRecord.DataSource = dtRecord;
        }

        /// <summary>
        /// 绑定医生所在的挂号科室
        /// </summary>
        /// <param name="dt">科室信息</param>
        /// <param name="deptId">当前登陆科室</param>
        public void BindDocInDept(DataTable dt, int deptId)
        {
            DataRow dr = dt.NewRow();
            dr["DeptId"] = 0;
            dr["Name"] = "所有科室";
            dr["DefaultFlag"] = 0;
            dt.Rows.InsertAt(dr, 0);
            cmbDept.DataSource = dt;
            cmbDept.SelectedIndex = 0;

            //if (dt != null)
            //{
            //    cmbDept.SelectedIndexChanged -= cmbDept_SelectedIndexChanged;
            //    cmbDept.DataSource = dt;
            //    cmbDept.SelectedIndexChanged += cmbDept_SelectedIndexChanged;
            //    if (DtPatient != null)
            //    {
            //        cmbDept.SelectedValue = DtPatient.Rows[0]["RegDeptID"];
            //    }
            //    else
            //    {
            //        cmbDept.SelectedValue = deptId;
            //        if (cmbDept.Text == string.Empty)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                cmbDept.SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 绑定科室下的医生
        /// </summary>
        /// <param name="dt">科室下的医生</param>
        /// <param name="doctorId">医生id</param>
        public void BindDoctor(DataTable dt, int doctorId)
        {
            DataRow dr = dt.NewRow();
            dr["EmpId"] = 0;
            dr["Name"] = "所有医生";
            dt.Rows.InsertAt(dr, 0);

            cmbDoctor.DataSource = dt;
            cmbDoctor.SelectedIndex = 0;
            //if (dt != null)
            //{
            //    cmbDoctor.DataSource = dt;
            //    if (DtPatient != null)
            //    {
            //        cmbDoctor.SelectedValue = DtPatient.Rows[0]["RegEmpID"];
            //    }
            //    else
            //    {
            //        cmbDoctor.SelectedValue = doctorId;
            //        if (cmbDoctor.Text == string.Empty)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                cmbDoctor.SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 绑定申请头表数据
        /// </summary>
        /// <param name="dt">申请头表数据</param>
        public void BindApplyHead(DataTable dt)
        {
            dgApplyHead.DataSource = dt;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件字典集合</returns>
        public Dictionary<string, string> GetQueryWhere()
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add(string.Empty, "(a.RegDate between '" + dtRegDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '"+ dtRegDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                query.Add("a.CardNo", txtCardNo.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txtVisitNO.Text))
            {
                query.Add("a.VisitNO", txtVisitNO.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txtName.Text))
            {
                query.Add("a.PatName", txtName.Text.Trim());
            }

            if (cmbDept.SelectedValue != null)
            {
                if (cmbDept.SelectedValue.ToString() != "0")
                {
                    query.Add("a.RegDeptID", cmbDept.SelectedValue.ToString());
                }
            }

            if (cmbDoctor.SelectedValue != null)
            {
                if (cmbDoctor.SelectedValue.ToString() != "0")
                {
                    query.Add("a.RegEmpID", cmbDoctor.SelectedValue.ToString());
                }
            }

            return query;
        }
        #endregion

        #region 函数
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">待转换的年龄字符串</param>
        /// <returns>年龄</returns>
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

        #region 画分组线以及设置字体颜色
        /// <summary>
        /// 将LIST转换为DataTable
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="list">列表集合</param>
        /// <returns>数据集</returns>
        private DataTable ToDataTable<T>(IEnumerable<T> list)
        {
            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();

            //获得反射的入口    
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                //给row 赋值    
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 画分组线
        /// </summary>
        /// <param name="graphics">绘图图面</param>
        /// <param name="presStyle">处方类型</param>
        private void PresPaintLine(Graphics graphics, PrescriptionStyle presStyle)
        {
            List<Prescription> presList = new List<Prescription>();
            switch (presStyle)
            {
                case PrescriptionStyle.西药与中成药:
                    iPrescription = dgWestPres;
                    presList = dgWestPres.DataSource as List<Prescription>;
                    break;
                case PrescriptionStyle.中草药:
                    iPrescription = dgChinesePres;
                    presList = dgChinesePres.DataSource as List<Prescription>;
                    break;
                case PrescriptionStyle.收费项目:
                    iPrescription = dgFee;
                    presList = dgFee.DataSource as List<Prescription>;
                    break;
            }

            if (iPrescription.DataSource == null)
            {
                return;
            }

            int penWidth = 2;
            if (presStyle == PrescriptionStyle.西药与中成药 || presStyle == PrescriptionStyle.全部)
            {
                //循环遍历所有记录
                for (int index = 0; index < iPrescription.Rows.Count; index++)
                {
                    Color penColer = GetPresForeColor(Tools.ToInt32(iPrescription.Rows[index].Cells["Status"].Value.ToString()));
                    int groupFlag = getGroupFlag(index);
                    if (groupFlag > 0)
                    {
                        PaintGroupLine(groupFlag, graphics, new Pen(penColer, penWidth), index);
                    }
                }
            }
        }

        /// <summary>
        /// 获取分组标志
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns>分组标识</returns>
        private int getGroupFlag(int rowIndex)
        {
            iPrescriptionDt = ToDataTable(iPrescription.DataSource as List<Prescription>);
            int groupFlag = 0;
            int listId = Convert.ToInt32(iPrescriptionDt.Rows[rowIndex]["PresNo"]);
            int groupId = Convert.ToInt32(iPrescriptionDt.Rows[rowIndex]["Group_Id"]);
            if (listId == 0)
            {
                return 0;
            }

            List<int> listCount = new List<int>();
            for (int index = 0; index < iPrescriptionDt.Rows.Count; index++)
            {
                if (Convert.ToInt32(iPrescriptionDt.Rows[index]["PresNo"]) == listId && Convert.ToInt32(iPrescriptionDt.Rows[index]["Group_Id"]) == groupId)
                {
                    listCount.Add(index);
                }
            }

            for (int i = 0; i < listCount.Count; i++)
            {
                if (rowIndex == listCount[i] && i == 0)
                {
                    groupFlag = 1;
                    break;
                }
                else if (rowIndex == listCount[i] && i == listCount.Count - 1)
                {
                    groupFlag = 3;
                    break;
                }
                else
                {
                    groupFlag = 2;
                }
            }

            if (listCount.Count == 1)
            {
                return 0;
            }
            else
            {
                return groupFlag;
            }
        }

        /// <summary>
        /// 处方状态
        /// </summary>
        private enum StatuFlag
        {
            新开=0,
            收费=1,
            退费=2
        }

        /// <summary>
        /// 绘制组线
        /// </summary>
        /// <param name="groupFlag">分组标识</param>
        /// <param name="graphics">绘图图面</param>
        /// <param name="pen">画笔</param>
        /// <param name="rowIndex">行索引</param>
        private void PaintGroupLine(int groupFlag, Graphics graphics, System.Drawing.Pen pen, int rowIndex)
        {
            //定义坐标变量
            int startPointX, startPointY, endPointX, endPointY;
            int firstLineWidth = 6;
            int firstLineHeight = GridCellBounds(rowIndex).Height / 2;
            switch (groupFlag)
            {
                case 1:
                    //小横线
                    startPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    startPointY = GridCellBounds(rowIndex).Bottom - firstLineHeight;
                    endPointX = GridCellBounds(rowIndex).Left;
                    endPointY = GridCellBounds(rowIndex).Bottom - firstLineHeight;
                    graphics.DrawLine(pen, startPointX, startPointY, endPointX, endPointY);
                    //小竖线
                    startPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    startPointY = GridCellBounds(rowIndex).Bottom - firstLineHeight;
                    endPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    endPointY = GridCellBounds(rowIndex).Bottom;
                    graphics.DrawLine(pen, startPointX, startPointY, endPointX, endPointY);
                    break;
                case 2:
                    startPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    startPointY = GridCellBounds(rowIndex).Top;
                    endPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    endPointY = GridCellBounds(rowIndex).Bottom;
                    graphics.DrawLine(pen, startPointX, startPointY, endPointX, endPointY);
                    break;
                case 3:
                    //小竖线
                    startPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    startPointY = GridCellBounds(rowIndex).Top;
                    endPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    endPointY = GridCellBounds(rowIndex).Top + firstLineHeight;
                    graphics.DrawLine(pen, startPointX, startPointY, endPointX, endPointY);
                    //小横线
                    startPointX = GridCellBounds(rowIndex).Left - firstLineWidth;
                    startPointY = GridCellBounds(rowIndex).Top + firstLineHeight;
                    endPointX = GridCellBounds(rowIndex).Left;
                    endPointY = GridCellBounds(rowIndex).Top + firstLineHeight;
                    graphics.DrawLine(pen, startPointX, startPointY, endPointX, endPointY);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置画线坐标
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns>画线坐标</returns>
        private Rectangle GridCellBounds(int rowIndex)
        {
            Rectangle rectangle = new Rectangle(
                    this.iPrescription.GetCellDisplayRectangle(this.Item_Id.Index, rowIndex, false).X,
                    this.iPrescription.GetCellDisplayRectangle(this.Item_Id.Index, rowIndex, false).Y,
                    this.iPrescription.GetCellDisplayRectangle(this.Item_Id.Index, rowIndex, false).Width + this.iPrescription.GetCellDisplayRectangle(this.Item_Name.Index, rowIndex, false).Width,
                    this.iPrescription.GetCellDisplayRectangle(this.Item_Id.Index, rowIndex, false).Height);

            return rectangle;
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetCellColor()
        {
            if (iPrescription == null || iPrescription.DataSource == null)
            {
                return;
            }

            iPrescriptionDt = ToDataTable(iPrescription.DataSource as List<Prescription>);
            for (int index = 0; index < iPrescriptionDt.Rows.Count; index++)
            {
                if (iPrescriptionDt.Rows[index]["Item_Name"].ToString() == "小计：")
                {
                    iPrescription.Rows[index].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    iPrescription.Rows[index].DefaultCellStyle.ForeColor = GetPresForeColor(Tools.ToInt32(iPrescriptionDt.Rows[index]["Status"].ToString()));
                }
            }
        }

        /// <summary>
        /// 获得处方字体的颜色
        /// </summary>
        /// <param name="status">处方状态</param>
        /// <returns>处方前景色</returns>
        public System.Drawing.Color GetPresForeColor(int status)
        {
            switch (status)
            {
                case (int)StatuFlag.收费:
                    return System.Drawing.Color.Orange;
                case (int)StatuFlag.退费:
                    return System.Drawing.Color.Fuchsia;
                default:
                    return System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// 绑定病历信息
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="dRow">数据行</param>
        private void BIndMedicalRecord(int patListId,DataRow dRow)
        {
            lblVisitNo.Text = dRow["VisitNO"].ToString();
            lblRegDate.Text = Convert.ToDateTime(dRow["RegDate"]).ToString("yyyy-MM-dd HH:mm");
            lblPatType.Text = dRow["PatTypeName"].ToString();
            lblDeptName.Text = dRow["DocDeptName"].ToString();
            txtOName.Text = dRow["PatName"].ToString();
            txtOSex.Text = dRow["PatSex"].ToString();
            txtOAge.Text = dRow["Age"].ToString();
            lblCardNo.Text = dRow["CardNO"].ToString();
            txtOAddress.Text = dRow["Address"].ToString();
            txtOPhone.Text = dRow["Mobile"].ToString();
            rtxtDisease.Text= dRow["DiseaseName"].ToString();
            OPD_MedicalRecord modelOMR = InvokeController("GetMedical", patListId) as OPD_MedicalRecord;
            if (modelOMR != null)
            {
                rtxtSymptoms.Text = modelOMR.Symptoms;
                rtxtSicknessHistory.Text = modelOMR.SicknessHistory;
                rtxtPhysicalExam.Text = modelOMR.PhysicalExam;
                rtxtAuxiliaryExam.Text = modelOMR.AuxiliaryExam;
            }
            else
            {
                rtxtSymptoms.Text = string.Empty;
                rtxtSicknessHistory.Text = string.Empty;
                rtxtPhysicalExam.Text = string.Empty;
                rtxtAuxiliaryExam.Text = string.Empty;
            }
        }
        #endregion

        #endregion

        #region 窗体事件
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            int patId = (int)InvokeController("GetPatListId");
            if (patId == 0)
            {
                MessageBoxShowSimple("您没有选择病人不能复制");
                return;
            }

            if (dgRecord.CurrentCell == null)
            {
                return;
            }

            DataRow dRow = ((DataTable)dgRecord.DataSource).Rows[dgRecord.CurrentCell.RowIndex];
            int patListId = Convert.ToInt32(dRow["PatListId"]);
            if (patId == patListId)
            {
                MessageBoxShowSimple("您不能复制当前就诊病人的信息，请选择上次就诊记录");
                return;
            }

            if (MessageBox.Show("您确认要复制该条就诊记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                bool bDiseaseHis = chkDiseaseHis.Checked;
                bool bWest = chkWest.Checked;
                bool bChinese = chkChinese.Checked;
                bool bFee = chkFee.Checked;
                bool bRtn = (bool)InvokeController("OneCopy", bDiseaseHis, bWest, bChinese, bFee, patId, patListId);
                if (bRtn)
                {
                    MessageBoxShowSimple("复制历史就诊记录成功！");
                    CopyFlag = true;
                }
                else
                {
                    MessageBoxShowSimple("复制历史就诊记录失败！");
                    CopyFlag = false;
                }
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmQueryHisRecord_Load(object sender, EventArgs e)
        {
            CopyFlag = false;
            dtRegDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd 00:00:00"));
            dtRegDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            if (DtPatient != null)
            {
                txtCardNo.Text = DtPatient.Rows[0]["CardNo"].ToString();
              //  txtVisitNO.Text = DtPatient.Rows[0]["VisitNO"].ToString();
                txtName.Text = DtPatient.Rows[0]["PatName"].ToString();
              //  dtRegDate.Bdate.Value = Convert.ToDateTime(DtPatient.Rows[0]["RegDate"]);
            }

            InvokeController("GetDocRelateDeptInfo");
            InvokeController("GetHisRecord");
        }

        /// <summary>
        /// 当前单元格改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgRecord_CurrentCellChanged(object sender, EventArgs e)
        {
            dgWestPres.DataSource = null;
            dgChinesePres.DataSource = null;
            dgFee.DataSource = null;
            dgApplyHead.DataSource = null;
            if (dgRecord.CurrentCell == null)
            {
                return;
            }

            DataRow dRow = ((DataTable)dgRecord.DataSource).Rows[dgRecord.CurrentCell.RowIndex];
            int patListId = Convert.ToInt32(dRow["PatListId"]);
            List<Prescription> westPres = InvokeController("GetPrescriptionData", patListId, 1) as List<Prescription>;
            Prescription.CalculateSubtotal(ref westPres);
            dgWestPres.DataSource = westPres;
            SetCellColor();
            List<Prescription> chinesePres = InvokeController("GetPrescriptionData", patListId, 2) as List<Prescription>;
            Prescription.CalculateSubtotal(ref chinesePres);
            dgChinesePres.DataSource = chinesePres;
            SetCellColor();
            List<Prescription> feePres = InvokeController("GetPrescriptionData", patListId, 3) as List<Prescription>;
            Prescription.CalculateSubtotal(ref feePres);
            dgFee.DataSource = feePres;
            InvokeController("GetApplyHead", patListId);
            BIndMedicalRecord(patListId, dRow);
        }

        /// <summary>
        /// 画分组线事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void gridPresDetail_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PresPaintLine(e.Graphics, PrescriptionStyle.西药与中成药);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 画线事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void gridChinesePresDetail_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PresPaintLine(e.Graphics, PrescriptionStyle.中草药);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetHisRecord");
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            frmCommon.Clear();
            this.Close();
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("GetDoctor", cmbDept.SelectedValue);
        }
        #endregion        
    }
}
