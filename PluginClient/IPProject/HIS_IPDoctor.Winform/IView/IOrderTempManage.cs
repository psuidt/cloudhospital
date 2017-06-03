using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 医嘱模板界面接口
    /// </summary>
    public interface IOrderTempManage : IBaseView
    {
        /// <summary>
        /// 模板级别
        /// </summary>
        int ModelLevel { get; }

        /// <summary>
        /// 模板主表对象
        /// </summary>
        IPD_OrderModelHead OrderModelHead { get; }

        /// <summary>
        /// 长期医嘱药品、材料和项目列表
        /// </summary>
        DataTable LongItemDrugList { get; set; }

        /// <summary>
        /// 临时医嘱药品、材料和项目列表
        /// </summary>
        DataTable TempItemDrugList { get; set; }

        /// <summary>
        /// 长期医嘱明细数据
        /// </summary>
        DataTable LongOrderDetails { get; }

        /// <summary>
        /// 临时医嘱明细数据
        /// </summary>
        DataTable TempOrderDetails { get; }

        /// <summary>
        /// 模板头ID
        /// </summary>
        int ModelHeadID { get; }

        /// <summary>
        /// 绑定用法列表
        /// </summary>
        /// <param name="channelList">用法列表</param>
        void bind_ChannelList(DataTable channelList);

        /// <summary>
        /// 绑定频次列表
        /// </summary>
        /// <param name="frequencyList">频次列表</param>
        void bind_FrequencyList(DataTable frequencyList);

        /// <summary>
        /// 绑定嘱托列表
        /// </summary>
        /// <param name="entrustList">嘱托列表</param>
        void bind_EntrustList(DataTable entrustList);

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        /// <param name="orderTempList">模板列表</param>
        /// <param name="rempHeadID">模板头ID</param>
        void bind_FeeTempList(List<IPD_OrderModelHead> orderTempList, int rempHeadID);

        /// <summary>
        /// 绑定药房列表
        /// </summary>
        /// <param name="drugStoreList">药房列表</param>
        void bind_DefaultDrugStore(DataTable drugStoreList);

        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="longOrderDt">长期医嘱列表</param>
        /// <param name="tempOrderDt">临时医嘱列表</param>
        void bind_OrderDetails(DataTable longOrderDt, DataTable tempOrderDt);
    }
}
