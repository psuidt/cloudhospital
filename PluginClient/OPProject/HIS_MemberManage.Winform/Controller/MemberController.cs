using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 会员控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmAccountManage")]
    [WinformView(Name = "FrmAccountManage", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmAccountManage")]
    [WinformView(Name = "FrmAddScore", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmAddScore")]
    [WinformView(Name = "FrmAccountDetail", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmAccountDetail")]
    [WinformView(Name = "FrmGetGift", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmGetGift")]
    public class MemberController : WcfClientController
    {
        /// <summary>
        /// 帐户管理
        /// </summary>
        IFrmAccountManage frmAccountManage;

        /// <summary>
        /// 会员信息
        /// </summary>
        IFrmMemberInfo frmMemberInfo;   //会员

        /// <summary>
        /// 添加分数
        /// </summary>
        IFrmAddScore frmAddScore;

        /// <summary>
        /// 帐户明细
        /// </summary>
        IFrmAccountDetail frmAccountDetail;

        /// <summary>
        /// 所有基础数据
        /// </summary>
        DataSet dsALLinfo = new DataSet();

        /// <summary>
        /// 异步完成标志
        /// </summary>
        bool asynFlag = false; 

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmAccountManage = (IFrmAccountManage)iBaseView["FrmAccountManage"];
            frmMemberInfo = (IFrmMemberInfo)iBaseView["FrmMemberInfo"];
            frmAddScore = (IFrmAddScore)iBaseView["FrmAddScore"];
            frmAccountDetail= (IFrmAccountDetail)iBaseView["FrmAccountDetail"];
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        public override void AsynInit()
        {           
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetAllInfo");
            DataTable dtPatType = retdata.GetData<DataTable>(0);
             
            DataTable dtSex = retdata.GetData<DataTable>(1);
            DataTable dtNation = retdata.GetData<DataTable>(2);
            DataTable dtCity = retdata.GetData<DataTable>(3);
            DataTable dtDegree = retdata.GetData<DataTable>(4);
            DataTable dtRelation = retdata.GetData<DataTable>(5);
            DataTable dtCardType = retdata.GetData<DataTable>(6);
            DataTable dtOccupation = retdata.GetData<DataTable>(7);
            DataTable dtMatrimony = retdata.GetData<DataTable>(8);
            DataTable dtRoute = retdata.GetData<DataTable>(9);
            DataTable dtNationality = retdata.GetData<DataTable>(10);

            dsALLinfo.Clear();
            dtPatType.TableName = "dtPatType";
            dsALLinfo.Tables.Add(dtPatType);
            dtSex.TableName = "dtSex";
            dsALLinfo.Tables.Add(dtSex);
            dtNation.TableName = "dtNation";
            dsALLinfo.Tables.Add(dtNation);
            dtCity.TableName = "dtCity";
            dsALLinfo.Tables.Add(dtCity);
            dtDegree.TableName = "dtDegree";
            dsALLinfo.Tables.Add(dtDegree);
            dtRelation.TableName = "dtRelation";
            dsALLinfo.Tables.Add(dtRelation);
            dtCardType.TableName = "dtCardType";
            dsALLinfo.Tables.Add(dtCardType);
            dtOccupation.TableName = "dtOccupation";
            dsALLinfo.Tables.Add(dtOccupation);
            dtMatrimony.TableName = "dtMatrimony";
            dsALLinfo.Tables.Add(dtMatrimony);
            dtRoute.TableName = "dtRoute";
            dsALLinfo.Tables.Add(dtRoute);
            dtNationality.TableName = "dtNationality";
            dsALLinfo.Tables.Add(dtNationality);
            asynFlag = true;
        }

        /// <summary>
        /// 异步加载完成
        /// </summary>
        public override void AsynInitCompleted()
        {            
            frmMemberInfo.BindAllInfo(dsALLinfo.Tables["dtPatType"], dsALLinfo.Tables["dtSex"], dsALLinfo.Tables["dtNation"], dsALLinfo.Tables["dtCity"], dsALLinfo.Tables["dtDegree"], dsALLinfo.Tables["dtRelation"], dsALLinfo.Tables["dtCardType"], dsALLinfo.Tables["dtOccupation"], dsALLinfo.Tables["dtMatrimony"], dsALLinfo.Tables["dtRoute"], dsALLinfo.Tables["dtNationality"]);       
        }

        #region 会员信息管理
        /// <summary>
        /// 获取默认值
        /// </summary>
        [WinformMethod]
        public void GetDefaultValue()
        {
            frmMemberInfo.SetDefaultValue(dsALLinfo);
        }

        /// <summary>
        /// 查询会员信息
        /// </summary>
        [WinformMethod]
        public void QueryMemInfo()
        {            
        }

        /// <summary>
        /// 绑定组织机构
        /// </summary>
        [WinformMethod]
        public void BindWorkInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(true);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetWorkInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccountManage.BindWorkInfo(dt);
        }

        /// <summary>
        /// 绑定会员信息
        /// </summary>
        /// <param name="pageNO">页码</param>
        /// <param name="pageSize">每页条数</param>
        [WinformMethod]
        public void BindMemberInfo(int pageNO,int pageSize)
        {
            if (string.IsNullOrEmpty(frmAccountManage.WorkID) == true)
            {
                frmAccountManage.WorkID = "-1";   
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(pageNO);
                request.AddData(pageSize);
                request.AddData(frmAccountManage.WorkID);
                request.AddData(frmAccountManage.MemberName);
                request.AddData(frmAccountManage.Mobile);
                request.AddData(frmAccountManage.StDate);
                request.AddData(frmAccountManage.EndDate);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetMemberInfoPage", requestAction);

            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccountManage.BindMemberInfo(dt, retdata.GetData<int>(1));
        }

        /// <summary>
        /// 弹出新增会员窗体
        /// </summary>
        /// <param name="newFlag">新会员id</param>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="cardNO">卡号</param>
        /// <param name="accountID">帐户id</param>
        /// <param name="pageNO">页号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="rowsIndex">行索引</param>
        /// <returns>保存标识</returns>
        [WinformMethod]
        public int ShowMemberInfo(int newFlag,int cardTypeID,string cardNO,int accountID,int pageNO,int pageSize,int rowsIndex)
        {                      
            frmMemberInfo.NewFlag = newFlag;

            switch (newFlag)
            {
                case 1:
                   
                    frmMemberInfo.CardNO = string.Empty;
                    frmMemberInfo.PageNO = pageNO;
                    frmMemberInfo.PageSize = pageSize;
                    frmMemberInfo.MemberGridIndex = rowsIndex;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增会员信息";
                    break;
                case 2:
                     
                    BindAllInfo();
                    frmMemberInfo.PageNO = pageNO;
                    frmMemberInfo.PageSize = pageSize;
                    frmMemberInfo.MemberGridIndex = rowsIndex;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改会员信息";
                    break;
                case 3:
                    frmMemberInfo.CardNO = string.Empty;
                    BindAllInfo();
                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增帐户信息";
                    frmMemberInfo.AccountGridInex = rowsIndex;
                    break;
                case 4:
                    frmMemberInfo.CardNO = cardNO;
                    frmMemberInfo.CardTypeID = cardTypeID;
                    frmMemberInfo.AccountID = accountID;
                    BindAllInfo();
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改帐户信息";
                    frmMemberInfo.AccountGridInex = rowsIndex;
                    break;
                case 5:
                    frmMemberInfo.CardNO = string.Empty;

                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增会员信息";
                    break;
                case 6:
                    frmMemberInfo.CardNO = string.Empty;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改会员信息";
                    break;
            }

            (iBaseView["FrmMemberInfo"] as Form).ShowDialog();
            return frmMemberInfo.SaveResult;
        }

        /// <summary>
        /// 绑定下拉列表信息
        /// </summary>
        [WinformMethod]
        public void BindAllInfo()
        {
            if ((frmMemberInfo.NewFlag != 1) && (frmMemberInfo.NewFlag != 5))
            {
                frmMemberInfo.MemberInfoEntity = ConvertExtend.ToObject<ME_MemberInfo>(frmAccountManage.MemberTable, frmAccountManage.SelectMemberIndex);
            }
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <param name="newFlag">新会员标识</param>
        /// <param name="memberInfo">会员信息实体</param>
        /// <param name="memberAccount">帐户信息实体</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public bool SaveMemberEntity(int newFlag,ME_MemberInfo memberInfo,ME_MemberAccount memberAccount)
        {
            memberInfo.OpenDate = System.DateTime.Now;
            memberInfo.OperateDate = System.DateTime.Now;
            memberInfo.OperateID = LoginUserInfo.EmpId;

            //默认值没有发生作用
            memberInfo.UseFlag = 1;

            //注册机构
            memberInfo.RegisterWork = LoginUserInfo.WorkId;                 

            memberAccount.OperateID = LoginUserInfo.EmpId;
            
            Action <ClientRequestData> requestAction = ((ClientRequestData request) =>
            {           
                request.AddData(memberInfo);
                request.AddData(memberAccount);        
                request.AddData(newFlag);

                if (newFlag==4)  //修改帐户号码
                {
                    request.AddData(frmMemberInfo.AccountID);
                }
                else
                {
                    request.AddData(0);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "SaveMemberInfo", requestAction);
            frmMemberInfo.SaveResult = retdata.GetData<int>(1);
            return retdata.GetData<bool>(0);           
        }

        /// <summary>
        /// 其他系统调用保存会员信息
        /// </summary>
        /// <param name="newFlag">新会员标识</param>
        /// <param name="memberInfo">会员信息实体</param>
        /// <param name="memberAccount">会员帐户实体</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int SaveMemberEntity2(int newFlag, ME_MemberInfo memberInfo, ME_MemberAccount memberAccount)
        {
            memberInfo.OpenDate = System.DateTime.Now;
            memberInfo.OperateDate = System.DateTime.Now;
            memberInfo.OperateID = LoginUserInfo.EmpId;
            memberInfo.UseFlag = 1;  //默认值没有发生作用
            memberInfo.RegisterWork = LoginUserInfo.WorkId;                 //注册机构

            memberAccount.OperateID = LoginUserInfo.EmpId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberInfo);
                request.AddData(memberAccount);
                request.AddData(newFlag);

                if (newFlag == 4)  //修改帐户号码
                {
                    request.AddData(memberAccount.AccountID);
                }
                else
                {
                    request.AddData(0);
                }
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "SaveMemberInfo", requestAction);
            return retdata.GetData<int>(1);
        }

        /// <summary>
        /// 检测该帐户类型下帐户号码是否已经使用
        /// </summary>
        /// <returns>true使用</returns>
        [WinformMethod]
        public bool CheckCardNO()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(Convert.ToInt16(frmMemberInfo.CardTypeID));
                request.AddData(frmMemberInfo.CardNO);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "CheckCardNO", requestAction);
            bool isUse = retdata.GetData<bool>(0);
            return isUse;
        }

        /// <summary>
        /// 设置新会员标识
        /// </summary>
        /// <param name="flag">新会员标识</param>
        [WinformMethod] 
        public void SetNewMember(int flag)
        {
            frmMemberInfo.NewFlag = flag;
        }

        /// <summary>
        /// 更新会员状态
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="useFlag">会员状态，1、有效；0、停用</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdateMemberUseFlag(int memberID, int useFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
                request.AddData(useFlag);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "UpdateMemberUseFlag", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }
        #endregion 

        /// <summary>
        /// 根椐会员ID获取帐户信息
        /// </summary>
        /// <param name="memberID">会员ID</param>
        [WinformMethod]
        public void GetMemberAccount(int memberID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetMemberAccountInfo", requestAction);
            frmAccountManage.AccountTable = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 更新帐户有效标志
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="useFlag">有效标志，1、有效；0、停用</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdateAccountUseFlag(int accountID, int useFlag )
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(useFlag);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "UpdateAccountUseFlag", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }

        /// <summary>
        /// 新增帐户信息效验
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="cardNO">帐户号码</param>
        /// <param name="cardType">帐户类型</param>
        /// <returns>true,表示会员没有该类型帐户</returns>
        [WinformMethod]
        public bool[] CheckNewAccount(int memberID,string cardNO,int cardType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
                request.AddData(cardNO);
                request.AddData(cardType);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "CheckNewAccount", requestAction);
            bool typeIsUse = retdata.GetData<bool>(0);   //true,表示会员没有该类型帐户
            bool noIsUse= retdata.GetData<bool>(1);      //false, 表示会员选帐户类型与帐户号码有效

            bool[] res=new bool[] { typeIsUse, noIsUse } ;
            return res;
        }

        /// <summary>
        /// 帐户积分清零
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="score">积分</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int ClearAccountScore(int accountID,int score)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(score);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "ClearAccountScore", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }

        /// <summary>
        /// 指定机构所有帐户积分归零
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int ClearAllAccountScore(int workID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "ClearAllAccountScore", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }

        /// <summary>
        /// 修改帐户功能下的帐户号码有效性检验
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="cardType">帐户类型</param>
        /// <param name="cardNO">帐户号码</param>
        /// <returns>true存在false不存在</returns>
        [WinformMethod]
        public bool CheckCardNOForEdit(int accountID, int cardType, string cardNO)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(cardType);
                request.AddData(cardNO);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "CheckCardNOForEdit", requestAction);
            bool isCheck = retdata.GetData<bool>(0);
            return isCheck;
        }

        /// <summary>
        /// 打开充值窗体
        /// </summary>
        /// <param name="dr">帐户数据行</param>
        /// <param name="memberName">会员名称</param>
        [WinformMethod]
        public void OpenAddScore(DataRow dr,string memberName)
        {
            frmAddScore.AccountDr = dr;
            frmAddScore.MemberName = memberName;
            (iBaseView["FrmAddScore"] as Form).ShowDialog();           
        }

        /// <summary>
        /// 获取指定机构的指定帐户的积分兑换规则
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="cardType">帐户类型</param>
        /// <returns>转换分数数据行</returns>
        [WinformMethod]
        public DataRow GetConvertPoints(int workID,int cardType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(cardType);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetConvertPoints", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                return dr;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存积分明细
        /// </summary>
        /// <param name="scoreList">明细明细实体类</param>
        /// <param name="oldScore">原分数</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int SaveAddScore(ME_ScoreList scoreList,int oldScore)
        {
            scoreList.OperateDate = DateTime.Now;
            scoreList.OperateID = LoginUserInfo.EmpId;
            Action <ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(scoreList);
                request.AddData(oldScore);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "SaveAddScore", requestAction);
            int res = retdata.GetData<int>(0);

            return res;
        }

        /// <summary>
        /// 获取积分明细列表
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        /// <returns>积分明细数据</returns>
        [WinformMethod]
        public DataTable QueryAccountScoreList(int accountID,string stDate,string endDate,int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(stDate);
                request.AddData(endDate);
                request.AddData(flag);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "QueryAccountScoreList", requestAction);
            return   retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 显示帐户明细
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        [WinformMethod]
        public void ShowAccountDetail(int accountID, string stDate, string endDate, int flag)
        {
            frmAccountDetail.ScoreTable = QueryAccountScoreList(accountID, stDate, endDate, flag);
            frmAccountDetail.AccountID = accountID;
            frmAccountDetail.BDate = stDate;
            frmAccountDetail.EDate = endDate;
            ((Form)frmAccountDetail).ShowDialog();
        }
    }
}