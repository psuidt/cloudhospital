using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 门诊病历库字典维护
    /// </summary>
    public partial class FrmOMRDic : BaseFormBusiness, IFrmOMRDic
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOMRDic()
        {
            InitializeComponent();
        }
    }
}
