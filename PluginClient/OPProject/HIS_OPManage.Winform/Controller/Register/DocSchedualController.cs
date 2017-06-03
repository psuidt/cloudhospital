using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmDocSchedual")]//与系统菜单对应
    [WinformView(Name = "FrmDocSchedual", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmDocSchedual")]
    //排班复制
    [WinformView(Name = "FrmCopySchedual", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmCopySchedual")]
   
    /// <summary>
    /// 医生排班界面控制器
    /// </summary>
    public class DocSchedualController:WcfClientController
    {
        /// <summary>
        /// 医生排班界面接口
        /// </summary>
        IFrmDocSchedual ifrmDocSchedual;

        /// <summary>
        /// 数据初始化
        /// </summary>
        public override void Init()
        {
            ifrmDocSchedual = (IFrmDocSchedual)iBaseView["FrmDocSchedual"];
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        [WinformMethod]
        public void SchedualDataInit()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "SchedualDataInit");
            DataTable dtDept = retdata.GetData<DataTable>(0);//科室
            DataTable dtDoctor = retdata.GetData<DataTable>(1);//医生
           // DataTable dtRegType = retdata.GetData<DataTable>(2); //挂号类别               
            ifrmDocSchedual.loadDept(dtDept);
            ifrmDocSchedual.DtAlldoctor = dtDoctor;
           ifrmDocSchedual.loadDoctor(dtDoctor);
            //frmDocSchedual.loadRegtype(dtRegType);
        }

        /// <summary>
        /// 加载日期列表
        /// </summary>
        [WinformMethod]
        public void GetSchedualDates()
        {
            DateTime bdate = ifrmDocSchedual.GetStatDate;
            DateTime edate = ifrmDocSchedual.GetEndDate;
            DateTime statdate = bdate;

            DataTable dtDate = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "SchedualDate";
            col.DataType = typeof(System.DateTime);
            dtDate.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "WeekDay";
            col.DataType = typeof(string);
            dtDate.Columns.Add(col);
            while (statdate <= edate)
            {
                DataRow dr = dtDate.NewRow();
                dr["SchedualDate"] = statdate;
                dr["WeekDay"] = GetWeek(statdate);
                dtDate.Rows.Add(dr);
                statdate = statdate.AddDays(1);
            }

            ifrmDocSchedual.loadSchedualDate(dtDate);
        }

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回星期几</returns>
        private string GetWeek(DateTime date)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(date.DayOfWeek)];
            return week;
        }

        /// <summary>
        /// 查询指定日期的排班信息
        /// </summary>
        /// <param name="date">查询日期</param>
        [WinformMethod]
        public void GetSchedualOneDate(DateTime date)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(date);
                request.AddData(ifrmDocSchedual.QueryDeptid);
                request.AddData(ifrmDocSchedual.QueryDocid);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "QuerySchedualOneDate", requestAction);
            DataTable dtSchdual = retdata.GetData<DataTable>(0);
            DataColumn  col = new DataColumn();
            col.ColumnName = "WeekDay";
            col.DataType = typeof(string);
            dtSchdual.Columns.Add(col);
            for (int index = 0; index < dtSchdual.Rows.Count; index++)
            {
                dtSchdual.Rows[index]["WeekDay"] = this.GetWeek(Convert.ToDateTime(dtSchdual.Rows[index]["SchedualDate"]));
            }

            ifrmDocSchedual.LoadSchedual(dtSchdual);
        }

        /// <summary>
        /// 查询一段时间的排班信息
        /// </summary>
        [WinformMethod]
        public void GetSchedualMoreDate()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmDocSchedual.GetStatDate);
                request.AddData(ifrmDocSchedual.GetEndDate);
                request.AddData(ifrmDocSchedual.QueryDeptid);
                request.AddData(ifrmDocSchedual.QueryDocid);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "QuerySchedualMoreDate", requestAction);
            DataTable dtSchdual = retdata.GetData<DataTable>(0);
            DataColumn col = new DataColumn();
            col.ColumnName = "WeekDay";
            col.DataType = typeof(string);
            dtSchdual.Columns.Add(col);
            for (int index = 0; index < dtSchdual.Rows.Count; index++)
            {
                dtSchdual.Rows[index]["WeekDay"] = this.GetWeek(Convert.ToDateTime(dtSchdual.Rows[index]["SchedualDate"]));
            }

            ifrmDocSchedual.LoadSchedual(dtSchdual);
        }

        /// <summary>
        /// 保存和更新排班
        /// </summary>
        /// <returns>保存和更新排班是否成功</returns>
        [WinformMethod]
        public bool  SaveSchedual()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmDocSchedual.CurDocSchedual);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "SaveSchedual", requestAction);
                MessageBoxShowSimple("排班成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除排班信息
        /// </summary>
        /// <returns>删除排班信息是否成功</returns>
        [WinformMethod]
        public bool DeleteSchedual()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmDocSchedual.CurDocSchedual);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "DeleteSchedual", requestAction);
                MessageBoxShowSimple("删除成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }
      
        /// <summary>
        /// 复制排班检查
        /// </summary>
        /// <returns>是否允许复制排班</returns>
        [WinformMethod]
        public bool CopySchedualCheck()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmDocSchedual.QueryDeptid);
                    request.AddData(ifrmDocSchedual.QueryDocid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "CopySchedualCheck", requestAction);
                DateTime maxSchedualDate = retdata.GetData<DateTime>(0);
                ifrmDocSchedual.CopyMaxDate = maxSchedualDate;
                if (maxSchedualDate.Date > DateTime.Now.Date)
                {
                    ifrmDocSchedual.CopyNewBDate = maxSchedualDate;
                }
                else
                {
                    ifrmDocSchedual.CopyNewBDate = DateTime.Now;
                }

                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);               
            }

            return false;
        }

        /// <summary>
        /// 复制排班接口
        /// </summary>
        IFrmCopySchedual ifrmcopySchedual;

        /// <summary>
        /// 复制排班界面初始化
        /// </summary>
       [WinformMethod]
        public void CopySchedualDataInit()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "CopySchedualDataInit");
            DataTable dtDept = retdata.GetData<DataTable>(0);//科室
            DataTable dtDoctor = retdata.GetData<DataTable>(1);//医生

            ifrmcopySchedual = (IFrmCopySchedual)iBaseView["FrmCopySchedual"];
            ifrmcopySchedual.LoadDept(dtDept);
            ifrmcopySchedual.LoadDoctor(dtDoctor);
            ifrmcopySchedual.MaxCopyDate = ifrmDocSchedual.CopyMaxDate;
            ifrmcopySchedual.MinNewCopyDate = ifrmDocSchedual.CopyNewBDate;
            ifrmcopySchedual.CopyDeptid = ifrmDocSchedual.QueryDeptid;
            ifrmcopySchedual.CopyDocid = ifrmDocSchedual.QueryDocid;           
        }

        /// <summary>
        /// 判断新排班日期内是否有排班信息
        /// </summary>
        /// <returns>返回是否有排班信息</returns>
        [WinformMethod]
        public bool GetCopySchedualCount()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmcopySchedual.NewBdate);
                    request.AddData(ifrmcopySchedual.NewEdate);
                    request.AddData(ifrmcopySchedual.CopyDeptid);
                    request.AddData(ifrmcopySchedual.CopyDocid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "GetCopySchedualCount", requestAction);
                return retdata.GetData<bool>(0);
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }

            return false;
        }

        /// <summary>
        /// 判断原排班日期内是否有排班信息
        /// </summary>
        /// <returns>返回是否有排班信息</returns>
        [WinformMethod]
        public bool GetOldSchedualCount()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmcopySchedual.OldBdate);
                    request.AddData(ifrmcopySchedual.OldEdate);
                    request.AddData(ifrmcopySchedual.CopyDeptid);
                    request.AddData(ifrmcopySchedual.CopyDocid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "GetOldSchedualCount", requestAction);
                if (retdata.GetData<bool>(0) == false)
                {
                    MessageBoxShowSimple("所选复制排班日期内不存在排班信息，不能复制排班");
                    return false;
                }

                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }

            return false;
        }

        /// <summary>
        /// 复制排班
        /// </summary>
        /// <returns>返回复制排班是否成功</returns>
        [WinformMethod]
        public bool SaveCopySchedualData()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmcopySchedual.OldBdate);
                    request.AddData(ifrmcopySchedual.OldEdate);
                    request.AddData(ifrmcopySchedual.NewBdate);
                    request.AddData(ifrmcopySchedual.NewEdate);
                    request.AddData(ifrmcopySchedual.CopyDeptid);
                    request.AddData(ifrmcopySchedual.CopyDocid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DocSchedualController", "CopySchedual", requestAction);
                MessageBoxShowSimple("复制排班成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }

            return false;
        }
    }
}
