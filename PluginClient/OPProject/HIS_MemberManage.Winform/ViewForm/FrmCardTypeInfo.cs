using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmCardTypeInfo : BaseFormBusiness, IFrmCardTypeInfo
    {
        /// <summary>
        /// 卡片前缀
        /// </summary>
        private string cardPrefix;

        /// <summary>
        /// Gets or sets 卡片前缀
        /// </summary>
        /// <value>卡片前缀</value>
        public string CardPrefix
        {
            get
            {
                return cardPrefix;
            }

            set
            {
                cardPrefix = value;
            }
        }

        /// <summary>
        /// 卡类型索引
        /// </summary>
        private int cardTypeIndex;

        /// <summary>
        /// Gets or sets 卡类型索引
        /// </summary>
        /// <value>卡类型索引</value>
        public int CardTypeIndex
        {
            get
            {
                return cardTypeIndex;
            }

            set
            {
                cardTypeIndex = value;
            }
        }
        
        /// <summary>
        /// 标识
        /// </summary>
        private int flag;

        /// <summary>
        /// Gets or sets 标识
        /// </summary>
        /// <value>标识</value>
        public int Flag
        {
            get
            {
                return flag;
            }

            set
            {
                flag = value;
            }
        }

        /// <summary>
        /// 卡类型id
        /// </summary>
        private int cardTypeID;

        /// <summary>
        ///  Gets or sets 卡类型id
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
        /// 卡类型
        /// </summary>
        private int cardType;

        /// <summary>
        /// Gets or sets 卡片类型
        /// </summary>
        /// <value>卡片类型</value>
        public int CardType
        {
            get
            {
                return cardType;
            }

            set
            {
                cardType = value;
            }
        }

        /// <summary>
        /// 卡类型描述
        /// </summary>
        private string cardTypeDesc;

        /// <summary>
        /// Gets or sets 卡类型描述
        /// </summary>
        /// <value>卡类型描述</value>
        public string CardTypeDesc
        {
            get
            {
                return cardTypeDesc;
            }

            set
            {
                cardTypeDesc = value;
            }
        }

        /// <summary>
        /// 卡接口描述
        /// </summary>
        private string cardInterfaceDesc;

        /// <summary>
        /// Gets or sets 卡接口描述
        /// </summary>
        /// <value>卡接口描述</value>
        public string CardInterfaceDesc
        {
            get
            {
                return cardInterfaceDesc;
            }

            set
            {
                cardInterfaceDesc = value;
            }
        }

        /// <summary>
        /// 会员信息实体类
        /// </summary>
        private ME_CardTypeList cardTypeList = new ME_CardTypeList();

        /// <summary>
        /// Gets or sets 会员信息实体类
        /// </summary>
        /// <value>会员信息实体类</value>
        public ME_CardTypeList MECardTypeList
        {
            get
            {
                CardTypeEntity.GetValue<ME_CardTypeList>(cardTypeList);
                return cardTypeList;
            }

            set
            {
                cardTypeList = value;
                CardTypeEntity.Load(cardTypeList);
            }
        }

        /// <summary>
        /// 绑定卡类型
        /// </summary>
        public void BindCardType()
        {
            var datasource = new[] 
            {
                new { Text = "磁条卡", Value = 1 },
                new { Text = "IC卡", Value = 2 },
                new { Text = "RFID卡", Value = 3 },                 
            };

            cbbCardType.ValueMember = "Value";
            cbbCardType.DisplayMember = "Text";
            cbbCardType.DataSource = datasource;
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmCardTypeInfo" /> class.
        /// </summary>
        public FrmCardTypeInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmCardTypeInfo_Load(object sender, EventArgs e)
        {
            BindCardType();
            txtCardType.Text = CardTypeDesc;
            cbbCardType.SelectedValue = CardType;          
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {   
            if (string.IsNullOrEmpty(txtCardType.Text))
            {
                MessageBoxEx.Show("卡片类型名称不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCardType.Focus();
                return;
            }
            else
            {
                bool flag = RegexName(txtCardType.Text);
                if (flag==true)
                {
                    MessageBoxEx.Show("卡片类型名称不能含有特殊字符！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }             
            
            CardTypeDesc = txtCardType.Text.Trim();
            CardType = (int)cbbCardType.SelectedValue;
            CardInterfaceDesc = string.Empty;
            CardPrefix = string.Empty;

            //新增卡片类型
            if (CardTypeID==0)
            {               
                bool check = (bool)InvokeController("CheckCardTypeNameForADD", CardTypeDesc);
                if (check==false)
                {
                    MessageBoxEx.Show("卡片类型名称：" + txtCardType.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }          
            }
            else
            {                
                bool check = (bool)InvokeController("CheckCardTypeNameForEdit", CardTypeDesc,CardTypeID);
                if (check == false)
                {
                    MessageBoxEx.Show("卡片类型名称：" + txtCardType.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int res = (int)InvokeController("SaveCardTypeInfo", CardTypeID, CardTypeDesc, CardInterfaceDesc, CardType,Flag,CardPrefix);
            if (res > 0)
            {
                InvokeController("BindCardTypeDataSource", CardTypeIndex);
                this.Close();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtCardType_KeyPress(object sender, KeyPressEventArgs e)
        {           
            if (e.KeyChar==13)
            {
                cbbCardType.Focus();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtCardType_KeyUp(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {             
        }

        /// <summary>
        /// 检查名称
        /// </summary>
        /// <param name="txt">名称字符串</param>
        /// <returns>true有特殊字符</returns>
        public bool RegexName(string txt)
        {
            Regex rx = new Regex(@"[`~!@#$%^&*()+=|{}':;',\[\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]+");
            return rx.IsMatch(txt);
        }
    }
}
