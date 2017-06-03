using System;
using System.Data;
using System.Text.RegularExpressions;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 礼品控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmConversionRull")]//在菜单上显示
    [WinformView(Name = "FrmConversionRull", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmConversionRull")]
    public class GiftController: WcfClientController
    {
        /// <summary>
        /// 积分转换界面
        /// </summary>
        IFrmConversionRull frmConversionRull;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmConversionRull = (IFrmConversionRull)iBaseView["FrmConversionRull"];         
        }

        /// <summary>
        /// 绑定机构信息
        /// </summary>
        /// <returns>机构信息</returns>
        [WinformMethod]
        public DataTable BindWorkInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(false);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetWorkInfo", requestAction);
            return  retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 绑定帐户类型信息
        /// </summary>
        /// <param name="workID">组织机构ID</param>
        /// <returns>帐户类型信息</returns>
        [WinformMethod]
        public DataTable BindCardTypeInfo(int workID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetCardTypeForWork", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 获取礼品数据
        /// </summary>
        /// <param name="workID">组织机构Id</param>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <returns>礼品数据</returns>
        [WinformMethod]
        public DataTable BindGiftInfo(int workID,int cardTypeID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(cardTypeID);
                request.AddData(0);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetGiftInfo", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 校验礼品名称中是否有特殊字符
        /// </summary>
        /// <param name="txt">礼品名称</param>
        /// <returns>true存在false不存在</returns>
        [WinformMethod]
        public bool RegexName(string txt)
        {
            Regex rx = new Regex(@"[`~!@#$%^&*()+=|{}':;',\[\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]+");
            return rx.IsMatch(txt);
        }

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <param name="giftID">礼品id</param>
        /// <param name="cardTypeID">卡类型Id</param>
        /// <param name="giftName">礼品名称</param>
        /// <param name="score">分数</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int SaveGiftInfo(int giftID,int cardTypeID,string giftName,int score)
        {
            ME_Gift giftInfo = new ME_Gift();
            giftInfo.GiftID = giftID;
            giftInfo.CardTypeID = cardTypeID;
            giftInfo.GiftName = giftName;
            giftInfo.Score = score;
            if (giftID == 0)
            {
                giftInfo.UseFlag = 1;
            }

            giftInfo.OperateDate = DateTime.Now;
            giftInfo.OperateID = LoginUserInfo.EmpId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(giftInfo);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "SaveGiftInfo", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 更新礼品标志
        /// </summary>
        /// <param name="giftID">礼品id</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdateGiftFlag(int giftID, int useFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(giftID);
                request.AddData(useFlag);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "UpdateGiftFlag", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 新增礼品管理设置校验礼品名称是否重复
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <returns>true重复false不重复</returns>
        [WinformMethod]
        public bool ChcekGiftNameForADD(int workID, int cardTypeID, string giftName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(cardTypeID);
                request.AddData(giftName);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "CheckGiftNameForADD", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 编辑礼品管理设置校验礼品名称是否重复
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <param name="giftID">礼品id</param>
        /// <returns>true重复1不重复</returns>
        [WinformMethod]
        public bool ChcekGiftNameForEdit(int workID, int cardTypeID, string giftName,int giftID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(cardTypeID);
                request.AddData(giftName);
                request.AddData(giftID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "CheckGiftNameForEdit", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 取得组织机构id
        /// </summary>
        /// <returns>组织机构id</returns>
        [WinformMethod]
        public int GetUserWorkID()
        {
            return GetUserInfo().WorkId;
        }
    }    
}