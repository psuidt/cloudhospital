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
    /// 药房系统账目查询类
    /// </summary>
    class DSAccountQuery : AccountQuery
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
        /// 进销存临时表
        /// </summary>
        /// <returns>临时表</returns>
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
        /// 创建业务类型表
        /// </summary>
        /// <returns>业务类型列表</returns>
        private List<DGBusiType> CreateBusiTypeList()
        {
            List<DGBusiType> list = new List<DGBusiType>()
            {
                new DGBusiType { BusiCode="011", BusiTypeName="采购入库", Remark="药房采购入库业务", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="012", BusiTypeName="期初入库", Remark="药房期初入库业务", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="013", BusiTypeName="流通入库", Remark="药房流通入库业务", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="015", BusiTypeName="药房返库", Remark="药房返库业务", NeedToDept=false, STType=DGEnum.StatisticType.Debit  },
                new DGBusiType { BusiCode="021", BusiTypeName="内耗出库", Remark="药房内耗出库业务类型", NeedToDept=false, STType=DGEnum.StatisticType.Debit  },
                new DGBusiType { BusiCode="022", BusiTypeName="报损出库", Remark="药房报损出库业务类型", NeedToDept=false, STType=DGEnum.StatisticType.Debit  },
                new DGBusiType { BusiCode="031", BusiTypeName="门诊发药", Remark="药房门诊发药", NeedToDept=true, STType=DGEnum.StatisticType.Debit , HeadTableName="DS_OPDispHead t1", JoinExpress="t1.DispHeadID=t2.DispHeadID", DetailTableName="DS_OPDispDetail t2", DetailIdFieldName="t2.DispDetailID", DeptFieldName="IsNull(PresDeptName,'无') as deptname", IsSupplier=false  },
                new DGBusiType { BusiCode="032", BusiTypeName="门诊退药", Remark="药房门诊退药", NeedToDept=false, STType=DGEnum.StatisticType.Lend  },
                new DGBusiType { BusiCode="033", BusiTypeName="住院发药", Remark="药房住院发药", NeedToDept=true, STType=DGEnum.StatisticType.LendAndDebit , HeadTableName="DS_IPDispHead t1", JoinExpress="t1.DispHeadID=t2.DispHeadID", DetailTableName="DS_IPDispDetail t2", DetailIdFieldName="t2.DispDetailID", DeptFieldName="IsNull(dbo.fnGetDeptName(t1.DeptID),'无') as deptname", IsSupplier=false  },
                new DGBusiType { BusiCode="042", BusiTypeName="盘点审核", Remark="药房盘点审核业务类型", NeedToDept=false, STType=DGEnum.StatisticType.LendAndDebit  },
                new DGBusiType { BusiCode="051", BusiTypeName="药房调价", Remark="药房调价业务", NeedToDept=false, STType=DGEnum.StatisticType.LendAndDebit  },
                new DGBusiType { BusiCode="031", BusiTypeName="发药调整", Remark="药房门诊发药", NeedToDept=false, STType=DGEnum.StatisticType.Lend },
                new DGBusiType { BusiCode="032", BusiTypeName="退药调整", Remark="药房门诊退药", NeedToDept=false, STType=DGEnum.StatisticType.Debit  }
            };

            return list;
        }

        /// <summary>
        /// 取得业务类型名
        /// </summary>
        /// <param name="code">业务代码</param>
        /// <returns>业务名</returns>
        private string GetBusiTypeName(string code)
        {
            List<DGBusiType> list = CreateBusiTypeList();
            DGBusiType m = list.Where(a => a.BusiCode == code).FirstOrDefault();
            return m.BusiTypeName;
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
        /// <param name="queryYear">年</param>
        /// <param name="queryMonth">月</param>
        /// <param name="typeId">类型ID</param>
        /// <returns>进销存账</returns>
        public DataTable CSPJReport(int deptId, int queryYear, int queryMonth, int typeId)
        {
            DataTable dtInventory = CreateInventoryTable();

            //期初金额
            DataTable dtOpen = NewDao<IDSDao>().GetAccountData(1, deptId, queryYear, queryMonth, typeId);

            //添加期初金额
            if (dtOpen.Rows.Count > 0)
            {
                DataRow openRow = dtInventory.NewRow();
                openRow["LPRJNAME"] = "期初金额";
                openRow["LPRJRETAILFEE"] = Convert.ToDecimal(dtOpen.Compute("sum(OverRetailFee)", string.Empty));
                openRow["LPRJTRADEFEE"] = Convert.ToDecimal(dtOpen.Compute("sum(OverStockFee)", string.Empty));
                openRow["DPRJNAME"] = string.Empty;
                openRow["DPRJRETAILFEE"] = DBNull.Value;
                openRow["DPRJTRADEFEE"] = DBNull.Value;
                dtInventory.Rows.Add(openRow);
            }
            else
            {
                DataRow openRow = dtInventory.NewRow();
                openRow["LPRJNAME"] = "期初金额";
                openRow["LPRJRETAILFEE"] = DBNull.Value;
                openRow["LPRJTRADEFEE"] = DBNull.Value;
                openRow["DPRJNAME"] = string.Empty;
                openRow["DPRJRETAILFEE"] = DBNull.Value;
                openRow["DPRJTRADEFEE"] = DBNull.Value;
                dtInventory.Rows.Add(openRow);
            }

            //处理业务数据
            List<DGLendModel> lendList = new List<DGLendModel>();//借方列表
            List<DGDebitModel> debitList = new List<DGDebitModel>();//贷方列表
            decimal retailFee = 0;
            decimal stockFee = 0;
            foreach (DGBusiType m in CreateBusiTypeList())
            {
                if (m.NeedToDept)
                {
                    //需要统计到科室供应商
                    DataTable dtData = NewDao<IDSDao>().GetAccountData((m.BusiTypeName == "发药调整" ? 3 : 0), deptId, queryYear, queryMonth, typeId, m.BusiCode, m);
                    if (dtData == null || dtData.Rows.Count <= 0)
                    {
                        continue;
                    }

                    //过滤出部门
                    DataView dv = dtData.DefaultView;
                    DataTable dtDept = dv.ToTable("dept", true, new string[] { "deptname" });
                    foreach (DataRow r in dtDept.Rows)
                    {
                        switch (m.STType)
                        {
                            case DGEnum.StatisticType.Debit:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", "deptname='" + r[0].ToString() + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", "deptname='" + r[0].ToString() + "'"));
                                DGDebitModel debitModel = new DGDebitModel() { DebitName = r[0].ToString() + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                debitList.Add(debitModel);
                                break;
                            case DGEnum.StatisticType.Lend:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", "deptname='" + r[0].ToString() + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", "deptname='" + r[0].ToString() + "'"));
                                DGLendModel lendModel = new DGLendModel() { LendName = r[0].ToString() + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                lendList.Add(lendModel);
                                break;
                            case DGEnum.StatisticType.LendAndDebit:
                                retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", "deptname='" + r[0].ToString() + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", "deptname='" + r[0].ToString() + "'"));
                                DGDebitModel debitModel1 = new DGDebitModel() { DebitName = r[0].ToString() + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                if (retailFee != 0 && stockFee != 0)
                                {
                                    debitList.Add(debitModel1);
                                }

                                retailFee = Convert.ToDecimal(dtData.Compute("sum(LendRetailFee)", "deptname='" + r[0].ToString() + "'"));
                                stockFee = Convert.ToDecimal(dtData.Compute("sum(LendStockFee)", "deptname='" + r[0].ToString() + "'"));
                                DGLendModel lendModel1 = new DGLendModel() { LendName = r[0].ToString() + m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                                if (retailFee != 0 && stockFee != 0)
                                {
                                    lendList.Add(lendModel1);
                                }

                                break;
                        }
                    }
                }
                else
                {
                    DataTable dtData = NewDao<IDSDao>().GetAccountData((m.BusiTypeName == "发药调整" ? 3 : 0), deptId, queryYear, queryMonth, typeId, m.BusiCode);
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
                            if (retailFee != 0 && stockFee != 0)
                            {
                                lendList.Add(lendModel1);
                            }

                            retailFee = Convert.ToDecimal(dtData.Compute("sum(DebitRetailFee)", string.Empty));
                            stockFee = Convert.ToDecimal(dtData.Compute("sum(DebitStockFee)", string.Empty));
                            DGDebitModel debitModel1 = new DGDebitModel() { DebitName = m.BusiTypeName, RetailFee = retailFee, StockFee = stockFee };
                            if (retailFee != 0 && stockFee != 0)
                            {
                                debitList.Add(debitModel1);
                            }

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
            DataTable dtEnd = NewDao<IDSDao>().GetAccountData(2, deptId, queryYear, queryMonth, typeId);
            if (dtEnd.Rows.Count > 0)
            {
                //添加期末金额
                DataRow endRow = dtInventory.NewRow();
                endRow["LPRJNAME"] = string.Empty;
                endRow["LPRJRETAILFEE"] = DBNull.Value;
                endRow["LPRJTRADEFEE"] = DBNull.Value;
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
                endRow["LPRJRETAILFEE"] = DBNull.Value;
                endRow["LPRJTRADEFEE"] = DBNull.Value;
                endRow["DPRJNAME"] = "期末金额";
                endRow["DPRJRETAILFEE"] = DBNull.Value;
                endRow["DPRJTRADEFEE"] = DBNull.Value;
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
        /// 查询对列求和
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="columnName">列名</param>
        /// <returns>对列求和</returns>
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
                case "011"://采购入库                    
                case "012"://期初入库
                case "013"://流通入库
                    DS_InStoreDetail inStoreDetail = (DS_InStoreDetail)NewObject<DS_InStoreDetail>().getmodel(detailID);
                    if (inStoreDetail != null)
                    {
                        DS_InstoreHead instoreHead = (DS_InstoreHead)NewObject<DS_InstoreHead>().getmodel(inStoreDetail.InHeadID);
                        if (instoreHead != null)
                        {
                            shower.RegTime = instoreHead.RegTime;
                            shower.AuditTime = instoreHead.AuditTime;
                            shower.BillNo = instoreHead.BillNO.ToString();
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
                case "021"://内耗出库
                case "022"://报损出库
                    DS_OutStoreDetail outStoreDetail = (DS_OutStoreDetail)NewObject<DS_OutStoreDetail>().getmodel(detailID);
                    if (outStoreDetail != null)
                    {
                        DS_OutStoreHead outStoreHead = (DS_OutStoreHead)NewObject<DS_OutStoreHead>().getmodel(outStoreDetail.OutHeadID);
                        if (outStoreDetail != null)
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
                case "031"://门诊发药
                case "032"://门诊退药
                    DS_OPDispDetail oPDispDetail = (DS_OPDispDetail)NewObject<DS_OPDispDetail>().getmodel(detailID);
                    if (oPDispDetail != null)
                    {
                        DS_OPDispHead oPDispHead = (DS_OPDispHead)NewObject<DS_OPDispHead>().getmodel(oPDispDetail.DispHeadID);
                        if (oPDispHead != null)
                        {
                            shower.RegTime = oPDispHead.DispTime;
                            shower.AuditTime = oPDispHead.DispTime;
                            shower.BillNo = oPDispHead.BillNO.ToString();
                            shower.RelationPeopleNo = "暂无";
                            shower.RelationPeople = "暂无";
                            shower.RelationUnit = oPDispHead.PresDeptName;
                            shower.RetailFee = oPDispHead.RetailFee;
                            shower.StockFee = 0;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = oPDispHead.PharmacistName;
                            shower.Remark = string.Empty;
                        }
                    }

                    break;
                case "033"://住院发药
                    DS_IPDispDetail iPDispDetail = (DS_IPDispDetail)NewObject<DS_IPDispDetail>().getmodel(detailID);
                    if (iPDispDetail != null)
                    {
                        DS_IPDispHead iPDispHead = (DS_IPDispHead)NewObject<DS_IPDispHead>().getmodel(iPDispDetail.DispHeadID);
                        if (iPDispHead != null)
                        {
                            shower.RegTime = iPDispHead.DispTime;
                            shower.AuditTime = iPDispHead.DispTime;
                            shower.BillNo = iPDispHead.BillNO.ToString();
                            shower.RelationPeopleNo = "暂无";
                            shower.RelationPeople = "暂无";
                            shower.RelationUnit = GetDeptName(iPDispHead.DeptID);
                            shower.RetailFee = iPDispHead.RetailFee;
                            shower.StockFee = 0;
                            shower.OpType = GetBusiTypeName(opType);
                            shower.RegPeople = GetEmpName(iPDispHead.PharmacistID);
                            shower.Remark = string.Empty;
                        }
                    }

                    break;
                case "042"://盘点审核
                    DS_AuditDetail auditDetail = (DS_AuditDetail)NewObject<DS_AuditDetail>().getmodel(detailID);
                    if (auditDetail != null)
                    {
                        DS_AuditHead auditHead = (DS_AuditHead)NewObject<DS_AuditHead>().getmodel(auditDetail.AuditHeadID);
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
                case "051"://药房调价
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
            string deptName = string.Empty;
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
