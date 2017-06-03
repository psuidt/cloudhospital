using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资库存
    /// </summary>
    public partial class FrmStoreQuery : BaseFormBusiness, IFrmStoreQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmStoreQuery()
        {
            InitializeComponent();
            fmCommon.AddItem(DeptRoom, "DeptRoom");
            fmCommon.AddItem(txt_Code, "txt_Code");
        }

        #region 函数
        /// <summary>
        /// 绑定科室选择框控件
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        public void BindDeptRoom(DataTable dtDept, int loginDeptID)
        {
            DeptRoom.DataSource = dtDept;
            DeptRoom.ValueMember = "DeptID";
            DeptRoom.DisplayMember = "DeptName";
            DataRow[] rows = dtDept.Select("DeptID=" + loginDeptID);
            if (rows.Length > 0)
            {
                DeptRoom.SelectedValue = loginDeptID;
                return;
            }

            if (dtDept.Rows.Count > 0)
            {
                DeptRoom.SelectedIndex = 0;
            }
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
        /// 绑定库存表格
        /// </summary>
        /// <param name="dt">库存数据</param>
        public void BindStoreGrid(DataTable dt)
        {
            dgStore.DataSource = dt;
        }

        /// <summary>
        /// 绑定批次数据
        /// </summary>
        /// <param name="dt">物资批次</param>
        public void BindStoreBatchGrid(DataTable dt)
        {
            dgBatch.DataSource = dt;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            int deptId = 0;//药库ID
            deptId = Convert.ToInt32(DeptRoom.SelectedValue);
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (txt_Code.Text.Trim() != string.Empty)
            {
                string code = txt_Code.Text.Trim();
                queryCondition.Add(string.Empty, "(b.PYCode like '%" + code + "%' or b.WBCode like '%" + code + "%' or c.PYCode like '%" + code + "%' or c.WBCode like '%" + code + "%' or b.MatName like '%" + code + "%')");
            }

            if (chk_store.Checked)
            {
                queryCondition.Add("a.Amount<", "0");
            }

            queryCondition.Add("a.DeptID", deptId.ToString());
            return queryCondition;
        }

        /// <summary>
        /// 获取库存标识ID
        /// </summary>
        /// <returns>库存标识ID</returns>
        public int GetStorageID()
        {
            int storageID = 0;
            if (dgStore.CurrentCell != null)
            {
                if (dgStore.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgStore.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgStore.DataSource)).Rows[currentIndex];
                    storageID = Convert.ToInt32(currentRow["storageID"]);
                    return storageID;
                }
                else
                {
                    return storageID;
                }
            }
            else
            {
                return storageID;
            }
        }
        #endregion

        #region 事件

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmStoreQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDeptRoomData", frmName);
        }

        /// <summary>
        /// 选择商品加载批次等信息
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgStore_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadMaterialBatch", frmName);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Query_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataTable dtReport = (DataTable)dgStore.DataSource;
            InvokeController("PrintMaterialStore", DeptRoom.Text, dtReport);
        }

        /// <summary>
        /// 变更编码查询数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txt_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Query_Click(null, null);
            }
        }
        #endregion

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
