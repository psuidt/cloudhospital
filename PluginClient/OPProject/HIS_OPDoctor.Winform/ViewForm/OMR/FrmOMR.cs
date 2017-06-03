using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 门诊病历书写
    /// </summary>
    public partial class FrmOMR : BaseFormBusiness, IFrmOMR
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOMR()
        {
            InitializeComponent();
        }
    }
}
