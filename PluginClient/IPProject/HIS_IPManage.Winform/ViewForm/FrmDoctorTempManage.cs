using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmDoctorTempManage : BaseFormBusiness, IDoctorTempManage
    {
        #region "属性"
        /// <summary>
        /// 模板列表
        /// </summary>
        private List<IP_FeeItemTemplateHead> feeTempList = new List<IP_FeeItemTemplateHead>();

        /// <summary>
        /// 是否点击了新增模板
        /// </summary>
        private bool isAdd = false;

        /// <summary>
        /// 账单模板
        /// </summary>
        private IP_FeeItemTemplateHead mFeeItemTemplateHead = new IP_FeeItemTemplateHead();

        /// <summary>
        /// 账单模板
        /// </summary>
        public IP_FeeItemTemplateHead FeeItemTemplateHead
        {
            get
            {
                // 验证是否选择了节点
                if (trvTempList.SelectedNode != null && trvTempList.SelectedNode.Name != "PTemp")
                {
                    IP_FeeItemTemplateHead itemTemplateHead = trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead;
                    if (isAdd)
                    {
                        // 新增模板的场合，做成新模板的数据
                        mFeeItemTemplateHead = new IP_FeeItemTemplateHead();
                        mFeeItemTemplateHead.PTempHeadID = itemTemplateHead.TempHeadID;
                        mFeeItemTemplateHead.TempLevel = itemTemplateHead.TempLevel;
                        mFeeItemTemplateHead.TempClass = 1;
                    }
                    else
                    {
                        // 修改模板的场合，直接返回当前选中模板数据
                        mFeeItemTemplateHead = itemTemplateHead;
                    }
                }

                return mFeeItemTemplateHead;
            }
        }

        /// <summary>
        /// 模板明细列表
        /// </summary>
        public DataTable FeeTempDetailDt
        {
            get
            {
                return grdTempList.DataSource as DataTable;
            }
        }

        /// <summary>
        /// 设置网格状态
        /// </summary>
        public bool SetGridState
        {
            set
            {
                bool b = value;
                if (b == false)
                {
                    grdTempList.ReadOnly = false;
                    grdTempList.Columns[0].ReadOnly = true;
                    grdTempList.Columns[1].ReadOnly = false;
                    grdTempList.Columns[2].ReadOnly = true;
                    grdTempList.Columns[3].ReadOnly = true;
                    grdTempList.Columns[4].ReadOnly = false;
                }
                else
                {
                    grdTempList.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 上一次选中的节点名
        /// </summary>
        private string lastTimeNodeName = string.Empty;

        /// <summary>
        /// 删除标志
        /// </summary>
        private bool isDel = false;

        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDoctorTempManage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开界面加载模板列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDoctorTempManage_OpenWindowBefore(object sender, EventArgs e)
        {
            // 加载模板列表
            InvokeController("GetIPFeeItemTempList", 0);
            // 加载模板明细弹出网格费用数据列表
            InvokeController("GetRegItemShowCard");
        }

        /// <summary>
        /// 设置树型控件右键菜单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void TempManage_Opening(object sender, CancelEventArgs e)
        {
            // 选择根节点或空间空白处时禁用右键菜单
            if (trvTempList.SelectedNode.Name == "PTemp" || trvTempList.SelectedNode == null)
            {
                TempManage.Enabled = false;
            }
            else
            {
                TempManage.Enabled = true;
                tolOperationTemp.Enabled = true;
                tolDelTemp.Enabled = true;
                IP_FeeItemTemplateHead itemTemplateHead = trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead;
                // 选中的是大类时禁用右键菜单
                if (itemTemplateHead.PTempHeadID == 0)
                {
                    tolOperationTemp.Enabled = false;
                    tolDelTemp.Enabled = false;
                }
                else
                {
                    // 选中节点为启用状态时禁用启用按钮
                    if (itemTemplateHead.DelFlag == 0)
                    {
                        tolOperationTemp.Enabled = false;
                    }
                    else
                    {
                        // 选中节点为停用状态时禁用停用按钮
                        tolDelTemp.Enabled = false;
                    }
                }
            }
        }

        #region "模板头编辑"

        /// <summary>
        /// 新增模板头
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolAddTemp_Click(object sender, EventArgs e)
        {
            if (trvTempList.SelectedNode != null && trvTempList.SelectedNode.Name != "PTemp")
            {
                isAdd = true;
                InvokeController("ShowAddDoctorTemp", isAdd);
            }
            else
            {
                InvokeController("MessageShow", "请选择要添加的模板类型！");
            }
        }

        /// <summary>
        /// 修改模板头
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolUpdTemp_Click(object sender, EventArgs e)
        {
            if (trvTempList.SelectedNode != null && trvTempList.SelectedNode.Name != "PTemp")
            {
                isAdd = false;
                InvokeController("ShowAddDoctorTemp", isAdd);
            }
            else
            {
                InvokeController("MessageShow", "请选择要修改的模板！");
            }
        }

        /// <summary>
        /// 停用模板
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolDelTemp_Click(object sender, EventArgs e)
        {
            isAdd = false;
            InvokeController("StopOrOperationFeeTemp", true);
        }

        /// <summary>
        /// 启用模板
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolOperationTemp_Click(object sender, EventArgs e)
        {
            isAdd = false;
            InvokeController("StopOrOperationFeeTemp", false);
        }

        /// <summary>
        /// 选中模板加载模板明细数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void trvTempList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 当选择的是跟节点时不加载数据
            if (trvTempList.SelectedNode == null ||
                trvTempList.SelectedNode.Name == "PTemp" ||
                (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).PTempHeadID == 0 ||
                lastTimeNodeName == trvTempList.SelectedNode.Name)
            {
                if (trvTempList.SelectedNode != null)
                {
                    lastTimeNodeName = trvTempList.SelectedNode.Name;
                }
                else
                {
                    lastTimeNodeName = "PTemp";
                }

                grdTempList.DataSource = new DataTable();
                return;
            }

            lastTimeNodeName = trvTempList.SelectedNode.Name;
            InvokeController("GetFeeTempDetails", (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).TempHeadID);
        }

        #endregion

        #region "模板明细编辑"

        /// <summary>
        /// 新增模板明细
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAddTemp_Click(object sender, EventArgs e)
        {
            // 当选择的是跟节点时不加载数据
            if (trvTempList.SelectedNode.Name == "PTemp" || (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).PTempHeadID == 0)
            {
                InvokeController("MessageShow", "请选择需要增加明细的模板！");
                return;
            }

            SetGridState = false;
            grdTempList.AddRow();
        }

        /// <summary>
        /// 修改模板明细数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnUpdTemp_Click(object sender, EventArgs e)
        {
            // 当选择的是跟节点时不加载数据
            if (trvTempList.SelectedNode.Name == "PTemp" || (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).PTempHeadID == 0)
            {
                InvokeController("MessageShow", "请选择需要修改明细的模板！");
                return;
            }

            SetGridState = false;
        }

        /// <summary>
        /// 删除模板明细数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDelTemp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除当前选中明细吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 当选择的是跟节点时不加载数据
                if (trvTempList.SelectedNode.Name == "PTemp" || (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).PTempHeadID == 0)
                {
                    InvokeController("MessageShow", "请选择需要删除明细的模板！");
                    return;
                }

                if (grdTempList.CurrentCell != null)
                {
                    int rowid = this.grdTempList.CurrentCell.RowIndex;
                    DataTable dt = (DataTable)grdTempList.DataSource;
                    dt.Rows.RemoveAt(rowid);
                    isDel = true;
                }
            }
        }

        /// <summary>
        /// 保存修改后的模板明细数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            grdTempList.EndEdit();
            InvokeController("SaveFeeTempDetailData", (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).TempHeadID, isDel);
        }

        /// <summary>
        /// 设置网格编辑状态
        /// </summary>
        /// <param name="state">状态</param>
        public void SetGrdTempListState(bool state)
        {
            SetGridState = state;
        }

        /// <summary>
        /// 弹出网格选中数据绑定到父网格上
        /// </summary>
        /// <param name="selectedValue">弹出网格选中的数据</param>
        /// <param name="stop">终止标志</param>
        /// <param name="customNextColumnIndex">绑定数据后光标聚焦的位置</param>
        private void grdTempList_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                //int colid = this.grdTempList.CurrentCell.ColumnIndex;
                int rowid = this.grdTempList.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdTempList.DataSource;
                dt.Rows[rowid]["TempHeadID"] = (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).TempHeadID;  // 模板ID
                dt.Rows[rowid]["ItemClass"] = row["ItemClass"];  // 项目类型ID
                dt.Rows[rowid]["ItemClassName"] = row["ItemClassName"];  // 项目类型名
                dt.Rows[rowid]["ItemCode"] = row["ItemCode"];  // 本院编码
                dt.Rows[rowid]["UnitPrice"] = row["UnitPrice"];  // 项目单价
                dt.Rows[rowid]["ItemID"] = row["ItemID"];  // 项目ID
                dt.Rows[rowid]["ItemName"] = row["ItemName"]; // 项目名
                dt.Rows[rowid]["ItemUnit"] = row["UnPickUnit"];   // 项目单位
                dt.Rows[rowid]["ExecDeptID"] = row["ExecDeptId"];  //  执行科室ID
                dt.Rows[rowid]["DelFlag"] = 0;  // 删除标识
                grdTempList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        #endregion

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        #endregion

        #region "数据绑定"

        /// <summary>
        /// 绑定网格弹出网格列表数据
        /// </summary>
        /// <param name="feeDetailDt">模板明细弹出网格列表</param>
        public void Bind_RegItemShowCard(DataTable feeDetailDt)
        {
            grdTempList.SelectionCards[0].BindColumnIndex = ItemName.Index;
            grdTempList.SelectionCards[0].CardColumn = "ItemCode|编码|80,ItemName|项目名称|150,UnitPrice|单价|80,ItemClassName|项目类型|auto";
            grdTempList.SelectionCards[0].CardSize = new System.Drawing.Size(500, 280);
            grdTempList.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            grdTempList.BindSelectionCardDataSource(0, feeDetailDt);
        }

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        /// <param name="feeTempList">模板列表</param>
        /// <param name="tempHeadID">默认选择模板ID</param>
        public void Bind_FeeTempList(List<IP_FeeItemTemplateHead> feeTempList, int tempHeadID)
        {
            this.feeTempList = feeTempList;
            trvTempList.Nodes.Clear();
            // 添加根节点
            Node pNode = new Node();
            pNode.Text = "全部模板";
            pNode.Name = "PTemp";
            trvTempList.Nodes.Add(pNode);
            trvTempList.SelectedNode = pNode;
            // 循环显示父节点
            List<IP_FeeItemTemplateHead> feeTempHeadList = feeTempList.Where(item => item.PTempHeadID == 0).ToList();
            foreach (IP_FeeItemTemplateHead feeTemp in feeTempHeadList)
            {
                Node newNode = new Node();
                newNode.Text = feeTemp.TempName;
                newNode.Name = feeTemp.TempHeadID.ToString();
                newNode.Tag = feeTemp;
                trvTempList.SelectedNode.Nodes.Add(newNode);
            }
            // 循环显示子节点
            List<IP_FeeItemTemplateHead> feeTempDetialList = feeTempList.Where(item => item.PTempHeadID > 0).ToList();
            if (feeTempDetialList.Count > 0)
            {
                foreach (IP_FeeItemTemplateHead feeTemp in feeTempDetialList)
                {
                    // 取得当前循环节点的根节点
                    Node selectNode = trvTempList.Nodes.Find(feeTemp.PTempHeadID.ToString(), true).FirstOrDefault();
                    Node newNode = new Node();
                    newNode.Text = feeTemp.TempName;
                    newNode.Name = feeTemp.TempHeadID.ToString();
                    newNode.Tag = feeTemp;
                    if (feeTemp.DelFlag == 1)
                    {
                        newNode.Style = new DevComponents.DotNetBar.ElementStyle(Color.Red);
                    }

                    trvTempList.SelectedNode = selectNode;
                    trvTempList.SelectedNode.Nodes.Add(newNode);
                }
            }

            if (tempHeadID != 0)
            {
                mFeeItemTemplateHead.TempHeadID = tempHeadID;
            }

            // 重新选中上一次的节点
            if (mFeeItemTemplateHead != null && mFeeItemTemplateHead.TempHeadID != 0)
            {
                Node selectNode = trvTempList.Nodes.Find(mFeeItemTemplateHead.TempHeadID.ToString(), true).FirstOrDefault();
                trvTempList.SelectedNode = selectNode;
            }
            else
            {
                // 默认选中根节点
                if (trvTempList.Nodes.Count > 0)
                {
                    trvTempList.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定模板明细网格数据
        /// </summary>
        /// <param name="feeTempDetailsDt">模板明细列表</param>
        public void Bind_FeeTempDetails(DataTable feeTempDetailsDt)
        {
            grdTempList.EndEdit();
            grdTempList.DataSource = feeTempDetailsDt;
        }

        #endregion
    }
}
