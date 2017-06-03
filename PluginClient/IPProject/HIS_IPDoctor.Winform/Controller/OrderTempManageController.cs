using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.Controller
{
    /// <summary>
    /// 医嘱模板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOrderTempManage")]//在菜单上显示
    [WinformView(Name = "FrmOrderTempManage", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmOrderTempManage")]
    [WinformView(Name = "FrmAddOrderTemp", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmAddOrderTemp")]
    public class OrderTempManageController : WcfClientController
    {
        /// <summary>
        /// 医嘱模板接口
        /// </summary>
        IOrderTempManage mIOrderTempManage;

        /// <summary>
        /// 新增医嘱模板接口
        /// </summary>
        IAddOrderTemp mIAddOrderTemp;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIOrderTempManage = (IOrderTempManage)DefaultView;
            mIAddOrderTemp = iBaseView["FrmAddOrderTemp"] as IAddOrderTemp;
        }

        #region "医嘱模板分类维护"

        /// <summary>
        /// 获取医嘱模板列表
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        [WinformMethod]
        public void GetOrderTempList(int tempHeadID)
        {
            if (mIOrderTempManage.ModelLevel == -1)
            {
                MessageBoxShowSimple("请选择模板级别！");
                return;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIOrderTempManage.ModelLevel);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetOrderTempList", requestAction);
            List<IPD_OrderModelHead> orderTempList = retdata.GetData<List<IPD_OrderModelHead>>(0);
            mIOrderTempManage.bind_FeeTempList(orderTempList, tempHeadID);
        }

        /// <summary>
        /// 显示新增模板界面
        /// </summary>       
        [WinformMethod]
        public void ShowAddOrderTemp()
        {
            // 修改模板时，判断模板是否为当前用户新建，如果不是则不能修改
            if (mIOrderTempManage.OrderModelHead.ModelHeadID != 0)
            {
                if (mIOrderTempManage.OrderModelHead.CreateDocID != LoginUserInfo.EmpId)
                {
                    MessageBoxShowSimple("修改失败，选定模板不是当前登录用户所属！");
                    return;
                }
            }

            mIAddOrderTemp.OrderModelHead = mIOrderTempManage.OrderModelHead;
            ((Form)iBaseView["FrmAddOrderTemp"]).ShowDialog();
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        [WinformMethod]
        public void DelteOrderTemp()
        {
            // 修改模板时，判断模板是否为当前用户新建，如果不是则不能修改
            if (mIOrderTempManage.OrderModelHead.ModelHeadID != 0)
            {
                if (mIOrderTempManage.OrderModelHead.CreateDocID != LoginUserInfo.EmpId)
                {
                    MessageBoxShowSimple("删除失败，选定模板不是当前登录用户所属！");
                    return;
                }
            }

            if (MessageBoxShowYesNo("确定要模板吗,删除后将不可恢复？") != DialogResult.Yes)
            {
                return;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIOrderTempManage.OrderModelHead);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "DelOrderTemp", requestAction);
            MessageBoxShowSimple("模板删除成功！");
            GetOrderTempList(0);
        }

        /// <summary>
        /// 保存新增模板
        /// </summary>
        [WinformMethod]
        public void SaveOrderTemplateHead()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (mIAddOrderTemp.OrderModelHead.ModelHeadID != 0)
                {
                    // 修改
                    mIAddOrderTemp.OrderModelHead.UpdateDate = DateTime.Now;
                }
                else
                {
                    // 新增
                    mIAddOrderTemp.OrderModelHead.CreatDeptID = LoginUserInfo.DeptId;
                    mIAddOrderTemp.OrderModelHead.CreateDocID = LoginUserInfo.EmpId;
                }

                request.AddData(mIAddOrderTemp.OrderModelHead);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "SaveOrderTemplateHead", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("模板保存成功！");
                // 关闭模板名维护界面
                mIAddOrderTemp.ColseForm();
                int tempHeadID = retdata.GetData<int>(1);
                GetOrderTempList(tempHeadID);
            }
        }

        #endregion

        /// <summary>
        /// 获取药房列表
        /// </summary>
        [WinformMethod]
        public void GetDrugStore()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetDrugStore");
            DataTable dtDrugStor = retdata.GetData<DataTable>(0);
            if (dtDrugStor == null || dtDrugStor.Rows.Count == 1)
            {
                MessageBoxShowSimple("没有可用药房！");
                return;
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.DeptId);
                });
                retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetDrugStoreByDeptID", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                mIOrderTempManage.bind_DefaultDrugStore(dt);
            }
        }

        /// <summary>
        /// 获取模板明细数据
        /// </summary>
        /// <param name="isEdit">false新增true编辑</param>
        [WinformMethod]
        public void GetOrderTempDetail(bool isEdit)
        {
            if (isEdit)
            {
                if (MessageBoxShowYesNo("您有未保存的医嘱，是否继续！") == DialogResult.No)
                {
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIOrderTempManage.ModelHeadID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetOrderTempDetail", requestAction);
            DataTable orderDetails = retdata.GetData<DataTable>(0);
            // 过滤数据
            DataTable longDetails = orderDetails.Clone();
            DataTable tempDetails = orderDetails.Clone();
            orderDetails.TableName = "OrderDetails";
            // 过滤长期医嘱明细数据
            DataView longView = new DataView(orderDetails);
            string longSqlWhere = " OrderCategory = 0";
            longView.RowFilter = longSqlWhere;
            longDetails.Merge(longView.ToTable());
            // 过滤临时医嘱明细数据
            DataView tempView = new DataView(orderDetails);
            string tempSqlWhere = " OrderCategory = 1";
            tempView.RowFilter = tempSqlWhere;
            tempDetails.Merge(tempView.ToTable());
            mIOrderTempManage.bind_OrderDetails(longDetails, tempDetails);
        }

        /// <summary>
        /// 获取医嘱ShowCard基础数据
        /// </summary>
        [WinformMethod]
        public void GetMasterData()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetMasterData");
            // 绑定药品项目等数据源
            DataTable itemDrugList = retdata.GetData<DataTable>(0);
            // 长期医嘱材料、项目数据源
            mIOrderTempManage.LongItemDrugList = GetOrderMasterData(itemDrugList);
            // 临时医嘱药品、材料、项目数据源
            mIOrderTempManage.TempItemDrugList = itemDrugList;
            // 绑定用法列表
            List<Basic_Channel> channelList = retdata.GetData<List<Basic_Channel>>(1);
            // 将用法List转换成DataTable
            DataTable dtUsage = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(channelList);
            mIOrderTempManage.bind_ChannelList(dtUsage);
            // 绑定频次列表
            List<Basic_Frequency> frequencyList = retdata.GetData<List<Basic_Frequency>>(2);
            DataTable frequencyDt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(frequencyList);
            mIOrderTempManage.bind_FrequencyList(frequencyDt);
            // 绑定嘱托列表
            List<Basic_Entrust> entrustList = retdata.GetData<List<Basic_Entrust>>(3);
            DataTable entrustDt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(entrustList);
            mIOrderTempManage.bind_EntrustList(entrustDt);
        }

        /// <summary>
        /// 刷新药品基础数据
        /// </summary>
        [WinformMethod]
        public void GetDrugMaster()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetDrugMaster");
            // 绑定药品项目等数据源
            DataTable itemDrugList = retdata.GetData<DataTable>(0);
            // 长期医嘱材料、项目数据源
            mIOrderTempManage.LongItemDrugList = GetOrderMasterData(itemDrugList);
            // 临时医嘱药品、材料、项目数据源
            mIOrderTempManage.TempItemDrugList = itemDrugList;
        }

        /// <summary>
        /// 过滤长期医嘱数据
        /// </summary>
        /// <param name="itemDrugList">药品项目等数据源</param>
        /// <returns>DataTable</returns>
        private DataTable GetOrderMasterData(DataTable itemDrugList)
        {
            DataTable dtCopy = itemDrugList.Clone();
            dtCopy.Clear();
            DataRow[] rows = itemDrugList.Select(" statid<>102 and itemClass<>2");
            foreach (DataRow dr in rows)
            {
                dtCopy.Rows.Add(dr.ItemArray);
            }

            return dtCopy;
        }

        /// <summary>
        /// 保存医嘱模板明细数据
        /// </summary>
        [WinformMethod]
        public void SaveOrderDetailsData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIOrderTempManage.LongOrderDetails);
                request.AddData(mIOrderTempManage.TempOrderDetails);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "SaveOrderDetailsData", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                // 保存成功后重新加载数据
                GetOrderTempDetail(false);
                MessageBoxShowSimple("保存模板明细数据成功！");
            }
        }

        /// <summary>
        /// 刷新医嘱模板明细列表
        /// </summary>
        /// <param name="isEdit">false新增 true编辑</param>
        [WinformMethod]
        public void RefreshOrderTempDetail(bool isEdit)
        {
            // 保存成功后重新加载数据
            GetOrderTempDetail(isEdit);
        }

        /// <summary>
        /// 删除模板明细数据
        /// </summary>
        /// <param name="modelDetailID">模板明细ID</param>
        [WinformMethod]
        public void DelOrderDetailsData(string modelDetailID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelDetailID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "DelOrderDetailsData", requestAction);
        }

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion
    }
}
