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
using HIS_MIInterface.Winform.IView;
using HIS_Entity.MIManage;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace HIS_MIInterface.Winform.ViewForm
{
    public partial class FrmMITest : BaseFormBusiness, IFrmMITest
    {
        public FrmMITest()
        {
            InitializeComponent();
        }

        #region 匹配界面用
        private void button11_Click(object sender, EventArgs e)
        {
            InvokeController("M_GetHISCatalogInfo");
        }
        #endregion

        public class cl
        {
            public string f { set; get; }
            public string a { set; get; }

            public string b { set; get; }

            public string d { set; get; }

            public string e { set; get; }
            public int c { set; get; }
        }
        private void btnGetCardInfo_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            List<cl> list = new List<cl>();
            list.Add(new cl() { f = "001", a = "A", b = "as", d = "as", c = 3, e = "1" });
            list.Add(new cl() { f = "001", a = "A", b = "as1", d = "as", c = 3, e = "1" });
            list.Add(new cl() { f = "002", a = "A", b = "as1", d = "as", c = 3, e = "1" });
            list.Add(new cl() { f = "003", a = "B", b = "as2", d = "as", c = 3, e = "0" });
            list.Add(new cl() { f = "003", a = "B", b = "as3", d = "as", c = 3, e = "1" });
            list.Add(new cl() { f = "003", a = "B", b = "as4", d = "as", c = 3, e = "1" });
            list.Add(new cl() { f = "004", a = "B", b = "as4", d = "as", c = 3, e = "1" });

            var q = from item in list
                    where item.e=="1"
                    group item by new { item.f, item.a,item.d }  into g
                    select new
                    {
                        f = g.Key.f,
                        a = g.Key.a,
                        d = g.Key.d,
                        Count = g.Sum(x => x.c)
                    };

            foreach (var v in q)
            {
                Console.WriteLine("Name={0}      Count={1}       BatNum={2}", v.f, v.a, v.Count, v.d);
            }

            InvokeController("Mz_GetCardInfo",sCardNo);
        }

        private void btnGetPatientInfo_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            InvokeController("Mz_GetPersonInfo", sCardNo);
        }

        private void btnMzRegistration_Click(object sender, EventArgs e)
        {
            MI_Register register = new MI_Register();
            register.SerialNO = txtMzCenterCostID.Text.ToString();
            register.MedicalClass = txtMedicalClass.Text.ToString();
            register.PatientType = 1;
            register.DeptName = txtDeptName.Text.ToString(); 
            register.DiagnDocID = txtDiagnDocID.Text.ToString();
            register.Doctor = txtDoctor.Text.ToString();
            register.GHFee = Convert.ToDecimal(txtGHFee.Text.ToString());
            register.JCFee = Convert.ToDecimal(txtJCFee.Text.ToString());
            register.PatientID = Convert.ToInt32(txtPatientId.Text.ToString());
            register.PatientName = txtPatientName.Text;
            register.PersonalCode = txtCardNo.Text;
            register.IdentityNum = txtIdNum.Text;
            register.MIID = 1;
            InvokeController("MZ_PreviewRegister", register);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int regId = Convert.ToInt32(txtregisterID.Text);
            string serialNO = txtMzCenterCostID.Text;
            //MI_Register MI_Register=
            InvokeController("MZ_Register", regId, serialNO);
        }
       
        private void btnCancelReg_Click(object sender, EventArgs e)
        {
            string serialNO = txtMzCenterCostID.Text;
            InvokeController("Mz_CancelRegister",  serialNO);

        }

        private void btnMzUpload_Click(object sender, EventArgs e)
        {
            TradeData tradeData = new TradeData();
            Tradeinfo tradeinfo = new Tradeinfo();
            RecipeList recipeList = new RecipeList();
            FeeitemList feeitemList = new FeeitemList();
            tradeinfo.tradeType = TradeType.普通门诊;
            tradeinfo.billtype = "0";

            List<Recipe> recipes = new List<Recipe>();
            Recipe recipe = new Recipe();
            recipes.Add(recipe);
            recipeList.recipes= recipes;

            List<Feeitem> feeitems = new List<Feeitem>();
            foreach (DataRow dr in ((DataTable)dataGridView2.DataSource).Rows)
            {                
                Feeitem feeitem = new Feeitem();
                feeitem.itemno = dr["PresDetailID"].ToString();
                feeitem.recipeno = dr["presNO"].ToString();
                feeitem.hiscode = dr["ItemID"].ToString();
                feeitem.itemname = dr["ItemName"].ToString();
                feeitem.itemtype = dr["ItemType"].ToString();
                feeitem.unitprice = dr["RetailPrice"].ToString();
                feeitem.count = dr["Amount"].ToString();
                feeitem.fee = dr["TotalFee"].ToString();
                feeitem.babyflag = "0";

                feeitems.Add(feeitem);
            }
            feeitemList.feeitems = feeitems;

            tradeData.MIID = 1;
            tradeData.SerialNo = txtMzCenterCostID.Text.ToString().Trim();

            tradeData.tradeinfo = tradeinfo;
            tradeData.recipeList = recipeList;
            tradeData.feeitemList = feeitemList;

            InvokeController("MZ_PreviewCharge", tradeData);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            string fph = txtMzCenterCostID.Text;
            InvokeController("Mz_GetCardNo", fph);
        }

        public void LoadFee(DataTable dt)
        {
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = " itemid<>0 ";
            //dataGridView2.DataSource = dv.ToTable();

            DataTable dtcopy = dt.Clone();

            dtcopy.ImportRow(dt.Rows[0]);
            dtcopy.Rows[0]["itemid"] = "101311501";
            dataGridView2.DataSource = dtcopy;

        }

        private void btnMzSettlement_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            int recordId = Convert.ToInt32(txtRecordID.Text);
            string fph = textBox4.Text.Trim();
            
            InvokeController("MZ_Charge", recordId,fph);
        }
        private void btnMzCancel_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            string fph = textBox4.Text.Trim();

            InvokeController("MZ_CancelCharge", fph);
        }

        #region 界面显示
        public void LoadTrade(decimal d)
        {
            txtPersoncount2.Text = d.ToString();
        }
        public void LoadCatalogInfo(DataTable dt)
        {
            dataGridView1.DataSource = dt;
        }

        public void LoadPatientInfo(PatientInfo patientInfo)
        {
            txtCardNo.Text = patientInfo.CardNo;
            txtPatientName.Text = patientInfo.PersonName;
            txtIdNum.Text = patientInfo.IdNo;
            txtPersoncount.Text = patientInfo.PersonCount;
        }

        public void LoadRegisterInfo(Dictionary<string, string> dic)
        {
            txtregisterID.Text = dic.ContainsKey("Id") ? dic["Id"] : "";//dt.Rows[0]["registerId"].ToString();
            txtMzSerialNo.Text = dic.ContainsKey("tradeno") ? dic["tradeno"] : "";//dt.Rows[0]["registerId"].ToString();
            txtFeeAll.Text = dic.ContainsKey("FeeAll") ? dic["FeeAll"] : "";//dt.Rows[0]["FeeAll"].ToString();
            txtFeeFund.Text = dic.ContainsKey("fund") ? dic["fund"] : "";//dt.Rows[0]["fund"].ToString();
            txtFeeSelf.Text = dic.ContainsKey("cash") ? dic["cash"] : "";//dt.Rows[0]["cash"].ToString();
            txtFeeCount.Text = dic.ContainsKey("personcountpay") ? dic["personcountpay"] : "";//dt.Rows[0]["personcountpay"].ToString();
        }

        public void LoadTradeInfo(Dictionary<string, string> dic)
        {
            txtPersoncount2.Text = dic.ContainsKey("personcount") ? dic["personcount"] : "0";//dt.Rows[0]["registerId"].ToString();
        }

        public void PreviewCharge(Dictionary<string, string> dic)
        {
            txtRecordID.Text = dic.ContainsKey("Id") ? dic["Id"] : "";//dt.Rows[0]["registerId"].ToString();
            txtFeeAll.Text = dic.ContainsKey("FeeAll") ? dic["FeeAll"] : "";//dt.Rows[0]["FeeAll"].ToString();
            txtFeeFund.Text = dic.ContainsKey("fund") ? dic["fund"] : "";//dt.Rows[0]["fund"].ToString();
            txtFeeSelf.Text = dic.ContainsKey("cash") ? dic["cash"] : "";//dt.Rows[0]["cash"].ToString();
            txtFeeCount.Text = dic.ContainsKey("personcountpay") ? dic["personcountpay"] : "";//dt.Rows[0]["personcountpay"].ToString();
        }
        #endregion

        #region 调用接口Dll
        private void button14_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            InvokeController("Mz_GetCardInfoDll", sCardNo);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            InvokeController("Mz_GetPersonInfoDll", sCardNo);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MI_Register register = new MI_Register();
            register.SerialNO = txtMzCenterCostID.Text.ToString();
            register.MedicalClass = txtMedicalClass.Text.ToString();
            register.PatientType = 1;
            register.DeptName = txtDeptName.Text.ToString();
            register.DiagnDocID = txtDiagnDocID.Text.ToString();
            register.Doctor = txtDoctor.Text.ToString();
            register.GHFee = Convert.ToDecimal(txtGHFee.Text.ToString());
            register.JCFee = Convert.ToDecimal(txtJCFee.Text.ToString());
            register.PatientID = Convert.ToInt32(txtPatientId.Text.ToString());
            register.PatientName = txtPatientName.Text;
            register.PersonalCode = txtCardNo.Text;
            register.IdentityNum = txtIdNum.Text;
            register.MIID = 1;
            InvokeController("MZ_PreviewRegisterDll", register);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int regId = Convert.ToInt32(txtregisterID.Text);
            string serialNO = txtMzCenterCostID.Text;
            //MI_Register MI_Register=
            InvokeController("MZ_RegisterDll", regId, serialNO);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string serialNO = txtMzCenterCostID.Text;
            InvokeController("Mz_CancelRegisterDll", serialNO);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            TradeData tradeData = new TradeData();
            Tradeinfo tradeinfo = new Tradeinfo();
            RecipeList recipeList = new RecipeList();
            FeeitemList feeitemList = new FeeitemList();
            tradeinfo.tradeType = TradeType.普通门诊;
            tradeinfo.billtype = "0";

            List<Recipe> recipes = new List<Recipe>();
            Recipe recipe = new Recipe();
            recipes.Add(recipe);
            recipeList.recipes = recipes;

            List<Feeitem> feeitems = new List<Feeitem>();
            foreach (DataRow dr in ((DataTable)dataGridView2.DataSource).Rows)
            {
                Feeitem feeitem = new Feeitem();
                feeitem.itemno = dr["PresDetailID"].ToString();
                feeitem.recipeno = dr["presNO"].ToString();
                feeitem.hiscode = dr["ItemID"].ToString();
                feeitem.itemname = dr["ItemName"].ToString();
                feeitem.itemtype = dr["ItemType"].ToString();
                feeitem.unitprice = dr["RetailPrice"].ToString();
                feeitem.count = dr["Amount"].ToString();
                feeitem.fee = dr["TotalFee"].ToString();
                feeitem.babyflag = "0";

                feeitems.Add(feeitem);
            }
            feeitemList.feeitems = feeitems;

            tradeData.MIID = 1;
            tradeData.SerialNo = txtMzCenterCostID.Text.ToString().Trim();

            tradeData.tradeinfo = tradeinfo;
            tradeData.recipeList = recipeList;
            tradeData.feeitemList = feeitemList;

            InvokeController("MZ_PreviewChargeDll", tradeData);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            int recordId = Convert.ToInt32(txtRecordID.Text);
            string fph = textBox4.Text.Trim();

            InvokeController("MZ_ChargeDll", recordId, fph);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            string fph = textBox4.Text.Trim();

            InvokeController("MZ_CancelChargeDll", fph);
        }
        #endregion

        private void button28_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            InvokeController("Mz_GetCardInfoDllNew", sCardNo);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            InvokeController("Mz_GetPersonInfoDllNew", sCardNo);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MI_Register register = new MI_Register();
            register.SerialNO = txtMzCenterCostID.Text.ToString();
            register.MedicalClass = txtMedicalClass.Text.ToString();
            register.PatientType = 1;
            register.DeptName = txtDeptName.Text.ToString();
            register.DiagnDocID = txtDiagnDocID.Text.ToString();
            register.Doctor = txtDoctor.Text.ToString();
            register.GHFee = Convert.ToDecimal(txtGHFee.Text.ToString());
            register.JCFee = Convert.ToDecimal(txtJCFee.Text.ToString());
            register.PatientID = Convert.ToInt32(txtPatientId.Text.ToString());
            register.PatientName = txtPatientName.Text;
            register.PersonalCode = txtCardNo.Text;
            register.IdentityNum = txtIdNum.Text;
            InvokeController("MZ_PreviewRegisterDllNew", register);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            int regId = Convert.ToInt32(txtregisterID.Text);
            string serialNO = txtMzCenterCostID.Text;
            //MI_Register MI_Register=
            InvokeController("MZ_RegisterDllNew", regId, serialNO);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string serialNO = txtMzCenterCostID.Text;
            InvokeController("Mz_CancelRegisterDllNew", serialNO);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            TradeData tradeData = new TradeData();
            Tradeinfo tradeinfo = new Tradeinfo();
            RecipeList recipeList = new RecipeList();
            FeeitemList feeitemList = new FeeitemList();
            tradeinfo.tradeType = TradeType.普通门诊;
            tradeinfo.billtype = "0";

            List<Recipe> recipes = new List<Recipe>();
            Recipe recipe = new Recipe();
            recipes.Add(recipe);
            recipeList.recipes = recipes;

            List<Feeitem> feeitems = new List<Feeitem>();
            foreach (DataRow dr in ((DataTable)dataGridView2.DataSource).Rows)
            {
                Feeitem feeitem = new Feeitem();
                feeitem.itemno = dr["PresDetailID"].ToString();
                feeitem.recipeno = dr["presNO"].ToString();
                feeitem.hiscode = dr["ItemID"].ToString();
                feeitem.itemname = dr["ItemName"].ToString();
                feeitem.itemtype = dr["ItemType"].ToString();
                feeitem.unitprice = dr["RetailPrice"].ToString();
                feeitem.count = dr["PresAmount"].ToString();
                feeitem.fee = dr["TotalFee"].ToString();
                feeitem.babyflag = "0";

                feeitems.Add(feeitem);
            }
            feeitemList.feeitems = feeitems;

            tradeData.MIID = 1;
            tradeData.SerialNo = txtMzCenterCostID.Text.ToString().Trim();

            tradeData.tradeinfo = tradeinfo;
            tradeData.recipeList = recipeList;
            tradeData.feeitemList = feeitemList;

            InvokeController("MZ_PreviewChargeDllNew", tradeData);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            int recordId = Convert.ToInt32(txtRecordID.Text);
            string fph = textBox4.Text.Trim();

            InvokeController("MZ_ChargeDllNew", recordId, fph);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请填入正确发票号！");
                return;
            }
            string fph = textBox4.Text.Trim();

            InvokeController("MZ_CancelChargeDllNew", fph);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int regId = Convert.ToInt32(txtregisterID.Text);
            string serialNO = txtMzCenterCostID.Text;
            //MI_Register MI_Register=
            InvokeController("MZ_UpdateRegisterDllNew", regId, serialNO);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InvokeController("MZ_ClearDataDllNew");
        }
    }

}
