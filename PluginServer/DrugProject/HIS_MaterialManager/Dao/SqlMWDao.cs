using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.CommonSql;

namespace HIS_MaterialManager.Dao
{
    /// <summary>
    /// 物资SQLSERVER数据库访问类
    /// </summary>
    public class SqlMWDao : AbstractDao, IMWDao
    {
        #region 公共获取数据方法
        /// <summary>
        /// 返回表实体的DATATABLE
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>实体的DATATABLE</returns>
        public DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere) where T : AbstractEntity
        {
            string sql = SqlServerProcess.GetExecutSql<T>(andWhere, orWhere);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 返回表实体的DATATABLE
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <param name="page">分页信息</param>
        /// <returns>实体的DATATABLE</returns>
        public DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, PageInfo page) where T : AbstractEntity
        {
            string sql = SqlServerProcess.GetExecutSql<T>(andWhere, orWhere);
            return oleDb.GetDataTable(SqlPage.FormatSql(sql, page, oleDb));
        }

        /// <summary>
        /// 返回表实体的DATATABLE
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>实体集合</returns>
        public IEnumerable<T> GetEntityType<T>(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null) where T : AbstractEntity
        {
            var sql = SqlServerProcess.GetExecutSql<T>(andWhere, orWhere);
            return oleDb.Query<T>(sql, null);
        }
        #endregion

        #region 物资供应商
        /// <summary>
        /// 加载供应商【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        public DataTable GetSupplyForShowCard()
        {
            string strSql = @"select SupplierID,SupportName,PYCode,WBCode 
                            from MW_SupportDic where DelFlag=0 and WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 物资类型
        /// <summary>
        /// 获取物资子类型
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>物资子类型数据</returns>
        public DataTable GetChildMaterialType(Dictionary<string, string> queryCondition)
        {
            var sql = "SELECT *,(select TypeName from [dbo].[MW_TypeDic] where TypeID={0}) as ParentName FROM [dbo].[MW_TypeDic] WHERE 1=1";
            string typeid = string.Empty;
            StringBuilder stb = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key == "ParentID")
                {
                    stb.AppendFormat("AND {0}={1} ", pair.Key, pair.Value);
                    typeid = pair.Value;
                }

                if (pair.Key == "TypeName")
                {
                    if (stb.Length > 0)
                    {
                        stb.AppendFormat("AND ({0} like '%{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" ({0} like '{1}%' ", pair.Key, pair.Value);
                    }
                }

                if (pair.Key == "PYCode")
                {
                    stb.AppendFormat(" OR {0} like '%{1}%') ", pair.Key, pair.Value);
                }
            }

            return oleDb.GetDataTable(string.Format(sql + stb.ToString(), typeid));
        }

        /// <summary>
        /// 物资子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资子类型数据</returns>
        public string GetChildDrugTypeSql(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT c.*,p.TypeName AS PNAME ");
            stb.Append("FROM [DG_ChildTypeDic] C INNER JOIN dbo.DG_TypeDic p ON p.TypeID = c.TypeID where 1 = 1");
            if (query != null && query.Count > 0)
            {
                var kp = query.FirstOrDefault(i => i.Key == "TypeID");

                if (kp.Key == "TypeID")
                {
                    stb.AppendFormat("and C.{0}={1} ", kp.Key, kp.Value);
                }

                var other = query.Where(i => i.Key != "TypeID");
                if (other.Any())
                {
                    stb.Append("and ( ");
                    var num = 0;
                    foreach (KeyValuePair<string, string> keyValuePair in other)
                    {
                        if (num == 0)
                        {
                            stb.AppendFormat(" C.{0} like '{1}%' ", keyValuePair.Key, keyValuePair.Value);
                        }
                        else
                        {
                            stb.AppendFormat("or C.{0} like '{1}%' ", keyValuePair.Key, keyValuePair.Value);
                        }
                        ++num;
                    }

                    stb.Append(" ) ");
                }
            }

            return stb.ToString();
        }

        /// <summary>
        /// 获取物资类型ShowCard
        /// </summary>
        /// <returns>物资类型数据</returns>
        public DataTable GetMaterialTypeShowCard()
        {
            string sqlstr = "SELECT a.TypeID,a.TypeName,b.TypeName ParentName,c.TypeName PParentName FROM MW_TypeDic a LEFT JOIN MW_TypeDic b ON a.ParentID=b.TypeID LEFT JOIN MW_TypeDic c ON b.ParentID=c.TypeID WHERE a.Level=3";
            return oleDb.GetDataTable(sqlstr);
        }
        #endregion

        #region 物资字典
        /// <summary>
        /// 获取物资字典
        /// </summary>
        /// <param name="strSql">查询字符串</param>
        /// <returns>物资字典数据</returns>
        public DataTable GetMaterialDic(string strSql)
        {
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 审核物资字典
        /// </summary>
        /// <param name="meterialID">物资id</param>
        /// <returns>1成功</returns>
        public int AuditDic(int meterialID)
        {
            string strsql = @"UPDATE MW_CenterSpecDic SET AuditStatus=1 WHERE CenterMatID={0}";
            return oleDb.DoCommand(string.Format(strsql, meterialID));
        }

        /// <summary>
        /// 根据物资字典ID获取厂家信息
        /// </summary>
        /// <param name="centerMatID">中心物资id</param>
        /// <returns>物资厂家典数据</returns>
        public DataTable LoadHisDic(int centerMatID)
        {
            string strsql = @"SELECT d.*,p.ProductName,m.MedicareName,
	      CASE d.IsStop  
		  WHEN 0 THEN '启用'
		  WHEN 1 THEN '停用' 
		  END AS StopName
          FROM MW_HospMakerDic d 
          LEFT JOIN dbo.MW_ProductDic p ON 
          p.ProductID = d.ProductID
          LEFT JOIN dbo.DG_MedicareDic m ON m.MedicareID = d.MedicareID
          WHERE CenterMatID={0}";
            return oleDb.GetDataTable(string.Format(strsql, centerMatID));
        }
        #endregion

        #region 物资库房设置
        /// <summary>
        /// 物资科室设置-是否已经存在物资科室
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>true存在,false不存在</returns>
        public bool ExistMaterialDept(int deptId)
        {
            string strsql = @"SELECT COUNT(1) FROM MW_DeptDic WHERE DeptID={0} AND WorkID={1}";
            strsql = string.Format(strsql, deptId, oleDb.WorkId);
            return Convert.ToInt32(oleDb.GetDataResult(strsql)) == 0 ? false : true;
        }

        /// <summary>
        /// 获取物资科室数据
        /// </summary>
        /// <returns>物资科室数据集</returns>
        public DataTable GetDeptDicData()
        {
            string strSql = @"SELECT 
                               DeptDicID ,
                               DeptName,
                               DeptCode,
                               DeptType,
                               (CASE DeptType WHEN 0 THEN '药房' WHEN 1 THEN '药库' ELSE '物资' END) AS DeptTypeName,
                               StopFlag,
                               (CASE StopFlag WHEN 1 THEN '停用' ELSE '启用' END) AS StopFlagName, 
                               DeptID,
                               WorkID,
                               CheckStatus
                               FROM MW_DeptDic WHERE WorkID={0} order by DeptType";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 按类型获取物资科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>物资科室列表</returns>
        public DataTable GetDrugDeptList(int deptType)
        {
            string strSql = @"SELECT DeptID, DeptName
                              FROM MW_DeptDic
                              WHERE DeptType = {0} AND StopFlag = 0 AND WorkID = {1}";
            strSql = string.Format(strSql, deptType, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 初始化物资科室的单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型</param>
        /// <returns>true成功，false失败</returns>
        public bool InitSerialNumber(int deptId, int deptType)
        {
            string sNType = string.Empty;
            string businessType = string.Empty;

            //取得业务类型对应的单据数据
            string strSql = @"SELECT   BusiTypeID, BusiTypeName, Remark, BusiCode, IsStop, DeptType, DelFlag
                            FROM      DG_BusiType
                            WHERE DelFlag=0 AND IsStop=0 AND DeptType={0}";
            strSql = string.Format(strSql, deptType);
            DataTable dt = oleDb.GetDataTable(strSql);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sNType = "4";
                    businessType = dt.Rows[i]["BusiCode"].ToString();
                    string haveSql = string.Format(@"SELECT COUNT(*) FROM Basic_SerialNumberSource WHERE BusinessType={0} and  DeptId={1} AND WorkID={2}", businessType, deptId, oleDb.WorkId);
                    if (Convert.ToInt32(oleDb.GetDataResult(haveSql)) > 0)
                    {
                        continue;
                    }

                    string insertSql = @"INSERT INTO Basic_SerialNumberSource
                                      (SNType,
                                        BusinessType,
                                        DeptId,
                                        CurrDate,
                                        CurrSequence,
                                        WorkID)
                                 VALUES
                                       ({0},
                                       '{1}',
                                       {2},
                                       GETDATE(),
                                       0,
                                       {3})";

                    insertSql = string.Format(insertSql, sNType, businessType, deptId, oleDb.WorkId);
                    int val = oleDb.DoCommand(insertSql);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取得物资科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>物资科室单据集</returns>
        public DataTable GetDrugDeptBill(int deptId)
        {
            string strSql = @"SELECT  t.SNType,t.BusinessType,t.DeptId,t.CurrDate,t.CurrSequence,t.WorkID,t1.BusiTypeName 
                            FROM Basic_SerialNumberSource t LEFT JOIN DG_BusiType t1 ON t.BusinessType=t1.BusiCode AND t1.IsStop=0 AND t1.DelFlag=0
                            WHERE SNType=4 AND t.DeptId={0} AND T.WorkID={1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 修改启用标志
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>小于0失败</returns>
        public int UpdateStartStatus(int deptId)
        {
            string strSql = @"UPDATE MW_DeptDic SET StopFlag = 0 WHERE DeptID = {0}";
            strSql = string.Format(strSql, deptId);
            return oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 停用物资科室
        /// </summary>
        /// <param name="deptDicID">物资科室Id</param>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int StopUseDrugDept(int deptDicID, int deptId)
        {
            //1更改物资科室启用状态
            string strSql = @"UPDATE MW_DeptDic SET StopFlag = 1 WHERE DeptDicID = {0}";
            strSql = string.Format(strSql, deptDicID);
            int ret = oleDb.DoCommand(strSql);
            MW_DeptDic model = (MW_DeptDic)NewObject<MW_DeptDic>().getmodel(deptDicID);
            string where = "DeptID=" + model.DeptID;
            DataTable dtStoage = ((MW_Storage)NewObject<MW_Storage>()).gettable(where);
            if (dtStoage.Rows.Count > 0)
            {
                return 1;
            }

            //2.删除单据流水表
            strSql = @"DELETE FROM Basic_SerialNumberSource WHERE DeptId={0}";
            strSql = string.Format(strSql, deptId);
            ret = oleDb.DoCommand(strSql);
            //3.删除物资科管理类型表
            strSql = @"DELETE FROM DG_Dept_Type WHERE DeptID ={0}";
            strSql = string.Format(strSql, deptId);
            ret = oleDb.DoCommand(strSql);
            return ret;
        }

        /// <summary>
        /// 设置物资科室盘点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="status">状态1开启盘点，0未开启</param>
        public void SetCheckStatus(int deptId, int status)
        {
            string strSql = @"UPDATE MW_DeptDic SET CheckStatus={0} WHERE DeptID={1} AND WorkID={2} ";
            strSql = string.Format(strSql, status, deptId, oleDb.WorkId);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典数据</returns>
        public DataTable GetMWTypeDic()
        {
            string strSql = @"SELECT 0 AS selected,
                               TypeID
                              ,TypeName
                              ,PYCode
                              ,WBCode
                              ,WorkID
                          FROM MW_TypeDic
                          WHERE [Level]=1";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 取得物资科室
        /// </summary>
        /// <returns>物资科室数据</returns>
        public DataTable GetMaterialDept()
        {
            string strSql = @"SELECT DeptID,DeptName FROM MW_DeptDic WHERE DeptType=2 AND StopFlag=0 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }
        #endregion

        #region 物资参数设置
        /// <summary>
        /// 取得启用的科室
        /// </summary>
        /// <returns>启用的科室集</returns>
        public DataTable GetUsedDeptData()
        {
            string strSql = @"SELECT  DeptID,DeptName FROM MW_DeptDic WHERE StopFlag=0 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取公共参数
        /// </summary>
        /// <returns>公共参数集</returns>
        public DataTable GetPublicParameters()
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 4 AND DeptID = 0 AND WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>科室参数集</returns>
        public DataTable GetDeptParameters(int deptId)
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 4 AND DeptID = {0} AND WorkID = {1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取部门的参数配置
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="paraId">参数ID</param>
        /// <returns>参数实体对象</returns>
        public Basic_SystemConfig GetDeptParameters(int deptId, string paraId)
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 4 AND DeptID = {0} AND WorkID = {1} and Paraid='{2}'";
            strSql = string.Format(strSql, deptId, oleDb.WorkId, paraId);
            return oleDb.Query<Basic_SystemConfig>(strSql, null).FirstOrDefault();
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="parameterList">参数列表</param>
        /// <returns>小于0失败</returns>
        public int SaveParameters(List<Basic_SystemConfig> parameterList)
        {
            int ret = 0;
            string strSql = string.Empty;
            foreach (Basic_SystemConfig m in parameterList)
            {
                strSql = @"SELECT COUNT(1) FROM Basic_SystemConfig WHERE DeptID={0} AND SystemType=4 AND ParaID='{1}' AND WorkID={2}";
                strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);
                //修改
                if (Convert.ToInt32(oleDb.GetDataResult(strSql)) > 0)
                {
                    strSql = @"UPDATE Basic_SystemConfig SET SystemType=4,DeptID=" + m.DeptID + ",ParaID='" + m.ParaID + "',ParaName='" + m.ParaName + "',Value='" + m.Value + "',DataType=0,Prompt='',Memo='" + m.Memo + "',WorkID=" + oleDb.WorkId +
                              " WHERE DeptID={0} AND SystemType=4 AND ParaID='{1}' AND WorkID={2}";
                    strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);
                }
                else
                {
                    //插入
                    strSql = @"INSERT INTO Basic_SystemConfig(SystemType,DeptID,ParaID,ParaName,Value,DataType,Prompt,Memo,WorkID)"
                             + @"VALUES(4, " + m.DeptID + ",'" + m.ParaID + "','" + m.ParaName + "','" + m.Value + "',0,'','" + m.Memo + "'," + oleDb.WorkId + ")";
                }

                ret = oleDb.DoCommand(strSql);
            }

            return ret;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int SaveParameters(int deptId)
        {
            List<Basic_SystemConfig> parameterList = new List<Basic_SystemConfig>();
            Basic_SystemConfig model = new Basic_SystemConfig();
            parameterList.Clear();
            //加载通用参数

            //物资材料利润百分比
            string pm016 = "16";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = 0, ParaID = "MWPricePercent", ParaName = "物资利润率", Value = pm016, DataType = 0, Prompt = string.Empty, Memo = "物资利润率(百分比)" });

            //加载部门参数
            //出库单自动审核
            string pm021 = "0";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AutoAuditOutStore", ParaName = "出库单自动审核", Value = pm021, DataType = 0, Prompt = string.Empty, Memo = "出库单自动审核:0手动;1自动" });

            //入库单自动审核 chk_input_check
            string pm020 = "0";
            
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AutoAuditInstore", ParaName = "入库单自动审核", Value = pm020, DataType = 0, Prompt = string.Empty, Memo = "入库单自动审核:0手动;1自动" });

            //月结时间为每月 iip_banlance_day
            string pm008 = "25";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "BalanceDay", ParaName = "默认月结时间", Value = pm008, DataType = 0, Prompt = string.Empty, Memo = "默认月结时间(每月多少号)" });
            //强制控制库存 chk_qzkc
            string pm010 = "0";
            
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "ControlStore", ParaName = "是否允许库存为负数", Value = pm010, DataType = 0, Prompt = string.Empty, Memo = "是否允许库存为负数:0不强制控制;1强制控制" });

            //允许强制平账 chk_qzpz
            string pm011 = "1";
           
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "AllowModifyAccount", ParaName = "允许强制平账", Value = pm011, DataType = 0, Prompt = string.Empty, Memo = "是否允许自动平账:0不允许;1允许" });

            //月结前必须对账 chk_dz
            string pm012 = "0";
           
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "CheckAccountWhenBalance", ParaName = "月结前必须对账", Value = pm012, DataType = 0, Prompt = string.Empty, Memo = "月结时是否对账:0不对账;1对账" });
            //对账误差范围 iip_wzfw
            string pm013 = "1";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 4, DeptID = deptId, ParaID = "ErrorRange", ParaName = "对账误差范围", Value = pm013, DataType = 0, Prompt = string.Empty, Memo = "金额允许误差范围(0.1元为单位)" });
            int ret = 0;
            string strSql = string.Empty;
            foreach (Basic_SystemConfig m in parameterList)
            {
                strSql = @"SELECT COUNT(1) FROM Basic_SystemConfig WHERE DeptID={0} AND SystemType=4 AND ParaID='{1}' AND WorkID={2}";
                strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);

                //修改
                if (Convert.ToInt32(oleDb.GetDataResult(strSql)) <= 0)
                {                  
                    strSql = @"INSERT INTO Basic_SystemConfig(SystemType,DeptID,ParaID,ParaName,Value,DataType,Prompt,Memo,WorkID)"
                             + @"VALUES(4, " + m.DeptID + ",'" + m.ParaID + "','" + m.ParaName + "','" + m.Value + "',0,'','" + m.Memo + "'," + oleDb.WorkId + ")";
                }

                ret = oleDb.DoCommand(strSql);
            }

            return ret;
        }
        #endregion

        #region 往来科室设置

        /// <summary>
        /// 根据科室Id获取往来科室数据
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>往来科室数据集</returns>
        public DataTable GetRelateDeptData(int deptId)
        {
            string strSql = @"SELECT 
                               DrugDeptID,
                               RelationDeptID,
                               RelationDeptName,
                               DelFlag,
                               Remark,
                               UpdateTime,
                               RegEmpID,
                               WorkID,
                               RelationDeptType,
                               (CASE RelationDeptType WHEN 0 THEN '药库' WHEN 1 THEN '药房' WHEN 2 THEN '科室' END) RelationDeptTypeName
                            FROM MW_DeptRelation
                            WHERE DelFlag=0 AND DrugDeptID={0} AND WorkID={1} and delflag=0 ";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取往来科室数据
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>往来科室数据</returns>
        public DataTable GetRelateDeptData(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(@"SELECT 
                               DrugDeptID,
                               RelationDeptID,
                               RelationDeptName,
                               DelFlag,
                               Remark,
                               UpdateTime,
                               RegEmpID,
                               WorkID,
                               RelationDeptType,
                               (CASE RelationDeptType WHEN 0 THEN '药库' WHEN 1 THEN '药房' WHEN 2 THEN '科室' END) RelationDeptTypeName
                            FROM MW_DeptRelation
                            WHERE DelFlag=0 ");

            //AND DrugDeptID = { 0 } AND WorkID = { 1 } and delflag = 0
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            DataTable dt = oleDb.GetDataTable(stb.ToString());
            return dt;
        }

        /// <summary>
        /// 批量保存往来科室
        /// </summary>
        /// <param name="dtSave">往来科室数据集</param>
        /// <param name="empId">操作员Id</param>
        /// <returns>小于0失败</returns>
        public int BatchSaveRelateDept(DataTable dtSave, int empId)
        {
            int ret = 0;
            foreach (DataRow row in dtSave.Rows)
            {
                string drugDeptID = row["DrugDeptID"].ToString();
                string relationDeptID = row["RelationDeptID"].ToString();
                string relationDeptTypeName = row["RelationDeptTypeName"].ToString();
                string relationDeptName = row["RelationDeptName"].ToString();
                string type = GetRelateDeptTypeId(relationDeptTypeName);
                string strSql = @"SELECT  COUNT(1)
                                FROM    MW_DeptRelation
                                WHERE   DelFlag = 0
                                        AND DrugDeptID = {0}
                                        AND RelationDeptID = {1}
                                        AND WorkID = {2}";
                strSql = string.Format(strSql, drugDeptID, relationDeptID, oleDb.WorkId);
                int cnt = Convert.ToInt32(oleDb.GetDataResult(strSql));

                //修改
                if (cnt > 0)
                {
                    strSql = @"UPDATE  MW_DeptRelation
                            SET  
                                    RelationDeptName='{0}' ,
                                    DelFlag=0 ,
                                    Remark='' ,
                                    UpdateTime=GETDATE() ,
                                    RegEmpID ={1},
                                    RelationDeptType={2}
                            FROM    MW_DeptRelation
                            WHERE   DrugDeptID = {3}
                                            AND RelationDeptID = {4}
                                            AND WorkID = {5}";
                    strSql = string.Format(strSql, relationDeptName, empId, type, drugDeptID, relationDeptID, oleDb.WorkId);
                }
                else
                {
                    //增加
                    strSql = @"INSERT INTO  MW_DeptRelation (DrugDeptID ,
                                RelationDeptID ,
                                RelationDeptName ,
                                DelFlag ,
                                Remark ,
                                UpdateTime ,
                                RegEmpID ,
                                WorkID ,
                                RelationDeptType
		                        )
		                        VALUES
		                        (
		                        {0},
		                        {1},
		                        '{2}',
		                        0,
		                        '',
		                        GETDATE(),
		                        {3},
		                        {4},
		                        {5}
		                        )";
                    strSql = string.Format(strSql, drugDeptID, relationDeptID, relationDeptName, empId, oleDb.WorkId, type);
                }

                ret = oleDb.DoCommand(strSql);
            }

            return ret;
        }

        /// <summary>
        /// 转换科室类别
        /// </summary>
        /// <param name="relationDeptTypeName">科室类别名称</param>
        /// <returns>科室类表标志</returns>
        private string GetRelateDeptTypeId(string relationDeptTypeName)
        {
            string type = "0";
            switch (relationDeptTypeName)
            {
                case "药库":
                    type = "0";
                    break;
                case "药房":
                    type = "1";
                    break;
                case "科室":
                    type = "2";
                    break;
            }

            return type;
        }

        /// <summary>
        /// 删除往来科室
        /// </summary>
        /// <param name="drugDeptID">药剂科室Id</param>
        /// <param name="relationDeptID">往来科室Id</param>
        /// <returns>小于0失败</returns>
        public int DeleteRelateDept(int drugDeptID, int relationDeptID)
        {
            int ret = 0;
            string strSql = @"SELECT  COUNT(1)
                                FROM    MW_DeptRelation
                                WHERE   DelFlag = 0
                                        AND DrugDeptID = {0}
                                        AND RelationDeptID = {1}
                                        AND WorkID = {2}";
            strSql = string.Format(strSql, drugDeptID, relationDeptID, oleDb.WorkId);
            int cnt = Convert.ToInt32(oleDb.GetDataResult(strSql));
            if (cnt > 0)
            {
                strSql = @"DELETE  FROM MW_DeptRelation
                         WHERE   DelFlag = 0
                                AND DrugDeptID = {0}
                                AND RelationDeptID = {1}
                                AND WorkID = {2}";
                strSql = string.Format(strSql, drugDeptID, relationDeptID, oleDb.WorkId);
                ret = oleDb.DoCommand(strSql);
            }
            else
            {
                ret = 0;
            }

            return ret;
        }
        #endregion

        #region 验收付款
        /// <summary>
        /// 取消付款,并删除付款记录
        /// </summary>
        /// <param name="payRecordID">付款id</param>
        /// <returns>1成功</returns>
        public int UpdatePayRecord(string payRecordID)
        {
            string strSql = "UPDATE MW_InStoreHead SET PayFlag=0  WHERE PayRecordID in ({0})";
            oleDb.DoCommand(string.Format(strSql, payRecordID));
            strSql = "UPDATE MW_PayRecord SET DelFlag=1 WHERE PayRecordID in ({0})";
            return oleDb.DoCommand(string.Format(strSql, payRecordID));
        }

        /// <summary>
        /// 查询付款记录
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>付款记录</returns>
        public DataTable LoadPayRecord(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT 0 as cks,* FROM MW_PayRecord WHERE DelFlag=0";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty && pair.Key != "LikeSearch")
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="inHeadID">入库单头id</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="payTime">付款时间</param>
        /// <param name="payRecordID">付款记录id</param>
        /// <param name="type">付款标识0未付款1已付款</param>
        /// <returns>1成功</returns>
        public int UpdateStoreHead(string inHeadID, string invoiceNO, DateTime payTime, int payRecordID, int type)
        {
            string strSql =
                "UPDATE MW_InStoreHead SET PayFlag={0},InvoiceNO='{2}',InvoiceTime='{3}',PayRecordID={4} WHERE InHeadID in ({1})";
            return oleDb.DoCommand(string.Format(strSql, type, inHeadID, invoiceNO, payTime, payRecordID));
        }

        /// <summary>
        /// 查询入库表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        public DataTable LoadInStoreHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT  *
FROM    ( SELECT    0 AS ck ,
                    a.* ,
                    b.BusiTypeName ,
                    bd.Name
          FROM      MW_InstoreHead a
                    LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
                    LEFT JOIN dbo.BaseDept bd ON bd.DeptId = a.DeptID
        ) AS a
     WHERE a.DelFlag = 0 AND a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.Append(" order by AuditTime,RegTime  ");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        public DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT a.*,c.CenterMatName,b.MatName,c.Spec,d.ProductName,f.Amount as TotalAmount,
                            a.RetailFee - a.StockFee AS DiffFee,
                            a.Amount as pAmount
                                FROM MW_InStoreDetail a
                                LEFT JOIN MW_HospMakerDic b
                                ON a.MaterialID = b.MaterialID
                                LEFT JOIN MW_CenterSpecDic c
                                ON b.CenterMatID = c.CenterMatID
                                LEFT JOIN MW_ProductDic d
                                ON b.ProductID = d.ProductID
                                LEFT JOIN MW_Storage f
								 ON a.MaterialID=f.MaterialID  AND f.DeptID=a.DeptID  
                            WHERE b.IsStop = 0 AND c.IsStop = 0
                            AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }
        #endregion

        #region 出入库
        /// <summary>
        /// 查询入库物资ShowCard
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>物资ShowCard数据源</returns>
        public DataTable GetDrugDicForInStoreShowCard(bool isRet, int deptID)
        {
            if (!isRet)
            {
                string strSql = @"SELECT b.MedicareID,b.MaterialID,a.CenterMatName,b.MatName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,a.TypeID,
                              b.StockPrice,b.RetailPrice,a.UnitID,a.UnitName
                              FROM MW_CenterSpecDic  a
                              LEFT JOIN MW_HospMakerDic b
                              ON a.CenterMatID = b.CenterMatID
                              LEFT JOIN MW_ProductDic c
                              ON b.ProductID = c.ProductID
                              WHERE a.IsStop = 0 AND b.IsStop = 0";
                return oleDb.GetDataTable(strSql);
            }
            else
            {
                string strSql = @"	  SELECT b.MedicareID,b.MaterialID,a.CenterMatName,b.MatName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,a.TypeID,
                              b.StockPrice,b.RetailPrice,a.UnitID,a.UnitName
                              FROM MW_CenterSpecDic a
                              LEFT JOIN MW_HospMakerDic b
                               ON a.CenterMatID = b.CenterMatID
                              LEFT JOIN MW_ProductDic c
                              ON b.ProductID = c.ProductID
                              LEFT JOIN MW_Storage d
                              ON b.MaterialID=d.MaterialID
                              WHERE a.IsStop = 0 AND b.IsStop = 0 AND d.DeptID={0}";
                return oleDb.GetDataTable(string.Format(strSql, deptID.ToString()));
            }
        }

        /// <summary>
        /// 查询物资入库批次ShowCard
        /// </summary>
        /// <param name="deptID">科室id</param>
        /// <returns>批次ShowCard数据源</returns>
        public DataTable GetBatchForInstoreShowCard(int deptID)
        {
            string strSql = @"SELECT ValidityTime,BatchNO,MaterialID FROM MW_Batch
                              WHERE DelFlag = 0 AND DeptID = {0}";
            strSql = string.Format(strSql, deptID.ToString());
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 出库明细单数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>出库单数据</returns>
        public DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT * FROM (
SELECT  sd.MaterialID ,
        h.MatName ,
        sd.UnitName,
        sd.Amount pAmount,
        cs.Spec ,
        sd.BatchNO ,
        sd.UnitID,
        dp.ProductName ,
        sd.Amount ,
        ds.Amount AS totalNum ,
        sd.RetailFee ,
        sd.RetailPrice ,
        sd.StockFee ,
        sd.StockPrice,
	    sd.RetailFee-sd.StockFee AS DifMoney,
		sd.OutHeadID,
        sd.OutDetailID,
        sd.ValidityDate,
		sd.WorkID,
        dh.DeptID,
        dh.ToDeptID
FROM    MW_OutStoreDetail sd
        left join MW_OutStoreHead dh on dh.OutStoreHeadID=sd.OutHeadID
        LEFT JOIN MW_Storage ds ON ds.MaterialID = sd.MaterialID AND dh.DeptID=ds.DeptID 
        INNER JOIN dbo.MW_HospMakerDic h ON h.MaterialID = sd.MaterialID
        INNER JOIN dbo.MW_CenterSpecDic cs ON cs.CenterMatID = h.CenterMatID
        INNER JOIN dbo.MW_ProductDic dp ON dp.ProductID = h.ProductID
		WHERE h.IsStop=0 AND cs.IsStop=0 and SD.WorkID={0}
		) AS t where 1=1 ", oleDb.WorkId);

            foreach (var pair in queryCondition)
            {
                stb.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
            }

            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 出库主表信息
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>出库数据</returns>
        public DataTable LoadOutStoreHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @" SELECT ds.*,db.BusiTypeName,ds.RetailFee-ds.StockFee AS DiffMoney   FROM MW_OutStoreHead ds
		LEFT JOIN DG_BusiType db 
		ON ds.BusiType=db.BusiCode ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, null));
            stb.Append(" order by AuditTime,RegTime  ");
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 加载主表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>查询出库主表</returns>
        public DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(@" SELECT  *
        FROM    ( SELECT    ds.* ,
                            db.BusiTypeName ,
                            ds.RetailFee-ds.StockFee AS DiffMoney,
                            d.Name,
                            bd.Name CurrentDept
                  FROM      MW_OutStoreHead ds
                            LEFT JOIN DG_BusiType db ON ds.BusiType = db.BusiCode
                            LEFT JOIN BaseDept d ON ds.ToDeptID = d.DeptId
                            LEFT JOIN BaseDept BD ON ds.DeptId=bd.DeptID
                  WHERE     DS.DelFlag = 0
                ) AS t
         WHERE  1 = 1 ");

            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    stb.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    stb.AppendFormat("AND {0}", pair.Value);
                }
            }

            stb.AppendFormat(" ORDER BY t.AuditTime, t.RegTime ");
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 科室的库存物资 showcard
        /// </summary>
        /// <param name="dept">科室id</param>
        /// <returns>科室的库存物资数据</returns>
        public DataTable GetStoreMWInFo(int dept)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT  s.MaterialID,
        s.Amount totalNum,
        h.MatName,
		h.WBCode,
		h.PYCode,
		pd.PYCode SPYCode,
		pd.WBCode SWBCode,
        c.Spec ,c.UnitID,c.UnitName,
        pd.ProductName ,
        db.StockPrice ,
        h.RetailPrice ,
		db.BatchNO,
        db.BatchAmount,
        db.ValidityTime
FROM    dbo.MW_Storage s
        INNER JOIN MW_HospMakerDic h ON s.MaterialID=h.MaterialID
        INNER JOIN dbo.MW_CenterSpecDic c ON h.CenterMatID=c.CenterMatID
        LEFT JOIN dbo.Mw_ProductDic pd ON pd.ProductID = h.ProductID
		LEFT JOIN dbo.MW_Batch db ON db.MaterialID=h.MaterialID AND db.DeptID=s.DeptID
	WHERE 1 = 1 AND s.DelFlag = 0 and h.IsStop=0 AND c.IsStop=0 AND db.DelFlag=0 AND s.DeptID={0} ", dept);
            return oleDb.GetDataTable(stb.ToString());
        }
        #endregion

        #region 库存查询
        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadMaterialStorage(Dictionary<string, string> condition, string typeId)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.MaterialID ,
		                                b.MatName,
                                        c.CenterMatName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount,
                                        a.Place ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID ,
                                        c.UnitName,
                                        a.DelFlag ,
                                        a.DeptID ,										
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    MW_Storage a
                                        INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                                        INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN MW_Location e ON a.LocationID=e.LocationID
                                WHERE  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND c.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere);
        }

        /// <summary>
        /// 查询库存信息（带批次）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>库存信息（带批次）</returns>
        public DataTable LoadMaterialStorages(Dictionary<string, string> condition, string typeId)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.MaterialID ,
		                                b.MatName,
                                        c.CenterMatName ,
                                        c.Spec ,
                                        d.ProductName ,
		                                a.Amount,
                                        a.Place ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitName ,
                                        f.BatchNO,
                                        f.ValidityTime,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    MW_Storage a
                                        INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                                        INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN MW_Location e ON a.LocationID=e.LocationID
                                        Left JOIN MW_Batch f ON a.StorageID=f.StorageID
                                WHERE  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND c.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询物资批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>物资批次信息列表</returns>
        public DataTable LoadMaterialBatch(int storageID)
        {
            string strSql = @"SELECT  0 ck,a.BatchID ,
                                        a.StorageID ,
                                        a.DeptID ,
                                        a.MaterialID ,
                                        c.CenterMatName ,
										b.MatName,
                                        a.BatchNO ,
                                        a.StockPrice ,
                                        b.RetailPrice ,
                                        a.InstoreTime ,
                                        a.BatchAmount ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.ValidityTime ,
                                        a.DelFlag ,
										ISNULL(a.StockPrice, 0)*ISNULL(a.BatchAmount, 0) as StockFee,
                                        ISNULL(b.RetailPrice, 0)*ISNULL(a.BatchAmount, 0) as RetailFee,
                                        (ISNULL(b.RetailPrice, 0)*ISNULL(a.BatchAmount, 0)
                                          - ISNULL(a.StockPrice, 0)*ISNULL(a.BatchAmount, 0)) as FeeDifference
                                FROM    MW_Batch a
                                        INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                                        INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                                WHERE   a.DelFlag = 0
                                        AND a.StorageID = {0}
                                        AND a.WorkID = {1}";
            strSql = string.Format(strSql, storageID, oleDb.WorkId);

            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 库存处理
        /// <summary>
        /// 获取最后一次结账记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>最后一次结账记录</returns>
        public MW_BalanceRecord GetMaxBlanceRecord(int deptID)
        {
            string strSql = @"SELECT TOP 1 * FROM MW_BalanceRecord WHERE DeptID = {0}
                            ORDER BY BalanceID DESC; ";
            return oleDb.Query<MW_BalanceRecord>(string.Format(strSql, deptID),string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 判断是否存在库存物资
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="materialID">物资ID</param>
        /// <param name="storageID">库存ID</param>
        /// <returns>是否存在</returns>
        public bool ExistStorage(int deptID, int materialID, out int storageID)
        {
            string strSql = "select StorageID from MW_Storage where DeptID={0} and MaterialID={1}";
            strSql = string.Format(strSql, deptID.ToString(), materialID.ToString());
            object obj = oleDb.GetDataResult(strSql);
            if (obj != null && obj != DBNull.Value)
            {
                storageID = Convert.ToInt32(obj);
                return true;
            }
            else
            {
                storageID = 0;
                return false;
            }
        }

        /// <summary>
        /// 增加药库库存
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="materialId">物资ID</param>
        /// <param name="amount">数量</param>
        /// <returns>返回状态:0正常；1库存不足；2数据库更新失败</returns>
        public bool AddStoreAmount(int deptID, int materialId, decimal amount)
        {
            string strSql = @"update MW_Storage 
                            set Amount=Amount+({0}) 
                            where DeptID={1} and MaterialID={2}";
            strSql = string.Format(strSql, amount, deptID, materialId);
            int rtn = oleDb.DoCommand(strSql);
            if (rtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 增加批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="materialId">物资ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <returns>返回状态</returns>
        public bool AddBatchAmount(int deptID, int materialId, string batchNO, decimal amount)
        {
            //更新库存
            string strSql = @"update MW_Batch set 
                           DelFlag=(case when BatchAmount+({0})=0 then 1 else 0 end),
                           BatchAmount=BatchAmount+({0})
                           where DeptID={1} and MaterialID={2} 
                           and BatchNO='{3}'";
            int rtn = oleDb.DoCommand(string.Format(strSql, amount, deptID, materialId, batchNO));
            return rtn > 0 ? true : false;
        }

        /// <summary>
        /// 获取批次库存量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="materialId">物资ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>批次库存量</returns>
        public MW_Batch GetBatchAmount(int deptID, int materialId, string batchNO)
        {
            string strSql = @"select * from MW_Batch 
                              where DeptID={0} and MaterialID={1}
                              and BatchNO='{2}'";
            strSql = string.Format(strSql, deptID, materialId, batchNO);
            return oleDb.Query<MW_Batch>(strSql,string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="deptID">科室id</param>
        /// <param name="materialId">物资id</param>
        /// <returns>库存实体对象</returns>
        public MW_Storage GetStorageInfo(int deptID, int materialId)
        {
            string strSql = @"select * from MW_Storage 
                              where DeptID={0} and MaterialID={1}";
            strSql = string.Format(strSql, deptID, materialId);
            return oleDb.Query<MW_Storage>(strSql,string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="materialId">物资ID</param>
        /// <returns>物资库存数量</returns>
        public decimal? GetStoreAmount(int deptID, int materialId)
        {
            string strSql = "select Amount from MW_Storage where DeptID={0} and MaterialID={1}";
            strSql = string.Format(strSql, deptID.ToString(), materialId.ToString());
            object obj = oleDb.GetDataResult(strSql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDecimal(obj);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 库位管理
        /// <summary>
        /// 获取库位详细信息
        /// </summary>
        /// <param name="locationid">库位id</param>
        /// <returns>库位详细信息</returns>
        public MW_Location GetLocationInfo(int locationid)
        {
            string strSql = "SELECT * FROM MW_Location WHERE LocationID={0}";
            return oleDb.Query<MW_Location>(string.Format(strSql, locationid),string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 通过名称获取库位信息
        /// </summary>
        /// <param name="parentid">父id</param>
        /// <param name="locationname">库位名称</param>
        /// <returns>库位实体对象</returns>
        public MW_Location GetLocationByName(int parentid, string locationname)
        {
            string strSql = "SELECT * FROM MW_Location WHERE ParentID={0} AND LocationName='{1}'";
            return oleDb.Query<MW_Location>(string.Format(strSql, parentid, locationname.Trim()), string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取库位列表信息
        /// </summary>
        /// <param name="deptid">可是id</param>
        /// <returns>库位列表数据</returns>
        public List<MW_Location> GetLocationList(int deptid)
        {
            string strSql = "SELECT * FROM MW_Location WHERE DeptID={0}";
            return oleDb.Query<MW_Location>(string.Format(strSql, deptid), string.Empty).ToList();
        }

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <param name="locationid">库位id</param>
        /// <param name="ids">库存id字符串</param>
        /// <param name="frmName">窗体入口</param>
        /// <returns>1成功</returns>
        public int UpdateStorage(int locationid, string ids, string frmName)
        {
            string strSql = "UPDATE MW_Storage SET LocationID={0} WHERE StorageID in ({1})";
            return oleDb.DoCommand(string.Format(strSql, locationid, ids));
        }
        #endregion

        #region 库存上下限设置

        /// <summary>
        /// 查询库存上下限数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>库存数据</returns>
        public DataTable GetStoreLimitData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT    a.StorageID ,
                                        a.MaterialID ,
                                        c.CenterMatName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount ,
                                        a.Place ,
                                        a.LocationID ,
                                        a.LStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.DelFlag ,
                                        a.DeptID
                                FROM    MW_Storage a
                                        INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                                        INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                                WHERE   a.DelFlag = 0  AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="details">库存数据列表</param>
        public void SaveStoreLimit(List<MW_Storage> details)
        {
            string strSql = string.Empty;
            foreach (MW_Storage s in details)
            {
                strSql = "UPDATE MW_Storage SET UpperLimit={0},LowerLimit={1} WHERE StorageID={2}";
                strSql = string.Format(strSql, s.UpperLimit, s.LowerLimit, s.StorageID);
                oleDb.DoCommand(strSql);
            }
        }

        /// <summary>
        /// 查询物资ShowCard
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>物资ShowCard数据源</returns>
        public DataTable GetMaterialDicShowCard(bool isRet, int deptID)
        {
            if (!isRet)
            {
                string strSql = @"SELECT b.MaterialID,a.CenterMatName,b.MatName,b.MatCode,b.AliasName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,
                              a.UnitID,a.UnitName,a.TypeID,b.StockPrice,b.RetailPrice
                              FROM MW_CenterSpecDic a
                              LEFT JOIN MW_HospMakerDic b
                              ON a.CenterMatID = b.CenterMatID
                              LEFT JOIN MW_ProductDic c
                              ON b.ProductID = c.ProductID
                              WHERE a.IsStop = 0 AND b.IsStop = 0";
                return oleDb.GetDataTable(strSql);
            }
            else
            {
                string strSql = @"SELECT b.MaterialID,a.CenterMatName,b.MatName,b.MatCode,b.AliasName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,
                              a.UnitID,a.UnitName,a.TypeID,b.StockPrice,b.RetailPrice
                              FROM MW_CenterSpecDic a
                              LEFT JOIN MW_HospMakerDic b
                              ON a.CenterMatID = b.CenterMatID
                              LEFT JOIN MW_ProductDic c
                              ON b.ProductID = c.ProductID
                              LEFT JOIN MW_Storage d
                              ON b.MaterialID=d.MaterialID
                              WHERE a.IsStop = 0 AND b.IsStop = 0 AND d.DeptID={0}";
                return oleDb.GetDataTable(string.Format(strSql, deptID.ToString()));
            }
        }
        #endregion

        #region 月结
        /// <summary>
        /// 月结记录
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>月结数据</returns>
        public DataTable GetMonthBalaceByDept(int deptId)
        {
            string strSql = @"SELECT * FROM MW_BalanceRecord WHERE DeptID = {0}
                            ORDER BY BalanceID DESC; ";
            return oleDb.GetDataTable(string.Format(strSql, deptId));
        }

        /// <summary>
        /// 执行月结
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="deptId">科室id</param>
        /// <param name="empId">员工id</param>
        /// <returns>执行结果对象</returns>
        public MWBillResult ExcutMonthBalance(int workId, int deptId, int empId)
        {
            MWBillResult result = new MWBillResult();
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_MW_MonthBalance");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@DeptID", DbType.Int32, deptId);
                oleDb.AddInParameter(cmd as DbCommand, "@EmpId", DbType.Int32, empId);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                oleDb.DoCommand(cmd);

                string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();

                result.Result = Convert.ToInt32(code);
                result.ErrMsg = msg;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 2;
                result.ErrMsg = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="deptId">科室id</param>
        /// <returns>对账结果id</returns>
        public MWSpResult ExcutSystemCheckAccount(int workId, int deptId)
        {
            MWSpResult result = new MWSpResult();
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_MW_CheckAccount");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@DeptID", DbType.Int32, deptId);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                oleDb.DoCommand(cmd);

                string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = (SqlCommand)cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);

                result.Result = Convert.ToInt32(code);
                result.ErrMsg = msg;
                result.Table = dt;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 2;
                result.ErrMsg = ex.Message;
                return result;
                throw;
            }
        }
        #endregion

        #region 采购计划
        /// <summary>
        /// 查询采购计划头表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>采购计划单</returns>
        public DataTable GetPlanHeadData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT a.PlanHeadID,
                              a.RegTime,
                              a.RegEmpName,
                              a.RegEmpID,
                              a.RetailFee,
                              a.StockFee,
                              a.UpdateTime,
                              a.DeptID,
	                          b.Name AS deptname,
                              a.WorkID,
                              a.AuditFlag,
	                          (CASE a.AuditFlag WHEN 0 THEN '未审' WHEN 1 THEN '审核' END) AS AuditFlagName,
                              a.AuditTime,
                              a.AuditEmpID,
                              a.AuditEmpName,
                              a.Remark
                          FROM MW_PlanHead a LEFT JOIN BaseDept b ON a.DeptID=b.DeptId
                          WHERE a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql = strSql + strWhere.ToString() + " order by a.RegTime desc";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 查询采购计划单据明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>采购计划明细单</returns>
        public DataTable GetPlanDetailData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"
                              SELECT    a.MaterialID ,
                                        c.CenterMatName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.RetailPrice ,
                                        a.StockPrice ,
                                        a.StockFee ,
                                        a.RetailFee ,
                                        a.PlanDetailID ,
                                        a.PlanHeadID 
                              FROM      MW_PlanDetail a
                                        LEFT JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        LEFT JOIN MW_CenterSpecDic c ON b.CenterMatID = c.CenterMatID
                                        LEFT JOIN MW_ProductDic d ON b.ProductID = d.ProductID
                                    WHERE     b.IsStop = 0
                                        AND c.IsStop = 0
			                            AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            DataTable dt = oleDb.GetDataTable(strSql + strWhere.ToString());
            return dt;
        }

        /// <summary>
        /// 删除采购计划单 
        /// </summary>
        /// <param name="billID">采购计划单表头Id</param>
        /// <returns>1成功</returns>
        public bool DeleteMWPlanBill(int billID)
        {
            bool boolRtn = true;
            //删除采购计划单明细表
            string strSql = @"DELETE FROM MW_PlanDetail WHERE PlanHeadID={0} AND WorkID={1}";
            strSql = string.Format(strSql, billID, oleDb.WorkId);
            int intRtn = oleDb.DoCommand(strSql);
            if (intRtn < 0)
            {
                boolRtn = false;
            }
            else
            {
                boolRtn = true;
            }

            //删除采购计划单头表
            strSql = @"DELETE FROM dbo.MW_PlanHead WHERE PlanHeadID={0} AND WorkID={1}";
            strSql = string.Format(strSql, billID, oleDb.WorkId);
            intRtn = oleDb.DoCommand(strSql);
            if (intRtn < 0)
            {
                boolRtn = false;
            }
            else
            {
                boolRtn = true;
            }

            return boolRtn;
        }

        /// <summary>
        /// 取得小于下限的库存物资
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>物资数据</returns>
        public DataTable GetLessLowerLimitData(int deptId)
        {
            string strSql = @"SELECT  b.MaterialID ,
                                a.CenterMatName ,
                                b.AliasName ,
                                a.Spec ,
                                b.ProductID ,
                                c.ProductName ,
                                a.UnitID ,
                                a.UnitName ,
                                b.StockPrice ,
                                b.RetailPrice,
								(ISNULL(w.LowerLimit,0) - ISNULL(w.Amount,0)) AS BuyAmount 
                        FROM    MW_CenterSpecDic a 
                                LEFT JOIN MW_HospMakerDic b ON a.CenterMatID = b.CenterMatID
                                LEFT JOIN MW_ProductDic c ON b.ProductID = c.ProductID		                       
								RIGHT JOIN  MW_Storage w ON w.MaterialID=b.MaterialID
                        WHERE w.DelFlag=0 AND ISNULL(w.Amount,0)<ISNULL(w.LowerLimit,0)
						      AND w.DeptID={0} AND w.WorkID={1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得小于上限的库存物资
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>物资数据</returns>
        public DataTable GetLessUpperLimitData(int deptId)
        {
            string strSql = @"SELECT  b.MaterialID ,
                                a.CenterMatName ,
                                b.AliasName ,
                                a.Spec ,
                                b.ProductID ,
                                c.ProductName ,
                                a.UnitID ,
                                a.UnitName ,
                                b.StockPrice ,
                                b.RetailPrice,
								(ISNULL(w.UpperLimit,0) - ISNULL(w.Amount,0)) AS BuyAmount 
                        FROM     MW_CenterSpecDic a 
                                LEFT JOIN MW_HospMakerDic b ON a.CenterMatID = b.CenterMatID
                                LEFT JOIN MW_ProductDic c ON b.ProductID = c.ProductID		                       
								RIGHT JOIN  MW_Storage w ON w.MaterialID=b.MaterialID
                        WHERE w.DelFlag=0 AND ISNULL(w.Amount,0)<ISNULL(w.UpperLimit,0)
						      AND w.DeptID={0} AND w.WorkID={1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年数据</returns>
        public DataTable GetAcountYears(int deptId)
        {
            DataTable dt = new DataTable();
            MW_BalanceRecord record = NewDao<IMWDao>().GetMaxBlanceRecord(deptId);
            if (record != null)
            {
                string strSql = @"SELECT DISTINCT BalanceYear AS ID,BalanceYear AS Name FROM MW_BalanceRecord WHERE DeptID={0} ORDER BY BalanceYear DESC";
                strSql = string.Format(strSql, deptId);
                dt = oleDb.GetDataTable(strSql);
                //if (System.DateTime.Now >= record.EndTime)
                //{
                //    if (record.BalanceMonth == 12)
                //    {
                //        DataRow row = dt.NewRow();
                //        row["ID"] = record.BalanceYear + 1;
                //        row["Name"] = record.BalanceYear + 1;
                //        dt.Rows.InsertAt(row, 0);
                //    }
                //}
            }

            return dt;
        }

        /// <summary>
        /// 获取会计月
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <returns>会计月数据</returns>
        public DataTable GetAcountMonths(int deptId, int year)
        {
            DataTable dt = new DataTable();
            string strSql = @"SELECT DISTINCT BalanceMonth AS ID,BalanceMonth AS Name FROM MW_Account WHERE DeptID={0} AND BalanceYear={1} ORDER BY BalanceMonth";
            strSql = string.Format(strSql, deptId, year);
            dt = oleDb.GetDataTable(strSql);
            if (dt != null && dt.Rows.Count == 0)
            {
                DataRow row = dt.NewRow();
                row["ID"] = DateTime.Now.Month;
                row["Name"] = DateTime.Now.Month;
                dt.Rows.InsertAt(row, 0);
            }

            //if (DateTime.Now.Year == year)
            //{
            //    DataRow row = dt.NewRow();
            //    if (dt.Select("ID=" + DateTime.Now.Month.ToString()).Length == 0)
            //    {
            //        row["ID"] = DateTime.Now.Month;
            //        row["Name"] = DateTime.Now.Month;
            //        dt.Rows.Add(row);
            //    }
            //}

            return dt;
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>明细账数据</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId)
        {
            string flag = "0";

            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    MW_Account a LEFT JOIN MW_HospMakerDic ho ON a.MaterialID=ho.MaterialID
								        LEFT JOIN MW_CenterSpecDic cs ON cs.CenterMatID=ho.CenterMatID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} ";
            if (accountType == 2)
            {
                //判断是否月结
                string sql = "SELECT DISTINCT BalanceFlag FROM MW_Account WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
                sql = string.Format(sql, deptId, queryYear, queryMonth);
               DataTable dtBalanceFlag =  oleDb.GetDataTable(sql);
                if (dtBalanceFlag.Rows.Count > 0)
                {
                    flag = dtBalanceFlag.Rows[0][0].ToString();

                    //计算
                    if (flag == "0")
                    {
                        strSql = @"SELECT a.BatchAmount*ho.RetailPrice AS OverRetailFee ,
                                           a.BatchAmount*a.StockPrice as OverStockFee
                                    FROM    dbo.MW_Batch a
                                            LEFT JOIN MW_HospMakerDic ho ON a.MaterialID = ho.MaterialID
                                            LEFT JOIN MW_CenterSpecDic cs ON cs.CenterMatID = ho.CenterMatID
                                WHERE  a.DeptID={0} ";
                        if (typeId == 0)
                        {
                            strSql = string.Format(strSql, deptId);
                        }
                        else
                        {
                            strSql = strSql + " and cs.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(typeId, 1) + " )";
                            strSql = string.Format(strSql, deptId, typeId);
                        }

                        return oleDb.GetDataTable(strSql);
                    }
                }
            }

            if (typeId == 0)
            {
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId);
            }
            else
            {
                strSql = strSql + " and cs.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(typeId, 4) + " )";
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId);
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="typeId">类型id</param>
        /// <returns>明细账数据</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, string busiCode, int typeId)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    MW_Account a LEFT JOIN MW_HospMakerDic ho ON a.MaterialID=ho.MaterialID
								        LEFT JOIN MW_CenterSpecDic cs ON cs.CenterMatID=ho.CenterMatID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} and a.BusiType='{4}'";
            if (typeId == 0)
            {
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, busiCode);
            }
            else
            {
                strSql = strSql + " and cs.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(typeId, 5) + " )";
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, busiCode, typeId);
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="busiTypeModel">业务类型实体</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>明细账数据</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, string busiCode, DGBusiType busiTypeModel, int typeId)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee,
                               " + busiTypeModel.DeptFieldName + @"
                                FROM    MW_Account a LEFT JOIN MW_HospMakerDic ho ON a.MaterialID=ho.MaterialID
								        LEFT JOIN MW_CenterSpecDic cs ON cs.CenterMatID=ho.CenterMatID
                                        left join " + busiTypeModel.DetailTableName + " on a.DetailID=" + busiTypeModel.DetailIdFieldName + @"
                                        left join " + busiTypeModel.HeadTableName + " on " + busiTypeModel.JoinExpress + @"
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3}  and a.BusiType='{4}'";
            if (typeId == 0)
            {
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, busiCode);
            }
            else
            {
                strSql = strSql + " and cs.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(typeId, 5) + " )";
                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, busiCode, typeId);
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得物资类型查询字段表达式
        /// </summary>
        /// <param name="typeId">物资类型</param>
        /// <param name="seq">参数顺序</param>
        /// <returns>物资类型查询字段表达式</returns>
        private string GetMaterialTypeQueryConditionString(int typeId, int seq)
        {
            string condition = string.Empty;
            MW_TypeDic typeMode = (MW_TypeDic)NewObject<MW_TypeDic>().getmodel(typeId);
            if (typeMode.Level == 1)
            {
                condition = "firsttypeid ={" + seq + "}";
            }
            else if (typeMode.Level == 2)
            {
                condition = "SecondTypeId ={" + seq + "}";
            }
            else if (typeMode.Level == 3)
            {
                condition = "ThirdTypeId ={" + seq + "}";
            }

            return condition;
        }
        #endregion

        #region 物资明细查询
        /// <summary>
        /// 获取会计月日期
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>会计月日期数据</returns>
        public DataTable GetBalanceDate(int deptId, int year, int month)
        {
            string strSql = @"SELECT BeginTime,EndTime FROM MW_BalanceRecord WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
            strSql = string.Format(strSql, deptId, year, month);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得物资明细账数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">物资Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>物资明细账数据</returns>
        public DataTable GetAccountDetail(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType)
        {
            string dateWhere = string.Empty;
            string strSql = @"SELECT  AccountID ,
                            (CASE AccountType WHEN 0  THEN '发生' WHEN 1 THEN '期初' WHEN 2 THEN '期末' WHEN 3 THEN '调整' END) as AccountType ,
                            b.BusiTypeName AS OpType ,
                            BusiType ,
                            BalanceYear ,
                            BalanceMonth ,
                            RetailPrice ,
                            StockPrice ,
                            BatchNO ,
                            BillNO ,
                            LendAmount ,
                            LendRetailFee ,
                            LendStockFee ,
                            DebitAmount ,
                            DebitRetailFee ,
                            DebitStockFee ,
                            OverAmount ,
                            OverRetailFee ,
                            OverStockFee ,
                            RegTime ,
                            BalanceFlag ,
                            BalanceID ,
                            MaterialID ,
                           
                            DeptID ,
                            DetailID,
		                    UnitName
                    FROM    MW_Account a
                            LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
		                    WHERE DeptID={0} AND MaterialID={1} and WorkID={2} ";
            strSql = string.Format(strSql, deptId, drugId, oleDb.WorkId);
            if (queryType == 1)
            {
                dateWhere = " AND RegTime BETWEEN '{0}' AND '{1}' ";
                dateWhere = string.Format(dateWhere, beginTime, endTime);
            }
            else
            {
                dateWhere = " AND BalanceMonth={0} AND BalanceYear={1} ";
                dateWhere = string.Format(dateWhere, month, year);
            }

            strSql = strSql + dateWhere + " order by RegTime desc";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 取得物资明细账汇总数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">物资Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>物资明细账汇总数据</returns>
        public DataTable GetAccountDetailSum(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType)
        {
            string dateWhere = string.Empty;
            string strSql = @"SELECT    SUM(ISNULL(OverAmount,0)) - SUM(ISNULL(LendAmount,0)) + SUM(ISNULL(DebitAmount,0)) as OpenAmount,
                                        SUM(ISNULL(OverRetailFee,0)) -SUM(ISNULL(LendRetailFee,0))+SUM(ISNULL(DebitRetailFee,0)) as OpenRetailFee,
                                        SUM(ISNULL(LendAmount,0)) AS LendAmount ,
                                        SUM(ISNULL(LendRetailFee,0)) AS LendRetailFee,
                                        SUM(ISNULL(DebitAmount,0)) AS DebitAmount ,
                                        SUM(ISNULL(DebitRetailFee,0)) AS DebitRetailFee,
                                        SUM(ISNULL(OverAmount,0)) AS OverAmount,
                                        SUM(ISNULL(OverRetailFee,0)) OverRetailFee
                    FROM    MW_Account a
                            LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
		                    WHERE DeptID={0} AND MaterialID={1} and WorkID={2} ";
            strSql = string.Format(strSql, deptId, drugId, oleDb.WorkId);
            if (queryType == 1)
            {
                dateWhere = " AND RegTime BETWEEN '{0}' AND '{1}' ";
                dateWhere = string.Format(dateWhere, beginTime, endTime);
            }
            else
            {
                dateWhere = " AND BalanceMonth={0} AND BalanceYear={1} ";
                dateWhere = string.Format(dateWhere, month, year);
            }

            strSql = strSql + dateWhere + " GROUP BY MaterialID";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }
        #endregion

        #region 物资汇总
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>入库业务数据</returns>
        public DataTable GetInStore(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT a.MaterialID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.UnitName,a.SupplierName as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.StockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.Amount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  b.SupplierName,a.MaterialID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.WorkID from MW_InStoreDetail a LEFT JOIN MW_InStoreHead b ON a.InHeadID=b.InHeadID 
              WHERE b.DelFlag=0  Group By   b.SupplierName,a.MaterialID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.StockPrice,a.RetailPrice,a.WorkID) a   
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by a.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>出库业务数据</returns>
        public DataTable GetOutStore(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT a.MaterialID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.UnitName,a.ToDeptName as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.StockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.Amount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  b.ToDeptName,a.MaterialID,b.BusiType,b.BillTime,a.DeptID,b.ToDeptID,a.WorkID from MW_OutStoreDetail a LEFT JOIN MW_OutStoreHead b ON a.OutHeadID=b.OutStoreHeadID WHERE b.DelFlag=0  Group By b.ToDeptName,a.MaterialID,b.BusiType,b.BillTime,a.DeptID,b.ToDeptID,a.StockPrice,a.RetailPrice,a.WorkID) a 
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by a.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型id</param>
        /// <returns>盘点业务数据</returns>
        public DataTable GetCheck(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT a.MaterialID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.UnitName,'' as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.FactStockFee-a.ActStockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.FactRetailFee-a.ActRetailFee) as RetailFee,sum(a.FactAmount-a.ActAmount) as UnitAmount,sum((a.FactRetailFee-a.ActRetailFee)-(a.FactStockFee-a.ActStockFee)) as Price,
			  a.MaterialID,b.BusiType,b.AuditTime,a.DeptID,a.WorkID from MW_AuditDetail a LEFT JOIN MW_AuditHead b on a.AuditHeadID=b.AuditHeadID  WHERE b.DelFlag=0  Group By  a.MaterialID,b.BusiType,b.AuditTime,a.DeptID,a.StockPrice,a.RetailPrice,a.WorkID) a 
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by a.AuditTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>调价业务数据</returns>
        public DataTable GetAdjPrice(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice,a.StockFee as StockFee,a.NewRetailPrice,a.AdjRetailFee,a.AdjAmount,d.UnitName,a.DrugID,'' as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,a.Price as Price,g.BusiTypeName 
		      FROM (select 
			  f.StockPrice as StockPrice,sum(f.StockPrice*a.AdjAmount) as StockFee,a.NewRetailPrice as NewRetailPrice,sum(a.AdjRetailFee) as AdjRetailFee,
			  sum(a.AdjAmount) as AdjAmount,sum(a.AdjRetailFee-(f.StockPrice*a.AdjAmount)) as Price,
			  b.BusiType,a.DeptID,a.WorkID,b.ExecTime,a.DrugID 
			  from DG_AdjDetail a LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID LEFT JOIN MW_Batch f ON a.BatchNO=f.BatchNO WHERE b.DelFlag=0  Group By  b.BusiType,a.DeptID,a.WorkID,b.ExecTime,a.NewRetailPrice,f.StockPrice,a.DrugID) a
              LEFT JOIN MW_HospMakerDic c ON a.DrugID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType g ON a.BusiType=g.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by a.ExecTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }
        #endregion

        #region 物资流水账
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>入库业务数据</returns>
        public DataTable GetInStores(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT b.BillTime,a.BillNo,a.MaterialID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.UnitName,b.SupplierName as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM MW_InStoreDetail a 
              LEFT JOIN MW_InStoreHead b ON a.InHeadID=b.InHeadID
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by b.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>出库业务数据</returns>
        public DataTable GetOutStores(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT b.BillTime,a.BillNo,a.MaterialID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.UnitName as UnitName,b.ToDeptName as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM MW_OutStoreDetail a 
              LEFT JOIN MW_OutStoreHead b on a.OutHeadID=b.OutStoreHeadID 
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by b.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资类型</param>
        /// <returns>盘点业务数据</returns>
        public DataTable GetChecks(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT b.AuditTime as BillTime,a.BillNo,a.MaterialID,a.StockPrice,(a.FactStockFee-a.ActStockFee) as StockFee,a.RetailPrice,(a.FactRetailFee-a.ActRetailFee) as RetailFee,(a.FactAmount-a.ActAmount) as UnitAmount,d.UnitName,'' as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,((a.FactRetailFee-a.ActRetailFee)-(a.FactStockFee-a.ActStockFee)) as Price,f.BusiTypeName FROM MW_AuditDetail a 
              LEFT JOIN MW_AuditHead b on a.AuditHeadID=b.AuditHeadID 
              LEFT JOIN MW_HospMakerDic c ON a.MaterialID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by b.AuditTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">物资id</param>
        /// <returns>调价业务数据</returns>
        public DataTable GetAdjPrices(Dictionary<string, string> query, string typeId)
        {
            string strSql = @"SELECT b.ExecTime,a.BillNo,a.DrugID,f.StockPrice,(f.StockPrice*a.AdjAmount) as StockFee,a.NewRetailPrice,a.AdjRetailFee,a.AdjAmount,d.UnitName,a.DrugID,'' as DeptName,c.MatName,d.CenterMatName,d.Spec,e.ProductName,(a.AdjRetailFee-(f.StockPrice*a.AdjAmount)) as Price,g.BusiTypeName FROM DG_AdjDetail a 
              LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID 
              LEFT JOIN MW_HospMakerDic c ON a.DrugID=c.MaterialID 
              LEFT JOIN MW_CenterSpecDic d ON c.CenterMatID=d.CenterMatID
              LEFT JOIN MW_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN MW_Batch f ON a.BatchNO=f.BatchNO
              LEFT JOIN DG_BusiType g ON b.BusiType=g.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "0")
                {
                    strWhere.AppendFormat(" AND d.TypeID in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE " + GetMaterialTypeQueryConditionString(Convert.ToInt32(typeId), 0) + " )", typeId);
                }
            }

            strWhere.Append(" order by b.ExecTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }
        #endregion
        #endregion

        #region 库存盘点
        /// <summary>
        /// 查询盘点单表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单表头</returns>
        public DataTable LoadCheckHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT
                               CheckHeadID,
                               BillNO,
                               RegEmpID,
                               RegEmpName,
                               RegTime,
                               AuditEmpID,
                               AuditEmpName,
                               AuditTime,
                               Remark,
                               DelFlag,
                               AuditFlag,
                               BusiType,
                               DeptID,
                               AuditHeadID,
                               AuditNO,
                               WorkID
                               FROM MW_CheckHead
                               WHERE DelFlag=0 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql = strSql + strWhere.ToString() + " order by BillNO";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询盘点单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单明细</returns>
        public DataTable LoadCheckDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT 
                               a.CheckDetailID,
                               a.StorageID,
                               a.Place,
                               a.DeptID,
                               a.MaterialID,
                               a.BillNO,
                               a.FactAmount,
                               a.FactStockFee,
                               a.FactRetailFee,
                               a.ActAmount,
                               a.ActStockFee,
                               a.ActRetailFee,
                               a.UnitID,
                               a.UnitName,
                               a.AuditFlag,
                               a.BillTime,
                               a.RetailPrice,
                               a.StockPrice,
                               a.CheckHeadID,
                               a.BatchNO,
                               a.ValidityDate,
                               a.WorkID,
                               c.CenterMatName ,
                               c.Spec ,
                               d.ProductName,
							   g.TypeName,
                               '' as ActAmountShow,
                               '' as FactAmountShow
                            FROM MW_CheckDetail a
                                INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID                              
								LEFT JOIN mw_TypeDic  g ON c.TypeID=g.TypeID
                                INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                            WHERE a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 取得药库盘点物资选择卡片数据
        /// </summary>
        /// <param name="deptID">药库ID</param>
        /// <returns>物资字典</returns>
        public DataTable GetDrugDicForCheckShowCard(int deptID)
        {
            string strSql = @"SELECT    b.MaterialID ,
                                        a.CenterMatName ,
										a.CenterMatCode,
										b.AliasName,
										b.MatName,
										b.MatCode,
                                        a.Spec ,
                                        b.ProductID ,
                                        c.ProductName ,
                                        a.PYCode ,
                                        a.WBCode ,
                                        b.PYCode AS TPYCode ,
                                        b.WBCode AS TWBCode ,
                                        a.TypeID,
                                        e.StockPrice ,
                                        b.RetailPrice ,
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place,
		                                d.Amount,
		                                e.BatchNO,
		                                e.ValidityTime,
                                        e.BatchAmount,
										g.TypeName,
										a.UnitID,
										a.UnitName
                                FROM    MW_CenterSpecDic a
                                        LEFT JOIN MW_HospMakerDic b ON a.CenterMatID = b.CenterMatID
                                        LEFT JOIN MW_ProductDic c ON b.ProductID = c.ProductID
            							LEFT JOIN MW_TypeDic  g ON a.TypeID=g.TypeID
                                        RIGHT JOIN MW_Storage d ON b.MaterialID = d.MaterialID
		                                LEFT JOIN MW_Batch e ON d.StorageID=e.StorageID
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0
		                                AND e.DelFlag =0
                                        AND d.DeptID = {0} and d.WorkID={1}";
            return oleDb.GetDataTable(string.Format(strSql, deptID.ToString(), oleDb.WorkId));
        }

        /// <summary>
        /// 提取库存物资
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT    b.MaterialID ,
                                        a.CenterMatName ,
										a.CenterMatCode,
										b.AliasName,
										b.MatName,
										b.MatCode,
                                        a.Spec ,
                                        b.ProductID ,
                                        c.ProductName ,
                                        a.PYCode ,
                                        a.WBCode ,
                                        b.PYCode AS TPYCode ,
                                        b.WBCode AS TWBCode ,
                                        a.TypeID,
                                        e.StockPrice ,
                                        b.RetailPrice ,
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place,
		                                d.Amount,
		                                e.BatchNO,
		                                e.ValidityTime,
                                        e.BatchAmount,
										g.TypeName,
										a.UnitID,
										a.UnitName
                                FROM    MW_CenterSpecDic a
                                        LEFT JOIN MW_HospMakerDic b ON a.CenterMatID = b.CenterMatID
                                        LEFT JOIN MW_ProductDic c ON b.ProductID = c.ProductID
            							LEFT JOIN MW_TypeDic  g ON a.TypeID=g.TypeID
                                        RIGHT JOIN MW_Storage d ON b.MaterialID = d.MaterialID
		                                LEFT JOIN MW_Batch e ON d.StorageID=e.StorageID
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0
		                                AND e.DelFlag =0
		                                AND e.DelFlag =0 and d.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key == "a.TypeID")
                {
                    strWhere.AppendFormat(" AND {0} in (SELECT ThirdTypeId FROM V_MW_MaterialType WHERE firsttypeid={1} )", pair.Key, pair.Value);
                }
                else
                {
                    if (pair.Key != string.Empty)
                    {
                        strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat("AND {0}", pair.Value);
                    }
                }
            }

            strWhere.AppendFormat(" order by d.Place");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        public DataTable LoadAudtiCheckHead(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT 
                               AuditHeadID,
                               BillNO,
                               EmpID,
                               EmpName,
                               AuditTime,
                               Remark,
                               DelFlag,
                               AuditFlag,
                               BusiType,
                               DeptID,
                               WorkID,
                               ProfitRetailFee,
                               ProfitStockFee,
                               LossRetailFee,
                               LossStockFee,
                               CheckStockFee,
                               ActStockFee,
                               CheckRetailFee,
                               ActRetailFee
                            FROM MW_AuditHead
                            WHERE DelFlag=0 AND AuditFlag=1 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql = strSql + strWhere.ToString() + " order by BillNO";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        public DataTable LoadAuditCheckDetail(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT   a.AuditDetailID ,
                                        a.StorageID ,
                                        a.MaterialID ,
                                        a.Place ,
                                        a.BatchNO ,
                                        a.ValidityDate ,
                                        a.DeptID ,
                                        a.BillNO ,
                                        a.FactAmount ,
                                        a.FactStockFee ,
                                        a.FactRetailFee ,
                                        a.ActAmount ,
                                        a.ActStockFee ,
                                        a.ActRetailFee ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.RetailPrice ,
                                        a.StockPrice ,
                                        a.AuditHeadID,
		                                c.CenterMatName,
		                                c.Spec,
                                        c.TypeID,
		                                d.ProductName,
                                        (a.FactRetailFee - a.ActRetailFee)  as DIFFFEE,
                                        (a.FactStockFee - a.ActStockFee) as DIFFTRADEFEE,
                                         a.ActAmount as BASENUM,
                                        a.UnitName as MiniUnit,
                                         a.FactAmount as CBASENUM,
                                         m.TypeName,
                                         (a.FactAmount - a.ActAmount) as DiffNum
                                FROM    MW_AuditDetail a
                                        LEFT JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                                        LEFT JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                                        LEFT JOIN MW_TypeDic m ON c.TypeID=m.TypeID 
                                        LEFT JOIN MW_ProductDic d ON b.ProductID = d.ProductID where 1=1 ";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            if (condition != null)
            {
                foreach (var pair in condition)
                {
                    if (pair.Key != string.Empty)
                    {
                        strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat("AND {0}", pair.Value);
                    }
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        public DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO)
        {
            string strSql = string.Empty;

            //按批次号汇总
            if (haveBatchNO) 
            {
                strSql = @"SELECT                                
                            a.StorageID,
                            a.Place,
                            a.DeptID,                     
                            a.MaterialID,
                            SUM(a.FactAmount) AS FactAmount,
                            SUM(a.FactStockFee) AS FactStockFee,
                            SUM(a.FactRetailFee) AS FactRetailFee,
                            a.ActAmount,
                            a.ActStockFee,
                            a.ActRetailFee,
                            a.UnitID,
                            a.UnitName,
                            a.RetailPrice,
                            a.StockPrice,
                            a.BatchNO,
                            a.ValidityDate,
                            c.CenterMatName ,
                            c.Spec ,
                            d.ProductName,
                            g.TypeName
                            FROM MW_CheckHead h LEFT JOIN MW_CheckDetail a ON h.CheckHeadID=a.CheckHeadID
                            INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                            INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                            LEFT JOIN MW_TypeDic  g ON c.TypeID=g.TypeID
                            INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                            WHERE a.WorkID={0} AND h.AuditFlag=0 AND h.DelFlag=0 ";
                strSql = string.Format(strSql, oleDb.WorkId);
                StringBuilder strWhere = new StringBuilder();
                foreach (var pair in condition)
                {
                    if (pair.Key != string.Empty)
                    {
                        strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat("AND {0}", pair.Value);
                    }
                }

                string strGroupBy = @" GROUP BY 
                                                a.StorageID, a.Place,a.DeptID,a.MaterialID,
                                                a.UnitID,a.UnitName,a.RetailPrice,a.StockPrice,
                                                a.ValidityDate,c.CenterMatName ,c.Spec ,d.ProductName,
                                                g.TypeName,a.BatchNO, a.ActAmount,
                                                a.ActStockFee,a.ActRetailFee";
                strSql = strSql + strWhere.ToString() + strGroupBy;
            }
            else
            {
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取库房盘点状态
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>盘点状态</returns>
        public int GetStoreRoomStatus(int deptId)
        {
            int status = 0;
            DataTable dt =  NewObject<MW_DeptDic>().gettable("DeptID=" + deptId + " and WorkID=" + oleDb.WorkId);
            if (dt != null && dt.Rows.Count > 0)
            {
                status = Convert.ToInt32(dt.Rows[0]["CheckStatus"]);
            }

            return status;
        }

        /// <summary>
        /// 取得所有未审核的盘点汇总明细
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>未审核盘点汇总明细</returns>
        public DataTable GetAllNotAuditDetail(int deptId)
        {
            string strSql = string.Empty;

            strSql = @"SELECT                                
                            a.StorageID,
                            a.Place,
                            a.DeptID,                     
                            a.MaterialID,
                            SUM(a.FactAmount) AS FactAmount,
                            SUM(a.FactStockFee) AS FactStockFee,
                            SUM(a.FactRetailFee) AS FactRetailFee,
                            a.ActAmount,
                            a.ActStockFee,
                            a.ActRetailFee,
                            a.UnitID,
                            a.UnitName,
                            a.RetailPrice,
                            a.StockPrice,
                            a.BatchNO,
                            a.ValidityDate,
                            c.CenterMatName ,
                            c.Spec ,
                            d.ProductName,
                            g.TypeName
                            FROM MW_CheckHead h LEFT JOIN MW_CheckDetail a ON h.CheckHeadID=a.CheckHeadID
                            INNER JOIN MW_HospMakerDic b ON a.MaterialID = b.MaterialID
                            INNER JOIN MW_CenterSpecDic c ON c.CenterMatID = b.CenterMatID
                            LEFT JOIN MW_TypeDic  g ON c.TypeID=g.TypeID
                            INNER JOIN MW_ProductDic d ON d.ProductID = b.ProductID
                            WHERE a.WorkID={0} AND h.AuditFlag=0 AND h.DelFlag=0 and a.DeptID={1}";
            strSql = string.Format(strSql, oleDb.WorkId, deptId);

            string strGroupBy = @" GROUP BY 
                                                a.StorageID, a.Place,a.DeptID,a.MaterialID,
                                                a.UnitID,a.UnitName,a.RetailPrice,a.StockPrice,
                                                a.ValidityDate,c.CenterMatName ,c.Spec ,d.ProductName,
                                                g.TypeName,a.BatchNO, a.ActAmount,
                                                a.ActStockFee,a.ActRetailFee";
            strSql = strSql + strGroupBy;

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 更新盘点头表审核状态信息
        /// </summary>
        /// <param name="head">盘点头表</param>
        /// <returns>小于0失败</returns>
        public int UpdateCheckHeadStatus(MW_CheckHead head)
        {
            string strSql =  @"UPDATE MW_CheckHead set AuditEmpID={0},AuditEmpName='{1}',AuditTime=GETDATE(),AuditFlag=1,AuditHeadID={2},AuditNO={3} WHERE DelFlag=0 AND AuditFlag=0 AND DeptID={4} AND WorkID={5}";
            strSql = string.Format(strSql, head.AuditEmpID, head.AuditEmpName, head.AuditHeadID, head.AuditNO, head.DeptID, oleDb.WorkId);
            return oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 取得物资一级类型
        /// </summary>
        /// <returns>物资类型</returns>
        public DataTable GetMaterialType()
        {
            string strSql = @"SELECT TypeID,ParentID,TypeName,PYCode,WBCode,Level FROM MW_TypeDic WHERE [Level]=1";
            DataTable dtRtn = oleDb.GetDataTable(strSql);
            return dtRtn;
        }

        /// <summary>
        /// 删除所有未审核盘点单
        /// </summary>
        /// <param name="deptID">库房Id</param>
        /// <returns>1成功</returns>
        public int DeleteAllNotAuditCheckHead(int deptID)
        {
            string strSql = @"UPDATE MW_CheckHead SET DelFlag=1 WHERE AuditFlag=0 AND DeptID={0} AND WorkID={1}";
            strSql = string.Format(strSql, deptID, oleDb.WorkId);
            int iRtn = oleDb.DoCommand(strSql);
            return iRtn;
        }
        #endregion
    }
}