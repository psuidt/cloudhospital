using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.DrugManage;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Bill;

namespace HIS_DrugManger.ObjectModel.Report
{
    /// <summary>
    /// 药库库存处理器
    /// </summary>
    public class DWInventoryStatistic : AbstractObjectModel
    {
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

        public DataTable GetInventoryStatistic(int deptId, int queryYear, int queryMonth, int typeId)
        {
            DataTable dtInventory = new DataTable();
            return dtInventory;
        }
    }
}
