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
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 供应商
    /// </summary>
    public partial class FrmSupply : BaseFormBusiness, IFrmSupply
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmSupply()
        {
            InitializeComponent();
            bindGridSelectIndex(dgSupply);
            fmCommon.AddItem(this.txtName, "SupportName", true, "供应商名称不能为空！", InvalidType.Empty, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtAddress, "Address", false, null, InvalidType.Custom, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtMan, "LinkMan", false, null, InvalidType.Custom, null, EN_CH.CH, null);
            fmCommon.AddItem(this.txtPhone, "PhoneNO");
        }

        /// <summary>
        /// 读取供应商
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="total">总数量</param>
        public void LoadSupply(DataTable dt, int total)
        {
            this.lbCount.Text = total.ToString();
            pagerMember.DataSource = dt;
            pagerMember.totalRecord = total;
            setGridSelectIndex(dgSupply);
        }

        /// <summary>
        /// 打开窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmSupply_OpenWindowBefore(object sender, EventArgs e)
        {
            GetQuryContion();
            InvokeController("GetSupply", 1, pagerMember.pageSize);
        }

        /// <summary>
        /// 获取供应商对象
        /// </summary>
        public DG_SupportDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> AndWhere
        {
            get; set;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> OrWhere
        {
            get; set;
        }

        /// <summary>
        /// 电话合法性验证
        /// </summary>
        /// <param name="txt">电话号码</param>
        /// <returns>返回是否合法</returns>
        public bool RegexPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$").IsMatch(txt);
        }

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
            InvokeController("GetSupply", 1, pagerMember.pageSize);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        private void GetQuryContion()
        {
            AndWhere = new List<Tuple<string, string, SqlOperator>>();
            OrWhere = new List<Tuple<string, string, SqlOperator>>();
            AndWhere.Add(Tuple.Create("delflag", "0", SqlOperator.Equal));
            if (txtNameQ.Text.Trim() != string.Empty)
            {
                OrWhere.Add(Tuple.Create("SupportName", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
                OrWhere.Add(Tuple.Create("PYCode", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
                OrWhere.Add(Tuple.Create("WBCode", txtNameQ.Text.Trim(), HIS_Entity.SqlAly.SqlOperator.Like));
            }
        }

        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.txtName.Focus();
            CurrentData = null;
            fmCommon.Clear();
        }
        #endregion

        /// <summary>
        /// 删除按钮
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
        /// 保存按钮
        /// </summary>
        /// <param name="sender">对象</param>
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

                DG_SupportDic productDic = null;
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
                    productDic = new DG_SupportDic();
                }

                try
                {
                    fmCommon.GetValue<DG_SupportDic>(productDic);
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
        /// 手机号码验证函数
        /// </summary>
        /// <param name="txt">手机号码</param>
        /// <returns>是否合法</returns>
        public bool RegexTelPhone(string txt)
        {
            return new Regex(@"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$").IsMatch(txt);
        }

        /// <summary>
        /// 点击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgSupply_Click(object sender, EventArgs e)
        {
            if (dgSupply.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgSupply.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgSupply.DataSource;
            DG_SupportDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_SupportDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_SupportDic>(pruDic);
        }

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
        /// 键盘操作
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
        /// 改变当前选中行
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgSupply_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgSupply.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgSupply.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgSupply.DataSource;
            DG_SupportDic pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_SupportDic>(dt, rowindex);
            CurrentData = pruDic;
            fmCommon.Load<DG_SupportDic>(pruDic);
        }

        /// <summary>
        ///  翻页
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页数</param>
        private void pagerMember_PageNoChanged(object sender, int pageNo, int pageSize)
        {
            InvokeController("GetSupply", pagerMember.pageNo, pagerMember.pageSize);
        }
    }
}
