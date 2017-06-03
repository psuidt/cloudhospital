using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_Entity.OPManage.BusiEntity;
using HIS_OPDoctor.Winform.Controller;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 诊疗管理主界面
    /// </summary>
    public partial class FrmPresMain : BaseFormBusiness, IFrmPresMain
    {
        /// <summary>
        /// 构造
        /// </summary>
        public FrmPresMain()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法

        #region 处方
        private int _canPrintChargedPres;
        public int CanPrintChargedPres
        {
            get
            {
                return _canPrintChargedPres;
            }
            set
            {
                _canPrintChargedPres = value;
            }
        }
        /// <summary>
        /// 科室选择
        /// </summary>
        public int DeptId
        {
            get
            {
                return Convert.ToInt32(cmbDept.SelectedValue);
            }

            set
            {
                cmbDept.SelectedValue = value;
            }
        }

        /// <summary>
        /// 清除处方控件内容
        /// </summary>
        private void ClearPresControlRows()
        {
            DataTable dtWest = (DataTable)WestPresControl.gridPresDetail.DataSource;
            if (dtWest != null)
            {
                dtWest.Rows.Clear();
                WestPresControl.gridPresDetail.DataSource = dtWest;
            }

            DataTable dtChinese = (DataTable)ChinesePresControl.gridPresDetail.DataSource;
            if (dtChinese != null)
            {
                dtChinese.Rows.Clear();
                ChinesePresControl.gridPresDetail.DataSource = dtChinese;
            }

            DataTable dtFee = (DataTable)FeePresControl.gridPresDetail.DataSource;
            if (dtFee != null)
            {
                dtFee.Rows.Clear();
                FeePresControl.gridPresDetail.DataSource = dtFee;
            }

            DataTable dtCheck = (DataTable)dgApplyHead.DataSource;
            if (dtCheck != null)
            {
                dtCheck.Rows.Clear();
                dgApplyHead.DataSource = dtCheck;
            }
        }

        /// <summary>
        /// 过滤表
        /// </summary>
        private DataTable dtFilter = null;

        /// <summary>
        /// 病人状态
        /// </summary>
        private int patStatus = 0;

        /// <summary>
        /// 当前选中病人信息
        /// </summary>
        private DataTable dtPatInfo;

        /// <summary>
        /// 设置模板明细列字段可见性
        /// </summary>
        private void SetTplDetailColumnVisible()
        {
            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历  
                    cfmbDoseNum.Visible = true;
                    cfmbFrequencyName.Visible = true;
                    cfmbDays.Visible = true;
                    cfmbEntrust.Visible = true;
                    cfmbChannelName.Visible = true;
                    cfmbSpec.Visible = true;
                    break;
                case 1://西药中成药处方
                    cfmbDoseNum.Visible = false;
                    cfmbFrequencyName.Visible = true;
                    cfmbDays.Visible = true;
                    cfmbEntrust.Visible = true;
                    cfmbChannelName.Visible = true;
                    cfmbSpec.Visible = true;
                    break;
                case 2://中草药处方
                    cfmbDoseNum.Visible = true;
                    cfmbFrequencyName.Visible = true;
                    cfmbDays.Visible = false;
                    cfmbEntrust.Visible = true;
                    cfmbChannelName.Visible = true;
                    cfmbSpec.Visible = true;
                    break;
                case 3://材料费用
                    cfmbDoseNum.Visible = false;
                    cfmbFrequencyName.Visible = false;
                    cfmbDays.Visible = false;
                    cfmbEntrust.Visible = false;
                    cfmbChannelName.Visible = false;
                    cfmbSpec.Visible = false;
                    break;
                case 4://检验检查
                    cfmbDoseNum.Visible = true;
                    cfmbFrequencyName.Visible = true;
                    cfmbDays.Visible = true;
                    cfmbEntrust.Visible = true;
                    cfmbChannelName.Visible = true;
                    cfmbSpec.Visible = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 检查处方是否保存
        /// </summary>
        /// <returns>false没保存</returns>
        private bool CheckPresIsSave()
        {
            bool bIsSave = true;
            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            if (tabSelectedIndex == 3)
            {
                DataTable dtFee = FeePresControl.gridPresDetail.DataSource as DataTable;
                if (dtFee != null && dtFee.Rows.Count > 0)
                {
                    DataRow[] drs = dtFee.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存费用数据，请保存");
                        bIsSave = false;
                    }
                }
            }
            else if (tabSelectedIndex == 2)
            {
                DataTable dtChinese = ChinesePresControl.gridPresDetail.DataSource as DataTable;
                if (dtChinese != null && dtChinese.Rows.Count > 0)
                {
                    DataRow[] drs = dtChinese.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存中草药处方数据，请保存");
                        bIsSave = false;
                    }
                }
            }
            else if (tabSelectedIndex == 1)
            {
                DataTable dtWest = WestPresControl.gridPresDetail.DataSource as DataTable;
                if (dtWest != null && dtWest.Rows.Count > 0)
                {
                    DataRow[] drs = dtWest.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存西成处方数据，请保存");
                        bIsSave = false;
                    }
                }
            }

            return bIsSave;
        }

        /// <summary>
        /// 加载处方模板数据
        /// </summary>
        private void LoadPresTemplateData()
        {
            int presType = 0;
            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历  
                    presType = 0;
                    break;
                case 1://西药中成药处方
                    presType = 0;
                    break;
                case 2://中草药处方
                    presType = 1;
                    break;
                case 3://材料费用
                    presType = 2;
                    break;
                case 4://检验检查
                    presType = 0;
                    break;
                default:
                    break;
            }

            int iLevel = GetTempalteLevel();
            GetPresTemplate(iLevel, presType, tvPresTmp);
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="presType">处方类型1西药，2中草药，3费用</param>
        /// <param name="tree">树控件</param>
        private void GetPresTemplate(int intLevel, int presType, AdvTree tree)
        {
            if (InvokeController == null)
            {
                return;
            }

            TemplateListHead = (List<OPD_PresMouldHead>)InvokeController("GetPresTemplate", intLevel, presType);

            tree.Nodes.Clear();
            LoadTree(tree, TemplateListHead, "0", null);
            if (tree.Nodes.Count > 0)
            {
                tree.SelectedNode = tree.Nodes[0];
            }

            tree.ExpandAll();
            DataTable dt = (DataTable)dgPresTmpdetail.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                dgPresTmpdetail.DataSource = dt;
            }
        }

        /// <summary>
        /// 递归遍历加载树节点
        /// </summary>
        /// <param name="treeView">树控件</param>
        /// <param name="list">模板头列表</param>
        /// <param name="pid">父节点id</param>
        /// <param name="pNode">节点</param>
        public void LoadTree(AdvTree treeView, List<OPD_PresMouldHead> list, string pid, Node pNode)
        {
            string sFilter = "PID=" + pid;
            Node parentNode = pNode;
            List<OPD_PresMouldHead> templist = list.Where(item => item.PID == Convert.ToInt32(pid)).ToList();
            foreach (OPD_PresMouldHead bd in templist)
            {
                Node tempNode = new Node();

                tempNode.Text = bd.ModuldName;
                tempNode.Name = bd.PresMouldHeadID.ToString();
                tempNode.AccessibleDescription = bd.MouldType.ToString();  //模板类型
                tempNode.Tag = bd.PresType;
                if (bd.MouldType == 0)
                {
                    tempNode.ImageIndex = 0;
                }
                else
                {
                    tempNode.ImageIndex = 1;
                }

                if (parentNode != null)
                {
                    parentNode.Nodes.Add(tempNode);
                }
                else
                {
                    treeView.Nodes.Add(tempNode);
                }

                LoadTree(treeView, list, bd.PresMouldHeadID.ToString(), tempNode);
            }
        }

        /// <summary>
        /// 获取模板权限级别
        /// </summary>
        /// <returns>权限级别</returns>
        private int GetTempalteLevel()
        {
            int tempalteLevel = 0;

            if (radAllLevel.Checked)
            {
                tempalteLevel = 0;
            }

            if (radDeptLevel.Checked)
            {
                tempalteLevel = 1;
            }

            if (radPersonLevel.Checked)
            {
                tempalteLevel = 2;
            }

            return tempalteLevel;
        }

        /// <summary>
        /// 清除病人信息
        /// </summary>
        private void ClearPatientInfo()
        {
            txtCardNo.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPatType.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtVisitNO.Text = string.Empty;
            txtSex.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAllergies.Text = string.Empty;
            txtDiagnosis.Text = string.Empty;
            txtCardNo.Tag = null;
            txtVisitNO.Tag = null;
            ClearOMRInfo();
        }

        /// <summary>
        /// 清空病历数据
        /// </summary>
        private void ClearOMRInfo()
        {
            lblVisitNo.Text = string.Empty;
            lblRegDate.Text = string.Empty;
            lblPatType.Text = string.Empty;
            lblDeptName.Text = string.Empty;
            txtOName.Text = string.Empty;
            txtOSex.Text = string.Empty;
            txtOAge.Text = string.Empty;
            lblCardNo.Text = string.Empty;
            txtOAddress.Text = string.Empty;
            txtOPhone.Text = string.Empty;
            rtxtDisease.Text = string.Empty;
            //rtxtAuxiliaryExam.Text = string.Empty;
            rtxtSymptoms.Text = string.Empty;
            rtxtSicknessHistory.Text = string.Empty;
            rtxtPhysicalExam.Text = string.Empty;
        }

        /// <summary>
        /// 设置诊断控件可用状态
        /// </summary>
        private void SetDiagnosisControlEnable()
        {
            if (txtCardNo.Tag == null)
            {
                linkDiagnosis.Enabled = false;
                txtDiagnosis.Enabled = false;
            }
            else
            {
                linkDiagnosis.Enabled = true;
                txtDiagnosis.Enabled = true;
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">待转换年龄字符串</param>
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
                    default:
                        break;
                }
            }

            return tempAge;
        }

        /// <summary>
        /// 过滤病人列表
        /// </summary>
        public void FilterPatientList()
        {
            if (dtFilter == null)
            {
                return;
            }

            DataTable dt = dtFilter;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "VisitNO like '%" + txtFilter.Text.Trim() + "%' or PatName like '%" + txtFilter.Text.Trim() + "%'";
            dgPatientList.DataSource = dv.ToTable();
        }

        /// <summary>
        /// 初始化处方控件
        /// </summary>
        private void InitPrescriptionControls()
        {
            OPDPresDbHelper presHelper = new Controller.OPDPresDbHelper();
            WestPresControl.InitDbHelper(presHelper);
            ChinesePresControl.InitDbHelper(presHelper);
            FeePresControl.InitDbHelper(presHelper);
        }

        /// <summary>
        /// 设置总金额
        /// </summary>
        private void SetTotalFee()
        {
            //医技
            DataTable dt = (DataTable)dgApplyHead.DataSource;
            decimal totalfee = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal fee = 0;
                if (decimal.TryParse(dt.Rows[i]["TotalFee"].ToString(), out fee))
                {
                }

                totalfee += fee;
            }

            //西药处方
            dt = WestPresControl.gridPresDetail.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal fee = 0;
                if (dt.Rows[i]["Item_Name_S"].ToString() == "小计：")
                {
                    continue;
                }

                if (decimal.TryParse(dt.Rows[i]["Item_Money"].ToString(), out fee))
                {
                }

                totalfee += fee;
            }

            //中药处方            
            dt = ChinesePresControl.gridPresDetail.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal fee = 0;
                if (dt.Rows[i]["Item_Name_S"].ToString() == "小计：")
                {
                    continue;
                }

                if (decimal.TryParse(dt.Rows[i]["Item_Money"].ToString(), out fee))
                {
                }

                totalfee += fee;
            }

            //费用            
            dt = FeePresControl.gridPresDetail.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal fee = 0;
                if (dt.Rows[i]["Item_Name_S"].ToString() == "小计：")
                {
                    continue;
                }

                if (decimal.TryParse(dt.Rows[i]["Item_Money"].ToString(), out fee))
                {
                }

                totalfee += fee;
            }

            lblTotal.Text = "总金额：￥" + totalfee + "元";
        }

        /// <summary>
        /// 加载处方病人
        /// </summary>
        /// <param name="patListId">病人Id</param>
        private void LoadPresControlPatient(int patListId)
        {
            if (cmbDept.SelectedValue == null)
            {
                return;
            }

            string presDeptName = cmbDept.Text;
            int presDeptId = Convert.ToInt32(cmbDept.SelectedValue);
            string presDocName = Convert.ToString(InvokeController("GetPresDocName"));
            int presDocId = Convert.ToInt32(InvokeController("GetPresDocID"));
            WestPresControl.LoadPatData(patListId, presDeptId, presDeptName, presDocId, presDocName);
            int presCount = Convert.ToInt32(InvokeController("GetPresCount"));
            int drugRepeatWarn = Convert.ToInt32(InvokeController("GetDrugRepeatWarn"));
            int dayGreater30 = Convert.ToInt32(InvokeController("GetDayGreater30"));
            WestPresControl.gridPresDetail.EndEdit();
            ChinesePresControl.gridPresDetail.EndEdit();
            FeePresControl.gridPresDetail.EndEdit();
            WestPresControl.PresCount = presCount;
            WestPresControl.DrugRepeatWarn = drugRepeatWarn;
            WestPresControl.DayGreater30 = dayGreater30;
            ChinesePresControl.LoadPatData(patListId, presDeptId, presDeptName, presDocId, presDocName);
            ChinesePresControl.DrugRepeatWarn = drugRepeatWarn;
            ChinesePresControl.DayGreater30 = dayGreater30;
            FeePresControl.LoadPatData(patListId, presDeptId, presDeptName, presDocId, presDocName);
            InvokeController("GetApplyHead", 0);
            SetTotalFee();
        }

        /// <summary>
        /// 设置选项卡改变逻辑处理
        /// </summary>
        private void SetTabChanged()
        {
            if (Convert.ToBoolean(txtName.Tag) == false)
            {
                return;
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            SetTplDetailColumnVisible();
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    break;
                case 1://西药中成药处方
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = true;
                    btnMergeGroup.Enabled = true;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    break;
                case 2://中草药处方
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = true;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    break;
                case 3://材料费用
                    FeePresControl.PrescriptionRefresh();
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = false;
                    break;
                case 4://检验检查
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = false;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = false;
                    break;
                default:
                    break;
            }

            LoadPresTemplateData();
        }

        /// <summary>
        /// 初始化执行科室
        /// </summary>
        /// <param name="flag">0西药房1中药房</param>
        private void InitDrugStoreRoom(int flag)
        {
            InvokeController("GetDrugStoreRoom", flag);
        }

        /// <summary>
        /// 西成药/中药打印
        /// </summary>
        /// <param name="type">0西药处方1中药处方2费用</param>
        private void PresPrint(int type, bool isDoublePrint)
        {
            DataTable dt = new DataTable();
            int rowIndex = -1;
            switch (type)
            {
                case 0:
                    if (WestPresControl.gridPresDetail.CurrentCell == null)
                    {
                        MessageBoxEx.Show("没有可打印的数据");
                        return;
                    }

                    dt = (DataTable)WestPresControl.gridPresDetail.DataSource;
                    rowIndex = WestPresControl.gridPresDetail.CurrentCell.RowIndex;
                    break;
                case 1:
                    if (ChinesePresControl.gridPresDetail.CurrentCell == null)
                    {
                        MessageBoxEx.Show("没有可打印的数据");
                        return;
                    }

                    dt = (DataTable)ChinesePresControl.gridPresDetail.DataSource;
                    rowIndex = ChinesePresControl.gridPresDetail.CurrentCell.RowIndex;
                    break;
                case 2:
                    if (FeePresControl.gridPresDetail.CurrentCell == null)
                    {
                        MessageBoxEx.Show("没有可打印的数据");
                        return;
                    }

                    dt = (DataTable)FeePresControl.gridPresDetail.DataSource;
                    rowIndex = FeePresControl.gridPresDetail.CurrentCell.RowIndex;
                    break;
                default:
                    break;
            }

            if (rowIndex >= 0)
            {
                DataRow dr = dt.Rows[rowIndex];
                DataTable presData = InvokeController("GetPrintPresData", Convert.ToInt32(dr["PresHeadId"]), Convert.ToInt32(dr["PresNO"])) as DataTable;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                if (presData.Rows.Count > 0)
                {
                    //if (presData.Rows[0]["IsCharged"].ToString() == "0")
                    //{
                    PresPrint presPrint = new PresPrint();
                    presPrint.PatType = txtPatType.Text;
                    presPrint.PatName = txtName.Text;
                    presPrint.VisitNO = txtVisitNO.Text;
                    presPrint.Sex = txtSex.Text;
                    presPrint.Age = txtAge.Text;
                    presPrint.Diagnosis = txtDiagnosis.Text;
                    presPrint.Address = txtAddress.Text;
                    presPrint.TelPhone = txtMobile.Text;
                    presPrint.PresNO = Tools.ToString(dr["PresNO"]);
                    presPrint.PresType = GetTypeName(Tools.ToString(presData.Rows[0]["IsEmergency"]), Tools.ToString(presData.Rows[0]["IsLunacyPosion"]));
                    presPrint.WorkerName = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
                    presPrint.WorkerID = (InvokeController("this") as AbstractController).LoginUserInfo.WorkId.ToString();
                    presPrint.DeptName = Tools.ToString(presData.Rows[0]["DeptName"]);
                    presPrint.PresDate = Convert.ToDateTime(presData.Rows[0]["PresDate"]).ToString("yyyy-MM-dd hh:mm:ss");
                    presPrint.DoctorName = Tools.ToString(presData.Rows[0]["DoctorName"]);
                    presPrint.ChannelName = Tools.ToString(presData.Rows[0]["ChannelName"]);
                    presPrint.FrequencyName = Tools.ToString(presData.Rows[0]["FrequencyName"]);
                    presPrint.DoseNum = Tools.ToString(presData.Rows[0]["DoseNum"]);
                    presPrint.PrintType = Tools.ToString(type);
                    if (_canPrintChargedPres == 0)
                    {
                        if (presData.Rows[0]["IsCharged"].ToString() == "0")
                        {
                            InvokeController("PrintPres", presPrint, presData, isDoublePrint);
                        }
                        else
                        {
                            MessageBoxEx.Show("已收费项目不能打印");
                        }
                    }
                    else
                    {
                        InvokeController("PrintPres", presPrint, presData, isDoublePrint);
                    }
                    //}
                    //else
                    //{
                    //    MessageBoxEx.Show("已收费项目不能打印");
                    //}
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
        /// 获取西药打印数据
        /// </summary>
        /// <param name="presData">处方数据</param>
        /// <returns>西药打印数据</returns>
        private DataTable GetWestData(DataTable presData)
        {
            int currentgroupid = 0;
            int ordeyno = 0;
            presData.Columns.Add("GroupLineUp");
            presData.Columns.Add("GroupLineDown");
            for (int index = 0; index < presData.Rows.Count; index++)
            {
                int groupid = Convert.ToInt32(presData.Rows[index]["GroupID"]);
                int groupcount = presData.Select("GroupID=" + groupid).Count();
                if (currentgroupid != groupid)
                {
                    if (groupcount > 1)
                    {
                        currentgroupid = groupid;
                        ordeyno = 1;
                        presData.Rows[index]["GroupLineUp"] = "┍";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = string.Empty;
                        presData.Rows[index]["GroupLineDown"] = string.Empty;
                    }
                }
                else
                {
                    if (ordeyno == groupcount)
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "┕";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                }

                ordeyno++;
            }

            return presData;
        }

        private DataTable GetFeeData(DataTable presData)
        {
            int currentgroupid = 0;
            int ordeyno = 0;
            presData.Columns.Add("GroupLineUp");
            presData.Columns.Add("GroupLineDown");
            for (int index = 0; index < presData.Rows.Count; index++)
            {
                int groupid = Convert.ToInt32(presData.Rows[index]["GroupID"]);
                int groupcount = presData.Select("GroupID=" + groupid).Count();
                if (currentgroupid != groupid)
                {
                    if (groupcount > 1)
                    {
                        currentgroupid = groupid;
                        ordeyno = 1;
                        presData.Rows[index]["GroupLineUp"] = "┍";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = string.Empty;
                        presData.Rows[index]["GroupLineDown"] = string.Empty;
                    }
                }
                else
                {
                    if (ordeyno == groupcount)
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "┕";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                }

                ordeyno++;
            }

            return presData;
        }

        /// <summary>
        /// 获取中药打印数据
        /// </summary>
        /// <param name="presData">处方数据</param>
        /// <returns>中药打印数据</returns>
        private DataTable GetChineseData(DataTable presData)
        {
            int rowsCount = presData.Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("Item_Name_Left");
            dt.Columns.Add("Usage_Unit_Left");
            dt.Columns.Add("Usage_Amount_Left");
            dt.Columns.Add("TotalFee_Left");
            dt.Columns.Add("MedicareID_Left");
            dt.Columns.Add("Item_Name_Right");
            dt.Columns.Add("Usage_Unit_Right");
            dt.Columns.Add("Usage_Amount_Right");
            dt.Columns.Add("TotalFee_Right");
            dt.Columns.Add("MedicareID_Right");

            dt.Columns.Add("Item_Name_RightThree");
            dt.Columns.Add("Usage_Unit_RightThree");
            dt.Columns.Add("Usage_Amount_RightThree");
            //dt.Columns.Add("TotalFee_RightThree");
            //dt.Columns.Add("MedicareID_RightThree");

            dt.Columns.Add("Item_Name_RightFour");
            dt.Columns.Add("Usage_Unit_RightFour");
            dt.Columns.Add("Usage_Amount_RightFour");
            //dt.Columns.Add("TotalFee_RightFour");
            //dt.Columns.Add("MedicareID_RightFour");
            for (int i = 0; i < rowsCount; i += 4)
            {
                DataRow dr = dt.NewRow();
                dr["Item_Name_Left"] = presData.Rows[i]["ItemName"];
                dr["Usage_Unit_Left"] = presData.Rows[i]["DosageUnit"];
                dr["Usage_Amount_Left"] = presData.Rows[i]["Dosage"];
                dr["TotalFee_Left"] = presData.Rows[i]["TotalFee"];
                dr["MedicareID_Left"] = presData.Rows[i]["MedicareID"];
                if ((i + 1) < rowsCount)
                {
                    dr["Item_Name_Right"] = presData.Rows[i + 1]["ItemName"];
                    dr["Usage_Unit_Right"] = presData.Rows[i+1]["DosageUnit"];
                    dr["Usage_Amount_Right"] = presData.Rows[i + 1]["Dosage"];
                    dr["TotalFee_Right"] = presData.Rows[i + 1]["TotalFee"];
                    dr["MedicareID_Right"] = presData.Rows[i + 1]["MedicareID"];
                }
                if ((i + 2) < rowsCount)
                {
                    dr["Item_Name_RightThree"] = presData.Rows[i + 2]["ItemName"];
                    dr["Usage_Unit_RightThree"] = presData.Rows[i+2]["DosageUnit"];
                    dr["Usage_Amount_RightThree"] = presData.Rows[i + 2]["Dosage"];
                    //dr["TotalFee_RightThree"] = presData.Rows[i + 2]["TotalFee"];
                    //dr["MedicareID_RightThree"] = presData.Rows[i + 2]["MedicareID"];
                }
                if ((i + 3) < rowsCount)
                {
                    dr["Item_Name_RightFour"] = presData.Rows[i + 3]["ItemName"];
                    dr["Usage_Unit_RightFour"] = presData.Rows[i + 3]["DosageUnit"];
                    dr["Usage_Amount_RightFour"] = presData.Rows[i + 3]["Dosage"];
                    //dr["TotalFee_RightFour"] = presData.Rows[i + 3]["TotalFee"];
                    //dr["MedicareID_RightFour"] = presData.Rows[i + 3]["MedicareID"];
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 加载常用诊断
        /// </summary>
        private void LoadCommonDianosis()
        {
            InvokeController("LoadCommonDianosis");
        }

        /// <summary>
        /// 全部打印
        /// </summary>
        /// <param name="type">0西药处方1中药处方</param>
        private void PrintAll(int type,bool printAll)
        {
            string presno = string.Empty;
            DataTable dt = new DataTable();
            switch (type)
            {
                case 0:
                    dt = (DataTable)WestPresControl.gridPresDetail.DataSource;
                    break;
                case 1:
                    dt = (DataTable)ChinesePresControl.gridPresDetail.DataSource;
                    break;
                case 2:
                    dt = (DataTable)FeePresControl.gridPresDetail.DataSource;
                    break;
                default:
                    break;
            }

            if (dt.Rows.Count > 0)
            {
                DataTable presData = InvokeController("GetPrintPresData", Convert.ToInt32(dt.Rows[0]["PresHeadId"]), 0) as DataTable;
                for (int i = 0; i < presData.Rows.Count; i++)
                {
                    if (presno != presData.Rows[i]["PresNO"].ToString())
                    {
                        if (presData.Rows[i]["IsCharged"].ToString() == "0")
                        {
                            presno = presData.Rows[i]["PresNO"].ToString();
                            DataTable printdt = presData.Select("PresNO=" + presno ).CopyToDataTable();
                            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                            myDictionary.Add("PatType", txtPatType.Text);
                            myDictionary.Add("PatName", txtName.Text);
                            myDictionary.Add("VisitNO", txtVisitNO.Text);
                            myDictionary.Add("Sex", txtSex.Text);
                            myDictionary.Add("Age", txtAge.Text);
                            myDictionary.Add("Diagnosis", txtDiagnosis.Text);
                            myDictionary.Add("Address", txtAddress.Text);
                            myDictionary.Add("TelPhone", txtMobile.Text);
                            myDictionary.Add("PresNO", presno);
                            myDictionary.Add("PresType", GetTypeName(presData.Rows[0]["IsEmergency"].ToString(), presData.Rows[0]["IsLunacyPosion"].ToString()));
                            decimal totalFee = 0;
                            for (int f = 0; f < printdt.Rows.Count; f++)
                            {
                                if (presData.Rows[i]["IsTake"].ToString() == "0")
                                {
                                    if (Convert.ToInt32(printdt.Rows[f]["DoseNum"]) > 0)
                                    {
                                        totalFee += Convert.ToDecimal(printdt.Rows[f]["TotalFee"]) * Convert.ToDecimal(printdt.Rows[f]["DoseNum"]);
                                    }
                                    else
                                    {
                                        totalFee += Convert.ToDecimal(printdt.Rows[f]["TotalFee"]);
                                    }
                                }
                            }

                            myDictionary.Add("TotalFee", totalFee);
                            myDictionary.Add("DeptName", presData.Rows[0]["DeptName"]);
                            myDictionary.Add("PresDate", Convert.ToDateTime(presData.Rows[0]["PresDate"]).ToString("yyyy-MM-dd hh:mm:ss"));
                            myDictionary.Add("DoctorName", presData.Rows[0]["DoctorName"]);
                            myDictionary.Add("ChannelName", presData.Rows[0]["ChannelName"]);
                            myDictionary.Add("FrequencyName", presData.Rows[0]["FrequencyName"]);
                            myDictionary.Add("DoseNum", presData.Rows[0]["DoseNum"]);
                            myDictionary.Add("WorkerID", (InvokeController("this") as AbstractController).LoginUserInfo.WorkId);
                            string patTypeStr = string.Empty;
                            bool isAll = Convert.ToBoolean(InvokeController("GetMediaPat", string.Empty));
                            if (isAll)
                            {
                                patTypeStr = "医保处方";
                            }
                            else
                            {
                                patTypeStr = "非医保处方";
                            }

                            if (type == 1)
                            {
                                myDictionary.Add("WorkerName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + patTypeStr + "(正方)");

                                DataTable chinesedt = GetChineseData(printdt);
                                ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新中草药, 0, myDictionary, chinesedt).Print(false);
                                if (printAll)
                                {
                                    myDictionary["WorkerName"] = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + patTypeStr + "(副方)";
                                    ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新中草药, 0, myDictionary, chinesedt).Print(false);
                                }
                            }
                            else if(type==0)
                            {
                                myDictionary.Add("WorkerName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + patTypeStr + "(正方)");

                                DataTable westdt = GetWestData(printdt);
                                ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新西成药, 0, myDictionary, westdt).Print(false);
                                if (printAll)
                                {
                                    myDictionary["WorkerName"] = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + patTypeStr + "(副方)";
                                    ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新西成药, 0, myDictionary, westdt).Print(false);
                                }
                            }
                            else if (type == 2)
                            {
                                myDictionary.Add("WorkerName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName  + "门诊费用");

                                DataTable feedt = GetFeeData(printdt);
                                ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊医生费用, 0, myDictionary, feedt).Print(false);
                                if (printAll)
                                {
                                    myDictionary.Add("WorkerName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName  + "门诊费用");
                                    ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊医生费用, 0, myDictionary, feedt).Print(false);
                                }
                            }
                        }
                        //else
                        //{
                        //    MessageBoxEx.Show("已收费项目不能打印");
                        //}
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("没有可打印数据");
            }
        }

        /// <summary>
        /// 获取打印处方类型
        /// </summary>
        /// <param name="isEmergency">急诊标识1急诊0门诊</param>
        /// <param name="isLunacyPosion">精毒标识0否1是</param>
        /// <returns>精毒急标识名称</returns>
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
        /// 取得焦点控件
        /// </summary>
        /// <returns>获取焦点控件handle</returns>
        [DllImport("user32.dll")]
        public static extern int GetFocus();

        /// <summary>
        /// 取得焦点控件名
        /// </summary>
        /// <returns>焦点控件名</returns>
        private string GetFocusControlName()
        {
            string name = string.Empty;
            IntPtr handle = (IntPtr)GetFocus();
            if (handle == null)
            {
                this.FindForm().KeyPreview = true;
            }
            else
            {
                Control c = Control.FromHandle(handle);//这就是
                name = c.Name;
            }

            return name;
        }

        /// <summary>
        /// 当前选中控件
        /// </summary>
        private RichTextBoxEx currentRichTextBox;

        /// <summary>
        /// 申请单打印
        /// </summary>
        private void MedicalApplyPrint()
        {
            DataTable dt = dgApplyHead.DataSource as DataTable;
            if (dgApplyHead.CurrentRow != null)
            {
                DataRow dr = dt.Rows[dgApplyHead.CurrentRow.Index];
                InvokeController("PrintData", Tools.ToString(dr["ApplyHeadID"]), Tools.ToString(dr["ApplyType"].ToString()));
            }
            else
            {
                MessageBoxEx.Show("没有可以打印的数据");
            }
        }
        #endregion

        #region 门诊病历
        /// <summary>
        /// 病历打印
        /// </summary>
        private void OMRPrint()
        {
            if (omrEditStatus == OPDEnum.OMREditiStatus.Edit)
            {
                MessageBoxShowSimple("请先保存病历再打印");
                return;
            }

            OPD_MedicalRecord omr = new OPD_MedicalRecord();
            omr.Symptoms = rtxtSymptoms.Text.Trim();
            omr.SicknessHistory = rtxtSicknessHistory.Text.Trim();
            omr.PhysicalExam = rtxtPhysicalExam.Text.Trim();
            omr.AuxiliaryExam = rtxtAuxiliaryExam.Text.Trim();
            omr.DocAdvise = rtxtDisease.Text.Trim();
            string age = txtAge.Text;//年龄单独处理因为需要转换
            InvokeController("OMRPrint", omr, age);
        }

        /// <summary>
        /// 设置控件焦点
        /// </summary>
        private void SetControlFocus()
        {
            foreach (Control control in OmrCustDoc.ContentArea.Controls)
            {
                if (control is TextBoxX)
                {
                    control.Enter += Control_Enter;
                }
            }
        }

        /// <summary>
        /// 控件回车事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void Control_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = null;
        }

        /// <summary>
        /// 插入特殊字符
        /// </summary>
        /// <param name="txt">Rich文本控件</param>
        /// <param name="symbol">字符</param>
        private void InsertSymbol(RichTextBoxEx txt, string symbol)
        {
            int i = txt.SelectionStart;
            txt.Focus();
            txt.Text = txt.Text.Insert(i, symbol);
            txt.Select(i + symbol.Length, 0);
        }

        /// <summary>
        /// 判断是否保存病历
        /// </summary>
        /// <returns>true保存</returns>
        private bool IsOMRSave()
        {
            if (OMREditStatus == OPDEnum.OMREditiStatus.Edit)
            {
                MessageBoxShowSimple("您没有保存该病人的病历数据，请保存");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 门诊病历编辑状态
        /// </summary>
        private OPDEnum.OMREditiStatus omrEditStatus = OPDEnum.OMREditiStatus.ReadOnly;

        /// <summary>
        /// 门诊病历操作状态
        /// </summary>
        [Description("门诊病历操作状态")]
        public OPDEnum.OMREditiStatus OMREditStatus
        {
            get
            {
                return omrEditStatus;
            }

            set
            {
                omrEditStatus = value;
                if (omrEditStatus == OPDEnum.OMREditiStatus.ReadOnly)
                {
                    rtxtSymptoms.ReadOnly = true;
                    rtxtSicknessHistory.ReadOnly = true;
                    rtxtPhysicalExam.ReadOnly = true;
                    lblTitle.ForeColor = Color.Black;
                    OmrCustDoc.SelectBackgroundImageType = EfwControls.HISControl.CustomDocument.Controls.BackgroundImageType.正常皮肤;
                }
                else if (omrEditStatus == OPDEnum.OMREditiStatus.Edit)
                {
                    rtxtSymptoms.ReadOnly = false;
                    rtxtSicknessHistory.ReadOnly = false;
                    rtxtPhysicalExam.ReadOnly = false;
                    lblTitle.ForeColor = Color.Blue;
                    OmrCustDoc.SelectBackgroundImageType = EfwControls.HISControl.CustomDocument.Controls.BackgroundImageType.白色皮肤;
                }
            }
        }

        /// <summary>
        /// 显示辅查信息
        /// </summary>
        private void ShowAuxiliaryExam()
        {
            DataTable dtMedicalApply = (DataTable)dgApplyHead.DataSource;
            if (dtMedicalApply != null)
            {
                string applyItems = string.Empty;
                for (int i = 0; i < dtMedicalApply.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        applyItems += dtMedicalApply.Rows[i]["ItemNames"].ToString();
                    }
                    else
                    {
                        applyItems += "、" + dtMedicalApply.Rows[i]["ItemNames"].ToString();
                    }
                }

                rtxtAuxiliaryExam.Text = applyItems;
            }
            else
            {
                rtxtAuxiliaryExam.Text = string.Empty;
            }
        }

        /// <summary>
        /// 保存病历
        /// </summary>
        private void SaveOMR()
        {
            if (rtxtSymptoms.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("病人主诉不能为空");
                rtxtSymptoms.Focus();
                return;
            }

            if (rtxtPhysicalExam.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("病人体查不能为空");
                rtxtPhysicalExam.Focus();
                return;
            }

            OPD_MedicalRecord omr = new OPD_MedicalRecord();
            omr.PresDoctorID = (InvokeController("this") as AbstractController).LoginUserInfo.EmpId;
            omr.PresDeptID = Convert.ToInt32(cmbDept.SelectedValue);
            omr.Symptoms = rtxtSymptoms.Text.Trim();
            omr.SicknessHistory = rtxtSicknessHistory.Text.Trim();
            omr.PhysicalExam = rtxtPhysicalExam.Text.Trim();
            omr.DocAdvise = string.Empty;
            omr.AuxiliaryExam = rtxtAuxiliaryExam.Text.Trim();
            omr.Remark = string.Empty;
            bool bRtn = (bool)InvokeController("SaveOMRData", omr);
            if (bRtn)
            {
                OMREditStatus = OPDEnum.OMREditiStatus.ReadOnly;
            }
            else
            {
                rtxtSymptoms.Focus();
            }
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="tree">树控件</param>
        private void GetOMRTemplate(int intLevel, AdvTree tree)
        {
            if (InvokeController == null)
            {
                return;
            }

            OMRTemplateListHead = (List<OPD_OMRTmpHead>)InvokeController("GetOMRTemplate", intLevel);

            tree.Nodes.Clear();
            LoadOMRTree(tree, OMRTemplateListHead, "-1", null);
            if (tree.Nodes.Count > 0)
            {
                tree.SelectedNode = tree.Nodes[0];
            }

            tree.ExpandAll();
            rtxtTplContent.Text = "注意：暂无显示模板内容，请点击模板节点显示模板内容";
        }

        /// <summary>
        /// 递归遍历加载树节点
        /// </summary>
        /// <param name="treeView">树控件</param>
        /// <param name="list">模板头列表</param>
        /// <param name="pid">父节点id</param>
        /// <param name="pNode">树节点</param>
        public void LoadOMRTree(AdvTree treeView, List<OPD_OMRTmpHead> list, string pid, Node pNode)
        {
            string sFilter = "PID=" + pid;
            Node parentNode = pNode;
            List<OPD_OMRTmpHead> templist = list.Where(item => item.PID == Convert.ToInt32(pid)).ToList();
            foreach (OPD_OMRTmpHead bd in templist)
            {
                Node tempNode = new Node();

                tempNode.Text = bd.ModuldName;
                tempNode.Name = bd.OMRTmpHeadID.ToString();
                tempNode.AccessibleDescription = bd.MouldType.ToString();  //模板类型
                if (bd.MouldType == 0)
                {
                    tempNode.ImageIndex = 0;
                }
                else
                {
                    tempNode.ImageIndex = 1;
                }

                if (parentNode != null)
                {
                    parentNode.Nodes.Add(tempNode);
                }
                else
                {
                    treeView.Nodes.Add(tempNode);
                }

                LoadOMRTree(treeView, list, bd.OMRTmpHeadID.ToString(), tempNode);
            }
        }

        /// <summary>
        /// 获取模板权限级别
        /// </summary>
        /// <returns>模板权限级别数值</returns>
        private int GetOMRTempalteLevel()
        {
            int tempalteLevel = 0;

            if (radOAll.Checked)
            {
                tempalteLevel = 0;
            }

            if (radODept.Checked)
            {
                tempalteLevel = 1;
            }

            if (radOPerson.Checked)
            {
                tempalteLevel = 2;
            }

            return tempalteLevel;
        }

        /// <summary>
        /// 加载处方模板数据
        /// </summary>
        private void LoadOMRTemplateData()
        {
            int iLevel = GetOMRTempalteLevel();
            GetOMRTemplate(iLevel, tvOMRTpl);
        }

        /// <summary>
        /// 绑定模板内容
        /// </summary>
        /// <param name="tmpDetail">模板模型</param>
        public void BindTplContentControl(OPD_OMRTmpDetail tmpDetail)
        {
            if (tmpDetail == null)
            {
                rtxtTplContent.Text = "注意：暂无显示模板内容，请点击模板节点显示模板内容";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("【主诉】" + tmpDetail.Symptoms);
                sb.Append("\r\n");
                sb.Append("【体查】" + tmpDetail.PhysicalExam);
                sb.Append("\r\n");
                sb.Append("【现病史】" + tmpDetail.SicknessHistory);
                rtxtTplContent.Text = sb.ToString();
                SetTitleColor("主诉");
                SetTitleColor("体查");
                SetTitleColor("现病史");
            }

            rtxtTplContent.Tag = tmpDetail;
        }

        /// <summary>
        /// 设置医院标题颜色
        /// </summary>
        /// <param name="findStr">查找字符串</param>
        private void SetTitleColor(string findStr)
        {
            ArrayList list = getIndexArray(rtxtTplContent.Text, findStr);
            int index = (int)list[0];
            rtxtTplContent.Select(index, findStr.Length);
            rtxtTplContent.SelectionColor = ColorTranslator.FromHtml("#1e5aab");
        }

        /// <summary>
        /// 取得字符串索引
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="findStr">查找字符串</param>
        /// <returns>字符串索引</returns>
        private ArrayList getIndexArray(string inputStr, string findStr)
        {
            ArrayList list = new ArrayList();
            int start = 0;
            while (start < inputStr.Length)
            {
                int index = inputStr.IndexOf(findStr, start);
                if (index >= 0)
                {
                    list.Add(index);
                    start = index + findStr.Length;
                }
                else
                {
                    break;
                }
            }

            return list;
        }
        #endregion

        #endregion

        #region 接口 
        #region 异步加载
        /// <summary>
        /// 绑定处方控件数据
        /// </summary>
        public void BindControlData()
        {
            InitPrescriptionControls();
        }

        /// <summary>
        /// 完成异步绑定处方数据源
        /// </summary>
        public void BindControlDataComplete()
        {
            WestPresControl.InitializeAsynCardData();
            ChinesePresControl.InitializeAsynCardData();
            FeePresControl.InitializeAsynCardData();
            txtCardNo.ReadOnly = false;
            txtVisitNO.ReadOnly = false;
            dgPatientList.Enabled = true;
            barMgr.Enabled = true;
        }
        #endregion

        /// <summary>
        /// 一键复制后刷新处方控件
        /// </summary>
        /// <param name="flag">true一键复制成功</param>
        public void RefreshOneCopyControl(bool flag)
        {
            if (flag)
            {
                WestPresControl.PrescriptionRefresh();
                ChinesePresControl.PrescriptionRefresh();
                FeePresControl.PrescriptionRefresh();
            }
        }

        /// <summary>
        /// 绑定处方模板明细
        /// </summary>
        /// <param name="dt">处方模板明细数据</param>
        public void BindTemplateDetailGrid(DataTable dt)
        {
            dgPresTmpdetail.DataSource = dt;
        }

        /// <summary>
        /// 模板级别
        /// </summary>
        public int TemplateLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 模板头列表类
        /// </summary>
        public List<OPD_PresMouldHead> TemplateListHead
        {
            get;
            set;
        }

        /// <summary>
        /// 设置处方按钮可用状态
        /// </summary>
        /// <param name="isvalid">挂号是否有效true有效false无效</param>
        public void EnablePresButton(bool isvalid)
        {
            if (isvalid)
            {
                btnSave.Enabled = true;
                btnNew.Enabled = true;
                btnUpdate.Enabled = true;
                btnChangePres.Enabled = true;
                btnMergeGroup.Enabled = true;
                btnComplete.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                btnUpdate.Enabled = false;
                btnChangePres.Enabled = false;
                btnMergeGroup.Enabled = false;
                btnComplete.Enabled = false;
                MessageBoxEx.Show("当前病人超过挂号有效期");
            }

            txtName.Tag = isvalid;
            SetTabChanged();
        }

        /// <summary>
        /// 绑定医生所在的挂号科室
        /// </summary>
        /// <param name="dt">科室信息</param>
        /// <param name="deptId">当前登陆科室</param>
        public void BindDocInDept(DataTable dt, int deptId)
        {
            if (dt != null)
            {
                cmbDept.DataSource = dt;
                cmbDept.SelectedValue = deptId;
                if (cmbDept.Text == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbDept.SelectedIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 过滤掉确费重复数据
        /// </summary>
        /// <param name="dt">数据</param>
        /// <returns>过滤后的申请单数据</returns>
        private DataTable FilterRepeatData(DataTable dt)
        {
            string[] filedNames = new string[] { "ApplyHeadID" };
            DataView dv = dt.DefaultView;
            DataTable distTable = dv.ToTable("Dist", true, filedNames);
            DataTable dtCopy = dt.Clone();
            foreach (DataRow row in distTable.Rows)
            {
                string applyHeadId = row["ApplyHeadID"].ToString();
                DataRow[] drArr = dt.Select("ApplyHeadID=" + applyHeadId, "ApplyStatu desc");
                if (drArr.Length > 0)
                {
                    if (drArr.Length > 1)
                    {
                        decimal totalFee = 0;
                        foreach (DataRow r in drArr)
                        {
                            totalFee += Convert.ToDecimal(r["TotalFee"]);
                        }

                        drArr[0]["TotalFee"] = totalFee;
                    }

                    dtCopy.Rows.Add(drArr[0].ItemArray);                    
                }
            }

            return dtCopy;
        }

        /// <summary>
        /// 绑定申请头表数据
        /// </summary>
        /// <param name="dt">申请头表数据</param>
        public void BindApplyHead(DataTable dt)
        {
            dt = FilterRepeatData(dt);
            dgApplyHead.DataSource = dt;
            SetTotalFee();
            ShowAuxiliaryExam();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ApplyStatu"].ToString() == "1" || dt.Rows[i]["ApplyStatu"].ToString() == "2")
                {
                    dgApplyHead.SetRowColor(i, ColorTranslator.FromHtml("#d96100"), true);
                }

                if (dt.Rows[i]["IsReturns"].ToString() == "1")
                {
                    dgApplyHead.SetRowColor(i, Color.Fuchsia, true);
                }
            }

            dgApplyHead.Refresh();
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtPatientList">病人列表</param>
        public void BindPatientList(DataTable dtPatientList)
        {
            dgPatientList.DataSource = dtPatientList;
            dtFilter = dtPatientList;
        }

        /// <summary>
        /// 绑定病人信息面板
        /// </summary>
        /// <param name="dtPatient">病人信息</param>
        /// <param name="dtDiseases">病人诊断信息</param>
        public void BindPatientInfo(DataTable dtPatient, DataTable dtDiseases)
        {
            dtPatInfo = dtPatient;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                //绑定病人信息面板
                txtCardNo.Text = dtPatient.Rows[0]["CardNO"].ToString();
                txtCardNo.Tag = dtPatient.Rows[0]["MemberID"].ToString();
                txtName.Text = dtPatient.Rows[0]["PatName"].ToString();
                txtPatType.Text = dtPatient.Rows[0]["PatTypeName"].ToString();
                txtMobile.Text = dtPatient.Rows[0]["Mobile"].ToString();
                txtAddress.Text = dtPatient.Rows[0]["Address"].ToString();
                txtVisitNO.Text = dtPatient.Rows[0]["VisitNO"].ToString();
                txtVisitNO.Tag = dtPatient.Rows[0]["PatListID"].ToString();
                txtSex.Text = dtPatient.Rows[0]["PatSex"].ToString();
                txtAge.Text = GetAge(dtPatient.Rows[0]["Age"].ToString());
                patStatus = Convert.ToInt32(dtPatient.Rows[0]["VisitStatus"]);
                txtAllergies.Text = dtPatient.Rows[0]["Allergies"].ToString();
                LoadPresControlPatient(Convert.ToInt32(dtPatient.Rows[0]["PatListID"]));
                //绑定病历面板
                lblVisitNo.Text = dtPatient.Rows[0]["VisitNO"].ToString();
                lblRegDate.Text = Convert.ToDateTime(dtPatient.Rows[0]["RegDate"]).ToString("yyyy-MM-dd HH:mm");
                lblPatType.Text = dtPatient.Rows[0]["PatTypeName"].ToString();
                lblDeptName.Text = dtPatient.Rows[0]["DocDeptName"].ToString();
                txtOName.Text = dtPatient.Rows[0]["PatName"].ToString();
                txtOSex.Text = dtPatient.Rows[0]["PatSex"].ToString();
                txtOAge.Text = GetAge(dtPatient.Rows[0]["Age"].ToString());
                lblCardNo.Text = dtPatient.Rows[0]["CardNO"].ToString();
                txtOAddress.Text = dtPatient.Rows[0]["Address"].ToString();
                txtOPhone.Text = dtPatient.Rows[0]["Mobile"].ToString();

                ShowAuxiliaryExam();
            }
            else
            {
                MessageBoxEx.Show("对不起，没有找到对应的患者，请核对相关信息");
                ClearPresControlRows();
                ClearPatientInfo();
            }

            SetDiagnosisControlEnable();
        }

        /// <summary>
        /// 绑定诊断信息
        /// </summary>
        /// <param name="diseaseNames">诊断名称以、号隔开</param>
        public void ShowDiseaseInfo(string diseaseNames)
        {
            txtDiagnosis.Text = diseaseNames;
            rtxtDisease.Text = diseaseNames;
        }

        /// <summary>
        /// 绑定药房科室
        /// </summary>
        /// <param name="dt">药房数据集</param>
        public void BindDrugStoreRoom(DataTable dt)
        {
            if (dt != null)
            {
                cmbExecStoreRoom.DisplayMember = "DeptName";
                cmbExecStoreRoom.ValueMember = "DeptID";
                cmbExecStoreRoom.DataSource = dt;
            }
        }

        /// <summary>
        /// 绑定常用诊断表格
        /// </summary>
        /// <param name="dtCommonDiagnosis">常用诊断信息</param>
        public void BindCommonDiagnosisGrid(DataTable dtCommonDiagnosis)
        {
            dgDisease.DataSource = dtCommonDiagnosis;
        }
        #region 门诊病历
        /// <summary>
        /// 模板级别
        /// </summary>
        public int OMRTemplateLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 模板头列表类
        /// </summary>
        public List<OPD_OMRTmpHead> OMRTemplateListHead
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新模板树
        /// </summary>
        public void FreshOMRTplTree()
        {
            LoadOMRTemplateData();
        }

        /// <summary>
        /// 绑定病历信息
        /// </summary>
        /// <param name="modelOMR">病历实体</param>
        public void BindOMRInfo(OPD_MedicalRecord modelOMR)
        {
            if (modelOMR != null)
            {
                rtxtSymptoms.Text = modelOMR.Symptoms;
                rtxtSicknessHistory.Text = modelOMR.SicknessHistory;
                rtxtPhysicalExam.Text = modelOMR.PhysicalExam;
            }
            else
            {
                rtxtSymptoms.Text = string.Empty;
                rtxtSicknessHistory.Text = string.Empty;
                rtxtPhysicalExam.Text = string.Empty;
            }
        }

        /// <summary>
        /// 绑定符号类型下拉框
        /// </summary>
        /// <param name="dtSymbolType">符号类型</param>
        /// <param name="dtSymbolContent">符号内容</param>
        public void BindOMRSymbolComboBox(DataTable dtSymbolType, DataTable dtSymbolContent)
        {
            cmbCharType.DisplayMember = "Name";
            cmbCharType.ValueMember = "ClassId";
            cmbCharType.Tag = dtSymbolContent;
            cmbCharType.DataSource = dtSymbolType;
            cmbCharType.SelectedIndex = 0;
        }
        #endregion
        #endregion

        #region 处方相关事件
        /// <summary>
        /// 窗体打开前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmPresMain_OpenWindowBefore(object sender, EventArgs e)
        {
            //初始化药房执行科室下拉框，默认为西药处方执行科室        
            cmbExecStoreRoom.SelectedIndexChanged -= new EventHandler(cmbExecStoreRoom_SelectedIndexChanged);
            InitDrugStoreRoom(0);
            cmbExecStoreRoom.SelectedIndexChanged += new EventHandler(cmbExecStoreRoom_SelectedIndexChanged);
            txtVisitNO.Height = 21;
            //绑定诊断窗体
            FrmDiagnosis frmDiagnosis = (FrmDiagnosis)InvokeController("GetDiagnosisForm");
            popup1.AddPopupPanel(txtDiagnosis, frmDiagnosis, PopupEvent.Click, txtDiagnosis.Width, 250);
            dgPatientList.RowHeadersVisible = false;
            //加载科室数据
            InvokeController("GetDocRelateDeptInfo");
            //初始化挂号日期
            dtRegDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtRegDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //打开窗体触发查询
            btnQuery_Click(null, null);
            SetDiagnosisControlEnable();
            //初始化系统参数
            InvokeController("GetSystemParameter");
            //设置焦点
            txtCardNo.Focus();

            //设置医院名称
            lblHospitalName.Text = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
            //清空病历信息
            ClearOMRInfo();
            //绑定符号选择下拉框
            InvokeController("GetSymbolData");
            //初始化病历模板
            LoadOMRTemplateData();
            //设置控件焦点
            SetControlFocus();
            //设置门诊病历只读状态
            OMREditStatus = OPDEnum.OMREditiStatus.ReadOnly;
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue == null)
            {
                MessageBoxShowSimple("没有挂号科室可选");
                cmbDept.Focus();
                return;
            }

            if (dtRegDate.Bdate.Value > dtRegDate.Edate.Value)
            {
                MessageBoxEx.Show("挂号开始日期不能大于挂号结束日期");
                dtRegDate.Focus();
                return;
            }

            //挂号科室
            int deptId = Convert.ToInt32(cmbDept.SelectedValue);
            //开始时间
            string regBeginDate = dtRegDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            //结束时间
            string regEndDate = dtRegDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            //就诊状态
            int visitStatus = 0;
            if (radWaiting.Checked)
            {
                visitStatus = 0;
            }

            if (radDone.Checked)
            {
                visitStatus = 2;
            }

            //患者归属
            int belong = 0;
            if (radMy.Checked)
            {
                belong = 0;
            }

            if (radMyDept.Checked)
            {
                belong = 1;
            }

            if (radAll.Checked)
            {
                belong = 2;
            }

            InvokeController("LoadPatientList", deptId, regBeginDate, regEndDate, visitStatus, belong);
            btnQuery.Text = "刷新病人(共" + dgPatientList.Rows.Count + "人)";
        }

        /// <summary>
        /// 病人列表双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgPatientList_DoubleClick(object sender, EventArgs e)
        {
            if (dgPatientList.CurrentCell != null)
            {
                if (dgPatientList.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgPatientList.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgPatientList.DataSource).Rows[rowIndex];
                    //病历是否处于编辑状态
                    if (IsOMRSave() == false)
                    {
                        return;
                    }

                    //处方是否保存
                    if (CheckPresIsSave() == false)
                    {
                        return;
                    }

                    InvokeController("GetPatientInfo", dRow["PatListID"].ToString(), 2);
                    switch (tabControlPres.SelectedTabIndex)
                    {
                        case 4:
                            InvokeController("GetApplyHead", 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 卡号键盘按下事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {   //病历是否处于编辑状态
                if (IsOMRSave() == false)
                {
                    return;
                }

                //处方是否保存
                if (CheckPresIsSave() == false)
                {
                    return;
                }

                InvokeController("GetPatientInfo", txtCardNo.Text, 0);
            }
        }

        /// <summary>
        /// 门诊号键盘按下事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtVisitNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //病历是否处于编辑状态
                if (IsOMRSave() == false)
                {
                    return;
                }

                //处方是否保存
                if (CheckPresIsSave() == false)
                {
                    return;
                }

                string visitNo = txtVisitNO.Text.Trim();
                if (visitNo.Length == 3)
                {
                    visitNo = DateTime.Now.ToString("yyyyMMdd") + visitNo;
                    txtVisitNO.Text = visitNo;
                }
                else if (visitNo.Length == 2)
                {
                    string currentDateStr = DateTime.Now.ToString("yyyyMMdd");
                    visitNo = DateTime.Now.ToString("yyyyMMdd") + "0" + visitNo;
                    txtVisitNO.Text = visitNo;
                }
                else if (visitNo.Length == 1)
                {
                    string currentDateStr = DateTime.Now.ToString("yyyyMMdd");
                    visitNo = DateTime.Now.ToString("yyyyMMdd") + "00" + visitNo;
                    txtVisitNO.Text = visitNo;
                }

                InvokeController("GetPatientInfo", visitNo, 1);
            }
        }

        /// <summary>
        /// 文本改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterPatientList();
        }

        /// <summary>
        /// 修改病人点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void linkUpdatePat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtCardNo.Tag != null)
            {
                int memberID = Convert.ToInt32(txtCardNo.Tag);
                int curpatlistid = Convert.ToInt32(txtVisitNO.Tag);
                InvokeController("ShowMemberInfo", txtCardNo.Text.Trim(), memberID,curpatlistid);
            }
            else
            {
                MessageBoxEx.Show("当前没有选中患者，请选择一个患者");
                txtCardNo.Focus();
            }
        }

        /// <summary>
        /// 修改诊断点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void linkDiagnosis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmDiagnosis frmDiagnosis = (FrmDiagnosis)InvokeController("GetDiagnosisForm");
            popup1.Show(txtDiagnosis, txtDiagnosis.Width, 250);
        }

        /// <summary>
        /// 新开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            //检查申请不需要判断
            //if (tabControlPres.SelectedTabIndex == 1 || tabControlPres.SelectedTabIndex == 2)
            if (tabControlPres.SelectedTabIndex !=0)
            {
                if (txtDiagnosis.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("没有输入诊断信息不能新开处方");
                    txtDiagnosis.Focus();
                    return;
                }
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    OMREditStatus = OPDEnum.OMREditiStatus.Edit;
                    rtxtSymptoms.Focus();
                    break;
                case 1://西药中成药处方
                    WestPresControl.PrescriptionNew();
                    break;
                case 2://中草药处方
                    ChinesePresControl.PrescriptionNew();
                    break;
                case 3://材料费用
                    FeePresControl.PrescriptionNew();
                    break;
                case 4://检验检查
                    InvokeController("ShowApply", string.Empty, "0", "0", "0");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtVisitNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar) && e.KeyChar != '\u0003')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 选择改变提交事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbDept_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                switch (tabSelectedIndex)
                {
                    case 0://门诊病历 
                        InvokeController("GetPatientOMRData");
                        OMREditStatus = OPDEnum.OMREditiStatus.ReadOnly;
                        break;
                    case 1://西药中成药处方
                        WestPresControl.PrescriptionRefresh();
                        break;
                    case 2://中草药处方
                        ChinesePresControl.PrescriptionRefresh();
                        break;
                    case 3://材料费用
                        FeePresControl.PrescriptionRefresh();
                        break;
                    case 4://检验检查
                        InvokeController("GetApplyHead", 0);
                        ShowAuxiliaryExam();
                        break;
                    default:
                        break;
                }

                SetTotalFee();
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            //if (tabControlPres.SelectedTabIndex == 1 || tabControlPres.SelectedTabIndex == 2)
            if (tabControlPres.SelectedTabIndex!=0)
            {
                if (txtDiagnosis.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("没有输入诊断信息不能保存处方");
                    txtDiagnosis.Focus();
                    return;
                }
            }

            SetTotalFee();
            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    SaveOMR();
                    return;
                case 1://西药中成药处方
                    if (WestPresControl.gridPresDetail.Rows.Count <= 0)
                    {
                        return;
                    }

                    WestPresControl.gridPresDetail.EndEdit();
                    WestPresControl.PrescriptionSave();
                    break;
                case 2://中草药处方
                    if (ChinesePresControl.gridPresDetail.Rows.Count <= 0)
                    {
                        return;
                    }

                    ChinesePresControl.gridPresDetail.EndEdit();
                    ChinesePresControl.PrescriptionSave();
                    break;
                case 3://材料费用
                    if (FeePresControl.gridPresDetail.Rows.Count <= 0)
                    {
                        return;
                    }

                    FeePresControl.gridPresDetail.EndEdit();
                    FeePresControl.PrescriptionSave();
                    break;
                case 4://检验检查
                    break;
                default:
                    break;
            }

            SetTotalFee();
            MessageBoxShowSimple("处方已经成功保存");
        }

        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            //仅药品处方检查诊断
            if (tabControlPres.SelectedTabIndex == 1 || tabControlPres.SelectedTabIndex == 2)
            {
                if (txtDiagnosis.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("没有输入诊断信息不能修改处方");
                    txtDiagnosis.Focus();
                    return;
                }
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    OMREditStatus = OPDEnum.OMREditiStatus.Edit;
                    rtxtSymptoms.Focus();
                    break;
                case 1://西药中成药处方
                    WestPresControl.PrescriptionEdit();
                    break;
                case 2://中草药处方
                    ChinesePresControl.PrescriptionEdit();
                    break;
                case 3://材料费用
                    FeePresControl.PrescriptionEdit();
                    break;
                case 4://检验检查
                    DataTable dt = dgApplyHead.DataSource as DataTable;
                    if (dgApplyHead.CurrentRow != null)
                    {
                        DataRow dr = dt.Rows[dgApplyHead.CurrentRow.Index];
                        InvokeController("ShowApply", dr["ApplyHeadID"].ToString(), dr["ApplyType"].ToString(), dr["ApplyStatu"].ToString(), dr["IsReturns"].ToString());
                    }
                    else
                    {
                        MessageBoxEx.Show("当前没有可修改记录");
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 换方事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnChangePres_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            if (txtDiagnosis.Text.Trim() == string.Empty)
            {
                MessageBox.Show("没有输入诊断信息不能换方");
                txtDiagnosis.Focus();
                return;
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    break;
                case 1://西药中成药处方
                    WestPresControl.PrescriptionChange();
                    break;
                case 2://中草药处方
                    ChinesePresControl.PrescriptionChange();
                    break;
                case 3://材料费用
                    break;
                case 4://检验检查
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 合组事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnMergeGroup_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            if (txtDiagnosis.Text.Trim() == string.Empty)
            {
                MessageBox.Show("没有输入诊断信息不能合组");
                txtDiagnosis.Focus();
                return;
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    break;
                case 1://西药中成药处方
                    WestPresControl.PrescriptionMergeGroup();
                    break;
                case 2://中草药处方
                    break;
                case 3://材料费用
                    break;
                case 4://检验检查
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 结束就诊事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            if (txtDiagnosis.Text.Trim() == string.Empty)
            {
                MessageBox.Show("没有输入诊断信息不能结束就诊");
                return;
            }

            InvokeController("CompleteDiagonsis", Convert.ToInt32(txtVisitNO.Tag));
            ClearPatientInfo();
            btnQuery_Click(null, null);
            //清空控件内容
            DataTable dt = (DataTable)WestPresControl.gridPresDetail.DataSource;
            dt.Rows.Clear();
            WestPresControl.gridPresDetail.DataSource = dt;

            DataTable dt1 = (DataTable)ChinesePresControl.gridPresDetail.DataSource;
            dt1.Rows.Clear();
            ChinesePresControl.gridPresDetail.DataSource = dt1;

            DataTable dt2 = (DataTable)FeePresControl.gridPresDetail.DataSource;
            dt2.Rows.Clear();
            FeePresControl.gridPresDetail.DataSource = dt2;
            DataTable dt3 = (DataTable)dgApplyHead.DataSource;
            if (dt3 != null)
            {
                dt3.Rows.Clear();
                dgApplyHead.DataSource = dt3;
            }

            lblTotal.Text = lblTotal.Text = "总金额：￥0元";
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                switch (tabSelectedIndex)
                {
                    case 0://门诊病历
                        OMRPrint();
                        break;
                    case 1://西药中成药处方
                        //PresPrint(0, false);
                        PrintAll(0, false);
                        break;
                    case 2://中草药处方
                        PrintAll(1, false);
                        break;
                    case 3://材料费用
                        PrintAll(2, false);
                        break;
                    case 4://检验检查
                        MedicalApplyPrint();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBoxEx.Show("当前没有可打印数据");
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选项卡打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabControlPres_TabItemOpen(object sender, EventArgs e)
        {
            if (tabItemWest.IsSelected)
            {
                InitDrugStoreRoom(0);
            }
            else if (tabItemChinese.IsSelected)
            {
                InitDrugStoreRoom(1);
            }
        }

        /// <summary>
        /// 选项卡点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabItemWest_Click(object sender, EventArgs e)
        {
            if (btnComplete.Enabled)
            {
                if (tabItemWest.IsSelected)
                {
                    InitDrugStoreRoom(0);
                }
                else if (tabItemChinese.IsSelected)
                {
                    InitDrugStoreRoom(1);
                }
            }
        }

        /// <summary>
        /// 选项卡改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabControlPres_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            LoadPresTemplateData();
            if (Convert.ToBoolean(txtName.Tag) == false)
            {
                return;
            }

            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            SetTplDetailColumnVisible();
            switch (tabSelectedIndex)
            {
                case 0://门诊病历
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    break;
                case 1://西药中成药处方
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = true;
                    btnMergeGroup.Enabled = true;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnPrintAll.Enabled = true;
                    break;
                case 2://中草药处方
                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = true;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnPrintAll.Enabled = true;
                    break;
                case 3://材料费用
                    if (txtCardNo.Tag != null)
                    {
                        FeePresControl.PrescriptionRefresh();
                    }

                    btnSave.Enabled = true;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                   // btnPrint.Enabled = false;
                  //  btnPrintAll.Enabled = false;
                    break;
                case 4://检验检查
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = false;
                    btnChangePres.Enabled = false;
                    btnMergeGroup.Enabled = false;
                    btnComplete.Enabled = true;
                    btnPrint.Enabled = true;
                    btnPrintAll.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 单元点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgApplyHead_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
            {
                return;
            }

            string buttonText = this.dgApplyHead.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (buttonText == "移除")
            {
                if (MessageBox.Show("确定要移除吗？", "移除前确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dt = this.dgApplyHead.DataSource as DataTable;
                    DataRow dr = dt.Rows[e.RowIndex];
                    DataTable newdt = (InvokeController("GetApplyStatus", Convert.ToInt32(dr["ApplyHeadID"])) as DataTable);
                    if (newdt != null)
                    {
                        if (newdt.Rows.Count > 0)
                        {
                            if (newdt.Rows[0]["ApplyStatus"].ToString() == "1" || newdt.Rows[0]["ApplyStatus"].ToString() == "2")
                            {
                                MessageBoxEx.Show("该记录已收费或确费不能移除");
                            }
                            else
                            {
                                InvokeController("DelApplyHead", dr["ApplyHeadID"].ToString());
                                SetTotalFee();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("该记录无法删除");
                    }
                }
            }

            if (buttonText == "退费")
            {
                if (MessageBox.Show("确定要退费吗？", "退费前确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dt = this.dgApplyHead.DataSource as DataTable;
                    int applyHeadid = Convert.ToInt32(dt.Rows[e.RowIndex]["ApplyHeadID"]);
                    InvokeController("RefundMediFee", applyHeadid);
                }
            }
        }

        /// <summary>
        /// 单元格双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgApplyHead_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dgApplyHead.DataSource as DataTable;
            if (dgApplyHead.CurrentRow != null)
            {
                DataRow dr = dt.Rows[dgApplyHead.CurrentRow.Index];
                InvokeController("ShowApply", dr["ApplyHeadID"].ToString(), dr["ApplyType"].ToString(), dr["ApplyStatu"].ToString(), dr["IsReturns"].ToString());
            }
        }

        /// <summary>
        /// 执行药房选择索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbExecStoreRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCardNo.Tag != null)
            {
                int deptid = Convert.ToInt32(cmbExecStoreRoom.SelectedValue);
                if (tabItemWest.IsSelected)
                {
                    WestPresControl.SetSeletedDrugRoomID(deptid);
                }
                else if (tabItemChinese.IsSelected)
                {
                    ChinesePresControl.SetSeletedDrugRoomID(deptid);
                }
            }
        }

        /// <summary>
        /// 热键事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmPresMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnNew.Enabled)
                    {
                        btnNew_Click(null, null);
                    }

                    break;
                case Keys.F2:
                    if (btnUpdate.Enabled)
                    {
                        btnUpdate_Click(null, null);
                    }

                    break;
                case Keys.F3:
                    if (btnChangePres.Enabled)
                    {
                        btnChangePres_Click(null, null);
                    }

                    break;
                case Keys.F4:
                    if (btnMergeGroup.Enabled)
                    {
                        btnMergeGroup_Click(null, null);
                    }

                    break;
                case Keys.F5:
                    if (btnRefresh.Enabled)
                    {
                        btnRefresh_Click(null, null);
                    }

                    break;
                case Keys.F8:
                    if (btnSave.Enabled)
                    {
                        btnSave_Click(null, null);
                    }

                    break;
                case Keys.F9:
                    if (btnComplete.Enabled)
                    {
                        btnComplete_Click(null, null);
                    }

                    break;
                default:
                    break;
            }

            if (GetFocusControlName() == "gridPresDetail")
            {
                if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
                {
                    int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                    switch (tabSelectedIndex)
                    {
                        case 1://西药中成药处方
                            if (WestPresControl.gridPresDetail.Rows.Count <= 0)
                            {
                                MessageBoxShowSimple("没有输入处方信息不能复制");
                                return;
                            }

                            WestPresControl.PrescriptionCopy();
                            break;
                        case 2://中草药处方
                            if (ChinesePresControl.gridPresDetail.Rows.Count <= 0)
                            {
                                MessageBoxShowSimple("没有输入处方信息不能复制");
                                return;
                            }

                            ChinesePresControl.PrescriptionCopy();
                            break;
                        case 3://材料费用
                            if (FeePresControl.gridPresDetail.Rows.Count <= 0)
                            {
                                MessageBoxShowSimple("没有输入费用信息不能复制");
                                return;
                            }

                            FeePresControl.PrescriptionCopy();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (GetFocusControlName() == "gridPresDetail")
            {
                if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
                {
                    int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                    switch (tabSelectedIndex)
                    {
                        case 1://西药中成药处方                        
                            WestPresControl.PrescriptionPaste();
                            break;
                        case 2://中草药处方                       
                            ChinesePresControl.PrescriptionPaste();
                            break;
                        case 3://材料费用
                            FeePresControl.PrescriptionPaste();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 开住院证点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnInpatientReg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                InvokeController("ShowDialog", "FrmInpatientCert");
            }
            else
            {
                MessageBoxEx.Show("请选择一个病人");
            }
        }

        /// <summary>
        /// 单个处方打印事件
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        private void WestPresControl_SinglePresPrint(int patListId, int presType, int presNo, bool isDoublePrint)
        {
            PresPrint(0, isDoublePrint);
        }

        /// <summary>
        /// 单个处方打印事件
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        private void ChinesePresControl_SinglePresPrint(int patListId, int presType, int presNo, bool isDoublePrint)
        {
            PresPrint(1, isDoublePrint);
        }

        /// <summary>
        /// 打印所有点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                switch (tabSelectedIndex)
                {
                    case 0://门诊病历
                        break;
                    case 1://西药中成药处方
                        PrintAll(0,true);
                        break;
                    case 2://中草药处方
                        PrintAll(1,true);
                        break;
                    case 3://材料费用
                        PrintAll(2,true);
                        break;
                    case 4://检验检查
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBoxEx.Show("当前没有可打印数据");
            }
        }

        /// <summary>
        /// 中药处方打印
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        private void ChinesePresControl_SinglePresPrint_1(int patListId, int presType, int presNo, bool isDoublePrint)
        {
            PresPrint(1, isDoublePrint);
        }

        /// <summary>
        /// 历史记录点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnHisRecord_Click(object sender, EventArgs e)
        {
            InvokeController("ShowHisRecord", cmbExecStoreRoom.SelectedValue, Convert.ToInt32(cmbDept.SelectedValue));
        }

        /// <summary>
        /// 刷新诊断事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshDianosis_Click(object sender, EventArgs e)
        {
            LoadCommonDianosis();
        }

        /// <summary>
        /// 删除常用诊断事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void mItemDelete_Click(object sender, EventArgs e)
        {
            if (dgDisease.Rows.Count == 0)
            {
                return;
            }

            if (MessageBoxEx.Show("确定要删除该常用诊断吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                int commonDianosisID = Convert.ToInt32(dgDisease["CommonDiagnosisID", dgDisease.CurrentRow.Index].Value);
                if ((bool)InvokeController("DeleteCommonDianosis", commonDianosisID))
                {
                    InvokeController("LoadCommonDianosis");
                }
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 常用诊断网格双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDisease_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNo.Tag == null)
                {
                    return;
                }

                if (dgDisease.Rows.Count == 0)
                {
                    return;
                }

                if (dgDisease.CurrentRow == null)
                {
                    return;
                }

                DataRow dRow = ((DataTable)dgDisease.DataSource).Rows[dgDisease.CurrentCell.RowIndex];
                string diagnosisName = dRow["DiagnosisName"].ToString();
                string dignosisCode = string.Empty;
                if (dRow["DiagnosisCode"] == DBNull.Value)
                {
                    dignosisCode = string.Empty;
                }
                else
                {
                    dignosisCode = dRow["DiagnosisCode"].ToString();
                }

                DataTable dtDisease = (DataTable)InvokeController("GetDiagnosisList");
                foreach (DataRow row in dtDisease.Rows)
                {
                    if (row["DiagnosisName"].ToString() == diagnosisName)
                    {
                        MessageBoxEx.Show("你已经添加了【" + diagnosisName + "】诊断，不能重复添加");
                        return;
                    }
                }

                InvokeController("AddDiagnosisFromCommon", dignosisCode, diagnosisName);
                MessageBoxShowSimple("添加诊断成功");
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 选项卡选改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (superTabControl1.SelectedTabIndex == 2)
            {
                LoadCommonDianosis();
            }
            else if (superTabControl1.SelectedTabIndex == 1)
            {
                LoadPresTemplateData();
            }
        }

        /// <summary>
        /// 淡雅U那个鼠标按下事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDisease_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                cmenuDianosis.Show(MousePosition);
            }
        }

        /// <summary>
        /// 消息显示
        /// </summary>
        /// <param name="message">消息内容</param>
        private void WestPresControl_MessageShowoflinkage(string message)
        {
            SetTotalFee();
            InvokeController("ShowMessage", message);
        }

        /// <summary>
        /// 消息显示
        /// </summary>
        /// <param name="message">消息内容</param>
        private void ChinesePresControl_MessageShowoflinkage(string message)
        {
            SetTotalFee();
            InvokeController("ShowMessage", message);
        }

        /// <summary>
        /// 消息显示
        /// </summary>
        /// <param name="message">消息内容</param>
        private void FeePresControl_MessageShowoflinkage(string message)
        {
            SetTotalFee();
            InvokeController("ShowMessage", message);
        }

        /// <summary>
        /// 刷新处方模板点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshDocTmp_Click(object sender, EventArgs e)
        {
            LoadPresTemplateData();
        }

        /// <summary>
        /// 单选按钮选中事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radAllLevel_CheckedChanged(object sender, EventArgs e)
        {
            LoadPresTemplateData();
        }

        /// <summary>
        /// 处方模板树节点双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvPresTmp_DoubleClick(object sender, EventArgs e)
        {
            //判断病人是否存在
            if (txtCardNo.Tag == null)
            {
                MessageBoxShowSimple("请选择病人");
                return;
            }

            //判断挂号有效期
            if (btnNew.Enabled == false)
            {
                return;
            }

            //获取当前节点 取得模板头ID，处方类型
            if (tvPresTmp.SelectedNode == null)
            {
                MessageBoxShowSimple("您没有选中模板树节点");
                return;
            }

            if (tvPresTmp.SelectedNode.AccessibleDescription == "0")
            {
                MessageBoxShowSimple("请您选择模板节点，再导入处方内容");
                return;
            }

            int presMouldHeadID = Convert.ToInt32(tvPresTmp.SelectedNode.Name);
            int presTmpType = Convert.ToInt32(tvPresTmp.SelectedNode.Tag);
            int tabSelectedIndex = tabControlPres.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 1://西药中成药处方
                    if (txtDiagnosis.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("没有输入诊断信息不能新开处方");
                        txtDiagnosis.Focus();
                        return;
                    }

                    WestPresControl.PrescriptionLoadTemplate(presMouldHeadID);
                    break;
                case 2://中草药处方
                    if (txtDiagnosis.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("没有输入诊断信息不能新开处方");
                        txtDiagnosis.Focus();
                        return;
                    }

                    ChinesePresControl.PrescriptionLoadTemplate(presMouldHeadID);
                    break;
                case 3://材料费用
                    FeePresControl.PrescriptionLoadTemplate(presMouldHeadID);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处方模板明细双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgPresTmpdetail_DoubleClick(object sender, EventArgs e)
        {
            //判断病人是否存在
            if (txtCardNo.Tag == null)
            {
                MessageBoxShowSimple("请选择病人");
                return;
            }

            //判断挂号有效期
            if (btnNew.Enabled == false)
            {
                return;
            }

            if (dgPresTmpdetail.Rows.Count == 0)
            {
                return;
            }

            if (dgPresTmpdetail.CurrentCell != null)
            {
                if (dgPresTmpdetail.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgPresTmpdetail.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgPresTmpdetail.DataSource).Rows[rowIndex];
                    int presTplDetailId = Convert.ToInt32(dRow["PresMouldDetailID"]);
                    int tabSelectedIndex = tabControlPres.SelectedTabIndex;
                    int[] ids = new int[1];
                    ids[0] = presTplDetailId;
                    switch (tabSelectedIndex)
                    {
                        case 1://西药中成药处方
                            if (txtDiagnosis.Text.Trim() == string.Empty)
                            {
                                MessageBox.Show("没有输入诊断信息不能新开处方");
                                txtDiagnosis.Focus();
                                return;
                            }

                            WestPresControl.PrescriptionLoadTemplateRow(ids);
                            break;
                        case 2://中草药处方
                            if (txtDiagnosis.Text.Trim() == string.Empty)
                            {
                                MessageBox.Show("没有输入诊断信息不能新开处方");
                                txtDiagnosis.Focus();
                                return;
                            }

                            ChinesePresControl.PrescriptionLoadTemplateRow(ids);
                            break;
                        case 3://材料费用
                            FeePresControl.PrescriptionLoadTemplateRow(ids);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 处方木棒树节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvPresTmp_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (tvPresTmp.Nodes.Count <= 0)
            {
                return;
            }

            //获取当前节点 取得模板头ID，处方类型
            if (tvPresTmp.SelectedNode == null)
            {
                MessageBoxShowSimple("您没有选中模板树节点");
                return;
            }

            if (tvPresTmp.SelectedNode.AccessibleDescription == "0")
            {
                //绑定处方模板明细
                InvokeController("GetPresTemplateDetail", -1);
                return;
            }

            int presMouldHeadID = Convert.ToInt32(tvPresTmp.SelectedNode.Name);
            int presTmpType = Convert.ToInt32(tvPresTmp.SelectedNode.Tag);
            //绑定处方模板明细
            InvokeController("GetPresTemplateDetail", presMouldHeadID);
        }

        /// <summary>
        /// 双薪药品ShowCard数据事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshShowCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (tabControlPres.SelectedTabIndex == 1)
            {
                WestPresControl.RefreshDrugData();
            }
            else if (tabControlPres.SelectedTabIndex == 2)
            {
                ChinesePresControl.RefreshDrugData();
            }
            else if (tabControlPres.SelectedTabIndex == 3)
            {
                FeePresControl.RefreshDrugData();
            }

            this.Cursor = Cursors.Default;
        }
        #endregion

        #region 病历相关事件
        /// <summary>
        /// 清除所有文本框事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (OMREditStatus == OPDEnum.OMREditiStatus.Edit)
            {
                rtxtSymptoms.Text = string.Empty;
                rtxtSicknessHistory.Text = string.Empty;
                rtxtPhysicalExam.Text = string.Empty;
            }
            else
            {
                MessageBoxShowSimple("只能在编辑状态下清空");
            }
        }

        /// <summary>
        /// 清除输入事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClearInput_Click(object sender, EventArgs e)
        {
            if (OMREditStatus == OPDEnum.OMREditiStatus.Edit)
            {
                if (rtxtSymptoms.Focused)
                {
                    rtxtSymptoms.Text = string.Empty;
                }

                if (rtxtSicknessHistory.Focused)
                {
                    rtxtSicknessHistory.Text = string.Empty;
                }

                if (rtxtPhysicalExam.Focused)
                {
                    rtxtPhysicalExam.Text = string.Empty;
                }
            }
            else
            {
                MessageBoxShowSimple("只能在编辑状态下清空");
            }
        }

        /// <summary>
        /// 选项卡改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabControlPres_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (e.OldTab.Name == "tabItemOMR")
            {
                //病历是否处于编辑状态
                if (IsOMRSave() == false)
                {
                    e.Cancel = true;
                }
            }
            else if (e.OldTab.Name == "tabItemFees")
            {
                DataTable dtFee = FeePresControl.gridPresDetail.DataSource as DataTable;
                if (dtFee != null && dtFee.Rows.Count > 0)
                {
                    DataRow[] drs = dtFee.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存费用数据，请保存");
                        e.Cancel = true;
                    }
                }
            }
            else if (e.OldTab.Name == "tabItemChinese")
            {
                DataTable dtChinese = ChinesePresControl.gridPresDetail.DataSource as DataTable;
                if (dtChinese != null && dtChinese.Rows.Count > 0)
                {
                    DataRow[] drs = dtChinese.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存中草药处方数据，请保存");
                        e.Cancel = true;
                    }
                }
            }
            else if (e.OldTab.Name == "tabItemWest")
            {
                DataTable dtWest = WestPresControl.gridPresDetail.DataSource as DataTable;
                if (dtWest != null && dtWest.Rows.Count > 0)
                {
                    DataRow[] drs = dtWest.Select("Status=4 and Item_Id_s<>''");
                    if (drs.Length > 0)
                    {
                        MessageBoxShowSimple("您没有保存西成处方数据，请保存");
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 特殊字符点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSymbol_Click(object sender, EventArgs e)
        {
            string insertSymbol = (sender as ButtonX).Text;

            if (OMREditStatus == OPDEnum.OMREditiStatus.Edit)
            {
                if (currentRichTextBox == rtxtSymptoms)
                {
                    InsertSymbol(rtxtSymptoms, insertSymbol);
                }

                if (currentRichTextBox == rtxtSicknessHistory)
                {
                    InsertSymbol(rtxtSicknessHistory, insertSymbol);
                }

                if (currentRichTextBox == rtxtPhysicalExam)
                {
                    InsertSymbol(rtxtPhysicalExam, insertSymbol);
                }
            }
            else
            {
                MessageBoxShowSimple("只有处于编辑状态下才能插入符号");
            }
        }

        /// <summary>
        /// 字符类型选择改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbCharType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCharType.SelectedValue != null)
            {
                flpSymbol.Controls.Clear();

                DataView dictSymbol = new DataView((DataTable)cmbCharType.Tag);
                dictSymbol.RowFilter = "ClassId=" + Convert.ToString(cmbCharType.SelectedValue);

                foreach (DataRowView dr in dictSymbol)
                {
                    ButtonX btnSymbol = new ButtonX();
                    btnSymbol.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                    btnSymbol.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                    btnSymbol.Text = dr["SymbolText"].ToString();
                    btnSymbol.Tooltip = dr["SymbolText"].ToString();
                    btnSymbol.Cursor = Cursors.Hand;
                    btnSymbol.Shape = new RoundRectangleShapeDescriptor();
                    btnSymbol.Size = new Size(23, 23);
                    btnSymbol.FocusCuesEnabled = false;
                    btnSymbol.TabStop = false;
                    btnSymbol.Click += new EventHandler(btnSymbol_Click);
                    flpSymbol.Controls.Add(btnSymbol);
                }
            }
        }

        /// <summary>
        /// 文本聚焦事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rtxtSymptoms_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = rtxtSymptoms;
        }

        /// <summary>
        /// 文本聚焦事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rtxtSicknessHistory_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = rtxtSicknessHistory;
        }

        /// <summary>
        /// 文本聚焦事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rtxtDisease_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = rtxtDisease;
        }

        /// <summary>
        /// 文本聚焦事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rtxtAuxiliaryExam_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = rtxtAuxiliaryExam;
        }

        /// <summary>
        /// 文本聚焦事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rtxtPhysicalExam_Enter(object sender, EventArgs e)
        {
            currentRichTextBox = rtxtPhysicalExam;
        }

        /// <summary>
        /// 单选按钮选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radOAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadOMRTemplateData();
        }

        /// <summary>
        /// 另存模板点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnAsSave_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Tag == null)
            {
                MessageBox.Show("请选择病人");
                return;
            }

            if (rtxtSymptoms.Text.Trim() == string.Empty)
            {
                MessageBox.Show("没有主诉信息不能存为模板");
                return;
            }

            OPD_OMRTmpDetail detailModel = new OPD_OMRTmpDetail();
            detailModel.OMRTmpHeadID = 0;
            detailModel.Symptoms = rtxtSymptoms.Text.Trim();
            detailModel.SicknessHistory = rtxtSicknessHistory.Text.Trim();
            detailModel.PhysicalExam = rtxtPhysicalExam.Text.Trim();
            detailModel.DocAdvise = string.Empty;
            detailModel.AuxiliaryExam = rtxtAuxiliaryExam.Text.Trim();
            InvokeController("ShowOMRTplDialog", detailModel);
        }

        /// <summary>
        /// 树节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvOMRTpl_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (tvOMRTpl.Nodes.Count <= 0)
            {
                return;
            }

            //获取当前节点 取得模板头ID，处方类型
            if (tvOMRTpl.SelectedNode == null)
            {
                MessageBoxShowSimple("您没有选中模板树节点");
                return;
            }

            if (tvOMRTpl.SelectedNode.AccessibleDescription == "0" || tvOMRTpl.SelectedNode.Name == "-1")
            {
                rtxtTplContent.Text = "注意：暂无显示模板内容，请点击模板节点显示模板内容";
                return;
            }
            else
            {
                int mouldHeadID = Convert.ToInt32(tvOMRTpl.SelectedNode.Name);
                //绑定处方模板明细
                InvokeController("GetOMRTemplateDetail", mouldHeadID);
            }
        }

        /// <summary>
        /// 树节点双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvOMRTpl_DoubleClick(object sender, EventArgs e)
        {
            //判断病人是否存在
            if (txtCardNo.Tag == null)
            {
                MessageBoxShowSimple("请选择病人");
                return;
            }

            //判断挂号有效期
            if (btnNew.Enabled == false)
            {
                MessageBoxShowSimple("超过挂号有效期不能导入");
                return;
            }

            if (tvOMRTpl.SelectedNode == null)
            {
                return;
            }

            if (tvOMRTpl.SelectedNode.AccessibleDescription == "0")
            {
                MessageBoxShowSimple("请选择模板节点");
                return;
            }

            if (rtxtTplContent.Text == string.Empty)
            {
                MessageBoxShowSimple("没有模板内容不能导入");
                return;
            }

            OPD_OMRTmpDetail model = (OPD_OMRTmpDetail)rtxtTplContent.Tag;
            if (model != null)
            {
                if (rtxtSymptoms.Text.Trim() == string.Empty || model.Symptoms.Trim() == string.Empty)
                {
                    rtxtSymptoms.Text += model.Symptoms;
                }
                else
                {
                    rtxtSymptoms.Text += "，" + model.Symptoms;
                }

                if (rtxtSicknessHistory.Text.Trim() == string.Empty || model.SicknessHistory.Trim() == string.Empty)
                {
                    rtxtSicknessHistory.Text += model.SicknessHistory;
                }
                else
                {
                    rtxtSicknessHistory.Text += "，" + model.SicknessHistory;
                }

                if (rtxtPhysicalExam.Text.Trim() == string.Empty || model.PhysicalExam == string.Empty)
                {
                    rtxtPhysicalExam.Text += model.PhysicalExam;
                }
                else
                {
                    rtxtPhysicalExam.Text += "，" + model.PhysicalExam;
                }
            }

            OMREditStatus = OPDEnum.OMREditiStatus.Edit;
        }

        private void FeePresControl_CostListPrint(int patListId)
        {
            PresPrint(2, false);
        }
        #endregion
       
    }
}