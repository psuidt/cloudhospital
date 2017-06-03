using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品字典控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDrugDic")]//与系统菜单对应
    [WinformView(Name = "FrmDrugDic", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.DrugDic.FrmDrugDic")]
    [WinformView(Name = "FrmHisDrugDic", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.DrugDic.FrmDrugDic")]
    public class DrugDicController : WcfClientController
    {
        /// <summary>
        /// 中心字典对象
        /// </summary>
        private IFrmDrugDic frmDrugDic;

        /// <summary>
        /// 本院字典对象
        /// </summary>
        private IFrmDrugDic frmHisDrugDic;

        /// <summary>
        /// 药剂科室参数
        /// </summary>
        private DataTable dtPharm;

        /// <summary>
        /// 药品类型数据源
        /// </summary>
        private DataTable dtDrugType;

        /// <summary>
        ///  药品类型ShowCard
        /// </summary>
        private DataTable dtDrugTypeForTb;

        /// <summary>
        /// 药品子类型数据源
        /// </summary>
        private DataTable dtDrugCType;

        /// <summary>
        ///  统计大项目
        /// </summary>
        private DataTable dtState;

        /// <summary>
        /// 单位数据源
        /// </summary>
        private DataTable dtUnit;

        /// <summary>
        /// 抗生素数据源
        /// </summary>
        private DataTable dtAnt;

        /// <summary>
        /// 剂型数据源
        /// </summary>
        private DataTable dtDosage;

        /// <summary>
        /// 生厂厂家数据源
        /// </summary>
        private DataTable dtProduct;

        /// <summary>
        /// 医保类型数据源
        /// </summary>
        private DataTable dtMediCare;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmDrugDic = (IFrmDrugDic)DefaultView;
            frmHisDrugDic = (IFrmDrugDic)iBaseView["FrmHisDrugDic"];
        }

        /// <summary>
        /// 异步加载函数
        /// </summary>
        public override void AsynInit()
        {
            #region 树

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "GetAllCenterTable");
            dtPharm = retdata.GetData<DataTable>(0);
            dtDrugType = retdata.GetData<DataTable>(1);
            #endregion

            #region Card控件

            //药品类型
            dtDrugTypeForTb = retdata.GetData<DataTable>(2);

            //药品子类型
            dtDrugCType = retdata.GetData<DataTable>(3);

            //统计大项目
            dtState = retdata.GetData<DataTable>(4);

            //通用单位
            dtUnit = retdata.GetData<DataTable>(5);

            //抗生素
            dtAnt = retdata.GetData<DataTable>(6);

            // 药剂
            dtDosage = retdata.GetData<DataTable>(7);
            #endregion

            #region 厂家典
            ServiceResponseData requestHis = InvokeWcfService("DrugProject.Service", "DrugDicController", "GetAllHisDic");
            dtProduct = requestHis.GetData<DataTable>(0);
            dtMediCare = requestHis.GetData<DataTable>(1);
            #endregion
        }

        /// <summary>
        /// 异步加载数据函数
        /// </summary>
        public override void AsynInitCompleted()
        {
            frmDrugDic.LoadDrugType(dtDrugType);

            frmHisDrugDic.LoadPharms(dtPharm);
            frmHisDrugDic.LoadDrugType(dtDrugType);
            frmHisDrugDic.LoadDrugTypeForTb(dtDrugTypeForTb);

            frmHisDrugDic.LoadDrugCType(dtDrugCType);
            frmHisDrugDic.LoadStat(dtState);
            frmHisDrugDic.LoadCommUnit(dtUnit);

            frmHisDrugDic.LoadAnt(dtAnt);
            frmHisDrugDic.LoadDosage(dtDosage);
            frmHisDrugDic.LoadProduct(dtProduct);
            frmHisDrugDic.LoadMedicare(dtMediCare);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void InitData(string frmName)
        {
            if (frmName == "FrmDrugDic")
            {
                frmDrugDic.LoadPharms(dtPharm);
                frmDrugDic.LoadDrugTypeForTb(dtDrugTypeForTb);
                frmDrugDic.LoadDrugCType(dtDrugCType);
                frmDrugDic.LoadStat(dtState);
                frmDrugDic.LoadCommUnit(dtUnit);
                frmDrugDic.LoadAnt(dtAnt);
                frmDrugDic.LoadDosage(dtDosage);
            }
        }

        /// <summary>
        /// 切换界面
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeView(string frmName)
        {
            if (frmName == "FrmDrugDic")
            {
                frmDrugDic = DefaultView as IFrmDrugDic;
            }
            else if (frmName == "FrmHisDrugDic")
            {
                frmDrugDic = iBaseView["FrmHisDrugDic"] as IFrmDrugDic;
            }
        }

        /// <summary>
        /// 获取药品字典数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDurgDic(string frmName)
        {
            if (frmName == "FrmDrugDic")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(frmDrugDic.AndWhere);
                    request.AddData(frmDrugDic.OrWhere);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "GetDrugDic", requestAction);
                var dt = retdata.GetData<DataTable>(0);
                frmDrugDic.LoadDrugDic(dt);
            }
            else if (frmName == "FrmHisDrugDic")
            {
                if (frmDrugDic.AndWhere != null)
                {
                    var x = frmDrugDic.AndWhere.Find(i => i.Item1 == "CreateWorkID");
                    if (x != null)
                    {
                        frmDrugDic.AndWhere.Remove(x);
                        frmDrugDic.AndWhere.Add(Tuple.Create("CreateWorkID", LoginUserInfo.WorkId.ToString(), SqlOperator.Equal));
                    }
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(frmDrugDic.AndWhere);
                    request.AddData(frmDrugDic.OrWhere);
                    request.AddData(LoginUserInfo.WorkId.ToString());
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "GetHospDrugDic", requestAction);
                var dt = retdata.GetData<DataTable>(0);
                frmDrugDic.LoadDrugDic(dt);
            }
        }

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        [WinformMethod]
        public void GetDeptParameters()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(0);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "GetDeptParameters", requestAction);

            DataTable dt = retdata.GetData<DataTable>(0);
            frmDrugDic.BindDeptParameters(dt);
        }

        /// <summary>
        /// 保存药品字典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="isNew">是否新增</param>
        [WinformMethod]
        public void SaveDrugDic(string frmName, bool isNew)
        {
            if (frmName == "FrmHisDrugDic")
            {
                frmDrugDic.CurrentData.CreateWorkID = LoginUserInfo.WorkId;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugDic.CurrentData);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "SaveDrugDic", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);

            if (result.Result == 0)
            {
                MessageBoxShowSimple("药品属性 保存成功");
                frmDrugDic.CurrentData.CenteDrugID = EfwControls.Common.Tools.ToInt32(result.ErrMsg);
                frmDrugDic.SaveSuccess(isNew);
                if (!isNew)
                {
                    GetDurgDic(frmName);
                }
            }
            else
            {
                MessageBoxShowSimple("保存失败:" + result.ErrMsg);
            }
        }

        /// <summary>
        /// 审核数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void AuditDurgDic(string frmName)
        {
            var dic = frmDrugDic.CurrentData;
            dic.Auditor = LoginUserInfo.EmpId;
            dic.AuditStatus = 1;
            dic.AuditTime = DateTime.Now;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dic);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "SaveDrugDic", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result != 0)
            {
                MessageBoxShowError(result.ErrMsg);
            }
            else
            {
                MessageBoxShowSimple("审核成功");
                GetDurgDic(frmName);
            }
        }

        /// <summary>
        /// 本院典列表数据
        /// </summary>
        [WinformMethod]
        public void LoadHisDic()
        {
            if (frmDrugDic.HospAndWhere != null)
            {
                frmDrugDic.HospAndWhere.Add(Tuple.Create("d.WorkId", LoginUserInfo.WorkId.ToString(), SqlOperator.Equal));
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugDic.HospAndWhere);
            });
            ServiceResponseData retdataD = InvokeWcfService("DrugProject.Service", "DrugDicController", "LoadHisDic", requestAction);
            var dt = retdataD.GetData<DataTable>(0);
            frmDrugDic.LoadHisDic(dt);
        }

        /// <summary>
        /// 保存本院典
        /// </summary>
        [WinformMethod]
        public void SaveHisDic()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugDic.CurrentHospDic);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDicController", "SaveHisDic", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (!flag)
            {
                MessageBoxShowError(retdata.GetData<string>(1));
            }
            else
            {
                MessageBoxShowSimple("成功");
                frmDrugDic.SaveHospSuccess();
                LoadHisDic();
            }
        }

        /// <summary>
        /// 获取子药品类型
        /// </summary>
        [WinformMethod]
        public void GetChildDrugType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugDic.DrugType);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "GetChildDrugType", requestAction);
            dtDrugCType = retdata.GetData<DataTable>(0);

            frmDrugDic.LoadDrugCType(dtDrugCType);
            frmHisDrugDic.LoadDrugCType(dtDrugCType);
        }
    }
}
