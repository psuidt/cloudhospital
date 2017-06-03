using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.FinanceMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    public partial class FrmMaterialBalance : BaseFormBusiness, IFrmMaterialBalance
    {
        public FrmMaterialBalance()
        {
            InitializeComponent();
        }
    }
}
