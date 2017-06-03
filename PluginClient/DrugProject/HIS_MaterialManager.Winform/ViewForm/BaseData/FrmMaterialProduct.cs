using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 生产厂家维护
    /// </summary>
    public partial class FrmMaterialProduct : BaseFormBusiness, IFrmMaterialProduct
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialProduct()
        {
            InitializeComponent();
            bindGridSelectIndex(dgProduct);
            fmCommon.AddItem(this.txtProduct, "ProductName", "生产产家不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(this.txtAddress, "Address");
            fmCommon.AddItem(this.txtPeople, "LinkMan");
            fmCommon.AddItem(this.txtPhone, "PhoneNO");
        }

        /// <summary>
        /// 绑定厂家列表
        /// </summary>
        /// <param name="dt">厂家列表</param>
        public void LoadProduct(DataTable dt)
        {
            this.dgProduct.DataSource = dt;
            this.lbCount.Text = dt.Rows.Count.ToString();
        }

        /// <summary>
        /// 选中的厂家信息
        /// </summary>
        public MW_ProductDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            CurrentData = null;
            GetQuryContion();
            InvokeController("GetProduct");
        }

        /// <summary>
        /// 获取查询条件
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
        /// 新增
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.txtProduct.Focus();
            CurrentData = null;
            fmCommon.Clear();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, System.EventArgs e)
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

                MW_ProductDic productDic = null;
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
                    productDic = new MW_ProductDic();
                }

                try
                {
                    fmCommon.GetValue<MW_ProductDic>(productDic);
                    productDic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productDic.ProductName);
                    productDic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productDic.ProductName);
                    CurrentData = productDic;
                    InvokeController("SaveProduct");
                    setGridSelectIndex(dgProduct);
                    this.btnAdd.Focus();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 验证手机号合法性
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>true：验证通过</returns>
        public bool RegexPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$").IsMatch(txt);
        }

        /// <summary>
        /// 验证手机号合法性
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>true：验证通过</returns>
        public bool RegexTelPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$").IsMatch(txt);
        }

        /// <summary>
        /// 打开界面前加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialProduct_OpenWindowBefore(object sender, EventArgs e)
        {
            QueryCondition = new Dictionary<string, string>();
            InvokeController("GetProduct");
        }

        /// <summary>
        /// 选中厂家信息加载厂家详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgProduct_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgProduct.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgProduct.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgProduct.DataSource;
            MW_ProductDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_ProductDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<MW_ProductDic>(pruDic);
        }

        /// <summary>
        /// 选中厂家信息加载厂家详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgProduct_Click(object sender, EventArgs e)
        {
            if (dgProduct.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgProduct.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgProduct.DataSource;
            MW_ProductDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_ProductDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<MW_ProductDic>(pruDic);
        }

        /// <summary>
        /// 输入查询条件自动查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtPructNameQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
    }
}
