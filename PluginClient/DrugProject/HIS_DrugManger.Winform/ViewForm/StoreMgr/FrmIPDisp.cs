using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 住院发药
    /// </summary>
    public partial class FrmIPDisp : BaseFormBusiness, IFrmIPDisp
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmIPDisp()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法

        private DataTable dtfee;
        /// <summary>
        /// 病人费用信息
        /// </summary>
        public DataTable dtFee
        {
            get
            {
                return dtfee;
            }

            set
            {
                dtfee= value;
            }
        }
        /// <summary>
        /// 设置全选，全不选
        /// </summary>
        /// <param name="isselect">是否选中</param>
        private void SetRecipeOrderAllSelect(bool isselect)
        {
            DataTable dt = dgDetail.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int value = 1;
                    if (!isselect)
                    {
                        value = 0;
                    }

                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        dt.Rows[index]["chk"] = value;
                    }
                }
            }
        }

        /// <summary>
        /// 当前待发药明细
        /// </summary>
        private DataTable dtCurrentDetails;

        /// <summary>
        /// 统领单
        /// </summary>
        private DataTable totalOrder = new DataTable();

        /// <summary>
        /// 取得当前待发药明细
        /// </summary>
        /// <returns>当前待发药明细数据集</returns>
        private DataTable GetCurrentDetails()
        {
            return InvokeController("GetCurrentDetail") as DataTable;
        }

        /// <summary>
        /// 创建统领信息表
        /// </summary>
        private void BuildTotalDt()
        {
            if (totalOrder != null)
            {
                if (totalOrder.Columns.Contains("DrugID"))
                {
                    return;
                }

                totalOrder.Columns.Add("DrugID", Type.GetType("System.Int32"));
                totalOrder.Columns.Add("ChemName", Type.GetType("System.String"));
                totalOrder.Columns.Add("Spec", Type.GetType("System.String"));
                totalOrder.Columns.Add("ProductName", Type.GetType("System.String"));
                totalOrder.Columns.Add("DoseName", Type.GetType("System.String"));
                totalOrder.Columns.Add("DispAmount", Type.GetType("System.Decimal"));
                totalOrder.Columns.Add("UnitName", Type.GetType("System.String"));
                totalOrder.Columns.Add("RetailPrice", Type.GetType("System.Decimal"));
                totalOrder.Columns.Add("RetailFee", Type.GetType("System.Decimal"));
                totalOrder.PrimaryKey = new DataColumn[] { totalOrder.Columns["DrguID"] };
            }
        }

        /// <summary>
        /// 检查树节点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="billTypeId">类型ID</param>
        /// <returns>提示句</returns>
        private string CheckNodeStatus(string deptId, string billTypeId)
        {
            string message = string.Empty;
            if (tvBill != null && tvBill.Nodes.Count > 0)
            {
                foreach (TreeNode firstNode in tvBill.Nodes)
                {
                    foreach (TreeNode node in firstNode.Nodes)
                    {
                        if (node.Checked)
                        {
                            DataRow row = (DataRow)node.Tag;
                            string patientDeptId = row["PresDeptID"].ToString();
                            string billType = row["BillTypeID"].ToString();
                            if (deptId != patientDeptId)
                            {
                                message = "不能跨科室选择单据";
                                break;
                            }

                            if (billTypeId != billType)
                            {
                                message = "不能选择不同的统领单据";
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                message = string.Empty;
            }

            return message;
        }

        /// <summary>
        /// 汇总统领单绑定表格
        /// </summary>
        /// <param name="dtDetail">统领明细</param>
        /// <param name="dgView">网格数据源</param>
        private void GetTotalOrder(DataTable dtDetail, DataGridView dgView)
        {
            var result = from r in dtDetail.AsEnumerable()
                         group r by new
                         {
                             DrugID = Convert.ToInt32(r["DrugID"]),
                             ChemName = r["ChemName"].ToString(),
                             Spec = r["DrugSpec"].ToString(),
                             ProductName = r["ProductName"].ToString(),
                             DoseName = r["DoseName"].ToString(),
                             UnitName = r["UnitName"].ToString(),
                             RetailPrice = Convert.ToDecimal(r["SellPrice"])
                         }
                         into g
                         select new
                         {
                             DrugID = g.Key.DrugID,
                             ChemName = g.Key.ChemName,
                             Spec = g.Key.Spec,
                             ProductName = g.Key.ProductName,
                             DoseName = g.Key.DoseName,
                             UnitName = g.Key.UnitName,
                             RetailPrice = g.Key.RetailPrice,
                             DispAmount = g.Sum(r => Convert.ToDecimal(r["DispAmount"])),
                             RetailFee = g.Sum(r => Convert.ToDecimal(r["SellFee"]))
                         };

            totalOrder.Clear();
            DataRow dr;
            foreach (var re in result)
            {
                dr = totalOrder.NewRow();
                dr["DrugID"] = re.DrugID;
                dr["ChemName"] = re.ChemName;
                dr["Spec"] = re.Spec;
                dr["ProductName"] = re.ProductName;
                dr["DoseName"] = re.DoseName;
                dr["DispAmount"] = re.DispAmount;
                dr["UnitName"] = re.UnitName;
                dr["RetailPrice"] = re.RetailPrice;
                dr["RetailFee"] = re.RetailFee;
                totalOrder.Rows.Add(dr);
            }

            dgView.DataSource = totalOrder;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 处理过滤的明细选中情况
        /// </summary>
        /// <param name="selectedDetail">过滤数据</param>
        public void HandleFilterDetail(DataTable selectedDetail)
        {
            DataTable dt = dgDetail.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                /*DataRow[] drs = selectedDetail.Select("BillDetailID=" + row["BillDetailID"].ToString() + "and chk=1");
                if (drs.Length > 0)
                {
                    row["chk"] = 1;
                }
                else
                {
                    row["chk"] = 0;
                }*/

                DataRow[] drs = selectedDetail.Select("BillDetailID=" + row["BillDetailID"].ToString() + "and chk=0");
                if (drs.Length > 0)
                {
                    row["chk"] = 0;
                }
            }
        }

        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtStoreRoom">库房信息</param>
        public void BindStoreRoomCombobox(DataTable dtStoreRoom)
        {
            cmbStoreRoom.DataSource = dtStoreRoom;
            cmbStoreRoom.ValueMember = "DeptID";
            cmbStoreRoom.DisplayMember = "DeptName";
            if (dtStoreRoom.Rows.Count > 0)
            {
                cmbStoreRoom.SelectedIndex = 0;
            }

            DataTable dt = dtStoreRoom.Copy();
            cmbCStoreRoom.DataSource = dt;
            cmbCStoreRoom.ValueMember = "DeptID";
            cmbCStoreRoom.DisplayMember = "DeptName";
            if (dt.Rows.Count > 0)
            {
                cmbCStoreRoom.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定临床科室ShowCard
        /// </summary>
        /// <param name="dtClinicalDept">临床科室数据</param>
        public void BindClinicalDeptShowCard(DataTable dtClinicalDept)
        {
            txtDeptCard.DisplayField = "Name";
            txtDeptCard.MemberField = "DeptId";
            txtDeptCard.CardColumn = "DeptId|编码|100,Name|科室名称|auto";
            txtDeptCard.QueryFieldsString = "Name,Pym,Wbm";
            txtDeptCard.ShowCardWidth = 350;
            txtDeptCard.ShowCardDataSource = dtClinicalDept;
        }

        /// <summary>
        /// 设置发药明细变量
        /// </summary>
        /// <param name="dt">发药明细</param>
        public void SetSendBillDetail(DataTable dt)
        {
            dtCurrentDetails = dt;
        }

        /// <summary>
        /// 绑定统领单类型
        /// </summary>
        /// <param name="dtBillType">统领单药品类型</param>
        public void BindIPDrugBillTypeComboBox(DataTable dtBillType)
        {
            cbxMessageType.DataSource = dtBillType;
            cbxMessageType.ValueMember = "BillTypeID";
            cbxMessageType.DisplayMember = "BillTypeName";
            if (dtBillType.Rows.Count > 0)
            {
                cbxMessageType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定待发药树控件
        /// </summary>
        /// <param name="dtHead">统领单头</param>
        public void LoadBillTree(DataTable dtHead)
        {
            //清除所有节点
            tvBill.Nodes.Clear();
            //过滤临床科室创建
            CreateTree(dtHead);
        }

        /// <summary>
        /// 创建树节点
        /// </summary>
        /// <param name="dtHead">头数据源</param>
        private void CreateTree(DataTable dtHead)
        {
            DataTable dtDept = GetDeptInfo(dtHead, new string[] { "PresDeptID", "PresDeptName" });
            foreach (DataRow row in dtDept.Rows)
            {
                TreeNode firstNode = new TreeNode();
                firstNode.Text = row["PresDeptName"].ToString();
                firstNode.Tag = row["PresDeptID"];
                firstNode.ImageIndex = 0;
                tvBill.Nodes.Add(firstNode);
                RecursionBill(Convert.ToInt32(row["PresDeptID"]), firstNode, dtHead);
            }

            tvBill.ExpandAll();
            if (tvBill.Nodes.Count > 0)
            {
                tvBill.SelectedNode = tvBill.Nodes[0];
            }
        }

        /// <summary>
        /// 带发药树节点
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="node">树节点对象</param>
        /// <param name="dtHead">表头数据源</param>
        private void RecursionBill(int deptId, TreeNode node, DataTable dtHead)
        {
            DataRow[] rows = dtHead.Select("PresDeptID=" + deptId.ToString());
            foreach (DataRow row in rows)
            {
                TreeNode nodes = new TreeNode();
                nodes.Text = Convert.ToDateTime(row["MakeDate"]).ToString("yyyy-MM-dd HH:mm") + "【" + row["BillTypeName"].ToString() + "】";
                nodes.Tag = row;
                node.Nodes.Add(nodes);
            }
        }

        /// <summary>
        /// 已发药树节点
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="node">树节点对象</param>
        /// <param name="dtHead">表头数据源</param>
        private void RecursionDispBill(int deptId, TreeNode node, DataTable dtHead)
        {
            DataRow[] rows = dtHead.Select("DeptID=" + deptId.ToString());
            foreach (DataRow row in rows)
            {
                TreeNode nodes = new TreeNode();
                nodes.Text = Convert.ToDateTime(row["DispTime"]).ToString("yyyy-MM-dd HH:mm") + "【" + row["BillTypeName"].ToString() + "】";
                nodes.Tag = row;
                node.Nodes.Add(nodes);
            }
        }

        /// <summary>
        /// 获取科室详情
        /// </summary>
        /// <param name="dtHead">表头</param>
        /// <param name="filedNames">字段名</param>
        /// <returns>科室详情</returns>
        public DataTable GetDeptInfo(DataTable dtHead, string[] filedNames)
        {
            DataView dv = dtHead.DefaultView;
            DataTable distTable = dv.ToTable("dept", true, filedNames);
            return distTable;
        }

        /// <summary>
        /// 创建发药树节点
        /// </summary>
        /// <param name="dtHead">表头数据源</param>
        private void CreateDispTree(DataTable dtHead)
        {
            DataTable dtDept = GetDeptInfo(dtHead, new string[] { "DeptID", "DeptName" });
            foreach (DataRow row in dtDept.Rows)
            {
                TreeNode firstNode = new TreeNode();
                firstNode.Text = row["DeptName"].ToString();
                firstNode.Tag = row["DeptID"];
                firstNode.ImageIndex = 0;
                tvCompleteBill.Nodes.Add(firstNode);
                RecursionDispBill(Convert.ToInt32(row["DeptID"]), firstNode, dtHead);
            }

            tvCompleteBill.ExpandAll();
            if (tvCompleteBill.Nodes.Count > 0)
            {
                tvCompleteBill.SelectedNode = tvCompleteBill.Nodes[0];
            }
        }

        /// <summary>
        /// 绑定已发药树控件
        /// </summary>
        /// <param name="dtHead">统领单头</param>
        public void LoadCompleteBillTree(DataTable dtHead)
        {
            //清除所有节点
            tvCompleteBill.Nodes.Clear();

            //过滤临床科室创建
            CreateDispTree(dtHead);
        }

        /// <summary>
        /// 取得待发药单据头查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetIPBillHeadCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (txtDeptCard.MemberValue != null)
            {
                queryCondition.Add("a.PresDeptID", txtDeptCard.MemberValue.ToString());
            }

            if (cbxMessageType.SelectedValue != null && cbxMessageType.SelectedValue.ToString() != "-1")
            {
                queryCondition.Add("a.BillTypeID", cbxMessageType.SelectedValue.ToString());
            }

            //在控制器中增加科室ID条件
            return queryCondition;
        }

        /// <summary>
        /// 取得待发药明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetIPBillDetailCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (txtDeptCard.MemberValue != null)
            {
                queryCondition.Add("k.PresDeptID", txtDeptCard.MemberValue.ToString());
            }

            if (cbxMessageType.SelectedValue != null && cbxMessageType.SelectedValue.ToString() != "-1")
            {
                queryCondition.Add("k.BillTypeID", cbxMessageType.SelectedValue.ToString());
            }

            //在控制器中增加科室ID条件
            return queryCondition;
        }

        /// <summary>
        /// 取得已发药单据头查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetDispBillHeadCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "a.DispTime BETWEEN '" + dtSendDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + dtSendDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");

            //在控制器中增加科室ID条件
            return queryCondition;
        }

        /// <summary>
        /// 取得已发药明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetDispBillDetailCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            string subCondition = " EXISTS(SELECT 1 FROM DS_IPDispHead r WHERE  a.DispHeadID = r.DispHeadID AND r.DispTime BETWEEN '" + dtSendDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + dtSendDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "')";
            queryCondition.Add(string.Empty, subCondition);

            //在控制器中增加科室ID条件,已发标志
            return queryCondition;
        }

        /// <summary>
        /// 绑定待发药表头Grid
        /// </summary>
        /// <param name="dtBillHead">待发药表头Grid</param>
        public void BindBillHeadGrid(DataTable dtBillHead)
        {
        }

        /// <summary>
        /// 绑定待发药单据明细Grid
        /// </summary>
        /// <param name="dtBillDetail">待发药单据明细Grid</param>
        public void BindBillDetailGrid(DataTable dtBillDetail)
        {
            DataTable dtEmpty = dtBillDetail.Clone();
            dgDetail.DataSource = dtEmpty;
        }

        /// <summary>
        /// 绑定已发药单据头Grid
        /// </summary>
        /// <param name="dtBillHead">已发药单据头Grid</param>
        public void BindDispHeadGrid(DataTable dtBillHead)
        {
        }

        /// <summary>
        /// 绑定已发药单据明细Grid
        /// </summary>
        /// <param name="dtBillDetail">已发药单据明细Grid</param>
        public void BindDispDetailGrid(DataTable dtBillDetail)
        {
        }

        /// <summary>
        /// 通过单据头Ｉｄ获取单据明细，绑定明细grid
        /// </summary>
        /// <param name="dt">明细</param>
        public void BindBillDetailByHeadIdGrid(DataTable dt)
        {
            //获取数据源
            dgDetail.EndEdit();
            DataTable dtDetail = dgDetail.DataSource as DataTable;
            if (dtDetail != null)
            {
                //插入数据
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    dtDetail.Rows.Add(dt.Rows[index].ItemArray);
                }

                dgDetail.DataSource = dtDetail;
            }
            //绑定统领单
            GetTotalOrder(dtDetail, dgHead);

            //绑定摆药单数据
            BindSinglePendulumGrid(dtDetail);
        }

        /// <summary>
        /// 通过树节点过滤已发药明细
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDispDetalByTreeNodeGrid(DataTable dt)
        {
            dgCompleteDetail.DataSource = dt;

            //绑定统领单
            GetTotalOrder(dt, dgCompleteHead);

            //绑定摆药单
            BindCompleteSinglePendulumGrid(dt);
        }

        /// <summary>
        /// 绑定摆药单数据
        /// </summary>
        /// <param name="dt">发药单明细</param>
        public void BindSinglePendulumGrid(DataTable dtdetail)
        {
            dGSinglePendulum.DataSource = CreateSinglePendulumData(dtdetail);
            SetGridColor(dGSinglePendulum);
        }

        /// <summary>
        /// 绑定已发药的摆药单
        /// </summary>
        /// <param name="dtCompleteDetail"></param>
        public void BindCompleteSinglePendulumGrid(DataTable dtCompleteDetail)
        {
            dgCompleteSinglePendulum.DataSource = CreateSinglePendulumData(dtCompleteDetail);
            SetGridColor(dgCompleteSinglePendulum);
        }


        /// <summary>
        /// 设置网格颜色
        /// </summary>
        private void SetGridColor(DataGridView SinglePendulumGrid)
        {
            if (SinglePendulumGrid != null && SinglePendulumGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in SinglePendulumGrid.Rows)
                {
                    string status_flag = SinglePendulumGrid== dgCompleteSinglePendulum ? row.Cells["ChemName1"].Value.ToString() : row.Cells["ChemName"].Value.ToString();
                    Color foreColor = Color.Black;
                    if (status_flag == "小   计")
                    {
                        foreColor = Color.Blue;
                    }

                    if (SinglePendulumGrid == dgCompleteSinglePendulum)
                    {
                        dgCompleteSinglePendulum.SetRowColor(row.Index, foreColor, true);
                    }
                    else
                    {
                        dGSinglePendulum.SetRowColor(row.Index, foreColor, true);
                    }
                }
            }
        }

        public DataTable CreateSinglePendulumData(DataTable dtdetail)
        {
            DataTable DtCollect = dtdetail.Clone();
            DtCollect.Clear();
            
            if (dtdetail.Rows.Count > 0)
            {
                if (dtdetail.Rows.Count == 1)
                {
                    DtCollect = dtdetail;
                }
                else
                {
                    for (int i = 1; i < dtdetail.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtdetail.Rows[i]["PatListID"]) != Convert.ToInt32(dtdetail.Rows[i - 1]["PatListID"])
                                 || Convert.ToInt32(dtdetail.Rows[i]["DrugID"]) != Convert.ToInt32(dtdetail.Rows[i - 1]["DrugID"])
                                 || Convert.ToInt32(dtdetail.Rows[i]["GroupNO"]) != Convert.ToInt32(dtdetail.Rows[i - 1]["GroupNO"])
                                 || dtdetail.Rows[i]["PresDocName"].ToString() != dtdetail.Rows[i - 1]["PresDocName"].ToString())
                        {
                            DataRow row = DtCollect.NewRow();
                            row["DrugID"] = dtdetail.Rows[i - 1]["DrugID"];
                            row["ChemName"] = dtdetail.Rows[i - 1]["ChemName"];
                            row["DrugSpec"] = dtdetail.Rows[i - 1]["DrugSpec"];
                            row["ProductName"] = dtdetail.Rows[i - 1]["ProductName"];

                            row["DispAmount"] = dtdetail.Compute("sum(DispAmount)", "PatListID="
                                                + dtdetail.Rows[i - 1]["PatListID"] + " And DrugID="
                                                + dtdetail.Rows[i - 1]["DrugID"] + " And GroupNO="
                                                + dtdetail.Rows[i - 1]["GroupNO"] + " And PresDocName='"
                                                + dtdetail.Rows[i - 1]["PresDocName"]+"'");

                            row["UnitName"] = dtdetail.Rows[i - 1]["UnitName"];
                            row["SellPrice"] = dtdetail.Rows[i - 1]["SellPrice"];

                            row["SellFee"] = dtdetail.Compute("sum(SellFee)", "PatListID="
                                                + dtdetail.Rows[i - 1]["PatListID"] + " And DrugID="
                                                + dtdetail.Rows[i - 1]["DrugID"] + " And GroupNO="
                                                + dtdetail.Rows[i - 1]["GroupNO"] + " And PresDocName='"
                                                + dtdetail.Rows[i - 1]["PresDocName"] + "'");

                            row["DoseName"] = dtdetail.Rows[i - 1]["DoseName"];
                            row["PatListID"] = dtdetail.Rows[i - 1]["PatListID"];
                            row["PatName"] = dtdetail.Rows[i - 1]["PatName"];
                            row["Sex"] = dtdetail.Rows[i - 1]["Sex"];
                            row["InpatientNO"] = dtdetail.Rows[i - 1]["InpatientNO"];
                            row["BedNO"] = dtdetail.Rows[i - 1]["BedNO"];
                            row["PresDocName"] = dtdetail.Rows[i - 1]["PresDocName"];
                            row["GroupNO"] = dtdetail.Rows[i - 1]["GroupNO"];
                            DtCollect.Rows.Add(row);
                        }

                        if (i == dtdetail.Rows.Count - 1)
                        {
                            DataRow row = DtCollect.NewRow();
                            row["DrugID"] = dtdetail.Rows[i]["DrugID"];
                            row["ChemName"] = dtdetail.Rows[i]["ChemName"];
                            row["DrugSpec"] = dtdetail.Rows[i]["DrugSpec"];
                            row["ProductName"] = dtdetail.Rows[i]["ProductName"];

                            row["DispAmount"] = dtdetail.Compute("sum(DispAmount)", "PatListID="
                                                + dtdetail.Rows[i]["PatListID"] + " And DrugID="
                                                + dtdetail.Rows[i]["DrugID"] + " And GroupNO="
                                                + dtdetail.Rows[i]["GroupNO"] + " And PresDocName='"
                                                + dtdetail.Rows[i - 1]["PresDocName"] + "'");

                            row["UnitName"] = dtdetail.Rows[i]["UnitName"];
                            row["SellPrice"] = dtdetail.Rows[i]["SellPrice"];

                            row["SellFee"] = dtdetail.Compute("sum(SellFee)", "PatListID="
                                                + dtdetail.Rows[i]["PatListID"] + " And DrugID="
                                                + dtdetail.Rows[i]["DrugID"] + " And GroupNO="
                                                + dtdetail.Rows[i]["GroupNO"] + " And PresDocName='"
                                                + dtdetail.Rows[i - 1]["PresDocName"] + "'");

                            row["DoseName"] = dtdetail.Rows[i]["DoseName"];
                            row["PatListID"] = dtdetail.Rows[i]["PatListID"];
                            row["PatName"] = dtdetail.Rows[i]["PatName"];
                            row["Sex"] = dtdetail.Rows[i]["Sex"];
                            row["InpatientNO"] = dtdetail.Rows[i]["InpatientNO"];
                            row["BedNO"] = dtdetail.Rows[i]["BedNO"];
                            row["PresDocName"] = dtdetail.Rows[i]["PresDocName"];
                            row["GroupNO"] = dtdetail.Rows[i]["GroupNO"];
                            DtCollect.Rows.Add(row);
                        }
                    }
                }
            }

            DataTable dtCopy = DtCollect.Clone();
            dtCopy.Clear();
            if (DtCollect.Rows.Count > 0)
            {
                dtCopy.Rows.Add(DtCollect.Rows[0].ItemArray);
                if (DtCollect.Rows.Count == 1)
                {
                    DataRow row = dtCopy.NewRow();
                    row["ChemName"] = "小   计";
                    row["DispAmount"] = DBNull.Value;
                    row["SellFee"] = DBNull.Value;
                    row["SellFee"] = DtCollect.Compute("sum(SellFee)", "PatListID=" + DtCollect.Rows[0]["PatListID"] + " And DrugID=" + dtdetail.Rows[0]["DrugID"]);
                    row["PatName"] = dtdetail.Rows[0]["PatName"];
                    dtCopy.Rows.Add(row);
                }
                else
                {
                    for (int i = 1; i < DtCollect.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DtCollect.Rows[i]["PatListID"]) != Convert.ToInt32(DtCollect.Rows[i - 1]["PatListID"]))
                        {
                            DataRow row = dtCopy.NewRow();
                            row["ChemName"] = "小   计";
                            row["DispAmount"] = DBNull.Value;
                            row["SellFee"] = DBNull.Value;
                            row["SellFee"] = DtCollect.Compute("sum(SellFee)", "PatListID=" + DtCollect.Rows[i - 1]["PatListID"]);
                            row["PatName"] = DtCollect.Rows[i-1]["PatName"];
                            dtCopy.Rows.Add(row);
                        }

                        dtCopy.Rows.Add(DtCollect.Rows[i].ItemArray);
                        if (i == DtCollect.Rows.Count - 1)
                        {
                            DataRow row = dtCopy.NewRow();
                            row["ChemName"] = "小   计";
                            row["DispAmount"] = DBNull.Value;
                            row["SellFee"] = DBNull.Value;
                            row["SellFee"] = DtCollect.Compute("sum(SellFee)", "PatListID=" + DtCollect.Rows[i]["PatListID"]);
                            row["PatName"] = DtCollect.Rows[i]["PatName"];
                            dtCopy.Rows.Add(row);
                        }
                    }
                }
            }
            return dtCopy;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmIPDisp_OpenWindowBefore(object sender, EventArgs e)
        {
            //构建统领单表
            BuildTotalDt();
            dgHead.DataSource = totalOrder;
            dgCompleteHead.DataSource = totalOrder;
            InvokeController("GetDrugStoreData");

            //给查询日期设置默认值
            dtSendDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtSendDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetClinicalDeptData");
            InvokeController("GetIPDrugBillType");
            InvokeController("GetIPDrugBillHead");
            InvokeController("GetIPDrugBillDetail");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (dgDetail != null && dgDetail.Rows.Count > 0)
            {
                DataTable dtDetail = dgDetail.DataSource as DataTable;
                dgDetail.DataSource = dtDetail.Clone();
            }

            if (dgHead != null && dgHead.Rows.Count > 0)
            {
                DataTable dtHead = dgHead.DataSource as DataTable;
                dgHead.DataSource = dtHead.Clone();
            }

            InvokeController("GetIPDrugBillHead");
            InvokeController("GetIPDrugBillDetail");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgDetail.Rows.Count <= 0)
                {
                    return;
                }

                DataTable dtDetail = dgDetail.DataSource as DataTable;
                DataRow[] drs = dtDetail.Select("chk=1");
                if (drs.Length > 0)
                {
                    int deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
                    for (int i = 0; i < dgDetail.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(((DataGridViewCheckBoxXCell)dgDetail.Rows[i].Cells["chk"]).Value) == true)
                        {
                            dtDetail.Rows[i]["chk"] = 1;
                        }
                        else
                        {
                            dtDetail.Rows[i]["chk"] = 0;
                        }
                    }

                    DataView dv = dtDetail.DefaultView;
                    dv.RowFilter = "chk=1";
                    DataTable dtTransfer = dv.ToTable();
                    InvokeController("IPDisp", dtTransfer, deptId);
                    btnQuery_Click(null, null);
                }
                else
                {
                    MessageBoxEx.Show("没有待发的药品，请重新选择");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            if (dgHead.Rows.Count > 0)
            {
                DataTable dtHead = dgHead.DataSource as DataTable;
                DataTable dtDetail = dgDetail.DataSource as DataTable;
                InvokeController("PrintTLBill", dtHead, dtDetail, cmbStoreRoom.Text);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dgDetail.Rows.Count > 0)
            {
                InvokeController("Filter", (DataTable)dgDetail.DataSource);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCompleteQuery_Click(object sender, EventArgs e)
        {
            if (dgCompleteDetail != null && dgCompleteDetail.Rows.Count > 0)
            {
                DataTable dtDetail = dgCompleteDetail.DataSource as DataTable;
                dgCompleteDetail.DataSource = dtDetail.Clone();
            }

            if (dgCompleteHead != null && dgCompleteHead.Rows.Count > 0)
            {
                DataTable dtHead = dgCompleteHead.DataSource as DataTable;
                dgCompleteHead.DataSource = dtHead.Clone();
            }

            InvokeController("GetDispIPBillHead");
            InvokeController("GetDispDrugBillDetail");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCompleteBill_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbStoreRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbStoreRoom.SelectedValue != null)
                {
                    InvokeController("SetSelectedDept", Convert.ToInt32(cmbStoreRoom.SelectedValue));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbCStoreRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCStoreRoom.SelectedValue != null)
                {
                    InvokeController("SetDispSelectedDept", Convert.ToInt32(cmbCStoreRoom.SelectedValue));
                }
            }
            catch
            {
            }
        }

        public string cKdeptId = "";
        public string cKbillTypeId = "";
        
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tvBill_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    this.Cursor = Cursors.WaitCursor;

                    //如果选择第一级节点禁止选中
                    if (e.Node.Level == 0)
                    {
                        dgDetail.EndEdit();
                        DataTable dtDetails = dgDetail.DataSource as DataTable;
                        dtDetails.Rows.Clear();

                        tvBillAllCheck(e.Node.Checked,e.Node);
                        return;
                    }

                    DataRow row = (DataRow)e.Node.Tag;
                    string deptId = row["PresDeptID"].ToString();
                    string billType = row["BillTypeID"].ToString();
                    string billHeadID = row["BillHeadID"].ToString();

                    //选中了节点加载明细单数据
                    if ((e.Node.Level == 1 && e.Node.Checked))
                    {
                        //判断是不是同科室同一种类型的单据
                        string message = CheckNodeStatus(deptId, billType);
                        if (message != string.Empty)
                        {
                            MessageBoxEx.Show(message);
                            e.Node.Checked = false;
                            return;
                        }

                        cKdeptId = deptId;
                        cKbillTypeId = billType;

                        //插入数据到明细表中
                        InvokeController("GetIPDrugBillDetailByHeadId", billHeadID);
                        //MessageBox.Show("添加数据");
                        return;
                    }

                    //取消选中移除明细单据数据
                    if ((e.Node.Level == 1 && !e.Node.Checked))
                    {
                        dgDetail.EndEdit();
                        DataTable dtDetails = dgDetail.DataSource as DataTable;
                        if (dtDetails != null)
                        {
                            DataRow[] removeRows = dtDetails.Select("BillHeadID=" + billHeadID);
                            dgDetail.CurrentCellChanged -= dgDetail_CurrentCellChanged;
                            foreach (DataRow removeRow in removeRows)
                            {
                                dtDetails.Rows.Remove(removeRow);
                            }

                            dgDetail.CurrentCellChanged += dgDetail_CurrentCellChanged;
                            GetTotalOrder(dtDetails, dgHead);
                            //绑定摆药单数据
                            BindSinglePendulumGrid(dtDetails);
                        }

                        //MessageBox.Show("移除数据成功");
                        return;
                    }
                }
            }
            catch (Exception error)
            {
                e.Node.Checked = false;
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 第一级节点的选中与取消
        /// </summary>
        /// <param name="AllCheckFlag">是否选中状态</param>
        /// <param name="firstNode">第一级节点对象</param>
        public void tvBillAllCheck(bool AllCheckFlag,TreeNode firstNode)
        {
            if(AllCheckFlag==true)
            {
                int i = 0;
                
                foreach (TreeNode node in firstNode.Nodes)
                {
                    DataRow row = (DataRow)node.Tag;
                    string billHeadID = row["BillHeadID"].ToString();
                    if (i == 0)
                    {
                        if (string.IsNullOrEmpty(cKdeptId))
                        {
                            cKdeptId = row["PresDeptID"].ToString();
                        }

                        if(string.IsNullOrEmpty(cKbillTypeId))
                        {
                            cKbillTypeId = row["BillTypeID"].ToString();
                        }
                    }

                    string patientDeptId = row["PresDeptID"].ToString();
                    string billType = row["BillTypeID"].ToString();
                    if (cKdeptId != patientDeptId)
                    {
                        continue;
                    }

                    if (cKbillTypeId != billType)
                    {
                        continue;
                    }

                    node.Checked = AllCheckFlag;
                    i++;
                }
            }
            else
            {
                foreach (TreeNode node in firstNode.Nodes)
                {
                    if (node.Checked == true)
                    {
                        node.Checked = AllCheckFlag;
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmIPDisp_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDetail.CurrentCell != null)
            {
                int rowIndex = dgDetail.CurrentCell.RowIndex;
                DataTable dt = dgDetail.DataSource as DataTable;
                DataRow currentRow = dt.Rows[rowIndex];
                lblBedNo.Text = currentRow["BedNO"].ToString()+ "号床";
                lblInPatientNo.Text= currentRow["CaseNumber"].ToString();
                lblPatName.Text= currentRow["PatName"].ToString();
                lblNursingLevel.Text = currentRow["NursingLever"].ToString();
                lblAge.Text= GetAge(currentRow["Age"].ToString());
                lblDeptName.Text= currentRow["deptName"].ToString();
                lblTimes.Text= "第" + currentRow["Times"].ToString() + "次住院";
                lblDocName.Text= "医生：" + currentRow["PresDocName"].ToString();
                lblPatTypeName.Text= currentRow["patTypeName"].ToString();
                lblNurseName.Text = "护士：" + currentRow["NurseName"].ToString();
                var PatListID = currentRow["PatListID"].ToString();

                InvokeController("GetPatientFeeInfo", Convert.ToInt32(PatListID));
                lblDepositFee.Text = "预交金：" + Convert.ToDecimal(dtFee.Rows[0][0]).ToString("0.00"); ;
                lblChargeFee.Text = "累计记账：" + Convert.ToDecimal(dtFee.Rows[1][0]).ToString("0.00");
                lblLastFee.Text = "余额：" + (Convert.ToDecimal(dtFee.Rows[0][0]) - Convert.ToDecimal(dtFee.Rows[1][0])).ToString("0.00");
                lblESituationName.Text = "诊断：" + currentRow["EnterSituationName"].ToString();
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">传入年龄</param>
        /// <returns>返回转换后年龄</returns>
        private string GetAge(string age)
        {
            string tempAge = string.Empty;
            if (!string.IsNullOrEmpty(age))
            {
                switch (age.Substring(0, 1))
                {
                    // 岁
                    case "Y":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "岁";
                        }

                        break;
                    // 月
                    case "M":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "月";
                        }

                        break;
                    // 天
                    case "D":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "天";
                        }

                        break;
                    // 时
                    case "H":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "时";
                        }

                        break;
                }
            }

            return tempAge;
        }
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgCompleteDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgCompleteDetail.CurrentCell != null)
            {
                int rowIndex = dgCompleteDetail.CurrentCell.RowIndex;
                DataTable dt = dgCompleteDetail.DataSource as DataTable;
                DataRow currentRow = dt.Rows[rowIndex];
                lblCBedNo.Text = currentRow["BedNO"].ToString() + "号床";
                lblCInPatientNo.Text = currentRow["CaseNumber"].ToString();
                lblCPatName.Text = currentRow["PatName"].ToString();
                lblCNurseLevel.Text = currentRow["NursingLever"].ToString();
                lblCAge.Text = GetAge(currentRow["Age"].ToString());
                lblCDeptName.Text = currentRow["deptName"].ToString();
                lblCTimes.Text = "第"+currentRow["Times"].ToString()+ "次住院";
                lblCDocName.Text = "医生：" + currentRow["PresDocName"].ToString();
                lblCPatTypeName.Text = currentRow["patTypeName"].ToString();
                lblCNurseName.Text = "护士：" + currentRow["NurseName"].ToString();
                var PatListID = currentRow["PatListID"].ToString();

                InvokeController("GetPatientFeeInfo", Convert.ToInt32(PatListID));
                lblCDepositFee.Text = "预交金：" + Convert.ToDecimal(dtFee.Rows[0][0]).ToString("0.00"); ;
                lblCChargeFee.Text = "累计记账：" + Convert.ToDecimal(dtFee.Rows[1][0]).ToString("0.00");
                lblCLastFee.Text = "余额：" + (Convert.ToDecimal(dtFee.Rows[0][0]) - Convert.ToDecimal(dtFee.Rows[1][0])).ToString("0.00");
                lblCESName.Text = "诊断：" + currentRow["EnterSituationName"].ToString();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tvCompleteBill_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Level == 0)
                {
                    string deptId = e.Node.Tag.ToString();
                    InvokeController("GetDispDrugBillDetailByTreeNode", deptId, 0);
                }

                if (e.Node.Level == 1)
                {
                    DataRow row = (DataRow)e.Node.Tag;
                    string billHeadID = row["DispHeadID"].ToString();
                    InvokeController("GetDispDrugBillDetailByTreeNode", billHeadID, 1);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDispClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
            {
                return;
            }

            string buttonText = this.dgDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (buttonText == "缺药")
            {
                decimal dispAmount = Convert.ToDecimal(this.dgDetail.Rows[e.RowIndex].Cells["DispAmount"].Value.ToString());
                if (dispAmount < 0)
                {
                    MessageBoxEx.Show("退药不能使用缺药功能");
                    return;
                }

                string billDetailId = this.dgDetail.Rows[e.RowIndex].Cells["BillDetailID"].Value.ToString();
                if (MessageBox.Show("您确认该药品缺药吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    try
                    {
                        bool bRtn = (bool)InvokeController("ShortageDrug", billDetailId);
                        if (bRtn)
                        {
                            dgDetail.EndEdit();
                            DataTable dtDetails = dgDetail.DataSource as DataTable;
                            if (dtDetails != null)
                            {
                                DataRow[] removeRows = dtDetails.Select("BillDetailID=" + billDetailId);
                                dgDetail.CurrentCellChanged -= dgDetail_CurrentCellChanged;
                                foreach (DataRow removeRow in removeRows)
                                {
                                    dtDetails.Rows.Remove(removeRow);
                                }

                                dgDetail.CurrentCellChanged += dgDetail_CurrentCellChanged;
                                GetTotalOrder(dtDetails, dgHead);
                                //绑定摆药单数据
                                BindSinglePendulumGrid(dtDetails);
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBoxEx.Show(error.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgDetail.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                if (e.Value == null || e.Value.ToString() == "0")
                {
                    e.Value = "缺药";
                }
                else
                {
                    e.Value = "缺药设置完成";
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDispPrint_Click(object sender, EventArgs e)
        {
            if (tvCompleteBill.SelectedNode == null)
            {
                return;
            }

            if (tvCompleteBill.SelectedNode.Tag != null && tvCompleteBill.SelectedNode.Level > 0)
            {
                DataRow row = (DataRow)tvCompleteBill.SelectedNode.Tag;
                int iDispHeadID = Convert.ToInt32(row["DispHeadID"].ToString());
                InvokeController("PrintIPDispBill", iDispHeadID, false);
            }
        }
        #endregion
    }
}
