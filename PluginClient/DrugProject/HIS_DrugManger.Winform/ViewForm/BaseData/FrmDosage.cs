using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmDosage : BaseFormBusiness, IFrmDosageManage
    {
        #region 接口
        /// <summary>
        /// 当前选中行对象
        /// </summary>
        public DG_DosageDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDosage()
        {
            InitializeComponent();
            bindGridSelectIndex(dgDosage);
            fmCommon.AddItem(this.txtName, "DosageName", "药品剂型名称不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(this.combDrugType, "TypeID");
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="dt">数据源</param>
        public void GetTotalNum(DataTable dt)
        {
            // this.lbCount.Text = dt.Rows.Count.ToString();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadData(DataTable dt)
        {
            this.dgDosage.DataSource = dt;
            this.lbCount.Text = dt.Rows.Count.ToString();
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDosage_OpenWindowBefore(object sender, EventArgs e)
        {
            QueryCondition = new Dictionary<string, string>();
            InvokeController("GetDosageData");
            InvokeController("GetDrugType");
        }

        /// <summary>
        /// 绑定类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindComBox(DataTable dt)
        {
            combDrugType.DisplayMember = "TypeName";
            combDrugType.ValueMember = "TypeID";
            combDrugType.DataSource = dt;
        }

        /// <summary>
        /// 绑定药品类型下拉框（查询条件）
        /// </summary>
        /// <param name="dtDrugType">药品类型数据源</param>
        public void BindComboBoxQuery(DataTable dtDrugType)
        {
            DataTable dt = dtDrugType.Copy();
            DataRow row = dt.NewRow();
            row["TypeID"] = -1;
            row["TypeName"] = "全部";
            dt.Rows.InsertAt(row, 0);
            combDrugTypeQuery.DisplayMember = "TypeName";
            combDrugTypeQuery.ValueMember = "TypeID";
            combDrugTypeQuery.DataSource = dt;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            CurrentData = null;
            GetQuryContion();
            InvokeController("GetDosageData");
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetQuryContion()
        {
            QueryCondition = new Dictionary<string, string>();
            if (txtNameQ.Text.Trim() != string.Empty)
            {
                QueryCondition.Add("PYCode", txtNameQ.Text.Trim());
            }

            if (combDrugTypeQuery.SelectedValue != null && combDrugTypeQuery.SelectedValue.ToString() != "-1")
            {
                QueryCondition.Add("TypeID", combDrugTypeQuery.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            CurrentData = null;
            this.txtName.Focus();
            fmCommon.Clear();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (CurrentData != null)
            {
                if (MessageBox.Show("确定要删除记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                if (fmCommon.Validate())
                {
                    try
                    {
                        InvokeController("DeleteDosage");
                        setGridSelectIndex(dgDosage);
                        CurrentData = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                        throw;
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("请选择记录删除！");
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (fmCommon.Validate())
            {
                DG_DosageDic dosageDic = null;
                if (CurrentData != null)
                {
                    if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.No)
                    {
                        return;
                    }

                    dosageDic = CurrentData;
                }
                else
                {
                    dosageDic = new DG_DosageDic();
                }

                try
                {
                    fmCommon.GetValue<DG_DosageDic>(dosageDic);
                    dosageDic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(dosageDic.DosageName.Trim());
                    dosageDic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(dosageDic.DosageName.Trim());
                    CurrentData = dosageDic;
                    InvokeController("SaveDosageData");                
                    setGridSelectIndex(dgDosage);
                    CurrentData = null;
                    txtName.Focus();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        /// <summary>
        /// 键盘操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtNameQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnQuery_Click(null, null);
                ((TextBoxX)sender).SelectAll();
                txtNameQ.Focus();
            }
        }

        /// <summary>
        /// 网格点击
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDosage_Click(object sender, EventArgs e)
        {
            if (dgDosage.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDosage.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDosage.DataSource;
            DG_DosageDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_DosageDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_DosageDic>(pruDic);
            combDrugType.SelectedValue = pruDic.TypeID;
        }

        /// <summary>
        /// 网格切换选中行
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDosage_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDosage.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDosage.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDosage.DataSource;
            DG_DosageDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_DosageDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_DosageDic>(pruDic);
        }
    }
}
