using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Update
{
 public  class ImageInfoUpdate:DataAccessBase
    {
        public ImageInfoUpdate()
        {

           Command = StoredProcedure.Name.SP_UPDATE_IMAGE_LOCATION.ToString();
        }

        ImageURL _iUrl;

        public ImageURL IUrl
        {
            get { return _iUrl; }
            set { _iUrl = value; }
        }  

        private DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }

        public void UpdateImageInfo()
        {
            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                this._ds = _db.Run(base.ConnectionString, returnParam(this._iUrl));
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
                                         DataBaseHelper.MakeParam("@id", NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, _iurl.Id ),
                                         DataBaseHelper.MakeParam("@imageURL", NpgsqlTypes.NpgsqlDbType.Varchar,  100,  ParameterDirection.Input, _iurl.ImageUrl ),
                                         DataBaseHelper.MakeParam("@comID",  NpgsqlTypes.NpgsqlDbType.Integer,  4,  ParameterDirection.Input, _iurl.ComID ),
                                         DataBaseHelper.MakeParam("@status", NpgsqlTypes.NpgsqlDbType.Boolean,  1,  ParameterDirection.Input, _iurl.IsActive )
                                    };

            return _param;
        }
    }
}
