using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 库位管理
    /// </summary>
    public partial class FrmLocation : BaseFormBusiness, IFrmLocation
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmLocation()
        {
            InitializeComponent();
            frmLocations.AddItem(DeptRoom, "DeptRoom");
            frmLocations.AddItem(cmb_Type, "cmb_Type");
            frmLocations.AddItem(txt_Dosage, "txt_Dosage");
            frmLocations.AddItem(txt_Code, "txt_Code");
            frmLocations.AddItem(chkNotLocation, "chkNotLocation");
            frmLocations.AddItem(chkIsLocation, "chkIsLocation");
            frmLocations.AddItem(chk_Store, "chk_Store");
        }

        #region 属性
        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 库位ID
        /// </summary>
        public int LocationID { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        #endregion

        #region 事件
        /// <summary>
        /// 添加库位信息
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void toolMenuAdd_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            LocationID = 0;
            ParentId = 0;
            Level = 0;
            if (this.LocationTree.SelectedNode != null)
            {
                ParentId = Convert.ToInt32(this.LocationTree.SelectedNode.Name);
                if (this.LocationTree.SelectedNode.Parent != null)
                {
                    Level = 1;
                }
            }

            if (Level == 0)
            {
                InvokeController("ShowDialog", "FrmLocationInfo");
                InvokeController("GetLocationList", Convert.ToInt32(DeptRoom.SelectedValue));
            }
            else
            {
                MessageBoxEx.Show("二级节点不能添加子节点");
            }
        }

        /// <summary>
        /// 修改库位信息
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void toolMenuEdit_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            LocationID = 0;
            ParentId = 0;
            int locationid = 0;
            int parentid = 0;
            if (this.LocationTree.SelectedNode != null)
            {
                if (int.TryParse(this.LocationTree.SelectedNode.Name, out locationid))
                {
                }

                LocationID = locationid;
                if (this.LocationTree.SelectedNode.Parent != null)
                {
                    if (int.TryParse(this.LocationTree.SelectedNode.Parent.Name, out parentid))
                    {
                    }

                    ParentId = parentid;
                }

                InvokeController("ChangeView", frmName);
                InvokeController("ShowDialog", "FrmLocationInfo");
                InvokeController("GetLocationList", Convert.ToInt32(DeptRoom.SelectedValue));
                this.LocationTree.SelectedNode = this.LocationTree.Nodes.Find(locationid.ToString(), true).FirstOrDefault();
            }
            else
            {
                MessageBoxEx.Show("请先选择一个节点");
            }
        }

        /// <summary>
        /// 删除库位信息
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void toolMenuDel_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            if (MessageBoxEx.Show("删除后无法恢复，确定删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.LocationTree.SelectedNode != null)
                {
                    string locaionid = this.LocationTree.SelectedNode.Name;
                    DG_Location location = new DG_Location();
                    location.LocationID = Convert.ToInt32(locaionid);
                    InvokeController("DelLoacation", location);
                    this.LocationTree.SelectedNode.Remove();
                }
                else
                {
                    MessageBoxEx.Show("请先选择一个节点");
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmLocation_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            int deptid = 0;
            DeptId = 0;
            InvokeController("GetDeptRoomData", frmName);
            if (DeptRoom.SelectedValue != null)
            {
                if (int.TryParse(DeptRoom.SelectedValue.ToString(), out deptid))
                {
                }

                DeptId = deptid;
                InvokeController("GetLocationList", deptid);
            }

            InvokeController("GetDrugTypeDic", frmName);
            InvokeController("LoadDrugStorage", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void DeptRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            int deptid = 0;
            if (DeptRoom.SelectedValue != null)
            {
                if (int.TryParse(DeptRoom.SelectedValue.ToString(), out deptid))
                {
                }

                DeptId = deptid;
                InvokeController("GetLocationList", deptid);
                InvokeController("LoadDrugStorage", frmName);
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
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void LocationTree_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (LocationTree.SelectedNode != null)
            {
                if (LocationTree.SelectedNode.Level > 0)
                {
                    int locationid = Convert.ToInt32(LocationTree.SelectedNode.Name);
                    string ids = string.Empty;
                    DataTable dt = dgStore.DataSource as DataTable;
                    if (dt != null)
                    {
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

                            var result = InvokeController("UpdateStorage", locationid, ids, frmName);
                            if (Convert.ToInt32(result) > 0)
                            {
                                MessageBoxEx.Show("归库完成");
                                InvokeController("LoadDrugStorage", frmName);
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("没有可归库的记录");
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("没有可归库的记录");
                    }
                }
                else
                {
                    MessageBoxEx.Show("父节点不能进行该操作");
                }
            }
            else
            {
                MessageBoxEx.Show("请选择一个库房");
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgStore.DataSource as DataTable;
            int ischeck = 0;
            if (chkall.Checked)
            {
                ischeck = 1;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ck"] = ischeck;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmLocation_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    {
                        btnSet_Click(null, null);
                    }

                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkIsLocation_CheckedChanged(object sender, EventArgs e)
        {
            InvokeController("LoadDrugStorage", frmName);
        }
        #endregion

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
        /// 加载库位节点信息
        /// </summary>
        /// <param name="locationlist">库位节点信息</param>
        public void GetLocationList(List<DG_Location> locationlist)
        {
            LocationTree.Nodes.Clear();
            List<DG_Location> parentlist = locationlist.Where(item => item.ParentID == 0).ToList();
            foreach (DG_Location tempbd in parentlist)
            {
                Node newnode = new Node();
                newnode.Text = tempbd.LocationName;
                newnode.Name = tempbd.LocationID.ToString();
                LocationTree.Nodes.Add(newnode);
            }

            List<DG_Location> childlist = locationlist.Where(item => item.ParentID > 0).ToList();
            foreach (DG_Location tempbd in childlist)
            {
                Node newnode = LocationTree.Nodes.Find(tempbd.ParentID.ToString(), true).FirstOrDefault();
                if (newnode == null)
                {
                    DG_Location parentlayer = childlist.Where(item => item.LocationID == tempbd.ParentID).FirstOrDefault();
                    if (parentlayer != null)
                    {
                        newnode.Name = parentlayer.LocationID.ToString();
                        newnode.Text = parentlayer.LocationName;
                        Node parentnode = LocationTree.Nodes.Find(parentlayer.ParentID.ToString(), true).FirstOrDefault();
                        LocationTree.SelectedNode = parentnode;
                        LocationTree.SelectedNode.Nodes.Add(newnode);
                        childlist.Remove(parentlayer);
                    }
                }

                Node childnode = new Node();
                childnode.Text = tempbd.LocationName;
                childnode.Name = tempbd.LocationID.ToString();
                if (newnode != null)
                {
                    LocationTree.SelectedNode = newnode;
                    LocationTree.SelectedNode.Nodes.Add(childnode);
                }
            }

            if (LocationTree.Nodes.Count > 0)
            {
                LocationTree.SelectedNode = LocationTree.Nodes[0];
            }
        }

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
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            int deptId = 0;//药库ID
            deptId = DeptId;
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (cmb_Type.SelectedValue != null)
            {
                queryCondition.Add("c.TypeID", cmb_Type.SelectedValue.ToString());
            }

            if (txt_Dosage.MemberValue != null)
            {
                queryCondition.Add("c.DosageID", txt_Dosage.MemberValue.ToString());
            }

            if (txt_Code.Text.Trim() != string.Empty)
            {
                string code = txt_Code.Text.Trim();
                queryCondition.Add(string.Empty, "(b.PYCode like '%" + code + "%' or b.WBCode like '%" + code + "%' or c.PYCode like '%" + code + "%' or c.WBCode like '%" + code + "%')");
            }

            if (chk_Store.Checked)
            {
                queryCondition.Add("a.Amount>", "1");
            }

            if (chkNotLocation.Checked)
            {
                queryCondition.Add("a.LocationID<", "0");
            }

            if (chkIsLocation.Checked)
            {
                queryCondition.Add("a.LocationID>", "1");
            }

            if (LocationTree.SelectedNode != null)
            {
                if (LocationTree.SelectedNode.Level == 1 && chkIsLocation.Checked== true)
                {
                    queryCondition.Add("a.LocationID", LocationTree.SelectedNode.Name);
                }
            }

            queryCondition.Add("a.DeptID", deptId.ToString());
            return queryCondition;
        }
        #endregion
    }
}
