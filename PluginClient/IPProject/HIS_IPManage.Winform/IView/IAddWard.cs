using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 添加病房接口
    /// </summary>
    public interface IAddWard : IBaseView
    {
        /// <summary>
        /// 病房号
        /// </summary>
        string RoomNO { get; }
    }
}
