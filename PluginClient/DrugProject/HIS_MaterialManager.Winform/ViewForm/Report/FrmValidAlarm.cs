using System;
using System.Collections.Generic;
using System.Data;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.Report;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资库存报警
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

        /// <summary>
        /// 界面打开前加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmValidAlarm_OpenWindowBefore(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDay.Text))
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                if (reg.IsMatch(textDay.Text))
                {
                    if (txtType.Tag != null)
                    {
                        InvokeController("LoadMaterialStorage", frmName, txtType.Tag.ToString());
                    }
                    else
                    {
                        InvokeController("LoadMaterialStorage", frmName, string.Empty);
                    }
                }
                else
                {
                    MessageBoxEx.Show("天数必须是正整数");
                }
            }
            else
            {
                if (txtType.Tag != null)
                {
                    InvokeController("LoadMaterialStorage", frmName, txtType.Tag.ToString());
                }
                else
                {
                    InvokeController("LoadMaterialStorage", frmName, string.Empty);
                }
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        public void BindMaterialTypeTextBox(int typeId, string typeName)
        {
            txtType.Text = typeName;
            txtType.Tag = typeId;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
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

        /// <summary>
        /// 选择物资类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnShowTypeTree_Click(object sender, EventArgs e)
        {
            InvokeController("OpenMaterialTypeDialog", frmName);
        }
    }
}
