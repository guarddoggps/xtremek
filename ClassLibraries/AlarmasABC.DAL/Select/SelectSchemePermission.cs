using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;

namespace AlarmasABC.DAL.Select
{
   public class SelectSchemePermission:DataAccessBase
    {
                
        public SelectSchemePermission()
        {
			Command = @"SELECT * FROM tblSchemePermission JOIN tblForms ON" +
					  @" (tblSchemePermission.formID = tblForms.id) WHERE schemeID = " +
            		  @"(SELECT schemeID FROM tblUserWiseScheme WHERE userID = :userID);";         
        }
        public SelectSchemePermission(string module)
        {
           Command = StoredProcedure.Name.SP_SELECT_MODULEALL.ToString();
        }

        private DataSet _ds;

        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        private User _userObj;

        public User UserObj
        {
            get { return _userObj; }
            set { _userObj = value; }
        }


        public void GetSchemePermission()
        {
            makePermissionParam _mp = new makePermissionParam(this._userObj);

            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString, _mp._params1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _mp = null;
            }
        }

        public void GetModule()
        {
            try
            {
                DataBaseHelper _db = new DataBaseHelper(Command, CommandType.Text);
                this._ds = _db.Run(base.ConnectionString);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {

            }
        }
    }

    class makePermissionParam
    {

        private User _userObj;

        public User UserObj
        {
            get { return _userObj; }
            set { _userObj = value; }
        }

       private NpgsqlParameter[] _params;

        public NpgsqlParameter[] _params1
        {
            get { return _params; }
            set { _params = value; }
        }


        public makePermissionParam(User _user)
        {
            this._userObj = _user;
            build();
        }

        private void build()
        {
            NpgsqlParameter[] _param = { 
                                    
                                        DataBaseHelper.MakeParam("userID",NpgsqlTypes.NpgsqlDbType.Integer,4,   ParameterDirection.Input,_userObj.UID )
                                         
                                                                              
                                    };
            this._params1 = _param;
        }



    }
}
