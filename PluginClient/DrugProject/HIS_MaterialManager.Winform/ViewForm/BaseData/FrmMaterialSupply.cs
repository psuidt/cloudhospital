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
    /// 物资供应商维护
    /// </summary>
    public partial class FrmMaterialSupply : BaseFormBusiness, IFrmMaterialSupply
    {
        /// <summary>
        /// 选中的供应商
        /// </summary>
        public MW_SupportDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, HIS_Entity.SqlAly.SqlOperator>> AndWhere
        {
            get; set;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, HIS_Entity.SqlAly.SqlOperator>> OrWhere
        {
            get; set;
        }

        /// <summary>
        /// 验证手机号合法性
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>true：合法</returns>
        public bool RegexPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$").IsMatch(txt);
        }

        /// <summary>
        /// 验证手机号合法性
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>true：合法</returns>
        public bool RegexTelPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$").IsMatch(txt);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialSupply()
        {
            InitializeComponent();
            bindGridSelectIndex(dgSupply);
            fmCommon.AddItem(this.txtName, "SupportName", "供应商名称不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(this.txtAddress, "Address");
            fmCommon.AddItem(this.txtMan, "LinkMan");
            fmCommon.AddItem(this.txtPhone, "PhoneNO");
        }
        
        /// <summary>
        /// 读取供应商数据
        /// </summary>
        /// <param name="dt">供应商列表</param>
        /// <param name="total">默认选择ID</param>
        public void LoadSupply(DataTable dt, int total)
        {
            this.lbCount.Text = total.ToString();
            pagerMember.DataSource = dt;
            pagerMember.totalRecord = total;
            setGridSelectIndex(dgSupply);
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialSupply_OpenWindowBefore(object sender, System.EventArgs e)
        {
            GetQuryContion();
            InvokeController("GetSupply", 1, pagerMember.pageSize);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">总页数</param>
        private void pagerMember_PageNoChanged(object sender, int pageNo, int pageSize)
        {
            InvokeController("GetSupply", pagerMember.pageNo, pagerMember.pageSize);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            CurrentData = null;
            GetQuryContion();
            InvokeController("GetSupply", 1, pagerMember.pageSize);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetQuryContion()
        {
            AndWhere = new List<Tuple<string, string, HIS_Entity.SqlAly.SqlOperator>>();
            OrWhere = new List<Tuple<string, string, HIS_Entity.SqlAly.SqlOperator>>();
            AndWhere.Add(Tuple.Create("delflag", "0", HIS_Entity.SqlAly.SqlOperator.Equal));
            if (txtNameQ.Text.Trim() != string.Empty)
            {
                OrWhere.Add(Tuple.Create("SupportName", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
                OrWhere.Add(Tuple.Create("PYCode", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
                OrWhere.Add(Tuple.Create("WBCode", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
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

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtName.Focus();
            CurrentData = null;
            fmCommon.Clear();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">控件</param>
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
                    InvokeController("DeleteSupply", pagerMember.pageNo, pagerMember.pageSize);
                    setGridSelectIndex(dgSupply);
                    CurrentData = null;
                }
            }
            else
            {
                MessageBoxEx.Show("请选择记录删除！");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
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

                MW_SupportDic productDic = null;
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
                    productDic = new MW_SupportDic();
                }

                try
                {
                    fmCommon.GetValue<MW_SupportDic>(productDic);
                    productDic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productDic.SupportName);
                    productDic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productDic.SupportName);
                    CurrentData = productDic;
                    InvokeController("SaveSupply", pagerMember.pageNo, pagerMember.pageSize);
                    setGridSelectIndex(dgSupply);
                    CurrentData = null;
                    this.txtName.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败" + ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// 选中供应商显示供应商详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgSupply_Click(object sender, EventArgs e)
        {
            if (dgSupply.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgSupply.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgSupply.DataSource;
            MW_SupportDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_SupportDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<MW_SupportDic>(pruDic);
        }

        /// <summary>
        /// 选中供应商显示供应商详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgSupply_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgSupply.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgSupply.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgSupply.DataSource;
            MW_SupportDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_SupportDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<MW_SupportDic>(pruDic);
        }

        /// <summary>
        /// 输入检索条件自动检索
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtNameQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
    }
}
