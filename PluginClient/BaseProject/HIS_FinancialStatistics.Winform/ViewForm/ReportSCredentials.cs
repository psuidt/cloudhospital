using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
   /// <summary>
   /// 报表加密
   /// </summary>
   public  class ReportSCredentials: ICredentials
    {
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;

        /// <summary>
        /// 密码
        /// </summary>
        private string passWord;

        /// <summary>
        /// 构造函数
        /// Initializes a new instance of the <see cref="ReportSCredentials"/> class.
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">用户密码</param>
        public ReportSCredentials(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;
        }

        /// <summary>
        /// 传入用户名和密码
        /// </summary>
        /// <param name="uri">报表服务地址</param>
        /// <param name="authType">登陆方式</param>
        /// <returns>用户名和密码的令牌</returns>
        public NetworkCredential GetCredential(Uri uri, string authType)
        {
            NetworkCredential net = new NetworkCredential(this.userName, this.passWord);
            return net;
        }
    }
}
