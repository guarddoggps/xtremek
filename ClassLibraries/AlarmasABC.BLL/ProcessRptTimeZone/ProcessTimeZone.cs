using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;
using AlarmasABC.DAL.Select;
using AlarmasABC.DAL.Update;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.BLL.ProcessRptTimeZone
{
    public class ProcessTimeZone : IAlopekBusinessLogic
    {
        private InvokeOperations.operations _mode;
        public ProcessTimeZone(InvokeOperations.operations _mode)
        {
            this._mode = _mode;
        }

        public ProcessTimeZone()
        {
 
        }

        private DataSet _ds;

        /// <summary>
        ///  Set/Get Dataset
        /// </summary>
        
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private int _uID;

        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private float _timeZone;

        public float TimeZone
        {
            get { return _timeZone; }
            set { _timeZone = value; }
        }

 

        public void invoke()
        {
            switch (this._mode)
            { 
                case InvokeOperations.operations.INSERT:
              
                    break;
                case InvokeOperations.operations.SELECT:
  
                    break;
                case InvokeOperations.operations.UPDATE:
                    UpdateTimeZone();
                    break;
                case InvokeOperations.operations.DELETE:
         
                    break;
                default:
                    break;
            }
        }


        private void UpdateTimeZone()
        {
            TimeZoneUpdate _UpTZ = new TimeZoneUpdate();
            try
            {
                _UpTZ.TimeZone = this._timeZone;
                _UpTZ.UID = this._uID;
                _UpTZ.updateTimeZone();
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _UpTZ = null; 
            }
        }

        public static void fillDropDownUsers(System.Web.UI.WebControls.DropDownList ddl, User _user)
        {
            try
            {
                User _U = new User();
                _U = _user;
                new UsersTimeZoneSelect(_U).UserDropDownList(_U._userDropDown);

                ddl.DataSource = _U._userDropDown;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Value";
                ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ddl = null;
            }
        }

        public static void fillDropDownTimeZone(System.Web.UI.WebControls.DropDownList ddl, RptTimeZone _timeZone)
        {
            try
            {
                RptTimeZone _TZ = new RptTimeZone();
                _TZ = _timeZone;

                new TimeZoneSelect(_TZ).TimeZoneDropDownList(_TZ._dropDownList);

                ddl.DataSource = _TZ._dropDownList;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Value";
                ddl.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ddl = null;
            }
        }
 

       
    }
}
