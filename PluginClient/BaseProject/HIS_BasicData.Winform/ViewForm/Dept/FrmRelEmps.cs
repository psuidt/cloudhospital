﻿using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_BasicData.Winform.ViewForm.Dept
{
    /// <summary>
    /// 人员关联病区
    /// </summary>
    public partial class FrmRelEmps : BaseFormBusiness, IFrmRels
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRelEmps()
        {
            InitializeComponent();
        }

        #region IFrmRelDepts

        /// <summary>
        /// 病区ID
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int WorkId { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 加载关联列表
        /// </summary>
        /// <param name="rels">关联列表</param>
        public void LoadRels(DataTable rels)
        {
            dgRels.DataSource = rels;
        }

        #endregion

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmRelDepts_Load(object sender, EventArgs e)
        {
            InvokeController("LoadWardDeptAndEmps", WorkId, WardId, Controller.RelDeptAndEmp.Emp);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            InvokeController("LoadWardDeptAndEmps", WorkId, WardId, Controller.RelDeptAndEmp.Emp);
        }

        /// <summary>
        /// 选中人员
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgRelDepts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var colIndex = e.ColumnIndex;
            if (rowIndex >= 0 && colIndex == 0)
            {
                var dtDataSource = dgRels.DataSource as DataTable;
                if (null == dtDataSource)
                {
                    return;
                }

                var dr = dtDataSource.Rows[rowIndex];
                var value = dr["bFlag"] as int?;
                if (value.HasValue)
                {
                    var iFlag = value.Value != 0 ? 0 : 1;
                    dr["bFlag"] = iFlag;
                }
                else
                {
                    dr["bFlag"] = 1;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var ret = InvokeController("SaveRelEmps", dgRels.DataSource as DataTable) + string.Empty;
            if (!string.IsNullOrEmpty(ret))
            {
                Result = true;
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                Result = false;
                MessageBoxShowSimple("保存失败,请重试");
            }
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmRelDepts_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnReset_Click(null, null);
                    break;
                case Keys.F7:
                    btnQuit_Click(null, null);
                    break;
                case Keys.F8:
                    btnSave_Click(null, null);
                    break;
            }
        }
    }
}
