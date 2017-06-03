using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MaterialManager.ObjectModel.Store
{
    /// <summary>
    /// 库存处理参数
    /// </summary>
    public class StoreParam
    {
        /// <summary>
        /// 物资id
        /// </summary>
        private int materialId;

        /// <summary>
        /// 科室id
        /// </summary>
        private int deptID;

        /// <summary>
        /// 数量
        /// </summary>
        private decimal amount;

        /// <summary>
        /// 批次号
        /// </summary>
        private string batchNO;

        /// <summary>
        /// 单位id
        /// </summary>
        private int unitID;

        /// <summary>
        /// 单位名称
        /// </summary>
        private string unitName;

        /// <summary>
        /// 有效日期
        /// </summary>
        private DateTime validityTime;

        /// <summary>
        /// 进价
        /// </summary>
        private decimal stockPrice;

        /// <summary>
        /// 零售价
        /// </summary>
        private decimal retailPrice;

        /// <summary>
        /// 操作业务类型
        /// </summary>
        public string BussConstant {  get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public int MaterialId
        {
            get
            {
                return this.materialId;
            }

            set
            {
                this.materialId = value;
            }
        }

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptID
        {
            get
            {
                return this.deptID;
            }

            set
            {
                this.deptID = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                this.amount = value;
            }
        }

        /// <summary>
        /// 库存批号
        /// </summary>
        public string BatchNO
        {
            get
            {
                return this.batchNO;
            }

            set
            {
                this.batchNO = value;
            }
        }

        /// <summary>
        /// 单位ID
        /// </summary>
        public int UnitID
        {
            get
            {
                return this.unitID;
            }

            set
            {
                this.unitID = value;
            }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get
            {
                return this.unitName;
            }

            set
            {
                this.unitName = value;
            }
        }

        /// <summary>
        /// 到效时间
        /// </summary>
        public DateTime ValidityTime
        {
            get
            {
                return this.validityTime;
            }

            set
            {
                this.validityTime = value;
            }
        }

        /// <summary>
        /// 进货价格
        /// </summary>
        public decimal StockPrice
        {
            get
            {
                return this.stockPrice;
            }

            set
            {
                this.stockPrice = value;
            }
        }

        /// <summary>
        /// 零售价格
        /// </summary>
        public decimal RetailPrice
        {
            get
            {
                return this.retailPrice;
            }

            set
            {
                this.retailPrice = value;
            }
        }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string PackUnit { get; set; }

        /// <summary>
        /// 包装系数
        /// </summary>
        public int UnitAmount { get; set; }

        /// <summary>
        /// 包装系数
        /// </summary>
        public int PackAmount { get; set; }

        /// <summary>
        /// 盘存数
        /// </summary>
        public decimal FactAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 账存数
        /// </summary>
        public decimal ActAmount
        {
            get;
            set;
        }
    }
}
