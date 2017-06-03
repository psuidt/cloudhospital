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
using HIS_DrugManage.Winform.IView.BaseData;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药品拆零
    /// </summary>
    public partial class FrmResolve : BaseFormBusiness, IFrmResolve
    {
        /// <summary>
        /// 药品子类型
        /// </summary>
        private DataTable dtDrugChildType;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmResolve()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 药品类型改变事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbDrugType_TextChanged(object sender, EventArgs e)
        {
            DataTable data = null;
            if (dtDrugChildType != null)
            {
                data = dtDrugChildType;
            }
            //else
            //{
            //    data = tbDrugCType.ShowCardDataSource;
            //}

            if (data != null && tbDrugType.MemberValue != null)
            {
                var cdata = data.Select("TypeID=" + tbDrugType.MemberValue);
                DataTable dtNew = data.Clone();
                for (int i = 0; i < cdata.Length; i++)
                {
                    dtNew.ImportRow(cdata[i]);
                }

                //tbDrugCType.ShowCardDataSource = dtNew;
            }
        }

        /// <summary>
        /// 绑定药品子类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugCType(DataTable dt)
        {
            dtDrugChildType = dt;
            /*tbDrugCType.DisplayField = "CTypeName";
            tbDrugCType.MemberField = "CTypeID";
            tbDrugCType.CardColumn = "CTypeName|药品子类型|auto,PYCode|拼音码|auto";
            tbDrugCType.QueryFieldsString = "CTypeName,PYCode";
            tbDrugCType.ShowCardWidth = 350;
            tbDrugCType.ShowCardDataSource = dt;*/
        }

        /// <summary>
        /// 绑定药品类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugTypeForTb(DataTable dt)
        {
            tbDrugType.DisplayField = "TypeName";
            tbDrugType.MemberField = "TypeID";
            tbDrugType.CardColumn = "TypeName|药品类型|auto,PYCode|拼音码|auto";
            tbDrugType.QueryFieldsString = "TypeName,PYCode";
            tbDrugType.ShowCardWidth = 350;
            tbDrugType.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定药品剂型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDosage(DataTable dt)
        {
            /*
            tbcDosage.DisplayField = "DosageName";
            tbcDosage.MemberField = "DosageID";
            tbcDosage.CardColumn = "DosageName|药品剂型|auto,PYCode|拼音码|auto";
            tbcDosage.QueryFieldsString = "DosageName,PYCode";
            tbcDosage.ShowCardWidth = 350;
            tbcDosage.ShowCardDataSource = dt;*/
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (tbDrugType.SelectedValue != null)
            {
                queryCondition.Add("c.TypeID", tbDrugType.MemberValue.ToString());
            }

            /*if (tbDrugCType.SelectedValue != null)
            {
                queryCondition.Add("c.CTypeID", tbDrugCType.MemberValue.ToString());
            }

            if (tbcDosage.MemberValue != null)
            {
                queryCondition.Add("c.DosageID", tbcDosage.MemberValue.ToString());
            }*/

            if (txt_Code.Text.Trim() != string.Empty)
            {
                string code = txt_Code.Text.Trim();
                queryCondition.Add(string.Empty, "(b.PYCode like '%" + code + "%' or b.WBCode like '%" + code + "%' or c.PYCode like '%" + code + "%' or c.WBCode like '%" + code + "%' or b.DrugID like '%" + code + "%' or c.ChemName LIKE '%" + code + "%' or b.TradeName LIKE '%" + code + "%')");
            }

            if (cmbDept.SelectedValue != null)
            {
                queryCondition.Add("a.DeptID", cmbDept.SelectedValue.ToString());
            }

            if (chk_notResolve.Checked)
            {
                queryCondition.Add("a.ResolveFlag", "0");
            }

            if (chk_isResolve.Checked)
            {
                queryCondition.Add("a.ResolveFlag", "1");
            }

            return queryCondition;
        }

        /// <summary>
        /// 绑定库存表格
        /// </summary>
        /// <param name="dt">库存数据</param>
        public void BindStoreGrid(DataTable dt)
        {
            dgStore.DataSource = dt;
        }

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
            if (dtDrugDept != null)
            {
                cmbDept.DataSource = dtDrugDept;
                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage");
        }

        /// <summary>
        /// 打开网格操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmResolve_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugDept");
            InvokeController("LoadDrugStorage");
            btnBack.Enabled = false;
        }

        /// <summary>
        /// 点击网格操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataTable dt = dgStore.DataSource as DataTable;
                if (Convert.ToInt32(dt.Rows[e.RowIndex]["ck"]) == 0)
                {
                    dt.Rows[e.RowIndex]["ck"] = 1;
                }
                else
                {
                    dt.Rows[e.RowIndex]["ck"] = 0;
                }
            }
        }

        /// <summary>
        /// 设置操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            DataTable dt = dgStore.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ck"]) == 1)
                {
                    ids += dt.Rows[i]["StorageID"] + ",";
                }
            }

            if (!string.IsNullOrEmpty(ids))
            {
                if (ids.Contains(","))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                }

                var result = InvokeController("UpdateStorage", ids);
                if (Convert.ToInt32(result) > 0)
                {
                    MessageBoxEx.Show("拆零完成");
                    InvokeController("LoadDrugStorage");
                }
            }
            else
            {
                MessageBoxEx.Show("没有可拆零的记录");
            }
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 打印操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtReport = (DataTable)dgStore.DataSource;
            InvokeController("PrintDrugStore", dtReport);
        }

        /// <summary>
        /// 取消拆零操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            DataTable dt = dgStore.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ck"]) == 1)
                {
                    ids += dt.Rows[i]["StorageID"] + ",";
                }
            }

            if (!string.IsNullOrEmpty(ids))
            {
                if (ids.Contains(","))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                }

                var result = InvokeController("UpdateBackStorage", ids);
                if (Convert.ToInt32(result) > 0)
                {
                    MessageBoxEx.Show("取消拆零完成");
                    InvokeController("LoadDrugStorage");
                }
            }
            else
            {
                MessageBoxEx.Show("没有可取消拆零的记录");
            }
        }

        /// <summary>
        /// 查询未拆零数据
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chk_notResolve_CheckedChanged(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage");
            btnBack.Enabled = false;
            btnSet.Enabled = true;
        }

        /// <summary>
        /// 查询已拆零数据
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chk_isResolve_CheckValueChanged(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage");
            btnBack.Enabled = true;
            btnSet.Enabled = false;
        }

        /// <summary>
        /// 选中所有事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable msgList = dgStore.DataSource as DataTable;
            if (msgList != null && msgList.Rows.Count > 0)
            {
                for (int i = 0; i < msgList.Rows.Count; i++)
                {
                    msgList.Rows[i]["ck"] = chkAll.Checked ? 1 : 0;
                }
            }
        }
    }
}
