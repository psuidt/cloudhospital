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
using HIS_Entity.Mongo;

namespace HIS_IPEMR.Winform.ViewForm
{
    public partial class FrmEmrManager : BaseFormBusiness, IFrmEmrManager
    {
        public FrmEmrManager()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();
            //emrControl1.OpenfileMongoDb();
            List<EmrPatData> emrlist=(List<EmrPatData>)InvokeController("GetEmrAllfromMongoDB");
            foreach (var emr in emrlist)
            {
                toolStripComboBox1.Items.Add(emr.id_string);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            emrControl1.OpenfileMongoDb(toolStripComboBox1.Text);
        }
    }
}
