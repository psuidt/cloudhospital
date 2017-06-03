using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_MIInterface.Winform.IView;
using System.Windows.Forms;
using EFWCoreLib.WcfFrame.DataSerialize;
using System.Data;
using HIS_Entity.MIManage;
using HIS_Entity.OPManage;
using EFWCoreLib.CoreFrame.Common;
using HIS_MIInterface.Interface;
using static HIS_Entity.MIManage.Common.JsonUtil;

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMITest")]//与系统菜单对应
    [WinformView(Name = "FrmMITest", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmMITest")]
    public class MITestController : WcfClientController
    {
        //MIInterFace mIInterFace = new MIInterFace();
        //接口方法入参
        InputClass input = new InputClass();
        Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
        IFrmMITest iFrmMITest; //测试界面
        public override void Init()
        {
            iFrmMITest = (IFrmMITest)iBaseView["FrmMITest"];
            MIInterFace.WorkId = WorkId;
            input.SInput = dicStr;
        }

        [WinformMethod]
        public void M_GetHISCatalogInfo()
        {
            int catalogType = 3;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(catalogType);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetHISCatalogInfo", requestAction);
            //void retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetHISCatalogInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMITest.LoadCatalogInfo(dtMemberInfo);
        }


        #region 门诊接口

        [WinformMethod]
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public void Mz_GetCardInfo(string sCardNo)
        {
            dicStr.Clear();
            dicStr.Add(InputType.CardNo, sCardNo);

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_GetCardInfo", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);

            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(2);
                iFrmMITest.LoadCatalogInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_GetPersonInfo(string sCardNo)
        {
            dicStr.Clear();
            dicStr.Add(InputType.CardNo, sCardNo);

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_GetPersonInfo", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);

            if (b)
            {
                List<PatientInfo> patientInfoList = retdataMember.GetData<List<PatientInfo>>(2);
                if (patientInfoList.Count > 0)
                {
                    iFrmMITest.LoadPatientInfo(patientInfoList[0]);
                }
                else
                {
                    MessageBoxShowError("未读到病人数据！");
                }
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }


        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewRegister(MI_Register register)
        {
            register.StaffName = LoginUserInfo.EmpName;
            register.RegTime = System.DateTime.Now;
            register.StaffID = LoginUserInfo.EmpId.ToString();
            register.BedNo = "";
            register.ICDCode = "";
            register.DiagnosisName = "";
            register.SocialCreateNum = "";

            dicStr.Clear();
            dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewRegister", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                iFrmMITest.LoadRegisterInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_Register(int registerId, string serialNO)
        {
            dicStr.Clear();
            dicStr.Add(InputType.RegisterId, registerId);
            dicStr.Add(InputType.SerialNO, serialNO);

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Register", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                iFrmMITest.LoadTradeInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }

        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_CancelRegister(string serialNO)
        {
            dicStr.Clear();
            dicStr.Add(InputType.SerialNO, serialNO);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_CancelRegister", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                MessageBoxShowError("退号成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }

        /// <summary>
        /// 调用费用预结算接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewCharge(TradeData tradeData)
        {
            tradeData.WorkID = LoginUserInfo.WorkId;

            dicStr.Clear();
            dicStr.Add(InputType.TradeData, JsonHelper.SerializeObject(tradeData));

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewCharge", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                iFrmMITest.PreviewCharge(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_Charge(int tradeRecordId, string fph)
        {
            dicStr.Clear();
            dicStr.Add(InputType.TradeRecordId, tradeRecordId.ToString());
            dicStr.Add(InputType.InvoiceNo, fph);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Charge", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);

                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(objects[0])[0];
                MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(objects[1])[0];
                //List<MI_MIPayRecordDetail> mIPayRecordDetailList = ConvertExtend.ToList<MI_MIPayRecordDetail>(objects[2]);
                DataTable dtPayrecordDetail = objects[2];
                MZ_MIPrinter(medicalInsurancePayRecord, mIPayRecordHead, dtPayrecordDetail);
                iFrmMITest.LoadTrade(medicalInsurancePayRecord.PersonCount);
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_CancelCharge(string fph)
        {
            dicStr.Clear();
            dicStr.Add(InputType.InvoiceNo, fph);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(input);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_CancelCharge", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            string s = retdataMember.GetData<string>(1);
            if (b)
            {
                MessageBoxShowError("退费成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + s);
            }
        }
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_UploadzyPatFee() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_DownloadzyPatFee() { throw new NotImplementedException(); }

        //获取已结算费用
        [WinformMethod]
        public void MZ_LoadFeeDetailByTicketNum() { throw new NotImplementedException(); }


        /// <summary>
        /// 获取门诊费用
        /// </summary>
        /// <param name="content"></param>
        [WinformMethod]
        public void Mz_GetCardNo(string content)
        {
            int patListID = 0;
            Action<ClientRequestData> requestAction1 = ((ClientRequestData request1) =>
            {
                request1.AddData(OP_Enum.MemberQueryType.门诊就诊号);
                request1.AddData(content);
            });

            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetRegPatListByCardNo", requestAction1);
            List<OP_PatList> patlist = retdata.GetData<List<OP_PatList>>(0);

            if (patlist.Count == 1)
            {
                patListID = patlist[0].PatListID;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patListID);
                    request.AddData(OP_Enum.PresStatus.未收费);
                });
                ServiceResponseData retdataMember = InvokeWcfService("OPProject.Service", "BalanceController", "GetPatPrescription", requestAction);
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

                iFrmMITest.LoadFee(dtMemberInfo);

            }
        }
        #endregion

        #region 调用Dll
        [WinformMethod]
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public void Mz_GetCardInfoDll(string sCardNo)
        {
            ResultClass resultClass = MIInterFace.Mz_GetCardInfo(sCardNo);

            if (resultClass.bSucess)
            {
                DataTable dtMemberInfo = (DataTable)resultClass.oResult;
                iFrmMITest.LoadCatalogInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_GetPersonInfoDll(string sCardNo)
        {
            ResultClass resultClass = MIInterFace.Mz_GetPersonInfo(sCardNo);

            if (resultClass.bSucess)
            {
                List<PatientInfo> patientInfoList = (List<PatientInfo>)resultClass.oResult;
                iFrmMITest.LoadPatientInfo(patientInfoList[0]);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }


        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewRegisterDll(MI_Register register)
        {
            register.StaffName = LoginUserInfo.EmpName;
            register.RegTime = System.DateTime.Now;
            register.StaffID = LoginUserInfo.EmpId.ToString();

            ResultClass resultClass = MIInterFace.MZ_PreviewRegister(register);

            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.LoadRegisterInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_RegisterDll(int registerId, string serialNO)
        {
            ResultClass resultClass = MIInterFace.MZ_Register(registerId, serialNO);

            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.LoadTradeInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_CancelRegisterDll(string serialNO)
        {
            ResultClass resultClass = MIInterFace.Mz_CancelRegister(serialNO);

            if (resultClass.bSucess)
            {
                MessageBoxShowError("退号成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 调用费用预结算接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewChargeDll(TradeData tradeData)
        {
            tradeData.WorkID = LoginUserInfo.WorkId;

            ResultClass resultClass = MIInterFace.MZ_PreviewCharge(tradeData);

            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.PreviewCharge(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_ChargeDll(int tradeRecordId, string fph)
        {
            ResultClass resultClass = MIInterFace.MZ_Charge(tradeRecordId, fph);

            if (resultClass.bSucess)
            {
                decimal objects = Convert.ToDecimal(resultClass.oResult);
                iFrmMITest.LoadTrade(objects);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_CancelChargeDll(string fph)
        {
            ResultClass resultClass = MIInterFace.MZ_CancelCharge(fph);

            if (resultClass.bSucess)
            {
                MessageBoxShowError("退费成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_UploadzyPatFeeDll() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_DownloadzyPatFeeDll() { throw new NotImplementedException(); }

        //获取已结算费用
        [WinformMethod]
        public void MZ_LoadFeeDetailByTicketNumDll() { throw new NotImplementedException(); }

        #endregion

        #region 调用DllNew
        [WinformMethod]
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public void Mz_GetCardInfoDllNew(string sCardNo)
        {
            dicStr.Clear();
            dicStr.Add(InputType.CardNo, sCardNo);

            ResultClass resultClass = MIInterFaceFactory.MZ_GetCardInfo(input);

            if (resultClass.bSucess)
            {
                DataTable dtMemberInfo = (DataTable)resultClass.oResult;
                iFrmMITest.LoadCatalogInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_GetPersonInfoDllNew(string sCardNo)
        {
            dicStr.Clear();
            dicStr.Add(InputType.CardNo, sCardNo);

            ResultClass resultClass = MIInterFaceFactory.MZ_GetPersonInfo(input);

            if (resultClass.bSucess)
            {
                List<PatientInfo> patientInfoList = (List<PatientInfo>)resultClass.oResult;
                iFrmMITest.LoadPatientInfo(patientInfoList[0]);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }


        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewRegisterDllNew(MI_Register register)
        {
            dicStr.Clear();

            register.StaffName = LoginUserInfo.EmpName;
            register.RegTime = System.DateTime.Now;
            register.StaffID = LoginUserInfo.EmpId.ToString();
            register.BedNo = "";
            register.ICDCode = "";
            register.DiagnosisName = "";
            register.SocialCreateNum = "";

            DataTable dt = new DataTable();
            DataColumn dcItemCode = new DataColumn("ItemCode", Type.GetType("System.String"));
            DataColumn dcItemName = new DataColumn("ItemName", Type.GetType("System.String"));
            DataColumn dcPrice = new DataColumn("Price", Type.GetType("System.String"));
            DataColumn dcCount = new DataColumn("Count", Type.GetType("System.String"));
            DataColumn dcFee = new DataColumn("Fee", Type.GetType("System.String"));

            dt.Columns.Add(dcItemCode);
            dt.Columns.Add(dcItemName);
            dt.Columns.Add(dcPrice);
            dt.Columns.Add(dcCount);
            dt.Columns.Add(dcFee);

            DataRow dr = dt.NewRow();
            dr["ItemCode"] = "101020001";
            dr["ItemName"] = "门诊诊疗费";
            dr["Price"] = "3";
            dr["Count"] = "1";
            dr["Fee"] = "3";
            dt.Rows.Add(dr);

            //DataRow dr1 = dt.NewRow();
            //dr1["ItemCode"] = "0101010101";
            //dr1["ItemName"] = "挂号费(普通门诊)";
            //dr1["Price"] = "0.5";
            //dr1["Count"] = "1";
            //dr1["Fee"] = "0.5";
            //dt.Rows.Add(dr1);

            dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
            dicStr.Add(InputType.DataTable, dt);


            ResultClass resultClass = MIInterFaceFactory.MZ_PreviewRegister(input);

            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.LoadRegisterInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("程序异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_RegisterDllNew(int registerId, string serialNO)
        {
            dicStr.Clear();
            dicStr.Add(InputType.RegisterId, registerId);
            //dicStr.Add(InputType.SerialNO, serialNO);
            ResultClass resultClass = MIInterFaceFactory.MZ_Register(input);

            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.LoadTradeInfo(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        [WinformMethod]
        public void MZ_UpdateRegisterDllNew(int registerId, string serialNO)
        {
            dicStr.Clear();
            dicStr.Add(InputType.RegisterId, registerId);
            dicStr.Add(InputType.SerialNO, serialNO);
            ResultClass resultClass = MIInterFaceFactory.MZ_UpdateRegister(input);

            if (resultClass.bSucess)
            {
                MessageBoxShowError("完成！" + resultClass.sRemarks);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }


        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void Mz_CancelRegisterDllNew(string serialNO)
        {
            dicStr.Clear();
            dicStr.Add(InputType.SerialNO, serialNO);

            ResultClass resultClass = MIInterFaceFactory.MZ_CancelRegister(input);

            if (resultClass.bSucess)
            {
                MessageBoxShowError("退号成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 调用费用预结算接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_PreviewChargeDllNew(TradeData tradeData)
        {
            tradeData.WorkID = LoginUserInfo.WorkId;

            dicStr.Clear();
            dicStr.Add(InputType.TradeData, JsonHelper.SerializeObject(tradeData));

            ResultClass resultClass = MIInterFaceFactory.MZ_PreviewCharge(input);
            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                iFrmMITest.PreviewCharge(resultDic);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_ChargeDllNew(int tradeRecordId, string fph)
        {
            dicStr.Clear();
            dicStr.Add(InputType.TradeRecordId, tradeRecordId);
            dicStr.Add(InputType.InvoiceNo, fph);
            ResultClass resultClass = MIInterFaceFactory.MZ_Charge(input);

            if (resultClass.bSucess)
            {
                decimal objects = Convert.ToDecimal(resultClass.oResult);
                iFrmMITest.LoadTrade(objects);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_CancelChargeDllNew(string fph)
        {
            dicStr.Clear();
            dicStr.Add(InputType.InvoiceNo, fph);
            ResultClass resultClass = MIInterFaceFactory.MZ_CancelCharge(input);

            if (resultClass.bSucess)
            {
                MessageBoxShowError("退费成功！");
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_UploadzyPatFeeDllNew() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public void MZ_DownloadzyPatFeeDllNew() { throw new NotImplementedException(); }

        //获取已结算费用
        [WinformMethod]
        public void MZ_LoadFeeDetailByTicketNumDllNew() { throw new NotImplementedException(); }




        #endregion
        private bool MZ_MIPrinter(MI_MedicalInsurancePayRecord medicalInsurancePayRecord, MI_MIPayRecordHead mIPayRecordHead, DataTable dtMIPayRecordDetailList)
        {
            #region 参数字段
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("TradeNO", medicalInsurancePayRecord.TradeNO);
            myDictionary.Add("Id", medicalInsurancePayRecord.ID);
            myDictionary.Add("PatientName", medicalInsurancePayRecord.PatientName);
            myDictionary.Add("ApplyNO", medicalInsurancePayRecord.ApplyNO);
            myDictionary.Add("FeeMIIn", medicalInsurancePayRecord.FeeMIIn);
            myDictionary.Add("FeeFund", medicalInsurancePayRecord.FeeFund);
            //myDictionary.Add("个人负担-Parameter4", medicalInsurancePayRecord);
            myDictionary.Add("FeeCash", medicalInsurancePayRecord.FeeCash);
            myDictionary.Add("PersonCountPay", medicalInsurancePayRecord.PersonCountPay);
            myDictionary.Add("PersonCount", medicalInsurancePayRecord.PersonCount);
            myDictionary.Add("FeeBigPay", medicalInsurancePayRecord.FeeBigPay);
            //myDictionary.Add("StaffName", medicalInsurancePayRecord.);
            myDictionary.Add("FeeAll", medicalInsurancePayRecord.FeeAll);

            myDictionary.Add("medicine", mIPayRecordHead.medicine);
            myDictionary.Add("tmedicine", mIPayRecordHead.tmedicine);
            myDictionary.Add("therb", mIPayRecordHead.therb);
            myDictionary.Add("examine", mIPayRecordHead.examine);
            myDictionary.Add("labexam", mIPayRecordHead.labexam);
            myDictionary.Add("treatment", mIPayRecordHead.treatment);
            myDictionary.Add("operation", mIPayRecordHead.operation);
            myDictionary.Add("material", mIPayRecordHead.material);
            myDictionary.Add("other", mIPayRecordHead.other);
            myDictionary.Add("xray", mIPayRecordHead.xray);
            myDictionary.Add("ultrasonic", mIPayRecordHead.ultrasonic);
            myDictionary.Add("CT", mIPayRecordHead.CT);
            myDictionary.Add("mri", mIPayRecordHead.mri);
            myDictionary.Add("oxygen", mIPayRecordHead.oxygen);
            myDictionary.Add("bloodt", mIPayRecordHead.bloodt);
            myDictionary.Add("orthodontics", mIPayRecordHead.orthodontics);
            myDictionary.Add("prosthesis", mIPayRecordHead.prosthesis);
            myDictionary.Add("forensic_expertise", mIPayRecordHead.forensic_expertise);
            #endregion

            #region 明细表
            DataTable dtPrint = dtMIPayRecordDetailList.Clone();
            foreach (DataColumn dc in dtMIPayRecordDetailList.Columns)
            {
                dtPrint.Columns.Add(dc.ColumnName + "1", dc.DataType);
            }
            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(i % 2))  //偶数行
                {
                    dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
                }
                else
                {
                    int j = i / 2;
                    dtPrint.Rows[j]["ItemName1"] = dtMIPayRecordDetailList.Rows[i]["ItemName"];
                    dtPrint.Rows[j]["Spec1"] = dtMIPayRecordDetailList.Rows[i]["Spec"];
                    dtPrint.Rows[j]["Unit1"] = dtMIPayRecordDetailList.Rows[i]["Unit"];
                    dtPrint.Rows[j]["UnitPrice1"] = dtMIPayRecordDetailList.Rows[i]["UnitPrice"];
                    dtPrint.Rows[j]["Count1"] = dtMIPayRecordDetailList.Rows[i]["Count"];
                    dtPrint.Rows[j]["Fee1"] = dtMIPayRecordDetailList.Rows[i]["Fee"];
                    dtPrint.Rows[j]["YBItemLevel1"] = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"];
                }
            }
            #endregion

            EfwControls.CustomControl.ReportTool.GetReport("医保收费.grf", 0, myDictionary, dtPrint).Print(true);
            return true;
        }
        //清空测试业务数据
        [WinformMethod]
        public void MZ_ClearDataDllNew()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "MZ_ClearData");
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                MessageBox.Show("清除成功！");
            }
            else
            {
                MessageBox.Show("清除失败！");
            }
        }
    }


}
