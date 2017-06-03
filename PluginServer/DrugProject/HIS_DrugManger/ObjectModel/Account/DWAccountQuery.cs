using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HIS_DrugManger.Dao;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 药库账目查询类
    /// </summary>
    class DWAccountQuery : AccountQuery
    {
        /// <summary>
        /// 查询药品流水账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>流水账列表</returns>
        public override DataTable AccountReport(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建进销存表结构
        /// </summary>
        /// <returns>进销存表结构</returns>
        private DataTable CreateInventoryTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LPRJNAME", Type.GetType("System.String"));
            dt.Columns.Add("LPRJRETAILFEE", Type.GetType("System.Decimal"));
            dt.Columns.Add("LPRJTRADEFEE", Type.GetType("System.Decimal"));
            dt.Columns.Add("DPRJNAME", Type.GetType("System.String"));
            dt.Columns.Add("DPRJRETAILFEE", Type.GetType("System.Decimal"));
            dt.Columns.Add("DPRJTRADEFEE", Type.GetType("System.Decimal"));
            return dt;
        }

        /// <summary>
        /// 求合计
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="columnName">列名</param>
        /// <returns>合计</returns>
        private decimal ColumnSum(DataTable dt, string columnName)
        {
            decimal d = 0;
            string val = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                if (row[columnName] == DBNull.Value || row[columnName].ToString().Trim() == string.Empty)
                {
                    val = "0";
                }
                else
                {
                    val = row[columnName].ToString();
                }

                d += decimal.Parse(val);
            }

            return d;
        }

        /// <summary>
        /// 创建业务类型表
        /// </summary>
        /// <returns>业务列表</returns>
        private List<DGBusiType> CreateBusiTypeList()
        {
            List<DGBusiType> list = new List<DGBusiType>()
            {
                new DGBusiType { BusiCode="111", BusiTypeName="采购入库", Remark="药库采购入库类型", NeedToDept=true, STType=DGEnum.StatisticType.Lend, HeadTableName="DW_InStoreHead t1", JoinExpress="t1.InHeadID=t2.InHeadID", DetailTableName="DW_InStoreDetail t2", DetailIdFieldName="t2.InDetailID", DeptFieldName="t1.SupplierName as deptname", IsSupplier=true  },
                new DGBusiType { BusiCode="112", BusiTypeName="期初入库", Remark="药库期初入库类型", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="113", BusiTypeName="药库退货", Remark="药库退货类型", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="121", BusiTypeName="流通出库", Remark="药库流通出库类型", NeedToDept=true, STType=DGEnum.StatisticType.Debit, HeadTableName="DW_OutStoreHead t1", JoinExpress="t1.OutStoreHeadID=t2.OutHeadID", DetailTableName="DW_OutStoreDetail t2", DetailIdFieldName="t2.OutDetailID", DeptFieldName="IsNull(dbo.fnGetDeptName(t1.ToDeptID),'无') as deptname", IsSupplier=false  },
                new DGBusiType { BusiCode="122", BusiTypeName="内耗出库", Remark="药库内耗出库业务类型", NeedToDept=true, STType=DGEnum.StatisticType.Debit,HeadTableName="DW_OutStoreHead t1", JoinExpress="t1.OutStoreHeadID=t2.OutHeadID", DetailTableName="DW_OutStoreDetail t2", DetailIdFieldName="t2.OutDetailID", DeptFieldName="IsNull(dbo.fnGetDeptName(t1.ToDeptID),'无') as deptname", IsSupplier=false   },
                new DGBusiType { BusiCode="123", BusiTypeName="药库报损", Remark="药库报损出库业务类型", NeedToDept=false, STType=DGEnum.StatisticType.Debit  },
                new DGBusiType { BusiCode="124", BusiTypeName="药库退库", Remark="药库退库业务", NeedToDept=false, STType=DGEnum.StatisticType.Debit  },
                new DGBusiType { BusiCode="142", BusiTypeName="盘点审核", Remark="药库盘点审核业务", NeedToDept=false, STType=DGEnum.StatisticType.LendAndDebit  },
                new DGBusiType { BusiCode="151", BusiTypeName="药库调价", Remark="药库调价业务", NeedToDept=false, STType=DGEnum.StatisticType.LendAndDebit  }
            };
           
            return list;
        }

        /// <summary>
        /// 查询进销存账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>流水账列表</returns>
        public override DataTable CSPJReport(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询进销存账
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="queryYear">会计年</param>
        /// <param name="queryMonth">会计月</param>
        /// <param name="typeId">类型</param>
        /// <returns>查询进销存账数据集</returns>
        public DataTable CSPJReport(int deptId, int queryYear, int queryMonth, int typeId)
        {
            DataTable dtInventory = CreateInventoryTable();

            //期初金额
            DataTable dtOpen = NewDao<IDWDao>().GetAccountData(1, deptId, queryYear, queryMonth, typeId);

            //添加期初金额
            if (dtOpen.Rows.Count > 0)
            {
                DataRow openRow = dtInventory.NewRow();
                openRow["LPRJNAME"] = "期初金额";
                openRow["LPRJRETAILFEE"] = Convert.ToDecimal(dtOpen.Compute("sum(OverRetailFee)", string.Empty));
                openRow["LPRJTRADEFEE"] = Convert.ToDecimal(dtOpen.Compute("sum(OverStockFee)", string.Empty));
                openRow["DPRJNAME"] = string.Empty;
                openRow["DPRJRETAILFEE"] = 0;
                openRow["DPRJTRADEFEE"] = 0;
                dtInventory.Rows.Add(openRow);
            }
            else
            {
                DataRow openRow = dtInventory.NewRow();
                openRow["LPRJNAME"] = "期初金额";
                openRow["LPRJRETAILFEE"] = 0;
                openRow["LPRJTRADEFEE"] = 0;
                openRow["DPRJNAME"] = string.Empty;
                openRow["DPRJRETAILFEE"] = 0;
                openRow["DPRJTRADEFEE"] = 0;
                dtInventory.Rows.Add(openRow);
            }

            //处理业务数据
            List<DGLendModel> lendList = new List<DGLendModel>();//借方列表
            List<DGDebitModel> debitList = new List<DGDebitModel>();//贷方列表
            decimal retailFee = 0;
            decimal stockFee = 0;
            foreach (DGBusiType m in CreateBusiTypeList())
            {
                //需要统计到科室供应商
                if (m.NeedToDept)
                {
                    DataTable dtData = NewDao<IDWDao>().GetAccountData(0, deptId, queryYear, queryMonth, typeId, m.BusiCode, m);
                    if (dtData == null || dtData.Rows.Count <= 0)
                    {
                        continue;
                    }

                    //过滤出部门
                    DataView dv = dtData.DefaultView;
                    DataTable dtDept = dv.ToTable("dept", true, new string[] { "deptname" });
                    foreach (DataRow r in dtDept.Rows)
                    {
                        string deptName = string.Empty;
                        if (r[0] == DBNull.Value)
                        {
                            r[0] = string.Empty;
                            deptName = string.Empty;
                        }
                        else
                        {
                            deptName = r[0].ToString();
                        }

                        switch (m.STType)
                        {
                            case DGEnum.StatisticType.Debit:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", "deptname='" + deptName + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", "deptname='" + deptName + "'"));
                                DGDebitModel debitModel = new DGDebitModel() { DebitName = deptName + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                debitList.Add(debitModel);
                                break;
                            case DGEnum.StatisticType.Lend:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", "deptname='" + deptName + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", "deptname='" + deptName + "'"));
                                DGLendModel lendModel = new DGLendModel() { LendName = deptName + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                lendList.Add(lendModel);
                                break;
                            case DGEnum.StatisticType.LendAndDebit:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", "deptname='" + deptName + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", "deptname='" + deptName + "'"));
                                DGDebitModel debitModel1 = new DGDebitModel() { DebitName = deptName + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                debitList.Add(debitModel1);
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", "deptname='" + deptName + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", "deptname='" + deptName + "'"));
                                DGLendModel lendModel1 = new DGLendModel() { LendName = deptName + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                lendList.Add(lendModel1);
                                break;
                        }
                    }
                }
                else
                {
                    DataTable dtData = NewDao<IDWDao>().GetAccountData(0, deptId, queryYear, queryMonth, typeId, m.BusiCode);
                    if (dtData == null || dtData.Rows.Count <= 0)
                    {
                        continue;
                    }

                    switch (m.STType)
                    {
                        case DGEnum.StatisticType.Debit:
                            retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", string.Empty));
                            stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", string.Empty));
                            DGDebitModel debitModel = new DGDebitModel() { DebitName = m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                            debitList.Add(debitModel);
                            break;
                        case DGEnum.StatisticType.Lend:
                            retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", string.Empty));
                            stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", string.Empty));
                            DGLendModel lendModel = new DGLendModel() { LendName = m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                            lendList.Add(lendModel);
                            break;
                        case DGEnum.StatisticType.LendAndDebit:
                            retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", string.Empty));
                            stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", string.Empty));
                            DGLendModel lendModel1 = new DGLendModel() { LendName = m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                            lendList.Add(lendModel1);

                            retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", string.Empty));
                            stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", string.Empty));
                            DGDebitModel debitModel1 = new DGDebitModel() { DebitName = m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                            debitList.Add(debitModel1);
                            break;
                    }
                }
            }

            //添加业务数据
            int cnt = lendList.Count > debitList.Count ? lendList.Count : debitList.Count;
            for (int i = 0; i < cnt; i++)
            {
                DataRow rowIn = dtInventory.NewRow();
                if (i > lendList.Count - 1)
                {
                    rowIn["LPRJNAME"] = string.Empty;
                    rowIn["LPRJRETAILFEE"] = DBNull.Value;
                    rowIn["LPRJTRADEFEE"] = DBNull.Value;
                }
                else
                {
                    rowIn["LPRJNAME"] = lendList[i].LendName;
                    rowIn["LPRJRETAILFEE"] = lendList[i].RetailFee;
                    rowIn["LPRJTRADEFEE"] = lendList[i].StockFee;
                }

                if (i > debitList.Count - 1)
                {
                    rowIn["DPRJNAME"] = string.Empty;
                    rowIn["DPRJRETAILFEE"] = DBNull.Value;
                    rowIn["DPRJTRADEFEE"] = DBNull.Value;
                }
                else
                {
                    rowIn["DPRJNAME"] = debitList[i].DebitName;
                    rowIn["DPRJRETAILFEE"] = debitList[i].RetailFee;
                    rowIn["DPRJTRADEFEE"] = debitList[i].StockFee;
                }

                dtInventory.Rows.Add(rowIn);
            }

            //期末金额
            DataTable dtEnd = NewDao<IDWDao>().GetAccountData(2, deptId, queryYear, queryMonth, typeId);
            if (dtEnd.Rows.Count > 0)
            {
                //添加期末金额
                DataRow endRow = dtInventory.NewRow();
                endRow["LPRJNAME"] = string.Empty;
                endRow["LPRJRETAILFEE"] = 0;
                endRow["LPRJTRADEFEE"] = 0;
                endRow["DPRJNAME"] = "期末金额";
                endRow["DPRJRETAILFEE"] = Convert.ToDecimal(dtEnd.Compute("sum(OverRetailFee)", string.Empty));
                endRow["DPRJTRADEFEE"] = Convert.ToDecimal(dtEnd.Compute("sum(OverStockFee)", string.Empty));
                dtInventory.Rows.Add(endRow);
            }
            else
            {
                //添加期末金额
                DataRow endRow = dtInventory.NewRow();
                endRow["LPRJNAME"] = string.Empty;
                endRow["LPRJRETAILFEE"] = 0;
                endRow["LPRJTRADEFEE"] = 0;
                endRow["DPRJNAME"] = "期末金额";
                endRow["DPRJRETAILFEE"] = 0;
                endRow["DPRJTRADEFEE"] = 0;
                dtInventory.Rows.Add(endRow);
            }

            //添加合计行
            DataRow sumRow = dtInventory.NewRow();
            sumRow["LPRJNAME"] = "合计";
            sumRow["LPRJRETAILFEE"] = ColumnSum(dtInventory, "LPRJRETAILFEE");
            sumRow["LPRJTRADEFEE"] = ColumnSum(dtInventory, "LPRJTRADEFEE");
            sumRow["DPRJNAME"] = "合计";
            sumRow["DPRJRETAILFEE"] = ColumnSum(dtInventory, "DPRJRETAILFEE");
            sumRow["DPRJTRADEFEE"] = ColumnSum(dtInventory, "DPRJTRADEFEE");
            dtInventory.Rows.Add(sumRow);
            return dtInventory;
        }

        /// <summary>
        /// 取得业务类型名称
        /// </summary>
        /// <param name="code">业务类型编码</param>
        /// <returns>业务类型名称</returns>
        private string GetBusiTypeName(string code)
        {
            List<DGBusiType> list = CreateBusiTypeList();
            DGBusiType m = list.Where(a => a.BusiCode == code).FirstOrDefault();
            return m.BusiTypeName;
        }

        /// <summary>
        /// 查询药品分类明细账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>分类明细账列表</returns>
        public override DataTable OrderAccountReport(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询收发存汇总
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>收发存汇总表</returns>
        public override DataTable RPT_CSPJAccoount(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取头表信息
        /// </summary>
        /// <param name="opType">业务类型</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="detailID">明细Id</param>
        /// <returns>头表信息</returns>
        public BillMasterShower GetBillHeadInfo(string opType, int deptId, int detailID)
        {
            BillMasterShower shower = new BillMasterShower();
            switch (opType)
            {               
                case "111"://采购入库
                case "112"://期初入库
                case "113"://药库退货
                    DW_InStoreDetail inStoreDetail = (DW_InStoreDetail)NewObject<DW_InStoreDetail>().getmodel(detailID);
                    if (inStoreDetail != null)
                    {
                        DW_InStoreHead instoreHead = (DW_InStoreHead)NewObject<DW_InStoreHead>().getmodel(inStoreDetail.InHeadID);
                        if (instoreHead != null)
                        {
                            shower.RegTime = instoreHead.RegTime;
                            shower.AuditTime = instoreHead.AuditTime;
                            shower.BillNo = instoreHead.BillNo.ToString();
                            shower.RelationPeopleNo = instoreHead.OpEmpID.ToString();
                            shower.RelationPeople = instoreHead.OpEmpName;
                            shower.RelationUnit = instoreHead.SupplierName;
                            shower.RetailFee = instoreHead.RetailFee;
                            shower.StockFee = instoreHead.StockFee;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = instoreHead.RegEmpName;
                            shower.Remark = instoreHead.Remark;
                        }
                    }

                    break;
                case "121"://流通出库
                case "122"://内耗出库
                case "123"://药库报损
                case "124"://药库退库
                    DW_OutStoreDetail outStoreDetail = (DW_OutStoreDetail)NewObject<DW_OutStoreDetail>().getmodel(detailID);
                    if (outStoreDetail != null)
                    {
                        DW_OutStoreHead outStoreHead = (DW_OutStoreHead)NewObject<DW_OutStoreHead>().getmodel(outStoreDetail.OutHeadID);
                        if (outStoreHead != null)
                        {
                            shower.RegTime = outStoreHead.RegTime;
                            shower.AuditTime = outStoreHead.AuditTime;
                            shower.BillNo = outStoreHead.BillNO.ToString();
                            shower.RelationPeopleNo = "暂无";
                            shower.RelationPeople = "暂无";
                            shower.RelationUnit = outStoreHead.ToDeptName;
                            shower.RetailFee = outStoreHead.RetailFee;
                            shower.StockFee = outStoreHead.StockFee;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = outStoreHead.RegEmpName;
                            shower.Remark = outStoreHead.Remark;
                        }
                    }

                    break;
                case "142"://盘点审核
                    DW_AuditDetail auditDetail = (DW_AuditDetail)NewObject<DW_AuditDetail>().getmodel(detailID);
                    if (auditDetail != null)
                    {
                        DW_AuditHead auditHead = (DW_AuditHead)NewObject<DW_AuditHead>().getmodel(auditDetail.AuditHeadID);
                        if (auditHead != null)
                        {
                            shower.RegTime = auditHead.AuditTime;
                            shower.AuditTime = auditHead.AuditTime;
                            shower.BillNo = auditHead.BillNO.ToString();
                            shower.RelationPeopleNo = auditHead.EmpID.ToString();
                            shower.RelationPeople = auditHead.EmpName;
                            shower.RelationUnit = GetDeptName(auditHead.DeptID);
                            shower.RetailFee = auditHead.CheckRetailFee;
                            shower.StockFee = auditHead.CheckStockFee;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = auditHead.EmpName;
                            shower.Remark = string.Empty;
                        }
                    }

                    break;
                case "151"://药库调价
                    DG_AdjDetail adjDetail = (DG_AdjDetail)NewObject<DG_AdjDetail>().getmodel(detailID);
                    if (adjDetail != null)
                    {
                        DG_AdjHead adjHead = (DG_AdjHead)NewObject<DG_AdjHead>().getmodel(adjDetail.AdjHeadID);
                        if (adjHead != null)
                        {
                            shower.RegTime = adjHead.RegTime;
                            shower.AuditTime = adjHead.ExecTime;
                            shower.BillNo = adjHead.BillNO.ToString();
                            shower.RelationPeopleNo = adjHead.RegEmpID.ToString();
                            shower.RelationPeople = GetEmpName(adjHead.RegEmpID);
                            shower.RelationUnit = GetDeptName(adjHead.DeptID);
                            shower.RetailFee = 0;
                            shower.StockFee = 0;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = GetEmpName(adjHead.RegEmpID);
                            shower.Remark = adjHead.Remark;
                        }
                    }

                    break;
            }

            return shower;
        }

        /// <summary>
        /// 获取科室名称
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>科室名称</returns>
        private string GetDeptName(int deptId)
        {
            string deptName =string.Empty;
            BaseDept dept = (BaseDept)NewObject<BaseDept>().getmodel(deptId);
            if (dept != null)
            {
                deptName = dept.Name;
            }

            return deptName;
        }

        /// <summary>
        /// 取得用户名
        /// </summary>
        /// <param name="empId">员工Id</param>
        /// <returns>用户名</returns>
        private string GetEmpName(int empId)
        {
            BasicDataManagement baseData = NewObject<BasicDataManagement>();
            return baseData.GetEmpName(empId);
        }
    }
}
