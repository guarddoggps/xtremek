using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.DAL.Queries;

namespace AlarmasABC.Utilities
{
    public class Utility
    {
        public Utility()
        {        
        }

        public static bool isExist(string _SQL)
        {
            bool rc = false;

            ExecuteSQL _execSQL = new ExecuteSQL();
            try
            {
                rc = _execSQL.IsExistData(_SQL);
            }
            catch (Exception ex)
            {
                throw new Exception("Utilities::Utility::"+ex.Message);
            }
            finally
            {
                _execSQL = null;
            }
           
            return rc;
        }

    }
}
