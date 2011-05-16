using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AlarmasABC.Core.Security;
using AlarmasABC.DAL.Security.Select;


namespace AlarmasABC.BLL.ProcessSecurityScheme
{
    public class ProcessNewScheme:IAlopekBusinessLogic
    {
        public InvokeOperations.operations mode;

        public ProcessNewScheme()
        {
            ///Default Constructor
        }

        public ProcessNewScheme(InvokeOperations.operations mode)
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


        public void fillUserGroup(System.Web.UI.WebControls.ListBox lstbx)
        {
            SchemeGroupListSelect _grpSelect = new SchemeGroupListSelect();
            Group Grp = new Group();
            try
            {

                _grpSelect.ComID = this._comID;
                _grpSelect.LoadGroupList(Grp._List);

                lstbx.DataSource = Grp._List;
                lstbx.DataTextField = "name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _grpSelect = null;
                Grp = null;

            }
        }


        public void fillUsers(System.Web.UI.WebControls.ListBox lstbx)
        {
            SchemeUserListSelect _usrSelect = new SchemeUserListSelect();
            User _Usr = new User();
            try
            {

                _usrSelect.ComID = this._comID;
                _usrSelect.LoadUserList(_Usr._list);

                lstbx.DataSource = _Usr._list;
                lstbx.DataTextField = "name";
                lstbx.DataValueField = "Value";
                lstbx.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _usrSelect = null;
                _Usr = null;

            }
        }

        public void loadModuleInfo()
        {
            SchemeModuleSelect _skmModule = new SchemeModuleSelect();
            try
            {
                _skmModule.selectSchemeModule();
                this._ds = _skmModule.Ds;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _skmModule = null;
            }
        }
    }
}
