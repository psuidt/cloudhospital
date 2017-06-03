using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资字典控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialDic")]//与系统菜单对应                       
    [WinformView(Name = "FrmMaterialDic", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialDic")]
    [WinformView(Name = "FrmHisMaterialDic", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialDic")]
    public class MaterialDicController : WcfClientController
    {
        /// <summary>
        /// 物资字典接口
        /// </summary>
        IFrmMaterialDic frmAccount;

        /// <summary>
        /// 物资字典接口
        /// </summary>
        IFrmMaterialDic frmHisMaterialDic;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmMaterialDic)iBaseView["FrmMaterialDic"];
            frmHisMaterialDic = (IFrmMaterialDic)iBaseView["FrmHisMaterialDic"];
        }

        /// <summary>
        /// 获取物资类型节点
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="frmName">界面入口</param>
        [WinformMethod]
        public void GetMaterialType(int type, string frmName)
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                Dictionary<string, string> Query = new Dictionary<string, string>();
                request.AddData(type);
                request.AddData(Query);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetMaterialType", requestAction);
            DataTable dtTable = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.LoadMaterialType(dtTable);
            }
            else
            {
                frmHisMaterialDic.LoadMaterialType(dtTable);
            }
        }

        /// <summary>
        /// 获取物资类型ShowCard
        /// </summary>
        /// <param name="frmName">界面入口</param>
        [WinformMethod]
        public void GetMaterialTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetMaterialTypeDic");
            DataTable dtTable = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.BindTypeCombox(dtTable);
            }
            else
            {
                frmHisMaterialDic.BindTypeCombox(dtTable);
            }
        }

        /// <summary>
        /// 获取物资参数
        /// </summary>
        [WinformMethod]
        public void GetDeptParameters()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(0);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "GetPublicParameters", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmHisMaterialDic.BindDeptParameters(dt);
        }

        /// <summary>
        /// 审核物资字典
        /// </summary>
        /// <param name="meterialID">物资ID</param>
        /// <param name="frmName">调用的界面</param>
        [WinformMethod]
        public void AuditDic(int meterialID, string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(meterialID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "AuditDic", requestAction);
            int result = retdata.GetData<int>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.CompleteAudit(result);
            }
            else
            {
                frmHisMaterialDic.CompleteAudit(result);
            }
        }

        /// <summary>
        /// 获取大项目类型
        /// </summary>
        /// <param name="frmName">调用的界面</param>
        [WinformMethod]
        public void GetStatItem(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetStatItem");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.LoadStat(dt);
            }
            else
            {
                frmHisMaterialDic.LoadStat(dt);
            }
        }

        /// <summary>
        /// 保存物资字典数据
        /// </summary>
        /// <param name="frmName">调用界面</param>
        [WinformMethod]
        public void SaveMaterialDic(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmMaterialDic")
                {
                    request.AddData(frmAccount.CurrentData);
                }
                else
                {
                    frmHisMaterialDic.CurrentData.CreateWorkID = LoginUserInfo.WorkId;
                    request.AddData(frmHisMaterialDic.CurrentData);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "SaveMaterialDic", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.CompleteSave(result);
            }
            else
            {
                frmHisMaterialDic.CompleteSave(result);
            }
        }

        /// <summary>
        /// 获取物资字典数据
        /// </summary>
        /// <param name="frmName">调用界面</param>
        /// <param name="level">级别</param>
        [WinformMethod]
        public void GetMaterialDic(string frmName, int level)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (frmName == "FrmMaterialDic")
                {
                    request.AddData(frmAccount.GetQuery(string.Empty));
                }
                else
                {
                    request.AddData(frmHisMaterialDic.GetQuery(LoginUserInfo.WorkId.ToString()));
                }

                request.AddData(level);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetMaterialDic", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.BInddgMeter(dt);
            }
            else
            {
                frmHisMaterialDic.BInddgMeter(dt);
            }
        }

        /// <summary>
        /// 查看本院字典
        /// </summary>
        /// <param name="frmName">调用界面</param>
        [WinformMethod]
        public void ChangeView(string frmName)
        {
            if (frmName == "FrmMaterialDic")
            {
                frmAccount = DefaultView as IFrmMaterialDic;
            }
            else if (frmName == "FrmHisMaterialDic")
            {
                //本院只能查本院的数据
                frmAccount = iBaseView["FrmHisDrugDic"] as IFrmMaterialDic;
            }
        }

        /// <summary>
        /// 获取单位绑定数据
        /// </summary>
        /// <param name="frmName">调用界面</param>
        [WinformMethod]
        public void GetUnit(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetUnit");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmMaterialDic")
            {
                frmAccount.LoadUnit(dt);
            }
            else
            {
                frmHisMaterialDic.LoadUnit(dt);
            }
        }

        /// <summary>
        /// 获取厂家绑定数据
        /// </summary>
        [WinformMethod]
        public void GetHisDic()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "GetHisDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmHisMaterialDic.LoadProduct(dt);
            DataTable tableMedicare = retdata.GetData<DataTable>(1);
            frmHisMaterialDic.LoadMedicare(tableMedicare);
        }

        /// <summary>
        /// 根据字典获取厂家信息
        /// </summary>
        /// <param name="centerMatID">中心ID</param>
        [WinformMethod]
        public void LoadHisDic(int centerMatID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(centerMatID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "LoadHisDic", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmHisMaterialDic.LoadHisDic(dt);
        }

        /// <summary>
        /// 保存本院典
        /// </summary>
        [WinformMethod]
        public void SaveHisDic()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmHisMaterialDic.CurrentHospDic);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDicController", "SaveHisDic", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (!flag)
            {
                MessageBoxShowError(retdata.GetData<string>(1));
            }
            else
            {
                MessageBoxShowSimple("成功");
                frmHisMaterialDic.SaveHospSuccess();
                LoadHisDic(frmHisMaterialDic.CurrentData.CenterMatID);
            }
        }
    }
}