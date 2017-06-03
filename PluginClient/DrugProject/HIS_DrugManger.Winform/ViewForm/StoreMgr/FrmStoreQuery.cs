using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.WinformFrame.Controller;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 库存查询
    /// </summary>
    public partial class FrmStoreQuery : BaseForm, IFrmStoreQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmStoreQuery()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 登录人员
        /// </summary>
        private string empName = string.Empty;

        /// <summary>
        /// 医院名称
        /// </summary>
        private string hospitalName = string.Empty;
        #endregion

        #region 接口
        /// <summary>
        /// 取得登录人姓名
        /// </summary>
        /// <param name="empName">登录人姓名</param>
        /// <param name="hospitalName"> 机构姓名</param>
        public void GetLoginName(string empName, string hospitalName)
        {
            this.empName = empName;
            this.hospitalName = hospitalName;
        }

        /// <summary>
        /// 绑定库房选择框控件
        /// </summary>
        /// <param name="dtDept">库房数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        public void BindStoreRoomCombox(DataTable dtDept, int loginDeptID)
        {
            cmb_StoreRoom.DataSource = dtDept;
            cmb_StoreRoom.ValueMember = "DeptID";
            cmb_StoreRoom.DisplayMember = "DeptName";
            DataRow[] rows = dtDept.Select("DeptID=" + loginDeptID);
            if (rows.Length > 0)
            {
                cmb_StoreRoom.SelectedValue = loginDeptID;
                return;
            }

            if (dtDept.Rows.Count > 0)
            {
                cmb_StoreRoom.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        public void BindTypeCombox(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["TypeID"] = 0;
            dr["TypeName"] = "所有药品类型";
            dt.Rows.InsertAt(dr, 0);
            cmb_Type.DataSource = dt;
            cmb_Type.ValueMember = "TypeID";
            cmb_Type.DisplayMember = "TypeName";
            if (dt.Rows.Count > 0)
            {
                cmb_Type.SelectedIndex = 0;
                int typeId = Convert.ToInt32(cmb_Type.SelectedValue);
                InvokeController("GetDosageDic", frmName, typeId);
            }
        }

        /// <summary>
        /// 绑定剂型选择卡片控件
        /// </summary>
        /// <param name="dt">药品剂型</param>
        public void BindDosageShowCard(DataTable dt)
        {
            txt_Dosage.DisplayField = "DosageName";
            txt_Dosage.MemberField = "DosageID";
            txt_Dosage.CardColumn = "DosageID|编码|55,DosageName|剂型名称|auto";
            txt_Dosage.QueryFieldsString = "DosageName,PYCode,WBCode";
            txt_Dosage.ShowCardWidth = 300;
            txt_Dosage.ShowCardDataSource = dt;
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
        /// <param name="dt">药品【批次</param>
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
            deptId = Convert.ToInt32(cmb_StoreRoom.SelectedValue);
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (cmb_Type.SelectedValue != null)
            {
                if (cmb_Type.SelectedValue.ToString() != "0")
                {
                    queryCondition.Add("c.TypeID", cmb_Type.SelectedValue.ToString());
                }
            }

            if (txt_Dosage.MemberValue != null)
            {
                queryCondition.Add("c.DosageID", txt_Dosage.MemberValue.ToString());
            }

            if (txt_Code.Text.Trim() != string.Empty)
            {
                string code = txt_Code.Text.Trim();
                queryCondition.Add(string.Empty, "(b.PYCode like '%" + code + "%' or b.WBCode like '%" + code + "%' or c.PYCode like '%" + code + "%' or c.WBCode like '%" + code + "%' or b.DrugID like '%" + code + "%' or b.TradeName like '%"+ code + "%' or c.ChemName like '%"+ code + "%')");
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmStoreQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            //加载数据
            InvokeController("GetStoreRoomData", frmName);
            InvokeController("GetDrugTypeDic", frmName);
            InvokeController("GetLoginName");
            if (frmName == "FrmStoreQueryDW")
            {
                dgStore.Columns[8].Visible = false;
                dgStore.Columns[6].Visible = false;
                btnUpdateStore.Visible = false;
                btn_Close.Location = new System.Drawing.Point(1057, 9);
            }
            else
            {
                dgStore.Columns[6].Visible = true;
                btnUpdateStore.Visible = true;
                btn_Close.Location = new System.Drawing.Point(1175, 9);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_Type_DropDownClosed(object sender, EventArgs e)
        {
            int typeId = Convert.ToInt32(cmb_Type.SelectedValue);
            InvokeController("GetDosageDic", frmName, typeId);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgStore_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadDrugBatch", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txt_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btn_Query_Click(null, null);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmStoreQuery_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataTable dtReport = (DataTable)dgStore.DataSource;
            InvokeController("PrintDrugStore", cmb_StoreRoom.Text, dtReport);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        private void dgStore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 && e.RowIndex != -1)
            {
                string DelFlag = dgStore.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                DataRow currentRow = ((DataTable)(dgStore.DataSource)).Rows[e.RowIndex];
                int storageID = Convert.ToInt32(currentRow["storageID"]);
                InvokeController("UpdateFlag", Convert.ToInt32(DelFlag), storageID);

                dgStore.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (DelFlag == "0" ? 1 : 0);
            }
        }

        /// <summary>
        /// 调整有效库存
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnUpdateStore_Click(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("你确定要将有效库存调整和实际库存一致吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dresult == DialogResult.OK)
            {
                //1.存在未发药的不允许修改有效库存,有效库存只能调整和实际库存一直
                if (dgStore.CurrentCell != null)
                {
                    if (dgStore.CurrentCell.RowIndex >= 0)
                    {
                        int currentIndex = dgStore.CurrentCell.RowIndex;
                        DataRow currentRow = ((DataTable)(dgStore.DataSource)).Rows[currentIndex];
                        var DrugID = currentRow["DrugID"];
                        InvokeController("UpdateValidateStore", Convert.ToInt32(DrugID), Convert.ToInt32(cmb_StoreRoom.SelectedValue));

                        InvokeController("LoadDrugStorage", frmName);
                    }
                }
            }
        }
    }
}
