using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;using Npgsql;
using System.Data;

namespace AlarmasABC.DAL.Delete
{
   public class DeleteImageInfo:DataAccessBase
    {
        public DeleteImageInfo()
        {

           Command = StoredProcedure.Name.SP_DELETE_IMAGE_INFO.ToString();
        }

        ImageURL _iUrl;

        public ImageURL IUrl
        {
            get { return _iUrl; }
            set { _iUrl = value; }
        }

        private string _iName;

        public string IName
        {
            get { return _iName; }
            set { _iName = value; }
        }

       

        public void DeleteImage()
        {
            try
            {
               DataBaseHelper _db = new DataBaseHelper(Command,CommandType.StoredProcedure);
               _db.Run(base.ConnectionString, returnParam(this._iUrl));
                
              
               
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            { 
            
            }
        }

        

        private NpgsqlParameter[] returnParam(ImageURL _iurl)
        {
            NpgsqlParameter[] _param = { 
                                         DataBaseHelper.MakeParam("@id",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, _iurl.Id )
                                         //DataBaseHelper.MakeParam("@INAME", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Output, )
                                         
                                    };

            return _param;
        }
    }
}
