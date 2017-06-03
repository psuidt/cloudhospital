using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;
using HIS_Entity.MIManage;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmMemberInfo : BaseFormBusiness, IFrmMemberInfo
    {
        #region 接口定义
        /// <summary>
        /// 查询条件
        /// </summary>
        private string sqlCondition;

        /// <summary>
        /// Gets or sets 查询条件
        /// </summary>
        /// <value>查询条件</value>
        public string SqlCondition
        {
            get
            {
                return sqlCondition;
            }

            set
            {
                sqlCondition = value;
            }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string strTel
        {
            get
            {
                return txtTele.Text;
            }

            set
            {
                txtTele.Text = value;
            }
        }

        /// <summary>
        /// 基础数据集
        /// </summary>
        private DataSet baseDataSet;

        /// <summary>
        /// Gets or sets 基础数据集
        /// </summary>
        /// <value>基础数据集</value>
        public DataSet BaseDataSet
        {
            get
            {
                return baseDataSet;
            }

            set
            {
                baseDataSet = value;
            }
        }

        /// <summary>
        /// 页号
        /// </summary>
        private int pageNO;

        /// <summary>
        /// Gets or sets 页号
        /// </summary>
        /// <value>页号</value>
        public int  PageNO
        {
            get
            {
                return pageNO;
            }

            set
            {
                pageNO = value;
            }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        private int pageSize;

        /// <summary>
        /// Gets or sets 每页条数
        /// </summary>
        /// <value>每页条数</value>
        public int PageSize
        {
            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// 会员网格索引
        /// </summary>
        private int memberGridIndex;

        /// <summary>
        /// Gets or sets 会员网格索引
        /// </summary>
        /// <value>会员网格索引</value>
        public int  MemberGridIndex
        {
            get
            {
                return memberGridIndex;
            }

            set
            {
                memberGridIndex = value;
            }
        }

        /// <summary>
        /// 帐户网格索引
        /// </summary>
        private int accountGridInex;

        /// <summary>
        /// Gets or sets 帐户网格索引
        /// </summary>
        /// <value>帐户网格索引</value>
        public int AccountGridInex
        {
            get
            {
                return accountGridInex;
            }

            set
            {
                accountGridInex = value;
            }
        }

        /// <summary>
        /// 保存界面信息时返回
        /// </summary>
        private int saveResult;

        /// <summary>
        /// Gets or sets 保存界面信息时返回
        /// </summary>
        /// <value>保存界面信息时返回</value>
        public int  SaveResult
        {
            get
            {
                return saveResult;
            }

            set
            {
                saveResult = value;
            }
        }

        /// <summary>
        /// 新增标识
        /// </summary>
        private int newFlag;

        /// <summary>
        /// Gets or sets 新增标识
        /// </summary>
        /// <value>新增标识</value>
        public int NewFlag
        {
            get
            {
                return newFlag;
            }

            set
            {
                newFlag = value;
            }
        }

        /// <summary>
        /// 会员信息实体类
        /// </summary>
        private ME_MemberInfo memberInfoEntity = new ME_MemberInfo();

        /// <summary>
        /// Gets or sets 会员信息实体类
        /// </summary>
        /// <value>会员信息实体类</value>
        public ME_MemberInfo MemberInfoEntity
        {
            get
            {
                MemberEntity.GetValue<ME_MemberInfo>(memberInfoEntity);
                return memberInfoEntity;
            }

            set
            {
                memberInfoEntity = value;
                MemberEntity.Load(memberInfoEntity);
            }
        }

        /// <summary>
        /// Gets or sets 会员帐户类型
        /// </summary>
        /// <value>会员帐户类型</value>
        public int CardTypeID
        {
            get
            {
                return Convert.ToInt16(cbbCardType.SelectedValue);
            }

            set
            {
                cbbCardType.SelectedValue = value;
            }
        }

        /// <summary>
        /// Gets or sets 帐户号码
        /// </summary>
        /// <value>帐户号码</value>
        public string CardNO
        {
            get
            {
                return txtCardNO.Text.Trim();
            }

            set
            {
                txtCardNO.Text = value;
            }
        }

        /// <summary>
        /// 账号ID
        /// </summary>
        private int accountID;

        /// <summary>
        /// Gets or sets 账号ID
        /// </summary>
        /// <value>账号ID</value>
        public int AccountID
        {
            get
            {
                return accountID;
            }

            set
            {
                accountID = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmMemberInfo" /> class.
        /// </summary>
        public FrmMemberInfo()
        {
            InitializeComponent();
            MemberEntity.AddItem(txtName, "Name","姓名必须填写!",  EfwControls.CustomControl.InvalidType.Empty,null);    //姓名
            MemberEntity.AddItem(tbcSex, "Sex", "性别必须填写!", EfwControls.CustomControl.InvalidType.Empty, null);      //性别
            MemberEntity.AddItem(dtBirthday, "Birthday","出生日期必须填写!", EfwControls.CustomControl.InvalidType.Empty, null);  //出生年月日
            MemberEntity.AddItem(tbcMatrimony, "Matrimony");  //婚姻状况
            MemberEntity.AddItem(tbcPatType, "PatType", "病人类型必须填写!", EfwControls.CustomControl.InvalidType.Empty, null);  //病人类型
            MemberEntity.AddItem(txtMedicCard, "MedicareCard");  //医保卡号
            MemberEntity.AddItem(txtID, "IDNumber","身份证号码格式不对！",EfwControls.CustomControl.InvalidType.IDcard,null);   //身份证号           
            MemberEntity.AddItem(tbcCity, "CityCode", "所属地区必须填写！", EfwControls.CustomControl.InvalidType.Empty, null);    //所属地区
            MemberEntity.AddItem(textAdd, "Address");     //详细地址
            MemberEntity.AddItem(txtTele, "Mobile");       //手机

            //MemberEntity.AddItem(txtTele, "Mobile","手机号码必须填写！",EfwControls.CustomControl.InvalidType.Empty,null);       //手机
            MemberEntity.AddItem(txtAllergies, "Allergies");  //过敏史
            MemberEntity.AddItem(tbcRoute, "Route", "知晓途径必须选择！", EfwControls.CustomControl.InvalidType.Empty, null);       //知晓途径
            MemberEntity.AddItem(tbcNationality, "Nationality");  //国籍
            MemberEntity.AddItem(tbcNation, "Nation");  //民族
            MemberEntity.AddItem(tbcDegree, "Degree");  //文化程度
            MemberEntity.AddItem(tbcOccupation, "Occupation");  //过敏史
            MemberEntity.AddItem(txtWorkUnit, "WorkUnit");  //工作单位
            MemberEntity.AddItem(txtWorkTel, "WorkTele");  //公司电话
            MemberEntity.AddItem(txtWorkADD, "WorkADD");  //公司地址
            MemberEntity.AddItem(txtRelationName, "RelationName");  //联系人
            MemberEntity.AddItem(tbcRelation, "Relation");  //联系人关系 
            MemberEntity.AddItem(textRelationTele, "RelationTele");  //联系人电话           
        }

        /// <summary>
        /// 绑定基础数据
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        /// <param name="dtSex">性别</param>
        /// <param name="dtNation">民族</param>
        /// <param name="dtCity">城市</param>
        /// <param name="dtDegree">学历</param>
        /// <param name="dtRelation">关系</param>
        /// <param name="dtCardType">卡类型</param>
        /// <param name="dtOccupation">职业</param>
        /// <param name="dtMatrimony">婚姻状况</param>
        /// <param name="dtRoute">知晓途径</param>
        /// <param name="dtNationality">国籍</param>
        public void BindAllInfo(DataTable dtPatType, DataTable dtSex, DataTable dtNation, DataTable dtCity, DataTable dtDegree, DataTable dtRelation, DataTable dtCardType, DataTable dtOccupation, DataTable dtMatrimony, DataTable dtRoute, DataTable dtNationality)
        {
            //绑定病人类型
            tbcPatType.DisplayField = "PatTypeName";
            tbcPatType.MemberField = "PatTypeID";
            tbcPatType.CardColumn = "PatTypeCode|编码|60,PatTypeName|病人类型|auto";
            tbcPatType.QueryFieldsString = "PatTypeName,PYCode,WBCode";
            tbcPatType.ShowCardWidth = 260;
            tbcPatType.ShowCardDataSource = dtPatType;

            //性别
            tbcSex.DisplayField = "Name";
            tbcSex.MemberField = "Code";
            tbcSex.CardColumn = "Code|编码|60,Name|性别|auto";
            tbcSex.QueryFieldsString = "Name,Pym,Wbm";
            tbcSex.ShowCardWidth = 160;
            tbcSex.ShowCardDataSource = dtSex;

            //民族
            tbcNation.DisplayField = "Name";
            tbcNation.MemberField = "Code";
            tbcNation.CardColumn = "Code|编码|60,Name|民族|auto";
            tbcNation.QueryFieldsString = "Name,Pym,Wbm";
            tbcNation.ShowCardWidth = 200;
            tbcNation.ShowCardDataSource = dtNation;

            //文化程度
            tbcDegree.DisplayField = "Name";
            tbcDegree.MemberField = "Code";
            tbcDegree.CardColumn = "Code|编码|60,Name|文化程度|auto";
            tbcDegree.QueryFieldsString = "Name,Pym,Wbm";
            tbcDegree.ShowCardWidth = 200;
            tbcDegree.ShowCardDataSource = dtDegree;

            //职业
            tbcOccupation.DisplayField = "Name";
            tbcOccupation.MemberField = "Code";
            tbcOccupation.CardColumn = "Code|编码|60,Name|职业|auto";
            tbcOccupation.QueryFieldsString = "Name,Pym,Wbm";
            tbcOccupation.ShowCardWidth = 300;
            tbcOccupation.ShowCardDataSource = dtOccupation;

            //关系
            tbcRelation.DisplayField = "Name";
            tbcRelation.MemberField = "Code";
            tbcRelation.CardColumn = "Code|编码|60,Name|与联系人关系|auto";
            tbcRelation.QueryFieldsString = "Name,Pym,Wbm";
            tbcRelation.ShowCardWidth = 280;
            tbcRelation.ShowCardDataSource = dtRelation;

            //所属地区  tbcCity
            tbcCity.DisplayField = "Name";
            tbcCity.MemberField = "Code";
            tbcCity.CardColumn = "Code|编码|90,Name|地区|auto";
            tbcCity.QueryFieldsString = "Name,Pym,Wbm";
            tbcCity.ShowCardWidth = 360;
            tbcCity.ShowCardDataSource = dtCity;

            //婚姻状况
            tbcMatrimony.DisplayField = "Name";
            tbcMatrimony.MemberField = "Code";
            tbcMatrimony.CardColumn = "Code|编码|60,Name|婚姻状况|auto";
            tbcMatrimony.QueryFieldsString = "Name,Pym,Wbm";
            tbcMatrimony.ShowCardWidth = 200;
            tbcMatrimony.ShowCardDataSource = dtMatrimony;

            //卡类型  cbbCardType
            cbbCardType.DisplayMember = "CardTypeName";
            cbbCardType.ValueMember = "CardTypeID";
            cbbCardType.DataSource = dtCardType;

            //知晓途径
            tbcRoute.DisplayField = "RouteDesc";
            tbcRoute.MemberField = "RouteID";
            tbcRoute.CardColumn = "RouteID|编码|50,RouteDesc|知晓途径|auto";
            tbcRoute.QueryFieldsString = "RouteDesc";
            tbcRoute.ShowCardWidth = 200;
            tbcRoute.ShowCardDataSource = dtRoute;

            //国籍  dtNationality   tbcNationality
            tbcNationality.DisplayField = "Name";
            tbcNationality.MemberField = "Code";
            tbcNationality.CardColumn = "Code|编码|60,Name|国籍|auto";
            tbcNationality.QueryFieldsString = "Name,Pym,Wbm";
            tbcNationality.ShowCardWidth = 200;
            tbcNationality.ShowCardDataSource = dtNationality;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        public void FrmMemberInfo_OpenWindowBefore(object sender, EventArgs e)
        {            
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmMemberInfo_Load(object sender, EventArgs e)
        {
            DataTable dtCardType= (DataTable)InvokeController("GetCardType");
            SaveResult = -1;
            BindAllInfo(BaseDataSet.Tables["dtPatType"], BaseDataSet.Tables["dtSex"], BaseDataSet.Tables["dtNation"], BaseDataSet.Tables["dtCity"], BaseDataSet.Tables["dtDegree"], BaseDataSet.Tables["dtRelation"], dtCardType, BaseDataSet.Tables["dtOccupation"], BaseDataSet.Tables["dtMatrimony"], BaseDataSet.Tables["dtRoute"], BaseDataSet.Tables["dtgj"]);
            MemberEntity.Load<ME_MemberInfo>(MemberInfoEntity);
            SetDefaultValue(BaseDataSet);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {            
            this.Close();
            txtName.Focus();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckDataFormat()==false)
            {
                return;
            }
          //  txtCardNO.Text = txtTele.Text;
            ME_MemberAccount newAccountEntity = new ME_MemberAccount();
            switch (NewFlag)
            {
                case 1:  //新增会员信息时新增帐户信息
                         //帐户表实体 
                    ME_MemberAccount newAccountEntity1 = new ME_MemberAccount();
                    newAccountEntity1.CardNO = CardNO;                           //帐户号码
                    newAccountEntity1.CardTypeID = Convert.ToInt16(CardTypeID);  //帐户类型
                    newAccountEntity1.OperateDate = System.DateTime.Now;
                    newAccountEntity1.RegDate= System.DateTime.Now;
                    newAccountEntity1.UseFlag = 1;
                    MemberInfoEntity.MemberID = 0;//
                    MemberInfoEntity.RegisterDate= System.DateTime.Now;
                    SaveMemberInfo(MemberInfoEntity, newAccountEntity1);
                    break;
                case 2:  //修改会员信息时新增帐户信息

                    SaveMemberInfo(MemberInfoEntity, newAccountEntity);
                    break;
                case 3: //新增帐户信息
                    newAccountEntity.CardNO = CardNO;                           //帐户号码
                    newAccountEntity.CardTypeID = Convert.ToInt16(CardTypeID);  //帐户类型
                    newAccountEntity.OperateDate = System.DateTime.Now;
                    newAccountEntity.MemberID = MemberInfoEntity.MemberID;
                    newAccountEntity.RegDate= System.DateTime.Now;
                    newAccountEntity.UseFlag = 1;
                    SaveAccountInfo(MemberInfoEntity,newAccountEntity);
                    break;
                case 4: //修改帐户信息
                    newAccountEntity.CardNO = CardNO;                           //帐户号码
                    newAccountEntity.CardTypeID = Convert.ToInt16(CardTypeID);  //帐户类型
                    newAccountEntity.OperateDate = System.DateTime.Now;
                    newAccountEntity.MemberID = MemberInfoEntity.MemberID;
                    newAccountEntity.UseFlag = 1;
                    SaveAccountInfo(MemberInfoEntity,newAccountEntity);
                    break;
                case 5:
                    ME_MemberAccount newAccountEntity5 = new ME_MemberAccount();
                    newAccountEntity5.CardNO = CardNO;                           //帐户号码
                    newAccountEntity5.CardTypeID = Convert.ToInt16(CardTypeID);  //帐户类型
                    newAccountEntity5.OperateDate = System.DateTime.Now;
                    newAccountEntity5.UseFlag = 1;
                    newAccountEntity5.RegDate= System.DateTime.Now;
                    MemberInfoEntity.MemberID = 0;// 必须清空为0
                    MemberInfoEntity.RegisterDate = System.DateTime.Now;
                    SaveMemberInfo2(MemberInfoEntity, newAccountEntity5);
                    break;
                case 6:  //修改会员信息时新增帐户信息
                    SaveMemberInfo2(MemberInfoEntity, newAccountEntity);
                    break;
            }
        }

        /// <summary>
        /// 校验电话号码
        /// </summary>
        /// <param name="txt">字符串</param>
        /// <returns>true是电话号码</returns>
        public bool RegexTelPhone(string txt)
        {
            return true;
           // return new Regex(@"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$").IsMatch(txt);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (panelMember.Enabled == false)
            {
                txtCardNO.Text = string.Empty;
            }
            else
            {
                ClearText();
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearText()
        {
            txtAllergies.Text = string.Empty;
            txtCardNO.Text = string.Empty;
            txtName.Text = string.Empty;
            txtRelationName.Text = string.Empty;
            txtMedicCard.Text = string.Empty;
            txtTele.Text = string.Empty;
            txtID.Text = string.Empty;
            txtWorkADD.Text = string.Empty;
            txtWorkTel.Text = string.Empty;
            txtWorkUnit.Text = string.Empty;
            MemberEntity.Clear();
        }

        /// <summary>
        /// 保存新增或修改会员信息
        /// </summary>
        /// <param name="memberInfo">会员信息</param>
        /// <param name="memberAccount">帐户信息</param>
        private void SaveMemberInfo(ME_MemberInfo memberInfo,ME_MemberAccount memberAccount)
        {
            if ((NewFlag == 1)  || (NewFlag == 5))
            {
                memberInfo.MemberID = 0;
                memberAccount.AccountID = 0;
                //if (string.IsNullOrEmpty(txtCardNO.Text))
                //{                    
                //    MessageBoxEx.Show("帐户号码必须填写", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (txtCardNO.Text != "")
                {
                    bool checkCard = (bool)InvokeController("CheckCardNO");
                    if (checkCard)
                    {
                        MessageBoxEx.Show("帐户号码：" + txtCardNO.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (MemberEntity.Validate())
            {
                bool result = (bool)InvokeController("SaveMemberEntity", NewFlag, memberInfo, memberAccount);   //获取前台控制器返回结果
                
                if (result)
                {
                    if (NewFlag==1)
                    {
                        PageNO = 1;
                        PageSize = 20;
                        MemberGridIndex = 0;
                        PageNO = 1;
                    }

                    InvokeController("BindMemberInfo", SqlCondition,PageNO, PageSize, MemberGridIndex);
                    MemberEntity.Clear();
                    this.Close();
                }
                else
                {
                    if ((NewFlag == 1) || (NewFlag == 5))
                    {
                        MessageBoxEx.Show("新增会员信息与会员帐户信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBoxEx.Show("修改会员信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <param name="memberInfo">会员信息</param>
        /// <param name="memberAccount">会员账号信息</param>
        private void SaveMemberInfo2(ME_MemberInfo memberInfo, ME_MemberAccount memberAccount)
        {
            if (NewFlag == 5)
            {
                //if (string.IsNullOrEmpty(txtCardNO.Text))
                //{
                //    MessageBoxEx.Show("帐户号码必须填写", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (txtCardNO.Text != string.Empty)
                {
                    bool checkCard = (bool)InvokeController("CheckCardNO");
                    if (checkCard)
                    {
                        MessageBoxEx.Show("帐户号码：" + txtCardNO.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (MemberEntity.Validate())
            {
                SaveResult = (int)InvokeController("SaveMemberEntity2", NewFlag, memberInfo, memberAccount);   //获取前台控制器返回结果
                 
                if (SaveResult > 0)
                {     
                    MemberEntity.Clear();
                    this.Close();
                }
                else
                {
                    if (NewFlag == 5) 
                    {
                        MessageBoxEx.Show("新增会员信息与会员帐户信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBoxEx.Show("修改会员信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// 保存帐户信息
        /// </summary>
        /// <param name="memberInfo">会员实体</param>
        /// <param name="accountEntity">帐户实体</param>
        private void SaveAccountInfo(ME_MemberInfo memberInfo, ME_MemberAccount accountEntity)
        {
          //  txtCardNO.Text = txtTele.Text;
            if (NewFlag == 3)
            {
                bool[] checkCard = (bool[])InvokeController("CheckNewAccount", accountEntity.MemberID, txtCardNO.Text.Trim(),  Convert.ToInt16(cbbCardType.SelectedValue));

                if (checkCard[0]==false)
                {
                    MessageBoxEx.Show("会员已有该类型帐户，请更换帐户类型或退出！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (checkCard[1] ==true)
                {
                    MessageBoxEx.Show("手机号码：" + txtCardNO.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTele.Focus();
                    return;
                }
            }
            else
            {
                bool isCheck= (bool)InvokeController("CheckCardNOForEdit", accountEntity.AccountID, Convert.ToInt16(cbbCardType.SelectedValue), txtCardNO.Text.Trim());

                if (isCheck == false)
                {
                    MessageBoxEx.Show("手机号码：" + txtCardNO.Text.Trim() + "使用中，请更换！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTele.Focus();
                    return;
                }
            }

            bool result = (bool)InvokeController("SaveMemberEntity", NewFlag, memberInfo,accountEntity);   //获取前台控制器返回结果
            if (result)
            {
                 InvokeController("GetAccountInfo", accountEntity.MemberID);
                  
                InvokeController("BindAccount", AccountGridInex);
                this.Close();
            }
            else
            {
                if (NewFlag == 3)
                {
                    MessageBoxEx.Show("新增会员帐户信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBoxEx.Show("修改会员帐户信息保存失败！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }
        }

        /// <summary>
        /// 检查数据格式
        /// </summary>
        /// <returns>false格式有问题</returns>
        private bool CheckDataFormat()
        {
            bool res = true;

            if (Convert.ToDateTime(dtBirthday.Text) > DateTime.Now)
            {
                MessageBoxEx.Show("出生日期不能大于当前日期！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                res = false;
            }

            if (RegexTelPhone(txtTele.Text) == false)
            {
                MessageBoxEx.Show("手机号码格式不对！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                res = false;
            }

            if (string.IsNullOrEmpty(txtWorkTel.Text) == false)
            {
                if (RegexTelPhone(txtWorkTel.Text) == false)
                {
                    MessageBoxEx.Show("单位电话格式不对！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    res = false;
                }
            }

            if (string.IsNullOrEmpty(textRelationTele.Text) == false)
            {
                if (RegexTelPhone(textRelationTele.Text) == false)
                {
                    MessageBoxEx.Show("联系人电话格式不对！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    res = false;
                }
            }

            return res;
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <param name="ds">数据集</param>
        public void SetDefaultValue(DataSet ds)
        {
            switch (NewFlag)
            {
                case 1:   //新增会员信息
                    txtCardNO.Text = string.Empty;
                   // txtCardNO.Enabled = true;
                    cbbCardType.Enabled = true;
                    SetControlState(true);
                    MemberEntity.Clear();
                    txtName.Focus();
                    dtBirthday.MonthCalendar.FirstDayOfWeek = DayOfWeek.Monday;
                    break;
                case 2:  //修改会员信息
                    txtCardNO.Text = string.Empty;
                   // txtCardNO.Enabled = false;
                    cbbCardType.Enabled = false;
                    SetControlState(true);
                    dtBirthday.MonthCalendar.FirstDayOfWeek = DayOfWeek.Monday;
                    txtName.Focus();
                    break;
                case 3:  //新增帐户信息
                    SetControlState(false);
                    txtCardNO.Enabled = true;
                    cbbCardType.Enabled = true;
                    txtCardNO.Text = string.Empty;
                    break;
                case 4:  //修改帐户信息
                    //panelMember.Enabled = false;
                    SetControlState(false);
                    txtCardNO.Text = CardNO;
                    cbbCardType.SelectedValue = CardTypeID;
                   // txtCardNO.Enabled = true;
                    cbbCardType.Enabled = false;
                    break;
                case 5:  //其他功能调用新增会员
                    txtCardNO.Text = string.Empty;
                  //  txtCardNO.Enabled = true;
                    cbbCardType.Enabled = true;
                    SetControlState(true);
                    MemberEntity.Clear();
                    dtBirthday.MonthCalendar.FirstDayOfWeek = DayOfWeek.Sunday;
                    txtName.Focus();
                    break;
                case 6:  //其他功能调用修改会员信息
                    txtCardNO.Text = string.Empty;
                  //  txtCardNO.Enabled = false;
                    cbbCardType.Enabled = false;
                    panelMember.Enabled = true;
                    dtBirthday.MonthCalendar.FirstDayOfWeek = DayOfWeek.Sunday;
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void TextRelationTele_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13)
            {
                cbbCardType.Focus();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmMemberInfo_Shown(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        /// <summary>
        /// 根椐任务修改控件状态
        /// </summary>
        /// <param name="flag">使用状态</param>
       private void SetControlState(bool flag)
       {
            foreach (System.Windows.Forms.Control control in this.panelMember.Controls)
            {
                if (control is DevComponents.DotNetBar.Controls.TextBoxX)
                {
                    TextBoxX tb = (TextBoxX)control;
                    if (tb.Name!= "txtCardNO")
                    {
                        tb.Enabled = flag;
                    }
                }

                if (control is EfwControls.CustomControl.TextBoxCard)
                {
                    TextBoxCard tb = (TextBoxCard)control;
                    tb.Enabled = flag;
                }

                dtBirthday.Enabled = flag;
            }
        }

        private void btnReadYB_Click(object sender, EventArgs e)
        {
            PatientInfo patientInfo = (PatientInfo)InvokeController("GetMedcareInfo");
            if (patientInfo != null)
            {
                txtName.Text = patientInfo.PersonName;
                tbcSex.MemberValue = patientInfo.Sex;
                dtBirthday.Value = DateTime.ParseExact(patientInfo.Birthday, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                txtMedicCard.Text = patientInfo.CardNo;
                //tbcPatType.Text = patientInfo.PersonType;
                txtID.Text = patientInfo.IdNo;
            }
        }
    }
}  
