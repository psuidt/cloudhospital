using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.Report;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药品有效期预警
    /// </summary>
    public partial class FrmValidAlarm : BaseFormBusiness, IFrmValidAlarm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmValidAlarm()
        {
            InitializeComponent();
        }

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmValidAlarm_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugTypeDic", frmName);
            if (frmName == "FrmValidAlarmYF")
            {
                dataGrid.Columns["NewAmount"].Visible = true;
                dataGrid.Columns["PackAmount"].Visible = false;
                dataGrid.Columns["UnitName"].Visible = false;
            }
            else
            {
                dataGrid.Columns["NewAmount"].Visible = false;
                dataGrid.Columns["PackAmount"].Visible = true;
                dataGrid.Columns["UnitName"].Visible = true;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDay.Text))
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                if (reg.IsMatch(textDay.Text))
                {
                    InvokeController("LoadDrugStorage", frmName);
                }
                else
                {
                    MessageBoxEx.Show("天数必须是正整数");
                }
            }
            else
            {
                InvokeController("LoadDrugStorage", frmName);
            }
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
        #endregion

        #region 函数
        /// <summary>
        /// 绑定药品类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        public void BindTypeCombox(DataTable dt)
        {
            cmb_Type.DataSource = dt;
            cmb_Type.ValueMember = "TypeID";
            cmb_Type.DisplayMember = "TypeName";
            if (dt.Rows.Count > 0)
            {
                cmb_Type.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (cmb_Type.SelectedValue != null)
            {
                queryCondition.Add("c.TypeID", cmb_Type.SelectedValue.ToString());
            }

            if (!string.IsNullOrEmpty(textDay.Text))
            {
                DateTime newTime = DateTime.Now.AddDays(Convert.ToDouble(textDay.Text));
                queryCondition.Add("f.ValidityTime<", "'" + newTime.ToString() + "'");
            }

            return queryCondition;
        }

        /// <summary>
        /// 绑定库存表格
        /// </summary>
        /// <param name="dt">库存数据</param>
        public void BindStoreGrid(DataTable dt)
        {
            dataGrid.DataSource = dt;
        }
        #endregion
    }
}
