using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using System.Data;

namespace SiInterface.Dao
{
    public class SqlMIDao : AbstractDao, IMIDao
    {
        public DataTable GetBooks(string searchChar, int flag)
        {
            string strsql = @"SELECT * FROM Books WHERE BookName LIKE '%{0}%' AND Flag={1}";
            strsql = string.Format(strsql, searchChar, flag);
            return oleDb.GetDataTable(strsql);
        }
    }
}
