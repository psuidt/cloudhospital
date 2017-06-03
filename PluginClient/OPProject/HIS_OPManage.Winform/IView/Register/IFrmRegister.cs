using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 门诊挂号界面接口类
    /// </summary>
    public  interface IFrmRegister:IBaseView
    {
        /// <summary>
        /// 当天挂号所有科室
        /// </summary>
        DataTable dtAllDept { get; set; }

        /// <summary>
        /// 当天挂号所有医生
        /// </summary>
        DataTable dtAllDoctor { get; set; }

        /// <summary>
        /// 绑定挂号类别
        /// </summary>
        /// <param name="dtRegType">挂号类别数据</param>
        void LoadRegType(DataTable dtRegType);

        /// <summary>
        /// 绑定挂号科室列表
        /// </summary>
        /// <param name="dtRegDepts">挂号科室</param>
        void LoadRegDepts(DataTable dtRegDepts);

        /// <summary>
        /// 绑定挂号医生列表
        /// </summary>
        /// <param name="dtRegDoctors">挂号医生</param>
        void LoadRegDoctors(DataTable dtRegDoctors);

        /// <summary>
        /// 绑定卡类型列表
        /// </summary>
        /// <param name="dtCardType">卡类型数据</param>
        void LoadCardType(DataTable dtCardType);
        
        /// <summary>
        /// 获取选择的卡类型
        /// </summary>
        int GetCardTypeID { get; }

        /// <summary>
        /// 获取卡号码
        /// </summary>
        string cardNo { get; }

       /// <summary>
       /// 电话号码
       /// </summary>
        string Mobile { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        string Address { get; set; }
      
        /// <summary>
        /// 年龄
        /// </summary>
        string Age { get; set; }

        /// <summary>
        /// 当前挂号病人对象
        /// </summary>
        OP_PatList CurPatlist { get; set; }

        /// <summary>
        /// 年龄单位
        /// </summary>
        string AgeUnit { get; set; }

        /// <summary>
        /// 时间 1上午2下午3晚上
        /// </summary>
        int timeRangeIndex { get; set; }

        /// <summary>
        /// 绑定病人类别列表
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        void LoadPatType(DataTable dtPatType);

        /// <summary>
        /// 当前挂号类别ID
        /// </summary>
        int GetCurRegTypeid { get; }

        /// <summary>
        /// 绑定操作员当天挂号病人信息
        /// </summary>
        /// <param name="dtRegInfo">挂号信息</param>
        void BindRegInfoByOperator(DataTable dtRegInfo);

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        void SetGridColor();

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
        string MedicardReadInfo { get; set; }

        /// <summary>
        /// 显示医保刷卡显示信息
        /// </summary>
        string SetMedicardReadInfo {set; }

        /// <summary>
        /// 挂完号后清空界面控件显示信息
        /// </summary>
         void ClearInfo();

        /// <summary>
        /// 早上开始时间
        /// </summary>
        string RegMoningBTime { get; set; }

        /// <summary>
        /// 下午开始时间
        /// </summary>
        string RegAfternoonBtime { get; set; }

        /// <summary>
        /// 晚上开始时间
        /// </summary>
        string RegNightBtime { get; set; }

        /// <summary>
        /// 新增会员后获取焦点
        /// </summary>
        void AddMemberSetFocus();        
    }
}
