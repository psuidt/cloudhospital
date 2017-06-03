using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;
using HIS_MemberManage.Winform.ViewForm;
using HIS_Entity.MIManage;
using HIS_MIInterface.Interface;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 会员管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMember")]
    [WinformView(Name = "FrmMember", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmMember")]
    [WinformView(Name = "FrmMemberInfo", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmMemberInfo")]
    [WinformView(Name = "FrmAddScore", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmAddScore")]
    [WinformView(Name = "FrmAccountDetail", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmAccountDetail")]
    [WinformView(Name = "FrmGetGift", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmGetGift")]
    [WinformView(Name = "FrmChangeCard", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmChangeCard")]
    public class NewMemberController : WcfClientController
    {
        /// <summary>
        /// 会员
        /// </summary>
        FrmMember frmMember;

        /// <summary>
        /// 会员信息
        /// </summary>
        IFrmMemberInfo frmMemberInfo;   //会员

        /// <summary>
        /// 添加积分
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
        /// 换卡
        /// </summary>
        IFrmChangeCard frmChangeCard;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmMember = (FrmMember)iBaseView["FrmMember"];
            frmMemberInfo = (FrmMemberInfo)iBaseView["FrmMemberInfo"];
            frmAddScore = (IFrmAddScore)iBaseView["FrmAddScore"];
            frmAccountDetail = (IFrmAccountDetail)iBaseView["FrmAccountDetail"];
            frmChangeCard=(IFrmChangeCard)iBaseView["FrmChangeCard"];
        }

        #region 会员信息
        /// <summary>
        /// 绑定机构信息
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
            frmMember.BindWorkInfo(dt);
        }

        /// <summary>
        /// 绑定会员信息
        /// </summary>
        /// <param name="sqlCondition">查询条件</param>
        /// <param name="pageNO">页号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="index">页索引</param>
        [WinformMethod]
        public void BindMemberInfo(string sqlCondition, int pageNO, int pageSize,int index)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(pageNO);
                request.AddData(pageSize);
                request.AddData(sqlCondition);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetMemberInfoPage", requestAction);
            frmMember.MemberTable = retdata.GetData<DataTable>(0);
            frmMember.Total = retdata.GetData<int>(1);
            frmMember.BindMemberInfo(frmMember.MemberTable, pageNO, pageSize, index);
        }        

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <param name="ds">基础数据集</param>
        /// <returns>基础数据集合数据</returns>
        [WinformMethod]
        public DataSet GetBaseDataOne(DataSet ds)
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetBaseDataOne");
            DataTable dtPatType = retdata.GetData<DataTable>(0);
            dtPatType.TableName = "dtPatType";
            if (ds.Tables.Contains("dtPatType") == false)
            {
                ds.Tables.Add(dtPatType);
            }

            DataTable dtSex = retdata.GetData<DataTable>(1);
            dtSex.TableName = "dtSex";
            if (ds.Tables.Contains("dtSex") == false)
            {
                ds.Tables.Add(dtSex);
            }

            DataTable dtNation = retdata.GetData<DataTable>(2);
            dtNation.TableName = "dtNation";
            if (ds.Tables.Contains("dtNation") == false)
            {
                ds.Tables.Add(dtNation);
            }

            DataTable dtCity = retdata.GetData<DataTable>(3);
            dtCity.TableName = "dtCity";
            if (ds.Tables.Contains("dtCity") == false)
            {
                ds.Tables.Add(dtCity);
            }

            DataTable dtDegree = retdata.GetData<DataTable>(4);
            dtDegree.TableName = "dtDegree";
            if (ds.Tables.Contains("dtDegree") == false)
            {
                ds.Tables.Add(dtDegree);
            }           

            return ds;
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>基础数据集合</returns>
        [WinformMethod]
        public DataSet GetBaseDataTwo(DataSet ds)
        { 
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetBaseDataTwo");
            DataTable dtRelation = retdata.GetData<DataTable>(0);
            dtRelation.TableName = "dtRelation";
            if (ds.Tables.Contains("dtRelation")==false)
            {
                ds.Tables.Add(dtRelation);
            }            

            DataTable dtOccupation = retdata.GetData<DataTable>(1);
            dtOccupation.TableName = "dtOccupation";
            if (ds.Tables.Contains("dtOccupation") == false)
            {
                ds.Tables.Add(dtOccupation);
            }

            DataTable dtMatrimony = retdata.GetData<DataTable>(2);
            dtMatrimony.TableName = "dtMatrimony";
            if (ds.Tables.Contains("dtMatrimony") == false)
            {
                ds.Tables.Add(dtMatrimony);
            }

            DataTable dtRoute = retdata.GetData<DataTable>(3);
            dtRoute.TableName = "dtRoute";
            if (ds.Tables.Contains("dtRoute") == false)
            {
                ds.Tables.Add(dtRoute);
            }

            DataTable dtgj = retdata.GetData<DataTable>(4);
            dtgj.TableName = "dtgj";
            if (ds.Tables.Contains("dtgj") == false)
            {
                ds.Tables.Add(dtgj);
            }

            return ds;
        }

        /// <summary>
        /// 绑定会员信息
        /// </summary>
        [WinformMethod]
        public void BindAllInfo()
        {
            if ((frmMemberInfo.NewFlag != 1) && (frmMemberInfo.NewFlag != 5))
            {
                frmMemberInfo.MemberInfoEntity = ConvertExtend.ToObject<ME_MemberInfo>(frmMember.MemberTable, frmMember.SelectMemberIndex);
            }
        }

        /// <summary>
        /// 弹出新增会员窗体
        /// </summary>
        /// <param name="newFlag">新增标识</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="cardNO">卡号</param>
        /// <param name="accountID">账户id</param>
        /// <param name="pageNO">页号</param>
        /// <param name="pageSize">页面条数</param>
        /// <param name="rowsIndex">行索引</param>
        /// <returns>保存标识</returns>
        [WinformMethod]
        public int ShowMemberInfo(int newFlag, int cardTypeID, string cardNO, int accountID, int pageNO, int pageSize, int rowsIndex)
        {
            frmMemberInfo.NewFlag = newFlag;

            switch (newFlag)
            {
                case 1:
                    frmMemberInfo.CardNO = string.Empty;
                    frmMemberInfo.PageNO = pageNO;
                    frmMemberInfo.PageSize = pageSize;
                    frmMemberInfo.MemberGridIndex = rowsIndex;
                    frmMemberInfo.BaseDataSet = frmMember.BaseData;
                    frmMemberInfo.SqlCondition = frmMember.SqlCondition;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增会员信息";
                    break;
                case 2:                  
                    frmMemberInfo.BaseDataSet = frmMember.BaseData;
                    BindAllInfo();
                    frmMemberInfo.PageNO = pageNO;
                    frmMemberInfo.PageSize = pageSize;
                    frmMemberInfo.MemberGridIndex = rowsIndex;
                    frmMemberInfo.SqlCondition = frmMember.SqlCondition;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改会员信息";
                    break;
                case 3:
                    frmMemberInfo.CardNO = string.Empty;
                    frmMemberInfo.BaseDataSet = frmMember.BaseData;
                    BindAllInfo();
                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增帐户信息";
                    frmMemberInfo.AccountGridInex = rowsIndex;
                    break;
                case 4:
                    frmMemberInfo.CardNO = cardNO;
                    frmMemberInfo.CardTypeID = cardTypeID;
                    frmMemberInfo.AccountID = accountID;
                    frmMemberInfo.BaseDataSet = frmMember.BaseData;
                    BindAllInfo();
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改帐户信息";
                    frmMemberInfo.AccountGridInex = rowsIndex;
                    break;
                case 5:
                    frmMemberInfo.CardNO = string.Empty;
                    frmMemberInfo.strTel = string.Empty;                  
                    DataSet ds = new DataSet();
                    ds = GetBaseDataOne(ds);
                    ds = GetBaseDataTwo(ds);
                    frmMemberInfo.BaseDataSet = ds;
                    (iBaseView["FrmMemberInfo"] as Form).Text = "新增会员信息";
                    break;
                case 6:
                    frmMemberInfo.CardNO = string.Empty;
                    DataSet dsUpdate = new DataSet();
                    dsUpdate = GetBaseDataOne(dsUpdate);
                    dsUpdate = GetBaseDataTwo(dsUpdate);
                    frmMemberInfo.BaseDataSet = dsUpdate;
                    frmMemberInfo.PageNO = pageNO;
                    frmMemberInfo.PageSize = pageSize;
                    frmMemberInfo.MemberGridIndex = 0;
                    GetMemberInfo(accountID);
                    (iBaseView["FrmMemberInfo"] as Form).Text = "修改会员信息";
                    break;
            }

            (iBaseView["FrmMemberInfo"] as Form).ShowDialog();
            return frmMemberInfo.SaveResult;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="memberId">会员Id</param>
        [WinformMethod]
        public void GetMemberInfo(int memberId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberId);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetMemberInfo", requestAction);
            DataTable dtMember = retdata.GetData<DataTable>(0);
            frmMemberInfo.NewFlag = 6;
            BindAllInfo(dtMember);
        }

        /// <summary>
        /// 绑定下拉列表信息
        /// </summary>
        /// <param name="dtMember">会员数据</param>
        [WinformMethod]
        public void BindAllInfo(DataTable dtMember)
        {
            frmMemberInfo.MemberInfoEntity = ConvertExtend.ToObject<ME_MemberInfo>(dtMember, 0);
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <param name="newFlag">新增标识</param>
        /// <param name="memberInfo">会员信息</param>
        /// <param name="memberAccount">会员帐户</param>
        /// <returns>true成功false失败</returns>
        [WinformMethod]
        public bool SaveMemberEntity(int newFlag, ME_MemberInfo memberInfo, ME_MemberAccount memberAccount)
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
        /// <param name="newFlag">新增标识</param>
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
        /// 检查账号是否启用
        /// </summary>
        /// <returns>true启用</returns>
        [WinformMethod]
        public bool CheckCardNO()
        {
            if (frmMemberInfo.CardNO != string.Empty)
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
            return false;
        }

        /// <summary>
        /// 更新会员状态
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="useFlag">会员状态，1、有效；0、停用</param>
        /// <returns>使用标识</returns>
        [WinformMethod]
        public int UpdateMemberUseFlag(int memberID, int useFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
                request.AddData(useFlag);
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "UpdateMemberUseFlag", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }

        /// <summary>
        /// 指定机构所有帐户积分归零
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <returns>1成功</returns>
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
        #endregion

        #region 帐户信息
        /// <summary>
        /// 新增帐户信息效验
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="cardNO">帐户号码</param>
        /// <param name="cardType">帐户类型</param>
        /// <returns>true,表示会员没有该类型帐户</returns>
        [WinformMethod]
        public bool[] CheckNewAccount(int memberID, string cardNO, int cardType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
                request.AddData(cardNO);
                request.AddData(cardType);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "CheckNewAccount", requestAction);
            
            //true,表示会员没有该类型帐户
            bool typeIsUse = retdata.GetData<bool>(0);

            //false, 表示会员选帐户类型与帐户号码有效
            bool noIsUse = retdata.GetData<bool>(1);      

            bool[] res = new bool[] { typeIsUse, noIsUse };
            return res;
        }

        /// <summary>
        /// 修改帐户功能下的帐户号码有效性检验
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="cardType">帐户类型</param>
        /// <param name="cardNO">帐户号码</param>
        /// <returns>true存在</returns>
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
        /// 获取帐户信息
        /// </summary>
        /// <param name="memberID">会员id</param>
        [WinformMethod]
        public void GetAccountInfo(int memberID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetMemberAccountInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMember.AccountTable = dt;
        }

        /// <summary>
        /// 绑定帐户
        /// </summary>
        /// <param name="index">索引</param>
        [WinformMethod]
        public void BindAccount(int index)
        {
            frmMember.BindAccountInfo(index);
        }

        /// <summary>
        /// 更新帐户使用标识
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdateAccountUseFlag(int accountID, int useFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(useFlag);
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "UpdateAccountUseFlag", requestAction);
            int isUse = retdata.GetData<int>(0);
            return isUse;
        }

        /// <summary>
        /// 打开充值窗体
        /// </summary>
        /// <param name="dr">帐户数据行</param>
        /// <param name="memberName">会员名称</param>
        [WinformMethod]
        public void OpenAddScore(DataRow dr, string memberName)
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
        /// <returns>转换分数规则数据行</returns>
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

            if (dt.Rows.Count > 0)
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
        /// <param name="payType">支付类型</param>
        /// <param name="cash">现金</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int SaveAddScore(ME_ScoreList scoreList, int oldScore,int payType,int cash)
        {
            scoreList.OperateDate = DateTime.Now;
            scoreList.OperateID = LoginUserInfo.EmpId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(scoreList);
                request.AddData(oldScore);
                request.AddData(payType);
                request.AddData(cash);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "SaveAddScore", requestAction);
            int res = retdata.GetData<int>(0);
            frmAddScore.TiecketCode= retdata.GetData<string>(1);

            return res;
        }

        /// <summary>
        /// 获取积分明细列表
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        /// <returns>积分明细数据</returns>
        [WinformMethod]
        public DataTable QueryAccountScoreList(int accountID, string stDate, string endDate, int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(stDate);
                request.AddData(endDate);
                request.AddData(flag);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "QueryAccountScoreList", requestAction);
            return retdata.GetData<DataTable>(0);
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

        /// <summary>
        /// 帐户积分清零
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="score">积分</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int ClearAccountScore(int accountID, int score)
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
        /// 取得机构ID
        /// </summary>
        /// <returns>机构id</returns>
        [WinformMethod]
        public int GetUserWorkID()
        {
            return GetUserInfo().WorkId;
        }

        /// <summary>
        /// 取得用户名
        /// </summary>
        /// <returns>用户名</returns>
        [WinformMethod]
        public string GetUserName()
        {
            return GetUserInfo().EmpName;
        }

        /// <summary>
        /// 用户机构名称
        /// </summary>
        /// <returns>机构名称</returns>
        [WinformMethod]
        public string GetUserWorkName()
        {
            return GetUserInfo().WorkName;
        }

        /// <summary>
        /// 取得卡类型
        /// </summary>
        /// <returns>卡类型记录</returns>
        [WinformMethod]
        public DataTable GetCardType()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetCardType");
            return retdata.GetData<DataTable>(0);
        }
        #endregion

        #region 换卡
        /// <summary>
        /// 取得换卡明细
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <returns>换卡明细数据</returns>
        [WinformMethod]
        public DataTable GetChangeCardList(int accountID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetChangeCardList", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 弹出换卡界面
        /// </summary>
        /// <param name="memberName">会员姓名</param>
        /// <param name="accountTypeName">帐户类型</param>
        /// <param name="accountID">帐户ID</param>
        /// <param name="oldCardNO">旧卡号</param>
        /// <param name="accountTypeID">帐户类型ID</param>
        /// <param name="memberID">会员Id</param>
        [WinformMethod]
        public void  ShowChangeCardList(string memberName,string accountTypeName, int accountID,string oldCardNO,int accountTypeID, int memberID)
        {
            frmChangeCard.AccountID = accountID;
            frmChangeCard.MemberName = memberName;
            frmChangeCard.AccountTypeName = accountTypeName;
            frmChangeCard.OldCardNO = oldCardNO;
            frmChangeCard.AccountTypeID = accountTypeID;
            frmChangeCard.MemberID = memberID;
         (iBaseView["FrmChangeCard"] as Form).ShowDialog();
        }

        /// <summary>
        /// 取得系统参数
        /// </summary>
        /// <returns>参数值</returns>
        [WinformMethod]
        public decimal GetSystemConfig()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetSystemConfig", requestAction);
            return retdata.GetData<decimal>(0);
        }

        /// <summary>
        /// 保存换卡信息
        /// </summary>
        /// <param name="list">换卡明细</param>
        /// <param name="payType">支付类型</param>
        /// <param name="memberID">会员id</param>
        [WinformMethod]
        public void SaveChangeCard(ME_ChangeCardList list,int payType,int memberID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                list.OperateID = LoginUserInfo.EmpId;

                request.AddData(list);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(payType);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "SaveChangeCardList", requestAction);
            int flag = retdata.GetData<int>(0);
            string tiecketCode= retdata.GetData<string>(1);
            if (flag>0)
            {              
                frmChangeCard.SetOldCard(list.NewCardNO);
                frmChangeCard.BindList();
                GetAccountInfo(memberID);

                //如果返回发票号码不为空则打印小票
                if (string.IsNullOrEmpty(tiecketCode) == false)
                {
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("PatName", frmChangeCard.MemberName);
                    myDictionary.Add("InvoiceNO", tiecketCode);
                    myDictionary.Add("CardNO", list.NewCardNO);
                    myDictionary.Add("Operator", LoginUserInfo.EmpName);
                    myDictionary.Add("ChargeDate", list.OperateDate);
                    myDictionary.Add("TotalFee", list.Amount);
                    myDictionary.Add("WtotalFee", CmycurD(list.Amount));
                    myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                    myDictionary.Add("TypeName", "换卡收费收据");

                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2020, 0, myDictionary, null).Print(true);        
                }
            }           
        }

        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        [WinformMethod]
        public string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str3 = string.Empty;    //从原num值中取出的值
            string str4 = string.Empty;    //数字的字符串形式
            string str5 = string.Empty; //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 = string.Empty;    //数字的汉语读法
            string ch2 = string.Empty;    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
            j = str4.Length;      //找出最高位
            if (j > 15)
            {
                return "溢出";
            }

            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = string.Empty;
                        ch2 = string.Empty;
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = string.Empty;
                                ch2 = string.Empty;
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = string.Empty;
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = string.Empty;
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }

                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }

            if (num == 0)
            {
                str5 = "零元整";
            }

            return str5;
        }
        #endregion

        [WinformMethod]
        public PatientInfo GetMedcareInfo()
        {
            HIS_Entity.MIManage.InputClass inputClasss = new HIS_Entity.MIManage.InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            inputClasss.SInput = dicStr;
            HIS_Entity.MIManage.ResultClass resultClass = MIInterFaceFactory.MZ_GetPersonInfo(inputClasss);
            if (resultClass.bSucess == true)
            {
                if (resultClass.sRemarks != "")
                {
                    MessageBox.Show(resultClass.sRemarks);
                }
                List<PatientInfo> patientInfoList = (List<PatientInfo>)resultClass.oResult;
                string patientState = "";
                if (patientInfoList[0].IsSpecifiedHosp.Trim() != "")
                {
                    patientState = patientInfoList[0].IsSpecifiedHosp.Contains("0") ? "本地红名单" : (patientInfoList[0].IsSpecifiedHosp.Contains("1") ? "本人定点医院" : (patientInfoList[0].IsSpecifiedHosp.Contains("2") ? "不是本人定点医院" : "转诊"));
                }
                string MedicardInfo = "姓名:" + patientInfoList[0].PersonName + " 定点医院:" + patientState + " 红名单:" + patientInfoList[0].IsInredList + " 余额:" + patientInfoList[0].PersonCount + " 身份证号码:" + patientInfoList[0].IdNo; //"12345678900987654";
                if (patientInfoList[0].IsInredList.Contains("false"))
                {
                    MessageBox.Show("卡号:" + patientInfoList[0].CardNo + " 姓名:" + patientInfoList[0].PersonName + " 定点医院:" + patientState + " 该病人不是本院红名单！");
                }
                if (patientInfoList[0].HospFlag.Trim() == "1")
                {
                    MessageBox.Show("卡号:" + patientInfoList[0].CardNo + " 姓名:" + patientInfoList[0].PersonName + " 定点医院:" + patientState + " 该病人已在院！");
                }
                return patientInfoList[0];
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }
        
    }
}
