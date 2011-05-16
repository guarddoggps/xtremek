using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ErrorReports
{
    public class ProcessEvents:IAlopekBusinessLogic
    {
        #region Private variables and Properties

        private int _userID;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _comID;
        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
        #endregion

        public void invoke()
        {
            SelectEventList _events = new SelectEventList();

            try
            {
                _events.UserID = this.UserID;
                _events.ComID = this.ComID;
                _events.GetEvents();
                this.Ds = _events.Ds;
            }
            catch (Exception ex)
            {
				throw new Exception ("ProcessEvents::invoke(): " + ex.Message);
            }
            finally
            {
                _events = null;
            }
        }
    }
}
