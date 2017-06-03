using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 住院结算查询控制器
    /// </summary>
    [WCFController]
    public class IPCostSearchController : WcfServerController
    {
        /// <summary>
        /// 获取查询条件基础数据
        /// </summary>
        /// <returns>查询条件基础数据</returns>
        [WCFMethod]
        public ServiceResponseData SetMaterData()
        {
            // 取得收费员列表
            DataSet ds = new DataSet();
            DataTable cashierDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.收费员, true);
            cashierDt.TableName = "CashierDt";
            ds.Merge(cashierDt);
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, true);
            deptDt.TableName = "deptDt";
            ds.Merge(deptDt);
            //ds.Tables.Add(deptDt);
            // 调用共通接口查询病人类型列表
            DataTable patTypeList = NewObject<BasicDataManagement>().GetPatType(true);
            patTypeList.TableName = "patTypeList";
            ds.Merge(patTypeList);
            //ds.Tables.Add(patTypeList);
            // 调用共通接口查询医生列表
            DataTable empDataDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.医生, true);
            empDataDt.TableName = "EmpDataDt";
            //ds.Tables.Add(EmpDataDt);
            ds.Merge(empDataDt);
            responseData.AddData(ds);
            return responseData;
        }

        /// <summary>
        /// 住院病人费用查询
        /// </summary>
        /// <returns>住院病人费用信息</returns>
        [WCFMethod]
        public ServiceResponseData IpCostSearchQuery()
        {
            Dictionary<string, object> paramsDic = requestData.GetData<Dictionary<string, object>>(0);
            DataTable dtPayMentData = NewDao<IIPCostSearchDao>().GetCostData(paramsDic);
            DataTable dtPayMent = NewObject<Basic_Payment>().gettable(" Delflag=0");
            DataTable dt = DtTrans(dtPayMentData, "CostHeadID", 16, dtPayMent, "PayName", "PayName", "CostMoney");
            responseData.AddData(dt);
            DataTable dtItemData = NewDao<IIPCostSearchDao>().GetCostDataGroupItem(paramsDic);
            DataTable dtItems = NewObject<Basic_StatItemSubclass>().gettable(" SubType=6 AND Delflag=0");
            DataTable dtItem = DtTrans(dtItemData, "CostHeadID", 16, dtItems, "SubName", "FpItemName", "ItemFee");
            responseData.AddData(dtItem);
            return responseData;
        }

        /// <summary>
        /// dataTable行转列
        /// </summary>
        /// <param name="dtOld">原来DataTable</param>
        /// <param name="pxzd">第一行需比较的字段</param>
        /// <param name="notChangeColumns">不需要改变的字段数</param>
        /// <param name="dtAddColumns">需增加的列数据datatble</param>
        /// <param name="dtaddColumnName">取增的列数据原的列名</param>
        /// <param name="addColumnName">需比较的列名</param>
        /// <param name="addColumnValue">赋值的列名的值</param>
        /// <returns>转换后的DataTable</returns>
        private DataTable DtTrans(DataTable dtOld, string pxzd, int notChangeColumns, DataTable dtAddColumns, string dtaddColumnName, string addColumnName, string addColumnValue)
        {
            DataTable dtNew = dtOld.Clone();
            dtNew.Rows.Clear();
            dtNew.Columns.RemoveAt(17);
            dtNew.Columns.RemoveAt(16);
            for (int i = 0; i < dtAddColumns.Rows.Count; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = dtAddColumns.Rows[i][dtaddColumnName].ToString();
                col.DataType = typeof(decimal);
                dtNew.Columns.Add(col);
            }

            int j;
            if (dtOld.Rows.Count > 0)
            {
                for (int i = 0; i < dtOld.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();

                    for (int col = 0; col < notChangeColumns; col++)
                    {
                        row[col] = dtOld.Rows[i][col];
                    }

                    for (int k = notChangeColumns; k < dtNew.Columns.Count; k++)
                    {
                        if (dtOld.Rows[i][addColumnName].ToString().Trim() == dtNew.Columns[k].ColumnName.ToString().Trim())
                        {
                            row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(Convert.ToDecimal(dtOld.Rows[i][addColumnValue]).ToString("0.00"));
                            break;
                        }
                    }

                    for (j = i + 1; j < dtOld.Rows.Count; j++)
                    {
                        if (dtOld.Rows[j][pxzd].ToString().Trim() == dtOld.Rows[i][pxzd].ToString().Trim())
                        {
                            for (int k = notChangeColumns; k < dtNew.Columns.Count; k++)
                            {
                                if (dtOld.Rows[j][addColumnName].ToString().Trim() == dtNew.Columns[k].ColumnName.ToString().Trim())
                                {
                                    // row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(dtOld.Rows[j][addColumnValue]);
                                    row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(row[dtNew.Columns[k].ColumnName] == DBNull.Value ? 0 : row[dtNew.Columns[k].ColumnName]) + Convert.ToDecimal(dtOld.Rows[j][addColumnValue]);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    i = j - 1;
                    dtNew.Rows.Add(row);
                }

                if (dtNew.Rows.Count > 0)
                {
                    DataRow dr = dtNew.NewRow();
                    dr[3] = "合计";
                    for (int col = 13; col < dtNew.Columns.Count; col++)
                    {
                        dr[col] = dtNew.Compute("sum(" + dtNew.Columns[col].ColumnName + ")", "true");
                    }

                    dtNew.Rows.Add(dr);
                    int count = dtNew.Columns.Count - 1;
                    for (int col = count; col >= 16; col--)
                    {
                        if (dtNew.Rows[dtNew.Rows.Count - 1][col] == DBNull.Value/* || Convert.ToDecimal(dtNew.Rows[dtNew.Rows.Count - 1][col]) == 0*/)
                        {
                            dtNew.Columns.RemoveAt(col);
                        }
                    }
                }
            }

            return dtNew;
        }

        /// <summary>
        /// 查询结算明细记录
        /// </summary>
        /// <returns>结算明细记录</returns>
        [WCFMethod]
        public ServiceResponseData GetCostDetail()
        {
            int costHeadID = requestData.GetData<int>(0);
            DataTable feeDt = NewDao<IIPCostSearchDao>().GetCostDetail(costHeadID);
            responseData.AddData(feeDt);
            return responseData;
        }
    }
}
