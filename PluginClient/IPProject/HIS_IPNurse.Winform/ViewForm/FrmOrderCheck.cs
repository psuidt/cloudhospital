using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 医嘱发送界面
    /// </summary>
    public partial class FrmOrderCheck : BaseFormBusiness, IOrderCheck
    {
        /// <summary>
        /// 病人列表
        /// </summary>
        private DataTable dtPatList = new DataTable();

        /// <summary>
        /// 医嘱列表
        /// </summary>
        private DataTable dtOrderList = new DataTable();

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOrderCheck()
        {
            InitializeComponent();
            grdOrderList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintGroupLine);
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmOrderCheck_OpenWindowBefore(object sender, EventArgs e)
        {
            ePSendResult.Expanded = false;
            grdPatList.DataSource = null;
            dgSendResult.DataSource = null;
            grdOrderList.DataSource = null;
            grdFeeList.DataSource = null;
            cmbDept.Items.Clear();
            InvokeController("GetDeptList");
            // 打开界面是默认执行查询
            btnQuery_Click(null, null);
        }

        /// <summary>
        /// 选中全部
        /// </summary>
        /// <param name="sender">chk</param>
        /// <param name="e">事件参数</param>
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            chkALL.Checked = (chkLong.Checked && chkTemp.Checked && chkNewOpen.Checked && chkNewStop.Checked) || (!chkLong.Checked && !chkTemp.Checked && !chkNewOpen.Checked && !chkNewStop.Checked);
        }

        #region 按钮事件
        
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetOrederCheckInfo();
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                DataTable dt = (DataTable)grdOrderList.DataSource;
                List<int> iGroupIDList = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    string sChecked = dr["CheckFlg"].ToString();
                    if (sChecked.Contains("1"))
                    {
                        int iGroupId = Convert.ToInt32(dr["GroupID"].ToString());
                        iGroupIDList.Add(iGroupId);
                    }
                }

                if (iGroupIDList.Count > 0)
                {
                    InvokeController("SendOrderCheckList", iGroupIDList, DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCalcel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        #region 网格事件
        
        /// <summary>
        /// 病人列表点击事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPatList.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFlg"]) == 1)
                        {
                            dt.Rows[rowIndex]["CheckFlg"] = 0;
                            // 从医嘱列表中去掉对应的数据
                            DataTable commandDt = grdOrderList.DataSource as DataTable;
                            commandDt.TableName = "DocList";
                            DataView view = new DataView(commandDt);
                            string sqlWhere = string.Format("PatListId <> {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                            view.RowFilter = sqlWhere;
                            view.Sort = "PatListId, GroupID ASC";
                            grdOrderList.DataSource = view.ToTable();
                        }
                        else
                        {
                            dt.Rows[rowIndex]["CheckFlg"] = 1;
                            // 将对应病人的数据追加到医嘱列表中

                            //NotCopiedDocDt.TableName = "DocList";
                            DataView view = new DataView(dtOrderList);
                            view.RowFilter = string.Format("PatListId = {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                            view.Sort = "PatListId, GroupID ASC";
                            DataTable commandDt = grdOrderList.DataSource as DataTable;
                            DataTable tempDt = view.ToTable();
                            for (int i = 0; i < tempDt.Rows.Count; i++)
                            {
                                tempDt.Rows[i]["CheckFlg"] = 1;
                            }

                            if (commandDt != null)
                            {
                                commandDt.Merge(tempDt);
                            }
                            else
                            {
                                grdOrderList.DataSource = tempDt;
                            }
                        }
                    }
                }

                grdFeeList.DataSource = new DataTable();
            }
        }

        /// <summary>
        /// 医嘱表点击事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdOrderList.Rows.Count > 0)
            {
                int rowIndex = grdOrderList.CurrentCell.RowIndex;
                DataTable dt = grdOrderList.DataSource as DataTable;
                int iPatListID = Convert.ToInt32(dt.Rows[rowIndex]["PatListID"]);
                int iGroupID = Convert.ToInt32(dt.Rows[rowIndex]["GroupID"]);

                if (e.ColumnIndex == 0)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string strWhere = string.Format(
                            "PatListID={0} AND GroupID={1}",
                                iPatListID,
                                iGroupID);
                        DataRow[] arrayDr = dt.Select(strWhere);
                        // 去掉选中
                        if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFlg"]) == 1)
                        {
                            if (arrayDr.Length > 0)
                            {
                                for (int i = 0; i < arrayDr.Length; i++)
                                {
                                    arrayDr[i]["CheckFlg"] = 0;
                                }
                            }
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
                        }                        
                    }
                }

                grdFeeList.EndEdit();
                // 获取医嘱关联的费用数据
                ePSendResult.Expanded = false;
                InvokeController("GetOrderRelationFeeList", iPatListID, iGroupID);
            }
        }

        /// <summary>
        /// 病人全选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbPatList_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = grdPatList.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["CheckFLG"] = cbPatList.Checked ? 1 : 0;
                }
            }

            if (cbPatList.Checked)
            {
                grdOrderList.DataSource = dtOrderList;
            }
            else
            {
                grdOrderList.DataSource = null;
            }
        }

        /// <summary>
        /// 医嘱全选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbOrder_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = grdOrderList.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["CheckFLG"] = cbOrder.Checked ? 1 : 0;
                }
            }
        }      
        #endregion

        #region 绑定方法
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptID">默认科室ID</param>
        public void bind_DeptList(DataTable deptDt, int deptID)
        {
            foreach (DataRow dr in deptDt.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["Name"].ToString();
                cbxItem.Tag = dr["DeptId"];
                cmbDept.Items.Add(cbxItem);
            }

            if (deptDt.Rows.Count > 0)
            {
                cmbDept.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        public void bind_PatList(DataTable patListDt)
        {
            dtPatList = patListDt;
            grdPatList.DataSource = patListDt;
            cbPatList.Checked = true;
            //setGridSelectIndex(grdPatList,0);
        }

        /// <summary>
        /// 绑定医嘱列表
        /// </summary>
        /// <param name="docListDt">医嘱列表</param>
        public void bind_OrederCheckInfo(DataTable docListDt)
        {
            dtOrderList = docListDt;
            // 合并同组部分数据数据
            if (docListDt!=null && docListDt.Rows.Count >= 1)
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

            grdOrderList.DataSource = docListDt;
            cbOrder.Checked = true;
            // 获取医嘱关联的费用数据
            if (docListDt != null && docListDt.Rows.Count > 1)
            {
                int iPatListID = Convert.ToInt32(docListDt.Rows[0]["PatListID"]);
                int iGroupID = Convert.ToInt32(docListDt.Rows[0]["GroupID"]);

                InvokeController("GetOrderRelationFeeList", iPatListID, iGroupID);
            }
        }

        /// <summary>
        /// 绑定医嘱关联费用列表
        /// </summary>
        /// <param name="patOrderRelationFeeList">医嘱关联费用列表</param>
        public void bind_PatOrderRelationFeeList(DataTable patOrderRelationFeeList)
        {
            grdFeeList.DataSource = patOrderRelationFeeList;
            grdFeeList.EndEdit();
        }

        /// <summary>
        /// 发送返回结果集
        /// </summary>
        /// <param name="sResultList">发送结果</param>
        public void bind_OrderSendResult(List<IP_OrderCheckError>  sResultList)
        {
            if (sResultList != null && sResultList.Count > 0)
            {
                foreach (IP_OrderCheckError orderCheckError in sResultList)
                {
                    orderCheckError.ErrorMessage = orderCheckError.OrderName + "  " + orderCheckError.ErrorMessage;
                }
                dgSendResult.DataSource= sResultList;
                ePSendResult.Expanded = true;
            }
            else
            {
                dgSendResult.DataSource = null;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 刷新发送数据
        /// </summary>
        public void GetOrederCheckInfo()
        {
            int iDeptId = -1;
            if (cmbDept.SelectedIndex > -1)
            { 
                iDeptId = Convert.ToInt32(((BaseItem)cmbDept.SelectedItem).Tag.ToString());
            }

            string sOrderCategory = "-1";
            sOrderCategory = chkLong.Checked == chkTemp.Checked ? "-1" : (chkLong.Checked ? "0" : "1");
            string sOrderStatus = "0";
            sOrderStatus = chkNewOpen.Checked == chkNewStop.Checked ? "0" : (chkNewOpen.Checked ? "1" : "2"); //cbNomal
            grdFeeList.DataSource = null;
            InvokeController("GetOrederCheckInfo", iDeptId, sOrderCategory, sOrderStatus);
        }

        /// <summary>
        /// 绘制未转抄医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void PaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = colDoctorName.Index;
            DataTable docList = grdOrderList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 获取分组线符号
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
                        mLastDocGroupFlag = 1;
                    }
                    else
                    {
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
                            mLastDocGroupFlag = 2;
                        }
                        else
                        {
                            mLastDocGroupFlag = 3;
                        }
                    }
                    else if (mLastDocGroupFlag == 3 || mLastDocGroupFlag == 0)
                    {
                        // 如果上一行绘制的是结束线，或者没有绘制分组线
                        // 判断下一行是否还是同组
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                        {
                            mLastDocGroupFlag = 1;
                        }
                        else
                        {
                            mLastDocGroupFlag = 0;
                        }
                    }
                }
            }

            return mLastDocGroupFlag;
        }

        #endregion
    }
}
