using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Update
{
    public class GroupWiseSchemeUpdate:DataAccessBase
    {

        public GroupWiseSchemeUpdate(int _GroupID , int _comID)
        {
            this.GroupID = _GroupID;
            this.ComID = _comID;
           Command = StoredProcedure.Name.SP_USERGROUPWISESCHEME.ToString();
        }

        private int _GroupID;

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        private int _comID;

        public int ComID
        {
            get { return _comID; }
            set { _comID = value; }
        }
        
        public int saveGroupWiseScheme()
        {
            DataSet _ds = new DataSet();
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command, CommandType.Text);                
                _ds= _db.Run(base.ConnectionString,returnParam());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
            return int.Parse(_ds.Tables[0].Rows[0][0].ToString());
        }

        
       private NpgsqlParameter[] returnParam()
        {
            NpgsqlParameter[] _param = { 
                                        DataBaseHelper.MakeParam("@GroupID", NpgsqlTypes.NpgsqlDbType.Bigint,  50,  ParameterDirection.Input,   this.GroupID),
                                        DataBaseHelper.MakeParam("@COMID",  NpgsqlTypes.NpgsqlDbType.Bigint,  50,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }
    }
}
