using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmConvertPoints : BaseFormBusiness, IFrmConvertPoints
    {
        #region 接口实现
        /// <summary>
        /// 卡类型id
        /// </summary>
        private int cardTypeID;

        /// <summary>
        /// Gets or sets 卡类型id
        /// </summary>
        /// <value>卡类型id</value>
        public int CardTypeID
        {
            get
            {
                return cardTypeID;
            }

            set
            {
                cardTypeID = value;
            }
        }

        /// <summary>
        /// 帐户类型数据
        /// </summary>
        private DataTable dtCardTypeInfo;

        /// <summary>
        /// Gets or sets 帐户类型数据
        /// </summary>
        /// <value>帐户类型数据</value>
        public DataTable DtCardTypeInfo
        {
            get
            {
                return dtCardTypeInfo;
            }

            set
            {
                dtCardTypeInfo = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmConvertPoints" /> class.
        /// </summary>
        public FrmConvertPoints()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定卡类型信息
        /// </summary>
        /// <param name="dt">卡类型数据</param>
        /// <param name="index">索引</param>
        public void BindCardTypeInfo(DataTable dt,int index)
        {
            dgCardTypeInfo.DataSource = dt;
            setGridSelectIndex(dgCardTypeInfo);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmConvertPoints_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(dgCardTypeInfo);
            bindGridSelectIndex(dgPoints);
            InvokeController("BindCardTypeDataSource",0);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiNew_Click(object sender, EventArgs e)
        {
            int cardTypeIndex = dgCardTypeInfo.Rows.Count;
            InvokeController("ShowCardTypeInfo", 0, string.Empty, string.Empty, 1, 1, cardTypeIndex, string.Empty);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiEdit_Click(object sender, EventArgs e)
        {
            if (dgCardTypeInfo.CurrentCell!=null)
            {
                int cardTypeIndex = dgCardTypeInfo.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgCardTypeInfo.DataSource;
                int cardTypeID= Convert.ToInt16(dt.Rows[cardTypeIndex]["CardTypeID"]);
                string cardTypeName= Convert.ToString(dt.Rows[cardTypeIndex]["CardTypeName"]);
                string cardInterface = Convert.ToString(dt.Rows[cardTypeIndex]["CardInterface"]);
                int cardType = Convert.ToInt16(dt.Rows[cardTypeIndex]["cardType"]);
                int flag= Convert.ToInt16(dt.Rows[cardTypeIndex]["useflag"]);
                string cardPrefix = Convert.ToString(dt.Rows[cardTypeIndex]["cardPrefix"]);
                InvokeController("ShowCardTypeInfo", cardTypeID, cardTypeName, cardInterface, cardType, flag, cardTypeIndex, cardPrefix);
            }  
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgCardTypeInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgCardTypeInfo.Rows[e.RowIndex].Cells["flagdesc"].Value);
            if (stopFlag == "停用")
            {
                dgCardTypeInfo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (dgCardTypeInfo.CurrentCell != null)
            {
                int rowIndex = dgCardTypeInfo.CurrentCell.RowIndex;
                DataTable dt = dgCardTypeInfo.DataSource as DataTable;
                int id = Convert.ToInt16(dt.Rows[rowIndex]["CardTypeID"]);
                int use = (int)InvokeController("UpdateUseFlag", id, 0);
                int flag = Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
               
                if (flag == 0)
                {
                    if (MessageBoxEx.Show(" 此帐户类型将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                     use = (int)InvokeController("UpdateUseFlag", id, 1);
                    if (use > 0)
                    {
                        dt.Rows[rowIndex]["UseFlag"] = 1;
                        dt.Rows[rowIndex]["flagdesc"] = "有效";
                        dt.AcceptChanges();
                    }
                }
                else
                {
                    if (MessageBoxEx.Show(" 此帐户类型将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    use = (int)InvokeController("UpdateUseFlag", id, 0);
                    if (use > 0)
                    {
                        dt.Rows[rowIndex]["UseFlag"] = 0;
                        dt.Rows[rowIndex]["flagdesc"] = "停用";
                        dt.AcceptChanges();
                    }
                }         
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiStart_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 绑定帐户网格
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="rowIndex">行索引</param>
        public void BinddgAccount(int cardTypeID, int rowIndex)
        {          
            DataTable dt = (DataTable)InvokeController("GetConvertPointsInfo", cardTypeID);
            dgPoints.DataSource = dt;
            setGridSelectIndex(dgPoints);    
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiNewConvertPoints_Click(object sender, EventArgs e)
        {
            if (dgCardTypeInfo.CurrentCell != null)
            {
                int rowIndex = dgCardTypeInfo.CurrentCell.RowIndex;
                DataTable dt = dgCardTypeInfo.DataSource as DataTable;
                int useFlag = Convert.ToInt16(dt.Rows[rowIndex]["UseFlag"]);
                if (useFlag == 0)
                {
                    MessageBoxEx.Show("账户类型已停用，不能对帐户进行规则设置！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cardTypeID = Convert.ToInt16(dt.Rows[rowIndex]["CardTypeID"]);
                string cardTypeName= Convert.ToString(dt.Rows[rowIndex]["CardTypeName"]);

                rowIndex = dgPoints.Rows.Count;
                InvokeController("ShowFrmConvertPointsInfo", cardTypeID, rowIndex, cardTypeName,0,0,0,0);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgPoints_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgPoints.Rows[e.RowIndex].Cells["UseFlagDesc"].Value);
            if (stopFlag == "停用")
            {
                dgPoints.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnEditPoints_Click(object sender, EventArgs e)
        {
            if (dgCardTypeInfo.CurrentCell != null)
            {
                int rowIndex = dgCardTypeInfo.CurrentCell.RowIndex;
                DataTable dt = dgCardTypeInfo.DataSource as DataTable;     
                string cardTypeName = Convert.ToString(dt.Rows[rowIndex]["CardTypeName"]);
                int useFlag = Convert.ToInt16(dt.Rows[rowIndex]["UseFlag"]);
                if (useFlag == 0)
                {
                    MessageBoxEx.Show("账户类型已停用，不能对帐户进行规则设置！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgPoints.CurrentCell != null)
                {
                    rowIndex = dgPoints.CurrentCell.RowIndex;
                    DataTable dtPoints= dgPoints.DataSource as DataTable;

                    int workID = Convert.ToInt16(dtPoints.Rows[rowIndex]["usework"]);
                    int cash = Convert.ToInt16(dtPoints.Rows[rowIndex]["Cash"]);
                    int score = Convert.ToInt16(dtPoints.Rows[rowIndex]["score"]);
                    int cardTypeID = Convert.ToInt16(dtPoints.Rows[rowIndex]["CardTypeID"]);
                    int convertID = Convert.ToInt16(dtPoints.Rows[rowIndex]["ConvertID"]);
                    InvokeController("ShowFrmConvertPointsInfo", cardTypeID, rowIndex, cardTypeName, workID, cash, score, convertID);
                }                 
            } 
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btiStop_Click(object sender, EventArgs e)
        {
            if (dgPoints.CurrentCell!=null)
            {
                int rowIndex = dgPoints.CurrentCell.RowIndex;
                DataTable dt = dgPoints.DataSource as DataTable;
                int flag = Convert.ToInt16(dt.Rows[rowIndex]["useflag"]);
                int id = Convert.ToInt16(dt.Rows[rowIndex]["ConvertID"]);
                if (flag==0)
                {
                    if (MessageBoxEx.Show(" 选中的积分兑换设置将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    flag = (int)InvokeController("UpdatePointsUseFlag", id, 1);                     
                }
                else
                {
                    if (MessageBoxEx.Show(" 选中的积分兑换设置将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    flag = (int)InvokeController("UpdatePointsUseFlag", id, 0);                    
                }

                BinddgAccount(CardTypeID, 0);
                setGridSelectIndex(dgPoints);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgCardTypeInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgCardTypeInfo.CurrentCell != null)
            {             
                int rowIndex = dgCardTypeInfo.CurrentCell.RowIndex;             
                DataTable dt = dgCardTypeInfo.DataSource as DataTable;
                CardTypeID = Convert.ToInt16(dt.Rows[rowIndex]["CardTypeID"]);
                int useFlag = Convert.ToInt16(dt.Rows[rowIndex]["useFlag"]);
                btnStop.Text = (useFlag > 0) ? "停用" : "启用";
                BinddgAccount(CardTypeID, 0);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgPoints_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgPoints.CurrentCell != null)
            {
                int rowIndex = dgPoints.CurrentCell.RowIndex;
                DataTable dtPoints = dgPoints.DataSource as DataTable;
                int useFlag = Convert.ToInt16(dtPoints.Rows[rowIndex]["useflag"]);
                btiStop.Text = (useFlag > 0) ? "停用" : "启用";
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
    }
}
