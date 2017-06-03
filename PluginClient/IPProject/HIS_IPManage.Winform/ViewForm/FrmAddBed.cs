using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 批量加床界面
    /// </summary>
    public partial class FrmAddBed : BaseFormBusiness, IAddBed
    {
        /// <summary>
        /// 病区ID
        /// </summary>
        private int wardID;

        /// <summary>
        /// 病区ID
        /// </summary>
        public int WardID
        {
            get
            {
                return wardID;
            }

            set
            {
                wardID = value;
            }
        }

        /// <summary>
        /// 病房号
        /// </summary>
        private string roomNO = string.Empty;

        /// <summary>
        /// 病房号
        /// </summary>
        public string RoomNO
        {
            get
            {
                return roomNO;
            }

            set
            {
                roomNO = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAddBed()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">当前界面</param>
        /// <param name="e">事件参数</param>
        private void FrmAddBed_Load(object sender, EventArgs e)
        {
            // 做成网格DataTable
            DataTable wardBedListDt = new DataTable("WardBedList");
            wardBedListDt.Columns.Add("BedID", typeof(int));
            wardBedListDt.Columns.Add("DeptID", typeof(int));
            wardBedListDt.Columns.Add("WardID", typeof(int));
            wardBedListDt.Columns.Add("RoomNO", typeof(string));
            wardBedListDt.Columns.Add("BedNO", typeof(string));
            wardBedListDt.Columns.Add("BedDoctorID", typeof(int));
            wardBedListDt.Columns.Add("BedNurseID", typeof(int));
            wardBedListDt.Columns.Add("IsPlus", typeof(int));
            wardBedListDt.Columns.Add("PatListID", typeof(int));
            wardBedListDt.Columns.Add("PatName", typeof(string));
            wardBedListDt.Columns.Add("PatSex", typeof(string));
            wardBedListDt.Columns.Add("PatDeptID", typeof(int));
            wardBedListDt.Columns.Add("PatDoctorID", typeof(int));
            wardBedListDt.Columns.Add("PatNurseID", typeof(int));
            wardBedListDt.Columns.Add("BabyID", typeof(int));
            wardBedListDt.Columns.Add("IsUsed", typeof(int));
            wardBedListDt.Columns.Add("IsStoped", typeof(int));
            wardBedListDt.Columns.Add("WorkID", typeof(int));
            drgBedList.DataSource = wardBedListDt;
        }

        /// <summary>
        /// 关闭添加病床界面
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭当前窗体
            InvokeController("Close", this);
        }
    }
}