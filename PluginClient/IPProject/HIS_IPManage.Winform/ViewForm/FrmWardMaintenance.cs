using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmWardMaintenance : BaseFormBusiness, IWardMaintenance
    {
        #region "属性"

        /// <summary>
        /// 病区ID
        /// </summary>
        public int WardID
        {
            get
            {
                return string.IsNullOrEmpty(txtInpatientAreaList.MemberValue.ToString()) ?
                     -1 : int.Parse(txtInpatientAreaList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 床位信息
        /// </summary>
        private IP_BedInfo ipBedInfo = new IP_BedInfo();

        /// <summary>
        /// 床位信息
        /// </summary>
        public IP_BedInfo IPBedInfo
        {
            get
            {
                if (grdBedList.CurrentCell != null)
                {
                    int rowIndex = grdBedList.CurrentCell.RowIndex;
                    DataTable dt = grdBedList.DataSource as DataTable;
                    ipBedInfo.BedID = Convert.ToInt32(dt.Rows[rowIndex]["BedID"].ToString()); // 床位ID
                    ipBedInfo.WardID = Convert.ToInt32(dt.Rows[rowIndex]["WardID"].ToString());// 病区ID
                    ipBedInfo.RoomNO = dt.Rows[rowIndex]["RoomNO"].ToString();// 房间号
                    ipBedInfo.BedNO = dt.Rows[rowIndex]["BedNO"].ToString();// 床号
                    ipBedInfo.BedDoctorID = Convert.ToInt32(dt.Rows[rowIndex]["BedDoctorID"].ToString());// 主治医生
                    ipBedInfo.BedNurseID = Convert.ToInt32(dt.Rows[rowIndex]["BedNurseID"].ToString());// 责任护士
                    ipBedInfo.IsPlus = Convert.ToInt32(dt.Rows[rowIndex]["IsPlus"].ToString());// 是否加床
                    ipBedInfo.PatListID = Convert.ToInt32(dt.Rows[rowIndex]["PatListID"].ToString());// 病人ID
                    ipBedInfo.PatName = dt.Rows[rowIndex]["PatName"].ToString();// 病人名
                    ipBedInfo.PatSex = dt.Rows[rowIndex]["PatSex"].ToString();// 病人性别
                    ipBedInfo.PatDeptID = Convert.ToInt32(dt.Rows[rowIndex]["PatDeptID"].ToString());// 病人科室ID
                    ipBedInfo.PatDoctorID = Convert.ToInt32(dt.Rows[rowIndex]["PatDoctorID"].ToString());// 病人责任医生
                    ipBedInfo.PatNurseID = Convert.ToInt32(dt.Rows[rowIndex]["PatNurseID"].ToString());// 病人责任护士
                    ipBedInfo.BabyID = Convert.ToInt32(dt.Rows[rowIndex]["BabyID"].ToString());// 婴儿ID
                    ipBedInfo.IsUsed = Convert.ToInt32(dt.Rows[rowIndex]["IsUsed"].ToString());// 是否再用
                    ipBedInfo.IsStoped = Convert.ToInt32(dt.Rows[rowIndex]["IsStoped"].ToString());// 是否停用
                    ipBedInfo.IsPack = Convert.ToInt32(dt.Rows[rowIndex]["IsPack"].ToString());// 是否停用
                }

                return ipBedInfo;
            }
        }
        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmWardMaintenance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开之前的加载事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmWardMaintenance_OpenWindowBefore(object sender, EventArgs e)
        {
            // 取得病区列表
            InvokeController("GetWardDept");
        }

        /// <summary>
        /// 注册功能键
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmWardMaintenance_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnAddHospitalBed_Click(sender, e);  // 增加床位
                    break;
                case Keys.F7:
                    btnUpdHospitalBed_Click(sender, e); // 修改床位
                    break;
                case Keys.F8:
                    btnStopHospitalBed_Click(sender, e); // 停用或启用床位
                    break;
                case Keys.F9:
                    btnBatchAddBed_Click(sender, e); // 批量增加床位
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 新增床位
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAddHospitalBed_Click(object sender, EventArgs e)
        {
            // 没有选择病区或选择全部病区时不能添加床位提示Msg
            if (string.IsNullOrEmpty(txtInpatientAreaList.Text.Trim()) ||
                Convert.ToInt32(txtInpatientAreaList.MemberValue) == -1)
            {
                InvokeController("MessageShow", "请选择要添加病床的病区！");
                return;
            }

            InvokeController("ShowUpdateBed", false);
        }

        /// <summary>
        /// 修改选中的病床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnUpdHospitalBed_Click(object sender, EventArgs e)
        {
            // 没有选择病区或选择全部病区时不能修改床位提示Msg
            if (string.IsNullOrEmpty(txtInpatientAreaList.Text.Trim()) ||
                Convert.ToInt32(txtInpatientAreaList.MemberValue) == -1)
            {
                InvokeController("MessageShow", "请选择要修改病床的病区！");
                return;
            }

            if (grdBedList.CurrentCell != null)
            {
                InvokeController("ShowUpdateBed", true);
            }
        }

        /// <summary>
        /// 停用或启用病床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnStopHospitalBed_Click(object sender, EventArgs e)
        {
            if (grdBedList.CurrentCell == null)
            {
                InvokeController("MessageShow", "请选择床位！");
                return;
            }

            int rowIndex = grdBedList.CurrentCell.RowIndex;
            DataTable dt = grdBedList.DataSource as DataTable;
            int status = Convert.ToInt32(dt.Rows[rowIndex]["IsStoped"].ToString()) == 0 ? 1 : 0;
            InvokeController("StoppedOrEnabledBed", status, Convert.ToInt32(dt.Rows[rowIndex]["BedID"]), dt.Rows[rowIndex]["BedNo"].ToString());
        }

        /// <summary>
        /// 批量增加床位
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnBatchAddBed_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭床位维护界面
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 根据病区ID查询床位列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtInpatientAreaList_TextChanged(object sender, EventArgs e)
        {
            if (txtInpatientAreaList.MemberValue != null)
            {
                if (!string.IsNullOrEmpty(txtInpatientAreaList.Text.Trim()))
                {
                    InvokeController("GetBedList", int.Parse(txtInpatientAreaList.MemberValue.ToString()));
                    grdBedFreeList.DataSource = new DataTable();
                }
            }
        }

        /// <summary>
        /// 加载床位费用列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdBedList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdBedList.CurrentCell != null)
            {
                int rowIndex = grdBedList.CurrentCell.RowIndex;
                DataTable dt = grdBedList.DataSource as DataTable;
                if (dt.Rows[rowIndex]["IsStopedName"].ToString().Equals("停用"))
                {
                    btnStopHospitalBed.Text = "启用病床(F8)";
                }
                else
                {
                    btnStopHospitalBed.Text = "停用病床(F8)";
                }

                InvokeController("GetBedFreeList", Convert.ToInt32(dt.Rows[rowIndex]["BedID"]));
            }
        }

        #endregion

        #region "私有方法"

        /// <summary>
        /// 绑定病区列表
        /// </summary>
        /// <param name="wardDt">病区列表</param>
        public void Bind_WardDeptList(DataTable wardDt)
        {
            txtInpatientAreaList.MemberField = "DeptId";
            txtInpatientAreaList.DisplayField = "Name";
            txtInpatientAreaList.CardColumn = "Name|名称|auto";
            txtInpatientAreaList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtInpatientAreaList.ShowCardWidth = 350;
            txtInpatientAreaList.ShowCardDataSource = wardDt;
            txtInpatientAreaList.MemberValue = wardDt.Rows[0]["DeptId"];
            txtInpatientAreaList.MemberValue = -1;
        }

        /// <summary>
        /// 绑定床位列表
        /// </summary>
        /// <param name="bedDt">床位列表</param>
        public void Bind_BedList(DataTable bedDt)
        {
            if (bedDt != null && bedDt.Rows.Count > 0)
            {
                if (bedDt.Rows[0]["IsStopedName"].ToString().Equals("停用"))
                {
                    btnStopHospitalBed.Text = "启用病床(F8)";
                }
                else
                {
                    btnStopHospitalBed.Text = "停用病床(F8)";
                }
            }

            grdBedList.DataSource = bedDt;
            grdBedFreeList.DataSource = new DataTable();
        }

        /// <summary>
        /// 绑定床位费用列表
        /// </summary>
        /// <param name="bedFreeDt">床位费用列表</param>
        public void Bind_BedFreeList(DataTable bedFreeDt)
        {
            grdBedFreeList.DataSource = bedFreeDt;
        }

        /// <summary>
        /// 数据未加载完后设置界面控件可用
        /// </summary>
        public void SetControlEnabled()
        {
            btnAddHospitalBed.Enabled = true;
            btnUpdHospitalBed.Enabled = true;
            btnStopHospitalBed.Enabled = true;
        }

        #endregion
    }
}