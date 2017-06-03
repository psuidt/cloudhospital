using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 库位信息
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LocationName.Text))
            {
                DG_Location location = new DG_Location();
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
        /// 获取库位详情
        /// </summary>
        /// <param name="location">库位详情</param>
        public void GetLocationInfo(DG_Location location)
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存后执行
        /// </summary>
        /// <param name="statu">返回结果</param>
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmLocationInfo_Load(object sender, EventArgs e)
        {
            InvokeController("GetLocationInfo");
        }
    }
}
