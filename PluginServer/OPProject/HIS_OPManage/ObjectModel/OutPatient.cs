using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 获取到符合查询条件的已挂号病人
    /// </summary>
    [Serializable]
    public class OutPatient : AbstractObjectModel
    {
        /// <summary>
        /// 通过指定查询类别和内容获取挂号病人信息
        /// </summary>
        /// <param name="queryType">查询类别</param>
        /// <param name="content">查询内容</param>
        /// <returns>病人信息</returns>
        public List<OP_PatList> GetPatlist(OP_Enum.MemberQueryType queryType,string content)
        {
            List<OP_PatList> patlist = new List<OP_PatList>();
            if (queryType == OP_Enum.MemberQueryType.门诊就诊号)
            {
                if (content.Length <= 3)
                {
                    content = DateTime.Now.ToString("yyyyMMdd") + content;
                }

                patlist = NewObject<OP_PatList>().getlist<OP_PatList>(" visitno='" + content + "'").OrderByDescending(p=>p.RegDate).ToList();
            }
            else if (queryType == OP_Enum.MemberQueryType.退费发票号)
            {
            }
            else
            {
                DataTable dtPatlist = NewObject<IOPManageDao>().GetRegPatList(queryType, content);
                patlist = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_PatList>(dtPatlist).OrderByDescending(p=>p.RegDate).ToList();
            }

            return patlist;
        }

        /// <summary>
        /// 通过组合内容获取挂号病人信息
        /// </summary>
        /// <param name="nameContent">病人姓名</param>
        /// <param name="telContent">电话号码</param>
        /// <param name="idContent">身份证号</param>
        /// <param name="mediCard">医保卡号</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>病人对象</returns>
        public List<OP_PatList> GetPatlist(string nameContent,string telContent,string idContent,string mediCard,DateTime bdate,DateTime edate)
        {
            List<OP_PatList> patlist = new List<OP_PatList>();
            DataTable dtPatlist = NewObject<IOPManageDao>().GetRegPatListByOther(nameContent, telContent, idContent,mediCard, bdate,edate);
            patlist = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_PatList>(dtPatlist);
            return patlist;
        }
    }
}
