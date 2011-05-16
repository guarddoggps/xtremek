using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmasABC.Core.Admin;
using System.Data;
using Npgsql;

namespace AlarmasABC.DAL.Insert
{
    public class ImageURLInsert : DataAccessBase
    {
        public ImageURLInsert()
        {
           Command = StoredProcedure.Name.SP_INSERT_IMAGE_LOCATION.ToString();
        }

        private ImageURL _iUrl;

        public ImageURL IUrl
        {
            get { return _iUrl; }
            set { _iUrl = value; }
        }

        public void AddImageUrl()
        {
            ImageUrlParams _iPara = new ImageUrlParams(this._iUrl);

            try
            {
                DataBaseHelper _db =new DataBaseHelper(Command,CommandType.StoredProcedure);
                _db.Run(base.ConnectionString, _iPara.Param );
            }
            catch (Exception ex)
            {
                throw new Exception(" Add Image URL " + ex.Message);
            }
            finally
            {
                _iPara = null;
            }

        }


    }

    class ImageUrlParams
    {
       
        
        private ImageURL _imgUrl;

        public ImageUrlParams(ImageURL _iurl)
        {
            this._imgUrl = _iurl;
            build();
        }

       private NpgsqlParameter[] _param;

        public NpgsqlParameter[] Param
        {
            get { return _param; }
            set { _param = value; }
        }

        private void build()
        {

            NpgsqlParameter[] _params = {
                                       


                                       DataBaseHelper.MakeParam("@imageName",    NpgsqlTypes.NpgsqlDbType.Varchar,     50,     ParameterDirection.Input,       this._imgUrl.ImageName),
                                       DataBaseHelper.MakeParam("@imageURL",     NpgsqlTypes.NpgsqlDbType.Varchar,     100,     ParameterDirection.Input,       this._imgUrl.ImageUrl),
                                       //DataBaseHelper.MakeParam("@FURL",     NpgsqlTypes.NpgsqlDbType.Varchar,     200,     ParameterDirection.Input,       this._imgUrl.FUrl),
                                       DataBaseHelper.MakeParam("@comID",    NpgsqlTypes.NpgsqlDbType.Integer,     4,     ParameterDirection.Input,       this._imgUrl.ComID),
                                       DataBaseHelper.MakeParam("@status",    NpgsqlTypes.NpgsqlDbType.Boolean,     1,     ParameterDirection.Input,       this._imgUrl.IsActive),
                                       
                                      
                                     };
            this._param = _params;
        }


    }
}

