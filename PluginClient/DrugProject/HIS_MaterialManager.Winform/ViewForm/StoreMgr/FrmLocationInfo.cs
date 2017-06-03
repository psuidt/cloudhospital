using System;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 库位信息管理
    /// </summary>
    public partial class FrmLocationInfo : BaseFormBusiness, IFrmLocationInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmLocationInfo()
        {
            InitializeComponent();
            frmLocation.AddItem(LocationName, "LocationName");
            frmLocation.AddItem(LocationRemark, "LocationRemark");
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LocationName.Text))
            {
                MW_Location location = new MW_Location();
                location.LocationName = LocationName.Text;
                location.Remark = LocationRemark.Text;
                InvokeController("SaveLocation", location);
            }
            else
            {
                MessageBoxEx.Show("库存名称不能为空");
            }
        }

        /// <summary>
        /// 绑定库位信息
        /// </summary>
        /// <param name="location">库位信息</param>
        public void GetLocationInfo(MW_Location location)
        {
            LocationName.Text = string.Empty;
            LocationRemark.Text = string.Empty;
            if (location != null)
            {
                LocationName.Text = location.LocationName;
                LocationRemark.Text = location.Remark;
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存库位信息
        /// </summary>
        /// <param name="statu">库位信息</param>
        public void SaveComplete(bool statu)
        {
            if (statu)
            {
                MessageBoxEx.Show("保存成功");
                LocationName.Text = string.Empty;
                LocationRemark.Text = string.Empty;
                this.Close();
            }
            else
            {
                MessageBoxEx.Show("已存在名称相同的库位");
            }
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmLocationInfo_Load(object sender, EventArgs e)
        {
            InvokeController("GetLocationInfo");
        }
    }
}
