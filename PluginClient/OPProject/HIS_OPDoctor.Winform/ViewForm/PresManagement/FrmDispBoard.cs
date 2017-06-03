using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 糖尿病专科看板
    /// </summary>
    public partial class FrmDispBoard : BaseFormBusiness, IFrmDispBoard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDispBoard()
        {
            InitializeComponent();
        }
    }
}
