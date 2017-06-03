using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 生产商
    /// </summary>
    public partial class FrmProduct : BaseFormBusiness, IFrmProduct
    {
        #region FORM 
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmProduct()
        {
            InitializeComponent();

            bindGridSelectIndex(dgProduct);

            fmCommon.AddItem(this.txtProduct, "ProductName", true, "生产产家不能为空！", InvalidType.Empty, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtAddress, "Address", false, null, InvalidType.Custom, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtPeople, "LinkMan", false, null, InvalidType.Custom, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtPhone, "PhoneNO", false, null, InvalidType.Custom, null, EN_CH.CH, null);
        }

        /// <summary>
        /// 读取生产商
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadProduct(DataTable dt)
        {
            this.dgProduct.DataSource = dt;
            this.lbCount.Text = dt.Rows.Count.ToString();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmProduct_OpenWindowBefore(object sender, EventArgs e)
        {
            QueryCondition = new Dictionary<string, string>();
            InvokeController("GetProduct");
        }

        /// <summary>
        /// 当前生产商对象
        /// </summary>
        public DG_ProductDic CurrentData { get; set; }

        /// <summary>
        /// 查询对象
        /// </summary>
        public Dictionary<string, string> QueryCondition { get; set; }
        #endregion

        #region 事件
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.txtProduct.Focus();
            CurrentData = null;
            fmCommon.Clear();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            CurrentData = null;
            GetQuryContion();
            InvokeController("GetProduct");
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        private void GetQuryContion()
        {
            QueryCondition = new Dictionary<string, string>();
            if (txtPructNameQ.Text.Trim() != string.Empty)
            {
                QueryCondition.Add("PYCode", txtPructNameQ.Text);
                QueryCondition.Add("ProductName", txtPructNameQ.Text);
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

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fmCommon.Validate())
            {
                if (this.txtPhone.Text.Trim() != string.Empty)
                {
                    if (!RegexTelPhone(this.txtPhone.Text))
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("联系人号码不正确");
                        this.txtPhone.Focus();
                        return;
                    }
                }

                DG_ProductDic productDic = null;
                if (CurrentData != null)
                {
                    if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    productDic = CurrentData;
                }
                else
                {
                    productDic = new DG_ProductDic();
                }

                try
                {
                    fmCommon.GetValue<DG_ProductDic>(productDic);
                    productDic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productDic.ProductName);
                    productDic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productDic.ProductName);
                    CurrentData = productDic;
                    InvokeController("SaveProduct");
                    setGridSelectIndex(dgProduct);
                    CurrentData = null;
                    this.btnAdd.Focus();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 点击按钮
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgProduct_Click(object sender, EventArgs e)
        {
            if (dgProduct.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgProduct.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgProduct.DataSource;
            DG_ProductDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_ProductDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_ProductDic>(pruDic);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelete_Click(object sender, EventArgs e)
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
                        InvokeController("DeletePruduct");
                        CurrentData = null;
                        setGridSelectIndex(dgProduct);
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
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtPructNameQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnQuery_Click(null, null);
                ((TextBoxX)sender).SelectAll();
                this.txtPructNameQ.Focus();
            }
        }

        /// <summary>
        /// 加载厂家下拉框
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDisease_textboxcard(DataTable dt)
        {
            //tbcPruductName.DisplayField = "ProductName";
            //tbcPruductName.MemberField = "ProductID";
            //tbcPruductName.CardColumn = "ProductID|ID|50,ProductName|厂家名称|auto";
            //tbcPruductName.QueryFieldsString = "ProductName,PYCode,WBCode";
            //tbcPruductName.ShowCardWidth = 350;
            //tbcPruductName.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnSave.Focus();
            }
        }

        /// <summary>
        /// 验证电话号码合法性
        /// </summary>
        /// <param name="txt">电话号码</param>
        /// <returns>是否合法</returns>
        public bool RegexPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$").IsMatch(txt);
        }

        /// <summary>
        /// 验证手机号码合法性
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>是否合法</returns>
        public bool RegexTelPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$").IsMatch(txt);
        }

        #endregion
        /// <summary>
        /// 选中行改变事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgProduct_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgProduct.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgProduct.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgProduct.DataSource;
            DG_ProductDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_ProductDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_ProductDic>(pruDic);
        }
    }
}