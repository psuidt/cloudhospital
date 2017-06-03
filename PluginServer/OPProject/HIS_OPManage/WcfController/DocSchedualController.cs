using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 医生排班控制器类
    /// </summary>
    [WCFController]
    public class DocSchedualController : WcfServerController
    {
        /// <summary>
        /// 排班基础信息加载
        /// </summary>
        /// <returns>dtDept科室 dtDoctor医生</returns>
        [WCFMethod]        
        public ServiceResponseData SchedualDataInit()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.门诊临床科室,false);
            responseData.AddData(dtDept);//科室
            DataTable  dtDoctor = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.医生和科室,false);
            responseData.AddData(dtDoctor);//医生
            return responseData;
        }

        /// <summary>
        /// 查找符合条件的排班日期
        /// </summary>      
        /// <returns>返回符合条件的排班日期</returns>
        [WCFMethod]
        public ServiceResponseData QuerySchedualAllDate()
        {
            DateTime bdate = requestData.GetData<DateTime>(0);
            DateTime edate= requestData.GetData<DateTime>(1);
            int deptid = requestData.GetData<int>(2);//科室ID
            int docid = requestData.GetData<int>(3);//医生ID
            DataTable dt = new DataTable();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获得指定日期的排班详细信息
        /// </summary>
        /// <returns>datatable指定日期的排班详细信息</returns>
        [WCFMethod]
        public ServiceResponseData QuerySchedualOneDate()
        {
            DateTime date = requestData.GetData<DateTime>(0);//指定日期
            int deptid = requestData.GetData<int>(1);//科室ID
            int docid = requestData.GetData<int>(2);//医生ID          
            DataTable dt = NewDao<IOPManageDao>().GetSchedualOneDate(date,date,deptid,docid);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查询一段时间内的排班信息
        /// </summary>
        /// <returns>返回排班信息</returns>
        [WCFMethod]
        public ServiceResponseData QuerySchedualMoreDate()
        {
            DateTime bdate = requestData.GetData<DateTime>(0);//指定日期
            DateTime edate = requestData.GetData<DateTime>(1);//指定日期
            int deptid = requestData.GetData<int>(2);//科室ID
            int docid = requestData.GetData<int>(3);//医生ID          
            DataTable dt = NewDao<IOPManageDao>().GetSchedualOneDate(bdate, edate, deptid, docid);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存和更新排班信息
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData SaveSchedual()            
        {
            OP_DocSchedual  docShedual = requestData.GetData<OP_DocSchedual>(0);
            this.BindDb(docShedual);
            List<OP_DocSchedual> docShedualOld = NewObject<OP_DocSchedual>().getlist<OP_DocSchedual>(" SchedualDate='" + docShedual.SchedualDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and deptid="+docShedual.DeptID+" and docempid="+docShedual.DocEmpID+ " and SchedualTimeRange=" + docShedual.SchedualTimeRange+" and flag="+docShedual.Flag+" and SchedualID!=" + docShedual.SchedualID + string.Empty);
            if(docShedualOld!=null && docShedualOld.Count>0)
            {
                throw new Exception("该科室医生已经存在" + docShedual.SchedualDate.ToString("yyyy-MM-dd HH:mm:ss") + "相同班次的排班，请重新输入排班信息");
            }

            docShedual.save(); 
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 保存和更新排班信息
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData DeleteSchedual()
        {
            OP_DocSchedual docShedual = requestData.GetData<OP_DocSchedual>(0);
            this.BindDb(docShedual);        
            docShedual.delete();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        ///  排班复基础信息加载
        /// </summary>
        /// <returns>dtDept科室dtDoctor医生</returns>
        [WCFMethod]
        public ServiceResponseData CopySchedualDataInit()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.门诊临床科室, true);
            responseData.AddData(dtDept);//科室
            DataTable dtDoctor = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.医生, true);
            responseData.AddData(dtDoctor);//医生            
            return responseData;
        }

        /// <summary>
        /// 判断科室有没有历史排班记录
        /// </summary>
        /// <returns>返回最近最后一次排班的日期</returns>
        [WCFMethod]
        public ServiceResponseData CopySchedualCheck()
        {
            int deptid = requestData.GetData<int>(0);
            int docid = requestData.GetData<int>(1);
            object date = NewObject<IOPManageDao>().GetMaxSchedualDate(deptid,docid);
            if (date==null ||string.IsNullOrEmpty(date.ToString())) 
            {
                throw new Exception("该科室无历史排班记录");
            }

            responseData.AddData(Convert.ToDateTime(date));
            return responseData;
        }

        /// <summary>
        /// 复制排班
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CopySchedual()
        {
            DateTime oldbdate = requestData.GetData<DateTime>(0);//指定日期
            DateTime oldedate = requestData.GetData<DateTime>(1);//指定日期

            DateTime newbdate = requestData.GetData<DateTime>(2);//指定日期
            DateTime newedate = requestData.GetData<DateTime>(3);//指定日期

            int deptid = requestData.GetData<int>(4);
            int docid = requestData.GetData<int>(5);
            string strWhere = " SchedualDate>='" + oldbdate.ToString("yyyy-MM-dd") + "' and SchedualDate<='" + oldedate.ToString("yyyy-MM-dd") + "'";
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid + string.Empty; 
            }

            if (docid > 0)
            {
                strWhere += "  and docempid=" + docid + string.Empty;
            }

            NewObject<IOPManageDao>().DeleteOldCopySchedual(newbdate,newedate, deptid, docid);
            List<OP_DocSchedual> oldScheduals = NewObject<OP_DocSchedual>().getlist<OP_DocSchedual>(strWhere);
            for (int index = 0; index < oldScheduals.Count; index++)
            {
                OP_DocSchedual newschedual = oldScheduals[index].Clone() as OP_DocSchedual;
                newschedual.SchedualID = 0;
                DateTime t1 = DateTime.Parse(oldScheduals[index].SchedualDate.ToString("yyyy-MM-dd"));
                DateTime t2 = DateTime.Parse(oldbdate.ToString("yyyy-MM-dd"));
                System.TimeSpan ts = t1 - t2;
                int days = ts.Days;
                newschedual.SchedualDate = Convert.ToDateTime(newbdate.ToString("yyyy-MM-dd")).AddDays(days);
                this.BindDb(newschedual);
                newschedual.save();
            }

            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 判断新排班日期内是否存在排班信息
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData GetCopySchedualCount()
        {
            DateTime newbdate = requestData.GetData<DateTime>(0);//指定日期
            DateTime newedate = requestData.GetData<DateTime>(1);//指定日期
            int deptid = requestData.GetData<int>(2);
            int docid = requestData.GetData<int>(3);
            string strWhere = " SchedualDate>='" + newbdate.ToString("yyyy-MM-dd") + "' and SchedualDate<='" + newedate.ToString("yyyy-MM-dd") + "'";
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid + string.Empty;
            }

            if (docid > 0)
            {
                strWhere += "  and docempid=" + docid + string.Empty;
            }

            List<OP_DocSchedual> newScheduals = NewObject<OP_DocSchedual>().getlist<OP_DocSchedual>(strWhere);
            bool result = false;
            if (newScheduals != null && newScheduals.Count > 0)
            {
                result = true;
            }

            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 判断原日期内是否存在排班信息
        /// </summary>
        /// <returns>true 存在 false 不存在</returns>
        [WCFMethod]
        public ServiceResponseData GetOldSchedualCount()
        {
            DateTime oldbdate = requestData.GetData<DateTime>(0);//指定日期
            DateTime oldedate = requestData.GetData<DateTime>(1);//指定日期
            int deptid = requestData.GetData<int>(2);
            int docid = requestData.GetData<int>(3);
            string strWhere = " SchedualDate>='" + oldbdate.ToString("yyyy-MM-dd") + "' and SchedualDate<='" + oldedate.ToString("yyyy-MM-dd") + "'";
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid + string.Empty;
            }

            if (docid > 0)
            {
                strWhere += "  and docempid=" + docid + string.Empty;
            }

            List<OP_DocSchedual> oldScheduals = NewObject<OP_DocSchedual>().getlist<OP_DocSchedual>(strWhere);
            bool result = false;
            if (oldScheduals != null && oldScheduals.Count > 0)
            {
                result = true;
            }

            responseData.AddData(result);
            return responseData;
        }
    }
}
