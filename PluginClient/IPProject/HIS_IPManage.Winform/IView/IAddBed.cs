using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 添加病床接口
    /// </summary>
    public interface IAddBed : IBaseView
    {
        /// <summary>
        /// 病区ID
        /// </summary>
        int WardID { get; set; }

        /// <summary>
        /// 病房号
        /// </summary>
        string RoomNO { get; set; }
    }
}
