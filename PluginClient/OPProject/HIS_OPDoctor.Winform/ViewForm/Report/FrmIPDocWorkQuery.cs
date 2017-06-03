using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 门诊医生工作量统计
    /// </summary>
    public partial class FrmIPDocWorkQuery : BaseFormBusiness, IFrmIPDocWorkQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmIPDocWorkQuery()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 清空统计指标
        /// </summary>
        private void ClearStaticsIndex()
        {
            lblPersonCnt.Text = "0人";
            lblDrugSum.Text = "0.00";
            lblDrugPer.Text = "0.00%";
            lblFeeAvg.Text = "0";
        }

        /// <summary>
        /// 设置统计指标
        /// </summary>
        /// <param name="dataRow">数据行</param>
        private void SetStaticsIndex(DataRow dataRow)
        {
            lblPersonCnt.Text = dataRow["PersonCnt"].ToString();
            lblDrugSum.Text = Convert.ToDecimal(dataRow["DrugSum"]).ToString("f2");
            lblDrugPer.Text = dataRow["DrugPer"].ToString();
            lblFeeAvg.Text = Convert.ToDecimal(dataRow["FeeAvg"]).ToString("f2");
        }

        /// <summary>
        /// 设置图形数据
        /// </summary>
        /// <param name="dtData">数据源</param>
        private void SetGraph(DataTable dtData)
        {
            DataTable dt = dtData;
            List<string> x = new List<string>();
            List<decimal> y = new List<decimal>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Flag"].ToString() == "0")
                {
                    decimal fees = Convert.ToDecimal(Convert.ToDecimal(dr["Fees"]).ToString("0.00"));
                    x.Add(dr["ItemType"].ToString());
                    y.Add(fees);
                }
            }

            chartType.Series.Clear();
            chartType.ChartAreas.Clear();
            if (radPie.Checked)
            {
                Series series1 = new Series();
                chartType.Series.Add(series1);
                chartType.Series["Series1"].ChartType = SeriesChartType.Pie;
                chartType.Legends[0].Enabled = true;
                chartType.Series["Series1"].LegendText = "#VALX(#VALY{F2})";//开启图例
                //chartType.Series["Series1"].Label = "#INDEX:#PERCENT";
                this.chartType.Series[0].CustomProperties = "DoughnutRadius=60, PieLabelStyle=Disabled, PieDrawingStyle=SoftEdge";
                this.chartType.Series[0].Label = "#PERCENT(#VALX:#VALY{F2})";
                this.chartType.Series[0]["PieLabelStyle"] = "Inside";
                chartType.Series["Series1"].IsXValueIndexed = false;
                chartType.Series["Series1"].IsValueShownAsLabel = false;
                chartType.Series["Series1"]["PieLineColor"] = "Black";//连线颜色
                chartType.Series["Series1"]["PieLabelStyle"] = "Outside";//标签位置
                chartType.Series["Series1"].ToolTip = "#VALX(#VALY{F2})";//显示提示用语
                ChartArea chartArea1 = new ChartArea();
                chartType.ChartAreas.Add(chartArea1);
                //开启三维模式的原因是为了避免标签重叠
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 45;//倾斜度(0～90)
                chartType.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
            }

            if (radColumn.Checked)
            {
                Series series1 = new Series();
                chartType.Series.Add(series1);
                chartType.Series["Series1"].ChartType = SeriesChartType.Column;
                chartType.Legends[0].Enabled = false;
                chartType.Series["Series1"].LegendText = string.Empty;
                chartType.Series["Series1"].Label = "#VALY{F2}";
                chartType.Series["Series1"].ToolTip = "#VALX";
                chartType.Series["Series1"]["PointWidth"] = "0.5";
                ChartArea chartArea1 = new ChartArea();
                chartType.ChartAreas.Add(chartArea1);
                //开启三维模式的原因是为了避免标签重叠
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;//开启三维模式;PointDepth:厚度BorderWidth:边框宽
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;//起始角度
                chartType.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 30;//倾斜度(0～90)
                chartType.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic;//表面光泽度
                chartType.ChartAreas["ChartArea1"].AxisX.Interval = 1; //决定x轴显示文本的间隔，1为强制每个柱状体都显示，3则间隔3个显示
                chartType.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("宋体", 9, FontStyle.Regular);
                chartType.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            }

            //chartType.Series[0].XValueMember = "name";
            //chartType.Series[0].YValueMembers = "sumcount";
            this.chartType.Series[0].Points.DataBindXY(x, y);
        }
        #endregion

        #region 接口
        #endregion

        #region 事件

        /// <summary>
        /// 窗体打开前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmDocWorkQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            //清空统计指标
            ClearStaticsIndex();
            //设置日期初始值
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            sdtDate.Bdate.Value = d1;
            sdtDate.Edate.Value = d2;
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (sdtDate.Bdate.Value > sdtDate.Edate.Value)
                {
                    MessageBoxShowSimple("开始日期不能大于结束日期");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
                DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
                DataTable dtReport = (DataTable)InvokeController("GetIPDoctorWorkLoad", bdate, edate);
                if (dtReport.Rows.Count > 0)
                {
                    SetStaticsIndex(dtReport.Rows[0]);
                }
                else
                {
                    ClearStaticsIndex();
                }

                //绑定网格
                dgDocWork.DataSource = dtReport;
                //绑定图形数据源
                SetGraph(dtReport);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选择改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radPie_CheckedChanged(object sender, EventArgs e)
        {
            btnQuery_Click(null, null);
        }

        /// <summary>
        /// 选择改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radColumn_CheckedChanged(object sender, EventArgs e)
        {
            btnQuery_Click(null, null);
        }
        #endregion      
    }
}