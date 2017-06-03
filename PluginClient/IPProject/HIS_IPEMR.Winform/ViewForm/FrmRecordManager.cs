using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPEMR.Winform.IView;

namespace HIS_IPEMR.Winform.ViewForm
{
    public partial class FrmRecordManager : BaseFormBusiness, IFrmRecordManager
    {
        public FrmRecordManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择科室ID
        /// </summary>
        public int DeptId
        {
            get
            {
                return Convert.ToInt32(tbcdept.SelectedValue);
            }

            set
            {
                tbcdept.SelectedValue = value;
            }
        }

        private int roletype = 0;
        /// <summary>
        /// 选择模板权限
        /// </summary>
        public int RoleType
        {
           get {
                
                if (rdoAll.Checked)
                {
                    roletype = 1;
                }
                else if (rdoDept.Checked)
                {
                    roletype = 2;
                }

                return roletype;
            }
            set
            {
                roletype = value;
            }
        }

        private int temptypeId;
        /// <summary>
        /// 选择病历树节点
        /// </summary>
        public int TempTypeID
        {
            get
            {
                temptypeId = Convert.ToInt32(tvMedicalTree.SelectedNode.Name);
                return temptypeId;
            }
            set
            {
                temptypeId = value;
            }
        }

        public int MedicalTreeId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int tempTypeId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TemplateName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void BindDept(DataTable dtDept)
        {
            throw new NotImplementedException();
        }

        public void BindMedicalTree(DataTable dtMedicalTree)
        {
            throw new NotImplementedException();
        }

        public void BindTemplateRecord(DataTable dtRecord)
        {
            throw new NotImplementedException();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        
    }
}
