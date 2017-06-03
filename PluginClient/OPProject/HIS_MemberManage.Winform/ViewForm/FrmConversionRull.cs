using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmConversionRull : BaseFormBusiness, IFrmConversionRull
    {
        /// <summary>
        /// 使用标识
        /// </summary>
        public int UseFlag = 1;

        /// <summary>
        /// 礼品id
        /// </summary>
        private int giftID;

        /// <summary>
        /// Gets or sets 礼品id
        /// </summary>
        ///  <value>礼品id</value>
        public int GiftID
        {
            get
            {
                return giftID;
            }

            set
            {
                giftID=value  ;
            }
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmConversionRull" /> class.
        /// </summary>
        public FrmConversionRull()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmConversionRull_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(dgGiftInfo);
            BindWorkInfo();
            BindCardTypeInfo(Convert.ToInt16(cbbWork.SelectedValue));
            int workID = Convert.ToInt16(cbbWork.SelectedValue);
            
            int cardTypeID= Convert.ToInt16(cbbCardType.SelectedValue);
            BindGiftInfo(workID, cardTypeID);
            SetContol(2);
        }

        /// <summary>
        /// 绑定机构信息
        /// </summary>
        public void BindWorkInfo()
        {
            bindGridSelectIndex(dgGiftInfo);
            DataTable dt = (DataTable)InvokeController("BindWorkInfo");
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dt;
            cbbWork.SelectedValue = (InvokeController("this") as AbstractController).WorkId;
        }

        /// <summary>
        /// 绑定卡类型
        /// </summary>
        /// <param name="workID">机构id</param>
        public void BindCardTypeInfo(int workID)
        {
            DataTable dt = (DataTable)InvokeController("BindCardTypeInfo", workID);
            cbbCardType.DisplayMember = "CardTypeName";
            cbbCardType.ValueMember = "CardTypeID";
            cbbCardType.DataSource = dt;

            DataTable dt1 = dt.Copy();

            cbbCardTypeName.DisplayMember = "CardTypeName";
            cbbCardTypeName.ValueMember = "CardTypeID";
            cbbCardTypeName.DataSource = dt1;
        }

        /// <summary>
        /// 绑定礼品信息
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        public void BindGiftInfo(int workID,int cardTypeID)
        {
            DataTable dt = (DataTable)InvokeController("BindGiftInfo", workID, cardTypeID);
            dgGiftInfo.DataSource = dt;
            txtWorkName.Text = Convert.ToString(cbbWork.Text);
        }

        /// <summary>
        /// 绑定礼品网格
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        public void BinddgGift(int workID,int cardTypeID)
        {
            DataTable dt = (DataTable)InvokeController("BindCardTypeInfo", workID);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btkNew_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue)!= (InvokeController("this") as AbstractController).WorkId)
            {
                MessageBoxEx.Show("只能新增登录用户所有机构的礼品兑换设置！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetContol(1);                        
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiEdit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (InvokeController("this") as AbstractController).WorkId)
            {
                MessageBoxEx.Show("只能编辑登录用户所有机构的礼品兑换设置！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UseFlag == 0)
            {
                MessageBoxEx.Show("礼品处于停用状态不能编辑！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGift.Focus();
                return;
            }

            SetContol(3);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiStop_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbbWork.SelectedValue) != (InvokeController("this") as AbstractController).WorkId)
            {
                MessageBoxEx.Show("只能对登录用户所有机构的礼品兑换设置的状态进行更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgGiftInfo.CurrentCell!=null)
            {
                int rowIndex = dgGiftInfo.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgGiftInfo.DataSource;
                int useFlag = Convert.ToInt16(dt.Rows[rowIndex]["UseFlag"]);
                int giftID = Convert.ToInt16(dt.Rows[rowIndex]["GiftID"]);
                if (useFlag==1)
                {
                    if (MessageBoxEx.Show(" 此礼品兑换设置将停用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    InvokeController("UpdateGiftFlag", giftID, 0);
                }
                else
                {
                    if (MessageBoxEx.Show(" 此礼品兑换设置将启用，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    InvokeController("UpdateGiftFlag", giftID, 1); 
                }

                BindGiftInfo(Convert.ToInt16(cbbWork.SelectedValue), Convert.ToInt16(cbbCardTypeName.SelectedValue));
                BtnQuery_Click(null, null);
                setGridSelectIndex(dgGiftInfo);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgGiftInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgGiftInfo.CurrentCell!=null)
            {
                int rowIndex = dgGiftInfo.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgGiftInfo.DataSource;

                txtWorkName.Text = Convert.ToString(dt.Rows[rowIndex]["WorkName"]);
                cbbCardTypeName.SelectedValue = Convert.ToString(dt.Rows[rowIndex]["CardTypeID"]);
                txtGift.Text = Convert.ToString(dt.Rows[rowIndex]["GiftName"]);
                intScore.Value = Convert.ToInt16(dt.Rows[rowIndex]["Score"]);
                GiftID= Convert.ToInt16(dt.Rows[rowIndex]["GiftID"]);
                UseFlag= Convert.ToInt16(dt.Rows[rowIndex]["UseFlag"]);
                btiStop.Text = (UseFlag > 0) ? "停用" : "启用";
            } 
            else
            {
                txtWorkName.Text = string.Empty;
                txtGift.Text = string.Empty;
                intScore.Value = 1;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCloase_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        /// <param name="flag">标识</param>
        private void SetContol(int flag)
        {
            switch (flag)
            {
                case 1:   //新增状态
                    txtGift.Text = string.Empty;
                    btkNew.Enabled = false;
                    btiEdit.Enabled = false;
                    btiStop.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    txtGift.Enabled = true;
                    dgGiftInfo.Enabled = false;
                    cbbCardTypeName.Enabled = true;
                    intScore.Value = 1;
                    break;
                case 2:    //界面初始化状态
                    btkNew.Enabled = true;
                    btiEdit.Enabled = true;
                    btiStop.Enabled = true;
                    btnSave.Enabled = false;
                    txtGift.Enabled = false;
                    btnCancel.Enabled = false;
                    dgGiftInfo.Enabled = true;
                    cbbCardTypeName.Enabled = false;
                    break;
                case 3:    //进入编辑状态
                    btkNew.Enabled = false;
                    btiEdit.Enabled = false;
                    btiStop.Enabled = false;
                    txtGift.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    dgGiftInfo.Enabled = false;
                    cbbCardTypeName.Enabled = false;
                    break;
            }   
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtiCancel_Click(object sender, EventArgs e)
        {
            SetContol(2);
            dgGiftInfo.CursorChanged += new EventHandler(dgGiftInfo_CurrentCellChanged);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            int workID = Convert.ToInt16(cbbWork.SelectedValue);
            int cardTypeID = Convert.ToInt16(cbbCardType.SelectedValue);
            BindGiftInfo(workID, cardTypeID);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            int cardTypeID = Convert.ToInt16(cbbCardTypeName.SelectedValue);
            if (cardTypeID<1)
            {
                MessageBoxEx.Show("请选择帐户类型！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtGift.Text))
            {
                MessageBoxEx.Show("礼品名称必须填写！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((bool)InvokeController("RegexName", txtGift.Text))
            {
                MessageBoxEx.Show("礼品名称含有特殊字符请修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGift.Focus();
                return;
            }

            int rowindx = ((DataTable)dgGiftInfo.DataSource).Rows.Count;
            int workID = Convert.ToInt16(cbbWork.SelectedValue);
             
            bool saveFlag = cbbCardTypeName.Enabled;

            if (CheckGiftName(saveFlag, workID, cardTypeID, txtGift.Text) == false)
            {
                return;
            }            

            int res = SaveGiftInfo(cbbCardTypeName.Enabled);    
            if (res > 0)
            {
                BindGiftInfo(workID, cardTypeID);
                BtnQuery_Click(null, null);
                SetContol(2);
                if (saveFlag==true)
                {
                    setGridSelectIndex(dgGiftInfo, rowindx);
                    MessageBoxShowSimple("新增礼品信息成功！");
                }
                else
                {
                    setGridSelectIndex(dgGiftInfo);
                }                
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmConversionRull_Shown(object sender, EventArgs e)
        {
            SetContol(2);
        }

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <param name="flag">启用标识</param>
        /// <returns>1成功</returns>
        private int SaveGiftInfo(bool flag)
        {
            int giftID = 0;

            //TRUE表示新增
            if (flag==false) 
            {
                giftID = GiftID;
            }
            
            int cardTypeID = Convert.ToInt16(cbbCardTypeName.SelectedValue);
            int res = (int)InvokeController("SaveGiftInfo", giftID, cardTypeID, txtGift.Text, intScore.Value);
            return res;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgGiftInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string stopFlag = Convert.ToString(dgGiftInfo.Rows[e.RowIndex].Cells["UseFalgDesc"].Value);
            if (stopFlag == "停用")
            {
                dgGiftInfo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// 检查礼品名称
        /// </summary>
        /// <param name="flag">标识</param>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <returns>false重复</returns>
        private bool CheckGiftName(bool flag,int workID, int cardTypeID,string giftName)
        {
            bool nameFlag = true;
            if (flag==true)
            {
                nameFlag = (bool)InvokeController("ChcekGiftNameForADD", workID, cardTypeID, giftName);
                if (nameFlag == false)
                {
                    MessageBoxEx.Show("礼品名称重复请修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGift.Focus();
                }
            }
            else
            {
                nameFlag = (bool)InvokeController("ChcekGiftNameForEdit", workID, cardTypeID, giftName, GiftID);
                if (nameFlag == false)
                {
                    MessageBoxEx.Show("礼品名称重复请修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGift.Focus();                
                }
            }

            return nameFlag;
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
        private void cbbWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbWork.SelectedValue)==(int)InvokeController("GetUserWorkID"))
            {
                btkNew.Enabled = true;
                btiEdit.Enabled = true;
                btiStop.Enabled = true;
            }
            else
            {
                btkNew.Enabled = false;
                btiEdit.Enabled = false;
                btiStop.Enabled = false;
            }
        }
    }
}
