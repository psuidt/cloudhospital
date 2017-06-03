using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using Newtonsoft.Json;

namespace HIS_OPDoctor.Winform.ViewForm.MedicalApply
{
    /// <summary>
    /// 检查申请单
    /// </summary>
    public partial class FrmMedicalApply : BaseFormBusiness, IFrmMedicalApply
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMedicalApply()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 0门珍 1住院
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 获取当前项目大类型值
        /// </summary>
        public int ExamClass { get; set; }

        /// <summary>
        /// 获取当前选中项目信息
        /// </summary>
        public DataTable CurrentItemData;

        /// <summary>
        /// 获取当前选中项目类型信息
        /// </summary>
        public DataTable CurrentTypeData;

        /// <summary>
        /// 获取已保存信息
        /// </summary>
        public DataTable UpdateData;

        /// <summary>
        /// 获取当前选中病人信息
        /// </summary>
        public DataTable CurrentPatList;

        /// <summary>
        /// 选中类型索引
        /// </summary>
        public int SelectTypeIndex { get; set; }

        /// <summary>
        /// 选中科室索引
        /// </summary>
        public int SelectDeptIndex { get; set; }

        /// <summary>
        /// 获取当前表头ID
        /// </summary>
        public string ApplyHeadID { get; set; }

        /// <summary>
        /// 获取当前表头ID
        /// </summary>
        public string TempApplyHeadID { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public string ApplyStatu { get; set; }

        /// <summary>
        /// 是否退费
        /// </summary>
        public string IsReturns { get; set; }

        /// <summary>
        /// 获取当前申请单类型
        /// </summary>
        public string ApplyType { get; set; }

        /// <summary>
        /// 获取当前需要保存数据
        /// </summary>
        public DataTable SaveItemData { get; set; }

        /// <summary>
        /// 化验单属性JSON对象
        /// </summary>
        public TestJson Test { get; set; }

        /// <summary>
        /// 申请单属性JSON对象
        /// </summary>
        public CheckJson Check { get; set; }

        /// <summary>
        /// 是否加载进来的
        /// </summary>
        private bool IsLoad { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public int PatListID { get; set; }

        /// <summary>
        /// 化验Json类
        /// </summary>
        public class TestJson
        {
            /// <summary>
            /// 目标
            /// </summary>
            public string Goal { get; set; }

            /// <summary>
            /// 标本编码
            /// </summary>
            public string Sample { get; set; }

            /// <summary>
            /// 标本属性名称
            /// </summary>
            public string SampleName { get; set; }
        }

        /// <summary>
        /// 检查属性Json类
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
        #endregion

        #region 动态获取对象
        /// <summary>
        /// 获取科室下拉对象
        /// </summary>
        /// <returns>下拉框控件</returns>
        public ComboBoxEx GetDeptObj()
        {
            var cbDepts = cbDept as ComboBoxEx;
            if (ExamClass == 1)
            {
                cbDepts = cbDeptCK as ComboBoxEx;
            }
            else if (ExamClass == 4)
            {
                cbDepts = cbDeptZL as ComboBoxEx;
            }

            return cbDepts;
        }

        /// <summary>
        /// 获取项目类型下拉对象
        /// </summary>
        /// <returns>下拉框控件</returns>
        public ComboBoxEx GetTypeObj()
        {
            var cbTypes = cbType as ComboBoxEx;
            if (ExamClass == 1)
            {
                cbTypes = cbTypeCK as ComboBoxEx;
            }
            else if (ExamClass == 4)
            {
                cbTypes = cbTypeZL as ComboBoxEx;
            }

            return cbTypes;
        }

        /// <summary>
        /// 获取打印对象
        /// </summary>
        /// <returns>复选框控件</returns>
        public CheckBoxX GetPrintObj()
        {
            var cbPrints = cbPrintHY as CheckBoxX;
            if (ExamClass == 1)
            {
                cbPrints = cbPrint as CheckBoxX;
            }
            else if (ExamClass == 4)
            {
                cbPrints = cbPrintZL as CheckBoxX;
            }

            return cbPrints;
        }

        /// <summary>
        /// 获取项目类型搜索框对象
        /// </summary>
        /// <returns>文本Showcard控件</returns>
        public TextBoxCard GetExecItemObj()
        {
            var txtExamItems = txtExamItem as TextBoxCard;
            if (ExamClass == 1)
            {
                txtExamItems = txtExamItemCK as TextBoxCard;
            }
            else if (ExamClass == 4)
            {
                txtExamItems = txtExamItemZL as TextBoxCard;
            }

            return txtExamItems;
        }

        /// <summary>
        /// 获取项目ListView对象
        /// </summary>
        /// <returns>ListView控件对象</returns>
        public ListView GetExamListObj()
        {
            var execItemLists = ExecItemList as ListView;
            if (ExamClass == 1)
            {
                execItemLists = ExecItemListCK as ListView;
            }
            else if (ExamClass == 4)
            {
                execItemLists = ExecItemListZL as ListView;
            }

            return execItemLists;
        }

        /// <summary>
        /// 获取项目网格对象
        /// </summary>
        /// <returns>网格控件</returns>
        public EfwControls.CustomControl.DataGrid GetExamTableObj()
        {
            var dgExecItems = dgExecItem as EfwControls.CustomControl.DataGrid;
            if (ExamClass == 1)
            {
                dgExecItems = dgExecItemCK as EfwControls.CustomControl.DataGrid;
            }
            else if (ExamClass == 4)
            {
                dgExecItems = dgExecItemZL as EfwControls.CustomControl.DataGrid;
            }

            return dgExecItems;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定医技申请科室
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void BindExecDept(DataTable dtDept)
        {
            var cbDepts = GetDeptObj();
            cbDepts.SelectedIndexChanged -= cbDept_SelectedIndexChanged;
            cbDepts.DataSource = dtDept;
            cbDepts.SelectedIndexChanged += cbDept_SelectedIndexChanged;
            if (dtDept.Rows.Count > 0)
            {
                if (UpdateData != null && ApplyType == ApplyControl.SelectedTabIndex.ToString())
                {
                    cbDepts.SelectedValue = UpdateData.Rows[0]["ExecuteDeptID"];
                }
                else
                {
                    cbDepts.SelectedIndex = 0;
                }

                if (cbDepts.SelectedIndex == 0)
                {
                    cbDept_SelectedIndexChanged(null, null);
                }
            }

            SelectDeptIndex = cbDepts.SelectedIndex;
        }

        /// <summary>
        /// 绑定项目分类
        /// </summary>
        /// <param name="dtType">项目分类数据</param>
        public void BindExecType(DataTable dtType)
        {
            bool isSelect = false;
            CurrentTypeData = dtType;
            var cbTypes = GetTypeObj();
            cbTypes.SelectedIndexChanged -= cbType_SelectedIndexChanged;
            cbTypes.DataSource = dtType;
            cbTypes.SelectedIndexChanged += cbType_SelectedIndexChanged;
            if (dtType.Rows.Count > 0)
            {
                if (UpdateData != null && ApplyType == ApplyControl.SelectedTabIndex.ToString())
                {
                    DataRow drType = dtType.Select("ExamTypeID=" + Tools.ToString(UpdateData.Rows[0]["ExamTypeID"]) ).FirstOrDefault();
                    if (drType != null)
                    {
                        cbTypes.SelectedValue = UpdateData.Rows[0]["ExamTypeID"];
                    }
                    else
                    {
                        isSelect = true;
                        cbTypes.SelectedIndex = 0;
                    }
                }
                else
                {
                    isSelect = true;
                    cbTypes.SelectedIndex = 0;
                }

                if (!isSelect)
                {
                    SelectTypeIndex = cbTypes.SelectedIndex;
                }
                else
                {
                    SelectTypeIndex = -1;
                }

                if (cbTypes.SelectedIndex == 0)
                {
                    cbType_SelectedIndexChanged(null, null);
                }
            }

            IsLoad = false;
        }

        /// <summary>
        /// 绑定项目信息
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        public void BindExecItem(DataTable dtItem)
        {
            var execItemLists = GetExamListObj();
            BindExecShowCard(dtItem);
            CurrentItemData = dtItem;
            execItemLists.Clear();
            execItemLists.BeginUpdate();
            for (int i = 0; i < dtItem.Rows.Count; i++)
            {
                ListViewItem newitem = new ListViewItem();
                newitem.Name = dtItem.Rows[i]["ExamItemID"].ToString();
                newitem.Text = dtItem.Rows[i]["ExamItemName"].ToString();
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 20);
                execItemLists.SmallImageList = imgList;
                execItemLists.Items.Add(newitem);
            }

            execItemLists.EndUpdate();
        }

        /// <summary>
        /// 设置网格数据
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        public void SetExecItem(DataTable dtItem)
        {
            var dgExecItems = GetExamTableObj();
            var execItemLists = GetExamListObj();
            execItemLists.ItemChecked -= ExecItemList_ItemChecked;
            for (int i = 0; i < dtItem.Rows.Count; i++)
            {
                ListViewItem item = execItemLists.Items.Find(dtItem.Rows[i]["ExamItemID"].ToString(), false).FirstOrDefault();
                if (item != null)
                {
                    item.Checked = true;
                }
            }

            execItemLists.ItemChecked += ExecItemList_ItemChecked;
            dgExecItems.DataSource = dtItem;
        }

        /// <summary>
        /// 绑定检验标本
        /// </summary>
        /// <param name="dtSample">标本数据</param>
        public void BindSample(DataTable dtSample)
        {
            multcbSample.displayField = "Name";
            multcbSample.valueField = "Id";
            multcbSample.DataSource = dtSample;
            if (CurrentTypeData != null)
            {
                if (CurrentTypeData.Rows.Count > 0)
                {
                    List<object> values = new List<object>();
                    string[] samplestr = CurrentTypeData.Rows[0]["SampleID"].ToString().Split(',');
                    for (var i = 0; i < samplestr.Length; i++)
                    {
                        values.Add(samplestr[i]);
                    }

                    multcbSample.SelectValue = values.ToArray();
                }
            }
        }

        /// <summary>
        /// 绑定项目搜索ShowCard
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        public void BindExecShowCard(DataTable dtItem)
        {
            var txtExamItems = GetExecItemObj();
            txtExamItems.DisplayField = "ExamItemName";
            txtExamItems.MemberField = "ExamItemID";
            txtExamItems.CardColumn = "ExamItemID|编码|55,ExamItemName|项目名称|auto";
            txtExamItems.QueryFieldsString = "ExamItemName,PYCode,WBCode";
            txtExamItems.ShowCardWidth = 260;
            txtExamItems.ShowCardDataSource = dtItem;
        }

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <param name="dtHeadDetail">表头和明细数据</param>
        public void BindHeadDetail(DataTable dtHeadDetail)
        {
            UpdateData = dtHeadDetail;
            if (UpdateData != null)
            {
                if (UpdateData.Rows.Count > 0)
                {
                    switch (UpdateData.Rows[0]["ApplyType"].ToString())
                    {
                        case "0":
                            Check = JsonConvert.DeserializeObject<CheckJson>(UpdateData.Rows[0]["ApplyContent"].ToString());
                            break;
                        case "1":
                            Test = JsonConvert.DeserializeObject<TestJson>(UpdateData.Rows[0]["ApplyContent"].ToString());
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 加载默认勾选项网格数据
        /// </summary>
        public void InitdgData()
        {
            if (UpdateData != null)
            {
                if (ApplyType == ApplyControl.SelectedTabIndex.ToString())
                {
                    var cbDepts = GetDeptObj();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ExamItemID");
                    dt.Columns.Add("ExamItemName");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("Amount");

                    //if (ApplyType == "2")
                    //{
                    //    dt.Columns.Add("Amount");
                    //}
                    dt.Columns.Add("ExecuteDeptName");
                    dt.Columns.Add("ApplyStatus");
                    dt.Columns.Add("DelFlag");
                    for (int i = 0; i < UpdateData.Rows.Count; i++)
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["ExamItemID"] = UpdateData.Rows[i]["ItemID"];
                        newdr["ExamItemName"] = UpdateData.Rows[i]["ItemName"];
                        newdr["Price"] = UpdateData.Rows[i]["Price"];
                        newdr["ExecuteDeptName"] = cbDepts.Text;
                        newdr["Amount"] = UpdateData.Rows[i]["Amount"];

                        //if (ApplyType == "2")
                        //{
                        //    newdr["Amount"] = UpdateData.Rows[i]["Amount"];
                        //}
                        newdr["ApplyStatus"] = GetStatuName(UpdateData.Rows[i]["ApplyStatu"].ToString(), UpdateData.Rows[i]["IsReturns"].ToString());
                        newdr["DelFlag"] = "移除";
                        dt.Rows.Add(newdr);
                    }

                    SetExecItem(dt);
                    InitProperty();
                }

                switch(ApplyType)
                {
                    case "0":
                        dateTimeCheck.Text = UpdateData.Rows[0]["CheckDate"].ToString();
                        break;
                    case "1":
                        datetimeHY.Text = UpdateData.Rows[0]["CheckDate"].ToString();
                        break;
                    case "2":
                        dateTimeZL.Text = UpdateData.Rows[0]["CheckDate"].ToString();
                        break;

                }
            }
            else
            {
                if (SystemType == 0)
                {
                    OPD_MedicalRecord medical = InvokeController("GetMedical", PatListID) as OPD_MedicalRecord;
                    if (medical != null)
                    {
                        txtDigest.Text = medical.SicknessHistory;
                    }
                }
                else
                {
                    txtDigest.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 加载面板中的属性
        /// </summary>
        public void InitProperty()
        {
            switch (ApplyType)
            {
                case "0":
                    Check = JsonConvert.DeserializeObject<CheckJson>(UpdateData.Rows[0]["ApplyContent"].ToString());
                    txtAssay.Text = Check.Assay;
                    txtAssist.Text = Check.Assist;
                    txtDigest.Text = Check.Digest;
                    txtPart.Text = Check.Part;
                    txtSigns.Text = Check.Signs;
                    txtXray.Text = Check.Xray;
                    break;
                case "1":
                    Test = JsonConvert.DeserializeObject<TestJson>(UpdateData.Rows[0]["ApplyContent"].ToString());
                    txtGoal.Text = Test.Goal;
                    List<object> sample = new List<object>(Test.Sample.Split(','));
                    //string[] samplestr = Test.Sample.Split(',');
                    //for (var i = 0; i < samplestr.Length; i++)
                    //{
                    //    sample.Add(samplestr[i]);
                    //}
                    //sample.Add(Test.Sample);
                   multcbSample.SelectValue = sample.ToArray();
                    
                    break;
                case "2":
                    break;
            }
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void ClearData()
        {
            txtAssay.Text = string.Empty;
            txtAssist.Text = string.Empty;
            txtDigest.Text = string.Empty;
            txtPart.Text = string.Empty;
            txtSigns.Text = string.Empty;
            txtXray.Text = string.Empty;
        }

        /// <summary>
        /// 清除送检目的数据
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="type">类型</param>
        public void AppendText(DataRow dr, int type)
        {
            string itemText = txtGoal.Text;
            string itemName = dr["ExamItemName"].ToString();
            if (type == 0)
            {
                if (!string.IsNullOrEmpty(itemText))
                {
                    bool isAdd = true;
                    string[] items = itemText.Split(',');
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] == itemName)
                        {
                            isAdd = false;
                            break;
                        }
                    }

                    if (isAdd)
                    {
                        itemText += "," + itemName;
                    }
                }
                else
                {
                    if (itemText != itemName)
                    {
                        itemText += itemName;
                    }
                }
            }
            else
            {
                if (itemText.Contains(","))
                {
                    string[] items = itemText.Split(',');
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] == itemName)
                        {
                            items = items.Where(item => item != itemName).ToArray();
                            break;
                        }
                    }

                    itemText = string.Join(",", items);
                }
                else
                {
                    if (itemText == itemName)
                    {
                        itemText = string.Empty;
                    }
                }
            }

            txtGoal.Text = itemText;
        }

        /// <summary>
        /// 获取状态名称
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="isreturn">是否退费</param>
        /// <returns>状态名称</returns>
        private string GetStatuName(string status, string isreturn)
        {
            string statuname = string.Empty;
            switch (status)
            {
                case "0":
                    statuname = "申请";
                    break;
                case "1":
                    if (isreturn == "0")
                    {
                        statuname = "收费";
                    }
                    else
                    {
                        statuname = "退费";
                    }

                    break;
                case "2":
                    statuname = "确费";
                    break;
            }

            return statuname;
        }

        /// <summary>
        /// 是否执行
        /// </summary>
        /// <returns>true执行false未执行</returns>
        private bool IsExcute()
        {
            if (ApplyStatu == "0")
            {
                return true;
            }
            else
            {
                if (IsReturns == "1")
                {
                    return true;
                }

                if (ApplyType == "0" && ExamClass == 1)
                {
                    return false;
                }

                if (ApplyType == "1" && ExamClass == 2)
                {
                    return false;
                }

                if (ApplyType == "2" && ExamClass == 4)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 移除网格选项去除项目选中
        /// </summary>
        /// <param name="id">编码</param>
        private void RemoveChecked(string id)
        {
            var execItemLists = GetExamListObj();
            ListViewItem item = execItemLists.Items.Find(id, false).FirstOrDefault();
            if (item != null)
            {
                item.Checked = false;
            }
        }

        /// <summary>
        /// 保存完成后清除数据
        /// </summary>
        private void SaveClearData()
        {
            var dgExams = GetExamTableObj();
            var execItemLists = GetExamListObj();
            dgExams.DataSource = null;
            txtGoal.Text = string.Empty;
            txtDigest.Text = string.Empty;
            txtAssay.Text = string.Empty;
            txtSigns.Text = string.Empty;
            txtAssist.Text = string.Empty;
            txtPart.Text = string.Empty;
            foreach (ListViewItem item in execItemLists.Items)
            {
                item.Checked = false;
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// Form Load事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmMedicalApply_Load(object sender, EventArgs e)
        {
            IsLoad = true;
            UpdateData = null;
            txtGoal.Text = string.Empty;

            //门诊不能编辑住院可以编辑
            if (SystemType == 0)
            {
                dateTimeCheck.Enabled = false;
                datetimeHY.Enabled = false;
                dateTimeZL.Enabled = false;
            }
            else
            {
                dateTimeCheck.Enabled = true;
                datetimeHY.Enabled = true;
                dateTimeZL.Enabled = true;
            }

            dateTimeCheck.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            datetimeHY.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeZL.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!string.IsNullOrEmpty(ApplyType))
            {
                switch (ApplyType)
                {
                    case "0":
                        ClearData();
                        dgExecItemCK.DataSource = null;
                        ExamClass = 1;
                        ApplyControl.SelectedTab = tabItem4;
                        break;
                    case "1":
                        dgExecItem.DataSource = null;
                        ExamClass = 2;
                        ApplyControl.SelectedTab = tabItem3;
                        break;
                    case "2":
                        dgExecItemZL.DataSource = null;
                        ExamClass = 4;
                        ApplyControl.SelectedTab = tabItem5;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(ApplyHeadID))
            {
                if (ApplyType == ApplyControl.SelectedTabIndex.ToString())
                {
                    InvokeController("GetHeadDetail");
                }
            }

            InvokeController("GetExecDept");
            InitdgData();

        }

        /// <summary>
        /// 选择索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbDepts = GetDeptObj();
            if (!IsLoad)
            {
                if (SelectDeptIndex != cbDepts.SelectedIndex)
                {
                    InvokeController("GetExamType", cbDepts.SelectedValue);
                    SelectDeptIndex = cbDepts.SelectedIndex;
                }
            }
            else
            {
                InvokeController("GetExamType", cbDepts.SelectedValue);
                SelectDeptIndex = cbDepts.SelectedIndex;
            }
        }

        /// <summary>
        /// 选择索引事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dgExams = GetExamTableObj();
            var cbTypes = GetTypeObj();
            TempApplyHeadID = ApplyHeadID;

            ApplyHeadID = string.Empty;
            if (IsLoad)
            {
                InvokeController("GetExamItem", cbTypes.SelectedValue);
                if (ExamClass == 2)
                {
                    InvokeController("GetSample");
                }
            }
            else
            {
                if (SelectTypeIndex != cbTypes.SelectedIndex)
                {
                    UpdateData = null;
                    dgExams.DataSource = null;
                    txtGoal.Text = string.Empty;
                    txtDigest.Text = string.Empty;
                    txtAssay.Text = string.Empty;
                    txtSigns.Text = string.Empty;
                    txtAssist.Text = string.Empty;
                    txtPart.Text = string.Empty;
                    InvokeController("GetExamItem", cbTypes.SelectedValue);
                    if (ExamClass == 2)
                    {
                        InvokeController("GetSample");
                    }
                }
            }

            SelectTypeIndex = cbTypes.SelectedIndex == -1 ? 0 : cbTypes.SelectedIndex;
        }

        /// <summary>
        /// 复选框选中事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void ExecItemList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var dgExecItems = GetExamTableObj();
            var cbDepts = GetDeptObj();
            DataTable dt = dgExecItems.DataSource as DataTable;
            if (e.Item.Checked)
            {
                DataRow dr = CurrentItemData.Select("ExamItemID=" + e.Item.Name ).FirstOrDefault();
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("ExamItemID");
                    dt.Columns.Add("ExamItemName");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("ExecuteDeptName");
                    dt.Columns.Add("Amount");
                    dt.Columns.Add("ApplyStatus");
                    dt.Columns.Add("DelFlag");
                }
                else
                {
                    DataRow existdr = dt.Select("ExamItemID=" + e.Item.Name ).FirstOrDefault();
                    if (existdr != null)
                    {
                        MessageBoxEx.Show("该记录已存在");
                        return;
                    }
                }

                DataRow newdr = dt.NewRow();
                if (dr != null)
                {
                    newdr["ExamItemID"] = dr["ExamItemID"];
                    newdr["ExamItemName"] = dr["ExamItemName"];
                    newdr["Price"] = dr["Price"];
                    newdr["Amount"] = "1";
                    newdr["ExecuteDeptName"] = cbDepts.Text;
                    newdr["ApplyStatus"] = "申请";
                    newdr["DelFlag"] = "移除";
                    dt.Rows.Add(newdr);
                    dgExecItems.DataSource = dt;
                    if (ExamClass == 2)
                    {
                        AppendText(dr, 0);
                    }
                }
            }
            else
            {
                if (dt != null)
                {
                    DataRow existdr = dt.Select("ExamItemID=" + e.Item.Name ).FirstOrDefault();
                    if (existdr != null)
                    {
                        AppendText(existdr, 1);
                        dt.Rows.Remove(existdr);
                        dgExecItems.DataSource = dt;
                    }
                }
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsExcute())
            {
                MessageBoxEx.Show("申请单已收费或确费不能修改");
                return;
            }

            var cbDepts = GetDeptObj();
            var cbTypes = GetTypeObj();
            var cbPrints = GetPrintObj();
            var dgExecItems = GetExamTableObj();
            if (cbDepts.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择科室");
                return;
            }

            if (cbTypes.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择项目类型");
                return;
            }

            if (ExamClass == 2)
            {
                
                if (string.IsNullOrEmpty(multcbSample.SelectText))//cbSample.Text
                {
                    MessageBoxEx.Show("请选择检验标本");
                    return;
                }
            }

            switch (ExamClass)
            {
                case 1:
                    if (string.IsNullOrEmpty(dateTimeCheck.Text))
                    {
                        MessageBox.Show("检查时间不能为空!");
                        return;
                    }
                    else
                    {
                        if (DateTime.Compare(Convert.ToDateTime(Convert.ToDateTime(dateTimeCheck.Text).ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString())) < 0)
                        {
                            MessageBox.Show("检查时间不能小于当前时间！");
                            return;
                        }
                    }
                    break;
                case 2:
                    if (string.IsNullOrEmpty(datetimeHY.Text))
                    {
                        MessageBox.Show("检查时间不能为空!");
                        return;
                    }
                    else
                    {
                        if (DateTime.Compare(Convert.ToDateTime(Convert.ToDateTime(datetimeHY.Text).ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString())) < 0)
                        {
                            MessageBox.Show("检查时间不能小于当前时间！");
                            return;
                        }
                    }
                    break;
                case 4:
                    if (string.IsNullOrEmpty(dateTimeZL.Text))
                    {
                        MessageBox.Show("检查时间不能为空!");
                        return;
                    }
                    else
                    {
                        if (DateTime.Compare(Convert.ToDateTime(Convert.ToDateTime(dateTimeZL.Text).ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString())) < 0)
                        {
                            MessageBox.Show("检查时间不能小于当前时间！");
                            return;
                        }
                    }
                    break;

            }

            SaveItemData = dgExecItems.DataSource as DataTable;

            EXA_MedicalApplyHead head = new EXA_MedicalApplyHead();
            string content = string.Empty;
            switch (ExamClass)
            {
                case 1:
                    content = "{\"Digest\":\"" + txtDigest.Text + "\",\"Signs\":\"" + txtSigns.Text + "\",\"Xray\":\"" + txtXray.Text + "\",\"Assay\":\"" + txtAssay.Text + "\",\"Assist\":\"" + txtAssist.Text + "\",\"Part\":\"" + txtPart.Text + "\"}";
                    head.ApplyType = 0;
                    ApplyType = "0";
                    head.CheckDate = Convert.ToDateTime(dateTimeCheck.Text);
                    break;
                case 2:
                    //cbSample.SelectedValue  cbSample.Text
                    content = "{\"Goal\":\"" + txtGoal.Text + "\",\"Sample\":\"" + string.Join(",", multcbSample.SelectValue) + "\",\"SampleName\":\"" + multcbSample.SelectText + "\"}";
                    head.ApplyType = 1;
                    ApplyType = "1";
                    head.CheckDate = Convert.ToDateTime(datetimeHY.Text);
                    break;
                case 4:
                    head.ApplyType = 2;
                    ApplyType = "2";
                    dgExecItemZL.EndEdit();
                    head.CheckDate = Convert.ToDateTime(dateTimeZL.Text);
                    break;
            }

            head.ApplyContent = content;
            head.ApplyDate = DateTime.Now;
            head.ExamTypeID = Convert.ToInt32(cbTypes.SelectedValue);
            head.ExecuteDeptID = Convert.ToInt32(cbDepts.SelectedValue);
            InvokeController("SaveExam", head, UpdateData);
            SaveClearData();
            if (cbPrints.Checked)
            {
                if (UpdateData != null)
                {
                    InvokeController("PrintData", UpdateData);
                }
                else
                {
                    MessageBoxEx.Show("没有可以打印的数据");
                }
            }
        
            ApplyHeadID = string.Empty;
            UpdateData = null;
        }

        /// <summary>
        /// 选中行后事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="selectedValue">事件参数</param>
        private void txtExamItem_AfterSelectedRow(object sender, object selectedValue)
        {
            var execItemLists = GetExamListObj();
            var txtExamItems = GetExecItemObj();
            foreach (ListViewItem item in execItemLists.Items)
            {
                if (item.Name == txtExamItems.MemberValue.ToString())
                {
                    item.ForeColor = Color.White;
                    item.BackColor = Color.Blue;
                    item.Checked = true;
                }
                else
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.White;
                }
            }

            txtExamItems.Text = string.Empty;
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgExecItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgExecItems = GetExamTableObj();
            if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
            {
                return;
            }

            string buttonText = dgExecItems.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (buttonText == "移除")
            {
                if (!IsExcute())
                {
                    MessageBoxEx.Show("申请单已收费或确费不能修改");
                    return;
                }

                DataTable dt = dgExecItems.DataSource as DataTable;
                AppendText(dt.Rows[e.RowIndex], 1);
                RemoveChecked(dt.Rows[e.RowIndex]["ExamItemID"].ToString());
                //dgExecItems.Rows.RemoveAt(e.RowIndex);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmMedicalApply_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplyHeadID = string.Empty;
            TempApplyHeadID = string.Empty;
            UpdateData = null;
            dgExecItem.DataSource = null;
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (UpdateData != null)
            {
                InvokeController("PrintData", UpdateData);
            }
            else
            {
                MessageBoxEx.Show("没有可以打印的数据");
            }
        }

        /// <summary>
        /// 选项卡改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void ApplyControl_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            int tabSelectedIndex = ApplyControl.SelectedTabIndex;
            switch (tabSelectedIndex)
            {
                case 0://检查
                    ClearData();
                    dgExecItemCK.DataSource = null;
                    Check = new CheckJson();
                    ExamClass = 1;
                    break;
                case 1://化验
                    dgExecItem.DataSource = null;
                    Test = new TestJson();
                    ExamClass = 2;
                    break;
                case 2://治疗
                    dgExecItemZL.DataSource = null;
                    ExamClass = 4;
                    break;
            }

            IsLoad = true;
            //UpdateData = null;

            if(string.IsNullOrEmpty(ApplyHeadID))
            {
                ApplyHeadID = TempApplyHeadID;
            }

            if (!string.IsNullOrEmpty(ApplyHeadID))
            {
                if (ApplyType == ApplyControl.SelectedTabIndex.ToString())
                {
                    InvokeController("GetHeadDetail");
                }
            }

            InvokeController("GetExecDept");
            InitdgData();
        }

        /// <summary>
        /// 单元格结束编辑事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgExecItemZL_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                DataTable dt = dgExecItemZL.DataSource as DataTable;
                DataRow dr = dt.Rows[e.RowIndex];
                int amount = 0;
                if (int.TryParse(dr["Amount"].ToString(), out amount))
                {
                }

                if (amount <= 0)
                {
                    MessageBoxEx.Show("请输入正确的数量");
                    dr["Amount"] = "1";
                    dgExecItemZL.EndEdit();
                    return;
                }
            }
        }
        #endregion

        private void dgExecItemCK_DoubleClick(object sender, EventArgs e)
        {
            if (dgExecItemCK.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgExecItemCK.DataSource;
                int curRow = dgExecItemCK.CurrentCell.RowIndex;
                int examitemid =Convert.ToInt32( dt.Rows[curRow]["ExamItemID"]);
                DataTable dtItemDetail = (DataTable)InvokeController("GetExamItemDetail", examitemid);
                FrmExamItemDetail frm = new FrmExamItemDetail(dtItemDetail);
                frm.ShowDialog();
            }
        }

        private void dgExecItem_DoubleClick(object sender, EventArgs e)
        {
            if (dgExecItem.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgExecItem.DataSource;
                int curRow = dgExecItem.CurrentCell.RowIndex;
                int examitemid = Convert.ToInt32(dt.Rows[curRow]["ExamItemID"]);
                DataTable dtItemDetail = (DataTable)InvokeController("GetExamItemDetail", examitemid);
                FrmExamItemDetail frm = new FrmExamItemDetail(dtItemDetail);
                frm.ShowDialog();
            }
        }

        private void dgExecItemZL_DoubleClick(object sender, EventArgs e)
        {
            if (dgExecItemZL.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgExecItemZL.DataSource;
                int curRow = dgExecItemZL.CurrentCell.RowIndex;
                int examitemid = Convert.ToInt32(dt.Rows[curRow]["ExamItemID"]);
                DataTable dtItemDetail = (DataTable)InvokeController("GetExamItemDetail", examitemid);
                FrmExamItemDetail frm = new FrmExamItemDetail(dtItemDetail);
                frm.ShowDialog();
            }
        }

        private void FrmMedicalApply_Shown(object sender, EventArgs e)
        {
          
        }
    }
}