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
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmFinacialItemStatistics : BaseFormBusiness, IFrmFinacialItemStatistics
    {
        /// <summary>
        /// 项目iD
        /// </summary>
        private string itemID;

        /// <summary>
        /// Gets or sets项目id
        /// </summary>
        /// <value>项目Id</value>
        public string ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmFinacialItemStatistics" /> class.
        /// </summary>
        public FrmFinacialItemStatistics()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dtWorker">组织机构信息</param>
        public void SetWork(DataTable dtWorker)
        {
            cmbWork.DisplayMember = "WorkName";
            cmbWork.ValueMember = "WorkId";
            cmbWork.DataSource = dtWorker;
        }

        /// <summary>
        /// 绑定病人类型
        /// </summary>
        public void BindPatType()
        {
            var datasource = new[] 
            {
                new { Text = "门诊", Value = 1 },
                new { Text = "住院", Value = 2 },
                new { Text = "全院", Value = 3 },
            };
            cbbPatType.ValueMember = "Value";
            cbbPatType.DisplayMember = "Text";
            cbbPatType.DataSource = datasource;
            cbbPatType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定世家类型
        /// </summary>
        public void BindTimeType()
        {
            var datasource = new[]
            {
                new { Text = "结算时间", Value = "CostDate" },
                new { Text = "缴款时间", Value = "AccountDate" },
                new { Text = "记账时间", Value = "ChargeDate" },
            };
            cbbTimeType.ValueMember = "Value";
            cbbTimeType.DisplayMember = "Text";
            cbbTimeType.DataSource = datasource;
            cbbTimeType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定费用项目
        /// </summary>
        public void BindFeeItem()
        {
            DataTable dt = (DataTable)InvokeController("QueryItemInfo");

            tbcItem.DisplayField = "ItemName";
            tbcItem.MemberField = "ItemID";
            tbcItem.CardColumn = "ItemCode|项目编码|120,ItemName|项目名称|auto,unitprice|项目单价|80";
            tbcItem.QueryFieldsString = "ItemName,Pym,Wbm";
            tbcItem.ShowCardWidth = 400;
            tbcItem.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFinacialItemStatistics_OpenWindowBefore(object sender, EventArgs e)
        {
            dtTimer.Value = System.DateTime.Now;
            BindTimeType();
            BindPatType();
            BindFeeItem();
            DataTable dt = (DataTable)InvokeController("GetWorker");
            SetWork(dt);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtQuery_Click(object sender, EventArgs e)
        {
            if (txtItem.Text == string.Empty)
            {
                MessageBoxEx.Show("请选择查询项目!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((Convert.ToInt16(cbbPatType.SelectedValue) == 1) && (Convert.ToString(cbbTimeType.SelectedValue) == "ChargeDate"))
            {
                MessageBoxEx.Show("门诊类型按记帐时间统计单项目收入!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int workID = Convert.ToInt32(cmbWork.SelectedValue);
            DateTime queryDate = dtTimer.Value;
            int patType = Convert.ToInt32(cbbPatType.SelectedValue);
            string timeType = Convert.ToString(cbbTimeType.SelectedValue);
            string itemInfo = ItemID;

            try
            {
                GridViewResult.Stop();
                this.Cursor = Cursors.WaitCursor;
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("GetItmeItemStatistics", workID, queryDate, patType, timeType, itemInfo);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("TjTime", "统计时间：" + dtTimer.Value.ToString("yyyy-MM"));
                myDictionary.Add("Printer", "制表人：" + currentUserName);
                myDictionary.Add("PrintTime", "制表时间：" + DateTime.Now.ToString("yyyy-MM-dd"));
                myDictionary.Add("ItemName", txtItem.Text);
                GridReport gridreport = new GridReport();
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 2009, 0, myDictionary, dtReport);
                GridViewResult.Report = gridreport.Report;
                GridViewResult.Start();
                GridViewResult.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }            
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选择行之后
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="selectedValue">事件参数</param>
        private void TbcItem_AfterSelectedRow(object sender, object selectedValue)
        {
            BuildItemIDStr(((DataRow)selectedValue).ItemArray[0].ToString(), ((DataRow)selectedValue).ItemArray[2].ToString());
        }

        /// <summary>
        /// 构建项目ID字符串
        /// </summary>
        /// <param name="id">编码</param>
        /// <param name="name">名称</param>
        private void BuildItemIDStr(string id, string name)
        {
            if (txtItem.Text.IndexOf(name) > -1)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                ItemID = id;
                txtItem.Text = name + "、";
            }
            else
            {
                if ((Regex.Matches(ItemID, ",").Count < 4))
                {
                    ItemID += "," + id;
                    txtItem.Text += name + "、";
                }
                else
                {
                    MessageBoxEx.Show("最多同时查询五个项目!", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ItemID = string.Empty;
            txtItem.Text = string.Empty;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewResult.PostColumnLayout();
                GridViewResult.Report.PrintPreview(true);
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }
    }
}
