using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_MIInterface.ObjectModel.Common
{
    public struct LogMsg
    {
        public DateTime dtTime;     //产生时间
        public Color cShowColor;    //显示颜色
        public String sLogMessage;  //日志文本内容
        public bool bIsDebugLog;    //是否为调试日志
    }
    public class LogQuenue
    {
        private static List<LogMsg> m_LogMsgQuenue = new List<LogMsg>();

        /// <summary>
        /// 添加到日志队列
        /// </summary>
        /// <param name="clrShowColor">显示颜色</param>
        /// <param name="sLogMsg">日志内容</param>
        /// <param name="bIsDebugLog">是否为调试日志</param>
        public static void Push(Color clrShowColor, String sLogMsg, bool bIsDebugLog)
        {
            LogMsg logmsg = new LogMsg();
            logmsg.dtTime = DateTime.Now;
            logmsg.sLogMessage = sLogMsg;
            logmsg.bIsDebugLog = bIsDebugLog;
            logmsg.cShowColor = clrShowColor;
            m_LogMsgQuenue.Add(logmsg);
        }

        /// <summary>
        /// 添加到日志队列
        /// </summary>
        /// <param name="clrShowColor">显示颜色</param>
        /// <param name="sLogMsg">日志内容</param>
        public static void Push(Color clrShowColor, String sLogMsg)
        {
            LogMsg logmsg = new LogMsg();
            logmsg.dtTime = DateTime.Now;
            logmsg.sLogMessage = sLogMsg;
            logmsg.bIsDebugLog = false;
            logmsg.cShowColor = clrShowColor;
            m_LogMsgQuenue.Add(logmsg);
        }

        /// <summary>
        /// 取得最先的日志
        /// </summary>
        /// <returns>日志信息结构</returns>
        public static bool Pop(ref LogMsg logmsg)
        {
            if (m_LogMsgQuenue.Count == 0)
                return false;

            logmsg = m_LogMsgQuenue[0];
            m_LogMsgQuenue.RemoveAt(0);
            return true;
        }
    }
}
