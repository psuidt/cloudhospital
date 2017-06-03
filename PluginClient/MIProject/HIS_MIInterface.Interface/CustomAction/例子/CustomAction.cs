using HIS_Entity.MIManage;
using HIS_Entity.MIManage.Common;
using HIS_MIInterface.Interface.BaseClass;
using SiInterfaceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Interface.CustomAction.例子
{
    public class CustomAction : AbstractMIAction<AbstractHISDao, CustomMIInterfaceDao>
    {
        public int _WorkId = 0;
        public CustomAction(int WorkId)
        {
            _WorkId = WorkId;
            hisDao.WorkId(WorkId);
        }
        SiInterfaceDll sDll;
        private bool IsNew = false;
        #region 门诊
        public override ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            sDll = new SiInterfaceDll();
            return ybInterfaceDao.Mz_GetCardInfo(sDll,inputClass.SInput[InputType.CardNo].ToString());
        }

        public override ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            sDll = new SiInterfaceDll();
            return ybInterfaceDao.Mz_GetPersonInfo(sDll,inputClass.SInput[InputType.CardNo].ToString());
        }

        public override ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            ResultClass resultClass;
            Reg.root root = PreviewRegisterToInput((MI_Register)inputClass.SInput[InputType.MI_Register]);
            resultClass = ybInterfaceDao.MZ_PreviewRegister(sDll, root);
            resultClass = hisDao.MZ_PreviewRegister(null);
            return resultClass;
        }

        public override ResultClass MZ_Register(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        public override ResultClass MZ_CancelRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        public override ResultClass MZ_Charge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        } 






        public override ResultClass MZ_DownloadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }      

        public override ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        public override ResultClass MZ_UploadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region 公用方法
        private Reg.root PreviewRegisterToInput(MI_Register register)
        {

            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;
            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "17";
            tradeinfo.billtype = "0";

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe recipe = new Reg.recipe();
            recipe.diagnoseno = "1";
            recipe.recipeno = "1";
            recipe.recipedate = System.DateTime.Now.ToString("yyyyMMdd hhmmss");
            recipe.recipetype = "1";
            recipe.helpmedicineflag = "0";

            recipe.hissectionname = register.DeptName;
            recipe.drid = register.DiagnDocID;
            recipe.drname = register.Doctor;
            recipe.registertradeno = register.SerialNO.ToString();
            recipe.billstype = "1";
            Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag 必填
            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            Reg.feeitem feeitem = new Reg.feeitem();
            feeitem.itemno = "1";
            feeitem.recipeno = "1";
            feeitem.hiscode = System.DateTime.Now.ToString("yyyyMMdd hhmmss");
            feeitem.itemname = "1";
            feeitem.itemtype = "0";
            feeitem.unitprice = register.DeptName;
            feeitem.count = register.DiagnDocID;
            feeitem.fee = register.Doctor;
            feeitem.babyflag = "0";

            Reg.feeitem[] feeitems = { feeitem };
            recipearray.recipe = recipes;

            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }

        private Reg.root TradeDataToInput(TradeData tradeData)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;

            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = ((int)tradeData.tradeinfo.tradeType).ToString();
            tradeinfo.billtype = tradeData.tradeinfo.billtype;

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = "";
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe[] recipes = new Reg.recipe[tradeData.recipeList.recipes.Count];
            for (int i = 0; i < tradeData.recipeList.recipes.Count; i++)
            {
                Reg.recipe recipe = new Reg.recipe();
                recipe.diagnoseno = tradeData.recipeList.recipes[i].diagnoseno;
                recipe.recipeno = tradeData.recipeList.recipes[i].recipeno;
                recipe.recipedate = tradeData.recipeList.recipes[i].recipedate;
                recipe.recipetype = tradeData.recipeList.recipes[i].recipetype;
                recipe.helpmedicineflag = tradeData.recipeList.recipes[i].helpmedicineflag;

                recipe.billstype = "1";
                recipes[i] = recipe;
            }

            recipearray.recipe = recipes;
            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag 必填
            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            Reg.feeitem[] feeitems = new Reg.feeitem[tradeData.feeitemList.feeitems.Count];
            for (int i = 0; i < tradeData.feeitemList.feeitems.Count; i++)
            {
                Reg.feeitem feeitem = new Reg.feeitem();
                feeitem.itemno = tradeData.feeitemList.feeitems[i].itemno;
                feeitem.recipeno = tradeData.feeitemList.feeitems[i].recipeno;
                feeitem.hiscode = tradeData.feeitemList.feeitems[i].hiscode;
                feeitem.itemname = tradeData.feeitemList.feeitems[i].itemname;
                feeitem.itemtype = tradeData.feeitemList.feeitems[i].itemtype;
                feeitem.unitprice = tradeData.feeitemList.feeitems[i].unitprice;
                feeitem.count = tradeData.feeitemList.feeitems[i].count;
                feeitem.fee = tradeData.feeitemList.feeitems[i].fee;
                feeitem.babyflag = "0";
                feeitems[i] = feeitem;
            }

            feeitemarray.feeitem = feeitems;

            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }
        private MI_MedicalInsurancePayRecord ResultToPayRecord(DivideResult.output output)
        {
            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = new MI_MedicalInsurancePayRecord();
            if (output.tradeinfo != null)
            {
                medicalInsurancePayRecord.PatientType = 1;
                medicalInsurancePayRecord.TradeNO = output.tradeinfo.tradeno;
                medicalInsurancePayRecord.FeeNO = output.tradeinfo.feeno;
                medicalInsurancePayRecord.TradeTime = Tools.ToDateTime2(output.tradeinfo.tradedate, DateTime.Now);
            }

            if (output.sumpay != null)
            {
                medicalInsurancePayRecord.FeeAll = Tools.ToDecimal(output.sumpay.feeall, 0);
                medicalInsurancePayRecord.FeeFund = Tools.ToDecimal(output.sumpay.fund, 0);
                medicalInsurancePayRecord.FeeCash = Tools.ToDecimal(output.sumpay.cash, 0);
                medicalInsurancePayRecord.PersonCountPay = Tools.ToDecimal(output.sumpay.personcountpay, 0);
                medicalInsurancePayRecord.PersonCount = 0;
            }
            if (output.payinfo != null)
            {
                medicalInsurancePayRecord.FeeMIIn = Tools.ToDecimal(output.payinfo.mzfeein, 0);
                medicalInsurancePayRecord.FeeMIOut = Tools.ToDecimal(output.payinfo.mzfeeout, 0);
                medicalInsurancePayRecord.FeeDeductible = Tools.ToDecimal(output.payinfo.mzpayfirst, 0);
                medicalInsurancePayRecord.FeeSelfPay = Tools.ToDecimal(output.payinfo.mzselfpay2, 0);
                medicalInsurancePayRecord.FeeBigPay = Tools.ToDecimal(output.payinfo.mzbigpay, 0);
                medicalInsurancePayRecord.FeeBigSelfPay = Tools.ToDecimal(output.payinfo.mzbigselfpay, 0);
                medicalInsurancePayRecord.FeeOutOFPay = Tools.ToDecimal(output.payinfo.mzoutofbig, 0);
                medicalInsurancePayRecord.Feebcbx = Tools.ToDecimal(output.payinfo.bcpay, 0);
                medicalInsurancePayRecord.Feejcbz = Tools.ToDecimal(output.payinfo.jcbz, 0);
            }
            return medicalInsurancePayRecord;
        }

        private MI_MIPayRecordHead ResultToPayRecordHead(DivideResult.output output)
        {
            MI_MIPayRecordHead mIPayRecordHead = new MI_MIPayRecordHead();
            if (IsNew)
            {
                if (output.medicatalog2 != null)
                {
                    mIPayRecordHead.medicine = Tools.ToDecimal(output.medicatalog2.medicine, 0);
                    mIPayRecordHead.therb = Tools.ToDecimal(output.medicatalog2.therb, 0);
                    mIPayRecordHead.tmedicine = Tools.ToDecimal(output.medicatalog2.tmedicine, 0);
                    mIPayRecordHead.examine = Tools.ToDecimal(output.medicatalog2.examine, 0);
                    mIPayRecordHead.labexam = Tools.ToDecimal(output.medicatalog2.labexam, 0);
                    mIPayRecordHead.treatment = Tools.ToDecimal(output.medicatalog2.treatment, 0);
                    mIPayRecordHead.operation = Tools.ToDecimal(output.medicatalog2.operation, 0);
                    mIPayRecordHead.material = Tools.ToDecimal(output.medicatalog2.material, 0);
                    mIPayRecordHead.other = Tools.ToDecimal(output.medicatalog2.otheropfee, 0);

                    mIPayRecordHead.diagnosis = Tools.ToDecimal(output.medicatalog2.diagnosis, 0);
                    mIPayRecordHead.medicalservice = Tools.ToDecimal(output.medicatalog2.medicalservice, 0);
                    mIPayRecordHead.commonservice = Tools.ToDecimal(output.medicatalog2.commonservice, 0);
                    mIPayRecordHead.registfee = Tools.ToDecimal(output.medicatalog2.registfee, 0);

                }
            }
            else
            {
                if (output.medicatalog != null)
                {
                    mIPayRecordHead.medicine = Tools.ToDecimal(output.medicatalog.medicine, 0);
                    mIPayRecordHead.therb = Tools.ToDecimal(output.medicatalog.therb, 0);
                    mIPayRecordHead.tmedicine = Tools.ToDecimal(output.medicatalog.tmedicine, 0);
                    mIPayRecordHead.examine = Tools.ToDecimal(output.medicatalog.examine, 0);
                    mIPayRecordHead.labexam = Tools.ToDecimal(output.medicatalog.labexam, 0);
                    mIPayRecordHead.treatment = Tools.ToDecimal(output.medicatalog.treatment, 0);
                    mIPayRecordHead.operation = Tools.ToDecimal(output.medicatalog.operation, 0);
                    mIPayRecordHead.material = Tools.ToDecimal(output.medicatalog.material, 0);
                    mIPayRecordHead.other = Tools.ToDecimal(output.medicatalog.other, 0);

                    mIPayRecordHead.xray = Tools.ToDecimal(output.medicatalog.xray, 0);
                    mIPayRecordHead.ultrasonic = Tools.ToDecimal(output.medicatalog.ultrasonic, 0);
                    mIPayRecordHead.CT = Tools.ToDecimal(output.medicatalog.ct, 0);
                    mIPayRecordHead.mri = Tools.ToDecimal(output.medicatalog.mri, 0);
                    mIPayRecordHead.oxygen = Tools.ToDecimal(output.medicatalog.oxygen, 0);
                    mIPayRecordHead.bloodt = Tools.ToDecimal(output.medicatalog.bloodt, 0);
                    mIPayRecordHead.orthodontics = Tools.ToDecimal(output.medicatalog.orthodontics, 0);
                    mIPayRecordHead.prosthesis = Tools.ToDecimal(output.medicatalog.prosthesis, 0);
                    mIPayRecordHead.forensic_expertise = Tools.ToDecimal(output.medicatalog.forensic_expertise, 0);
                }
            }
            return mIPayRecordHead;
        }

        private List<MI_MIPayRecordDetail> ResultToPayRecordDetail(DivideResult.output output)
        {
            List<MI_MIPayRecordDetail> mIPayRecordDetailList = new List<MI_MIPayRecordDetail>();
            if (output.feeitems.Length > 0)
            {
                foreach (DivideResult.feeitem item in output.feeitems)
                {
                    if (item != null)
                    {
                        MI_MIPayRecordDetail mIPayRecordDetail = new MI_MIPayRecordDetail();
                        mIPayRecordDetail.itemno = item.itemno;
                        mIPayRecordDetail.recipeno = item.recipeno;
                        mIPayRecordDetail.hiscode = item.hiscode;
                        mIPayRecordDetail.itemcode = item.itemcode;
                        mIPayRecordDetail.itemname = item.itemname;
                        mIPayRecordDetail.itemtype = Tools.ToInt32(item.itemtype, 0);
                        mIPayRecordDetail.unitprice = Tools.ToDecimal(item.unitprice, 0);
                        mIPayRecordDetail.count = Tools.ToDecimal(item.count, 0);
                        mIPayRecordDetail.fee = Tools.ToDecimal(item.fee, 0);
                        mIPayRecordDetail.feein = Tools.ToDecimal(item.feein, 0);
                        mIPayRecordDetail.feeout = Tools.ToDecimal(item.feeout, 0);
                        mIPayRecordDetail.selfpay2 = Tools.ToDecimal(item.selfpay2, 0);
                        mIPayRecordDetail.state = Tools.ToInt32(item.state, 0);
                        mIPayRecordDetail.feetype = item.fee_type;
                        mIPayRecordDetail.preferentialfee = Tools.ToDecimal(item.preferentialfee, 0);
                        mIPayRecordDetail.preferentialscale = Tools.ToDecimal(item.preferentialscale, 0);
                        mIPayRecordDetailList.Add(mIPayRecordDetail);
                    }
                }
            }
            return mIPayRecordDetailList;
        }

        private Reg.root RegisterToInput(MI_Register register)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;

            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "11";
            tradeinfo.billtype = "0";
            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;

            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe recipe = new Reg.recipe();
            recipe.hissectionname = register.DeptName;
            recipe.drid = register.DiagnDocID;
            recipe.drname = register.Doctor;
            recipe.registertradeno = register.SerialNO.ToString();
            recipe.billstype = "1";
            Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;

            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            input.feeitemarray = feeitemarray;

            return root;
        }

        #endregion
    }
}
