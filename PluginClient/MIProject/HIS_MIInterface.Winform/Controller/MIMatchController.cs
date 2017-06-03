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

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMIMatch")]//与系统菜单对应
    [WinformView(Name = "FrmMIMatch", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmMIMatch")]
    public class MIMatchController : WcfClientController
    {
        IFrmMIMatch iFrmMIMatch; //测试界面
        private Dictionary<string, string> columnNames = new Dictionary<string, string>();
        public override void Init()
        {
            iFrmMIMatch = (IFrmMIMatch)iBaseView["FrmMIMatch"];
        }
        /// <summary>
        /// 获取HIS的目录
        /// </summary>
        /// <param name="catalogType">目录类型1西药 2项目</param>
        /// <param name="stopFlag">停用标志 1过滤停用，0全部</param>
        /// <param name="matchFlag">匹配标志 1未匹配 0全部</param>
        /// <param name="ybID">医保类型ID</param>
        [WinformMethod]
        public void M_GetHISCatalogInfo(int catalogType, int stopFlag, int matchFlag, int ybID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(catalogType);
                request.AddData(stopFlag);
                request.AddData(matchFlag);
                request.AddData(ybID);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetHISCatalogInfo", requestAction);
            //ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetHISCatalogInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIMatch.LoadHISCatalogInfo(dtMemberInfo);
        }
        /// <summary>
        /// 获取医保目录
        /// </summary>
        /// <param name="catalogType">目录类型1西药 2项目</param>
        /// <param name="ybId">医保类型ID</param>
        [WinformMethod]
        public void M_GetMICatalogInfo(int catalogType, int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(catalogType);
                request.AddData(ybId);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetMICatalogInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIMatch.LoadMICatalogInfo(dtMemberInfo);
        }
        /// <summary>
        /// 获取匹配目录
        /// </summary>
        /// <param name="catalogType">目录类型1西药 2项目</param>
        /// <param name="ybId">医保类型ID</param>
        /// <param name="auditFlag">审核标志-1 全部 0 已匹配 1 已审核 2 未通过</param>
        [WinformMethod]
        public void M_GetMatchCatalogInfo(int catalogType, int ybId, int auditFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(catalogType);
                request.AddData(ybId);
                request.AddData(auditFlag);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetMatchCatalogInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIMatch.LoadMatchCatalogInfo(dtMemberInfo);
        }
        /// <summary>
        /// 获取医保类型
        /// </summary>
        [WinformMethod]
        public void M_GetMIType()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetMIType");
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMIMatch.LoadMIType(dtMemberInfo);
        }
        /// <summary>
        /// 导入医保目录
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_ImportMILog(int ybId)
        {
            string sType = M_GetMedicalInsuranceData(ybId, "ImportMILog", "filetype");
            string sColumnName = M_GetMedicalInsuranceData(ybId, "ImportMILog", "columnNames");
            string sSeparator = M_GetMedicalInsuranceData(ybId, "ImportMILog", "separator");

            if (sType.Trim() == "1")
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "请选择要导入的Excel文件";
                open.Filter = "xls files (*.xls)|*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] sColumnNames = sColumnName.Split('|');
                    columnNames.Clear();
                    foreach (string s in sColumnNames)
                    {
                        columnNames.Add(s.Split(',')[1], s.Split(',')[0]);
                    }

                    string fileName = open.FileName;
                    DataTable dtMemberInfo = ExcelHelper.Import(fileName, columnNames);

                    iFrmMIMatch.LoadMICatalogInfo(dtMemberInfo);
                }
            }
        }
        /// <summary>
        /// 导出HIS目录
        /// </summary>
        /// <param name="ybId"></param>
        /// <param name="dtList"></param>
        /// <param name="iType"></param>
        [WinformMethod]
        public void M_ExportHisLog(int ybId, List<DataTable> dtList, int iType)
        {
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    MessageBox.Show("数据源为空！");
            //    return;
            //}

            string typeName = "";
            string sColumnName = "";
            try
            {
                string sType = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "filetype");
                switch (iType)
                {
                    case 1:
                        typeName = "西药";
                        sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "YpColumnNames");
                        break;
                    case 2:
                        typeName = "材料";
                        sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "ClColumnNames");
                        break;
                    case 3:
                        typeName = "项目";
                        sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "XmColumnNames");
                        break;
                    case 4:
                        typeName = "中草药";
                        sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "ZCYColumnNames");
                        break;
                    default:
                        typeName = "西药";
                        sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "YpColumnNames");
                        break;

                }
                string sSeparator = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "separator");

                List<Dictionary<string, string>> columnNamesList = new List<Dictionary<string, string>>();

                string[] sColumnNames = sColumnName.Split('^');
                for (int i = 0; i < sColumnNames.Length; i++)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    string[] sColumnNamess = sColumnNames[i].Split('|');

                    foreach (string s in sColumnNamess)
                    {
                        dss.Add(s.Split(',')[0], s.Split(',')[1]);
                    }
                    columnNamesList.Add(dss);
                }

                ExportFile doExport = new ExportFile("HIS" + typeName + "目录", columnNamesList, sSeparator);
                ExportType _exportType = sType.Trim() == "1" ? ExportType.Excel : ExportType.Txt;
                if (doExport.InitShowDialog(_exportType))
                {
                    if (doExport.DoExportWork(dtList))
                        doExport.OpenFile();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("导出失败！:" + e.Message);
                return;
            }



            //try
            //{
            //    string sType = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "filetype");
            //    string sColumnName = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "columnNames");
            //    string sSeparator = M_GetMedicalInsuranceData(ybId, "ExportHisLog", "separator");

            //    string[] sColumnNames = sColumnName.Split('|');
            //    columnNames.Clear();
            //    if (sType.Trim() == "1")
            //    {
            //        foreach (string s in sColumnNames)
            //        {
            //            columnNames.Add(s.Split(',')[0], s.Split(',')[1]);
            //        }

            //        ExcelHelper.Export(dt, "HIS目录", columnNames, @"E:\\医院" + typeName + "目录.xls");
            //    }
            //    else
            //    {

            //    }
            //    MessageBox.Show("导出完成！");
            //}
            //catch
            //{
            //    MessageBox.Show("导出失败！");
            //    return;
            //}

        }

        /// <summary>
        /// 导入审核结果
        /// </summary>
        /// <param name="ybId">医保类型</param>
        /// <param name="iTypeId">目录类型 0 西药 1项目材料 -1 全部</param>
        [WinformMethod]
        public void M_ImportMatchLog(int ybId,int iTypeId)
        {
            string sType = M_GetMedicalInsuranceData(ybId, "ImportMatchLog", "filetype");
            string sSeparator = M_GetMedicalInsuranceData(ybId, "ImportMatchLog", "separator");

            if (sType.Trim() == "1")
            {
                string sColumnName = M_GetMedicalInsuranceData(ybId, "ImportMatchLog", "columnNames");


                OpenFileDialog open = new OpenFileDialog();
                open.Title = "请选择要导入的Excel文件";
                open.Filter = "xls files (*.xls)|*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] sColumnNames = sColumnName.Split('|');
                    columnNames.Clear();
                    foreach (string s in sColumnNames)
                    {
                        columnNames.Add(s.Split(',')[0], s.Split(',')[1]);
                    }

                    string fileName = open.FileName;

                    DataTable dtMemberInfo = ExcelHelper.Import(fileName, columnNames);
                    if (dtMemberInfo.Columns.Contains("ItemType"))
                    {
                        foreach (DataRow dr in dtMemberInfo.Rows)
                        {
                            dr["ItemType"] = iTypeId;
                        }
                    }
                    iFrmMIMatch.LoadMatchCatalogInfo(dtMemberInfo);
                }
            }
            else
            {
                string sCName = "columnNamesYP";
                if (iTypeId == 0)
                {
                    sCName = "columnNamesYP";
                }
                else
                {
                    sCName = "columnNamesSFXM";
                }
                string sColumnName = M_GetMedicalInsuranceData(ybId, "ImportMatchLog", sCName);
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "请选择要导入的Text|Out文件";
                open.Filter = "(*.out)|*.out|(*.txt)|*.txt";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] sColumnNames = sColumnName.Split('|');
                    columnNames.Clear();
                    foreach (string s in sColumnNames)
                    {
                        columnNames.Add(s.Split(',')[0], s.Split(',')[1]);
                    }

                    string fileName = open.FileName;

                    DataTable dtMemberInfo = ExcelHelper.ImportText(fileName, columnNames);
                    if (dtMemberInfo.Columns.Contains("ItemType"))
                    {
                        foreach (DataRow dr in dtMemberInfo.Rows)
                        {
                            dr["ItemType"] = iTypeId;
                        }
                    }
                    iFrmMIMatch.LoadMatchCatalogInfo(dtMemberInfo);
                }
            }
        }
        /// <summary>
        /// 保存医保目录数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_SaveMILog(DataTable dt, int ybId)
        {
            //M_DeleteMatchLogs
            Action<ClientRequestData> requestActiond = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdata = InvokeWcfService("MIProject.Service", "MIMatchController", "M_DeleteMILog", requestActiond);
            bool dflag = retdata.GetData<bool>(0);
            if (!dflag)
            {
                MessageBoxShowSimple("删除失败！");
            }
            else
            {
                int iCount = 1000;
                int iRowCount = dt.Rows.Count;
                int iRound = iRowCount % iCount == 0 ? iRowCount / iCount : iRowCount / iCount + 1;//最终DataTable个数

                List<DataTable> list = new List<DataTable>();
                for (int i = 0; i < iRound; i++)
                {
                    DataTable dtCopy = dt.AsEnumerable().Skip(i * iCount).Take(iCount).CopyToDataTable();
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(dtCopy);
                        request.AddData(ybId);
                    });
                    ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_SaveMILog", requestAction);
                    bool flag = retdataMember.GetData<bool>(0);
                    if (!flag)
                    {
                        MessageBoxShowError("保存失败！");
                    }
                    else
                    {
                        MessageBoxShowSimple("第" + i * 1000 + "到" + (i * 1000 + dtCopy.Rows.Count).ToString() + " 保存完毕！");
                    }
                }

                iFrmMIMatch.ReFresh();
            }
        }

        /// <summary>
        /// 保存匹配数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_SaveMatchLogs(DataTable dt, int ybId)
        {
            //M_DeleteMatchLogs
            Action<ClientRequestData> requestActiond = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
                request.AddData(dt.Rows[0]["ItemType"].ToString());
            });
            ServiceResponseData retdata = InvokeWcfService("MIProject.Service", "MIMatchController", "M_DeleteMatchLogs", requestActiond);
            bool dflag = retdata.GetData<bool>(0);
            if (!dflag)
            {
                MessageBoxShowSimple("删除失败！");
            }
            else
            {
                int iCount = 1000;
                int iRowCount = dt.Rows.Count;
                int iRound = iRowCount % iCount == 0 ? iRowCount / iCount : iRowCount / iCount + 1;//最终DataTable个数

                List<DataTable> list = new List<DataTable>();
                for (int i = 0; i < iRound; i++)
                {
                    DataTable dtCopy = dt.AsEnumerable().Skip(i * iCount).Take(iCount).CopyToDataTable();
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(dtCopy);
                        request.AddData(ybId);
                    });
                    ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_SaveMatchLogs", requestAction);
                    bool flag = retdataMember.GetData<bool>(0);
                    if (!flag)
                    {
                        MessageBoxShowError("保存失败！");
                    }
                    else
                    {
                        MessageBoxShowSimple("第"+i*1000+"到"+ (i * 1000+dtCopy.Rows.Count).ToString()+" 保存完毕！");
                    }
                }

                iFrmMIMatch.ReFresh();
            }
        }
        /// <summary>
        /// 更新匹配目录-单条重置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditFlag"></param>
        [WinformMethod]
        public void M_UpdateMatchLogs(string id, string auditFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
                request.AddData(auditFlag);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_UpdateMatchLogs", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("操作成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("操作失败！");
            }
        }
        /// <summary>
        /// 更新匹配目录-全部重置
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_UpdateAllMatchLogs(int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_UpdateAllMatchLogs", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("操作成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("操作失败！");
            }
        }

        /// <summary>
        /// 更新医保目录
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_UpdateMIlog(int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_UpdateMIlog", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("更新成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("更新失败！");
            }
        }
        /// <summary>
        /// 获取配置文件数据
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="method"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string M_GetMedicalInsuranceData(int typeId, string method, string name)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
                request.AddData(method);
                request.AddData(name);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetMedicalInsuranceData", requestAction);
            string sMemberInfo = retdataMember.GetData<string>(0);
            return sMemberInfo;
        }


        /// <summary>
        /// 更新医保目录
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_UpdateDrugLogLevel(int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_UpdateDrugLogLevel", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("药品更新成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("药品更新失败！");
            }
        }


        /// <summary>
        /// 更新医保目录
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_UpdateFeeItemLogLevel(int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_UpdateFeeItemLogLevel", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("项目更新成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("项目更新失败！");
            }
        }


        /// <summary>
        /// 更新医保目录
        /// </summary>
        /// <param name="ybId"></param>
        [WinformMethod]
        public void M_UpdateMWLogLevel(int ybId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ybId);
            });
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_UpdateMWLogLevel", requestAction);
            bool flag = retdataMember.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("材料更新成功！");
                iFrmMIMatch.ReFresh();
            }
            else
            {
                MessageBoxShowSimple("材料更新失败！");
            }
        }

    }
}
