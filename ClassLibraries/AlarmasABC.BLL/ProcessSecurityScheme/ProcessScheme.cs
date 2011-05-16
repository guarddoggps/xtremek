using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Security;
using AlarmasABC.DAL.Security.Select;
using AlarmasABC.DAL.Select;

namespace AlarmasABC.BLL.ProcessSecurityScheme
{
    public class ProcessScheme:IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode;

        public ProcessScheme()
        {
            ///Default Constructor
        }

        public ProcessScheme(InvokeOperations.operations mode)
        {
            this.mode = mode;
        }

        #region instance variable and propertirs
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _schemeName;

        public string SchemeName
        {
            get { return _schemeName; }
            set { _schemeName = value; }
        }
        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        private bool defaultScheme;

        public bool DefaultScheme
        {
            get { return defaultScheme; }
            set { defaultScheme = value; }
        }
        #endregion

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void invoke()
        {
            switch (this.mode)
            {
                case InvokeOperations.operations.SELECT:
                    selectScheme();
                    break;
                case InvokeOperations.operations.INSERT:
                    
                    break;
                case InvokeOperations.operations.UPDATE:
                    
                    break;
                case InvokeOperations.operations.DELETE:
                    
                    break;
                default: break;

            }
        }

        private void selectScheme()
        {
            SchemeSelect _schmSelect = new SchemeSelect();
            try
            {
                _schmSelect.ComID = this._comID;
                _schmSelect.selectScheme();
                this._ds = _schmSelect.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _schmSelect = null;
            }
        }

        public void selectSchemeUnits()
        {
            SchemeUnitsSelect _schmUnits = new SchemeUnitsSelect();
            try
            {
                _schmUnits.ID = this._ID;
                _schmUnits.selectSchemeUnits();
                this._ds = _schmUnits.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _schmUnits = null;
            }

        }


        public void selectSchemeUsers()
        {
            SchemeUsersSelect _schmUsers = new SchemeUsersSelect();
            try
            {
                _schmUsers.ID = this._ID;
                _schmUsers.selectSchemeUsers();
                this._ds = _schmUsers.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _schmUsers = null;
            }
        }

        /// <summary>
        /// load dropdownlist 
        /// </summary>
        /// <param name="ddl">System.Web.UI.WebControls.DropDownList ddl</param>
        public static void fillDropDownItems(System.Web.UI.WebControls.DropDownList ddl,int _comID)
        {
            try
            {

                SecurityScheme _schm = new SecurityScheme();
                SecuritySchemeSelect _schemeSelect = new SecuritySchemeSelect();
                _schemeSelect.ComID = _comID;
                _schemeSelect.SchemeDropDownList(_schm._schemeList);

                ddl.DataSource = _schm._schemeList;
                ddl.DataTextField = "SchemeName";
                ddl.DataValueField = "ID";
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
