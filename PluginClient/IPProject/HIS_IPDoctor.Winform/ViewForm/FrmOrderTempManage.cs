using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    public partial class FrmOrderTempManage : BaseFormBusiness, IOrderTempManage
    {
        #region "属性"  
        /// <summary>
        /// 组号ID
        /// </summary>
        //private int GroupID = 0;

        /// <summary>
        /// 是否已加载模板头明细数据
        /// </summary>
        private bool isLoadModelHead = false;

        /// <summary>
        /// 是否编辑过模板明细数据
        /// </summary>
        private bool isEdit = false;

        /// <summary>
        /// 默认药房ID
        /// </summary>
        private string mDefaultDrugStore = string.Empty;

        /// <summary>
        /// 模板级别
        /// </summary>
        public int ModelLevel
        {
            get
            {
                int mModelLevel = -1;
                if (advRoot.SelectedNode.Text.Contains("全院模板"))
                {
                    mModelLevel = 0;
                }
                else if (advRoot.SelectedNode.Text.Contains("科室模板"))
                {
                    mModelLevel = 1;
                }
                else if (advRoot.SelectedNode.Text.Contains("个人模板"))
                {
                    mModelLevel = 2;
                }

                return mModelLevel;
            }
        }

        /// <summary>
        /// 模板类型（fals:类型/true:模板）
        /// </summary>
        private bool mTempType = false;

        /// <summary>
        /// 是否为新增模板
        /// </summary>
        private bool mIsAdd = false;

        /// <summary>
        /// 模板头表数据
        /// </summary>
        private IPD_OrderModelHead mOrderModelHead = new IPD_OrderModelHead();

        /// <summary>
        /// 模板头表数据
        /// </summary>
        public IPD_OrderModelHead OrderModelHead
        {
            get
            {
                IPD_OrderModelHead iPDOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                if (mIsAdd)
                {
                    mOrderModelHead = new IPD_OrderModelHead();
                    mOrderModelHead.PID = iPDOrderModelHead.ModelHeadID;  // 父模板ID
                    mOrderModelHead.ModelLevel = iPDOrderModelHead.ModelLevel;  // 模板级别
                    mOrderModelHead.CreateDate = DateTime.Now;
                    mOrderModelHead.UpdateDate = DateTime.Now;
                    mOrderModelHead.DeleteFlag = 0;
                    if (!mTempType)
                    {
                        mOrderModelHead.ModelType = 0;
                    }
                    else
                    {
                        // 修改模板的场合，直接返回当前选中模板数据
                        mOrderModelHead.ModelType = 1;
                    }
                }
                else
                {
                    // 修改模板的场合，直接返回当前选中模板数据
                    mOrderModelHead = iPDOrderModelHead;
                }

                return mOrderModelHead;
            }
        }

        /// <summary>
        /// 长期医嘱药品、材料和项目列表
        /// </summary>
        private DataTable mLongItemDrugList = new DataTable();

        /// <summary>
        /// 长期医嘱药品、材料和项目列表
        /// </summary>
        public DataTable LongItemDrugList
        {
            get
            {
                return mLongItemDrugList;
            }

            set
            {
                mLongItemDrugList = value;
            }
        }

        /// <summary>
        /// 长期医嘱ShowCard网格当前显示的药品、项目、材料源数据
        /// </summary>
        private DataTable mShowCardLongDrugList = new DataTable();

        /// <summary>
        /// 临时医嘱ShowCard网格当前显示的药品、项目、材料源数据
        /// </summary>
        private DataTable mShowCardTempDrugList = new DataTable();

        /// <summary>
        /// 临时医嘱药品、材料和项目列表
        /// </summary>
        private DataTable mTempItemDrugList = new DataTable();

        /// <summary>
        /// 临时医嘱药品、材料和项目列表
        /// </summary>
        public DataTable TempItemDrugList
        {
            get
            {
                return mTempItemDrugList;
            }

            set
            {
                mTempItemDrugList = value;
            }
        }

        /// <summary>
        /// 模板头ID
        /// </summary>
        public int ModelHeadID
        {
            get
            {
                IPD_OrderModelHead iPDOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                return iPDOrderModelHead.ModelHeadID;
            }
        }

        /// <summary>
        /// 长期医嘱明细数据
        /// </summary>
        public DataTable LongOrderDetails
        {
            get
            {
                return grdLongOrderDeteil.DataSource as DataTable;
            }
        }

        /// <summary>
        /// 临时医嘱明细数据
        /// </summary>
        public DataTable TempOrderDetails
        {
            get
            {
                return grdTempOrderDetail.DataSource as DataTable;
            }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOrderTempManage()
        {
            InitializeComponent();
            grdLongOrderDeteil.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintGroupLine);
            grdTempOrderDetail.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(HasBeenDocPaintGroupLine);
        }

        /// <summary>
        /// 长期绘制分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="groupFlag">组标记</param>
        private void PaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = LongItemName.Index;
            DataTable docList = grdLongOrderDeteil.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 临时绘制已转抄医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="groupFlag">组标记</param>
        private void HasBeenDocPaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = TempItemName.Index;
            DataTable docList = grdTempOrderDetail.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 分组标记
        /// </summary>
        private int mLastDocGroupFlag = 0;

        /// <summary>
        /// 获取分组线符号
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        /// <param name="docList">datatable数据</param>
        /// <returns>分组标志</returns>
        private int GetGroupFlag(int rowIndex, DataTable docList)
        {
            //try
            //{
            int groupID = Tools.ToInt32(docList.Rows[rowIndex]["GroupID"]);
            // 判断是否为第一行
            if ((rowIndex - 1) == -1)
            {
                // 如果下一行和第一行是同组
                // 判断是否存在多行数据
                if (rowIndex < docList.Rows.Count - 1)
                {
                    if (Tools.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                    {
                        //groupFlag = 1;
                        mLastDocGroupFlag = 1;
                    }
                    else
                    {
                        //groupFlag = 0;
                        mLastDocGroupFlag = 0;
                    }
                }
                else
                {
                    mLastDocGroupFlag = 0;
                }
            }
            else
            {
                // 判断是否为最后一行
                if ((rowIndex + 1) == docList.Rows.Count)
                {
                    // 如果上一行和最后一行是同组
                    if (Tools.ToInt32(docList.Rows[rowIndex - 1]["GroupID"]) == groupID)
                    {
                        mLastDocGroupFlag = 3;
                    }
                    else
                    {
                        mLastDocGroupFlag = 0;
                    }
                }
                else
                {
                    // 中间的行
                    // 如果上一行绘制的是开始线或者上一行绘制的是中间竖线
                    if (mLastDocGroupFlag == 1 || mLastDocGroupFlag == 2)
                    {
                        // 判断下一行是否还是同组
                        if (Tools.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                        {
                            //groupFlag = 2;
                            mLastDocGroupFlag = 2;
                        }
                        else
                        {
                            //groupFlag = 3;
                            mLastDocGroupFlag = 3;
                        }
                    }
                    else if (mLastDocGroupFlag == 3 || mLastDocGroupFlag == 0)
                    {
                        // 如果上一行绘制的是结束线，或者没有绘制分组线
                        // 判断下一行是否还是同组
                        if (Tools.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                        {
                            //groupFlag = 1;
                            mLastDocGroupFlag = 1;
                        }
                        else
                        {
                            //groupFlag = 0;
                            mLastDocGroupFlag = 0;
                        }
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return mLastDocGroupFlag;
        }

        /// <summary>
        /// 窗体打开前数据加载事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOrderTempManage_OpenWindowBefore(object sender, EventArgs e)
        {
            // 加载网格ShowCard基础数据
            InvokeController("GetMasterData");
            // 加载执行药房
            InvokeController("GetDrugStore");
        }

        #region "模板分类维护"

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        /// <param name="orderTempList">模板列表</param>
        /// <param name="tempHeadID">模板头ID</param>
        public void bind_FeeTempList(List<IPD_OrderModelHead> orderTempList, int tempHeadID)
        {
            // 清空现有节点
            advTempDetails.Nodes.Clear();
            advTempDetails.ImageList = imageList1;
            // 显示根节点
            List<IPD_OrderModelHead> orderTempHeadList = orderTempList.Where(item => item.PID == 0).ToList();
            Node newNode = new Node();
            newNode.Text = orderTempHeadList[0].ModelName;
            newNode.Name = orderTempHeadList[0].ModelHeadID.ToString();
            newNode.Tag = orderTempHeadList[0];
            newNode.ImageIndex = 0;
            advTempDetails.Nodes.Add(newNode);
            advTempDetails.SelectedNode = newNode;
            // 显示子节点
            List<IPD_OrderModelHead> tempDetialList = orderTempList.Where(item => item.PID > 0).ToList();
            if (tempDetialList.Count > 0)
            {
                foreach (IPD_OrderModelHead feeTemp in tempDetialList)
                {
                    // 取得当前循环节点的父节点
                    Node selectNode = advTempDetails.Nodes.Find(feeTemp.PID.ToString(), true).FirstOrDefault();
                    // 如果根节点不存在则默认上级节点为根节点
                    if (selectNode == null)
                    {
                        selectNode = advTempDetails.Nodes.Find("0", true).FirstOrDefault();
                    }

                    Node detailNode = new Node();
                    detailNode.Text = feeTemp.ModelName;
                    detailNode.Name = feeTemp.ModelHeadID.ToString();
                    detailNode.Tag = feeTemp;
                    if (feeTemp.ModelType == 0)
                    {
                        detailNode.ImageIndex = 0;
                    }
                    else
                    {
                        detailNode.ImageIndex = 1;
                    }

                    advTempDetails.SelectedNode = selectNode;
                    advTempDetails.SelectedNode.Nodes.Add(detailNode);
                }
            }

            if (tempHeadID != 0)
            {
                mOrderModelHead.ModelHeadID = tempHeadID;
            }
            // 重新选中上一次的节点
            if (mOrderModelHead != null && mOrderModelHead.ModelHeadID != 0)
            {
                Node selectNode = advTempDetails.Nodes.Find(mOrderModelHead.ModelHeadID.ToString(), true).FirstOrDefault();
                advTempDetails.SelectedNode = selectNode;
            }
            else
            {
                // 默认选中根节点
                if (advTempDetails.Nodes.Count > 0)
                {
                    advTempDetails.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 选中模板类型加载模板列表
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void advRoot_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选中的是根节点时不加载数据，界面保持不变
            if (advRoot.SelectedNode == null || advRoot.SelectedNode.Text.Contains("所有级别"))
            {
                return;
            }

            isLoadModelHead = false;
            InvokeController("GetOrderTempList", 0);
            isLoadModelHead = true;
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolAddType_Click(object sender, EventArgs e)
        {
            mTempType = false;
            mIsAdd = true;
            InvokeController("ShowAddOrderTemp");
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolDelType_Click(object sender, EventArgs e)
        {
            mIsAdd = false;
            // 如果选中的分类下存在子节点则不允许删除
            if (advTempDetails.SelectedNode.Nodes.Count > 0)
            {
                InvokeController("MessageShow", "当前分类下存在子分类或模板数据，请先删除子分类或模板后在执行删除！");
                return;
            }

            InvokeController("DelteOrderTemp");
        }

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolAddTemp_Click(object sender, EventArgs e)
        {
            mTempType = true;
            mIsAdd = true;
            InvokeController("ShowAddOrderTemp");
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolDelTemp_Click(object sender, EventArgs e)
        {
            mIsAdd = false;
            InvokeController("DelteOrderTemp");
        }

        /// <summary>
        /// 修改模板分类名
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolUpdType_Click(object sender, EventArgs e)
        {
            mIsAdd = false;
            InvokeController("ShowAddOrderTemp");
        }

        /// <summary>
        /// 修改模板名
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolUpdTemp_Click(object sender, EventArgs e)
        {
            mIsAdd = false;
            InvokeController("ShowAddOrderTemp");
        }

        /// <summary>
        /// 设置右键菜单状态
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TempManage_Opening(object sender, CancelEventArgs e)
        {
            if (advTempDetails.SelectedNode != null)
            {
                IPD_OrderModelHead iPDOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                // 选中跟节点时
                if (iPDOrderModelHead.PID == 0)
                {
                    TempManage.Enabled = true;
                    tolAddType.Enabled = true;
                    tolAddTemp.Enabled = false;
                    tolUpdTemp.Enabled = false;
                    tolDelTemp.Enabled = false;
                    tolUpdType.Enabled = false;
                    tolDelType.Enabled = false;
                }
                else
                {
                    TempManage.Enabled = true;
                    // 选中分类节点时禁用模板按钮
                    if (iPDOrderModelHead.ModelType == 0)
                    {
                        tolAddType.Enabled = true;
                        tolUpdType.Enabled = true;
                        tolDelType.Enabled = true;
                        tolAddTemp.Enabled = true;
                        tolUpdTemp.Enabled = false;
                        tolDelTemp.Enabled = false;
                    }
                    else
                    {
                        tolAddType.Enabled = false;
                        tolUpdType.Enabled = false;
                        tolDelType.Enabled = false;
                        tolAddTemp.Enabled = false;
                        tolUpdTemp.Enabled = true;
                        tolDelTemp.Enabled = true;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 选中模板加载模板明细数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void advTempDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 首次加载模板树时不加载明细数据
            if (isLoadModelHead)
            {
                if (advTempDetails.SelectedNode != null)
                {
                    mOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                    if (mOrderModelHead.ModelType == 1)
                    {
                        txtTempName.Text = mOrderModelHead.ModelName;
                    }
                    else
                    {
                        txtTempName.Text = string.Empty;
                    }

                    isEdit = false;
                    InvokeController("GetOrderTempDetail", isEdit);
                }
            }
        }

        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="longOrderDt">长期医嘱列表</param>
        /// <param name="tempOrderDt">临时医嘱列表</param>
        public void bind_OrderDetails(DataTable longOrderDt, DataTable tempOrderDt)
        {
            // 同组数据合并
            //tabControl1.SelectedTabIndex = 0;
            DataGroup(longOrderDt);
            DataGroup(tempOrderDt);
            grdLongOrderDeteil.DataSource = longOrderDt;
            grdTempOrderDetail.DataSource = tempOrderDt;
            grdLongOrderDeteil.EndEdit();
            grdLongOrderDeteil.ReadOnly = true;
            grdTempOrderDetail.EndEdit();
            grdTempOrderDetail.ReadOnly = true;
        }

        /// <summary>
        /// 合并同组部分数据()
        /// </summary>
        /// <param name="orderDetails">明细数据</param>
        private void DataGroup(DataTable orderDetails)
        {
            if (orderDetails.Rows.Count > 0)
            {
                int tempGroupID = 0;
                for (int i = 0; i < orderDetails.Rows.Count; i++)
                {
                    // 说明性医嘱不显示ItemID和剂量
                    if (Tools.ToInt32(orderDetails.Rows[i]["ItemID"]) == 0 ||
                        Tools.ToInt32(orderDetails.Rows[i]["ItemType"]) == 5)
                    {
                        orderDetails.Rows[i]["ItemID"] = DBNull.Value;
                        orderDetails.Rows[i]["Dosage"] = DBNull.Value;
                    }

                    if (i == 0)
                    {
                        tempGroupID = Tools.ToInt32(orderDetails.Rows[i]["GroupID"]);
                        orderDetails.Rows[i]["IsLast"] = 1; // 同组第一条
                        continue;
                    }

                    if (Tools.ToInt32(orderDetails.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        orderDetails.Rows[i]["ChannelName"] = DBNull.Value;
                        orderDetails.Rows[i]["Frenquency"] = DBNull.Value;
                        orderDetails.Rows[i]["DropSpec"] = DBNull.Value;
                        orderDetails.Rows[i]["Entrust"] = DBNull.Value;
                        orderDetails.Rows[i]["IsLast"] = 0;
                    }
                    else
                    {
                        orderDetails.Rows[i]["IsLast"] = 1; // 同组第一条
                    }

                    tempGroupID = Tools.ToInt32(orderDetails.Rows[i]["GroupID"]);
                }
            }
        }

        /// <summary>
        /// 绑定药房列表
        /// </summary>
        /// <param name="drugStoreList">药房列表</param>
        public void bind_DefaultDrugStore(DataTable drugStoreList)
        {
            if (drugStoreList != null && drugStoreList.Rows.Count > 0)
            {
                cmbDrugStore.DisplayMember = "Name";
                cmbDrugStore.ValueMember = "DeptIDs";
                cmbDrugStore.DataSource = drugStoreList;
                cmbDrugStore.SelectedIndex = 0;
                mDefaultDrugStore = cmbDrugStore.SelectedValue.ToString();
            }
        }

        /// <summary>
        /// 切换药房重新绑定药品数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbDrugStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 切换科室重新加载ShowCard数据源
            if (!mDefaultDrugStore.Contains(cmbDrugStore.SelectedValue.ToString()))
            {
                mDefaultDrugStore = cmbDrugStore.SelectedValue.ToString();
                if (cmbDrugStore.SelectedValue.ToString().Contains("-1"))
                {
                    // 判断当前正在编辑的医嘱类型
                    if (tabControl1.SelectedTabIndex == 0)
                    {
                        // 长期医嘱
                        if (grdLongOrderDeteil.CurrentCell != null)
                        {
                            // 根据当前行的大项目ID过滤选中药房后的数据
                            AddRowFilterShowCardData(true, mLongItemDrugList);
                            grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                            grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                        }
                        else
                        {
                            grdLongOrderDeteil.BindSelectionCardDataSource(0, mLongItemDrugList);
                            mShowCardLongDrugList.Rows.Clear();
                            mShowCardLongDrugList.Merge(mLongItemDrugList);
                        }
                    }
                    else if (tabControl1.SelectedTabIndex == 1)
                    {
                        // 临时医嘱
                        if (grdTempOrderDetail.CurrentCell != null)
                        {
                            // 根据当前行的大项目ID过滤选中药房后的数据
                            AddRowFilterShowCardData(true, mTempItemDrugList);
                            grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                            grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                        }
                        else
                        {
                            grdTempOrderDetail.BindSelectionCardDataSource(0, mTempItemDrugList);
                            mShowCardTempDrugList.Rows.Clear();
                            mShowCardTempDrugList.Merge(mTempItemDrugList);
                        }
                    }
                }
                else
                {
                    // 根据选中的药房过滤药品数据
                    // 长期医嘱
                    DataTable longDt = new DataTable();
                    longDt = mLongItemDrugList.Clone();
                    mLongItemDrugList.TableName = "m_LongItemDrugList";
                    // 根据药房ID过滤数据
                    DataView drugView = new DataView(mLongItemDrugList);
                    string sqlWhere = " ItemClass=1 AND ExecDeptId IN (" + cmbDrugStore.SelectedValue.ToString() + ")";
                    drugView.RowFilter = sqlWhere;
                    longDt.Merge(drugView.ToTable());
                    // 过滤出不为药品的数据
                    DataView itemView = new DataView(mLongItemDrugList);
                    string itemSqlWhere = " ItemClass <>1";
                    itemView.RowFilter = itemSqlWhere;
                    longDt.Merge(itemView.ToTable());

                    // 临时医嘱
                    DataTable tempDt = new DataTable();
                    tempDt = mTempItemDrugList.Clone();
                    mTempItemDrugList.TableName = "m_TempItemDrugList";
                    // 根据药房ID过滤数据
                    DataView tempDrugView = new DataView(mTempItemDrugList);
                    string tempSqlWhere = " ItemClass=1 AND ExecDeptId IN (" + cmbDrugStore.SelectedValue.ToString() + ")";
                    tempDrugView.RowFilter = tempSqlWhere;
                    tempDt.Merge(tempDrugView.ToTable());
                    // 过滤出不为药品的数据
                    DataView tempItemView = new DataView(mTempItemDrugList);
                    string tempItemSqlWhere = " ItemClass <>1";
                    tempItemView.RowFilter = tempItemSqlWhere;
                    tempDt.Merge(tempItemView.ToTable());

                    // 判断当前正在编辑的医嘱类型
                    if (tabControl1.SelectedTabIndex == 0)
                    {
                        if (grdLongOrderDeteil.CurrentCell != null)
                        {
                            // 长期医嘱
                            // 根据当前行的大项目ID过滤选中药房后的数据
                            int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                            DataTable dt = grdLongOrderDeteil.DataSource as DataTable;
                            int statID = Tools.ToInt32(dt.Rows[rowIndex]["StatID"]);
                            AddRowFilterShowCardData(true, longDt);
                            grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                            grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                        }
                        else
                        {
                            grdLongOrderDeteil.BindSelectionCardDataSource(0, longDt);
                            mShowCardLongDrugList.Rows.Clear();
                            mShowCardLongDrugList.Merge(longDt);
                        }
                    }
                    else
                    {
                        if (grdTempOrderDetail.CurrentCell != null)
                        {
                            // 临时医嘱
                            // 根据当前行的大项目ID过滤选中药房后的数据
                            int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                            DataTable dt = grdTempOrderDetail.DataSource as DataTable;
                            int statID = Tools.ToInt32(dt.Rows[rowIndex]["StatID"]);
                            AddRowFilterShowCardData(true, tempDt);
                            grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                            grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                        }
                        else
                        {
                            grdTempOrderDetail.BindSelectionCardDataSource(0, tempDt);
                            mShowCardTempDrugList.Rows.Clear();
                            mShowCardTempDrugList.Merge(tempDt);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定网格ShowCard用法列表
        /// </summary>
        /// <param name="channelList">用法列表</param>
        public void bind_ChannelList(DataTable channelList)
        {
            // 长期医嘱
            grdLongOrderDeteil.SelectionCards[1].BindColumnIndex = LongChannelName.Index;
            grdLongOrderDeteil.SelectionCards[1].CardColumn = "ChannelName|用法名|auto";
            grdLongOrderDeteil.SelectionCards[1].CardSize = new System.Drawing.Size(200, 280);
            grdLongOrderDeteil.SelectionCards[1].QueryFieldsString = "ID,ChannelName,PYCode,WBCode";
            grdLongOrderDeteil.BindSelectionCardDataSource(1, channelList);
            // 临时医嘱
            grdTempOrderDetail.SelectionCards[1].BindColumnIndex = TempChannelName.Index;
            grdTempOrderDetail.SelectionCards[1].CardColumn = "ChannelName|用法名|auto";
            grdTempOrderDetail.SelectionCards[1].CardSize = new System.Drawing.Size(200, 280);
            grdTempOrderDetail.SelectionCards[1].QueryFieldsString = "ID,ChannelName,PYCode,WBCode";
            grdTempOrderDetail.BindSelectionCardDataSource(1, channelList);
        }

        /// <summary>
        /// 绑定网格ShowCard频次列表
        /// </summary>
        /// <param name="frequencyList">频次列表</param>
        public void bind_FrequencyList(DataTable frequencyList)
        {
            // 长期医嘱
            grdLongOrderDeteil.SelectionCards[2].BindColumnIndex = LongFrenquency.Index;
            grdLongOrderDeteil.SelectionCards[2].CardColumn = "FrequencyName|频次名|auto";
            grdLongOrderDeteil.SelectionCards[2].CardSize = new System.Drawing.Size(140, 280);
            grdLongOrderDeteil.SelectionCards[2].QueryFieldsString = "FrequencyID,FrequencyName,PYCode,WBCode";
            grdLongOrderDeteil.BindSelectionCardDataSource(2, frequencyList);
            // 临时医嘱
            grdTempOrderDetail.SelectionCards[2].BindColumnIndex = TempFrenquency.Index;
            grdTempOrderDetail.SelectionCards[2].CardColumn = "FrequencyName|频次名|auto";
            grdTempOrderDetail.SelectionCards[2].CardSize = new System.Drawing.Size(140, 280);
            grdTempOrderDetail.SelectionCards[2].QueryFieldsString = "FrequencyID,FrequencyName,PYCode,WBCode";
            grdTempOrderDetail.BindSelectionCardDataSource(2, frequencyList);
        }

        /// <summary>
        /// 绑定网格ShowCard嘱托列表
        /// </summary>
        /// <param name="entrustList">嘱托列表</param>
        public void bind_EntrustList(DataTable entrustList)
        {
            // 长期医嘱
            grdLongOrderDeteil.SelectionCards[3].BindColumnIndex = LongEntrust.Index;
            grdLongOrderDeteil.SelectionCards[3].CardColumn = "EntrustName|嘱托内容|auto";
            grdLongOrderDeteil.SelectionCards[3].CardSize = new System.Drawing.Size(200, 280);
            grdLongOrderDeteil.SelectionCards[3].QueryFieldsString = "EntrustID,EntrustName,PYCode,WBCode";
            grdLongOrderDeteil.BindSelectionCardDataSource(3, entrustList);
            // 临时医嘱
            grdTempOrderDetail.SelectionCards[3].BindColumnIndex = TempEntrust.Index;
            grdTempOrderDetail.SelectionCards[3].CardColumn = "EntrustName|嘱托内容|auto";
            grdTempOrderDetail.SelectionCards[3].CardSize = new System.Drawing.Size(200, 280);
            grdTempOrderDetail.SelectionCards[3].QueryFieldsString = "EntrustID,EntrustName,PYCode,WBCode";
            grdTempOrderDetail.BindSelectionCardDataSource(3, entrustList);
        }

        /// <summary>
        /// 新增医嘱模板明细数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // 判断是否选中的是医嘱模板
            if (advTempDetails.SelectedNode != null)
            {
                IPD_OrderModelHead iPDOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                if (iPDOrderModelHead.ModelType == 0)
                {
                    // 选中的不是医嘱模板则允许新增
                    InvokeController("MessageShow", "请选择医嘱模板！");
                    return;
                }
            }
            else
            {
                // 没有选中模板数据
                return;
            }

            // 长期医嘱
            if (tabControl1.SelectedTabIndex == 0)
            {
                grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
                SetLongGridReadOnly(0);
                DataTable dt = grdLongOrderDeteil.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    AddEmptyRow(dt.Rows.Count, false, false);
                    grdLongOrderDeteil.CurrentCell = this.grdLongOrderDeteil[LongItemID.Index, dt.Rows.Count - 1];
                }
                else
                {
                    if (grdLongOrderDeteil[LongItemName.Name, grdLongOrderDeteil.Rows.Count - 1].Value.ToString() == string.Empty)
                    {
                        grdLongOrderDeteil.HideSelectionCardWhenCustomInput = true;
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                        dt.AcceptChanges();
                    }

                    AddEmptyRow(dt.Rows.Count, false, false);
                    grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
                    grdLongOrderDeteil.CurrentCell = this.grdLongOrderDeteil[LongItemID.Index, dt.Rows.Count - 1];
                    grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                }
            }
            else if (tabControl1.SelectedTabIndex == 1)
            {
                // 临时医嘱
                // 设置网格可用
                grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
                SetTempGridReadOnly(0);
                DataTable dt = grdTempOrderDetail.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    AddEmptyRow(dt.Rows.Count, false, false);
                    grdTempOrderDetail.CurrentCell = this.grdTempOrderDetail[TempItemID.Index, dt.Rows.Count - 1];
                }
                else
                {
                    if (grdTempOrderDetail[TempItemName.Name, grdTempOrderDetail.Rows.Count - 1].Value.ToString() == string.Empty)
                    {
                        grdTempOrderDetail.HideSelectionCardWhenCustomInput = true;
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                        dt.AcceptChanges();
                    }

                    AddEmptyRow(dt.Rows.Count, false, false);
                    grdTempOrderDetail.HideSelectionCardWhenCustomInput = false;
                    grdTempOrderDetail.CurrentCell = this.grdTempOrderDetail[TempItemID.Index, dt.Rows.Count - 1];
                    grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                }
            }

            isEdit = true;
        }

        /// <summary>
        /// 修改医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnUpd_Click(object sender, EventArgs e)
        {
            tolUpdate_Click(null, null);
        }

        /// <summary>
        /// 删除医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            tolDelete_Click(null, null);
        }

        /// <summary>
        /// 保存医嘱模板明细数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证输入的数据是否正确，是否存在剂量为0的数据
            grdLongOrderDeteil.ReadOnly = true;
            grdTempOrderDetail.ReadOnly = true;
            grdLongOrderDeteil.EndEdit();
            grdTempOrderDetail.EndEdit();
            DataTable longOrder = grdLongOrderDeteil.DataSource as DataTable;
            DataTable tempOrder = grdTempOrderDetail.DataSource as DataTable;
            // 长期医嘱是否存在剂量为0的数据
            if (longOrder != null)
            {
                RemoveEmpty(longOrder);
                for (int i = 0; i < longOrder.Rows.Count; i++)
                {
                    // 自由录入的说明性医嘱不验证
                    if (Tools.ToInt32(longOrder.Rows[i]["OrderType"].ToString()) != 1 &&
                        Tools.ToInt32(longOrder.Rows[i]["ItemType"].ToString()) != 5)
                    {
                        if (Tools.ToDecimal(longOrder.Rows[i]["Dosage"].ToString()) <= 0)
                        {
                            tabControl1.SelectedTabIndex = 0;
                            grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, i];
                            grdLongOrderDeteil.BeginEdit(true);
                            InvokeController("MessageShow", "请输入剂量！");
                            return;
                        }
                    }
                }
            }

            // 临时医嘱是否存在剂量为0的数据
            if (tempOrder != null)
            {
                RemoveEmpty(tempOrder);
                for (int i = 0; i < tempOrder.Rows.Count; i++)
                {
                    // 自由绿如的说明性医嘱不验证
                    if (Tools.ToInt32(tempOrder.Rows[i]["OrderType"].ToString()) != 1)
                    {
                        if (Tools.ToDecimal(tempOrder.Rows[i]["Dosage"].ToString()) <= 0)
                        {
                            tabControl1.SelectedTabIndex = 1;
                            grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, i];
                            grdTempOrderDetail.BeginEdit(true);
                            InvokeController("MessageShow", "请输入剂量！");
                            return;
                        }
                    }
                }
            }

            //request.AddData(m_IOrderTempManage.LongOrderDetails);
            //request.AddData(m_IOrderTempManage.TempOrderDetails);
            if (longOrder != null && tempOrder != null)
            {
                InvokeController("SaveOrderDetailsData");
                isEdit = false;
            }
        }

        /// <summary>
        /// 去除DataTable中的完全空白行
        /// </summary>
        /// <param name="dt">传入的datatable</param>
        private void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool isNull = true;
                if (dt.Rows[i]["ItemID"] != DBNull.Value || string.IsNullOrEmpty(dt.Rows[i]["ItemID"].ToString()))
                {
                    if (Tools.ToInt32(dt.Rows[i]["ItemID"]) > 0)
                    {
                        isNull = false;
                    }
                }
                else
                {
                    isNull = false;
                }

                // 判断是否为自由录入的说明性医嘱
                if (Tools.ToInt32(dt.Rows[i]["OrderType"]) == 1&& Tools.ToInt32(dt.Rows[i]["ItemType"]) == 5)
                {
                    // 如果没有录入医嘱内容，则删除当前行
                    if (string.IsNullOrEmpty(Tools.ToString(dt.Rows[i]["ItemName"])))
                    {
                        isNull = true;
                    }
                    else
                    {
                        isNull = false;
                    }
                }

                if (isNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }

            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }

            dt.AcceptChanges();
        }

        /// <summary>
        /// 刷新医嘱模板明细数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (advTempDetails.SelectedNode != null)
            {
                InvokeController("RefreshOrderTempDetail", isEdit);
                isEdit = false;
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        #region "长期医嘱"

        /// <summary>
        /// 控制网格编辑状态
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void grdLongOrderDeteil_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdLongOrderDeteil.CurrentCell != null)
            {
                // 1.同组医嘱除了第一条之外其他数据的频次，用法，嘱托，滴数都不能修改
                // 2.同组医嘱的第一条除医嘱内容、单位、执行科室外都可修改
                // 3.医嘱项目除医嘱内容、单位、执行科室外都可修改
                int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                DataTable orderDt = grdLongOrderDeteil.DataSource as DataTable;
                // 已保存的数据在没点修改之前不允许修改
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["SaveFlag"]) == 1)
                {
                    grdLongOrderDeteil.ReadOnly = true;
                    return;
                }

                // 如果为自由录入的说明性医嘱，只能修改医嘱内容、用法、频次、滴速、嘱托
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["OrderType"]) == 1)
                {
                    SetLongGridReadOnly(3);
                    grdLongOrderDeteil.HideSelectionCardWhenCustomInput = true;
                    return;
                }

                // 网格选择的说明性医嘱
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["ItemType"]) == 5)
                {
                    SetLongGridReadOnly(3);
                    grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
                    return;
                }

                grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
                // 空白数据行除医嘱内容、单位、执行科室外都可修改
                if (orderDt.Rows[rowIndex]["ItemID"].ToString() == "0")
                {
                    SetLongGridReadOnly(0);
                    return;
                }
                else
                {
                    // 判断组号是否为空时，默认为新增的数据，除医嘱内容、单位、执行科室外都可修改
                    if (string.IsNullOrEmpty(orderDt.Rows[rowIndex]["GroupID"].ToString()))
                    {
                        SetLongGridReadOnly(0);
                        return;
                    }
                    else
                    {
                        // 判断是否是组内第一行
                        if (Tools.ToInt32(orderDt.Rows[rowIndex]["IsLast"]) == 1)
                        {
                            // 是组内第一行时，除医嘱内容、单位、执行科室外都可修改
                            SetLongGridReadOnly(0);
                            return;
                        }
                        else
                        {
                            // 不是组内第一行时，只能修改项目Code和剂量
                            SetLongGridReadOnly(1);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 回车新增行
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="colIndex">列号</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="jumpStop">是否向下跳格</param>
        private void grdLongOrderDeteil_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
            DataTable longOrderDt = grdLongOrderDeteil.DataSource as DataTable;
            // 如果当前选中行已保存在数据库中，则整组数据都不能使用回车键新增行，只能使用右键菜单新增
            if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["ModelDetailID"]) != 0 || rowIndex == 0)
            {
                grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                return;
            }

            // 如果为自由录入的说明性医嘱，只能修改医嘱内容、用法、频次、滴速、嘱托
            if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["OrderType"]) == 1)
            {
                // 当前操作的为剂量单元格
                if (colIndex == LongEntrust.Index)
                {
                    tolAuto_Click(null, null);
                }
            }

            // 当前操作的为剂量单元格
            if (colIndex == LongDosage.Index)
            {
                // 判断当前行是否为药品数据
                int itemType = Tools.ToInt32(longOrderDt.Rows[rowIndex]["ItemType"]);
                if (itemType == 1)
                {
                    // 判断是否为同组第一行,如果是第一行则不进行新增行操作
                    if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["IsLast"]) == 1)
                    {
                        grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                        return;
                    }

                    jumpStop = true;
                    AddEmptyRow(rowIndex + 1, true, false);
                    grdLongOrderDeteil.CurrentCell = this.grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                    grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                    return;
                }
            }
        }

        /// <summary>
        /// 网格ShowCard数据选择事件
        /// </summary>
        /// <param name="SelectedValue">选中行</param>
        /// <param name="stop">是否停止路格</param>
        /// <param name="customNextColumnIndex">下一列号</param>
        private void grdLongOrderDeteil_SelectCardRowSelected(object SelectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
            DataRow row = (DataRow)SelectedValue;
            int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
            DataTable longOrderDt = grdLongOrderDeteil.DataSource as DataTable;

            if (customNextColumnIndex == LongItemID.Index)
            {
                // 判断同组医嘱中是否存在相同的数据
                int groupID = Tools.ToInt32(longOrderDt.Rows[rowIndex]["GroupID"]);
                int itemID = Tools.ToInt32(longOrderDt.Rows[rowIndex]["ItemID"]);
                string itemName = longOrderDt.Rows[rowIndex]["ItemName"].ToString();
                longOrderDt.Rows[rowIndex]["ItemID"] = row["ItemID"];
                longOrderDt.Rows[rowIndex]["ItemName"] = row["ItemName"];
                DataRow[] orderDr = longOrderDt.Select("GroupID = " + groupID);
                if (orderDr.Length > 1)
                {
                    int num = 0;
                    for (int i = 0; i < orderDr.Length; i++)
                    {
                        if (row["ItemID"].ToString().Contains(orderDr[i]["ItemID"].ToString()))
                        {
                            if (num > 0)
                            {
                                InvokeController("MessageShow", "这一组已经有" + orderDr[i]["ItemName"].ToString() + "药品，请重新选择！");
                                grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex];
                                longOrderDt.Rows[rowIndex]["ItemID"] = itemID;
                                longOrderDt.Rows[rowIndex]["ItemName"] = itemName;
                                grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                                return;
                            }

                            num++;
                        }
                    }
                }

                longOrderDt.Rows[rowIndex]["ItemType"] = row["ItemClass"];
                longOrderDt.Rows[rowIndex]["ExecDeptId"] = row["ExecDeptId"];
                longOrderDt.Rows[rowIndex]["ExecDeptName"] = row["ExecDeptName"];
                longOrderDt.Rows[rowIndex]["StatID"] = row["StatID"];
                longOrderDt.Rows[rowIndex]["DosageUnit"] = row["DoseUnitName"];
                longOrderDt.Rows[rowIndex]["Spec"] = row["Standard"];
                longOrderDt.Rows[rowIndex]["Factor"] = row["DoseConvertNum"];
            }
            else if (customNextColumnIndex == LongChannelName.Index)
            {
                longOrderDt.Rows[rowIndex]["ChannelID"] = row["ID"];
                longOrderDt.Rows[rowIndex]["ChannelName"] = row["ChannelName"];
            }
            else if (customNextColumnIndex == LongFrenquency.Index)
            {
                longOrderDt.Rows[rowIndex]["FrenquencyID"] = row["FrequencyID"];
                longOrderDt.Rows[rowIndex]["Frenquency"] = row["FrequencyName"];
                int itemType = Tools.ToInt32(longOrderDt.Rows[rowIndex]["ItemType"]);
                // 判断是否存在下一行
                if (longOrderDt.Rows.Count == rowIndex + 1)
                {
                    // 不存在下一行数据
                    AddEmptyRow(rowIndex + 1, itemType == 1 ? true : false, false);
                    stop = true;
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                }
                else
                {
                    // 判断下一行数据是否为空
                    int itemID = Tools.ToInt32(longOrderDt.Rows[rowIndex + 1]["ItemID"]);
                    if (itemID == 0)
                    {
                        stop = true;
                        grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                    }
                    else
                    {
                        stop = true;
                        grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, rowIndex + 1];
                    }
                }
            }
            else if (customNextColumnIndex == LongEntrust.Index)
            {
                longOrderDt.Rows[rowIndex]["Entrust"] = row["EntrustName"];
                int itemType = Tools.ToInt32(longOrderDt.Rows[rowIndex]["ItemType"]);
                // 判断是否存在下一行
                if (longOrderDt.Rows.Count == rowIndex + 1)
                {
                    // 不存在下一行数据
                    AddEmptyRow(rowIndex + 1, itemType == 1 ? true : false, false);
                    stop = true;
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                }
                else
                {
                    // 判断下一行数据是否为空
                    int itemID = Tools.ToInt32(longOrderDt.Rows[rowIndex + 1]["ItemID"]);
                    if (itemID == 0)
                    {
                        stop = true;
                        grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                    }
                    else
                    {
                        stop = true;
                        grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, rowIndex + 1];
                    }
                }
            }

            grdLongOrderDeteil.Refresh();
            grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
        }

        /// <summary>
        /// 设置长期医嘱网格列编辑状态
        /// </summary>
        /// <param name="readOnlyStatus">编辑状态</param>
        private void SetLongGridReadOnly(int readOnlyStatus)
        {
            switch (readOnlyStatus)
            {
                // 点击按钮新开或者回车新开非同组医嘱
                case 0:
                    grdLongOrderDeteil.ReadOnly = false;
                    LongExecDeptName.ReadOnly = true;
                    LongItemName.ReadOnly = true;
                    LongDosageUnit.ReadOnly = true;
                    LongItemID.ReadOnly = false;
                    LongChannelName.ReadOnly = false;
                    LongFrenquency.ReadOnly = false;
                    LongDropSpec.ReadOnly = false;
                    LongEntrust.ReadOnly = false;
                    LongDosage.ReadOnly = false;
                    break;
                // 同组医嘱回车新开
                case 1:
                    grdLongOrderDeteil.ReadOnly = false;
                    LongExecDeptName.ReadOnly = true;
                    LongItemName.ReadOnly = true;
                    LongDosageUnit.ReadOnly = true;
                    LongChannelName.ReadOnly = true;
                    LongFrenquency.ReadOnly = true;
                    LongDropSpec.ReadOnly = true;
                    LongEntrust.ReadOnly = true;
                    LongItemID.ReadOnly = false;
                    LongDosage.ReadOnly = false;
                    break;
                // 说明性医嘱
                case 2:
                    grdLongOrderDeteil.ReadOnly = false;
                    LongExecDeptName.ReadOnly = true;
                    LongItemName.ReadOnly = false;
                    LongDosageUnit.ReadOnly = true;
                    LongChannelName.ReadOnly = false;
                    LongFrenquency.ReadOnly = false;
                    LongDropSpec.ReadOnly = false;
                    LongEntrust.ReadOnly = false;
                    LongItemID.ReadOnly = true;
                    LongDosage.ReadOnly = true;
                    break;
                // 网格选择的说明性医嘱
                case 3:
                    grdLongOrderDeteil.ReadOnly = false;
                    LongExecDeptName.ReadOnly = true;
                    LongItemName.ReadOnly = false;
                    LongDosageUnit.ReadOnly = true;
                    LongChannelName.ReadOnly = true;
                    LongFrenquency.ReadOnly = false;
                    LongDropSpec.ReadOnly = true;
                    LongEntrust.ReadOnly = true;
                    LongItemID.ReadOnly = true;
                    LongDosage.ReadOnly = true;
                    break;
            }
        }

        #endregion

        #region "临时医嘱"

        /// <summary>
        /// 控制临时医嘱网格可编辑状态
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void grdTempOrderDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdTempOrderDetail.CurrentCell != null)
            {
                // 1.同组医嘱除了第一条之外其他数据的频次，用法，嘱托，滴数都不能修改
                // 2.同组医嘱的第一条除医嘱内容、单位、执行科室外都可修改
                // 3.医嘱项目除医嘱内容、单位、执行科室外都可修改
                int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                DataTable orderDt = grdTempOrderDetail.DataSource as DataTable;
                // 已保存的数据在没点修改之前不允许修改
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["SaveFlag"]) == 1)
                {
                    grdTempOrderDetail.ReadOnly = true;
                    return;
                }

                // 如果为自由录入说明性医嘱，只能修改医嘱内容、用法、频次、滴速、嘱托
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["OrderType"]) == 1)
                {
                    SetTempGridReadOnly(3);
                    grdTempOrderDetail.HideSelectionCardWhenCustomInput = true;
                    return;
                }

                // 网格选择的说明性医嘱
                if (Tools.ToInt32(orderDt.Rows[rowIndex]["ItemType"]) == 5)
                {
                    SetLongGridReadOnly(3);
                    grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
                    return;
                }

                grdTempOrderDetail.HideSelectionCardWhenCustomInput = false;
                // 空白数据行除医嘱内容、单位、执行科室外都可修改
                if (orderDt.Rows[rowIndex]["ItemID"].ToString() == "0")
                {
                    SetTempGridReadOnly(0);
                    return;
                }
                else
                {
                    // 判断组号是否为空时，默认为新增的数据，除医嘱内容、单位、执行科室外都可修改
                    if (string.IsNullOrEmpty(orderDt.Rows[rowIndex]["GroupID"].ToString()))
                    {
                        SetTempGridReadOnly(0);
                        return;
                    }
                    else
                    {
                        // 判断是否是组内第一行
                        if (Tools.ToInt32(orderDt.Rows[rowIndex]["IsLast"]) == 1)
                        {
                            // 是组内第一行时，除医嘱内容、单位、执行科室外都可修改
                            SetTempGridReadOnly(0);
                            return;
                        }
                        else
                        {
                            // 不是组内第一行时，只能修改项目Code和剂量
                            SetTempGridReadOnly(1);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 临时医嘱回车新增行
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="colIndex">列号</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="jumpStop">是否往下跳格</param>
        private void grdTempOrderDetail_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
            DataTable longOrderDt = grdTempOrderDetail.DataSource as DataTable;
            // 如果当前选中行已保存在数据库中，则整组数据都不能使用回车键新增行，只能使用右键菜单新增
            if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["ModelDetailID"]) != 0 || rowIndex == 0)
            {
                grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                return;
            }

            // 如果为自由录入的说明性医嘱，只能修改医嘱内容、用法、频次、滴速、嘱托
            if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["OrderType"]) == 1)
            {
                // 当前操作的为剂量单元格
                if (colIndex == TempEntrust.Index)
                {
                    tolAuto_Click(null, null);
                    //grdTempOrderDetail.CurrentCell = this.grdTempOrderDetail[TempItemName.Index, rowIndex + 1];
                }
            }

            // 当前操作的为剂量单元格
            if (colIndex == TempDosage.Index)
            {
                // 判断当前行是否为药品数据
                int itemType = Tools.ToInt32(longOrderDt.Rows[rowIndex]["ItemType"]);
                if (itemType == 1)
                {
                    // 判断是否为同组第一行,如果是第一行则不进行新增行操作
                    if (Tools.ToInt32(longOrderDt.Rows[rowIndex]["IsLast"]) == 1)
                    {
                        grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                        return;
                    }

                    jumpStop = true;
                    AddEmptyRow(rowIndex + 1, true, false);
                    grdTempOrderDetail.CurrentCell = this.grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                    grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                    return;
                }
            }
        }

        /// <summary>
        /// 临时医嘱网格ShowCard数据选择事件
        /// </summary>
        /// <param name="SelectedValue">选中行</param>
        /// <param name="stop">是否停止跳格</param>
        /// <param name="customNextColumnIndex">下一列号</param>
        private void grdTempOrderDetail_SelectCardRowSelected(object SelectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
            DataRow row = (DataRow)SelectedValue;
            int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
            DataTable tempOrderDt = grdTempOrderDetail.DataSource as DataTable;

            if (customNextColumnIndex == TempItemID.Index)
            {
                // 判断同组医嘱中是否存在相同的数据
                int groupID = Tools.ToInt32(tempOrderDt.Rows[rowIndex]["GroupID"]);
                DataRow[] orderDr = tempOrderDt.Select("GroupID = " + groupID);
                int itemID = Tools.ToInt32(tempOrderDt.Rows[rowIndex]["ItemID"]);
                string itemName = tempOrderDt.Rows[rowIndex]["ItemName"].ToString();
                tempOrderDt.Rows[rowIndex]["ItemID"] = row["ItemID"];
                tempOrderDt.Rows[rowIndex]["ItemName"] = row["ItemName"];
                if (orderDr.Length > 1)
                {
                    int num = 0;
                    for (int i = 0; i < orderDr.Length; i++)
                    {
                        if (row["ItemID"].ToString().Contains(orderDr[i]["ItemID"].ToString()))
                        {
                            if (num > 0)
                            {
                                InvokeController("MessageShow", "这一组已经有" + orderDr[i]["ItemName"].ToString() + "药品，请重新选择！");
                                grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex];
                                tempOrderDt.Rows[rowIndex]["ItemID"] = itemID;
                                tempOrderDt.Rows[rowIndex]["ItemName"] = itemName;
                                grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                                return;
                            }
                        }
                    }
                }

                tempOrderDt.Rows[rowIndex]["ItemType"] = row["ItemClass"];
                tempOrderDt.Rows[rowIndex]["ItemID"] = row["ItemID"];
                tempOrderDt.Rows[rowIndex]["ItemName"] = row["ItemName"];
                tempOrderDt.Rows[rowIndex]["ExecDeptId"] = row["ExecDeptId"];
                tempOrderDt.Rows[rowIndex]["ExecDeptName"] = row["ExecDeptName"];
                tempOrderDt.Rows[rowIndex]["StatID"] = row["StatID"];
                tempOrderDt.Rows[rowIndex]["DosageUnit"] = row["DoseUnitName"];
            }
            else if (customNextColumnIndex == TempChannelName.Index)
            {
                tempOrderDt.Rows[rowIndex]["ChannelID"] = row["ID"];
                tempOrderDt.Rows[rowIndex]["ChannelName"] = row["ChannelName"];
            }
            else if (customNextColumnIndex == TempFrenquency.Index)
            {
                tempOrderDt.Rows[rowIndex]["FrenquencyID"] = row["FrequencyID"];
                tempOrderDt.Rows[rowIndex]["Frenquency"] = row["FrequencyName"];
                int itemType = Tools.ToInt32(tempOrderDt.Rows[rowIndex]["ItemType"]);
                // 判断是否存在下一行
                if (tempOrderDt.Rows.Count == rowIndex + 1)
                {
                    // 不存在下一行数据
                    AddEmptyRow(rowIndex + 1, itemType == 1 ? true : false, false);
                    stop = true;
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                }
                else
                {
                    // 判断下一行数据是否为空
                    int itemID = Tools.ToInt32(tempOrderDt.Rows[rowIndex + 1]["ItemID"]);
                    if (itemID == 0)
                    {
                        stop = true;
                        grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                    }
                    else
                    {
                        stop = true;
                        grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, rowIndex + 1];
                    }
                }
            }
            else if (customNextColumnIndex == TempEntrust.Index)
            {
                tempOrderDt.Rows[rowIndex]["Entrust"] = row["EntrustName"];
                int itemType = Tools.ToInt32(tempOrderDt.Rows[rowIndex]["ItemType"]);
                // 判断是否存在下一行
                if (tempOrderDt.Rows.Count == rowIndex + 1)
                {
                    // 不存在下一行数据
                    AddEmptyRow(rowIndex + 1, itemType == 1 ? true : false, false);
                    stop = true;
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                }
                else
                {
                    // 判断下一行数据是否为空
                    int itemID = Tools.ToInt32(tempOrderDt.Rows[rowIndex + 1]["ItemID"]);
                    if (itemID == 0)
                    {
                        stop = true;
                        grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                    }
                    else
                    {
                        stop = true;
                        grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, rowIndex + 1];
                    }
                }
            }

            grdTempOrderDetail.Refresh();
            grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
        }

        /// <summary>
        /// 设置临时医嘱网格列编辑状态
        /// </summary>
        /// <param name="readOnlyStatus">编辑状态</param>
        private void SetTempGridReadOnly(int readOnlyStatus)
        {
            switch (readOnlyStatus)
            {
                // 点击按钮新开或者回车新开非同组医嘱
                case 0:
                    grdTempOrderDetail.ReadOnly = false;
                    TempItemID.ReadOnly = false; // 编码
                    TempDosage.ReadOnly = false;  // 剂量
                    TempChannelName.ReadOnly = false; // 用法
                    TempFrenquency.ReadOnly = false;  // 频次
                    TempDropSpec.ReadOnly = false;  // 滴速
                    TempEntrust.ReadOnly = false;  // 嘱托
                    TempDosageUnit.ReadOnly = true;  // 单位
                    TempItemName.ReadOnly = true;  // 医嘱内容
                    TempExecDeptName.ReadOnly = true;  // 执行科室
                    break;
                // 同组医嘱回车新开
                case 1:
                    grdTempOrderDetail.ReadOnly = false;
                    TempItemID.ReadOnly = false; // 编码
                    TempDosage.ReadOnly = false;  // 剂量
                    TempChannelName.ReadOnly = true; // 用法
                    TempFrenquency.ReadOnly = true;  // 频次
                    TempDropSpec.ReadOnly = true;  // 滴速
                    TempEntrust.ReadOnly = true;  // 嘱托
                    TempDosageUnit.ReadOnly = true;  // 单位
                    TempItemName.ReadOnly = true;  // 医嘱内容
                    TempExecDeptName.ReadOnly = true;  // 执行科室
                    break;
                // 说明性医嘱
                case 2:
                    grdTempOrderDetail.ReadOnly = false;
                    TempItemID.ReadOnly = true;
                    TempDosage.ReadOnly = true;
                    TempChannelName.ReadOnly = false;
                    TempFrenquency.ReadOnly = false;
                    TempDropSpec.ReadOnly = false;
                    TempEntrust.ReadOnly = false;
                    TempDosageUnit.ReadOnly = true;
                    TempItemName.ReadOnly = false;
                    TempExecDeptName.ReadOnly = true;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 新增数据行过滤网格数据
        /// </summary>
        /// <param name="isEnter">是否是回车新增(true：按下回车键新增/false:点击新增按钮新增)</param>
        /// <param name="filterDt">药品项目数据源</param>
        private void AddRowFilterShowCardData(bool isEnter, DataTable filterDt)
        {
            // 按下回车键新增
            if (isEnter)
            {
                DataTable orderDt = new DataTable();
                int rowIndex = 0;
                if (tabControl1.SelectedTabIndex == 0)
                {
                    rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                    orderDt = grdLongOrderDeteil.DataSource as DataTable;
                }
                else
                {
                    rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                    orderDt = grdTempOrderDetail.DataSource as DataTable;
                }

                if (rowIndex == -1)
                {
                    rowIndex = 0;
                }
                else
                {
                    // 当前数据是否为空白数据，如果是空白数据则取上一行数据的rowIndex
                    if (string.IsNullOrEmpty(orderDt.Rows[rowIndex]["ItemName"].ToString()))
                    {
                        if (rowIndex != 0)
                        {
                            rowIndex = rowIndex - 1;
                        }
                    }
                }
                // 上一行数据的项目ID
                string itemID = string.Empty;
                string groupID = orderDt.Rows[rowIndex]["GroupID"].ToString();
                DataRow[] itemDr = orderDt.Select("GroupID = " + groupID);
                if (itemDr.Length > 0)
                {
                    for (int i = 0; i < itemDr.Length; i++)
                    {
                        itemID += itemDr[i]["ItemID"].ToString() + ',';
                    }

                    itemID = itemID.Substring(0, itemID.Length - 1);
                }

                int itemType = Tools.ToInt32(orderDt.Rows[rowIndex]["ItemType"]);

                if (string.IsNullOrEmpty(itemID) && itemType == 0)
                {
                    return;
                }
                // 上一行数据的大项目ID
                int statid = Tools.ToInt32(orderDt.Rows[rowIndex]["StatID"]);
                // 从源数据中克隆表结构
                DataTable tempOrder = mShowCardLongDrugList.Clone();
                tempOrder.Clear();
                DataRow[] orderRows;
                // 药品数据
                if (statid == 100 || statid == 101)
                {
                    if (filterDt != null)
                    {
                        orderRows = filterDt.Select(" StatID IN (100,101) AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                    }
                    else
                    {
                        if (tabControl1.SelectedTabIndex == 0)
                        {
                            orderRows = mShowCardLongDrugList.Select(" StatID IN (100,101) AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                        }
                        else
                        {
                            orderRows = mShowCardTempDrugList.Select(" StatID IN (100,101) AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                        }
                    }
                }
                else if (statid == 102)
                {
                    if (filterDt != null)
                    {
                        // 中草药
                        orderRows = filterDt.Select(" StatID =102 AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                    }
                    else
                    {
                        // 中草药
                        if (tabControl1.SelectedTabIndex == 0)
                        {
                            orderRows = mShowCardLongDrugList.Select(" StatID =102 AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                        }
                        else
                        {
                            orderRows = mShowCardTempDrugList.Select(" StatID =102 AND ItemID NOT IN ( " + itemID + ")", string.Empty);
                        }
                    }
                }
                else
                {
                    if (filterDt != null)
                    {
                        // 药品、项目和材料
                        orderRows = filterDt.Select(" ItemClass=" + itemType + " AND ItemID NOT IN (" + itemID + ")");
                    }
                    else
                    {
                        // 中草药
                        if (tabControl1.SelectedTabIndex == 0)
                        {
                            // 药品、项目和材料
                            orderRows = mShowCardLongDrugList.Select(" ItemClass=" + itemType + " AND ItemID NOT IN (" + itemID + ")");
                        }
                        else
                        {
                            // 药品、项目和材料
                            orderRows = mShowCardTempDrugList.Select(" ItemClass=" + itemType + " AND ItemID NOT IN (" + itemID + ")");
                        }
                    }
                }

                foreach (DataRow dr in orderRows)
                {
                    tempOrder.Rows.Add(dr.ItemArray);
                }

                if (tabControl1.SelectedTabIndex == 0)
                {
                    grdLongOrderDeteil.BindSelectionCardDataSource(0, tempOrder);
                    mShowCardLongDrugList.Rows.Clear();
                    mShowCardLongDrugList.Merge(tempOrder);
                }
                else
                {
                    grdTempOrderDetail.BindSelectionCardDataSource(0, tempOrder);
                    mShowCardTempDrugList.Rows.Clear();
                    mShowCardTempDrugList.Merge(tempOrder);
                }
            }
            else
            {
                // 按下新增按钮时，默认加载所有数据
                if (tabControl1.SelectedTabIndex == 0)
                {
                    grdLongOrderDeteil.BindSelectionCardDataSource(0, mLongItemDrugList);
                    mShowCardLongDrugList.Rows.Clear();
                    mShowCardLongDrugList.Merge(mLongItemDrugList);
                }
                else
                {
                    grdTempOrderDetail.BindSelectionCardDataSource(0, mTempItemDrugList);
                    mShowCardTempDrugList.Rows.Clear();
                    mShowCardTempDrugList.Merge(mTempItemDrugList);
                }
            }
        }

        /// <summary>
        /// 新增空白行
        /// </summary>
        /// <param name="insertRowIndex">插入空白行的位置</param>
        /// <param name="insertType">是否是回车新增(true：按下回车键新增/false:点击新增按钮新增)</param>
        /// <param name="isAuto">是否自由录入（false:否true:是）</param>
        private void AddEmptyRow(int insertRowIndex, bool insertType, bool isAuto)
        {
            AddRowFilterShowCardData(insertType, null);
            DataTable tempDt;
            if (tabControl1.SelectedTabIndex == 0)
            {
                tempDt = grdLongOrderDeteil.DataSource as DataTable;
            }
            else
            {
                tempDt = grdTempOrderDetail.DataSource as DataTable;
            }

            DataRow orderDr = tempDt.NewRow();
            orderDr["ModelDetailID"] = 0;
            orderDr["ModelHeadID"] = mOrderModelHead.ModelHeadID;
            orderDr["OrderCategory"] = tabControl1.SelectedTabIndex;

            // 获取时间戳，用来作为唯一组号
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            int timesGroupID = Convert.ToInt32(ts.TotalSeconds);
            if (insertType)
            {
                if (insertRowIndex == 0)
                {
                    // 第一行数据
                    orderDr["GroupID"] = timesGroupID;
                    orderDr["IsLast"] = 1;
                }
                else
                {
                    // 取得上一行数据的类型、组号ID
                    // 上一行数据的大项目ID
                    int statid = Tools.ToInt32(tempDt.Rows[insertRowIndex - 1]["StatID"]);
                    int groupID = Tools.ToInt32(tempDt.Rows[insertRowIndex - 1]["GroupID"]);
                    // 如果为药品则同组
                    if (statid == 100 || statid == 101 || statid == 102)
                    {
                        orderDr["GroupID"] = groupID;
                        orderDr["IsLast"] = 0;
                    }
                    else
                    {
                        orderDr["GroupID"] = timesGroupID;
                        orderDr["IsLast"] = 1;
                    }
                }
            }
            else
            {
                // 点击按钮新开
                orderDr["GroupID"] = timesGroupID;
                orderDr["IsLast"] = 1;
            }

            orderDr["ItemName"] = string.Empty;
            orderDr["DosageUnit"] = string.Empty;
            orderDr["Factor"] = 0;
            orderDr["ChannelName"] = string.Empty;
            orderDr["ChannelID"] = 0;
            orderDr["FrenquencyID"] = 0;
            orderDr["Frenquency"] = string.Empty;
            orderDr["DoseNum"] = 0;
            orderDr["Amount"] = 0;
            orderDr["Unit"] = string.Empty;
            orderDr["UnitNO"] = 0;
            orderDr["Entrust"] = string.Empty;
            orderDr["FirstNum"] = 0;
            orderDr["ExecDeptId"] = 0;
            orderDr["ExamItemID"] = 0;
            orderDr["Spec"] = string.Empty;
            orderDr["Flag"] = 0;
            orderDr["StatID"] = 0;
            if (isAuto)
            {
                orderDr["OrderType"] = 1;
                orderDr["ItemType"] = 5;
            }
            else
            {
                orderDr["OrderType"] = 0;
                //OrderDr["ItemID"] = 0;
                //OrderDr["Dosage"] = 0;
                //OrderDr["DropSpec"] = 0;
                orderDr["ItemType"] = 0;
            }

            orderDr["SaveFlag"] = 0;
            tempDt.Rows.InsertAt(orderDr, insertRowIndex);
        }

        /// <summary>
        /// 插入一行数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolInsert_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                if (grdLongOrderDeteil.CurrentCell != null)
                {
                    grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
                    int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                    DataTable orderDt = grdLongOrderDeteil.DataSource as DataTable;
                    grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
                    // 获取当前选中行的项目类型
                    int itemType = Tools.ToInt32(orderDt.Rows[rowIndex]["ItemType"]);

                    // 药品项目
                    if (itemType == 1)
                    {
                        // 判断下一行数据是否存在。
                        if (rowIndex < orderDt.Rows.Count - 1)
                        {
                            // 判断下一行数据是否选中了数据
                            if (Tools.ToInt32(orderDt.Rows[rowIndex]["ItemID"]) > 0)
                            {
                                // 下一行数据已选中数据，则新增一行
                                AddEmptyRow(rowIndex + 1, true, false);
                            }
                        }
                        else
                        {
                            // 当前操作的是最后一行数据，直接在同组下新增一行
                            AddEmptyRow(rowIndex + 1, true, false);
                        }
                    }
                    else
                    {
                        // 非药品项目
                        // 判断是否存在下一行
                        if (rowIndex >= orderDt.Rows.Count - 1)
                        {
                            // 下一行数据已选中数据，则新增一行
                            AddEmptyRow(rowIndex + 1, false, false);
                        }
                    }
                    // 选中下一行的项目ID列弹出ShowCard网格
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, rowIndex + 1];
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, rowIndex + 1];
                    grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                }
            }
            else
            {
                if (grdTempOrderDetail.CurrentCell != null)
                {
                    grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
                    int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                    DataTable orderDt = grdTempOrderDetail.DataSource as DataTable;
                    grdTempOrderDetail.HideSelectionCardWhenCustomInput = false;
                    // 获取当前选中行的项目类型
                    int itemType = Tools.ToInt32(orderDt.Rows[rowIndex]["ItemType"]);

                    // 药品项目
                    if (itemType == 1)
                    {
                        // 判断下一行数据是否存在。
                        if (rowIndex < orderDt.Rows.Count - 1)
                        {
                            // 判断下一行数据是否选中了数据
                            if (Tools.ToInt32(orderDt.Rows[rowIndex]["ItemID"]) > 0)
                            {
                                // 下一行数据已选中数据，则新增一行
                                AddEmptyRow(rowIndex + 1, true, false);
                            }
                        }
                        else
                        {
                            // 当前操作的是最后一行数据，直接在同组下新增一行
                            AddEmptyRow(rowIndex + 1, true, false);
                        }
                    }
                    else
                    {
                        // 非药品项目
                        // 判断是否存在下一行
                        if (rowIndex >= orderDt.Rows.Count - 1)
                        {
                            // 下一行数据已选中数据，则新增一行
                            AddEmptyRow(rowIndex + 1, false, false);
                        }
                    }
                    // 选中下一行的项目ID列弹出ShowCard网格
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, rowIndex + 1];
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, rowIndex + 1];
                    grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                }
            }

            isEdit = true;
        }

        /// <summary>
        /// 修改当前选中行数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolUpdate_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                if (grdLongOrderDeteil.CurrentCell != null)
                {
                    grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
                    int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                    DataTable orderDt = grdLongOrderDeteil.DataSource as DataTable;
                    orderDt.Rows[rowIndex]["SaveFlag"] = 0;
                    grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                }
            }
            else
            {
                if (grdTempOrderDetail.CurrentCell != null)
                {
                    grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
                    int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                    DataTable orderDt = grdTempOrderDetail.DataSource as DataTable;
                    orderDt.Rows[rowIndex]["SaveFlag"] = 0;
                    grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                }
            }

            isEdit = true;
        }

        /// <summary>
        /// 删除一行数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolDelete_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                if (grdLongOrderDeteil.CurrentCell != null)
                {
                    if (MessageBox.Show("确定要删除选中医嘱明细数据吗！删除后将不可恢复？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        grdLongOrderDeteil.ReadOnly = true;
                        grdLongOrderDeteil.EndEdit();
                        int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                        DataTable orderDt = grdLongOrderDeteil.DataSource as DataTable;
                        int modelDetailID = Tools.ToInt32(orderDt.Rows[rowIndex]["ModelDetailID"]);
                        // 删除选中行数据
                        orderDt.Rows.Remove(orderDt.Rows[rowIndex]);
                        orderDt.AcceptChanges();
                        // 检查数据是否以保存，如果是已保存的数据则从数据库中删除
                        if (modelDetailID > 0)
                        {
                            InvokeController("DelOrderDetailsData", modelDetailID.ToString());
                        }

                        InvokeController("MessageShow", "模板明细数据删除成功！");
                    }
                }
            }
            else
            {
                if (grdTempOrderDetail.CurrentCell != null)
                {
                    if (MessageBox.Show("确定要删除选中医嘱明细数据吗！删除后将不可恢复？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        grdTempOrderDetail.ReadOnly = true;
                        grdTempOrderDetail.EndEdit();
                        int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                        DataTable orderDt = grdTempOrderDetail.DataSource as DataTable;
                        int modelDetailID = Tools.ToInt32(orderDt.Rows[rowIndex]["ModelDetailID"]);
                        // 删除选中行数据
                        orderDt.Rows.Remove(orderDt.Rows[rowIndex]);
                        orderDt.AcceptChanges();
                        // 检查数据是否以保存，如果是已保存的数据则从数据库中删除
                        if (modelDetailID > 0)
                        {
                            InvokeController("DelOrderDetailsData", modelDetailID.ToString());
                        }

                        InvokeController("MessageShow", "模板明细数据删除成功！");
                    }
                }
            }
        }

        /// <summary>
        /// 删除整组数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolDeleteGroup_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                if (grdLongOrderDeteil.CurrentCell != null)
                {
                    if (MessageBox.Show("确定要删除选中医嘱明细数据吗！删除后将不可恢复？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        int rowIndex = grdLongOrderDeteil.CurrentCell.RowIndex;
                        DataTable orderDt = grdLongOrderDeteil.DataSource as DataTable;
                        // 获取当前选中行的整组数据
                        DataRow[] orderDr = orderDt.Select("GroupID = " + orderDt.Rows[rowIndex]["GroupID"].ToString());
                        if (orderDr.Length > 0)
                        {
                            string modelDetailIDArray = string.Empty;
                            // 循环删除数据
                            for (int i = 0; i < orderDr.Length; i++)
                            {
                                if (Tools.ToInt32(orderDr[i]["ModelDetailID"]) > 0)
                                {
                                    modelDetailIDArray += orderDr[i]["ModelDetailID"].ToString() + ",";
                                }

                                orderDt.Rows.Remove(orderDr[i]);
                            }

                            orderDt.AcceptChanges();
                            if (modelDetailIDArray.Length > 0)
                            {
                                modelDetailIDArray = modelDetailIDArray.Substring(0, modelDetailIDArray.Length - 1);
                                InvokeController("DelOrderDetailsData", modelDetailIDArray);
                            }

                            InvokeController("MessageShow", "模板明细数据删除成功！");
                        }
                    }
                }
            }
            else
            {
                if (grdTempOrderDetail.CurrentCell != null)
                {
                    if (MessageBox.Show("确定要删除选中医嘱明细数据吗！删除后将不可恢复？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        int rowIndex = grdTempOrderDetail.CurrentCell.RowIndex;
                        DataTable orderDt = grdTempOrderDetail.DataSource as DataTable;
                        // 获取当前选中行的整组数据
                        DataRow[] orderDr = orderDt.Select("GroupID = " + orderDt.Rows[rowIndex]["GroupID"].ToString());
                        if (orderDr.Length > 0)
                        {
                            string modelDetailIDArray = string.Empty;
                            // 循环删除数据
                            for (int i = 0; i < orderDr.Length; i++)
                            {
                                if (Tools.ToInt32(orderDr[i]["ModelDetailID"]) > 0)
                                {
                                    modelDetailIDArray += orderDr[i]["ModelDetailID"].ToString() + ",";
                                }

                                orderDt.Rows.Remove(orderDr[i]);
                            }

                            orderDt.AcceptChanges();
                            if (modelDetailIDArray.Length > 0)
                            {
                                modelDetailIDArray = modelDetailIDArray.Substring(0, modelDetailIDArray.Length - 1);
                                InvokeController("DelOrderDetailsData", modelDetailIDArray);
                            }

                            InvokeController("MessageShow", "模板明细数据删除成功！");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 刷新网格选项卡数据数据
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolRefresh_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                if (MessageBox.Show("您有医嘱未保存，是否继续刷新药品选项卡？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    return;
                }
            }

            InvokeController("GetDrugMaster");
            // 判断当前正在编辑的医嘱类型
            if (tabControl1.SelectedTabIndex == 0)
            {
                // 长期医嘱
                if (grdLongOrderDeteil.CurrentCell != null)
                {
                    // 根据当前行的大项目ID过滤选中药房后的数据
                    AddRowFilterShowCardData(true, mLongItemDrugList);
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemID.Index, grdLongOrderDeteil.CurrentCell.RowIndex];
                }
                else
                {
                    grdLongOrderDeteil.BindSelectionCardDataSource(0, mLongItemDrugList);
                    mShowCardLongDrugList.Rows.Clear();
                    mShowCardLongDrugList.Merge(mLongItemDrugList);
                }
            }
            else if (tabControl1.SelectedTabIndex == 1)
            {
                // 临时医嘱
                if (grdTempOrderDetail.CurrentCell != null)
                {
                    // 根据当前行的大项目ID过滤选中药房后的数据
                    AddRowFilterShowCardData(true, mTempItemDrugList);
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemID.Index, grdTempOrderDetail.CurrentCell.RowIndex];
                }
                else
                {
                    grdTempOrderDetail.BindSelectionCardDataSource(0, mTempItemDrugList);
                    mShowCardTempDrugList.Rows.Clear();
                    mShowCardTempDrugList.Merge(mTempItemDrugList);
                }
            }
        }

        /// <summary>
        /// 录入说明性医嘱
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void tolAuto_Click(object sender, EventArgs e)
        {
            // 判断是否选中的是医嘱模板
            if (advTempDetails.SelectedNode != null)
            {
                IPD_OrderModelHead iPDOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                if (iPDOrderModelHead.ModelType == 0)
                {
                    // 选中的不是医嘱模板则允许新增
                    InvokeController("MessageShow", "请选择医嘱模板！");
                    return;
                }
            }
            else
            {
                // 没有选中模板数据
                return;
            }

            // 长期医嘱
            if (tabControl1.SelectedTabIndex == 0)
            {
                grdLongOrderDeteil.CurrentCellChanged -= grdLongOrderDeteil_CurrentCellChanged;
                grdLongOrderDeteil.HideSelectionCardWhenCustomInput = true;
                SetLongGridReadOnly(2);
                DataTable dt = grdLongOrderDeteil.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    AddEmptyRow(dt.Rows.Count, false, true);
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, dt.Rows.Count - 1];
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemName.Index, dt.Rows.Count - 1];
                }
                else
                {
                    if (grdLongOrderDeteil[LongItemName.Name, grdLongOrderDeteil.Rows.Count - 1].Value.ToString() == string.Empty)
                    {
                        //grdLongOrderDeteil.HideSelectionCardWhenCustomInput = true;
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                        dt.AcceptChanges();
                    }

                    AddEmptyRow(dt.Rows.Count, false, true);
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongDosage.Index, dt.Rows.Count - 1];
                    grdLongOrderDeteil.CurrentCell = grdLongOrderDeteil[LongItemName.Index, dt.Rows.Count - 1];
                    grdLongOrderDeteil.CurrentCellChanged += grdLongOrderDeteil_CurrentCellChanged;
                }
            }
            else if (tabControl1.SelectedTabIndex == 1)
            {
                // 临时医嘱
                // 设置网格可用
                grdTempOrderDetail.CurrentCellChanged -= grdTempOrderDetail_CurrentCellChanged;
                grdTempOrderDetail.HideSelectionCardWhenCustomInput = true;
                SetTempGridReadOnly(2);
                DataTable dt = grdTempOrderDetail.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    AddEmptyRow(dt.Rows.Count, false, true);
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, dt.Rows.Count - 1];
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemName.Index, dt.Rows.Count - 1];
                }
                else
                {
                    if (grdTempOrderDetail[TempItemName.Name, grdTempOrderDetail.Rows.Count - 1].Value.ToString() == string.Empty)
                    {
                        dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                        dt.AcceptChanges();
                    }

                    AddEmptyRow(dt.Rows.Count, false, true);
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempDosage.Index, dt.Rows.Count - 1];
                    grdTempOrderDetail.CurrentCell = grdTempOrderDetail[TempItemName.Index, dt.Rows.Count - 1];
                    grdTempOrderDetail.CurrentCellChanged += grdTempOrderDetail_CurrentCellChanged;
                }
            }

            isEdit = true;
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOrderTempManage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnNew_Click(null, null); // 新增模板明细
                    break;
                case Keys.F7:
                    btnUpd_Click(null, null);  // 修改模板明细
                    break;
                case Keys.F8:
                    btnDel_Click(null, null);  // 删除模板明细
                    break;
                case Keys.F9:
                    btnSave_Click(null, null);  // 保存模板明细
                    break;
                case Keys.F10:
                    btnRefresh_Click(null, null);  // 刷新模板明细
                    break;
                case Keys.Insert:
                    if ((int)e.Modifiers != ((int)Keys.Control))
                    {
                        tolInsert_Click(null, null);
                    }

                    break;
                case Keys.Delete:
                    if ((int)e.Modifiers != ((int)Keys.Control))
                    {
                        tolDelete_Click(null, null);
                    }

                    break;
                default:
                    break;
            }
            // 自由录入
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.Insert)
            {
                tolAuto_Click(null, null);
            }
            // 删除一组
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.Delete)
            {
                tolDeleteGroup_Click(null, null);
            }
            // 修改当前行
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.F2)
            {
                tolUpdate_Click(null, null);
            }
            // 刷新药品选项卡数据源
            if ((int)e.Modifiers == ((int)Keys.Control) && e.KeyCode == Keys.F5)
            {
                tolRefresh_Click(null, null);
            }
        }

        /// <summary>
        /// 长嘱网格CellValueChanged事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void grdLongOrderDeteil_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == LongItemID.Index)
            {
                grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
            }
        }
    }
}
