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
    public partial class FrmDispRpt : BaseFormBusiness, IFrmDispRpt
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDispRpt()
        {
            InitializeComponent();
            frmCommon.AddItem(DeptRoom, "DeptRoom");
            frmCommon.AddItem(cmb_Type, "cmb_Type");
            frmCommon.AddItem(cmb_CType, "cmb_CType");
            frmCommon.AddItem(PostType, "PostType");
            frmCommon.AddItem(btnQuery, "btnQuery");
        }

        /// <summary>
        /// 当前行对象
        /// </summary>
        public DataRow CurrentHead { get; set; }

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
        /// 绑定发药类型
        /// </summary>
        public void BindPostType()
        {
            var datasource = new[]
            {
                new { Text = "住院", Value = "0" },
                new { Text = "门诊", Value = "1" },
            };

            PostType.DataSource = datasource;
            PostType.ValueMember = "Value";
            PostType.DisplayMember = "Text";
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
            }
        }

        /// <summary>
        /// 绑定药品子类型下拉框控件
        /// </summary>
        /// <param name="dt">药品子类型数据源</param>
        public void BindChildDrugType(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["CTypeID"] = 0;
            dr["CTypeName"] = "所有药品子类型";
            dt.Rows.InsertAt(dr, 0);
            cmb_CType.DataSource = dt;
            cmb_CType.ValueMember = "CTypeID";
            cmb_CType.DisplayMember = "CTypeName";
            if (dt.Rows.Count > 0)
            {
                cmb_CType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品网格数据
        /// </summary>
        /// <param name="dt">药品数据源</param>
        public void BindDGDrug(DataTable dt)
        {
            dgDrug.DataSource = dt;
            if (dt.Rows.Count <= 0)
            {
                DataTable dtSellDetail = dgDept.DataSource as DataTable;
                if (dtSellDetail != null)
                {
                    dtSellDetail.Rows.Clear();
                    dgDept.DataSource = dtSellDetail;
                }
            }
        }

        /// <summary>
        /// 绑定科室网格数据
        /// </summary>
        /// <param name="dt">科室数据源</param>
        public void BindDGDept(DataTable dt)
        {
            dgDept.DataSource = dt;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void panelEx2_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 第一次打开页面事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOpDisDrugStatistics_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDeptRoomData", frmName);
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            cmb_Type.SelectedIndexChanged -= cmb_Type_SelectedIndexChanged;
            InvokeController("GetDrugTypeDic", frmName);
            cmb_Type.SelectedIndexChanged += cmb_Type_SelectedIndexChanged;
            InvokeController("GetChildDrugType", cmb_Type.SelectedValue.ToString(), frmName);
            BindPostType();
            PostType.SelectedIndex = 0;
        }

        /// <summary>
        /// 改变选中事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("GetChildDrugType", cmb_Type.SelectedValue.ToString(), frmName);
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetDispTotal", PostType.SelectedValue.ToString());
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "a.DispTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            if (DeptRoom.SelectedValue != null)
            {
                if (PostType.SelectedValue.ToString() == "0")
                {
                    queryCondition.Add("a.ExecDeptID", DeptRoom.SelectedValue.ToString());
                }
                else
                {
                    queryCondition.Add("a.DeptID", DeptRoom.SelectedValue.ToString());
                }
            }

            if (cmb_CType.SelectedValue != null)
            {
                if (cmb_CType.SelectedValue.ToString() != "0")
                {
                    queryCondition.Add("b.CTypeID", cmb_CType.SelectedValue.ToString());
                }
            }

            return queryCondition;
        }

        /// <summary>
        /// 网格改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrug_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDrug.CurrentRow == null)
            {
                return;
            }

            int currentIndex = dgDrug.CurrentCell.RowIndex;
            DataRow currentRow = ((DataTable)(dgDrug.DataSource)).Rows[currentIndex];
            CurrentHead = currentRow;
            TotalCount.Text = currentRow["UseAmount"].ToString();
            InvokeController("GetDGDept", PostType.SelectedValue.ToString());
        }
    }
}
