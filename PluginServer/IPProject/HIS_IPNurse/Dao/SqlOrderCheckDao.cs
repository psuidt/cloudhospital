using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPDoctor;
using HIS_Entity.IPManage;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 医嘱发送接口实现
    /// </summary>
    public class SqlOrderCheckDao : AbstractDao, IOrderCheckDao
    {
        #region 界面所用
        /// <summary>
        /// 获取可发送医嘱病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停/正常 1:新开 2：新停 3：正常)</param>
        /// <returns>医嘱病人列表</returns>
        public DataTable GetOrderCheckPatList(int deptId, string orderCategory, string orderStatus)
        {
            string strSql = string.Empty;
            strSql = @" SELECT  1 CheckFlg ,  BedNo ,PatListID ,  PatName ,SerialNumber ,Status ,
                                CASE Sex WHEN 1 THEN '男'  WHEN 2 THEN '女'  END AS SexName
                        FROM    dbo.IP_PatList
                        WHERE   PatListID IN ( SELECT   PatListID
                                                 FROM   dbo.IPD_OrderRecord
                                                 WHERE  PatDeptID = {0} AND DeleteFlag=0 
                                                 AND   (OrderCategory = {2} OR {2}=-1) ";

            if (orderStatus == "1")
            {
                //新开
                strSql += " AND (OrderStatus = 2 AND ExecFlag = 0) AND ExecDate<'{1}' ";
            }
            else if (orderStatus == "2")
            {
                //新停
                strSql += " AND OrderStatus = 4 ";
            }
            else if (orderStatus == "3")
            {
                //正常
                strSql += " AND (OrderStatus = 2 AND ExecFlag = 1) AND ExecDate<'{1}'";
            }
            else
            {
                strSql += " AND ( OrderStatus=2 AND ExecDate<'{1}' or  OrderStatus=4 ) ";
            }

            strSql += " ) ORDER BY PatListID ";
            strSql = string.Format(strSql, deptId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), orderCategory);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取可发送医嘱列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <returns>可发送医嘱列表</returns>
        public DataTable GetOrederCheckInfo(int deptId, string orderCategory, string orderStatus)
        {
            string strSql = @"  SELECT  1 CheckFlg ,  
                                CASE A.OrderCategory  WHEN 0 THEN '长'  WHEN 1 THEN '临'  END AS OrderCategory ,  
                                CASE A.OrderStatus    WHEN 2 THEN '开'  WHEN 4 THEN '停'  END AS OrderStatus,  
                                B.BedNo,  A.PatListID,  A.GroupID,  A.OrderID,  B.PatName,  B.SerialNumber,  A.OrderBdate,  
                                C.Name AS DocName,  A.ItemName + (CASE A.Spec WHEN '' THEN '' ELSE '【' + A.Spec + '】' END) ItemName ,  
                                A.Spec,  A.Dosage,  A.DosageUnit,  A.ChannelName, A.Frequency,  
                                CASE A.AstFlag WHEN 1 THEN '阴性(-)' WHEN 2 THEN '阳性(+)' END AS AstFlg,  
                                A.FirstNum, A.TeminalNum,  A.EOrderDate,  A.Entrust,
                                CASE A.OrderType WHEN 2 THEN '是' ELSE '' END AS SelfDrug,  
                                CASE A.OrderType WHEN 3 THEN '是' ELSE '' END AS BeltDrug  
                                FROM    dbo.IPD_OrderRecord A  
                                LEFT JOIN IP_PatList B ON A.PatListID=B.PatListID  
                                LEFT JOIN BaseEmployee C ON A.OrderDoc=C.EmpId  			  
                                WHERE    A.DeleteFlag=0 AND A.PatDeptID = {0}  
				                AND ( A.OrderCategory={2} or {2}=-1 )";

            if (orderStatus == "1")
            {
                //新开
                strSql += " AND (OrderStatus = 2 AND ExecFlag = 0 )  AND ExecDate<'{1}' ";
            }
            else if (orderStatus == "2")
            {
                //新停
                strSql += " AND OrderStatus = 4 ";
            }
            else if (orderStatus == "3")
            {
                //正常
                strSql += " AND (OrderStatus = 2 AND ExecFlag = 1) AND ExecDate<'{1}' ";
            }
            else
            {
                strSql += " AND ( OrderStatus=2 AND ExecDate<'{1}' or  OrderStatus=4 ) ";
            }

            strSql += "  ORDER BY A.PatListID,A.GroupID  ";
            strSql = string.Format(strSql, deptId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), orderCategory);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人医嘱关联的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>病人医嘱关联的费用列表</returns>
        public DataTable GetOrderRelationFeeList(int patListID, int groupID)
        {
            string strSql = @"SELECT 0 CheckFlg ,0 UpdFlg ,B.Name AS ExecDeptName,A.* FROM IP_FeeItemGenerate A
                LEFT JOIN BaseDept B ON A.ExecDeptDoctorID = B.DeptId
                WHERE A.PatListID = {0}
                AND A.GroupID = {1} AND A.WorkID = {2}";
            strSql = string.Format(strSql, patListID, groupID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 算法所用

        /// <summary>
        /// 通过GroupList获取接口类和开始结束时间
        /// </summary>
        /// <param name="groupIdList">组号集合</param>
        /// <param name="endTime">发送结束时间</param>
        /// <returns>接口类和开始结束时间</returns>
        public DataTable GetIPOrderCheckByGroupId(List<int> groupIdList, DateTime endTime)
        {
            string sGroupIDs = string.Join(",", groupIdList.ToArray());
            string sql = @"  SELECT DISTINCT
                                    ippl.BedNo,
                                    ipdor.GroupID,
		                            ipdor.TeminalNum,
		                            ipdor.OrderType,
                                    (case ipdor.StatID when 102 then 1 else 0 end) zcyFlag,
                                    ipdor.OrderStatus,
                                    ipdor.ChannelID,
                                    ipdor.FrenquencyID,
                                    ipdor.Frequency,
		                            ipdor.OrderCategory,
                                    ipdor.ItemType,
	                                ipdor.FirstNum,
                                    ipdor.ExecFlag,
		                            ipdor.ExecDate,
                                    ipdor.OrderBdate,
		                            ipdor.EOrderDate 
                               FROM dbo.IPD_OrderRecord ipdor
                               INNER JOIN dbo.IP_PatList ippl ON ipdor.PatListID=ippl.PatListID AND ippl.WorkID = ipdor.WorkID
                              WHERE ipdor.DeleteFlag=0 
                                AND ipdor.OrderStatus>1
                                AND ipdor.OrderStatus<5
                                AND ipdor.GroupID IN({0})";
            sql = string.Format(sql, sGroupIDs);
            DataTable dt = oleDb.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 获取医嘱信息
        /// </summary>
        /// <param name="groupIdList">组号集合</param>
        /// <returns>医嘱列表</returns>
        public List<IPD_OrderRecord> GetIPDOrderByGroupId(List<int> groupIdList)
        {
            string sGroupIDs = string.Join(",", groupIdList.ToArray());
            List<IPD_OrderRecord> orderRecordList = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>(
                " ItemType=1 AND DeleteFlag=0 And  GroupID in (" + sGroupIDs + ")");
            return orderRecordList;
        }

        /// <summary>
        /// 查询药品库存-每组均要查询一次
        /// </summary>
        /// <returns>药品库存</returns>
        public List<IP_DrugStore> GetDrugStore()
        {
            List<IP_DrugStore> drugStoreList = new List<IP_DrugStore>();
            string sql = @"  SELECT a.DrugID  ,
                                    d.ValidAmount StoreAmount ,	--有效库存
                                    a.DeptID ExecDeptId 
                               FROM    DS_Storage a
                            INNER JOIN DS_ValidStorage d ON a.StorageID = d.StorageID
                            INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID  AND b.WorkID = a.WorkID AND b.IsStop = 0 
                            INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID=b.CenteDrugID AND c.IsStop = 0
                            WHERE d.WorkID={0}";
            sql = string.Format(sql, WorkId);
            DataTable dt = oleDb.GetDataTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                IP_DrugStore drugStore = new IP_DrugStore();
                drugStore.DrugID = Convert.ToInt32(dr["DrugID"]);
                drugStore.ExecDeptId = Convert.ToInt32(dr["ExecDeptId"]);
                drugStore.ExecDeptName = string.Empty;
                drugStore.StoreAmount = Convert.ToDecimal(dr["StoreAmount"]);
                drugStoreList.Add(drugStore);
            }

            return drugStoreList;
        }

        /// <summary>
        /// 判断项目是否停用   仅仅在算法开头查询一次
        /// </summary>
        /// <returns>项目信息</returns>
        public List<IP_DrugStore> GetItemInfo()
        {
            List<IP_DrugStore> itemStoreList = new List<IP_DrugStore>();
            string sql = @"--收费项目 3
                         SELECT  a.ItemID ,     
		                    9999 StoreAmount ,
                            0 ExecDeptId ,
                            '' ExecDeptName       
                         FROM    Basic_HospFeeItem a 
                      INNER JOIN Basic_CenterFeeItem b ON a.CenterItemID = b.FeeID AND b.IsStop = 0
                         WHERE   a.IsStop = 0
		                    AND a.IsBle=1
                            AND a.WorkID={0}
                            --组合项目 4
                         UNION ALL
                         SELECT  a.ExamItemID ItemID ,       
                            9999 StoreAmount ,
                            ISNULL(b.ExecDeptID,0) ExecDeptId ,
                            dbo.fnGetDeptName(b.ExecDeptID) ExecDeptName        
                        FROM    Basic_ExamItem a
                            INNER JOIN Basic_ExamType b ON a.ExamTypeID = b.ExamTypeID AND b.DelFlag=0
                        WHERE   a.DelFlag = 0 AND a.WorkID={0}

						UNION ALL
						SELECT a.MaterialID ItemID ,
                                9999 StoreAmount ,	--有效库存
                                0 ExecDeptId ,
                                '' ExecDeptName       
                        FROM MW_HospMakerDic a 
                        INNER JOIN MW_CenterSpecDic b ON a.CenterMatID=b.CenterMatID
                        WHERE a.IsStop = 0 AND b.IsStop = 0 AND a.IsBle=1 AND a.WorkID={0} ";
            sql = string.Format(sql, WorkId);
            DataTable dt = oleDb.GetDataTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                IP_DrugStore drugStore = new IP_DrugStore();
                drugStore.DrugID = Convert.ToInt32(dr["ItemID"]);
                drugStore.ExecDeptId = Convert.ToInt32(dr["ExecDeptId"]);
                drugStore.ExecDeptName = dr["ExecDeptName"].ToString();
                drugStore.StoreAmount = Convert.ToDecimal(dr["StoreAmount"]);
                itemStoreList.Add(drugStore);
            }

            return itemStoreList;
        }

        /// <summary>
        /// 获取执行单用法配置-开头获取一次
        /// </summary>
        /// <returns>执行单用法信息</returns>
        public DataTable GetExecuteBillChannel()
        {
            string strSql = @"SELECT beb.ID,BillName ,bebc.ChannelID 
                            FROM dbo.Basic_ExecuteBills beb
                            left JOIN  dbo.Basic_ExecuteBillChannel bebc ON beb.ID=bebc.ExecBillID AND beb.WorkID=bebc.WorkID
                          WHERE beb.DelFlag=0 and beb.WorkID={0}";
            strSql = string.Format(strSql, WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取总量取整的药品-开头获取一次
        /// </summary>
        /// <returns>总量取整的药品信息</returns>
        public DataTable GetDrugs()
        {
            string strSql = @"SELECT  a.DrugID , a.DeptID ExecDeptId ,
                                    (CASE a.ResolveFlag WHEN 1 THEN 1 ELSE  CONVERT(DECIMAL(18, 4), a.UnitAmount) END) MiniConvertNum,
                                    a.ResolveFlag , --药品拆零 1可拆零      
		                            b.RoundingMode --取整方式 0总量取整 1单次取整
                            FROM    DS_Storage a
                         INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID AND b.WorkID = a.WorkID
                          LEFT JOIN DG_CenterSpecDic c ON b.CenteDrugID = c.CenteDrugID
                            WHERE   b.IsStop = 0 AND c.IsStop = 0
                                and a.WorkID={0}";
            strSql = string.Format(strSql, WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取实时项目药品的价格-开头获取一次
        /// </summary>
        /// <returns>项目药品的价格信息</returns>
        public DataTable GetStorePrice()
        {
            string strSql = @"SELECT ItemID  ,ExecDeptId    ,InPrice  ,SellPrice ,Standard  ,MiniConvertNum,ResolveFlag
                                 FROM dbo.ViewFeeItem_SimpleList
                                WHERE WorkID={0}";
            strSql = string.Format(strSql, WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取自动发送的GroupId
        /// </summary>
        /// <param name="endTime">结束时间</param>
        /// <returns>组号ID集合</returns>
        public DataTable GetIPOrderCheckGroupList(DateTime endTime)
        {
            // 做成非口服药查询时间
            DateTime fKFDrugTime = Convert.ToDateTime(endTime.AddDays(1).ToString("yyyy-MM-dd ")+ "00:00:01");
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT DISTINCT ");
            strSql.Append(" A.GroupID ");
            strSql.Append(" FROM    dbo.IPD_OrderRecord A ");
            strSql.Append(" WHERE A.DeleteFlag = 0 ");
            strSql.Append(" AND OrderStatus IN(2, 4) ");
            strSql.AppendFormat(" AND A.WorkID = '{0}' ", WorkId);
            strSql.AppendFormat(" AND A.ExecDate < '{0}' AND[dbo].[fnIsOralMedicine](ChannelID) = 0 ", fKFDrugTime);
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT DISTINCT ");
            strSql.Append(" A.GroupID ");
            strSql.Append(" FROM    dbo.IPD_OrderRecord A ");
            strSql.Append(" WHERE A.DeleteFlag = 0 ");
            strSql.Append(" AND OrderStatus IN(2, 4) ");
            strSql.AppendFormat(" AND A.WorkID = '{0}' ", WorkId);
            strSql.AppendFormat(" AND A.ExecDate < '{0}' AND[dbo].[fnIsOralMedicine](ChannelID) = 1 ", endTime);
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取批量发送的GroupId
        /// </summary>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="iOrderCategory">医嘱类型</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>组号ID集合</returns>
        public DataTable GetIPOrderCheckGroupListByPatListId(int iPatientId, int iOrderCategory, DateTime endTime)
        {
            string strSql = @"SELECT DISTINCT   A.GroupID  FROM    dbo.IPD_OrderRecord A    
                                  WHERE    A.DeleteFlag=0  AND OrderStatus in (2,4)
                                AND A.WorkID='{0}' 
                                AND A.PatListID={1}
                                AND (A.OrderCategory={2} or {2}=-1)
                                AND A.ExecDate<'{3}' ";
            strSql = string.Format(strSql, WorkId, iPatientId, iOrderCategory, endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 发送完更新医嘱表
        /// </summary>
        /// <param name="iGroupId">组号ID</param>
        /// <param name="execDate">执行时间</param>
        /// <param name="empId">操作员ID</param>
        /// <returns>true发送成功</returns>
        public bool UpdateOrderSend(int iGroupId, DateTime execDate, int empId)
        {
            //0.更新申请单数据
            string sqlApply = @"update dbo.EXA_MedicalApplyDetail
                          SET  ApplyStatus = 1
                        WHERE PresDetailID in ( select OrderID from dbo.IPD_OrderRecord where  GroupID={0} AND  DeleteFlag=0 )";
            sqlApply = string.Format(sqlApply, iGroupId);
            oleDb.DoCommand(sqlApply);
            //1.更新医嘱表
            string sqlUp = @" update dbo.IPD_OrderRecord
                          SET  OrderStatus = (CASE WHEN (OrderStatus=4 or OrderCategory = 1 ) THEN 5 ELSE OrderStatus END ) ,
                               ExecFlag = 1,
                               ExecDate = '{1}',
                               ExecNurse = {2}
                        WHERE GroupID={0} AND  DeleteFlag=0";

            sqlUp = string.Format(sqlUp, iGroupId, execDate.ToString("yyyy-MM-dd HH:mm:ss"), empId);
            return oleDb.DoCommand(sqlUp) > 0;
        }

        #endregion

        #region
        /// <summary>
        /// 获取病人的费用账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">2-长期账单 3-临时账单</param>
        /// <returns>病人费用账单</returns>
        public DataTable GetPatFeeAccount(int patListID, int orderType)
        {
            string sql = @" SELECT DISTINCT CASE WHEN B.ID > 0 THEN 1  ELSE 0 END AS StrikeABalanceFLG ,  A.GenerateID ,  0 CheckFLG,  C.ItemCode ,  A.ItemName ,  C.ExecDeptName ,  A.Spec ,  C.UnitPrice ,  C.StoreAmount ,  A.Amount ,  
                            A.Unit ,  A.TotalFee ,  D.Name MarkEmpName ,  A.PresDate ,  A.PatListID ,  A.PatName ,  A.PatDeptID ,  A.PatDoctorID ,  A.PatNurseID ,  A.BabyID ,  A.ItemID ,  A.FeeClass ,  A.StatID ,  A.PackAmount , 
                            A.InPrice ,  A.SellPrice ,  A.DoseAmount ,  A.PresDeptID ,  A.PresDoctorID ,  A.ExecDeptDoctorID ,  A.MarkDate ,  A.MarkEmpID ,  A.SortOrder ,  A.PackUnit ,  A.OrderID ,  A.GroupID ,  A.OrderType ,  
                            A.FrequencyID ,  A.FrequencyName ,  A.ChannelID ,  A.ChannelName ,  A.IsStop ,  0 AS IsUpdate ,  A.WorkID 
                            FROM IP_FeeItemGenerate A 
                            LEFT JOIN IP_FeeItemRelationship B ON A.GenerateID = B.GenerateID  
                            LEFT JOIN dbo.ViewFeeItem_SimpleList C ON A.ItemID = C.ItemID  AND A.ExecDeptDoctorID = C.ExecDeptId  AND A.FeeClass = C.ItemClass  AND A.StatID = C.StatID  
                            LEFT JOIN BaseEmployee D ON A.MarkEmpID = D.EmpId 
                             WHERE  A.IsStop <> 1 AND A.WorkID = {0} AND ( A.PatListID = {1} or {1}=-1) AND (A.OrderType = {2} or ({2}=-1 and A.OrderType in (2,3)))     ORDER BY MarkDate ";
            sql = string.Format(sql, WorkId, patListID, orderType);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取可发送账单的生成表ID
        /// </summary>
        /// <param name="iPatientID">病人登记ID</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>账单的生成表ID集合</returns>
        public DataTable GetFeeAccount(int iPatientID, DateTime endTime)
        {
            string sql = @"SELECT DISTINCT  A.GenerateID
                            FROM IP_FeeItemGenerate A 
                      Left JOIN IP_FeeItemRelationship B ON A.GenerateID = B.GenerateID  AND datediff(day,ExecDate,'{2}')>0
                          WHERE  A.IsStop <> 1  AND A.WorkID = {0}  AND A.OrderType in (2,3) AND (A.PatListID={1} or {1}=-1 )";
            sql = string.Format(sql, WorkId, iPatientID, endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            DataTable dt = oleDb.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 获取可发送床位费的病人ID
        /// </summary>
        /// <param name="iPatientID">病人登记ID</param>
        /// <returns>病人信息</returns>
        public DataTable GetBedPatientIdList(int iPatientID)
        {
            string sql = @" SELECT DISTINCT  B.PatListID ,B.SerialNumber,dbo.fnGetDeptName(B.CurrDeptID) DeptName,B.PatName
                            FROM IP_BedInfo A  
                      INNER JOIN IP_PatList B  ON A.PatListID = B.PatListID  
                         WHERE  A.IsStoped <> 1 AND A.WorkID = 1  AND (B.PatListID={1} or {1}=-1)";
            sql = string.Format(sql, WorkId, iPatientID);
            DataTable dtBed = oleDb.GetDataTable(sql);
            return dtBed;
        }
        #endregion

        /// <summary>
        /// 按科室获取自动发送的GroupId
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>组号集合</returns>
        public DataTable GetDeptIPOrderCheckGroupList(int iDeptId, DateTime endTime)
        {
            string strSql = @"SELECT DISTINCT   A.GroupID  FROM    dbo.IPD_OrderRecord A    
                                  WHERE   A.DeleteFlag=0  AND OrderStatus in (2,4)
                                AND A.WorkID='{0}' AND A.ExecDate<'{1}' AND A.PatDeptID={2}";
            strSql = string.Format(strSql, WorkId, endTime.ToString("yyyy-MM-dd HH:mm:ss"), iDeptId);
            return oleDb.GetDataTable(strSql);
        }
    }
}
