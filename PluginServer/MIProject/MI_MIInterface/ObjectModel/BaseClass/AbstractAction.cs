using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;

namespace MI_MIInterface.ObjectModel.BaseClass
{
    /// <summary>
    /// 处理对象基类
    /// </summary>
    public abstract class AbstractAction:AbstractObjectModel
    {
                //执行业务开始进行初始化数据
        protected virtual void BeginInitData()
        {
        }
        //执行完成业务后初始化数据
        protected virtual void EndInitData()
        {

        }        
        /// <summary>
        /// 获取第三方接口系统的访问地址
        /// </summary>
        /// <returns></returns>
        public abstract string GetRemoteCommParaDescript();

        public string actionClass
        {
            get
            {
                return this.GetType().FullName;
            }
        }

        //客户端请求执行处理
        public void ExecAction()
        {
            //m_AnsMidParams = new String[MAX_PARAS];   //最大允许MAX_PARAS个参数

            //resultData = new ResultData();
            //resultData.createGuid();
            //resultData.msg = "操作成功";
            //paramData = new ParamData();
            //ReceiveMidData();

            //if (paramData.businessNo != null)
            //{
            //    System.Reflection.MethodInfo method = this.GetType().GetMethod(paramData.businessNo);
            //    if (method == null)
            //    {
            //        resultData.flag = -1;
            //        resultData.msg = "业务号不存在";
            //    }
            //    else
            //    {
            //        try
            //        {
            //            LogQuenue.Push(Color.Black, String.Format("医院ID为：{0}", paramData.hospitalId));
            //            LogQuenue.Push(Color.Black, String.Format("业务号为：{0}", paramData.businessNo));
            //            LogQuenue.Push(Color.Black, String.Format("客户端传入业务数据ID为：{0}", paramData.clientBusinessId));
            //            LogQuenue.Push(Color.Black, "开始执行...");
            //            resultData.flag = 0;

            //            BeginInitData();
            //            method.Invoke(this, null);
            //            EndInitData();

            //            LogQuenue.Push(Color.Black, "执行成功...");
            //        }
            //        catch (Exception err)
            //        {
            //            resultData.flag = -1;
            //            resultData.msg = err.Message;
            //        }
            //    }
            //}
            //else
            //{
            //    resultData.flag = -1;
            //    resultData.msg = "业务号不存在";
            //}
            //ClearAnsMidParams();
            ////应答数据到中间件
            //PushAnsMidParam(1, "0");//客户端收不到?
            //PushAnsMidParam(2, "2");//2-同步 1-异步
            //PushAnsMidParam(3, resultData.flag + "#" + resultData.msg);
            //PushAnsMidParam(4, resultData.serverBusinessId);
            //int iRet = SendAnsMidData(false, true);
            //if (iRet < 0)
            //    RunLog.LogQuenue.Push(Color.Red, String.Format("发送应答数据到中间件失败, 错误代码：{0}", iRet));
            ////执行完移除线程
            //DataConverThread.ThreadRunFinshed(dcbinfo.m_pThread);
        }

    }
}
