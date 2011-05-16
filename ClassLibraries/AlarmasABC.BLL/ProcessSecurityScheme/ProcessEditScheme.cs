using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Security;
using AlarmasABC.Core.Tracking;
using AlarmasABC.DAL.Insert;
using AlarmasABC.DAL.Delete;

using AlarmasABC.DAL.Update;
using AlarmasABC.DAL.Security.Select;
using AlarmasABC.DAL.Security.Update;
using AlarmasABC.DAL.Security.Delete;

namespace AlarmasABC.BLL.ProcessSecurityScheme
{
    public class ProcessEditScheme : IAlopekBusinessLogic
    {
        #region properties
        private SchemePermission _schemePermission;

        public SchemePermission SchemePermission
        {
            get { return _schemePermission; }
            set { _schemePermission = value; }
        }

        private int _GroupID;

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private int _SId;

        public int SId
        {
            get { return _SId; }
            set { _SId = value; }
        }

        private int _CompanyId;

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        private string _schemeID;
        public string SchemeID
        {
            get { return _schemeID; }
            set { _schemeID = value; }
        }

        private string _schemeName;

        public string SchemeName
        {
            get { return _schemeName; }
            set { _schemeName = value; }
        }

        private string _comID;

        public string ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }

        private bool _defaultScheme;

        public bool DefaultScheme
        {
            get { return _defaultScheme; }
            set { _defaultScheme = value; }
        }

        private DataSet _dsSchemeInfoSelect;
        public DataSet DsSchemeInfoSelect
        {
            get { return _dsSchemeInfoSelect; }
            set { _dsSchemeInfoSelect = value; }
        }
        
        #endregion

        #region Methords
        public ProcessEditScheme()
        {
        }

        public void invoke()
        {
            //getSchemeInfo();
        }

        public void getSchemeInfo()
        {
            try
            {
                SchemeInfoSelect _schemeInforSelect = new SchemeInfoSelect(this.SchemeID);
                _schemeInforSelect.getSchemeInfo();
                this.DsSchemeInfoSelect = _schemeInforSelect.Ds;

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
        }

        public DataSet getMoudule()
        {
            DataSet _ds = new DataSet();
            try
            {
                ModuleSelect _schemeModule = new ModuleSelect(this.SchemeID);

                _ds = _schemeModule.getModule();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public DataSet getNotUserGroup()
        {
            DataSet _ds = new DataSet();
            try
            {
                NotUserGroupSelect _schemeNotUserGroup = new NotUserGroupSelect(this.SchemeID,this.ComID);

                _ds = _schemeNotUserGroup.getNotUserGroup();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public DataSet getUserGroup()
        {
            DataSet _ds = new DataSet();
            try
            {
                UserGroupSelect _schemeUserGroup = new UserGroupSelect(this.SchemeID,this.ComID);
                _ds = _schemeUserGroup.getUserGroup();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }


        public DataSet getNotListedUser()
        {
            DataSet _ds = new DataSet();
            try
            {
                NotListedUserSelect _schemeNotListedUser = new NotListedUserSelect(this.SchemeID, this.ComID);
                _ds = _schemeNotListedUser.getNotListedUser();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public DataSet getListedUser()
        {
            DataSet _ds = new DataSet();
            try
            {
                ListedUserSelect _schemeListedUser = new ListedUserSelect(this.SchemeID, this.ComID);
                _ds = _schemeListedUser.getListedUser();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _ds;
        }

        public int saveSchemeInfo()
        {
            int _i;
            try
            {
                SchemeInfoUpdate  _schemeinfoUpdate = new SchemeInfoUpdate( this.SchemeID, this.SchemeName, int.Parse(this.ComID),this.DefaultScheme);
                _i = _schemeinfoUpdate.saveSchemeInfo();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        public int saveUserWiseScheme()
        {
            int _i;
            try
            {
                UserWiseSchemeUpdate _schemeUserWiseUpdate = new UserWiseSchemeUpdate(this.SchemeID, this.UserID, int.Parse(this.ComID));
                _i = _schemeUserWiseUpdate.saveUserWiseScheme();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }
        public int saveGroupWiseScheme()
        {
            int _i;
            try
            {
                GroupWiseSchemeUpdate _schemeGroupWiseUpdate = new GroupWiseSchemeUpdate(this.GroupID, int.Parse(this.ComID));
                _i = _schemeGroupWiseUpdate.saveGroupWiseScheme();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        public int saveSchemePermission()
        {
            int _i;
            try
            {
                SchemePermissionUpdate _schemePermissionUpdate = new SchemePermissionUpdate(this.SchemePermission);
                _i = _schemePermissionUpdate.saveSchemePermission();

            }
            catch (Exception ex)
            {
                throw new Exception("selectUnit():: " + ex.Message);
            }
            return _i;
        }

        public void deleteSchemeInfo()
        {
            SchemeDelete _skmDel = new SchemeDelete();
            try
            {
                _skmDel.ID = this._SId;
                _skmDel.ComID = this._CompanyId;
                _skmDel.DeleteScheme();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                _skmDel = null;
            }
        }

        #endregion
        

    }
}
