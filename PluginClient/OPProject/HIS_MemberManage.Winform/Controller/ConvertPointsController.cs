using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 转换积分控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmConvertPoints")]//在菜单上显示
    [WinformView(Name = "FrmConvertPoints", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmConvertPoints")]
    [WinformView(Name = "FrmCardTypeInfo", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmCardTypeInfo")]
    [WinformView(Name = "FrmConvertPointsInfo", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmConvertPointsInfo")]
    public class ConvertPointsController : WcfClientController
    {
        /// <summary>
        /// 积分兑换规则设置
        /// </summary>
        IFrmConvertPoints frmConvertPoints;

        /// <summary>
        /// 账户信息
        /// </summary>
        IFrmCardTypeInfo frmCardTypeInfo;

        /// <summary>
        /// 积分兑换设置明细
        /// </summary>
        IFrmConvertPointsInfo frmConvertPointsInfo;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmConvertPoints = (IFrmConvertPoints)iBaseView["FrmConvertPoints"];
            frmCardTypeInfo = (IFrmCardTypeInfo)iBaseView["FrmCardTypeInfo"];
            frmConvertPointsInfo = (IFrmConvertPointsInfo)iBaseView["FrmConvertPointsInfo"];
        }

        /// <summary>
        /// 绑定卡片类型网格
        /// </summary>
        /// <param name="index">索引</param>
        [WinformMethod]
        public void BindCardTypeDataSource(int index)
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "GetAccountTypeInfo");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmConvertPoints.BindCardTypeInfo(dt, index);
        }

        /// <summary>
        /// 显示卡类型信息
        /// </summary>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="cardTypeName">卡类型名称</param>
        /// <param name="cardTypeInterface">卡类型接口</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="flag">标识</param>
        /// <param name="cardTypeIndex">卡类型索引</param>
        /// <param name="cardPrefix">卡前缀</param>
        [WinformMethod]
        public void ShowCardTypeInfo(int cardTypeID, string cardTypeName, string cardTypeInterface, int cardType,int flag,int cardTypeIndex,string cardPrefix)
        {
            frmCardTypeInfo.CardInterfaceDesc = cardTypeInterface;
            frmCardTypeInfo.CardTypeDesc = cardTypeName;
            frmCardTypeInfo.CardTypeID = cardTypeID;
            frmCardTypeInfo.CardType = cardType;
            frmCardTypeInfo.Flag = flag;
            frmCardTypeInfo.CardTypeIndex = cardTypeIndex;
            frmCardTypeInfo.CardPrefix = cardPrefix;
            (iBaseView["FrmCardTypeInfo"] as Form).ShowDialog();
        }

        /// <summary>
        /// 保存卡片类型
        /// </summary>
        /// <param name="cardTypeID">卡类型Id</param>
        /// <param name="cardTypeName">卡类型名称</param>
        /// <param name="cardTypeInterface">卡类型接口</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="flag">标识</param>
        /// <param name="cardPrefix">卡前缀</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int SaveCardTypeInfo(int cardTypeID, string cardTypeName, string cardTypeInterface, int cardType,int flag,string cardPrefix)
        {
            ME_CardTypeList cardTypeList = new ME_CardTypeList();
            cardTypeList.CardTypeID = cardTypeID;
            cardTypeList.CardTypeName = cardTypeName;
            cardTypeList.CardInterface = cardTypeInterface;
            cardTypeList.CardType = cardType;
            cardTypeList.FindFlag = 1;
            cardTypeList.UseFlag = flag;
            cardTypeList.OperateDate = DateTime.Now;
            cardTypeList.OperateID = LoginUserInfo.EmpId;
            cardTypeList.CardPrefix = cardPrefix;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeList);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "SaveCardTypeInfo", requestAction);
            int res = retdata.GetData<int>(0);
            return res;
        }

        /// <summary>
        /// 新增帐户类型时检查帐户名称的有效性
        /// </summary>
        /// <param name="name">卡类型名称</param>
        /// <returns>true成功false失败</returns>
        [WinformMethod]
        public bool CheckCardTypeNameForADD(string name)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(name);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "CheckCardTypeNameForADD", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }

        /// <summary>
        /// 修改帐户类型时检查帐户名称的有效性
        /// </summary>
        /// <param name="name">帐户名称</param>
        /// <param name="id">ID</param>
        /// <returns>true成功false失败</returns>
        [WinformMethod]
        public bool CheckCardTypeNameForEdit(string name, int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(name);
                request.AddData(id);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "CheckCardTypeNameForEdit", requestAction);
            bool res = retdata.GetData<bool>(0);
            return res;
        }

        /// <summary>
        /// 更改使用标识
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="flag">状态</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdateUseFlag(int id,int flag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
                request.AddData(flag);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "UpdateUseFlag", requestAction);
            int res = retdata.GetData<int>(0);
            return res;
        }

        /// <summary>
        /// 获取转换分数规则
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns>转换规则信息</returns>
        [WinformMethod]
        public DataTable GetConvertPointsInfo(int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "GetConvertPointsInfo", requestAction);
            DataTable res = retdata.GetData<DataTable>(0);
            return res;
        }

        /// <summary>
        /// 打开积分设置界面
        /// </summary>
        /// <param name="cardTypeID">卡类型Id</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cardTypeName">卡类型名</param>
        /// <param name="workID">组织机构ID</param>
        /// <param name="cash">现金</param>
        /// <param name="score">分数</param>
        /// <param name="convertID">转换规则ID</param>
        [WinformMethod]
        public void ShowFrmConvertPointsInfo(int cardTypeID,int rowIndex,string cardTypeName,int workID,int cash,int score,int convertID)
        {
            frmConvertPointsInfo.CardTypeID = cardTypeID;
            frmConvertPointsInfo.CardTypeName = cardTypeName;
            frmConvertPointsInfo.RowIndex = rowIndex;
            frmConvertPointsInfo.WorkID = workID;
            frmConvertPointsInfo.CardTypeName = cardTypeName;
            frmConvertPointsInfo.Score = score;
            frmConvertPointsInfo.Cash = cash;
            frmConvertPointsInfo.ConvertID = convertID;
            (iBaseView["FrmConvertPointsInfo"] as Form).ShowDialog();            
        }

        /// <summary>
        /// 绑定机构数据
        /// </summary>
        /// <returns>机构数据</returns>
        [WinformMethod]
        public DataTable BindWorkInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<bool>(false);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetWorkInfo", requestAction);
            return   retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 保存积分兑换设置
        /// </summary>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="cash">现金</param>
        /// <param name="score">分数</param>
        /// <param name="useFlag">使用标识</param>
        /// <param name="rowindex">行索引</param>
        /// <param name="points">基数</param>
        /// <param name="workID">组织机构ID</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int  SavePoints(int cardTypeID, int cash, int score,int useFlag,int rowindex,int points,int workID)
        {
            ME_ConvertPoints convertPoints = new ME_ConvertPoints();
            convertPoints.CardTypeID = cardTypeID;
            convertPoints.Cash = cash;
            convertPoints.Score = score;
            convertPoints.UseFlag = useFlag;
            convertPoints.UseWork = workID;
            convertPoints.OperateDate = DateTime.Now;
            convertPoints.OperateID = LoginUserInfo.EmpId;
            if (points>0)
            {
                convertPoints.ConvertID= points;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<ME_ConvertPoints>(convertPoints);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "SavePoints", requestAction);

            frmConvertPoints.BinddgAccount(cardTypeID, rowindex);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        ///添加分数检查分数
        /// </summary>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="workID">组织机构ID</param>
        /// <returns>true存在false不存在</returns>
        [WinformMethod]
        public bool CheckPointsForADD(int cardTypeID,int workID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<int>(cardTypeID);
                request.AddData<int>(workID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "CheckPointsForADD", requestAction);

            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 检验帐户类型前缀是否唯一
        /// </summary>
        /// <param name="cardPrefix">卡前缀</param>
        /// <param name="id">编码</param>
        /// <param name="newFlag">新增标识</param>
        /// <returns>true允许保存存</returns>
        [WinformMethod]
        public bool CheckCardPrefix(string cardPrefix,int id,bool newFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<string>(cardPrefix);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "CheckCardPrefix", requestAction);
            DataTable dt= retdata.GetData<DataTable>(0);

            if (newFlag==true)
            {
                return (dt.Rows.Count > 0) ? false : true;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt16(dt.Rows[0]["CardTypeID"])==id)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 更新分数使用标识
        /// </summary>
        /// <param name="pointsID">分数编码</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int UpdatePointsUseFlag(int pointsID,int useFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<int>(pointsID);
                request.AddData<int>(useFlag);
                request.AddData<int>(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ConvertPointsController", "UpdatePointsUseFlag", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 取得用户的组织机构ID
        /// </summary>
        /// <returns>组织机构ID</returns>
        [WinformMethod]
        public int GetUserWorkID()
        {
            return GetUserInfo().WorkId;
        }
    }
}
