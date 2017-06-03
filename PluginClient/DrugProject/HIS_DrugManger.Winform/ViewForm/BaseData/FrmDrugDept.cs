using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药剂科室设置
    /// </summary>
    public partial class FrmDrugDept : BaseFormBusiness, IFrmDrugDept
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDrugDept()
        {
            InitializeComponent();
        }

        #region 自定义方法
        /// <summary>
        /// 药剂科室实体
        /// </summary>
        private DG_DeptDic currtDeptDic;

        /// <summary>
        /// Gets or sets当前药剂科室实体
        /// </summary>
        /// <value>药剂科室实体</value>
        public DG_DeptDic CurrtDeptDic
        {
            get
            {
                if (txtCard_Dept.Text == string.Empty)
                {
                    currtDeptDic = null;
                }
                else
                {
                    currtDeptDic = new DG_DeptDic();
                    currtDeptDic.DeptName = txtCard_Dept.Text.Trim();
                    currtDeptDic.DeptCode = string.Empty;
                    currtDeptDic.DeptType = GetDeptType();
                    currtDeptDic.StopFlag = 1;
                    currtDeptDic.DeptID = Convert.ToInt32(txtCard_Dept.MemberValue);
                    currtDeptDic.CheckStatus = 0;
                }

                 return currtDeptDic;
            }

            set
            {
                currtDeptDic = value;
            }          
        }

        /// <summary>
        /// 取得科室类型
        /// </summary>
        /// <returns>类型标志</returns>
        private int GetDeptType()
        {
            int deptType = 0;
            if (ckb_ds.Checked)
            {
                //药房
                deptType = 0;
            }

            if (ckb_dw.Checked)
            {
                //药库
                deptType = 1;
            }

            return deptType;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            InvokeController("GetDeptDicData");
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定科室选项卡
        /// </summary>
        /// <param name="dt">科室数据集</param>
        public void BindDep(DataTable dt)
        {
            txtCard_Dept.DisplayField = "Name";
            txtCard_Dept.MemberField = "DeptId";
            txtCard_Dept.CardColumn = "DeptId|编码|100,Name|科室名称|auto";
            txtCard_Dept.QueryFieldsString = "Name,Pym,Wbm";
            txtCard_Dept.ShowCardWidth = 350;
            txtCard_Dept.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定药剂科室Grid
        /// </summary>
        /// <param name="dt">药剂科室</param>
        public void BindDrugDeptGrid(DataTable dt)
        {
            dgDrugDeptList.DataSource = dt;
        }

        /// <summary>
        /// 绑定药剂科室单据Grid
        /// </summary>
        /// <param name="dt">药剂科室单据</param>
        public void BindDrugDeptBillGrid(DataTable dt)
        {
            dgDetail.DataSource = dt;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugDept_OpenWindowBefore(object sender, EventArgs e)
        {
            //加载药剂科室列表数据
            LoadData();
            //加载科室数据
            InvokeController("LoadDeptData");
            //设置默认值
            ckb_ds.Checked = true;
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            int deptId = 0;
            try
            {
                if (txtCard_Dept.MemberValue == null)
                {
                    MessageBox.Show("请选择科室!", "提示");
                    txtCard_Dept.Focus();
                    return;
                }

                deptId = Convert.ToInt32(txtCard_Dept.MemberValue);
                if (Convert.ToBoolean(InvokeController("ExistDrugDept", deptId)))
                {
                    MessageBox.Show("该科室已经是药剂科室，无法添加");
                    return;
                }

                InvokeController("AddDrugDept");//添加药剂科室
                LoadData();//刷新数据
                if (dgDrugDeptList.Rows.Count > 0)
                {
                    dgDrugDeptList.CurrentCell = dgDrugDeptList[0, dgDrugDeptList.Rows.Count - 1];
                }
                else
                {
                    dgDrugDeptList.CurrentCell = dgDrugDeptList[0, 0];
                }

                MessageBox.Show("添加药剂科室成功");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgDrugDeptList.Rows.Count <= 0)
                {
                    MessageBox.Show("列表中没有数据，不能进行删除操作");
                    return;
                }

                DataGridViewRow gridRow = dgDrugDeptList.CurrentRow;
                if (gridRow == null)
                {
                    MessageBox.Show("请选择一条科室数据");
                    return;
                }
               
                if (gridRow.Cells["StopFlag"].Value.ToString() == "0")
                {
                    MessageBox.Show("该科室已经启用，无法删除");
                    return;
                }

                if (MessageBox.Show("您确定要删除该药剂科室么？", "删除确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                int lastRowIndex = this.dgDrugDeptList.CurrentCell.RowIndex;
                int deptDicID = Convert.ToInt32(gridRow.Cells["DeptDicID"].Value);
                InvokeController("DeleteDeptDic", deptDicID);
                LoadData();//刷新数据
                if (lastRowIndex != 0)
                {
                    dgDrugDeptList.CurrentCell = dgDrugDeptList[dgDrugDeptList.CurrentCell.ColumnIndex, lastRowIndex - 1];
                }
                else
                {
                    if (dgDrugDeptList.Rows.Count > 0)
                    {
                        dgDrugDeptList.CurrentCell = dgDrugDeptList[dgDrugDeptList.CurrentCell.ColumnIndex, 0];
                    }
                }

                MessageBox.Show("删除成功");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 启用操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgDrugDeptList.Rows.Count <= 0)
                {
                    MessageBox.Show("列表中没有数据，不能进行启用操作");
                    return;
                }

                DataGridViewRow gridRow = dgDrugDeptList.CurrentRow;
                if (gridRow == null)
                {
                    MessageBox.Show("请选择一条科室数据");
                    return;
                }

                if (gridRow.Cells["StopFlag"].Value.ToString() == "0")
                {
                    MessageBox.Show("该科室已经被启用");
                    return;
                }

                int deptId = Convert.ToInt32(gridRow.Cells["DeptID"].Value);
                int deptType = Convert.ToInt32(gridRow.Cells["DeptType"].Value);
                int rowIndex = gridRow.Index;
                InvokeController("Start", deptId, deptType); 
                LoadData();
                dgDrugDeptList.Rows[rowIndex].Selected = true;
                this.dgDrugDeptList.CurrentCell = this.dgDrugDeptList.Rows[rowIndex].Cells[0];
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 停用操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgDrugDeptList.Rows.Count <= 0)
                {
                    MessageBox.Show("列表中没有数据，不能进行停用操作");
                    return;
                }

                DataGridViewRow gridRow = dgDrugDeptList.CurrentRow;
                if (gridRow == null)
                {
                    MessageBox.Show("请选择一条科室数据");
                    return;
                }

                if (gridRow.Cells["StopFlag"].Value.ToString() == "1")
                {
                    MessageBox.Show("该科室已经被停用");
                    return;
                }

                int deptDicID = Convert.ToInt32(gridRow.Cells["DeptDicID"].Value);
                int deptId = Convert.ToInt32(gridRow.Cells["DeptID"].Value);
                int rowIndex = gridRow.Index;
                InvokeController("StopUseDrugDept", deptDicID, deptId);
                MessageBox.Show("该科室停用成功");
                LoadData();
                dgDrugDeptList.Rows[rowIndex].Selected = true;
                this.dgDrugDeptList.CurrentCell = this.dgDrugDeptList.Rows[rowIndex].Cells[0];
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 变化选中行操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dg_drugDeptList_CurrentCellChanged(object sender, EventArgs e)
        {         
            try
            {
                if (dgDrugDeptList.CurrentCell != null)
                {
                    int index = dgDrugDeptList.CurrentCell.RowIndex;                    
                    if (index >= 0)
                    {                       
                        DataTable dt = (DataTable)dgDrugDeptList.DataSource;
                        int deptId = Convert.ToInt32(dt.Rows[index]["DeptID"]);
                        InvokeController("GetDrugDeptBill", deptId);

                        //设置科室状态
                        txtUseState.Text = dt.Rows[index]["StopFlagName"].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 设置行颜色
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dg_drugDeptList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int stopFlag = Convert.ToInt32(dgDrugDeptList.Rows[e.RowIndex].Cells["StopFlag"].Value);
            if (stopFlag == 1)
            {
                dgDrugDeptList.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
