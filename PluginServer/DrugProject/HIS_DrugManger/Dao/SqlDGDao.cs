using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.CommonSql;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药品基础数据SQLSERVER数据库访问
    /// </summary>
    public class SqlDGDao : AbstractDao, IDGDao
    {
        #region 药剂科室设置
        /// <summary>
        /// 药剂科室设置-是否已经存在药剂科室
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>true存在,false不存在</returns>
        public bool ExistDrugDept(int deptId)
        {
            string strsql = @"SELECT COUNT(1) FROM DG_DeptDic WHERE DeptID={0} AND WorkID={1}";
            strsql = string.Format(strsql, deptId, oleDb.WorkId);
            return Convert.ToInt32(oleDb.GetDataResult(strsql)) == 0 ? false : true;
        }

        /// <summary>
        /// 获取药剂科室数据
        /// </summary>
        /// <returns>药剂科室数据集</returns>
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
                               FROM DG_DeptDic WHERE WorkID={0} order by DeptType";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 按类型获取药剂科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>药剂科室列表</returns>
        public DataTable GetDrugDeptList(int deptType)
        {
            string strSql = @"SELECT DeptID, DeptName
                              FROM DG_DeptDic
                              WHERE DeptType = {0} AND StopFlag = 0 AND WorkID = {1}";
            strSql = string.Format(strSql, deptType, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 初始化药剂科室的单据
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
                    sNType = "3";
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
        /// 取得药剂科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室单据集</returns>
        public DataTable GetDrugDeptBill(int deptId)
        {
            string strSql = @"SELECT  t.SNType,t.BusinessType,t.DeptId,t.CurrDate,t.CurrSequence,t.WorkID,t1.BusiTypeName 
                            FROM Basic_SerialNumberSource t LEFT JOIN DG_BusiType t1 ON t.BusinessType=t1.BusiCode AND t1.IsStop=0 AND t1.DelFlag=0
                            WHERE SNType=3 AND t.DeptId={0} AND T.WorkID={1}";
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
            string strSql = @"UPDATE DG_DeptDic SET StopFlag = 0 WHERE DeptID = {0}";
            strSql = string.Format(strSql, deptId);
            return oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 获取药品的包装换算系数
        /// </summary>
        /// <param name="drugId">药品ID</param>
        /// <returns>药品的包装换算系数</returns>
        public decimal GetPackAmount(int drugId)
        {
            string strSql = string.Format(
                @" SELECT PackAmount FROM dbo.DG_CenterSpecDic INNER JOIN dbo.DG_HospMakerDic ON dbo.DG_HospMakerDic.CenteDrugID = dbo.DG_CenterSpecDic.CenteDrugID WHERE DrugID = '{0}'",
                drugId);
            DataTable dt = oleDb.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToDecimal(dt.Rows[0]["PackAmount"]);
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 停用药剂科室
        /// </summary>
        /// <param name="deptDicID">药剂科室Id</param>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        public int StopUseDrugDept(int deptDicID, int deptId)
        {
            //1更改药剂科室启用状态
            string strSql = @"UPDATE DG_DeptDic SET StopFlag = 1 WHERE DeptDicID = {0}";
            strSql = string.Format(strSql, deptDicID);
            int ret = oleDb.DoCommand(strSql);
            DG_DeptDic model = (DG_DeptDic)NewObject<DG_DeptDic>().getmodel(deptDicID);
            if (model.DeptType == 0)
            {
                string where = "DeptID=" + model.DeptID;
                DataTable dtStoage = ((DS_Storage)NewObject<DS_Storage>()).gettable(where);
                if (dtStoage.Rows.Count > 0)
                {
                    return 1;
                }
            }
            else
            {
                string where = "DeptID=" + model.DeptID;
                DataTable dtStoage = ((DW_Storage)NewObject<DW_Storage>()).gettable(where);
                if (dtStoage.Rows.Count > 0)
                {
                    return 1;
                }
            }

            //2.删除单据流水表
            strSql = @"DELETE FROM Basic_SerialNumberSource WHERE DeptId={0}";
            strSql = string.Format(strSql, deptId);
            ret = oleDb.DoCommand(strSql);

            //3.删除药剂科管理类型表
            strSql = @"DELETE FROM DG_Dept_Type WHERE DeptID ={0}";
            strSql = string.Format(strSql, deptId);
            ret = oleDb.DoCommand(strSql);
            return ret;
        }

        /// <summary>
        /// 设置药剂科室盘点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="status">状态1开启盘点，0未开启</param>
        /// <param name="typeFlag">库房类型0药房，1药库</param>
        public void SetCheckStatus(int deptId, int status, int typeFlag)
        {
            string strSql = @"UPDATE DG_DeptDic SET CheckStatus={0} WHERE DeptID={1} AND WorkID={2} and DeptType={3}";
            strSql = string.Format(strSql, status, deptId, oleDb.WorkId, typeFlag);
            oleDb.DoCommand(strSql);
        }
        #endregion      

        #region 药品参数设置
        /// <summary>
        /// 取得启用的药剂科室
        /// </summary>
        /// <returns>启用的药剂科室集</returns>
        public DataTable GetUsedDrugDeptData()
        {
            string strSql = @"SELECT  DeptID,DeptName FROM DG_DeptDic WHERE StopFlag=0 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取药品公共参数
        /// </summary>
        /// <returns>药品公共参数集</returns>
        public DataTable GetPublicParameters()
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 3 AND DeptID = 0 AND WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室参数集</returns>
        public DataTable GetDeptParameters(int deptId)
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 3 AND DeptID = {0} AND WorkID = {1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取部门的参数配置
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="paraId">参数ID</param>
        /// <returns>部门的参数配置</returns>
        public Basic_SystemConfig GetDeptParameters(int deptId, string paraId)
        {
            string strSql = @"SELECT id, SystemType, DeptID, ParaID, ParaName, Value, DataType, Prompt, Memo, WorkID FROM Basic_SystemConfig
                               WHERE SystemType = 3 AND DeptID = {0} AND WorkID = {1} and Paraid='{2}'";
            strSql = string.Format(strSql, deptId, oleDb.WorkId, paraId);
            return oleDb.Query<Basic_SystemConfig>(strSql, null).FirstOrDefault();
        }

        /// <summary>
        /// 保存药品参数
        /// </summary>
        /// <param name="parameterList">药品参数列表</param>
        /// <returns>小于0失败</returns>
        public int SaveDrugParameters(List<Basic_SystemConfig> parameterList)
        {
            int ret = 0;
            string strSql = string.Empty;
            foreach (Basic_SystemConfig m in parameterList)
            {
                strSql = @"SELECT COUNT(1) FROM Basic_SystemConfig WHERE DeptID={0} AND SystemType=3 AND ParaID='{1}' AND WorkID={2}";
                strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);
                if (Convert.ToInt32(oleDb.GetDataResult(strSql)) > 0)
                {
                    strSql = @"UPDATE Basic_SystemConfig SET SystemType=3,DeptID=" + m.DeptID + ",ParaID='" + m.ParaID + "',ParaName='" + m.ParaName + "',Value='" + m.Value + "',DataType=0,Prompt='',Memo='" + m.Memo + "',WorkID=" + oleDb.WorkId +
                              " WHERE DeptID={0} AND SystemType=3 AND ParaID='{1}' AND WorkID={2}";
                    strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);
                }
                else
                {
                    strSql = @"INSERT INTO Basic_SystemConfig(SystemType,DeptID,ParaID,ParaName,Value,DataType,Prompt,Memo,WorkID)"
                             + @"VALUES(3, " + m.DeptID + ",'" + m.ParaID + "','" + m.ParaName + "','" + m.Value + "',0,'','" + m.Memo + "'," + oleDb.WorkId + ")";
                }

                ret = oleDb.DoCommand(strSql);
            }

            return ret;
        }

        /// <summary>
        /// 保存药品参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药品参数</returns>
        public int SaveDrugParameters(int deptId)
        {
            List<Basic_SystemConfig> parameterList = new List<Basic_SystemConfig>();
            Basic_SystemConfig model = new Basic_SystemConfig();
            parameterList.Clear();

            //加载通用参数
            //发药模式
            string pm014 = "0";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "DispModel", ParaName = "发药模式", Value = pm014, DataType = 0, Prompt = string.Empty, Memo = "发药模式:0经管发药(不含频次、用法等);1临床发药(含频次、用法等)" });

            //需要药房确认接收
            string pm009 = "0";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "NeedInstoreConfirm", ParaName = "是否需要药房确认接收", Value = pm009, DataType = 0, Prompt = string.Empty, Memo = "药品流通出库时是否需要药房确认接收。" });

            //西药利润百分比
            string pm015 = "16";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "WMPricePercent", ParaName = "西药利润率", Value = pm015, DataType = 0, Prompt = string.Empty, Memo = "西药利润率(百分比)" });

            //中成药利润百分比
            string pm016 = "24";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "CPMPricePercent", ParaName = "中成药利润率", Value = pm016, DataType = 0, Prompt = string.Empty, Memo = "中成药利润率(百分比)" });

            //中药利润百分比
            string pm017 = "25";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "TCMPricePercent", ParaName = "中药利润率(百分比)", Value = pm017, DataType = 0, Prompt = string.Empty, Memo = "中药利润率(百分比)" });

            //摆药单打印药品类型
            string pm018 = "1";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "PrintPutBillType", ParaName = "摆药单打印药品类型", Value = pm018, DataType = 0, Prompt = string.Empty, Memo = "摆药单打印药品类型：0全部类型；1口服药" });

            //统领单打印药品类型
            string pm019 = "1";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = 0, ParaID = "PrintReceiveBillType", ParaName = "统领单打印药品类型", Value = pm019, DataType = 0, Prompt = string.Empty, Memo = "统领单打印药品类型：0全部；1针剂；2针剂+大输液；3仅打印大输液" });

            //加载部门参数
            //出库单自动审核
            string pm021 = "0";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AutoAuditOutStore", ParaName = "出库单自动审核", Value = pm021, DataType = 0, Prompt = string.Empty, Memo = "出库单自动审核:0手动;1自动" });

            //入库单自动审核 chk_input_check
            string pm020 = "0";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AutoAuditInstore", ParaName = "入库单自动审核", Value = pm020, DataType = 0, Prompt = string.Empty, Memo = "入库单自动审核:0手动;1自动" });

            //月结时间为每月 iip_banlance_day
            string pm008 = "25";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "BalanceDay", ParaName = "默认月结时间", Value = pm008, DataType = 0, Prompt = string.Empty, Memo = "默认月结时间(每月多少号)" });

            //强制控制库存 chk_qzkc
            string pm010 = "0";

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "ControlStore", ParaName = "是否允许库存为负数", Value = pm010, DataType = 0, Prompt = string.Empty, Memo = "是否允许库存为负数:0不强制控制;1强制控制" });

            //允许强制平账 chk_qzpz
            string pm011 = "1";

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "AllowModifyAccount", ParaName = "允许强制平账", Value = pm011, DataType = 0, Prompt = string.Empty, Memo = "是否允许自动平账:0不允许;1允许" });

            //月结前必须对账 chk_dz
            string pm012 = "0";

            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "CheckAccountWhenBalance", ParaName = "月结前必须对账", Value = pm012, DataType = 0, Prompt = string.Empty, Memo = "月结时是否对账:0不对账;1对账" });

            //对账误差范围 iip_wzfw
            string pm013 = "1";
            parameterList.Add(new Basic_SystemConfig() { SystemType = 3, DeptID = deptId, ParaID = "ErrorRange", ParaName = "对账误差范围", Value = pm013, DataType = 0, Prompt = string.Empty, Memo = "金额允许误差范围(0.1元为单位)" });

            int ret = 0;
            string strSql = string.Empty;
            foreach (Basic_SystemConfig m in parameterList)
            {
                strSql = @"SELECT COUNT(1) FROM Basic_SystemConfig WHERE DeptID={0} AND SystemType=3 AND ParaID='{1}' AND WorkID={2}";
                strSql = string.Format(strSql, m.DeptID, m.ParaID, oleDb.WorkId);
                if (Convert.ToInt32(oleDb.GetDataResult(strSql)) <= 0)
                {
                    //修改
                    strSql = @"INSERT INTO Basic_SystemConfig(SystemType,DeptID,ParaID,ParaName,Value,DataType,Prompt,Memo,WorkID)"
                             + @"VALUES(3, " + m.DeptID + ",'" + m.ParaID + "','" + m.ParaName + "','" + m.Value + "',0,'','" + m.Memo + "'," + oleDb.WorkId + ")";
                }

                ret = oleDb.DoCommand(strSql);
            }

            return ret;
        }
        #endregion

        #region 剂型、药品类型、子类型、药理、供应商
        /// <summary>
        /// 加载供应商【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        public DataTable GetSupplyForShowCard()
        {
            string strSql = @"select SupplierID,SupportName,PYCode,WBCode 
                            from DG_SupportDic where DelFlag=0 and WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型典</returns>
        public DataTable GetTypeDic()
        {
            string strSql = @"SELECT 0 AS selected,
                               TypeID
                              ,TypeName
                              ,PYCode
                              ,WBCode
                              ,WorkID
                          FROM DG_TypeDic
                          WHERE TypeID<4";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 药品子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>子类型</returns>
        public DataTable GetChildDrugType(Dictionary<string, string> query = null)
        {
            return oleDb.GetDataTable(GetChildDrugTypeSql(query));
        }

        /// <summary>
        /// 药品子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>子类型</returns>
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
        /// 查询药剂
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药剂</returns>
        public DataTable GetDosageUserOr(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(" SELECT D.*,T.TypeName  FROM dbo.DG_DosageDic D LEFT JOIN DG_TypeDic T ON D.TypeID=T.TypeID where 1 = 1");

            stb.Append(" and DelFlag=0 ");
            if (query != null && query.Count > 0)
            {
                foreach (var pair in query)
                {
                    if (pair.Key == "PYCode")
                    {
                        stb.Append(" and ( ");
                        stb.AppendFormat(" D.{0} LIKE '{1}%' ", pair.Key, pair.Value);
                        stb.AppendFormat("OR D.DosageName LIKE '{0}%' ", pair.Value);
                        stb.Append(" ) ");
                    }
                    else
                    {
                        stb.Append(" and ");
                        stb.AppendFormat(" D.{0} LIKE '{1}%' ", pair.Key, pair.Value);
                    }
                }
            }

            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 根据子类型获取父类型名称
        /// </summary>
        /// <param name="cTypeId">子类型ID</param>
        /// <returns>父类型名称</returns>
        public string GetTypeName(string cTypeId)
        {
            string sql = "select a.TypeName from DG_TypeDic a left join DG_ChildTypeDic b on a.TypeID=b.TypeID where b.CTypeID={0}";
            return oleDb.GetDataTable(string.Format(sql, cTypeId)).Rows[0]["TypeName"].ToString();
        }

        /// <summary>
        /// 通过父ID字段药理分类查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药理分类</returns>
        public DataTable GetPharmByParentId(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(@" SELECT t.*,d.PharmName AS PNAME FROM dbo.DG_Pharmacology t INNER JOIN DG_Pharmacology d 
ON t.ParentID = d.PharmID
AND t.Delflag = 0 ");

            if (query != null && query.Count > 0)
            {
                foreach (var pair in query)
                {
                    stb.AppendFormat(" and t.{0}= '{1}' ", pair.Key, pair.Value);
                }
            }

            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 联合药品类型和子类型查询
        /// </summary>
        /// <returns>药品类型和子类型</returns>
        public DataTable GetDrugTypeAndChild()
        {
            string sql = @"SELECT P.TypeName,C.CTypeName,C.CTypeID,P.TypeID  FROM [dbo].[DG_TypeDic] P LEFT JOIN  [dbo].[DG_ChildTypeDic] C
                            ON P.TypeID=C.TypeID";
            return oleDb.GetDataTable(sql);
        }
        #endregion

        #region 药库药房往来科室设置
        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <param name="menuTypeFlag">菜单类型0药房往来科室维护，1药库往来科室维护</param>
        /// <returns>库房数据</returns>
        public DataTable GetStoreRoomData(int menuTypeFlag)
        {
            int deptType = 0;
            if (menuTypeFlag == 0)
            {
                deptType = 0;
            }
            else if (menuTypeFlag == 1)
            {
                deptType = 1;
            }

            string strSql = @"SELECT a.DeptID,a.DeptName,ISNULL(b.Pym,'') Pym,ISNULL(b.Wbm,'') Wbm FROM  DG_DeptDic a LEFT JOIN BaseDept b ON a.DeptID=b.DeptId 
                             WHERE a.DeptType={0} AND a.StopFlag=0 AND b.DelFlag=0 AND a.WorkID={1}";
            strSql = string.Format(strSql, deptType, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

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
                            FROM DG_DeptRelation
                            WHERE DelFlag=0 AND DrugDeptID={0} AND WorkID={1} and delflag=0 ";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取往来科室
        /// </summary>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <returns>往来科室</returns>
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
                            FROM DG_DeptRelation
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
                                FROM    DG_DeptRelation
                                WHERE   DelFlag = 0
                                        AND DrugDeptID = {0}
                                        AND RelationDeptID = {1}
                                        AND WorkID = {2}";
                strSql = string.Format(strSql, drugDeptID, relationDeptID, oleDb.WorkId);
                int cnt = Convert.ToInt32(oleDb.GetDataResult(strSql));
                if (cnt > 0)
                {
                    //修改
                    strSql = @"UPDATE  DG_DeptRelation
                            SET  
                                    RelationDeptName='{0}' ,
                                    DelFlag=0 ,
                                    Remark='' ,
                                    UpdateTime=GETDATE() ,
                                    RegEmpID ={1},
                                    RelationDeptType={2}
                            FROM    DG_DeptRelation
                            WHERE   DrugDeptID = {3}
                                            AND RelationDeptID = {4}
                                            AND WorkID = {5}";
                    strSql = string.Format(strSql, relationDeptName, empId, type, drugDeptID, relationDeptID, oleDb.WorkId);
                }
                else
                {
                    //增加
                    strSql = @"INSERT INTO  DG_DeptRelation (DrugDeptID ,
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
                                FROM    DG_DeptRelation
                                WHERE   DelFlag = 0
                                        AND DrugDeptID = {0}
                                        AND RelationDeptID = {1}
                                        AND WorkID = {2}";
            strSql = string.Format(strSql, drugDeptID, relationDeptID, oleDb.WorkId);
            int cnt = Convert.ToInt32(oleDb.GetDataResult(strSql));
            if (cnt > 0)
            {
                strSql = @"DELETE  FROM DG_DeptRelation
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
                              AuditTime=(CASE a.AuditFlag WHEN 0 THEN '' WHEN 1 THEN CONVERT(varchar(20),a.AuditTime,120) END),
                              a.AuditEmpID,
                              a.AuditEmpName,
                              a.Remark,
                              a.PlanDate
                          FROM DW_PlanHead a LEFT JOIN BaseDept b ON a.DeptID=b.DeptId
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
                              SELECT    a.DrugID ,
                                        c.ChemName ,
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
                                        a.PlanHeadID ,
                                        a.CTypeID ,
                                        e.CTypeName
                              FROM      DW_PlanDetail a
                                        LEFT JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        LEFT JOIN DG_CenterSpecDic c ON b.CenteDrugID = c.CenteDrugID
                                        LEFT JOIN DG_ProductDic d ON b.ProductID = d.ProductID
                                        LEFT JOIN DG_ChildTypeDic e ON a.CTypeID = e.CTypeID
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
        /// 取得药品字典选择卡片数据
        /// </summary>
        /// <returns>药品字典信息</returns>
        public DataTable GetDrugDicShowCard()
        {
            string strSql = @"SELECT  CAST(b.DrugID AS VARCHAR(100)) AS DrugID,
                                a.ChemName ,
                                b.TradeName ,
                                a.Spec ,
                                b.ProductID ,
                                c.ProductName ,
                                a.PYCode ,
                                a.WBCode ,
                                b.PYCode AS TPYCode ,
                                b.WBCode AS TWBCode ,
                                a.PackUnitID ,
                                a.PackUnit ,
                                a.PackAmount ,
                                b.StockPrice ,
                                b.RetailPrice,
		                        a.CTypeID,
		                        d.CTypeName
                        FROM    DG_CenterSpecDic a
                                LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
		                        LEFT JOIN DG_ChildTypeDic d ON a.CTypeID=d.CTypeID
                        WHERE   a.IsStop = 0
                                AND b.IsStop = 0 and b.WorkID=" + oleDb.WorkId;
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 删除采购计划单 
        /// </summary>
        /// <param name="billID">采购计划单表头Id</param>
        /// <returns>返回结果</returns>
        public bool DeleteDWPlanBill(int billID)
        {
            bool boolRtn = true;

            //删除采购计划单明细表
            string strSql = @"DELETE FROM DW_PlanDetail WHERE PlanHeadID={0} AND WorkID={1}";
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
            strSql = @"DELETE FROM dbo.DW_PlanHead WHERE PlanHeadID={0} AND WorkID={1}";
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
        /// 取得小于下限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        public DataTable GetLessLowerLimitDrugData(int deptId)
        {
            string strSql = @"SELECT  b.DrugID ,
                                a.ChemName ,
                                b.TradeName ,
                                a.Spec ,
                                b.ProductID ,
                                c.ProductName ,
                                a.PackUnitID ,
                                a.PackUnit ,
                                a.PackAmount ,
                                b.StockPrice ,
                                b.RetailPrice,
		                        a.CTypeID,
		                        d.CTypeName,
								(ISNULL(w.LowerLimit,0) - ISNULL(w.Amount,0)) AS BuyAmount 
                        FROM    DG_CenterSpecDic a 
                                LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
		                        LEFT JOIN DG_ChildTypeDic d ON a.CTypeID=d.CTypeID
								RIGHT JOIN  DW_Storage w ON w.DrugID=b.DrugID
                        WHERE w.DelFlag=0 AND ISNULL(w.Amount,0)<ISNULL(w.LowerLimit,0)
						      AND w.DeptID={0} AND w.WorkID={1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得小于上限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        public DataTable GetLessUpperLimitDrugData(int deptId)
        {
            string strSql = @"SELECT  b.DrugID ,
                                a.ChemName ,
                                b.TradeName ,
                                a.Spec ,
                                b.ProductID ,
                                c.ProductName ,
                                a.PackUnitID ,
                                a.PackUnit ,
                                a.PackAmount ,
                                b.StockPrice ,
                                b.RetailPrice,
		                        a.CTypeID,
		                        d.CTypeName,
								(ISNULL(w.UpperLimit,0) - ISNULL(w.Amount,0)) AS BuyAmount 
                        FROM    DG_CenterSpecDic a 
                                LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
		                        LEFT JOIN DG_ChildTypeDic d ON a.CTypeID=d.CTypeID
								RIGHT JOIN  DW_Storage w ON w.DrugID=b.DrugID
                        WHERE w.DelFlag=0 AND ISNULL(w.Amount,0)<ISNULL(w.UpperLimit,0)
						      AND w.DeptID={0} AND w.WorkID={1}";
            strSql = string.Format(strSql, deptId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 药品字典

        /// <summary>
        /// 药品库存是否存在
        /// </summary>
        /// <param name="centeDrugId">中心药品ID</param>
        /// <returns>返回结果</returns>
        public bool StoreExsitDrug(int centeDrugId)
        {
            string sql = string.Format(
                @"SELECT  COUNT(0)
FROM    dbo.DG_CenterSpecDic c
        INNER JOIN dbo.DG_HospMakerDic m ON c.CenteDrugID = m.CenteDrugID
        INNER JOIN dbo.DW_Storage s ON m.DrugID = s.DrugID
WHERE   c.CenteDrugID = {0}
        AND s.DelFlag = 0
		UNION 
		SELECT  COUNT(0)
FROM    dbo.DG_CenterSpecDic c
        INNER JOIN dbo.DG_HospMakerDic m ON c.CenteDrugID = m.CenteDrugID
        INNER JOIN dbo.DS_Storage s ON m.DrugID = s.DrugID
WHERE   c.CenteDrugID = {1}
        AND s.DelFlag = 0",
        centeDrugId,
        centeDrugId);
            var x = oleDb.GetDataResult(sql);
            if (Convert.ToInt32(x) > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 返回表实体的DATATABLE
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>表实体的DATATABLE</returns>
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
        /// <param name="page">分页对象</param>
        /// <returns>表实体的DATATABLE</returns>
        public DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, PageInfo page) where T : AbstractEntity
        {
            string sql = SqlServerProcess.GetExecutSql<T>(andWhere, orWhere);
            return oleDb.GetDataTable(SqlPage.FormatSql(sql, page, oleDb));
        }

        /// <summary>
        /// 药品字典查询
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>药品字典</returns>
        public DataTable GetDrugDic(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(@"select * from (SELECT  d.* ,
        CASE d.AuditStatus 
          WHEN 0 THEN '未审核'
          WHEN 1 THEN '已审核'
        END AS AuditName,
		CASE d.IsStop  
		  WHEN 0 THEN '启用'
		  WHEN 1 THEN '停用' 
		  END AS StopName,
        T.TypeName AS drugTypeNam ,
        C.CTypeName AS CTypeName ,
        p.PharmName AS PharmName,
		E.Name AS UserName,
		S.DosageName AS DosageName
FROM    DG_CenterSpecDic D
        LEFT JOIN dbo.DG_TypeDic T ON D.TypeID = t.TypeID
        LEFT JOIN dbo.DG_ChildTypeDic C ON d.CTypeID = C.CTypeID
        LEFT JOIN dbo.DG_Pharmacology p ON p.PharmID = d.PharmID
		LEFT JOIN BaseEmployee E ON e.EmpId=d.Auditor 
        LEFT JOIN dbo.DG_DosageDic S ON D.DosageID=S.DosageID) M
        WHERE 1=1 ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 本院典查所有数据 由所有通过审核的中心典并上没有通过审核的本院典
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <param name="createWorkId">创建机构ID</param>
        /// <returns>本院典所有数据</returns>
        public DataTable GetDrugDic(
            List<Tuple<string, string, SqlOperator>> andWhere,
          List<Tuple<string, string, SqlOperator>> orWhere,
          int createWorkId)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT  *
        FROM    ( SELECT    d.* ,
                            CASE d.AuditStatus
                              WHEN 0 THEN '未审核'
                              WHEN 1 THEN '已审核'
                            END AS AuditName ,
                     
                            CASE d.IsStop
                              WHEN 0 THEN '启用'
                              WHEN 1 THEN '停用'
                            END AS StopName ,
                            T.TypeName AS drugTypeNam ,
                            C.CTypeName AS CTypeName ,
                            p.PharmName AS PharmName ,
                            E.Name AS UserName,
							H.WorkID as CreateWorkIDS,
						    S.DosageName AS DosageName
                  FROM      DG_CenterSpecDic D
                            LEFT JOIN DG_HospMakerDic H on D.CenteDrugID=H.CenteDrugID
                            LEFT JOIN dbo.DG_TypeDic T ON D.TypeID = t.TypeID
                            LEFT JOIN dbo.DG_ChildTypeDic C ON d.CTypeID = C.CTypeID
                            LEFT JOIN dbo.DG_Pharmacology p ON p.PharmID = d.PharmID
                            LEFT JOIN BaseEmployee E ON e.EmpId = d.Auditor
                            LEFT JOIN dbo.DG_DosageDic S ON D.DosageID=S.DosageID
                  WHERE     1 = 1
                            AND d.WorkID = 0
                            AND d.AuditStatus = 1
                  UNION
                  SELECT    d.* ,
                            CASE d.AuditStatus
                              WHEN 0 THEN '未审核'
                              WHEN 1 THEN '已审核'
                            END AS AuditName ,
      
                            CASE d.IsStop
                              WHEN 0 THEN '启用'
                              WHEN 1 THEN '停用'
                            END AS StopName ,
                            T.TypeName AS drugTypeNam ,
                            C.CTypeName AS CTypeName ,
                            p.PharmName AS PharmName ,
                            E.Name AS UserName,
							H.WorkID as CreateWorkIDS,
						    S.DosageName AS DosageName
                  FROM      DG_CenterSpecDic D
                            LEFT JOIN DG_HospMakerDic H on D.CenteDrugID=H.CenteDrugID
                            LEFT JOIN dbo.DG_TypeDic T ON D.TypeID = t.TypeID
                            LEFT JOIN dbo.DG_ChildTypeDic C ON d.CTypeID = C.CTypeID
                            LEFT JOIN dbo.DG_Pharmacology p ON p.PharmID = d.PharmID
                            LEFT JOIN BaseEmployee E ON e.EmpId = d.Auditor
                            LEFT JOIN dbo.DG_DosageDic S ON D.DosageID=S.DosageID
                  WHERE     1 = 1
                            AND d.CreateWorkID = {0}
                            AND d.AuditStatus = 0
                ) AS t where 1=1 ",
                createWorkId);
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 查询本院典
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>本院典数据</returns>
        public DataTable GetDrugDicHisDataTable(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(@"
   SELECT d.*,p.ProductName,m.MedicareName,
                            CASE WHEN (d.Maker=0) 
                            THEN '进口' ELSE '国产' 
                            END AS MakerName,
	CASE d.IsStop  
		  WHEN 0 THEN '启用'
		  WHEN 1 THEN '停用' 
		  END AS StopName,
CASE d.RoundingMode  
		  WHEN 0 THEN '总量取整'
		  WHEN 1 THEN '单次取整' 
		  END AS RoundingModeName
FROM DG_HospMakerDic d 
   LEFT JOIN dbo.DG_ProductDic p ON 
   p.ProductID = d.ProductID
   LEFT JOIN dbo.DG_MedicareDic m ON m.MedicareID = d.MedicareID
        WHERE 1=1 ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 查询实体对象
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>实体对象</returns>
        public IEnumerable<T> GetEntityType<T>(
            List<Tuple<string, string, SqlOperator>> andWhere = null,
             List<Tuple<string, string, SqlOperator>> orWhere = null) where T : AbstractEntity
        {
            var sql = SqlServerProcess.GetExecutSql<T>(andWhere, orWhere);
            return oleDb.Query<T>(sql, null);
        }
        #endregion

        #region 药库库存
        /// <summary>
        /// 查询科室库存药品信息
        /// </summary>
        /// <param name="dept">科室Id</param>
        /// <returns>科室库存药品信息</returns>
        public DataTable GetStoreDrugInFo(int dept)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT  s.DrugID,
        s.Amount totalNum,
        c.ChemName ,
		c.PYCode,
		c.WBCode,
		pd.PYCode SPYCode,
		pd.WBCode SWBCode,
        c.Spec ,
        pd.ProductName ,
        h.StockPrice ,
        h.RetailPrice ,
        c.PackUnitID ,
        c.PackUnit,
        c.MiniUnitID,
        c.CTypeID,
        c.MiniUnit ,
        c.DosageID ,
        ud.UnitName,
		db.BatchNO,
        db.BatchAmount,
        db.ValidityTime,
		db.RetailPrice as BatchRetail,
		db.StockPrice as BatchStock
FROM    dbo.DW_Storage s
        INNER JOIN DG_HospMakerDic h ON h.DrugID = s.DrugID
        INNER JOIN dbo.DG_CenterSpecDic c ON h.CenteDrugID = c.CenteDrugID
        LEFT JOIN dbo.DG_ProductDic pd ON pd.ProductID = h.ProductID
        LEFT JOIN DG_UnitDic ud ON ud.UnitID = c.PackUnitID
		LEFT JOIN dbo.DW_Batch db ON db.DrugID=h.DrugID AND db.DeptID=s.DeptID
	WHERE 1 = 1 AND s.DelFlag = 0 and h.IsStop=0 AND c.IsStop=0 AND db.DelFlag=0 AND s.DeptID={0} ",
    dept);
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 获取库位详细信息
        /// </summary>
        /// <param name="locationid">库位ID</param>
        /// <returns>库位详细信息</returns>
        public DG_Location GetLocationInfo(int locationid)
        {
            string strSql = "SELECT * FROM DG_Location WHERE LocationID={0}";
            return oleDb.Query<DG_Location>(string.Format(strSql, locationid), string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取库位详细信息
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <param name="locationname">库位名称</param>
        /// <returns>库位详细信息</returns>
        public DG_Location GetLocationByName(int parentid, string locationname)
        {
            string strSql = "SELECT * FROM DG_Location WHERE ParentID={0} AND LocationName='{1}'";
            return oleDb.Query<DG_Location>(string.Format(strSql, parentid, locationname.Trim()), string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取库位列表信息
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <returns>库位列表信息</returns>
        public List<DG_Location> GetLocationList(int deptid)
        {
            string strSql = "SELECT * FROM DG_Location WHERE DeptID={0}";
            return oleDb.Query<DG_Location>(string.Format(strSql, deptid), string.Empty).ToList();
        }

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <param name="locationid">库位ID</param>
        /// <param name="ids">ID集</param>
        /// <param name="frmName">窗体名称</param>
        /// <returns>返回结果</returns>
        public int UpdateStorage(int locationid, string ids, string frmName)
        {
            string strSql = "UPDATE DW_Storage SET LocationID={0} WHERE StorageID in ({1})";
            if (frmName == "FrmLocation")
            {
                strSql = "UPDATE DS_Storage SET LocationID={0} WHERE StorageID in ({1})";
            }

            return oleDb.DoCommand(string.Format(strSql, locationid, ids));
        }

        /// <summary>
        /// 拆零操作
        /// </summary>
        /// <param name="ids">ID集</param>
        /// <param name="type">类型</param>
        /// <returns>返回结果</returns>
        public int UpdateStorages(string ids, int type)
        {
            string strSql = "UPDATE DS_Storage SET ResolveFlag=1 WHERE StorageID in ({0})";
            if (type == 1)
            {
                strSql = "UPDATE DS_Storage SET ResolveFlag=0 WHERE StorageID in ({0})";
            }

            return oleDb.DoCommand(string.Format(strSql, ids));
        }
        #endregion

        #region 药品调价
        /// <summary>
        /// 读取药品调价表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>药品调价表头</returns>
        public DataTable LoadAdjHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT a.AdjHeadID,a.BillNO,a.RegEmpID,a.RegTime,a.Remark,a.AdjCode,b.DeptName,c.Name,a.Remark,a.ExecTime
            FROM DG_AdjHead a LEFT JOIN DG_DeptDic b ON a.DeptID=b.DeptID 
            LEFT JOIN BaseEmployee c ON a.RegEmpID=c.EmpId
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

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 读取药品调价表详情
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>药品调价表详情</returns>
        public DataTable LoadAdjDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT a.*,c.ChemName,c.Spec,d.ProductName,c.PackUnitID,c.PackUnit,c.MiniUnitID,c.MiniUnit FROM DG_AdjDetail a 
            LEFT JOIN DG_HospMakerDic b
            ON a.DrugID = b.DrugID
            LEFT JOIN DG_CenterSpecDic c
            ON b.CenteDrugID = c.CenteDrugID
            LEFT JOIN DG_ProductDic d
            ON b.ProductID = d.ProductID
            WHERE b.IsStop = 0 AND c.IsStop = 0 AND a.WorkID = {0}";
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
        /// 根据ID获取表头信息
        /// </summary>
        /// <param name="headid">表头ID</param>
        /// <returns>表头信息</returns>
        public DG_AdjHead LoadAdjHeadById(int headid)
        {
            string strSql = @"SELECT * FROM DG_AdjHead WHERE AdjHeadID={0}";
            return oleDb.Query<DG_AdjHead>(string.Format(strSql, headid), string.Empty).FirstOrDefault();
        }
        #endregion

        #region 药房药品显示卡片数据
        /// <summary>
        /// 获取药房药品ShowCard数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药房药品数据</returns>
        public DataTable GetDrugStoreShowCardData(int deptId)
        {
            string strsql = @"SELECT a.DrugID, c.CenterDrugCode ItemCode, c.ChemName, c.Spec,b.TradeName,b.PYCode,B.WbCode, 
                                        a.Amount StoreAmount, a.DeptID ExecDeptId, dbo.fnGetDeptName(a.DeptID) ExecDeptName, c.PYCode Pym, 
                                        c.WBCode Wbm, dbo.fnGetProductName(b.ProductID) [Address], a.PackUnit UnPickUnit, dbo.fnGetDrugPrice(a.DrugID,a.DeptID) 
                                        SellPrice, dbo.fnGetDrugInPrice(a.DrugID) InPrice, a.UnitName MiniUnitName, CONVERT(DECIMAL(18, 4), a.UnitAmount) 
                                        MiniConvertNum, a.ResolveFlag, CONVERT(DECIMAL(18, 4), dbo.fnGetDrugPrice(a.DrugID,a.DeptID) / a.UnitAmount) UnitPrice, 
                                        1 ItemClass, c.StatID, a.WorkID
                        FROM      DS_Storage a LEFT JOIN
                                        DG_HospMakerDic b ON a.DrugID = b.DrugID AND b.WorkID = a.WorkID LEFT JOIN
                                        DG_CenterSpecDic c ON b.CenteDrugID = c.CenteDrugID
                        WHERE a.DeptID={0} and a.WorkId={1}";//   b.IsStop = 0 AND c.IsStop = 0 and 过滤已停用的药品也需要显示
            strsql = string.Format(strsql, deptId, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }
        #endregion
    }
}
