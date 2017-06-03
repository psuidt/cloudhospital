using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiInterface.Dao
{
    public interface IMIDao
    {
        DataTable GetBooks(string searchChar, int flag);
    }
}
