using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_ThatFee.Winform.IView;

namespace HIS_ThatFee.Winform.ViewForm
{
    public partial class FrmThatFeeQuery : BaseFormBusiness, IFrmThatFeeQuery
    {
        #region 属性
        /// <summary>
        /// 获取选择的项目ID
        /// </summary>
        public string ItemIDs { get; set; }

        /// <summary>
        /// 获取执行科室ID
        /// </summary>
        public string ConfirDeptID { get; set; }

        /// <summary>
        /// ///获取开始日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 获取结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 0门诊 1住院
        /// </summary>
        public int SystemType { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmThatFeeQuery()
        {
            InitializeComponent();
        }

        #region 接口
        /// <summary>
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        public void BindDept(DataTable dtDept)
        {
            DataRow dr = dtDept.NewRow();
            dr["DeptId"] = 0;
            dr["Name"] = "所有科室";
            dtDept.Rows.InsertAt(dr, 0);
            cmbDept.SelectedIndexChanged -= cmbDept_SelectedIndexChanged;
            cmbDept.DataSource = dtDept;
            cmbDept.SelectedIndexChanged += cmbDept_SelectedIndexChanged;
            if (dtDept != null)
            {
                if (dtDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定项目搜索ShowCard
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        public void BindExecShowCard(DataTable dtItem)
        {
            cbExamItem.DisplayField = "ExamItemName";
            cbExamItem.MemberField = "ExamItemID";
            cbExamItem.CardColumn = "ExamItemID|编码|55,ExamItemName|项目名称|auto";
            cbExamItem.QueryFieldsString = "ExamItemName,PYCode,WBCode";
            cbExamItem.ShowCardWidth = 324;
            cbExamItem.ShowCardDataSource = dtItem;
        }

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        public void GetQueryWhere()
        {
            SystemType = rbOP.Checked ? 0 : 1;
            ConfirDeptID = cmbDept.SelectedIndex > 0 ? cmbDept.SelectedValue.ToString() : string.Empty;
            BeginDate = timeDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            EndDate = timeDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
        }

        /// <summary>
        /// 绑定确费网格信息
        /// </summary>
        /// <param name="dtFee">申请明细数据源</param>
        public void BindThatFee(DataTable dtFee)
        {
            DataRow dr = dtFee.NewRow();
            int count = 0;
            decimal totalFee = 0;
            for (int i = 0; i < dtFee.Rows.Count; i++)
            {
                int currentCount = Tools.ToInt32(dtFee.Rows[i]["Amount"]);
                count += currentCount;
                totalFee += (Tools.ToDecimal(dtFee.Rows[i]["Price"]) * currentCount);
            }

            dr["ExcuteName"] = "合计";
            dr["Amount"] = count;
            dr["Price"] = totalFee.ToString("0.00");
            dtFee.Rows.Add(dr);
            dgThatFee.DataSource = dtFee;
        }
        #endregion

        #region 函数
        /// <summary>
        /// 处理显示选择的项目
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="name">项目名称</param>
        private void BuildItemIDStr(string id, string name)
        {
            if (txtExamItem.Text.IndexOf(name) > -1)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtExamItem.Text))
            {
                ItemIDs = id;
                txtExamItem.Text = name + "、";
            }
            else
            {
                if ((Regex.Matches(ItemIDs, ",").Count < 4))
                {
                    ItemIDs += "," + id;
                    txtExamItem.Text += name + "、";
                }
                else
                {
                    MessageBoxEx.Show("最多同时查询五个项目!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        private void ClearData()
        {
            cbExamItem.Text = string.Empty;
            txtExamItem.Text = string.Empty;
            ItemIDs = string.Empty;
        }
        #endregion

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmThatFeeQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            timeDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            timeDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetDept", frmName);
            InvokeController("GetExamItem", "0");
            InvokeController("GetThatFeeDetail");
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("GetExamItem", cmbDept.SelectedValue.ToString());
            ClearData();
        }

        /// <summary>
        /// 清除点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClean_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        /// <summary>
        /// 检查项目选择行后事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="selectedValue">事件参数</param>
        private void cbExamItem_AfterSelectedRow(object sender, object selectedValue)
        {
            BuildItemIDStr(((DataRow)selectedValue).ItemArray[0].ToString(), ((DataRow)selectedValue).ItemArray[1].ToString());
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetThatFeeDetail");
        }

        /// <summary>
        /// 单选按钮选择事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rbIP_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();
            InvokeController("GetThatFeeDetail");
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
    }
}
