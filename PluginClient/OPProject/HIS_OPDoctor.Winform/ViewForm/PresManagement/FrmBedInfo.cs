using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    public partial class FrmBedInfo : BaseFormBusiness, IFrmBedInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBedInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmBedInfo_Load(object sender, EventArgs e)
        {
            InvokeController("GetBedInfo");
        }

        /// <summary>
        /// 绑定床位网格信息
        /// </summary>
        /// <param name="dtBedInfo">床位数据</param>
        public void BindBedInfo(DataTable dtBedInfo)
        {
            dgBed.DataSource = dtBedInfo;
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnBedQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetBedInfo");
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
