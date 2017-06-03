using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WebFrame.WebAPI;
using HIS_Entity.OPManage;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace Api_PerformanceTest.WApiController
{
    /// <summary>
    /// 门诊收费性能测试接口
    /// http://localhost:8021/HISApi/PTProject.Service/Balance/Test
    /// http://localhost:8021/HISApi/PTProject.Service/Balance/Balance?visitNO=
    /// </summary>
    [efwplusApiController(PluginName = "PTProject.Service")]
    public class BalanceController : WebApiController
    {
        [HttpGet]
        public string Test()
        {
            //ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetHello");
            string hello = "Hello";
            return hello + " WebApi";
        }
        [HttpGet]
        public string BalancePres(string visitNO)
        {
            try
            {
              //  string visitNO = "34";
                int empid = 100;
                int deptid = 1048;
                SysLoginRight sys= new EFWCoreLib.CoreFrame.Business.SysLoginRight();
                sys.WorkId = 1;
                //获取病人信息
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                   
                    request.LoginRight = sys;
                    request.AddData(6);
                    request.AddData(visitNO);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetRegPatListByCardNo", requestAction);
                List<OP_PatList> patlist = retdata.GetData<List<OP_PatList>>(0);
                if (patlist.Count == 0)
                {
                    return "Falg:1";
                }
                OP_PatList curPatlist = patlist[0];
                //获取处方
                requestAction = ((ClientRequestData request) =>
                 {
                     request.LoginRight = sys;
                     request.AddData(curPatlist.PatListID);
                     request.AddData(OP_Enum.PresStatus.未收费);
                 });
                retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetPatPrescription", requestAction);
                DataTable dtPresc = retdata.GetData<DataTable>(0);
                //预算
                List<Prescription> budgePres = new List<Prescription>();
                List<int> budgeNums = new List<int>();
                List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dtPresc);
                budgeNums = GetBudgePrescriptionAmbitList(prescriptions);
                if (budgeNums.Count == 0)
                {
                    throw new Exception("没有选中的处方需要收费");
                }
                budgePres = prescriptions.Where(p => p.Selected == 1 && p.SubTotalFlag == 0).ToList();

                requestAction = ((ClientRequestData request) =>
               {
                   request.LoginRight = sys;
                   request.AddData(budgePres);//处方
                   request.AddData(empid);//操作员ID
                   request.AddData(curPatlist);//当前病人对象
                   request.AddData(curPatlist.PatTypeID);//结算病人类型 
                   request.AddData(budgeNums);//选中的处方张数
               });
                retdata = InvokeWcfService("OPProject.Service", "BalanceController", "BudgeBalance", requestAction);
                List<ChargeInfo> budgeInfo = retdata.GetData<List<ChargeInfo>>(0);
                //正式结算
                List<OP_CostPayMentInfo> paylist = new List<OP_CostPayMentInfo>();

                OP_CostPayMentInfo payment = new OP_CostPayMentInfo();
                payment.PayMentID = 1002;
                payment.PayMentMoney = 600;
                paylist.Add(payment);

                budgeInfo[0].PayInfoList = paylist;
                budgeInfo[0].PayTotalFee = 600;
                budgeInfo[0].FavorableTotalFee = 0;
                budgeInfo[0].PosFee = 0;
                budgeInfo[0].CashFee = 600;//现金
                budgeInfo[0].RoundFee = 0;
                budgeInfo[0].ChangeFee = 0;
                requestAction = ((ClientRequestData request) =>
                {
                    request.LoginRight = sys;
                    request.AddData(budgePres);//费用明细对象
                    request.AddData(empid);//操作员ID
                    request.AddData(curPatlist);//当前病人对象          
                    request.AddData(budgeInfo);//预算对象   
                    request.AddData(1);
                    request.AddData(empid);
                    request.AddData(deptid);
                });
                retdata = InvokeWcfService("OPProject.Service", "BalanceController", "Balance", requestAction);
                return "Falg:0";
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        private List<int> GetBudgePrescriptionAmbitList(List<Prescription> prescriptions)
        {
            List<int> prescNums = new List<int>();
            for (int i = 0; i < prescriptions.Count; i++)
            {
                if (prescriptions[i].Selected == 0)
                {
                    continue;
                }
                if (Convert.IsDBNull(prescriptions[i].PrescGroupID))
                    continue;
                int ambit = Convert.ToInt32(prescriptions[i].PrescGroupID);
                if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescNums.Add(ambit);
                }
            }
            return prescNums;
        }
    }
}
