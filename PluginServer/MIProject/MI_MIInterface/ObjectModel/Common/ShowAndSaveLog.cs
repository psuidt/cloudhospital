using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MI_MIInterface.ObjectModel.Common
{
    public delegate void ShowLogMethod(System.Drawing.Color clr, String msg);//更新显示代理程序
    public class ShowAndSaveLog
    {
        private static FileStream m_fLog = null;            //日志文件
        private static DateTime m_LogFileDate;              //日志文件
        private static Thread m_thrProcLog = null;          //处理日志记录线程
        private static ShowLogMethod ShowLog = null;

        //创建日志文件
        public static void InitShowAndSaveLog(ShowLogMethod _showLog)
        {
            if (ShowLog == null)
            {
                ShowLog = _showLog;
                //创建日志文件
                String sLogFileName = Application.StartupPath + "\\Log";
                if (Directory.Exists(sLogFileName) == false)
                    Directory.CreateDirectory(sLogFileName);

                m_LogFileDate = DateTime.Now.Date;
                sLogFileName += "\\Log" + m_LogFileDate.ToString("yyyy-MM-dd") + ".Log";
                m_fLog = File.Open(sLogFileName, FileMode.Append, FileAccess.Write);
            }
        }

        //写异常日志方法
        public static void WriteLog(string Msg)
        {
            String sLogFileName = Application.StartupPath + "\\Log";
            if (Directory.Exists(sLogFileName) == false)
                Directory.CreateDirectory(sLogFileName);

            m_LogFileDate = DateTime.Now.Date;
            sLogFileName += "\\Log_Ex_" + m_LogFileDate.ToString("yyyy-MM-dd") + ".Log";
            File.AppendAllText(sLogFileName, Msg);
        }

        ~ShowAndSaveLog()
        {
            m_fLog.Close();
        }

        public static void StartProcLog()
        {
            if (m_thrProcLog == null)
            {
                m_thrProcLog = new Thread(new ThreadStart(ProcessLogMsg));
                m_thrProcLog.Priority = ThreadPriority.BelowNormal;
                m_thrProcLog.Start();
            }
        }

        public static void StopProcLog()
        {
            if (m_thrProcLog != null && m_thrProcLog.IsAlive)
            {
                m_thrProcLog.Abort();
                if (m_thrProcLog.ThreadState == ThreadState.Suspended)
                    m_thrProcLog.Resume();
                m_thrProcLog.Join();
                m_thrProcLog = null;
            }
        }

        #region 日志处理线程执行函数
        private static void ProcessLogMsg()
        {
            while (m_thrProcLog.IsAlive)
            {
                try
                {
                    LogMsg logMsg = new LogMsg();
                    if (LogQuenue.Pop(ref logMsg))
                    {
                        //检查日期是否有已经改变，文件名也要随着变
                        if (DateTime.Now.Date != m_LogFileDate)
                        {
                            m_fLog.Close();
                            m_LogFileDate = DateTime.Now.Date;
                            String sLogFileName = Application.StartupPath + "\\Log\\Log" + m_LogFileDate.ToString("yyyy-MM-dd") + ".Log";
                            m_fLog = File.Open(sLogFileName, FileMode.Append, FileAccess.Write);
                        }
                        String sMsg = String.Format("{0}.{1} -> {2}\r\n",
                                        logMsg.dtTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                        logMsg.dtTime.Millisecond,
                                        logMsg.sLogMessage);
                        if (logMsg.bIsDebugLog == false)
                        {
                            //保存到文件
                            Byte[] uMsg = System.Text.Encoding.Default.GetBytes(sMsg);
                            m_fLog.Write(uMsg, 0, uMsg.Length);
                        }
                        //在主窗口显示
                        ShowLog(logMsg.cShowColor, sMsg);
                        continue;
                    }
                    else
                        Thread.Sleep(500);
                }
                catch (ThreadAbortException e)
                {
                    //捕获线程退出异常
                    Thread.ResetAbort();
                    break;
                }
            }
        }
        #endregion

    }
}
