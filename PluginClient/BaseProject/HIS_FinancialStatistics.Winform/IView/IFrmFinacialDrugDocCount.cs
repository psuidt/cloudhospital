using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_FinancialStatistics.Winform.IView
{
    /// <summary>
    /// 药品医生窗体接口
    /// </summary>
    public interface IFrmFinacialDrugDocCount
    {
        /// <summary>
        /// 设置机构
        /// </summary>
        /// <param name="dtWorker">医疗机构数据</param>
        void SetWorkers(DataTable dtWorker);

        /// <summary>
        /// 设置医生
        /// </summary>
        /// <param name="dtDoctor">医生数据</param>
        void SetDoctors(DataTable dtDoctor);

        /// <summary>
        /// 加载报表数据
        /// </summary>
        /// <param name="dtData">报表数据</param>
        void LoadData(DataTable dtData);
    }
}
