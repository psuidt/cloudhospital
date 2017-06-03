using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 优惠项目控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPromotionProject")]//在菜单上显示
    [WinformView(Name = "FrmPromotionProject", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmPromotionProject")]
    public class PromotionProjectController : WcfClientController
    {
        /// <summary>
        /// 优惠项目
        /// </summary>
        IFrmPromotionProject frmPromotionProject;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmPromotionProject = (IFrmPromotionProject)iBaseView["FrmPromotionProject"];
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
                request.AddData(true);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetWorkInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 获取登录机构ID
        /// </summary>
        /// <returns>机构id</returns>
        [WinformMethod]
        public int GetWorkIDForLoginInfo()
        {
            return LoginUserInfo.WorkId;
        }

        /// <summary>
        /// 获取方案头表信息
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>方案头表信息</returns>
        [WinformMethod]
        public DataTable GetPromotionProjectHeadInfo(int workID, string stDate, string endDate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
                request.AddData(stDate);
                request.AddData(endDate);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "GetPromotionProjectHeadInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        ///绑定方案头表网格
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        [WinformMethod]
        public void BindDgHead(int workID, string stDate, string endDate)
        {
            DataTable dt = GetPromotionProjectHeadInfo(workID, stDate, endDate);
            frmPromotionProject.BindPromotionProjectHeadInfo(dt);
        }

        /// <summary>
        /// 保存方案头表信息
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="headName">头名称</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int SaveHeadInfo(int promID, string headName, string stDate, string endDate)
        {
            ME_PromotionProjectHead headEntity = new ME_PromotionProjectHead();
            headEntity.PromID = promID;
            headEntity.PromName = headName;
            headEntity.StartDate = Convert.ToDateTime(stDate);
            headEntity.EndDate = Convert.ToDateTime(endDate);
            if (promID == 0)
            {
                headEntity.UseFlag = 0;
            }

            headEntity.OperateDate = DateTime.Now;
            headEntity.OperateID = LoginUserInfo.UserId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(headEntity);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "SaveHeadInfo", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 删除方案头表信息
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int DelPromPro(int promID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(promID);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "DeletePromPro", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 更新方案状态信息
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="flag">标识</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int UpdateHeadUseFlag(int promID, int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(promID);
                request.AddData(flag);
                request.AddData(LoginUserInfo.UserId);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "UpdateHeadUseFlag", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 获取指定机构的帐户类型
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <returns>帐户类型数据</returns>
        [WinformMethod]
        public DataTable GetCardTypeInfo(int workID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetCardTypeForWork", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 获取病人费用类型
        /// </summary>
        /// <returns>病人费用类型</returns>
        [WinformMethod]
        public DataTable GetPatFeeType()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "GetPatFeeType");
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 启用方案时检验方案有效期是否存在重复
        /// </summary>
        /// <param name="stDate">日期</param>
        /// <returns>true存在</returns>
        [WinformMethod]
        public bool CheckPromDate(string stDate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stDate);
                request.AddData(LoginUserInfo.WorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "CheckPromDate", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }

        /// <summary>
        /// 获取方案明细内容
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>方案明细内容</returns>
        [WinformMethod]
        public DataTable GetPromotionProjectDetail(int promID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(promID);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "GetPromotionProjectDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 保存明细数据
        /// </summary>
        /// <param name="detailEntity">方案明细实体</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int SavePromotionProjectDetail(ME_PromotionProjectDetail detailEntity)
        {
            detailEntity.OperateID = LoginUserInfo.UserId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(detailEntity);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "SavePromotionProjectDetail", requestAction);
            int res = retdata.GetData<int>(0);
            return res;
        }

        #region 校验新增或修改明细项目时是否存在得复
        /// <summary>
        /// 检查总额优惠
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool CheckDetailForAmount(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int promID, int promSunID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(patTypeID);
                request.AddData(costTypeID);
                request.AddData(promTypeID);
                request.AddData(promID);
                request.AddData(promSunID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "CheckDetailForAmount", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }

        /// <summary>
        /// 检查类型优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="classID">类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool CheckDetailForClass(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int classID, int promID, int promSunID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(patTypeID);
                request.AddData(costTypeID);
                request.AddData(promTypeID);
                request.AddData(promID);
                request.AddData(promSunID);
                request.AddData(classID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "CheckDetailForClass", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }

        /// <summary>
        /// 检查类型优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="itemID">项目id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool CheckDetailForItem(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int itemID, int promID, int promSunID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(patTypeID);
                request.AddData(costTypeID);
                request.AddData(promTypeID);
                request.AddData(promID);
                request.AddData(promSunID);
                request.AddData(itemID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "CheckDetailForItem", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }
        #endregion

        /// <summary>
        /// 获取大项目信息
        /// </summary>
        /// <returns>大项目信息</returns>
        [WinformMethod]
        public DataTable GetStatItem()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "GetStatItem");
            DataTable res = retdata.GetData<DataTable>(0);
            return res;
        }

        /// <summary>
        /// 获取收费项目信息
        /// </summary>
        /// <returns>收费项目信息</returns>
        [WinformMethod]
        public DataTable GetSimpleFeeItemDataDt()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "GetSimpleFeeItemDataDt");
            DataTable res = retdata.GetData<DataTable>(0);
            return res;
        }

        /// <summary>
        /// 更新明细使用状态标志
        /// </summary>
        /// <param name="promSunID">明细id</param>
        /// <param name="flag">使用标识</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int UpdateDetailFlag(int promSunID, int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(promSunID);
                request.AddData(flag);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "UpdateDetailFlag", requestAction);
            int res = retdata.GetData<int>(0);
            return res;
        }

        /// <summary>
        /// 复制方案主表与明细表产生一个新的方案
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int CopypPromotionProject(int promID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(promID);
                request.AddData(LoginUserInfo.UserId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PromotionProjectController", "CopypPromotionProject", requestAction);
            int res = retdata.GetData<int>(0);
            return res;
        }

        /// <summary>
        /// 取得机构id
        /// </summary>
        /// <returns>机构id</returns>
        [WinformMethod]
        public int GetUserWorkID()
        {
            return GetUserInfo().WorkId;
        }
    }
}
