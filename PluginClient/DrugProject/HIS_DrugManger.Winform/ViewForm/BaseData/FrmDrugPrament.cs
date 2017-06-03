using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.BasicData;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药品参数维护
    /// </summary>
    public partial class FrmDrugPrament : BaseFormBusiness, IFrmDrugPrament
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDrugPrament()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 参数列表
        /// </summary>
        private List<Basic_SystemConfig> parameterList = new List<Basic_SystemConfig>();

        /// <summary>
        /// 取得药品参数
        /// </summary>
        /// <returns>参数实体列表</returns>
        private List<Basic_SystemConfig> GetDrugParameters()
        {
            int deptId = Convert.ToInt32(lstDrugRoom.SelectedItems[0].Tag);
            Basic_SystemConfig model = new Basic_SystemConfig();
            parameterList.Clear();

            //加载通用参数
            //发药模式
            string pm014 = "0";
            if (rd_jgfy.Checked)
            {
                pm014 = "0";
            }

            if (rd_tlfy.Checked)
            {
                pm014 = "1";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "DispModel", ParaName = "发药模式", Value = pm014, DataType = 0, Prompt = string.Empty, Memo = "发药模式:0经管发药(不含频次、用法等);1临床发药(含频次、用法等)" });

            //需要药房确认接收
            string pm009 = "0";
            if (ckb_receive.Checked)
            {
                pm009 = "1";
            }
            else
            {
                pm009 = "0";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "NeedInstoreConfirm", ParaName = "是否需要药房确认接收", Value = pm009, DataType = 0, Prompt = string.Empty, Memo = "药品流通出库时是否需要药房确认接收。" });

            //西药利润百分比
            string pm015 = iip_xy.Text;
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "WMPricePercent", ParaName = "西药利润率", Value = pm015, DataType = 0, Prompt = string.Empty, Memo = "西药利润率(百分比)" });

            //中成药利润百分比
            string pm016 = iip_zcy.Text;
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "CPMPricePercent", ParaName = "中成药利润率", Value = pm016, DataType = 0, Prompt = string.Empty, Memo = "中成药利润率(百分比)" });

            //中药利润百分比
            string pm017 = iip_zcaoy.Text; 
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "TCMPricePercent", ParaName = "中药利润率(百分比)", Value = pm017, DataType = 0, Prompt = string.Empty, Memo = "中药利润率(百分比)" });

            //摆药单打印药品类型
            string pm018 = "1";
            if (rb_byqb.Checked)
            {
                pm018 = "0";
            }

            if (rb_kfy.Checked)
            {
                pm018 = "1";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "PrintPutBillType", ParaName = "摆药单打印药品类型", Value = pm018, DataType = 0, Prompt = string.Empty, Memo = "摆药单打印药品类型：0全部类型；1口服药" });

            //统领单打印药品类型
            string pm019 = "1";
            if (rb_tl_all.Checked)
            {
                pm019 = "0";
            }

            if(rb_zj.Checked)
            {
                pm019 = "1";
            }

            if(rb_zjdsy.Checked)
            {
                pm019 = "2";
            }

            if(rb_dsy.Checked)
            {
                pm019 = "3";
            }

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "PrintReceiveBillType", ParaName = "统领单打印药品类型", Value = pm019, DataType = 0, Prompt = string.Empty, Memo = "统领单打印药品类型：0全部；1针剂；2针剂+大输液；3仅打印大输液" });

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

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AutoAuditOutStore", ParaName = "出库单自动审核", Value = pm021, DataType = 0, Prompt = string.Empty, Memo = "出库单自动审核:0手动;1自动" });

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

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AutoAuditInstore", ParaName = "入库单自动审核", Value = pm020, DataType = 0, Prompt = string.Empty, Memo = "入库单自动审核:0手动;1自动" });

            //月结时间为每月 iip_banlance_day
            string pm008 = iip_banlance_day.Text;
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "BalanceDay", ParaName = "默认月结时间", Value = pm008, DataType = 0, Prompt = string.Empty, Memo = "默认月结时间(每月多少号)" });

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

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "ControlStore", ParaName = "是否允许库存为负数", Value = pm010, DataType = 0, Prompt = string.Empty, Memo = "是否允许库存为负数:0不强制控制;1强制控制" });

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

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AllowModifyAccount", ParaName = "允许强制平账", Value = pm011, DataType = 0, Prompt = string.Empty, Memo = "是否允许自动平账:0不允许;1允许" });

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

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "CheckAccountWhenBalance", ParaName = "月结前必须对账", Value = pm012, DataType = 0, Prompt = string.Empty, Memo = "月结时是否对账:0不对账;1对账" });

            //对账误差范围 iip_wzfw
            string pm013 = iip_wzfw.Text;           
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "ErrorRange", ParaName = "对账误差范围", Value = pm013, DataType = 0, Prompt = string.Empty, Memo = "金额允许误差范围(0.1元为单位)" });
            return parameterList;
        }

        /// <summary>
        /// 取得参数行
        /// </summary>
        /// <param name="dt">参数集</param>
        /// <param name="paraID">参数编号</param>
        /// <returns>参数行</returns>
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
            //发药模式
            string pm014 = GetParameterRow(dt, "DispModel");
            if (pm014 == "0" || pm014 == string.Empty)
            {
                rd_jgfy.Checked = true;
                rd_tlfy.Checked = false;
            }
            else
            {
                rd_jgfy.Checked = false;
                rd_tlfy.Checked = true;
            }

            //需要药房确认接收
            string pm009 = GetParameterRow(dt, "NeedInstoreConfirm");
            if (pm009 == "0" || pm009 == string.Empty)
            {
                ckb_receive.Checked = false;
            }
            else
            {
                ckb_receive.Checked = true;
            }

            //西药利润百分比
            string pm015 = GetParameterRow(dt, "WMPricePercent");
            if (pm015 == string.Empty)
            {
                iip_xy.Value = 15;
                iip_xy.Text = "15";
            }
            else
            {
                iip_xy.Value = Convert.ToInt32(pm015);
                iip_xy.Text = pm015;
            }

            //中成药利润百分比
            string pm016 = GetParameterRow(dt, "CPMPricePercent");
            if (pm016 == string.Empty)
            {
                iip_zcy.Value = 25;
                iip_zcy.Text = "25";
            }
            else
            {
                iip_zcy.Value = Convert.ToInt32(pm016);
                iip_zcy.Text = pm016;
            }

            //中药利润百分比
            string pm017 = GetParameterRow(dt, "TCMPricePercent");
            if (pm017 == string.Empty)
            {
                iip_zcaoy.Value = 20;
                iip_zcaoy.Text = "20";
            }
            else
            {
                iip_zcaoy.Value = Convert.ToInt32(pm017);
                iip_zcaoy.Text = pm017;
            }

            //摆药单打印药品类型
            string pm018 = GetParameterRow(dt, "PrintPutBillType");
            if (pm018 == string.Empty || pm018 == "1")
            {
                rb_byqb.Checked = false;
                rb_kfy.Checked = true;
            }
            else
            {
                rb_byqb.Checked = true;
                rb_kfy.Checked = false;
            }

            //统领单打印药品类型
            string pm019 = GetParameterRow(dt, "PrintReceiveBillType");
            if (pm019 == string.Empty || pm019 == "1")
            {
                rb_tl_all.Checked = false;
                rb_zj.Checked = true;
                rb_zjdsy.Checked = false;
                rb_dsy.Checked = false;
            }
            else if (pm019 == "0")
            {
                rb_tl_all.Checked = true;
                rb_zj.Checked = false;
                rb_zjdsy.Checked = false;
                rb_dsy.Checked = false;
            }
            else if (pm019 == "2")
            {
                rb_tl_all.Checked = false;
                rb_zj.Checked = false;
                rb_zjdsy.Checked = true;
                rb_dsy.Checked = false;
            }
            else if (pm019 == "3")
            {
                rb_tl_all.Checked = false;
                rb_zj.Checked = false;
                rb_zjdsy.Checked = false;
                rb_dsy.Checked = true;
            }
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室参数数据源</param>
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
            if(pm011== string.Empty || pm011=="1")
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
        /// 绑定药剂科室列表
        /// </summary>
        /// <param name="dt">药剂科室数据集</param>
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
        /// 选择改变事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void lstDrugRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDrugRoom.SelectedItems.Count == 0)
            {
                return;
            }

            //获取当前选中的行对应的科室ID
            int deptId = Convert.ToInt32(lstDrugRoom.SelectedItems[0].Tag);
            lblCurrentDeptName.Text = "当前选中药房：" + lstDrugRoom.SelectedItems[0].Text;

            //加载药品科室参数
            InvokeController("GetDeptParameters", deptId);            
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugPrament_Load(object sender, EventArgs e)
        {
            if (lstDrugRoom.Items.Count > 0)
            {
                lstDrugRoom.Items[0].Selected = true;
                lblCurrentDeptName.Text = "当前选中药房：" + lstDrugRoom.SelectedItems[0].Text;
            }
        }

        /// <summary>
        /// 窗体打开前加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugPrament_OpenWindowBefore(object sender, EventArgs e)
        {
            //调用控制器绑定数据
            //加载药剂科室数据
            InvokeController("GetUsedDrugDeptData");

            //加载药品公共参数
            InvokeController("GetPublicParameters");
            if (lstDrugRoom.Items.Count > 0)
            {
                lstDrugRoom.Items[0].Selected = true;
                lblCurrentDeptName.Text = "当前选中药房：" + lstDrugRoom.SelectedItems[0].Text;
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">对象</param>
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
                InvokeController("SaveDrugParameters", modelList);
                MessageBox.Show("保存药品参数成功", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
