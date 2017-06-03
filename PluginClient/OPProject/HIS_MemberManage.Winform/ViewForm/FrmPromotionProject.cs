using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmPromotionProject : BaseFormBusiness,IFrmPromotionProject
    {
        #region 接口内容实现
        /// <summary>
        /// 优惠明细ID
        /// </summary>
        private int promSunID;

        /// <summary>
        /// Gets or sets 优惠明细ID
        /// </summary>
        /// <value>优惠明细ID</value>
        public int PromSunID
        {
            get
            {
                return promSunID;
            }

            set
            {
                promSunID = value;
            }
        }

        /// <summary>
        /// 明细标识
        /// </summary>
        private int detailFlag;

        /// <summary>
        /// Gets or sets  明细标识
        /// </summary>
        /// <value>明细标识</value>
        public int DetailFlag
        {
            get
            {
                return detailFlag;
            }

            set
            {
                detailFlag = value;
            }
        }

        /// <summary>
        /// 头标识id
        /// </summary>
        private int headFlag;

        /// <summary>
        /// Gets or sets 头标识id
        /// </summary>
        /// <value>优惠明细ID</value>
        public int HeadFlag
        {
            get
            {
                return headFlag;
            }

            set
            {
                headFlag = value;
            }
        }

        /// <summary>
        /// 方案id
        /// </summary>
        private int promID;

        /// <summary>
        /// Gets or sets 方案id
        /// </summary>
        /// <value>方案id</value>
        public int PromID
        {
            get
            {
                return promID;
            }

            set
            { 
                promID = value;
            }
        }

        /// <summary>
        /// 头名称
        /// </summary>
        private string headName;

        /// <summary>
        /// Gets or sets 头名称
        /// </summary>
        /// <value>头名称</value>
        public string HeadName
        {
            get
            {
                return headName;
            }

            set
            {
                headName=value  ;
            }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private string stDate;

        /// <summary>
        /// Gets or sets 开始日期
        /// </summary>
        /// <value>开始日期</value>
        public string StDate
        {
            get
            {
                return stDate;
            }

            set
            {
                stDate = value;
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private string endDate;

        /// <summary>
        /// Gets or sets 结束日期
        /// </summary>
        /// <value>结束日期</value>
        public string EndsDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }
        #endregion

        /// <summary>
        ///  构造
        ///  Initializes a new instance of the<see cref="FrmPromotionProject" /> class.
        /// </summary>
        public FrmPromotionProject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmPromotionProject_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(dgHead);
            bindGridSelectIndex(dgDetail);
            BindWorkInfo();
            cbbWork.SelectedValue= (int)InvokeController("GetWorkIDForLoginInfo");
            statRegDate.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statRegDate.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
            InvokeController("BindDgHead",Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text+" 23:59:59");
            BindPromType();
            BindPatType();
            BingPatFeeType();
            BindDiscountDesc();
            panelDetail.Enabled = false;
            SetDetailState(4);
            SetControlState(2);
            BindStatItem();
            BindFeeItem();
        }

        /// <summary>
        /// 绑定机构信息
        /// </summary>
        public void BindWorkInfo()
        {
            DataTable dt = (DataTable)InvokeController("BindWorkInfo");
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dt;
        }

        /// <summary>
        /// 绑定优惠方案头表
        /// </summary>
        /// <param name="dt">优惠方案头表数据</param>
        public void BindPromotionProjectHeadInfo(DataTable dt)
        {           
            dgHead.DataSource = dt;
            setGridSelectIndex(dgHead);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnNewProHead_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue)!=(int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能在操作员所属机构新增方案！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetControlState(1);
        }

        /// <summary>
        /// 设置空间状态
        /// </summary>
        /// <param name="flag">标识</param>
        public void SetControlState(int flag)
        {
            switch (flag)
            {
                case 1:  //新增方案头表
                    txtHeadName.Enabled = true;
                    txtHeadName.Text = string.Empty;
                    txtHeadName.Focus();
                    stHeadTime.Enabled = true;
                    stHeadTime.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                    stHeadTime.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
                    btnSaveHead.Enabled = true;
                    btnEditProHead.Enabled = false;
                    btnNewProHead.Enabled = false;
                    btnDelProHead.Enabled = false;
                    btnStopHead.Enabled = false;
                    btnCopyProHead.Enabled = false;
                    btnCancel.Enabled = true;
                    PromID = 0;
                    break;
                case 2:  //取消新增方案头
                    txtHeadName.Enabled = false;
                    txtHeadName.Text = string.Empty;
                    stHeadTime.Enabled = false;
                    btnSaveHead.Enabled = false;
                    btnEditProHead.Enabled = true;
                    btnNewProHead.Enabled = true;
                    btnDelProHead.Enabled = true;
                    btnStopHead.Enabled = true;
                    btnCopyProHead.Enabled = true;
                    btnCancel.Enabled = false;
                    stHeadTime.Bdate.Text = string.Empty;
                    stHeadTime.Edate.Text = string.Empty;
                    break;
                case 3:  //编辑方案头表
                    txtHeadName.Enabled = true;
                    txtHeadName.Text = string.Empty;
                    txtHeadName.Focus();
                    stHeadTime.Enabled = true;
                    stHeadTime.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                    stHeadTime.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
                    btnSaveHead.Enabled = true;
                    btnEditProHead.Enabled = false;
                    btnNewProHead.Enabled = false;
                    btnDelProHead.Enabled = false;
                    btnStopHead.Enabled = false;
                    btnCopyProHead.Enabled = false;
                    btnCancel.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetControlState(2);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnEditProHead_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能修改操作员所属机构优惠方案！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetControlState(3);
            if (dgHead.CurrentCell!=null)
            {
                int rowIndex = dgHead.CurrentCell.RowIndex;
                DataTable dt = dgHead.DataSource as DataTable;
                txtHeadName.Text = Convert.ToString(dt.Rows[rowIndex]["PromName"]);
                stHeadTime.Bdate.Text = Convert.ToString(dt.Rows[rowIndex]["StartDate"]);
                stHeadTime.Edate.Text = Convert.ToString(dt.Rows[rowIndex]["EndDate"]);
                PromID= Convert.ToInt16(dt.Rows[rowIndex]["PromID"]);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSaveHead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHeadName.Text))
            {
                MessageBoxEx.Show("优惠方案名称不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHeadName.Focus();
                return;
            }

            if (RegexName(txtHeadName.Text))
            {
                MessageBoxEx.Show("优惠方案名称含有特殊字符请修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHeadName.Focus();
                return;
            }

            if (((int) InvokeController("SaveHeadInfo", PromID,txtHeadName.Text.Trim(), stHeadTime.Bdate.Text, stHeadTime.Edate.Text+" 23:59:59"))>0)
            {
                InvokeController("BindDgHead", Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59");
                SetControlState(2);
                if (PromID > 0)
                {
                    setGridSelectIndex(dgHead);
                }
                else
                {
                    setGridSelectIndex(dgHead, dgHead.Rows.Count);
                }
            }
        }

        /// <summary>
        /// 检查名称是否有特殊字符
        /// </summary>
        /// <param name="txt">字符串</param>
        /// <returns>true有特殊字符</returns>
        public bool RegexName(string txt)
        {
            Regex rx = new Regex(@"[`~!@#$%^&*()+=|{}':;',\[\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]+");
            return rx.IsMatch(txt);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgHead_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgHead.Rows[e.RowIndex].Cells["UseFlagDesc"].Value);
            if (stopFlag == "停用")
            {
                dgHead.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgHead_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgHead.CurrentCell!=null)
            {
                int rosIndex = dgHead.CurrentCell.RowIndex;
                DataTable dtHead = dgHead.DataSource as DataTable;
                PromID = Convert.ToInt16(dtHead.Rows[rosIndex]["PromID"]);
                HeadFlag= Convert.ToInt16(dtHead.Rows[rosIndex]["UseFlag"]);
                StDate= Convert.ToString(dtHead.Rows[rosIndex]["StartDate"]);
                switch (HeadFlag)
                {
                    case 0:
                        btnStopHead.Text = "启用方案";
                        break;
                    case 1:
                        btnStopHead.Text = "停用方案";
                        break;
                    case 2:
                        btnStopHead.Text = "启用方案";
                        break;
                }

                DataTable dtDetail = (DataTable)InvokeController("GetPromotionProjectDetail", PromID);
                dgDetail.DataSource = dtDetail;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnDelProHead_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能删除操作员所属机构优惠方案！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBoxEx.Show(" 即将删除已选择优惠方案，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (HeadFlag!=0)
            {
                MessageBoxEx.Show("优惠方案只有处于初始状态才能删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((int)InvokeController("DelPromPro", PromID) > 0)
            {
                InvokeController("BindDgHead", Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59");
                SetControlState(2);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnStopHead_Click(object sender, EventArgs e)
        {
            if (dgHead.CurrentCell==null)
            {
                return;
            }

            int res = -1;
            bool checkFlag = (bool)InvokeController("CheckPromDate", StDate);
            switch (HeadFlag)
            {
                case 0: //初始状态
                    if (MessageBoxEx.Show(" 优惠方案即将启用，启用后的优惠方案不能再进行明细内容修改，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    if (checkFlag == false)
                    {
                        MessageBoxEx.Show("指定优惠时间段内有优惠方案在使用！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    res = (int)InvokeController("UpdateHeadUseFlag", PromID, 1);
                    break;
                case 1: //启用状态
                    if (MessageBoxEx.Show(" 优惠方案即将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    res = (int)InvokeController("UpdateHeadUseFlag", PromID, 2);
                    break;
                case 2:  //停用状态
                    if (MessageBoxEx.Show(" 优惠方案即将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    if (checkFlag == false)
                    {
                        MessageBoxEx.Show("指定优惠时间段内有优惠方案在使用！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    res = (int)InvokeController("UpdateHeadUseFlag", PromID, 1);
                    break;
            }

            if (res>0)
            {
                InvokeController("BindDgHead", Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59");
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (HeadFlag!=0)
            {
                MessageBoxEx.Show("优惠方案明细只有处于初始状态才能修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToInt16(cbbWork.SelectedValue)!= (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能对本机构的方案进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BindCardTypeInfo();
            
            SetDetailState(1);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能对本机构的方案进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetDetailState(2);
        }

        #region 方案明细数据绑定
        /// <summary>
        /// 绑定卡类型
        /// </summary>
        public void BindCardTypeInfo()
        {
            DataTable dt = (DataTable)InvokeController("GetCardTypeInfo", Convert.ToInt16(cbbWork.SelectedValue));
            dt.Rows.RemoveAt(0);
            cbbCardType.DataSource = dt;
            cbbCardType.DisplayMember = "CardTypeName";
            cbbCardType.ValueMember = "CardTypeID";              
        }

        /// <summary>
        /// 绑定病人类型
        /// </summary>
        public void BindPatType()
        {
            var datasource = new[]
            {
                new { Text = "门诊病人", Value = 1 },
                new { Text = "住院病人", Value = 2 },
            };
            cbbPatType.ValueMember = "Value";
            cbbPatType.DisplayMember = "Text";
            cbbPatType.DataSource = datasource;
        }

        /// <summary>
        /// 绑定优惠类型
        /// </summary>
        public void BindPromType()
        {
            var datasource = new[]
            {
                new { Text = "总额优惠", Value = 1 },
                new { Text = "类型优惠", Value = 2 },
                new { Text = "项目优惠", Value = 3 },
            };

            cbbPromType.ValueMember = "Value";
            cbbPromType.DisplayMember = "Text";
            cbbPromType.DataSource = datasource;
        }

        /// <summary>
        /// 绑定费用类型
        /// </summary>
        public void BingPatFeeType()
        {
            DataTable dt = (DataTable)InvokeController("GetPatFeeType");
            cbbFeeType.DisplayMember = "PatTypeName";
            cbbFeeType.ValueMember = "PatTypeID";
            cbbFeeType.DataSource = dt;
        }

        /// <summary>
        /// 绑定折扣描述
        /// </summary>
        public void BindDiscountDesc()
        {
            var datasource = new[] 
            {
                new { Text = "总额折扣", Value = 1 },
                new { Text = "总额扣减", Value = 2 },                
            };

            cbbDiscount.ValueMember = "Value";
            cbbDiscount.DisplayMember = "Text";
            cbbDiscount.DataSource = datasource;             

            cbbDiscountForClass.ValueMember = "Value";
            cbbDiscountForClass.DisplayMember = "Text";
            cbbDiscountForClass.DataSource = datasource;

            cbbDiscountForItem.ValueMember = "Value";
            cbbDiscountForItem.DisplayMember = "Text";
            cbbDiscountForItem.DataSource = datasource;
        }

        /// <summary>
        /// 绑定统计大项目
        /// </summary>
        public void BindStatItem()
        {
            DataTable dt= (DataTable)InvokeController("GetStatItem");

            cbbClass.ValueMember = "StatID";
            cbbClass.DisplayMember = "StatName";
            cbbClass.DataSource = dt;
        }

        /// <summary>
        /// 绑定费用项目
        /// </summary>
        public void BindFeeItem()
        {
            DataTable dt = (DataTable)InvokeController("GetSimpleFeeItemDataDt");

            tbcItem.DisplayField = "ItemName";
            tbcItem.MemberField = "ItemID";
            tbcItem.CardColumn = "ItemCode|项目编码|120,ItemName|项目名称|auto,unitprice|项目单价|80";
            tbcItem.QueryFieldsString = "ItemName,Pym,Wbm";
            tbcItem.ShowCardWidth = 400;

            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["ItemID"];
            dt.PrimaryKey = keys;

            tbcItem.ShowCardDataSource = dt;
        }

        #endregion
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="flag">操作标识</param>
        public void SetDetailState(int flag)
        {            
            switch (flag)
            {
                case 1:     //新增明细
                    btnEditDetail.Enabled = false;
                    btnNewDetail.Enabled = false;
                    btnSaveDetail.Enabled = true;
                    panelDetail.Enabled = true;
                    btnNext.Enabled = true;
                    btnCancelDetail.Enabled = true;
                    PromSunID = 0;
                    DetailFlag = 0;  //新增
                    stiDetail.SelectedPanel = stpTotal;
                    stiDetail.Enabled = true;
                    stpTotal.Enabled = true;
                    btnStopDetail.Enabled = false;
                    cbbPromType.SelectedValue = 1;
                    break;
                case 2:    //修改
                    btnEditDetail.Enabled = false;
                    btnNewDetail.Enabled = false;
                    btnSaveDetail.Enabled = true;
                    panelDetail.Enabled = true;
                    btnNext.Enabled = false;
                    btnCancelDetail.Enabled = true;
                    DetailFlag = 1;  //修改  
                    stiDetail.Enabled = true;
                    btnStopDetail.Enabled = false;
                    break;
                case 3:    //保存 
                    btnEditDetail.Enabled = true;
                    btnNewDetail.Enabled = true;
                    btnSaveDetail.Enabled = false;
                    panelDetail.Enabled = false;
                    btnNext.Enabled = false;
                    btnCancelDetail.Enabled = false;
                    btnStopDetail.Enabled = true;
                    break;
                case 4:   //取消
                    btnEditDetail.Enabled = true;
                    btnNewDetail.Enabled = true;
                    btnSaveDetail.Enabled = false;
                    stiDetail.Enabled = false;
                    panelDetail.Enabled = false;
                    btnNext.Enabled = false;
                    btnCancelDetail.Enabled = false;
                    btnStopDetail.Enabled = true;
                    break;
                case 5:   //下一条
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbPromType_SelectedIndexChanged(object sender, EventArgs e)
        {
            stiDetail.Enabled = true;
            switch (Convert.ToInt16(cbbPromType.SelectedValue))
            {
                case 1:                   
                    stiDetail.SelectedPanel = stpTotal;
                    stpTotal.Enabled = true;
                    stpClass.Enabled = false;
                    stpItem.Enabled = false;
                   
                    cbbDiscount.SelectedIndexChanged += new System.EventHandler(cbbDiscount_SelectedIndexChanged); 
                    break;
                case 2:
                    stpTotal.Enabled = false;
                    stpClass.Enabled = true; 
                    stpItem.Enabled = false;
                    stiDetail.SelectedPanel = stpClass;
                    break;
                case 3:
                    stpTotal.Enabled = false;
                    stpClass.Enabled = false; 
                    stpItem.Enabled = true;
                    stiDetail.SelectedPanel = stpItem;
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCancelDetail_Click(object sender, EventArgs e)
        {
            SetDetailState(4);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            dgDetail.DataSource = null;
            InvokeController("BindDgHead", Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59");
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

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSaveDetail_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能对本机构的方案进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DetailFlag == 0)
            {
                PromSunID = 0; //新增
            }            
            
            if (CheckDetail() == false)
            {
                MessageBoxEx.Show("已存在相同条件的优惠项目！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int res = (int)InvokeController("SavePromotionProjectDetail", BuildPromotionProjectDetail(PromSunID));

            if (res>0)
            {
                DataTable dtDetail = (DataTable)InvokeController("GetPromotionProjectDetail", PromID);
                dgDetail.DataSource = dtDetail;
                if (DetailFlag==0)
                {
                    setGridSelectIndex(dgDetail, dgDetail.Rows.Count-1);
                }
                else
                {
                    setGridSelectIndex(dgDetail);
                }

                SetDetailState(4);
            }
        }

        /// <summary>
        /// 校验新增与修改明细是否存在重复
        /// </summary>
        /// <returns>true存在</returns>
        private bool CheckDetail()
        {
            //校验是否存在
            bool resCheck = true;
            switch (stiDetail.SelectedPanel.Name)
            {
                case "stpTotal":
                    resCheck = (bool)InvokeController("CheckDetailForAmount", Convert.ToInt16(cbbCardType.SelectedValue), Convert.ToInt16(cbbPatType.SelectedValue), Convert.ToInt16(cbbFeeType.SelectedValue), Convert.ToInt16(cbbPromType.SelectedValue), PromID, PromSunID);

                    break;
                case "stpClass":
                    resCheck = (bool)InvokeController("CheckDetailForClass", Convert.ToInt16(cbbCardType.SelectedValue), Convert.ToInt16(cbbPatType.SelectedValue), Convert.ToInt16(cbbFeeType.SelectedValue), Convert.ToInt16(cbbPromType.SelectedValue), Convert.ToInt16(cbbClass.SelectedValue), PromID, PromSunID);
                    break;
                case "stpItem":
                    resCheck = (bool)InvokeController("CheckDetailForItem", Convert.ToInt16(cbbCardType.SelectedValue), Convert.ToInt16(cbbPatType.SelectedValue),Convert.ToInt16(cbbFeeType.SelectedValue), Convert.ToInt16(cbbPromType.SelectedValue), Convert.ToInt32(((DataRow)tbcItem.SelectedValue).ItemArray[0]),PromID, PromSunID);
                    break;
            }

            return resCheck;
        }

        /// <summary>
        /// 构造方案明细类
        /// </summary>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>方案明细类</returns>
        private ME_PromotionProjectDetail BuildPromotionProjectDetail(int promSunID)
        {
            ME_PromotionProjectDetail detailEntity = new ME_PromotionProjectDetail();
            detailEntity.PromSunID = promSunID;
            detailEntity.PromID = PromID;
            detailEntity.CardTypeID = Convert.ToInt16(cbbCardType.SelectedValue);
            detailEntity.PatientType = Convert.ToInt16(cbbPatType.SelectedValue);
            detailEntity.CostType = Convert.ToInt16(cbbFeeType.SelectedValue);

            switch (stiDetail.SelectedPanel.Name)
            {
                case "stpTotal":
                    detailEntity.PromTypeID = 1;  //总额优惠  
                    detailEntity.PromBase = intBase.Value;
                    detailEntity.Prom = Convert.ToInt16(cbbDiscount.SelectedValue);
                    detailEntity.DiscountNumber = intDiscount.Value;
                    detailEntity.PromClass = 0;
                    detailEntity.PromPro = 0;
                    break;
                case "stpClass":
                    detailEntity.PromTypeID = 2;  //分类优惠
                    detailEntity.Prom = Convert.ToInt16(cbbDiscountForClass.SelectedValue);
                    detailEntity.PromClass= Convert.ToInt16(cbbClass.SelectedValue);
                    detailEntity.DiscountNumber = intDiscountForClass.Value;
                    detailEntity.PromPro = 0;
                    break;
                case "stpItem":
                    detailEntity.PromTypeID = 3;  //项目优惠  intDiscountForItem
                    detailEntity.Prom = Convert.ToInt16(cbbDiscountForItem.SelectedValue);
                    detailEntity.PromPro= Convert.ToInt32(((DataRow)tbcItem.SelectedValue).ItemArray[0]);
                    detailEntity.DiscountNumber = intDiscountForItem.Value;
                    detailEntity.PromClass = 0;
                    break;
            }

            detailEntity.UseFlag = 1;
            detailEntity.OperateDate = DateTime.Now;
            return detailEntity;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDetail.CurrentCell!=null)
            {
                int rowIndex = dgDetail.CurrentCell.RowIndex;
                DataTable dtDetail = dgDetail.DataSource as DataTable;
                PromSunID = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromSunID"]);
                PromID= Convert.ToInt16(dtDetail.Rows[rowIndex]["PromID"]);
                cbbCardType.SelectedValue = dtDetail.Rows[rowIndex]["CardTypeID"];  //帐户类型
                cbbPatType.SelectedValue= dtDetail.Rows[rowIndex]["PatientType"];   //病人类型
                cbbFeeType.SelectedValue = dtDetail.Rows[rowIndex]["CostType"];     //费用类型
                cbbPromType.SelectedValue = dtDetail.Rows[rowIndex]["PromTypeID"];  //优惠方式
                int useDetail = Convert.ToInt16(dtDetail.Rows[rowIndex]["useflag"]);

                btnStopDetail.Text = (useDetail == 1) ? "停用" : "启用";

                switch (Convert.ToInt16(cbbPromType.SelectedValue))
                {
                    case 1: //总额优惠                          
                        intBase.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromBase"]);
                        cbbDiscount.SelectedValue = dtDetail.Rows[rowIndex]["Prom"];
                         intDiscount.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpTotal;
                        stpTotal.Enabled = true;
                        break;
                    case 2: //分类优惠
                        cbbDiscountForClass.SelectedValue = dtDetail.Rows[rowIndex]["PromClass"];
                        cbbClass.SelectedValue= dtDetail.Rows[rowIndex]["Prom"];
                        intDiscountForClass.Value= Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpClass;
                        stpClass.Enabled = true;
                        break;
                    case 3: //项目优惠
                        tbcItem.MemberValue = (dtDetail.Rows[rowIndex]["PromPro"]);
                        cbbDiscountForItem.SelectedValue= dtDetail.Rows[rowIndex]["Prom"];  
                        intDiscountForItem.Value=Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpItem;
                        stpItem.Enabled = true;
                        break;
                }
            }
            else
            {
                PromSunID = 0;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnEditDetail_Click_1(object sender, EventArgs e)
        {
            if (dgDetail.CurrentCell != null)
            {
                if (HeadFlag != 0)
                {
                    MessageBoxEx.Show("优惠方案明细只有处于初始状态才能修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BindCardTypeInfo();
                SetDetailState(2);

                int rowIndex = dgDetail.CurrentCell.RowIndex;
                DataTable dtDetail = dgDetail.DataSource as DataTable;
                PromSunID = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromSunID"]);
                PromID = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromID"]);
                cbbCardType.SelectedValue = dtDetail.Rows[rowIndex]["CardTypeID"];  //帐户类型
                cbbPatType.SelectedValue = dtDetail.Rows[rowIndex]["PatientType"];   //病人类型
                cbbFeeType.SelectedValue = dtDetail.Rows[rowIndex]["CostType"];     //费用类型
                cbbPromType.SelectedValue = dtDetail.Rows[rowIndex]["PromTypeID"];  //优惠方式

                switch (Convert.ToInt16(cbbPromType.SelectedValue))
                {
                    case 1: //总额优惠                          
                        intBase.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromBase"]);
                        cbbDiscount.SelectedValue = dtDetail.Rows[rowIndex]["Prom"];
                        intDiscount.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpTotal;
                        stpTotal.Enabled = true;
                        break;
                    case 2: //分类优惠
                        cbbDiscountForClass.SelectedValue = dtDetail.Rows[rowIndex]["Prom"];
                        cbbClass.SelectedValue = dtDetail.Rows[rowIndex]["PromClass"]; 
                        intDiscountForClass.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpClass;
                        stpClass.Enabled = true;
                        break;
                    case 3: //项目优惠
                        tbcItem.MemberValue = dtDetail.Rows[rowIndex]["PromPro"];
                        cbbDiscountForItem.SelectedValue = dtDetail.Rows[rowIndex]["Prom"];
                        intDiscountForItem.Value = Convert.ToInt16(dtDetail.Rows[rowIndex]["DiscountNumber"]);
                        stiDetail.SelectedPanel = stpItem;
                        stpItem.Enabled = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能对本机构的方案进行操作！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DetailFlag == 0)
            {
                PromSunID = 0; //新增
            }

            if (CheckDetail() == false)
            {
                MessageBoxEx.Show("已存在相同条件的优惠项目！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int res = (int)InvokeController("SavePromotionProjectDetail", BuildPromotionProjectDetail(PromSunID));
            if (res > 0)
            {
                DataTable dtDetail = (DataTable)InvokeController("GetPromotionProjectDetail", PromID);
                dgDetail.DataSource = dtDetail;
                if (DetailFlag == 0)
                {
                    setGridSelectIndex(dgDetail, dgDetail.Rows.Count - 1);
                }
                else
                {
                    setGridSelectIndex(dgDetail);
                }

                cbbCardType.Focus();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDetail_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgDetail.Rows[e.RowIndex].Cells["FlagDesc"].Value);
            if (stopFlag == "停用")
            {
                dgDetail.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnStopDetail_Click(object sender, EventArgs e)
        {
            if (dgDetail.CurrentCell!=null)
            {
                int rowIndex = dgDetail.CurrentCell.RowIndex;
                DataTable dtDetail = dgDetail.DataSource as DataTable;
                PromSunID = Convert.ToInt16(dtDetail.Rows[rowIndex]["PromSunID"]);
                int useFlag = (Convert.ToInt16(dtDetail.Rows[rowIndex]["useFlag"]) == 1) ? 0 : 1;
                int res = (int)InvokeController("UpdateDetailFlag", PromSunID, useFlag);
                if (res>0)
                {
                    DataTable dt  = (DataTable)InvokeController("GetPromotionProjectDetail", PromID);
                    dgDetail.DataSource = dt;
                    setGridSelectIndex(dgDetail);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCopyProHead_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (int)InvokeController("GetWorkIDForLoginInfo"))
            {
                MessageBoxEx.Show("只能在操作员所属机构新增方案！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int res = (int)InvokeController("CopypPromotionProject", PromID);
            if (res > 0)
            {
                InvokeController("BindDgHead", Convert.ToInt16(cbbWork.SelectedValue), statRegDate.Bdate.Text, statRegDate.Edate.Text + " 23:59:59");
                setGridSelectIndex(dgHead, dgHead.Rows.Count);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            intDiscount.MaxValue = (Convert.ToInt16(cbbDiscount.SelectedValue) == 1)?  100 : 10000;
            labelX18.Text= (Convert.ToInt16(cbbDiscount.SelectedValue) == 1) ? "%" : string.Empty;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbDiscountForClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            intDiscountForClass.MaxValue = (Convert.ToInt16(cbbDiscountForClass.SelectedValue) == 1) ? 100 : 10000;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbDiscountForItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            intDiscountForItem.MaxValue = (Convert.ToInt16(cbbDiscountForItem.SelectedValue) == 1) ? 100 : 10000;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void stiDetail_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            switch(stiDetail.SelectedPanel.Name)
            {
                case "stpTotal":
                   // stiDetail.SelectedPanel = stpTotal;
                    stpTotal.Enabled = true;
                    stpClass.Enabled = false;
                    stpItem.Enabled = false;

                    cbbPromType.SelectedValue = 1;
                    break;
                case "stpClass":
                    stpTotal.Enabled = false;
                    stpClass.Enabled = true;
                    stpItem.Enabled = false;
                    cbbPromType.SelectedValue = 2;
                  //  stiDetail.SelectedPanel = stpClass;
                    break;
                case "stpItem":
                    stpTotal.Enabled = false;
                    stpClass.Enabled = false;
                    stpItem.Enabled = true;
                    cbbPromType.SelectedValue = 3;
                    // stiDetail.SelectedPanel = stpItem;
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbWork.SelectedValue)==(int)InvokeController("GetUserWorkID"))
            {
                btnNewProHead.Enabled = true;
                btnEditProHead.Enabled = true;
                btnDelProHead.Enabled = true;
                btnStopHead.Enabled = true;
                btnCopyProHead.Enabled = true;

                btnNewDetail.Enabled = true;
                btnEditDetail.Enabled = true;
                btnStopDetail.Enabled = true;
            }
            else
            {
                btnNewProHead.Enabled = false;
                btnEditProHead.Enabled = false;
                btnDelProHead.Enabled = false;
                btnStopHead.Enabled = false;
                btnCopyProHead.Enabled = false;

                btnNewDetail.Enabled = false;
                btnEditDetail.Enabled = false;
                btnSaveDetail.Enabled = false;
                btnNext.Enabled = false;
                btnCancelDetail.Enabled = false;
                btnStopDetail.Enabled = false;
            }
        }
    }
}
