using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资参数维护
    /// </summary>
    public partial class FrmMaterialPrament : BaseFormBusiness, IFrmMaterialPrament
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialPrament()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 参数列表
        /// </summary>
        private List<Basic_SystemConfig> parameterList = new List<Basic_SystemConfig>();

        /// <summary>
        /// 取得物资参数
        /// </summary>
        /// <returns>参数实体列表</returns>
        private List<Basic_SystemConfig> GetDrugParameters()
        {
            int deptId = Convert.ToInt32(lstDrugRoom.SelectedItems[0].Tag);
            Basic_SystemConfig model = new Basic_SystemConfig();
            parameterList.Clear();
            //加载通用参数
           
            //物资材料利润百分比
            string pm016 = iip_wzcl.Text;
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = 0, ParaID = "MWPricePercent", ParaName = "物资利润率", Value = pm016, DataType = 0, Prompt = string.Empty, Memo = "物资利润率(百分比)" });
           
            //加载部门参数
            //出库单自动审核
            string pm021 = "0";
            if (chk_out_chexk.Checked)
            {
                pm021 = "1";
            }
            else
            {
                pm021 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AutoAuditOutStore", ParaName = "出库单自动审核", Value = pm021, DataType = 0, Prompt = string.Empty, Memo = "出库单自动审核:0手动;1自动" });

            //入库单自动审核 chk_input_check
            string pm020 = "0";
            if (chk_input_check.Checked)
            {
                pm020 = "1";
            }
            else
            {
                pm020 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AutoAuditInstore", ParaName = "入库单自动审核", Value = pm020, DataType = 0, Prompt = string.Empty, Memo = "入库单自动审核:0手动;1自动" });

            //月结时间为每月 iip_banlance_day
            string pm008 = iip_banlance_day.Text;
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "BalanceDay", ParaName = "默认月结时间", Value = pm008, DataType = 0, Prompt = string.Empty, Memo = "默认月结时间(每月多少号)" });
            //强制控制库存 chk_qzkc
            string pm010 = "0";
            if (chk_qzkc.Checked)
            {
                pm010 = "1";
            }
            else
            {
                pm010 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "ControlStore", ParaName = "是否允许库存为负数", Value = pm010, DataType = 0, Prompt = string.Empty, Memo = "是否允许库存为负数:0不强制控制;1强制控制" });

            //允许强制平账 chk_qzpz
            string pm011 = "1";
            if (chk_qzpz.Checked)
            {
                pm011 = "1";
            }
            else
            {
                pm011 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AllowModifyAccount", ParaName = "允许强制平账", Value = pm011, DataType = 0, Prompt = string.Empty, Memo = "是否允许自动平账:0不允许;1允许" });

            //月结前必须对账 chk_dz
            string pm012 = "0";
            if (chk_dz.Checked)
            {
                pm012 = "1";
            }
            else
            {
                pm012 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "CheckAccountWhenBalance", ParaName = "月结前必须对账", Value = pm012, DataType = 0, Prompt = string.Empty, Memo = "月结时是否对账:0不对账;1对账" });
            //对账误差范围 iip_wzfw
            string pm013 = iip_wzfw.Text;           
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "ErrorRange", ParaName = "对账误差范围", Value = pm013, DataType = 0, Prompt = string.Empty, Memo = "金额允许误差范围(0.1元为单位)" });
            return parameterList;
        }

        /// <summary>
        /// 取得参数行
        /// </summary>
        /// <param name="dt">参数集</param>
        /// <param name="paraID">参数编号</param>
        /// <returns>参数</returns>
        private string GetParameterRow(DataTable dt,string paraID)
        {
            if (dt.Rows.Count <= 0 || dt == null)
            {
                return string.Empty;
            }

           DataRow[] rows = dt.Select("ParaID='" + paraID+"'");
            if (rows.Length > 0)
            {
                return rows[0]["Value"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="dt">参数集</param>
        public void BindPublicParameters(DataTable dt)
        {           
            //物资利润百分比
            string pm016 = GetParameterRow(dt, "MWPricePercent");
            if (pm016 == string.Empty)
            {
                iip_wzcl.Value = 25;
                iip_wzcl.Text = "25";
            }
            else
            {
                iip_wzcl.Value = Convert.ToInt32(pm016);
                iip_wzcl.Text = pm016;
            }           
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室</param>
        public void BindDeptParameters(DataTable dt)
        {
            //出库单自动审核
            string pm021 = GetParameterRow(dt, "AutoAuditOutStore");
            if (pm021 == "0" || pm021 == string.Empty)
            {
                chk_out_chexk.Checked = false;
            }
            else
            {
                chk_out_chexk.Checked = true;
            }

            //入库单自动审核 chk_input_check
            string pm020 = GetParameterRow(dt, "AutoAuditInstore");
            if (pm020 == "0" || pm020 == string.Empty)
            {
                chk_input_check.Checked = false;
            }
            else
            {
                chk_input_check.Checked = true;
            }

            //月结时间为每月 iip_banlance_day
            string pm008= GetParameterRow(dt, "BalanceDay");
            if (pm008 == string.Empty)
            {
                iip_banlance_day.Value = 25;
                iip_banlance_day.Text = "25";
            }
            else
            {
                iip_banlance_day.Value = Convert.ToInt32(pm008);
                iip_banlance_day.Text = pm008;
            }

            //强制控制库存 chk_qzkc
            string pm010 = GetParameterRow(dt, "ControlStore");
            if (pm010 == string.Empty || pm010 == "0")
            {
                chk_qzkc.Checked = false;
            }
            else
            {
                chk_qzkc.Checked = true;
            }

            //允许强制平账 chk_qzpz
            string pm011 = GetParameterRow(dt, "AllowModifyAccount");
            if (pm011 == string.Empty || pm011 == "1")
            {
                chk_qzpz.Checked = true;
            }
            else
            {
                chk_qzpz.Checked = false;
            }

            //月结前必须对账 chk_dz
            string pm012 = GetParameterRow(dt, "CheckAccountWhenBalance");
            if (pm012 == string.Empty || pm012 == "0")
            {
                chk_dz.Checked = false;
            }
            else
            {
                chk_dz.Checked = true;
            }

            //对账误差范围 iip_wzfw
            string pm013 = GetParameterRow(dt, "ErrorRange");
            if (pm013 == string.Empty)
            {
                iip_wzfw.Value = 1;
                iip_wzfw.Text = "1";
            }
            else
            {
                iip_wzfw.Value = Convert.ToInt32(pm013);
                iip_wzfw.Text = pm013;
            }           
        }

        /// <summary>
        /// 绑定物资科室列表
        /// </summary>
        /// <param name="dt">物资科室数据集</param>
        public void BindDrugDeptList(DataTable dt)
        {
            lstDrugRoom.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int deptId = Convert.ToInt32(dr["DeptID"]);
                string deptName = dr["DeptName"].ToString().Trim();
                ListViewItem item = new ListViewItem();
                item.Text = deptName;
                item.Tag = deptId;
                lstDrugRoom.Items.Add(item);
            }

            if (lstDrugRoom.Items.Count > 0)
            {
                lstDrugRoom.Items[0].Selected = true;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 选中库房
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void lstDrugRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDrugRoom.SelectedItems.Count == 0)
            {
                return;
            }

            //获取当前选中的行对应的科室ID
            int deptId = Convert.ToInt32(lstDrugRoom.SelectedItems[0].Tag);
            lblCurrentDeptName.Text = "当前选中库房：" + lstDrugRoom.SelectedItems[0].Text;
            //加载物资科室参数
            InvokeController("GetDeptParameters", deptId);            
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmDrugPrament_Load(object sender, EventArgs e)
        {
            if (lstDrugRoom.Items.Count > 0)
            {
                lstDrugRoom.Items[0].Selected = true;
                lblCurrentDeptName.Text = "当前选中库房：" + lstDrugRoom.SelectedItems[0].Text;
            }
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmDrugPrament_OpenWindowBefore(object sender, EventArgs e)
        {
            //调用控制器绑定数据
            //加载物资科室数据
            InvokeController("GetUsedDeptData");
            //加载物资公共参数
            InvokeController("GetPublicParameters");
            if (lstDrugRoom.Items.Count > 0)
            {
                lstDrugRoom.Items[0].Selected = true;
                lblCurrentDeptName.Text = "当前选中库房：" + lstDrugRoom.SelectedItems[0].Text;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDrugRoom.Items.Count > 0)
                {
                    if (lstDrugRoom.SelectedItems.Count == 0)
                    {
                        lstDrugRoom.Items[0].Selected = true;
                    }
                }
                else
                {
                     MessageBoxEx.Show("没有可选的库房数据，请维护库房数据", "提示");
                    return;
                }

                if (lstDrugRoom.SelectedItems == null)
                {
                    MessageBoxEx.Show("请选中一条库房", "提示");
                    return;
                }

                List<Basic_SystemConfig> modelList = GetDrugParameters();
                InvokeController("SaveParameters", modelList);
                MessageBox.Show("保存物资参数成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
