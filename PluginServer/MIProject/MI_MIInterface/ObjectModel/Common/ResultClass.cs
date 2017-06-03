using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_MIInterface.ObjectModel.Common
{
    public class ResultClass
    {
        private bool _bSucess = false;
        /// <summary>
        /// 执行是否成功标志
        /// </summary>
        public bool bSucess { set { _bSucess = value; } get { return _bSucess; } }

        private string _sMemo = "";
        /// <summary>
        /// 返回的警告和错误信息
        /// </summary>
        public string sMome{ set { _sMemo = value; } get { return _sMemo; } }

        private object _oResult = null;
        public object oResult{ set { _oResult = value; }  get { return _oResult; } }

    }
}
