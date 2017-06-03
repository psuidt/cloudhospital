using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.HISControl.BedCard.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    public partial class FrmIPDoctorMain : BaseFormBusiness, IFrmIPDoctorMain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmIPDoctorMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        private bool loadcompleted;

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool LoadCompleted
        {
            get
            {
                return loadcompleted;
            }

            set
            {
                loadcompleted = value;
            }
        }

        /// <summary>
        /// 选择的病人ID
        /// </summary>
        public int SelectPatListID
        {
            get
            {
                int patlistid = 0;
                if (superTabControl1.SelectedTabIndex == 0)
                {
                    if (myBedPatient.SelectedBed != null)
                    {
                        patlistid = myBedPatient.SelectedBed.PatientID;
                    }
                }

                if (superTabControl1.SelectedTabIndex == 1)
                {
                    if (DeptBedPatient.SelectedBed != null)
                    {
                        patlistid = DeptBedPatient.SelectedBed.PatientID;
                    }
                }

                return patlistid;
            }
        }

        /// <summary>
        /// 血糖Url
        /// </summary>
        private string bloodurl;

        /// <summary>
        /// 血糖Url
        /// </summary>
        public string BloodUrl
        {
            get
            {
                return bloodurl;
            }

            set
            {
                bloodurl = value;
            }
        }

        /// <summary>
        /// 绑定科室病人信息
        /// </summary>
        /// <param name="dtDeptPatient">科室病人</param>
        public void BindDeptBedPatient(DataTable dtDeptPatient)
        {
            DeptBedPatient.DataSource = null;
            // 定义床位集合
            List<BedInfo> list = GetBedInfo(dtDeptPatient);          
            DeptBedPatient.DataSource = list;           
        }

        #region 床头卡信息赋值
        /// <summary>
        /// 床头卡信息绑定
        /// </summary>
        /// <param name="dtPatient">病人信息</param>
        /// <returns>床头卡信息</returns>
        private List<BedInfo> GetBedInfo(DataTable dtPatient)
        {
            // 定义床位集合
            List<BedInfo> list = new List<BedInfo>();
            // 当前病区有床位的场合
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                // 循环显示床位
                for (int i = 0; i < dtPatient.Rows.Count; i++)
                {
                    BedInfo bed = new BedInfo();
                    // 床位相关数据
                    bed.BedNo = dtPatient.Rows[i]["BedNO"].ToString(); // 床位号
                    bed.WardCode = dtPatient.Rows[i]["WardID"].ToString(); // 病区ID
                    bed.PatientID = Convert.ToInt32(dtPatient.Rows[i]["PatListID"].ToString());  // 病人登记ID
                    bed.PatientNum = dtPatient.Rows[i]["SerialNumber"].ToString();   // 住院流水号
                    bed.CaseNumber = Tools.ToDecimal(dtPatient.Rows[i]["CaseNumber"].ToString());   // 住院病案号
                    bed.PatientName = dtPatient.Rows[i]["PatName"].ToString(); // 病人姓名
                    bed.Sex = dtPatient.Rows[i]["PatSex"].ToString(); // 性别
                    bed.Age = GetAge(dtPatient.Rows[i]["Age"].ToString());  // 年龄
                    bed.Diagnosis = dtPatient.Rows[i]["EnterDiseaseName"].ToString(); // 入院诊断
                    bed.Dept = dtPatient.Rows[i]["DeptName"].ToString(); // 入院科室
                    bed.DeptCode = dtPatient.Rows[i]["PatDeptID"].ToString(); // 入院科室
                    bed.Doctor = dtPatient.Rows[i]["DoctorName"].ToString(); // 医生
                    bed.Nurse = dtPatient.Rows[i]["NurseName"].ToString(); // 护士
                    bed.EnterTime = DateTime.Parse(dtPatient.Rows[i]["EnterHDate"].ToString()).ToString("yyyy-MM-dd"); // 入院时间 
                    bed.PatTypeName= dtPatient.Rows[i]["patTypeName"].ToString();
                    bed.NursingLever = dtPatient.Rows[i]["NursingLever"].ToString();//护理级别
                    bed.DietType = dtPatient.Rows[i]["DietType"].ToString();//饮食种类
                    bed.Situation= dtPatient.Rows[i]["OutSituation"].ToString();//出院情况
                    bed.Step =Convert.ToInt32( dtPatient.Rows[i]["IsLeaveHosOrder"]);//是否定义出院
                    list.Add(bed);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">传入年龄字符</param>
        /// <returns>转换后年龄</returns>
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

        #endregion
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        public void BindDept(DataTable dtDept)
        {
            cmbDept.DisplayMember = "Name";
            cmbDept.ValueMember = "deptID";
            cmbDept.DataSource = dtDept;
        }

        /// <summary>
        /// 选择的科室ID
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
        /// 绑定我的病人床位信息
        /// </summary>
        /// <param name="dtMyPatient">我的病人</param>
        public void BindMyBedPatient(DataTable dtMyPatient)
        {
            myBedPatient.DataSource = null;
            List<BedInfo> list = GetBedInfo(dtMyPatient);
            myBedPatient.DataSource = list;
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmIPDoctorMain_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDept");         
            superTabControl1.SelectedTabIndex = 0;
            // 设置床位控件显示内容
            myBedPatient.BedContextFields = new List<ContextField>();
            myBedPatient.BedContextFields.Add(new ContextField("住院号", "CaseNumber"));
            myBedPatient.BedContextFields.Add(new ContextField("科室", "Dept"));
            myBedPatient.BedContextFields.Add(new ContextField("医生", "Doctor"));
            myBedPatient.BedContextFields.Add(new ContextField("护士", "Nurse"));
            myBedPatient.BedContextFields.Add(new ContextField("入科", "EnterTime"));           
            myBedPatient.BedContextFields.Add(new ContextField("类型", "PatTypeName"));
            //myBedPatient.BedContextFields.Add(new ContextField("护理等级", "NursingLever"));
            //myBedPatient.BedContextFields.Add(new ContextField("饮食种类", "DietType"));
            //myBedPatient.BedContextFields.Add(new ContextField("病人情况", "Situation"));
            myBedPatient.BedContextFields.Add(new ContextField("诊断", "Diagnosis"));

            DeptBedPatient.BedContextFields = new List<ContextField>();
            DeptBedPatient.BedContextFields.Add(new ContextField("住院号", "CaseNumber"));
            DeptBedPatient.BedContextFields.Add(new ContextField("科室", "Dept"));
            DeptBedPatient.BedContextFields.Add(new ContextField("医生", "Doctor"));
            DeptBedPatient.BedContextFields.Add(new ContextField("护士", "Nurse"));
            DeptBedPatient.BedContextFields.Add(new ContextField("入科", "EnterTime"));           
            DeptBedPatient.BedContextFields.Add(new ContextField("类型", "PatTypeName"));
            //DeptBedPatient.BedContextFields.Add(new ContextField("护理等级", "NursingLever"));
            //DeptBedPatient.BedContextFields.Add(new ContextField("饮食种类", "DietType"));
            //DeptBedPatient.BedContextFields.Add(new ContextField("病人情况", "Situation"));
            DeptBedPatient.BedContextFields.Add(new ContextField("诊断", "Diagnosis"));
            InvokeController("GetBedPatient");

            InvokeController("GetBloodUrl");

            DataTable dtNusingLever = (DataTable)InvokeController("getNusingLever");//护理级别
            for (int i = 0; i < dtNusingLever.Rows.Count; i++)
            {
                ToolStripItem tsItem = new ToolStripMenuItem(dtNusingLever.Rows[i]["Name"].ToString());
                tsItem.Tag = dtNusingLever.Rows[i]["Code"];
                tsItem.Click += new EventHandler(tsItemNusingLevel_Click);
                this.护理级别ToolStripMenuItem.DropDownItems.Add(tsItem);
            }

            DataTable dtDietType = (DataTable)InvokeController("getDietType");//饮食种类
            for (int i = 0; i < dtDietType.Rows.Count; i++)
            {
                ToolStripItem tsItem = new ToolStripMenuItem(dtDietType.Rows[i]["Name"].ToString());
                tsItem.Tag = dtDietType.Rows[i]["Code"];
                tsItem.Click += new EventHandler(tsDietType_Click);
                this.饮食ToolStripMenuItem.DropDownItems.Add(tsItem);
            }

            DataTable dtOutSituation = (DataTable)InvokeController("getOutSituation");//出院情况
            for (int i = 0; i < dtOutSituation.Rows.Count; i++)
            {
                ToolStripItem tsItem= new ToolStripMenuItem(dtOutSituation.Rows[i]["Name"].ToString());
                tsItem.Tag = dtOutSituation.Rows[i]["Code"];
                tsItem.Click += new EventHandler(tsItem_Click);
                this.病人情况ToolStripMenuItem.DropDownItems.Add(tsItem);
            }            
        }

        /// <summary>
        /// 护理级别修改
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tsItemNusingLevel_Click(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                string curItem = (sender as ToolStripItem).Text;
                string situationCode = (sender as ToolStripItem).Tag.ToString();
                if (MessageBox.Show("确定将护理级别改为" + curItem + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    InvokeController("UpdatePatNursing", SelectPatListID, situationCode);
                    InvokeController("GetBedPatient");
                }
            }
        }

        /// <summary>
        /// 病人饮食种类修改
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tsDietType_Click(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                string curItem = (sender as ToolStripItem).Text;
                string situationCode = (sender as ToolStripItem).Tag.ToString();
                if (MessageBox.Show("确定将病人饮食种类改为" + curItem + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    InvokeController("UpdatePatDietType", SelectPatListID, situationCode);
                    InvokeController("GetBedPatient");
                }
            }
        }

        /// <summary>
        /// 病人情况修改
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tsItem_Click(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                string curItem= (sender as ToolStripItem).Text;
                string situationCode = (sender as ToolStripItem).Tag.ToString();
                if (MessageBox.Show("确定将病人情况改为" + curItem + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    InvokeController("UpdatePatSituation", SelectPatListID, situationCode);
                    InvokeController("GetBedPatient");
                }
            }
        }

        /// <summary>
        /// 科室改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbDept_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InvokeController("GetBedPatient");
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("GetBedPatient");
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 病人医嘱按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnOrder_Click(object sender, EventArgs e)
        {
            //if (SelectPatListID != 0)
            //{              
            //    InvokeController("Show", "FrmOrderManager");
            //}
            //else
            //{
            //    MessageBoxEx.Show("请选择病人");
            //}
            if (loadcompleted)
            {
                InvokeController("Show", "FrmOrderManager");
                InvokeController("OrderManagerInit");
            }
        }

        /// <summary>
        /// 科室病人床头卡双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void DeptBedPatient_BedDoubleClick(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                if (loadcompleted)
                {
                    InvokeController("Show", "FrmOrderManager");
                    InvokeController("OrderManagerInit");
                }
            }
        }

        /// <summary>
        /// 我的病人床头卡双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void myBedPatient_BedDoubleClick(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                if (loadcompleted)
                {
                    InvokeController("Show", "FrmOrderManager");
                    InvokeController("OrderManagerInit");
                }
            }
        }

        /// <summary>
        /// 血糖数据查看
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void 血糖数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                if (superTabControl1.SelectedTabIndex == 0)
                {
                    if (myBedPatient.SelectedBed != null)
                    {
                        InvokeController("OpenBrowser", myBedPatient.SelectedBed.PatientNum,myBedPatient.SelectedBed.PatientName);
                    }
                }

                if (superTabControl1.SelectedTabIndex == 1)
                {
                    if (DeptBedPatient.SelectedBed != null)
                    {
                        InvokeController("OpenBrowser", DeptBedPatient.SelectedBed.PatientNum, DeptBedPatient.SelectedBed.PatientName);
                    }
                }               
            }
        }

        private void tsmHomePage_Click(object sender, EventArgs e)
        {
            if (SelectPatListID != 0)
            {
                if (superTabControl1.SelectedTabIndex == 0)
                {
                    if (myBedPatient.SelectedBed != null)
                    {
                        InvokeController("ShowMedicalCasePage", myBedPatient.SelectedBed.PatientID, myBedPatient.SelectedBed.DeptCode, myBedPatient.SelectedBed.Dept);
                    }
                }

                if (superTabControl1.SelectedTabIndex == 1)
                {
                    if (DeptBedPatient.SelectedBed != null)
                    {
                        InvokeController("ShowMedicalCasePage", DeptBedPatient.SelectedBed.PatientID, DeptBedPatient.SelectedBed.DeptCode,DeptBedPatient.SelectedBed.Dept);
                    }
                }
            }
        }
    }
}
