using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmOrderPrint")]//与系统菜单对应
    [WinformView(Name = "FrmOrderPrint", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmOrderPrint")]
    //血糖数据查询
    [WinformView(Name = "FrmBloodGlucose", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmBloodGlucose")]

    /// <summary>
    /// 医嘱打印,血糖查看界面控制器
    /// </summary>
    public class OrderPrintController : WcfClientController
    {
        /// <summary>
        /// 医嘱打印界面接口
        /// </summary>
        IFrmOrderPrint ifrmOrderPrint;

        /// <summary>
        /// 血糖数据查看界面接口
        /// </summary>
        IFrmBloodGlucose ifrmbloodGlucose;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmOrderPrint = (IFrmOrderPrint)iBaseView["FrmOrderPrint"];
            ifrmbloodGlucose = (IFrmBloodGlucose)iBaseView["FrmBloodGlucose"];
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="isOut">是否出院false未出院true出院</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetPatInfoList(DateTime bdate, DateTime edate, bool isOut, int deptid, string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(deptid);
                request.AddData(bdate);
                request.AddData(edate);
                request.AddData(isOut);
                request.AddData(false);
                request.AddData(string.Empty);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfo", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            if (frmName == "FrmOrderPrint")
            {
                ifrmOrderPrint.BindPatInfo(dtPatInfo);
            }
            else if (frmName == "FrmBloodGlucose")
            {
                ifrmbloodGlucose.BindPatInfo(dtPatInfo);
            }
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptList(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "GetDeptList", requestAction);
            DataTable dtDept = retdata.GetData<DataTable>(0);
            if (frmName == "FrmOrderPrint")
            {
                ifrmOrderPrint.BindDept(dtDept);
            }
            else if (frmName == "FrmBloodGlucose")
            {
                ifrmbloodGlucose.BindDept(dtDept);
            }
        }
        #region 查看医嘱      

        /// <summary>
        /// 查看病人医嘱信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        [WinformMethod]
        public void GetPatOrders(int patlistid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(3);
                request.AddData(patlistid);
                request.AddData(10);
                request.AddData(ifrmOrderPrint.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetOrders", requestAction);
            DataTable orderDetails = retdata.GetData<DataTable>(0);
            // 过滤数据
            DataTable longDetails = orderDetails.Clone();
            DataTable tempDetails = orderDetails.Clone();
            orderDetails.TableName = "OrderDetails";
            // 过滤长期医嘱明细数据
            DataView longView = new DataView(orderDetails);
            string longSqlWhere = " OrderCategory = 0";
            longView.RowFilter = longSqlWhere;
            longDetails.Merge(longView.ToTable());
            // 过滤临时医嘱明细数据
            DataView tempView = new DataView(orderDetails);
            string tempSqlWhere = " OrderCategory = 1";
            tempView.RowFilter = tempSqlWhere;
            tempDetails.Merge(tempView.ToTable());
            for (int i = 0; i < longDetails.Rows.Count; i++)
            {
                longDetails.Rows[i]["BDate"] = Convert.ToDateTime(longDetails.Rows[i]["OrderBdate"]).ToString("MM-dd");
                longDetails.Rows[i]["BTime"] = Convert.ToDateTime(longDetails.Rows[i]["OrderBdate"]).ToString("HH:mm");
                if (Convert.ToInt32(longDetails.Rows[i]["OrderStatus"]) >= 3)
                {
                    longDetails.Rows[i]["EDate"] = Convert.ToDateTime(longDetails.Rows[i]["EOrderDate"]).ToString("MM-dd");
                    longDetails.Rows[i]["ETime"] = Convert.ToDateTime(longDetails.Rows[i]["EOrderDate"]).ToString("HH:mm");
                }
                if (Convert.ToInt32(longDetails.Rows[i]["ItemType"]) != 5 && Convert.ToInt32(longDetails.Rows[i]["sumfee"])==0)
                {
                    longDetails.Rows[i]["ItemName"] += "(" + "dc" + ")";
                }
                longDetails.Rows[i]["DosageUnit"] = longDetails.Rows[i]["Dosage"] + longDetails.Rows[i]["DosageUnit"].ToString();
                int ordertype = Convert.ToInt32(longDetails.Rows[i]["OrderType"]);
                string memo = ordertype == 1 ? "交病人" : ordertype == 2 ? "自备" :"出院带药";
                if (ordertype==1||ordertype==2||ordertype==3)
                {
                    memo= '【' + memo + '】';
                    longDetails.Rows[i]["ItemName"] = longDetails.Rows[i]["ItemName"].ToString().Trim() + memo;
                    longDetails.Rows[i]["ItemName"] = longDetails.Rows[i]["ItemName"].ToString() + longDetails.Rows[i]["Amount"].ToString() + longDetails.Rows[i]["Unit"];
                }
                
            }

            for (int i = 0; i < tempDetails.Rows.Count; i++)
            {
                tempDetails.Rows[i]["BDate"] = Convert.ToDateTime(tempDetails.Rows[i]["OrderBdate"]).ToString("MM-dd");
                tempDetails.Rows[i]["BTime"] = Convert.ToDateTime(tempDetails.Rows[i]["OrderBdate"]).ToString("HH:mm");
                tempDetails.Rows[i]["DosageUnit"] = tempDetails.Rows[i]["Dosage"] + tempDetails.Rows[i]["DosageUnit"].ToString();
                if (Convert.ToInt32(tempDetails.Rows[i]["ItemType"]) != 5 && Convert.ToInt32(tempDetails.Rows[i]["sumfee"]) == 0)
                {
                    tempDetails.Rows[i]["ItemName"] += "(" + "dc" + ")";
                }
                int ordertype = Convert.ToInt32(tempDetails.Rows[i]["OrderType"]);
                string memo = ordertype == 1 ? "交病人" : ordertype == 2 ? "自备" : "出院带药";
                if (ordertype == 1 || ordertype == 2 || ordertype == 3)
                {
                    memo = '【' + memo + '】';
                    tempDetails.Rows[i]["ItemName"] = tempDetails.Rows[i]["ItemName"].ToString().Trim() +memo;
                    tempDetails.Rows[i]["ItemName"] = tempDetails.Rows[i]["ItemName"].ToString() + tempDetails.Rows[i]["Amount"].ToString() + tempDetails.Rows[i]["Unit"];

                }
            }

            ifrmOrderPrint.BindOrderDetail(GetGroupData(longDetails), GetGroupData(tempDetails));
        }

        /// <summary>
        /// 按条件查询医嘱数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="myDictionary">查询条件</param>
        [WinformMethod]
        public void GetPatOrdersPrint(int patlistid, Dictionary<string, object> myDictionary)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(3);
                request.AddData(patlistid);
                request.AddData(10);
                request.AddData(ifrmOrderPrint.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetOrders", requestAction);
            DataTable orderDetails = retdata.GetData<DataTable>(0);
            // 过滤数据
            DataTable longDetails = orderDetails.Clone();
            DataTable tempDetails = orderDetails.Clone();
            orderDetails.TableName = "OrderDetails";
            // 过滤长期医嘱明细数据
            DataView longView = new DataView(orderDetails);
            string longSqlWhere = " OrderCategory = 0";
            longView.RowFilter = longSqlWhere;
            longDetails.Merge(longView.ToTable());
            // 过滤临时医嘱明细数据
            DataView tempView = new DataView(orderDetails);
            string tempSqlWhere = " OrderCategory = 1";
            tempView.RowFilter = tempSqlWhere;
            tempDetails.Merge(tempView.ToTable());
            for (int i = 0; i < longDetails.Rows.Count; i++)
            {
                longDetails.Rows[i]["BDate"] = Convert.ToDateTime(longDetails.Rows[i]["OrderBdate"]).ToString("MM-dd");
                longDetails.Rows[i]["BTime"] = Convert.ToDateTime(longDetails.Rows[i]["OrderBdate"]).ToString("HH:mm");
                if (Convert.ToInt32(longDetails.Rows[i]["OrderStatus"]) >= 3)
                {
                    longDetails.Rows[i]["EDate"] = Convert.ToDateTime(longDetails.Rows[i]["EOrderDate"]).ToString("MM-dd");
                    longDetails.Rows[i]["ETime"] = Convert.ToDateTime(longDetails.Rows[i]["EOrderDate"]).ToString("HH:mm");
                }
                if (Convert.ToInt32(longDetails.Rows[i]["ItemType"]) != 5 && Convert.ToInt32(longDetails.Rows[i]["sumfee"]) == 0)
                {
                    longDetails.Rows[i]["ItemName"] += "(" + "dc" + ")";
                }
                longDetails.Rows[i]["DosageUnit"] = longDetails.Rows[i]["Dosage"] + longDetails.Rows[i]["DosageUnit"].ToString();
                int ordertype = Convert.ToInt32(longDetails.Rows[i]["OrderType"]);
                string memo = ordertype == 1 ? "交病人" : ordertype == 2 ? "自备" : "出院带药";
                if (ordertype == 1 || ordertype == 2 || ordertype == 3)
                {
                    memo = '【' + memo + '】';
                    longDetails.Rows[i]["ItemName"] = longDetails.Rows[i]["ItemName"].ToString().Trim() + memo;
                    longDetails.Rows[i]["ItemName"] = longDetails.Rows[i]["ItemName"].ToString() + longDetails.Rows[i]["Amount"].ToString() + longDetails.Rows[i]["Unit"];
                }
            }

            for (int i = 0; i < tempDetails.Rows.Count; i++)
            {
                tempDetails.Rows[i]["BDate"] = Convert.ToDateTime(tempDetails.Rows[i]["OrderBdate"]).ToString("MM-dd");
                tempDetails.Rows[i]["BTime"] = Convert.ToDateTime(tempDetails.Rows[i]["OrderBdate"]).ToString("HH:mm");
                tempDetails.Rows[i]["DosageUnit"] = tempDetails.Rows[i]["Dosage"] + tempDetails.Rows[i]["DosageUnit"].ToString();
                if (Convert.ToInt32(tempDetails.Rows[i]["ItemType"]) != 5 && Convert.ToInt32(tempDetails.Rows[i]["sumfee"]) == 0)
                {
                    tempDetails.Rows[i]["ItemName"] += "(" + "dc" + ")";
                }
                int ordertype = Convert.ToInt32(tempDetails.Rows[i]["OrderType"]);
                string memo = ordertype == 1 ? "交病人" : ordertype == 2 ? "自备" : "出院带药";
                if (ordertype == 1 || ordertype == 2 || ordertype == 3)
                {
                    memo = '【' + memo + '】';
                    tempDetails.Rows[i]["ItemName"] = tempDetails.Rows[i]["ItemName"].ToString().Trim() + memo;
                    tempDetails.Rows[i]["ItemName"] = tempDetails.Rows[i]["ItemName"].ToString() + tempDetails.Rows[i]["Amount"].ToString() + tempDetails.Rows[i]["Unit"];

                }
            }

            ifrmOrderPrint.PrintAll(myDictionary, GetGroupData(longDetails), GetGroupData(tempDetails));
        }

        /// <summary>
        /// 打印画分组线
        /// </summary>
        /// <param name="presData">数据</param>
        /// <returns>加分组线的数据</returns>
        private DataTable GetGroupData(DataTable presData)
        {
            int currentgroupid = 0;
            int ordeyno = 0;
            presData.Columns.Add("GroupLineUp");
            presData.Columns.Add("GroupLineDown");
            for (int index = 0; index < presData.Rows.Count; index++)
            {
                int groupid = Convert.ToInt32(presData.Rows[index]["GroupID"]);
                int groupcount = presData.Select("GroupID=" + groupid).Count();
                if (currentgroupid != groupid)
                {
                    if (groupcount > 1)
                    {
                        currentgroupid = groupid;
                        ordeyno = 1;
                        presData.Rows[index]["GroupLineUp"] = "┍";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = string.Empty;
                        presData.Rows[index]["GroupLineDown"] = string.Empty;
                    }
                }
                else
                {
                    if (ordeyno == groupcount)
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "┕";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                }

                ordeyno++;
            }

            return presData;
        }

        #endregion

        #region 查看血糖数据
        /// <summary>
        /// 获取血糖数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        [WinformMethod]
        public void GetBloodGluRecord(int patlistid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetBloodGluRecord", requestAction);
            DataTable dtBloodGluRecords = retdata.GetData<DataTable>(0);
            ifrmbloodGlucose.BindBloodGluRecord(dtBloodGluRecords);
        }
        #endregion
       }
    }
