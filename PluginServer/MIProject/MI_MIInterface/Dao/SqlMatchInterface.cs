using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace MI_MIInterface.Dao
{
    public class SqlMatchInterface : AbstractDao, IMatchInterface
    {

        public bool M_UpdateMIlog(int ybId)
        {
            return true;
        }

    }
}
