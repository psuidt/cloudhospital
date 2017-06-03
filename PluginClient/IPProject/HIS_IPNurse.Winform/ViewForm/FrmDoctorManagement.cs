using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 医嘱转抄
    /// </summary>
    public partial class FrmDoctorManagement : BaseFormBusiness, IDoctorManagement
    {
        /// <summary>
        /// 未转抄列表
        /// </summary>
        private DataTable notCopiedDocDt = new DataTable();

        /// <summary>
        /// 已转抄列表
        /// </summary>
        private DataTable hasBeenCopiedDocDt = new DataTable();

        /// <summary>
        /// 全选反选开关
        /// </summary>
        private bool isAllCheck = false;

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptID
        {
            get
            {
                if (txtDeptList.MemberValue != null)
                {
                    return Convert.ToInt32(txtDeptList.MemberValue.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 医嘱类别(长期/临时)
        /// </summary>
        public string OrderCategory
        {
            get
            {
                return chkLong.Checked == chkTemp.Checked ? "-1" : (chkLong.Checked ? "0" : "1");
            }
        }

        /// <summary>
        /// 医嘱类型(新开/新停)
        /// </summary>
        public string OrderStatus
        {
            get
            {
                return chkNewOpen.Checked == chkNewStop.Checked ? "0" : (chkNewOpen.Checked ? "1" : "2");
            }
        }

        /// <summary>
        /// 皮试医嘱
        /// </summary>
        public string AstFlag
        {
            get
            {
                if (chkALL.Checked)
                {
                    return string.Empty;
                }
                else
                {
                    string mAstFlag = "0,1,2";
                    if (!chkSkinTest.Checked)
                    {
                        mAstFlag = string.Empty;
                    }

                    return mAstFlag;
                }
            }
        }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        private int mPatListID = 0;

        /// <summary>
        /// 病人登记ID
        /// </summary>
        public int PatListID
        {
            get
            {
                if (tabControl2.SelectedTabIndex == 0)
                {
                    mPatListID = 0;
                    if (grdDoctorList.CurrentCell != null)
                    {
                        int rowIndex = grdDoctorList.CurrentCell.RowIndex;
                        DataTable docListDt = grdDoctorList.DataSource as DataTable;
                        mPatListID = Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]);
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    mPatListID = 0;
                    if (grdHasBeenCopiedDocList.CurrentCell != null)
                    {
                        int rowIndex = grdHasBeenCopiedDocList.CurrentCell.RowIndex;
                        DataTable docListDt = grdHasBeenCopiedDocList.DataSource as DataTable;
                        mPatListID = Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]);
                    }
                }

                return mPatListID;
            }
        }

        /// <summary>
        /// 组号ID
        /// </summary>
        private int mGroupID = 0;

        /// <summary>
        /// 医嘱分组ID
        /// </summary>
        public int GroupID
        {
            get
            {
                if (tabControl2.SelectedTabIndex == 0)
                {
                    mGroupID = 0;
                    if (grdDoctorList.CurrentCell != null)
                    {
                        int rowIndex = grdDoctorList.CurrentCell.RowIndex;
                        DataTable docListDt = grdDoctorList.DataSource as DataTable;
                        mGroupID = Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]);
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    mGroupID = 0;
                    if (grdHasBeenCopiedDocList.CurrentCell != null)
                    {
                        int rowIndex = grdHasBeenCopiedDocList.CurrentCell.RowIndex;
                        DataTable docListDt = grdHasBeenCopiedDocList.DataSource as DataTable;
                        mGroupID = Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]);
                    }
                }

                return mGroupID;
            }
        }

        /// <summary>
        /// 转抄标志
        /// </summary>
        public bool IsTrans
        {
            get
            {
                if (tabControl2.SelectedTabIndex == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDoctorManagement()
        {
            InitializeComponent();
            grdDoctorList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintGroupLine);
            grdHasBeenCopiedDocList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(HasBeenDocPaintGroupLine);
        }

        /// <summary>
        /// 窗体打开前加载事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmDoctorManagement_OpenWindowBefore(object sender, EventArgs e)
        {
            chkALL.Checked = true;
            InvokeController("GetDeptList");
            bindGridSelectIndex(grdPatList);
            bindGridSelectIndex(grdDoctorList);
            bindGridSelectIndex(grdHasBeenCopiedDocList);
            if (tabControl2.SelectedTabIndex == 0)
            {
                btnCopied.Enabled = true;
                btnSend.Enabled = true;
                btnCancelCopied.Enabled = false;
            }
            else
            {
                btnCopied.Enabled = false;
                btnSend.Enabled = false;
                btnCancelCopied.Enabled = true;
            }

            InitSkinTest();

            // 打开界面加载病人列表和医嘱列表
            btnSelect_Click(null, null);
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCalcel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询未转抄医嘱列表以及病人列表
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            InvokeController("GetDocList");
        }

        /// <summary>
        /// 选择病人过滤未转抄医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = grdPatList.CurrentCell.RowIndex;
            DataTable dt = grdPatList.DataSource as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFlg"]) == 1)
                {
                    dt.Rows[rowIndex]["CheckFlg"] = 0;

                    // 从医嘱列表中去掉对应的数据
                    if (tabControl2.SelectedTabIndex == 0)
                    {
                        DataTable commandDt = grdDoctorList.DataSource as DataTable;
                        commandDt.TableName = "DocList";
                        DataView view = new DataView(commandDt);
                        string sqlWhere = string.Format("PatListId <> {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.RowFilter = sqlWhere;
                        view.Sort = "PatListId, GroupID ASC";
                        grdDoctorList.DataSource = view.ToTable();
                    }
                    else if (tabControl2.SelectedTabIndex == 1)
                    {
                        DataTable commandDt = grdHasBeenCopiedDocList.DataSource as DataTable;
                        commandDt.TableName = "DocList";
                        DataView view = new DataView(commandDt);
                        string sqlWhere = string.Format("PatListId <> {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.RowFilter = sqlWhere;
                        view.Sort = "PatListId, GroupID ASC";
                        grdHasBeenCopiedDocList.DataSource = view.ToTable();
                    }

                    // 存在一条不选中的数据，则将全选按钮去掉勾选
                    isAllCheck = true;
                    cbPatList.Checked = false;
                    isAllCheck = false;
                }
                else
                {
                    dt.Rows[rowIndex]["CheckFlg"] = 1;
                    // 将对应病人的数据追加到医嘱列表中
                    if (tabControl2.SelectedTabIndex == 0)
                    {
                        notCopiedDocDt.TableName = "DocList";
                        DataView view = new DataView(notCopiedDocDt);
                        view.RowFilter = string.Format("PatListId = {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.Sort = "PatListId, GroupID ASC";
                        DataTable commandDt = grdDoctorList.DataSource as DataTable;
                        DataTable tempDt = view.ToTable();
                        for (int i = 0; i < tempDt.Rows.Count; i++)
                        {
                            tempDt.Rows[i]["CheckFlg"] = 1;
                        }

                        commandDt.Merge(tempDt);
                    }
                    else if (tabControl2.SelectedTabIndex == 1)
                    {
                        notCopiedDocDt.TableName = "DocList";
                        DataView view = new DataView(hasBeenCopiedDocDt);
                        view.RowFilter = string.Format("PatListId = {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.Sort = "PatListId, GroupID ASC";
                        DataTable commandDt = grdHasBeenCopiedDocList.DataSource as DataTable;
                        DataTable tempDt = view.ToTable();
                        for (int i = 0; i < tempDt.Rows.Count; i++)
                        {
                            tempDt.Rows[i]["CheckFlg"] = 1;
                        }

                        commandDt.Merge(tempDt);
                    }

                    // 判断是否勾选了所有数据
                    DataRow[] patDr = dt.Select("CheckFlg=0");
                    if (patDr.Length <= 0)
                    {
                        isAllCheck = true;
                        cbPatList.Checked = true;
                        isAllCheck = false;
                    }
                }
            }
            // 清空医嘱关联费用数据
            orderFee1.LoadOrderFeeGridData(-1, -1, -1, string.Empty);
        }

        /// <summary>
        /// 未转抄医嘱选中事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdDoctorList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdDoctorList.CurrentCell != null)
            {
                int rowIndex = grdDoctorList.CurrentCell.RowIndex;
                DataTable docListDt = grdDoctorList.DataSource as DataTable;
                if (e.ColumnIndex == 0)
                {
                    if (docListDt != null && docListDt.Rows.Count > 0)
                    {
                        string strWhere = string.Format(
                            "PatListID={0} AND GroupID={1}",
                                Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]),
                                Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]));
                        DataRow[] arrayDr = docListDt.Select(strWhere);
                        // 去掉选中
                        if (Convert.ToInt32(docListDt.Rows[rowIndex]["CheckFlg"]) == 1)
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["CheckFlg"] = 0;
                                }

                                // 去掉全选按钮的勾选
                                isAllCheck = true;
                                chkDoctorList.Checked = false;
                                isAllCheck = false;
                            }
                        }
                        else
                        {
                            // 勾选数据
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["CheckFlg"] = 1;
                                }

                                // 如果所有数据都已勾选则默认勾选全选
                                DataRow[] docDt = docListDt.Select("CheckFlg=0");
                                if (docDt.Length <= 0)
                                {
                                    isAllCheck = true;
                                    chkDoctorList.Checked = true;
                                    isAllCheck = false;
                                }
                            }
                        }
                    }
                }

                mPatListID = Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]);
                mGroupID = Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]);
                if (mGroupID != orderFee1.GroupID)
                {
                    orderFee1.LoadOrderFeeGridData(mPatListID, mGroupID, Tools.ToInt32(txtDeptList.MemberValue.ToString()), txtDeptList.Text);
                }
            }
        }

        /// <summary>
        /// 已转抄医嘱选中事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdHasBeenCopiedDocList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdHasBeenCopiedDocList.CurrentCell != null)
            {
                int rowIndex = grdHasBeenCopiedDocList.CurrentCell.RowIndex;
                DataTable docListDt = grdHasBeenCopiedDocList.DataSource as DataTable;
                if (e.ColumnIndex == 0)
                {
                    if (docListDt != null && docListDt.Rows.Count > 0)
                    {
                        string strWhere = string.Format(
                            "PatListID={0} AND GroupID={1}",
                                Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]),
                                Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]));
                        DataRow[] arrayDr = docListDt.Select(strWhere);
                        // 去掉选中
                        if (Convert.ToInt32(docListDt.Rows[rowIndex]["CheckFlg"]) == 1)
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["CheckFlg"] = 0;
                                }
                            }

                            // 去掉全选按钮的勾选
                            isAllCheck = true;
                            chkHasBeenCopiedDocList.Checked = false;
                            isAllCheck = false;
                        }
                        else
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["CheckFlg"] = 1;
                                }
                            }

                            // 如果所有数据都已勾选则默认勾选全选
                            DataRow[] docDt = docListDt.Select("CheckFlg=0");
                            if (docDt.Length <= 0)
                            {
                                isAllCheck = true;
                                chkHasBeenCopiedDocList.Checked = true;
                                isAllCheck = false;
                            }
                        }
                    }
                }

                mPatListID = Convert.ToInt32(docListDt.Rows[rowIndex]["PatListID"]);
                mGroupID = Convert.ToInt32(docListDt.Rows[rowIndex]["GroupID"]);
                if (mGroupID != orderFee1.GroupID)
                {
                    // 获取医嘱关联的费用数据
                    orderFee1.LoadOrderFeeGridData(mPatListID, mGroupID, Tools.ToInt32(txtDeptList.MemberValue.ToString()), txtDeptList.Text);
                }
            }
        }

        #region "CheckBox事件"

        /// <summary>
        /// 勾选全部
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkALL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkALL.Checked)
            {
                chkLong.Checked = false;
                chkTemp.Checked = false;
                chkNewOpen.Checked = false;
                chkNewStop.Checked = false;
                chkSkinTest.Checked = false;
            }
        }

        /// <summary>
        /// 勾选长期医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkLong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLong.Checked)
            {
                chkALL.Checked = false;
            }
            else
            {
                if (!chkTemp.Checked &&
                    !chkNewOpen.Checked &&
                    !chkNewStop.Checked &&
                    !chkSkinTest.Checked)
                {
                    chkALL.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选临时医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkTemp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTemp.Checked)
            {
                chkALL.Checked = false;
            }
            else
            {
                if (!chkLong.Checked &&
                    !chkNewOpen.Checked &&
                    !chkNewStop.Checked &&
                    !chkSkinTest.Checked)
                {
                    chkALL.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选新开医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkNewOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNewOpen.Checked)
            {
                chkALL.Checked = false;
            }
            else
            {
                if (!chkLong.Checked &&
                    !chkTemp.Checked &&
                    !chkNewStop.Checked &&
                    !chkSkinTest.Checked)
                {
                    chkALL.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选新停医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkNewStop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNewStop.Checked)
            {
                chkALL.Checked = false;
            }
            else
            {
                if (!chkLong.Checked &&
                    !chkTemp.Checked &&
                    !chkNewOpen.Checked &&
                    !chkSkinTest.Checked)
                {
                    chkALL.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选皮试医嘱
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkSkinTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSkinTest.Checked)
            {
                chkALL.Checked = false;
            }
            else
            {
                if (!chkLong.Checked &&
                    !chkTemp.Checked &&
                    !chkNewOpen.Checked &&
                    !chkNewStop.Checked)
                {
                    chkALL.Checked = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptID">默认科室ID</param>
        public void bind_DeptList(DataTable deptDt, int deptID)
        {
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDeptList.ShowCardWidth = 350;
            txtDeptList.ShowCardDataSource = deptDt;
            if (deptID > 0)
            {
                // 检查当前用户所属科室是否存在住院科室列表中
                DataRow[] dr = deptDt.Select(string.Format("DeptId={0}", deptID));
                if (dr.Length > 0)
                {
                    txtDeptList.MemberValue = deptID;
                }
                else
                {
                    // 如果不存在住院科室列表中，则默认选择第一条数据
                    txtDeptList.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }
            else
            {
                if (deptDt != null && deptDt.Rows.Count > 0)
                {
                    txtDeptList.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }

            bind_DeptInfo(deptDt, deptID);
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        public void bind_PatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
            setGridSelectIndex(grdPatList);
        }

        /// <summary>
        /// 绑定医嘱列表
        /// </summary>
        /// <param name="docListDt">医嘱列表</param>
        public void bind_DocList(DataTable docListDt)
        {
            // 合并同组部分数据数据
            if (docListDt.Rows.Count > 0)
            {
                int tempGroupID = 0;
                for (int i = 0; i < docListDt.Rows.Count; i++)
                {
                    if (Convert.ToDateTime(docListDt.Rows[i]["EOrderDate"]).ToString("yyyy-MM-dd").Contains("1900-01-01"))
                    {
                        docListDt.Rows[i]["EOrderDate"] = DBNull.Value;
                    }

                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(docListDt.Rows[i]["GroupID"]);
                        continue;
                    }

                    if (Convert.ToInt32(docListDt.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        docListDt.Rows[i]["OrderCategory"] = DBNull.Value;
                        docListDt.Rows[i]["OrderStatus"] = DBNull.Value;
                        docListDt.Rows[i]["BedNo"] = DBNull.Value;
                        docListDt.Rows[i]["PatName"] = DBNull.Value;
                        docListDt.Rows[i]["SerialNumber"] = DBNull.Value;
                        docListDt.Rows[i]["OrderBdate"] = DBNull.Value;
                        docListDt.Rows[i]["DocName"] = DBNull.Value;
                        docListDt.Rows[i]["ChannelName"] = DBNull.Value;
                        docListDt.Rows[i]["Frequency"] = DBNull.Value;
                        docListDt.Rows[i]["EOrderDate"] = DBNull.Value;
                    }

                    tempGroupID = Convert.ToInt32(docListDt.Rows[i]["GroupID"]);
                }
            }

            if (tabControl2.SelectedTabIndex == 0)
            {
                grdDoctorList.DataSource = docListDt;
                notCopiedDocDt = docListDt;
                setGridSelectIndex(grdDoctorList);
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                grdHasBeenCopiedDocList.DataSource = docListDt;
                hasBeenCopiedDocDt = docListDt;
                setGridSelectIndex(grdHasBeenCopiedDocList);
            }

            if (docListDt.Rows.Count > 0)
            {
                mPatListID = Convert.ToInt32(docListDt.Rows[0]["PatListID"]);
                mGroupID = Convert.ToInt32(docListDt.Rows[0]["GroupID"]);
                // 获取医嘱关联的费用数据
                //InvokeController("GetPatDocRelationFeeList");
                orderFee1.LoadOrderFeeGridData(mPatListID, mGroupID, Tools.ToInt32(txtDeptList.MemberValue.ToString()), txtDeptList.Text);
            }
        }

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        /// <param name="FeeDt">费用列表</param>
        //public void bind_SimpleFeeItemData(DataTable FeeDt)
        //{
        // 长期医嘱
        //grdFeeList.SelectionCards[0].BindColumnIndex = ItemID.Index;
        //grdFeeList.SelectionCards[0].CardColumn = "ItemCode|编码|100,ItemName|项目名称|150,UnitPrice|单价|80,StoreAmount|库存数|80,ExecDeptName|执行科室|auto";
        //grdFeeList.SelectionCards[0].CardSize = new System.Drawing.Size(580, 280);
        //grdFeeList.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
        //grdFeeList.BindSelectionCardDataSource(0, FeeDt);
        //}

        /// <summary>
        /// 绑定医嘱关联费用列表
        /// </summary>
        /// <param name="PatDocRelationFeeList"></param>
        //public void bind_PatDocRelationFeeList(DataTable PatDocRelationFeeList)
        //{
        //    grdFeeList.DataSource = PatDocRelationFeeList;
        //    grdFeeList.EndEdit();
        //    SetOrderDetailstGridState = true;
        //}

        /// <summary>
        /// 新增医嘱费用
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    if (grdDoctorList.CurrentCell != null)
        //    {
        //        // 设置费用网格为可编辑状态
        //        SetOrderDetailstGridState = false;
        //        // 新增医嘱费用
        //        grdFeeList.AddRow();
        //        DataTable dt = grdFeeList.DataSource as DataTable;
        //    }
        //}

        /// <summary>
        /// 修改医嘱费用
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void btnUpd_Click(object sender, EventArgs e)
        //{
        //// 设置费用网格为可编辑状态
        ////SetOrderDetailstGridState = false;
        //}

        /// <summary>
        /// 删除医嘱费用
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void btnDel_Click(object sender, EventArgs e)
        //{
        //    if (grdFeeList.CurrentCell != null)
        //    {
        //        // 询问是否确认删除
        //        if (MessageBox.Show("确定要删除选中费用吗！删除后将不可恢复？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            // 设置网格不可编辑
        //            SetOrderDetailstGridState = true;
        //            grdFeeList.EndEdit();
        //            // 取得选中的数据进行删除
        //            int rowIndex = grdFeeList.CurrentCell.RowIndex;
        //            DataTable FeeList = grdFeeList.DataSource as DataTable;
        //            int GenerateID = 0;
        //            if (!string.IsNullOrEmpty(FeeList.Rows[rowIndex]["GenerateID"].ToString()))
        //            { 
        //                GenerateID = Convert.ToInt32(FeeList.Rows[rowIndex]["GenerateID"]);
        //            }
        //            FeeList.Rows.RemoveAt(rowIndex);
        //            // 判断当前选中的数据是否存在数据库中
        //            if (GenerateID > 0)
        //            {
        //                InvokeController("DelFeeItemData", GenerateID);
        //            }
        //            InvokeController("MessageShow", "医嘱费用删除成功！");
        //        }
        //    }
        //    //int rowIndex=
        //    //DataTable FeeList = grdFeeList.DataSource as DataTable;
        //    //if (FeeList != null && FeeList.Rows.Count > 0)
        //    //{
        //    //    bool IsDel = false;
        //    //    for (int i = 0; i < FeeList.Rows.Count; i++)
        //    //    {
        //    //        // 判断当前数据行是否已被勾选
        //    //        if (!string.IsNullOrEmpty(FeeList.Rows[i]["CheckFlg"].ToString()))
        //    //        {
        //    //            if (int.Parse(FeeList.Rows[i]["CheckFlg"].ToString()) == 1)
        //    //            {
        //    //                IsDel = true;
        //    //                break;
        //    //                //TempDt.Rows.Add(FeeList.Rows[i].ItemArray);
        //    //            }
        //    //        }
        //    //    }
        //    //    // 没有选择任何需要删除的费用数据时提示Msg
        //    //    if (!IsDel)
        //    //    {
        //    //        InvokeController("MessageShow", "请选择需要删除的费用数据！");
        //    //        return;
        //    //    }
        //    //    InvokeController("DelFeeItemData", FeeList);
        //    //}
        //}

        /// <summary>
        /// 保存医嘱费用
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    SetOrderDetailstGridState = true;
        //    grdFeeList.EndEdit();
        //    DataTable FeeDt = grdFeeList.DataSource as DataTable;
        //    if (FeeDt == null || FeeDt.Rows.Count <= 0)
        //    {
        //        //InvokeController("MessageShow", "没有需要保存的费用数据！");
        //        return;
        //    }
        //    InvokeController("SaveFeeItemData", FeeDt);
        //}

        /// <summary>
        /// 选中费用数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void grdFeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // 已转抄医嘱只能查看费用数据不能编辑
        //    if (tabControl2.SelectedTabIndex == 1)
        //    {
        //        return;
        //    }
        //    if (e.ColumnIndex == 5)
        //    {
        //        // 如果选中的是费用数据时，则不允许修改
        //        int rowIndex = grdFeeList.CurrentCell.RowIndex;
        //        DataTable FeeItemDt = grdFeeList.DataSource as DataTable;
        //        if (!string.IsNullOrEmpty(FeeItemDt.Rows[rowIndex]["FeeClass"].ToString()))
        //        {
        //            if (Convert.ToInt32(FeeItemDt.Rows[rowIndex]["FeeClass"].ToString()) == 1)
        //            {
        //                SetOrderDetailstGridState = true;
        //            }
        //            else
        //            {
        //                SetOrderDetailstGridState = false;
        //            }
        //        }
        //    }
        //    //if (e.ColumnIndex == 0)
        //    //{
        //    //    int rowIndex = grdFeeList.CurrentCell.RowIndex;
        //    //    DataTable dt = grdFeeList.DataSource as DataTable;
        //    //    if (dt != null && dt.Rows.Count > 0)
        //    //    {
        //    //        // 判断费用数据是否为药品项目
        //    //        if (!string.IsNullOrEmpty(dt.Rows[rowIndex]["FeeClass"].ToString()))
        //    //        {
        //    //            // dt.Rows[rowIndex]["CheckFLG"] = 0;
        //    //            if (Convert.ToInt32(dt.Rows[rowIndex]["FeeClass"]) == 1)
        //    //            {
        //    //                // 药品费用不允许删除，或修改
        //    //                return;
        //    //            }
        //    //        }

        //    //        // 选中标识为空时，默认为未选中
        //    //        if (string.IsNullOrEmpty(dt.Rows[rowIndex]["CheckFLG"].ToString()))
        //    //        {
        //    //            dt.Rows[rowIndex]["CheckFLG"] = 0;
        //    //        }
        //    //        // 去掉选中
        //    //        if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFlg"]) == 1)
        //    //        {
        //    //            dt.Rows[rowIndex]["CheckFlg"] = 0;
        //    //        }
        //    //        else
        //    //        {
        //    //            // 选中数据
        //    //            dt.Rows[rowIndex]["CheckFlg"] = 1;
        //    //        }
        //    //    }
        //    //}
        //    //else if (e.ColumnIndex == 6)
        //    //{
        //    //    int rowIndex = grdFeeList.CurrentCell.RowIndex;
        //    //    DataTable FeeItemDt = grdFeeList.DataSource as DataTable;
        //    //    if (!string.IsNullOrEmpty(FeeItemDt.Rows[rowIndex]["FeeClass"].ToString()))
        //    //    {
        //    //        if (Convert.ToInt32(FeeItemDt.Rows[rowIndex]["FeeClass"].ToString()) == 1)
        //    //        {
        //    //            SetOrderDetailstGridState = true;
        //    //        }
        //    //        else
        //    //        {
        //    //            SetOrderDetailstGridState = false;
        //    //        }
        //    //    }
        //    //}
        //}

        /// <summary>
        /// 弹出网格选中事件
        /// </summary>
        /// <param name="SelectedValue"></param>
        /// <param name="stop"></param>
        /// <param name="customNextColumnIndex"></param>
        //private void grdFeeList_SelectCardRowSelected(object SelectedValue, ref bool stop, ref int customNextColumnIndex)
        //{
        //    DataRow row = (DataRow)SelectedValue;
        //    int rowid = this.grdFeeList.CurrentCell.RowIndex;
        //    DataTable dt = (DataTable)grdFeeList.DataSource;
        //    if (string.IsNullOrEmpty(dt.Rows[rowid]["GenerateID"].ToString()))
        //    {
        //        dt.Rows[rowid]["GenerateID"] = 0;
        //    }
        //    dt.Rows[rowid]["PatListID"] = m_PatListID;  // 病人登记ID
        //    dt.Rows[rowid]["BabyID"] = 0;   // BadyId
        //    dt.Rows[rowid]["ItemID"] = row["ItemID"]; // 项目ID
        //    dt.Rows[rowid]["ItemName"] = row["ItemName"]; // 项目名
        //    dt.Rows[rowid]["FeeClass"] = row["ItemClass"]; // 项目类型
        //    int ItemClass = Convert.ToInt32(row["ItemClass"]);
        //    dt.Rows[rowid]["StatID"] = row["StatID"]; // 大项目ID
        //    dt.Rows[rowid]["Spec"] = row["Standard"]; // 规格
        //    dt.Rows[rowid]["Unit"] = row["MiniUnitName"]; // 单位
        //    dt.Rows[rowid]["PackAmount"] = row["MiniConvertNum"]; // 划价系数
        //    dt.Rows[rowid]["InPrice"] = row["InPrice"];  // 批发价
        //    dt.Rows[rowid]["SellPrice"] = row["SellPrice"]; // 销售价
        //    dt.Rows[rowid]["Amount"] = 0;  // 数量
        //    dt.Rows[rowid]["TotalFee"] = 0; // 总金额
        //    dt.Rows[rowid]["DoseAmount"] = 0;// 处方帖数
        //    if (ItemClass == 2)
        //    {
        //        if (txtDeptList.MemberValue != null)
        //        {
        //            dt.Rows[rowid]["ExecDeptDoctorID"] = Convert.ToInt32(txtDeptList.MemberValue.ToString()); // 执行科室ID
        //            dt.Rows[rowid]["ExecDeptName"] = txtDeptList.Text; // 执行科室名
        //        }
        //        else
        //        {
        //            dt.Rows[rowid]["ExecDeptDoctorID"] = 0; // 执行科室ID
        //            dt.Rows[rowid]["ExecDeptName"] = string.Empty; // 执行科室名
        //        }
        //    }
        //    else
        //    {
        //        dt.Rows[rowid]["ExecDeptDoctorID"] = row["ExecDeptId"]; // 执行科室ID
        //        dt.Rows[rowid]["ExecDeptName"] = row["ExecDeptName"]; // 执行科室名
        //    }
        //    dt.Rows[rowid]["PresDate"] = DateTime.Now.ToString("yyyy-MM-dd"); // 处方日期
        //    dt.Rows[rowid]["MarkDate"] = DateTime.Now; // 划价时间
        //    dt.Rows[rowid]["IsStop"] = 0;
        //    //dt.Rows[rowid]["CheckFlg"] = 0;
        //    dt.Rows[rowid]["UpdFlg"] = 1;
        //}

        /// <summary>
        /// 根据数量计算金额
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        //private void grdFeeList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 5)
        //    {
        //        if (grdFeeList.CurrentCell != null)
        //        {
        //            int rowIndex = grdFeeList.CurrentCell.RowIndex;
        //            DataTable dt = grdFeeList.DataSource as DataTable;
        //            if (grdFeeList.CurrentCell.DataGridView[e.ColumnIndex, rowIndex].Value is DBNull)
        //            {
        //                return;
        //            }
        //            if (!string.IsNullOrEmpty(dt.Rows[rowIndex]["Amount"].ToString())
        //            && !string.IsNullOrEmpty(dt.Rows[rowIndex]["PackAmount"].ToString()))
        //            {
        //                // 根据数量计算总价格（数量*单价/划价系数）
        //                int count = int.Parse(dt.Rows[rowIndex]["Amount"].ToString());
        //                decimal PackAmount = decimal.Parse(dt.Rows[rowIndex]["PackAmount"].ToString());
        //                decimal price = decimal.Parse(dt.Rows[rowIndex]["SellPrice"].ToString());
        //                dt.Rows[rowIndex]["TotalFee"] = Math.Round(count * price / PackAmount, 2);
        //                dt.Rows[rowIndex]["UpdFlg"] = 1;
        //            }
        //            grdFeeList.Refresh();
        //        }
        //    }
        //}

        /// <summary>
        /// 改变窗体状态
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmDoctorManagement_VisibleChanged(object sender, EventArgs e)
        {
            //grdFeeList.EndEdit();
        }

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCopied_Click(object sender, EventArgs e)
        {
            DataTable docDt = grdDoctorList.DataSource as DataTable;
            if (docDt != null && docDt.Rows.Count > 0)
            {
                List<string> arrayOrderID = new List<string>();
                // 取得待转抄医嘱ID
                for (int i = 0; i < docDt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(docDt.Rows[i]["CheckFlg"]) == 1)
                    {
                        arrayOrderID.Add(docDt.Rows[i]["OrderID"].ToString());
                    }
                }
                // 勾选了待转抄的数据
                if (arrayOrderID.Count > 0)
                {
                    InvokeController("CopiedDoctocList", arrayOrderID);
                    // 清空医嘱关联费用数据
                    orderFee1.LoadOrderFeeGridData(-1, -1, -1, string.Empty);
                }
            }
        }

        /// <summary>
        /// 取消转抄
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancelCopied_Click(object sender, EventArgs e)
        {
            DataTable docDt = grdHasBeenCopiedDocList.DataSource as DataTable;
            if (docDt != null && docDt.Rows.Count > 0)
            {
                List<string> arrayOrderID = new List<string>();
                // 取得待取消转抄医嘱ID
                for (int i = 0; i < docDt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(docDt.Rows[i]["CheckFlg"]) == 1)
                    {
                        arrayOrderID.Add(docDt.Rows[i]["OrderID"].ToString());
                    }
                }
                // 勾选了待取消转抄的数据
                if (arrayOrderID.Count > 0)
                {
                    InvokeController("CancelTransDocOrder", arrayOrderID);
                    // 清空医嘱关联费用数据
                    orderFee1.LoadOrderFeeGridData(-1, -1, -1, string.Empty);
                }
            }
        }

        /// <summary>
        /// Tab切换
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void tabControl2_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            if (tabControl2.SelectedTabIndex == 0)
            {
                btnCopied.Enabled = true;
                btnSend.Enabled = true;
                btnCancelCopied.Enabled = false;
                orderFee1.SetButtonEnabled(true);
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                orderFee1.SetButtonEnabled(false);
                btnCopied.Enabled = false;
                btnSend.Enabled = false;
                btnCancelCopied.Enabled = true;
            }

            btnSelect_Click(null, null);
        }

        /// <summary>
        /// 绘制未转抄医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void PaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = colDocName.Index;
            DataTable docList = grdDoctorList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 绘制已转抄医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void HasBeenDocPaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = colDoctorName.Index;
            DataTable docList = grdHasBeenCopiedDocList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 组线符号
        /// </summary>
        private int mLastDocGroupFlag = 0;

        /// <summary>
        /// 获取分组线符号
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="docList">数据源</param>
        /// <returns>分组线符号</returns>
        private int GetGroupFlag(int rowIndex, DataTable docList)
        {
            int groupID = Convert.ToInt32(docList.Rows[rowIndex]["GroupID"]);

            // 判断是否为第一行
            if ((rowIndex - 1) == -1)
            {
                // 如果下一行和第一行是同组
                // 判断是否存在多行数据
                if (rowIndex < docList.Rows.Count - 1)
                {
                    if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
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
                    if (Convert.ToInt32(docList.Rows[rowIndex - 1]["GroupID"]) == groupID)
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
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
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
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
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

            return mLastDocGroupFlag;
        }

        /// <summary>
        /// 未转抄医嘱全选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkDoctorList_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAllCheck)
            {
                DataTable doctorList = grdDoctorList.DataSource as DataTable;
                if (doctorList != null && doctorList.Rows.Count > 0)
                {
                    for (int i = 0; i < doctorList.Rows.Count; i++)
                    {
                        doctorList.Rows[i]["CheckFLG"] = chkDoctorList.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 已转抄医嘱全选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkHasBeenCopiedDocList_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAllCheck)
            {
                DataTable doctorList = grdHasBeenCopiedDocList.DataSource as DataTable;
                if (doctorList != null && doctorList.Rows.Count > 0)
                {
                    for (int i = 0; i < doctorList.Rows.Count; i++)
                    {
                        doctorList.Rows[i]["CheckFLG"] = chkHasBeenCopiedDocList.Checked ? 1 : 0;
                    }
                }
            }
        }

        #region 皮试界面

        /// <summary>
        /// 初始化皮试界面
        /// </summary>
        private void InitSkinTest()
        {
            cbUnCheck.Checked = true;
            dgSkinTest.DataSource = null;
            //btnN.Enabled = false;
            sdtOrderDate.Bdate.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            sdtOrderDate.Edate.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptID">默认科室ID</param>
        private void bind_DeptInfo(DataTable deptDt, int deptID)
        {
            txtDept.MemberField = "DeptId";
            txtDept.DisplayField = "Name";
            txtDept.CardColumn = "Name|名称|auto";
            txtDept.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDept.ShowCardWidth = 350;
            txtDept.ShowCardDataSource = deptDt;
            if (deptID > 0)
            {
                // 检查当前用户所属科室是否存在住院科室列表中
                DataRow[] dr = deptDt.Select(string.Format("DeptId={0}", deptID));
                if (dr.Length > 0)
                {
                    txtDept.MemberValue = deptID;
                }
                else
                {
                    // 如果不存在住院科室列表中，则默认选择第一条数据
                    txtDept.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }
            else
            {
                if (deptDt != null && deptDt.Rows.Count > 0)
                {
                    txtDept.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            QuerySkinTestInfo();
        }

        /// <summary>
        /// 标注阴性
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnY_Click(object sender, EventArgs e)
        {
            int iOrderId = 0;
            if (dgSkinTest.CurrentRow != null)
            {
                iOrderId = Convert.ToInt32(dgSkinTest.CurrentRow.Cells["SkinOrderID"].Value);
                InvokeController("CheckSkinTest", iOrderId, true);
            }
        }

        /// <summary>
        /// 标注阳性
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnN_Click(object sender, EventArgs e)
        {
            int iOrderId = 0;
            if (dgSkinTest.CurrentRow != null)
            {
                iOrderId = Convert.ToInt32(dgSkinTest.CurrentRow.Cells["SkinOrderID"].Value);
                InvokeController("CheckSkinTest", iOrderId, false);
            }
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 加载皮试医嘱数据
        /// </summary>
        /// <param name="dtSkinTest">皮试医嘱数据</param>
        public void LoadSkinTestInfo(DataTable dtSkinTest)
        {
            dgSkinTest.DataSource = dtSkinTest;
        }

        /// <summary>
        /// 查询皮试数据方法
        /// </summary>
        public void QuerySkinTestInfo()
        {
            int iDeptID = 0;
            if (txtDept.MemberValue != null)
            {
                iDeptID = Convert.ToInt32(txtDept.MemberValue.ToString());
            }

            bool bIsCheckeed = cbChecked.Checked;
            string sBDate = sdtOrderDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sEDate = sdtOrderDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            InvokeController("QuerySkinTestData", iDeptID, bIsCheckeed, sBDate, sEDate);
        }

        #endregion

        /// <summary>
        /// 医嘱发送
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            DataTable docDt = grdDoctorList.DataSource as DataTable;
            if (docDt != null && docDt.Rows.Count > 0)
            {
                List<string> arrayOrderID = new List<string>();
                List<int> arrayGroupoID = new List<int>();
                // 取得待转抄医嘱ID
                for (int i = 0; i < docDt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(docDt.Rows[i]["CheckFlg"]) == 1)
                    {
                        arrayOrderID.Add(docDt.Rows[i]["OrderID"].ToString());
                        int iGroupId = Convert.ToInt32(docDt.Rows[i]["GroupID"].ToString());
                        if (!arrayGroupoID.Contains(iGroupId))
                        {
                            arrayGroupoID.Add(iGroupId);
                        }
                    }
                }
                // 勾选了待转抄的数据
                if (arrayOrderID.Count > 0)
                {
                    InvokeController("CopiedandSendDoctocList", arrayOrderID, arrayGroupoID);
                    // 清空医嘱关联费用数据
                    orderFee1.LoadOrderFeeGridData(-1, -1, -1, string.Empty);
                }
            }
        }

        /// <summary>
        /// 病人列表全选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbPatList_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAllCheck)
            {
                DataTable doctorList = grdPatList.DataSource as DataTable;
                if (doctorList != null && doctorList.Rows.Count > 0)
                {
                    for (int i = 0; i < doctorList.Rows.Count; i++)
                    {
                        doctorList.Rows[i]["CheckFLG"] = cbPatList.Checked ? 1 : 0;
                    }
                }

                if (tabControl2.SelectedTabIndex == 0)
                {
                    if (cbPatList.Checked)
                    {
                        grdDoctorList.DataSource = notCopiedDocDt;
                    }
                    else
                    {
                        DataTable orderDt = notCopiedDocDt.Clone();
                        grdDoctorList.DataSource = orderDt;
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    if (cbPatList.Checked)
                    {
                        grdHasBeenCopiedDocList.DataSource = hasBeenCopiedDocDt;
                    }
                    else
                    {
                        DataTable orderDt = hasBeenCopiedDocDt.Clone();
                        grdHasBeenCopiedDocList.DataSource = orderDt;
                    }
                }
            }
        }
    }
}
