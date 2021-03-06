﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;using Npgsql;
using AlarmasABC.Core.Admin;
using AlarmasABC.Core.Tracking;

namespace AlarmasABC.DAL.Security.Update
{
    public class UserWiseSchemeUpdate:DataAccessBase
    {

        public UserWiseSchemeUpdate(int _userID , int _comID)
        {
            this.UserID = _userID;
            this.ComID = _comID;            
           Command = StoredProcedure.Name.SP_UPDATE_USERWISE_SCHEME.ToString();
        }

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
        
        public int saveUserWiseScheme()
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
                                        DataBaseHelper.MakeParam("@userID", NpgsqlTypes.NpgsqlDbType.Bigint,  50,  ParameterDirection.Input,   this.UserID),
                                        DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Bigint,  50,  ParameterDirection.Input,   this.ComID)
                                    };

            return _param;
        }
    }
}
