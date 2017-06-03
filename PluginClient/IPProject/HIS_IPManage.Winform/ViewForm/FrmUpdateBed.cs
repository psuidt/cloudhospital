using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmUpdateBed : BaseFormBusiness, IUpdateBed
    {
        #region "属性"
        /// <summary>
        /// 新增费用按钮
        /// </summary>
        ButtonItem btnAdd = new ButtonItem();

        /// <summary>
        /// 删除费用按钮
        /// </summary>
        ButtonItem btnDel = new ButtonItem();

        /// <summary>
        /// 网格状态
        /// </summary>
        public bool SetGridState
        {
            set
            {
                bool b = value;
                if (b == false)
                {
                    grdBedFreeList.ReadOnly = false;
                    grdBedFreeList.Columns[2].ReadOnly = true;
                }
                else
                {
                    grdBedFreeList.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 床位对象
        /// </summary>
        private IP_BedInfo bedInfo = new IP_BedInfo();

        /// <summary>
        /// 床位对象
        /// </summary>
        public IP_BedInfo IPBedInfo
        {
            get
            {
                frmBed.GetValue<IP_BedInfo>(bedInfo);
                bedInfo.IsPlus = chkIsPlus.Checked ? 1 : 0;
                return bedInfo;
            }

            set
            {
                bedInfo = value;
                if (bedInfo.IsPlus == 1)
                {
                    chkIsPlus.Checked = true;
                }
                else
                {
                    chkIsPlus.Checked = false;
                }

                frmBed.Load(bedInfo);
            }
        }

        /// <summary>
        /// 新增床位标识
        /// </summary>
        private bool mIsAddBed;

        /// <summary>
        /// 新增床位标识
        /// </summary>
        public bool IsAddBed
        {
            get
            {
                return mIsAddBed;
            }

            set
            {
                mIsAddBed = value;
            }
        }

        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmUpdateBed()
        {
            InitializeComponent();
            frmBed.AddItem(txtWardNo, "RoomNO"); // 房间号
            frmBed.AddItem(txtBedNo, "BedNO"); // 床位号
            frmBed.AddItem(txtDoctorID, "BedDoctorID"); // 责任医生
            frmBed.AddItem(txtNurseID, "BedNurseID"); // 责任护士
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmUpdateBed_Load(object sender, EventArgs e)
        {
            //txtWardNo.Focus();
        }

        /// <summary>
        /// 注册功能键
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmUpdateBed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnAdd.Focused)
            {
                btnAdd_Click(sender, e);
            }
        }

        /// <summary>
        /// 新增床位费用
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 更改网格为可编辑状态
            SetGridState = false;
            // 给网格追加新数据行
            grdBedFreeList.AddRow();
        }

        /// <summary>
        /// 删除床位费用
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdBedFreeList.CurrentCell != null)
            {
                // 删除网格选中行
                int rowid = this.grdBedFreeList.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdBedFreeList.DataSource;
                dt.Rows.RemoveAt(rowid);
            }
        }

        /// <summary>
        /// 将床位以及床位费信息保存到数据库中
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnBedSave_Click(object sender, EventArgs e)
        {
            if (CheckControlValue())
            {
                grdBedFreeList.EndEdit();
                DataTable bedFreeDt = (DataTable)grdBedFreeList.DataSource;
                InvokeController("SaveBedInfo", bedFreeDt);
            }
        }

        /// <summary>
        /// 绑定弹出网格选中数据到父网格的DataTable中
        /// </summary>
        /// <param name="selectedValue">选中行的数据</param>
        /// <param name="stop">终止标志</param>
        /// <param name="customNextColumnIndex">绑定数据后光标聚焦的位置</param>
        private void grdBedFreeList_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int colid = this.grdBedFreeList.CurrentCell.ColumnIndex;
                int rowid = this.grdBedFreeList.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdBedFreeList.DataSource;

                dt.Rows[rowid]["ItemID"] = row["ItemID"];
                dt.Rows[rowid]["ItemName"] = row["ItemName"];
                dt.Rows[rowid]["ItemUnit"] = row["UnPickUnit"];
                dt.Rows[rowid]["UnitPrice"] = row["UnitPrice"];
                dt.Rows[rowid]["FeeClass"] = 3;
                dt.Rows[rowid]["ItemAmount"] = 0;

                grdBedFreeList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 关闭窗体时，修改网格为不可编辑状态
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmUpdateBed_VisibleChanged(object sender, EventArgs e)
        {
            btnAdd.Dispose();
            btnDel.Dispose();
            btnAdd = new ButtonItem();
            btnDel = new ButtonItem();
            // 新增费用
            btnAdd.Name = "btnAdd";
            btnAdd.Text = "新增费用(&N)";
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 删除费用
            btnDel.Name = "btnDel";
            btnDel.Text = "删除费用(&D)";
            btnDel.Click += new System.EventHandler(this.btnDel_Click);

            if (bar1.Items.Count > 0)
            {
                bar1.Items.Clear();
            }

            bar1.Items.AddRange(new BaseItem[] 
            {
            btnAdd,
            btnDel
            });

            if (mIsAddBed)
            {
                txtWardNo.Focus();
            }
            else
            {
                btnAdd.Focus();
            }
            
            grdBedFreeList.EndEdit();
        }

        /// <summary>
        /// 床位费用选中
        /// </summary>
        /// <param name="sender">grdBedFreeList</param>
        /// <param name="e">事件参数</param>
        private void grdBedFreeList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (grdBedFreeList.CurrentCell != null)
            {
                if (e.ColumnIndex == 1)
                {
                    int rowIndex = grdBedFreeList.CurrentCell.RowIndex;
                    DataTable dt = grdBedFreeList.DataSource as DataTable;
                }
            }
        }

        /// <summary>
        /// 界面打开病房号获得焦点
        /// </summary>
        /// <param name="sender">FrmUpdateBed</param>
        /// <param name="e">事件参数</param>
        private void FrmUpdateBed_Shown(object sender, EventArgs e)
        {
            txtWardNo.Focus();
        }

        /// <summary>
        /// 选中床位护士，新增床位费用按钮获得焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="selectedValue">选中的数据</param>
        private void txtNurseID_AfterSelectedRow(object sender, object selectedValue)
        {
            if (selectedValue != null)
            {
                btnAdd_Click(null, null);
                btnAdd.Focus();
            }
            else
            {
                txtNurseID.MemberValue = null;
                txtNurseID.Name = string.Empty;
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void FormClose()
        {
            grdBedFreeList.EndEdit();
            SetGridState = true;
            this.Close();
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            grdBedFreeList.EndEdit();
            SetGridState = true;
            this.Close();
        }

        #endregion

        #region "数据绑定"
        /// <summary>
        /// 网格中弹出网格数据绑定
        /// </summary>
        /// <param name="feeItemDataDt">床位费弹出网格数据</param>
        public void Bind_FeeItemData(DataTable feeItemDataDt)
        {
            grdBedFreeList.SelectionCards[0].BindColumnIndex = 0; // 绑定数据的列
            grdBedFreeList.SelectionCards[0].CardColumn = "ItemCode|编码|100,ItemName|项目名称|auto,UnitPrice|单价|80";  // 弹出网格列头
            grdBedFreeList.SelectionCards[0].CardSize = new Size(350, 260);  // 弹出网格高度、宽度
            grdBedFreeList.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm"; // 检索字段
            grdBedFreeList.BindSelectionCardDataSource(0, feeItemDataDt); // 绑定弹出网格数据
        }

        /// <summary>
        /// 绑定床位费用列表
        /// </summary>
        /// <param name="bedFreeDt">床位费用列表</param>
        /// <param name="isAdd">是否为新增后重新加载</param>
        public void Bind_BedFreeList(DataTable bedFreeDt, bool isAdd)
        {
            grdBedFreeList.DataSource = bedFreeDt;
            if (isAdd)
            {
                SetGridState = false;
                txtBedNo.ReadOnly = true;
                txtWardNo.ReadOnly = true;
            }
            else
            {
                SetGridState = true;
                txtBedNo.ReadOnly = false;
                txtWardNo.ReadOnly = false;
            }
        }

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        public void Bind_txtCurrDoctor(DataTable doctorDt)
        {
            txtDoctorID.MemberField = "EmpId";
            txtDoctorID.DisplayField = "Name";
            txtDoctorID.CardColumn = "Name|名称|auto";
            txtDoctorID.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDoctorID.ShowCardWidth = 350;
            txtDoctorID.ShowCardDataSource = doctorDt;
        }

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="currNurseDt">护士列表</param>
        public void Bind_txtCurrNurse(DataTable currNurseDt)
        {
            txtNurseID.MemberField = "EmpId";
            txtNurseID.DisplayField = "Name";
            txtNurseID.CardColumn = "Name|名称|auto";
            txtNurseID.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtNurseID.ShowCardWidth = 350;
            txtNurseID.ShowCardDataSource = currNurseDt;
        }

        /// <summary>
        /// 验证病床录入信息
        /// </summary>
        /// <returns>true：验证通过/false：验证不通过</returns>
        private bool CheckControlValue()
        {
            if (string.IsNullOrEmpty(txtWardNo.Text.Trim()))
            {
                InvokeController("MessageShow", "病房号不能为空，请输入病房号！");
                return false;
            }

            if (string.IsNullOrEmpty(txtBedNo.Text.Trim()))
            {
                InvokeController("MessageShow", "床位号不能为空，请输入床位号！");
                return false;
            }   
                   
            return true;
        }
        #endregion
    }
}