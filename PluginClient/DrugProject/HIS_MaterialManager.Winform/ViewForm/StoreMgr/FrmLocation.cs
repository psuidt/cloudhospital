using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资库位管理
    /// </summary>
    public partial class FrmLocation : BaseFormBusiness, IFrmLocation
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmLocation()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public int LocationID { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        #endregion

        #region 事件

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmLocation_OpenWindowBefore(object sender, EventArgs e)
        {
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

            InvokeController("LoadMaterialStorage", frmName, string.Empty);
        }

        /// <summary>
        /// 新增节点
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void toolMenuAdd_Click(object sender, EventArgs e)
        {
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
        /// 编辑节点
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void toolMenuEdit_Click(object sender, EventArgs e)
        {
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
        /// 删除节点
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void toolMenuDel_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("删除后无法恢复，确定删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.LocationTree.SelectedNode != null)
                {
                    string locaionid = this.LocationTree.SelectedNode.Name;
                    MW_Location location = new MW_Location();
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
        /// 归库
        /// </summary>
        /// <param name="sender">控件</param>
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
                                InvokeController("LoadMaterialStorage", frmName, string.Empty);
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
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选中库位
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmLocation_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnSet_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void BtnSearch_Click(object sender, EventArgs e)
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
        /// 选择物资类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnShowTypeTree_Click(object sender, EventArgs e)
        {
            InvokeController("OpenMaterialTypeDialog", frmName);
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
        /// 加载库位节点信息
        /// </summary>
        /// <param name="locationlist">库位节点信息</param>
        public void GetLocationList(List<MW_Location> locationlist)
        {
            LocationTree.Nodes.Clear();
            List<MW_Location> parentlist = locationlist.Where(item => item.ParentID == 0).ToList();
            foreach (MW_Location tempbd in parentlist)
            {
                Node newnode = new Node();
                newnode.Text = tempbd.LocationName;
                newnode.Name = tempbd.LocationID.ToString();
                LocationTree.Nodes.Add(newnode);
            }

            List<MW_Location> childlist = locationlist.Where(item => item.ParentID > 0).ToList();
            foreach (MW_Location tempbd in childlist)
            {
                Node newnode = LocationTree.Nodes.Find(tempbd.ParentID.ToString(), true).FirstOrDefault();
                if (newnode == null)
                {
                    MW_Location parentlayer = childlist.Where(item => item.LocationID == tempbd.ParentID).FirstOrDefault();
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
            if (txt_Code.Text.Trim() != string.Empty)
            {
                string code = txt_Code.Text.Trim();
                queryCondition.Add(string.Empty, "(b.PYCode like '%" + code + "%' or b.WBCode like '%" + code + "%' or c.PYCode like '%" + code + "%' or c.WBCode like '%" + code + "%')");
            }

            if (chk_Store.Checked)
            {
                queryCondition.Add("a.Amount<", "0");
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
                if (LocationTree.SelectedNode.Level == 1)
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
