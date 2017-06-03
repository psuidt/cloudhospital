using EFWCoreLib.CoreFrame.Business;
using HIS_IPEMR.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS_IPEMR.Winform.ViewForm
{
    public partial class FrmEmrModel : BaseFormBusiness,IFrmEmrModel
    {
        public FrmEmrModel()
        {
            InitializeComponent();
        }
    }
}
